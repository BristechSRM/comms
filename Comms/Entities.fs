namespace Comms.Entities

open Amazon.DynamoDBv2.DataModel

[<DynamoDBTable("Thread")>]
type ThreadEntity()  = 
    member val Id = "" with get, set
    
[<DynamoDBTable("CorrespondenceItem")>]
type CorrespondenceEntity() =
    member val Id = "" with get, set
    member val SenderId = "" with get, set
    member val ReceiverId = "" with get, set
    member val Date = "" with get, set
    member val Message = "" with get, set
    member val Type= "" with get, set
    member val SenderHandle = "" with get, set
    member val ReceiverHandle = "" with get, set
    member val ThreadId = "" with get, set


