namespace Comms.Entities

open Amazon.DynamoDBv2.DataModel

[<DynamoDBTable("CorrespondenceItem")>]
[<CLIMutable>]
type CorrespondenceItemEntity = 
    { Id : string
      ExternalId : string
      SenderId : string
      ReceiverId : string
      Date : string
      Message : string
      Type : string
      SenderHandle : string
      ReceiverHandle : string }

[<DynamoDBTable("LastContact")>]
[<CLIMutable>]
type LastContactEntity = 
    { Id : string
      ProfileIdOne : string
      ProfileIdTwo : string
      Date : string }
