module Comms.CorrespondenceService

open Amazon.DynamoDBv2
open Amazon
open Amazon.DynamoDBv2.DataModel
open Amazon.DynamoDBv2.DocumentModel;
open Comms.Models
open System
open Serilog

let client = new AmazonDynamoDBClient(RegionEndpoint.EUWest1)
let context = new DynamoDBContext(client)

let getCorrespondence () =
    Log.Information("Contacting DynamoDB for correspondence items")
    context.Scan<CorrespondenceItem>()
