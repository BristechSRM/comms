module Comms.ThreadsController



open System.Net
open System.Web.Http
open System.Net.Http
open Serilog
open Comms.ThreadService


type ThreadsController() = 
    inherit ApiController()
    member x.Get() = 
        Log.Information("Received GET request for last contact")
        x.Request.CreateResponse(getThreads())

    member x.Get(id : string) = 
        Log.Information("Received GET request for last contact")
        let thread = getThread id
        match thread with
        | Some thread -> x.Request.CreateResponse(thread)
        | None -> x.Request.CreateResponse(HttpStatusCode.NotFound)