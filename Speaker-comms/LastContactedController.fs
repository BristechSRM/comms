namespace SpeakerComms.LastContactedController

open System.Web.Http
open Speakercomms.LastContactedService

type LastContactedController() =
    inherit ApiController()

    member __.Get() = 
        getLastContacted()