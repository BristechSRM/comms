module Comms.CorrespondenceController

open Comms.CorrespondenceService
open Serilog
open System.Net
open System.Net.Http
open System.Web.Http
open Comms.Models
open Comms.Entities

type CorrespondenceController() = 
    inherit ApiController()
    
    member x.Get(profileIdOne, profileIdTwo) = 
        Log.Information("Received get request for correspondence between profile 1: {profileIdOne} and profile 2 : {profileIdTwo}", profileIdOne, profileIdTwo)
        let correspondence = getCorrespondence profileIdOne profileIdTwo
        x.Request.CreateResponse(correspondence)
    
    member x.Post(correspondence) = 
        Log.Information("Received POST request for correspondence")
        let result = createCorrespondenceItem (correspondence)
        match result with
        | Success newId-> x.Request.CreateResponse(HttpStatusCode.Created, newId)
        | Failure error -> x.Request.CreateResponse(error.HttpStatus, error.Message)
