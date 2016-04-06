namespace SpeakerComms.CorrespondenceController

open System.Web.Http
open SpeakerComms.CorrespondenceService
open System.Net.Http
open Serilog

type CorrespondenceController() = 
    inherit ApiController()

    member x.Get() = 
        Log.Information("Received GET request for correspondence")
        x.Request.CreateResponse(getCorrespondence())
