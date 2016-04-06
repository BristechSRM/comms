namespace Correspondence.CorrespondenceController

open System.Web.Http
open Correspondence.CorrespondenceService
open System.Net.Http
open Serilog

type CorrespondenceController() =
    inherit ApiController()

    member x.Get() =
        Log.Information("Received GET request for correspondence")
        x.Request.CreateResponse(getCorrespondence())
