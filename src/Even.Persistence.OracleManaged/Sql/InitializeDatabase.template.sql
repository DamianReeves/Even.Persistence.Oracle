declare 
    table_count integer;
begin

  execute immediate('alter session set current_schema ={{Schema}}');
  
  select count (object_id) into table_count from user_objects where exists (
    select object_name from user_objects where (object_name = upper('{{EventsTableName}}') and object_type = 'TABLE'));
    
  if table_count = 0 then
    dbms_output.put_line('Creating the {{EventsTableName}} table');
    execute immediate (
      'create table {{EventsTableName}} (
        GlobalSequence number not null,
        EventID varchar2(36) not null,
        StreamHash raw(20) not null,
        StreamName varchar2(200) not null,
        EventType varchar2(50) not null,
        UtcTimeStamp timestamp not null,
        Metadata blob,
        Payload blob not null,
        PayloadFormat number not null,
        constraint PK_{{EventsTableName}} primary key (GlobalSequence))');
        
    execute immediate ('create index IX_{{EventsTableName}}_ on {{EventsTableName}} (StreamHash)');

    execute immediate ('create sequence {{EventsTableName}}_GlobSeq');
    
    execute immediate ('
    create or replace trigger {{EventsTableName}}_GlobSeq_Trig 
    before insert on {{EventsTableName}} 
    for each row
    begin
      select {{EventsTableName}}_GlobSeq.nextval
      into   :new.GlobalSequence
      from   dual;
    end;');
  else
    dbms_output.put_line ('The {{EventsTableName}} table already exists in the database.');
  end if;
  
  select count (object_id) into table_count from user_objects where exists (
    select object_name from user_objects where (object_name = upper('{{ProjectionIndexTableName}}') and object_type = 'TABLE'));
  if table_count = 0 then
    dbms_output.put_line('Creating the {{ProjectionIndexTableName}} table');
    
    execute immediate (
    'create table {{ProjectionIndexTableName}} (
      ProjectionStreamHash raw(20) not null,
      ProjectionStreamSequence number(19) not null,
      GlobalSequence number(19) not null,
      constraint PK_{{ProjectionIndexTableName}} primary key (ProjectionStreamHash, ProjectionStreamSequence)
    )');
  else
   dbms_output.put_line ('The {{ProjectionIndexTableName}} table already exists in the database.');
  end if;
  
  select count (object_id) into table_count from user_objects where exists (
    select object_name from user_objects where (object_name = upper('{{ProjectionCheckpointTableName}}') and object_type = 'TABLE'));
  if table_count = 0 then
    dbms_output.put_line('Creating the {{ProjectionCheckpointTableName}} table');
    
    execute immediate (
    'create table {{ProjectionCheckpointTableName}} (
      ProjectionStreamHash raw(20) not null primary key,
      LastGlobalSequence number(19) not null
    )');
  else
   dbms_output.put_line ('The {{ProjectionCheckpointTableName}} table already exists in the database.');
  end if;
  
  exception when others then dbms_output.put_line('An unexpected exception has occured. Please re-evaluate the PL/SQL script');
end;