module Speakercomms.CorrespondenceService

open Amazon.DynamoDBv2
open Amazon
open Amazon.DynamoDBv2.DataModel
open Amazon.DynamoDBv2.DocumentModel;
open Speakercomms.Models
open System

let client = new AmazonDynamoDBClient(RegionEndpoint.EUWest1)
let context = new DynamoDBContext(client)

let updateMap key value (map : Map<String, String>) = 
    if not <| map.ContainsKey(key) || ((Map.find key map) < value)then
        map.Add (key, value)
    else 
        map

let getCorrespondence () =                     
    let items = context.Scan<CorrespondenceItem>()
    let mutable lastContactedMap : Map<String, String> = Map.empty
        
    for item in items do
        lastContactedMap <- 
            lastContactedMap
            |> updateMap item.From item.Date
            |> updateMap item.To item.Date
        
    lastContactedMap