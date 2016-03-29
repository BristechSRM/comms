namespace SpeakerComms.LastContactedController

open System.Web.Http
open Speakercomms.CorrespondenceService

type LastContactedController() =
    inherit ApiController()

    member __.Get() = 
        getCorrespondence()