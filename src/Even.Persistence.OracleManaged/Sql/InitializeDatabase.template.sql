declare 
    table_count integer;
begin

  execute immediate('alter session set current_schema ={{Schema}}');
  
  select count (object_id) into table_count from user_objects where exists (
    select object_name from user_objects where (object_name = upper('{{Events}}') and object_type = 'TABLE'));
    
  if table_count = 0 then
    dbms_output.put_line('Creating the {{Events}} table');
    execute immediate (
      'create table {{Events}} (
        GlobalSequence number not null,
        EventID varchar2(36) not null,
        StreamHash raw(20) not null,
        StreamName varchar2(200) not null,
        EventType varchar2(50) not null,
        UtcTimeStamp timestamp not null,
        Metadata blob,
        Payload blob not null,
        PayloadFormat number not null,
        constraint PK_{{Events}} primary key (GlobalSequence))');
        
    execute immediate ('create index IX_{{Events}}_ on {{Events}} (StreamHash)');

    execute immediate ('create sequence {{Events}}_GlobSeq');
    
    execute immediate ('
    create or replace trigger {{Events}}_GlobSeq_Trig 
    before insert on {{Events}} 
    for each row
    begin
      select {{Events}}_GlobSeq.nextval
      into   :new.GlobalSequence
      from   dual;
    end;');
  else
    dbms_output.put_line ('The {{Events}} table already exists in the database.');
  end if;
  
  select count (object_id) into table_count from user_objects where exists (
    select object_name from user_objects where (object_name = upper('{{ProjectionIndex}}') and object_type = 'TABLE'));
  if table_count = 0 then
    dbms_output.put_line('Creating the {{ProjectionIndex}} table');
    
    execute immediate (
    'create table {{ProjectionIndex}} (
      ProjectionStreamHash raw(20) not null,
      ProjectionStreamSequence number(19) not null,
      GlobalSequence number(19) not null,
      constraint PK_{{ProjectionIndex}} primary key (ProjectionStreamHash, ProjectionStreamSequence)
    )');
  else
   dbms_output.put_line ('The {{ProjectionIndex}} table already exists in the database.');
  end if;
  
  select count (object_id) into table_count from user_objects where exists (
    select object_name from user_objects where (object_name = upper('{{ProjectionCheckpoint}}') and object_type = 'TABLE'));
  if table_count = 0 then
    dbms_output.put_line('Creating the {{ProjectionCheckpoint}} table');
    
    execute immediate (
    'create table {{ProjectionCheckpoint}} (
      ProjectionStreamHash raw(20) not null primary key,
      LastGlobalSequence number(19) not null
    )');
  else
   dbms_output.put_line ('The {{ProjectionCheckpoint}} table already exists in the database.');
  end if;
  
  exception when others then dbms_output.put_line('An unexpected exception has occured. Please re-evaluate the PL/SQL script');
end;