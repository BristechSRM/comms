module Comms.CorrespondenceController

open Comms.CorrespondenceService
open Serilog
open System.Net
open System.Net.Http
open System.Web.Http

type CorrespondenceController() = 
    inherit ApiController()
    
    member x.Get(profileIdOne : string, profileIdTwo : string) = 
        Log.Information("Received get request for correspondence between profile 1: {profileIdOne} and profile 2 : {profileIdTwo}", profileIdOne, profileIdTwo)
        let correspondence = getCorrespondence profileIdOne profileIdTwo
        x.Request.CreateResponse(correspondence)
    
    member x.Post(correspondence) = 
        Log.Information("Received POST request for correspondence")
        let newId = createCorrespondenceItem (correspondence)
        match newId with
        | Some newId -> x.Request.CreateResponse(HttpStatusCode.Created, newId)
        | None -> x.Request.CreateResponse(HttpStatusCode.InternalServerError, "A new correspondence item was not created.")
