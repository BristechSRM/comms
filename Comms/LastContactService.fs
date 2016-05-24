module LastContactService

open Amazon.DynamoDBv2
open Amazon.DynamoDBv2.DataModel
open Entities
open Serilog
open System

let client = new AmazonDynamoDBClient(DynamoDbConfig.config)
let context = new DynamoDBContext(client)
let pairIds (idOne : string) (idTwo : string) = String.concat "+" [ idOne; idTwo ]

let getLastContactRecord (idOne : string) idTwo = 
    let result = context.Load<LastContactEntity>(idOne + idTwo)
    match box result |> isNull with
    | true -> 
        let result2 = context.Load<LastContactEntity>(idTwo + idOne)
        match box result2 |> isNull with
        | true -> None
        | false -> Some result2
    | false -> Some result

let updateLastContact (item : CorrespondenceItemEntity) = 
    Log.Information("Updating last contact between {id1} and {id2}", item.ReceiverId, item.SenderId)
    let record = getLastContactRecord item.ReceiverId item.SenderId
    match record with
    | Some lastContact -> 
        let recordDate = DateTime.Parse(lastContact.Date)
        let itemDate = DateTime.Parse(item.Date)
        if itemDate > recordDate then context.Save({ lastContact with Date = item.Date })
    | None -> 
        let lastContact = 
            { Id = item.ReceiverId + item.SenderId
              ProfileIdOne = item.ReceiverId
              ProfileIdTwo = item.SenderId
              Date = item.Date }
        context.Save(lastContact)

let getAllLastContacts() = 
    Log.Information("Getting all last contact records")
    context.Scan<LastContactEntity>()
