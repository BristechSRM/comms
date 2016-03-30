namespace SpeakerComms.LastContactedController

open System.Web.Http
open SpeakerComms.LastContactedService

type LastContactedController() =
    inherit ApiController()

    member __.Get() = 
        getLastContacted()