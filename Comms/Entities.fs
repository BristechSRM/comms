namespace Comms.Entities

open Amazon.DynamoDBv2.DataModel

[<DynamoDBTable("Thread")>]
[<CLIMutable>]
type ThreadEntity = {
    Id : string
}
    
[<DynamoDBTable("CorrespondenceItem")>]
[<CLIMutable>]
type CorrespondenceEntity = {
    Id : string
    SenderId : string
    ReceiverId : string
    Date : string
    Message : string
    Type : string
    SenderHandle : string
    ReceiverHandle : string
    ThreadId : string
}
