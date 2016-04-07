module Comms.Models

open Amazon.DynamoDBv2.DataModel

[<DynamoDBTable("Correspondence")>]
type CorrespondenceItem () =
    member val From = "" with get, set
    member val To = "" with get, set
    member val Date = "" with get, set
    member val Message = "" with get, set
