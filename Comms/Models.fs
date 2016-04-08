module Comms.Models

open Amazon.DynamoDBv2.DataModel
open System
[<DynamoDBTable("Correspondence")>]
type CorrespondenceItem () =
    member val From = "" with get, set
    member val To = "" with get, set
    member val Date = "" with get, set
    member val Message = "" with get, set

type Correspondence = {
    SenderId : Guid
    ReceiverId : Guid
    Type : string
    SenderHandle : string
    ReceiverHandle : string
    Date : string
    Message : string
}

type ThreadDetail  = {
    id
}
