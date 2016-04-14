module Comms.ThreadsController



open System.Net
open System.Web.Http
open System.Net.Http
open Serilog
open Comms.ThreadService
open Comms.Models


type ThreadsController() = 
    inherit ApiController()
    member x.Get() = 
        Log.Information("Received GET request for threads")
        x.Request.CreateResponse(getThreads())

    member x.Get(id : string) = 
        Log.Information("Received GET request for thread: {id}", id)
        let thread = getThread id
        match thread with
        | Some thread -> x.Request.CreateResponse(thread)
        | None -> x.Request.CreateResponse(HttpStatusCode.NotFound)
    