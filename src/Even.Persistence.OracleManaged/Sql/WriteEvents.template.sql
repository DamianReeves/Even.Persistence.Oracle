declare
    -- declare types to store event fields
    type eventId_t          is table of number index by binary_integer;
    type streamHash_t       is table of raw(20) index by binary_integer;
    type streamName_t       is table of varchar2(200) index by binary_integer;
    type eventType_t        is table of varchar2(50) index by binary_integer;
    type utcTimeStamp_t     is table of timestamp index by binary_integer;
    type metadata_t         is table of blob index by binary_integer;
    type payload_t          is table of blob index by binary_integer;
    type payloadFormat_t   is table of number index by binary_integer;
    
    -- add variables
    eventIds       eventId_t;
    streamHashes    streamHash_t;
    streamNames     streamName_t;
    eventTypes      eventType_t;
    utcTimeStamps   utcTimeStamp_t;
    metadata        metadata_t;
    payloads        payload_t;
    payloadFormats  payloadFormat_t;
begin
    eventIds := :eventIds;
    streamHashes := :streamHashes;
    streamNames := :streamNames;
    eventTypes := :eventTypes;
    utcTimeStamps := :utcTimeStamps;
    metadata := :metadata;
    payloads := :payloads;
    payloadFormats := :payloadFormats;
    
    forall i in event_ids.first..event_ids.last
        insert into {{SchemaTableName}}.{{EventsTableName}} ( 
            EventID, 
            StreamHash, 
            StreamName, 
            EventType, 
            UtcTimeStamp, 
            Metadata, 
            Payload, 
            PayloadFormat)
        values(
            eventIds(i),
            streamHashes(i),
            streamnames(i),
            eventTypes(i),
            utcTimeStamps(i),
            metadata(i),
            payloads(i),
            payloadFormats(i)
        );
end;