namespace Correspondence.LastContactedController

open System.Web.Http
open Correspondence.LastContactedService
open System.Net.Http
open Serilog

type LastContactedController() =
    inherit ApiController()

    member x.Get() =
        Log.Information("Received GET request for last contacted")
        x.Request.CreateResponse(getLastContacted())
