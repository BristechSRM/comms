module Program

open Microsoft.Owin.Hosting
open System.Configuration
open System.Threading
open Logging
open Serilog

(*
    Do not run Visual Studio as Administrator!

    Open a command prompt as Administrator and run the following command, replacing username with your username
    netsh http add urlacl url=http://*:8080/ user=username
*)

[<EntryPoint>]
let main _ = 
    setupLogging()

    try
        let baseUrl = ConfigurationManager.AppSettings.Item("BaseUrl")
        use server = WebApp.Start<Bristech.Srm.HttpConfig.Startup>(baseUrl)
        Log.Information("Listening on {Address}", baseUrl)

        let waitIndefinitelyWithToken = 
            let cancelSource = new CancellationTokenSource()
            cancelSource.Token.WaitHandle.WaitOne() |> ignore
        0

    with
    | ex ->
        Log.Fatal("Exception: {0}", ex)
        1
