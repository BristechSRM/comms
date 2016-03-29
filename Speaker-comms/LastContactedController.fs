namespace SpeakerComms.LastContactedController

open System.Web.Http
open SpeakerComms.LastContactedService

type LastContactedController() =
    inherit ApiController()

    member __.Get() = 
        // Temporary, being used just for demo.
        let lastContactedData = 
            ["saatviga.sudhahar@bristol.ac.uk", "22/03/2016";
             "carolyn@kwmc.org.uk", "11/03/2016";
             "matt@doics.co", "11/03/2016";
             "l.kreczko@bristol.ac.uk", "29/02/2016";
             "ddrozdzewski@scottlogic.com", "17/02/2016";
             "me@rachelandrew.co.uk", "26/02/2016";
             "toby@warmfusion.co.uk", "11/03/2016";
             "rich@rich-knight.com", "11/03/2016";
             "tom@ultrahaptics.com", "16/02/2016";
             "12e.david@gmail.com", "19/02/2016";]
             |> Map.ofList
        lastContactedData
        //getLastContacted()