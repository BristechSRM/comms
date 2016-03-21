module main

open Microsoft.Owin.Hosting
open System
open Server
open System.Threading

[<EntryPoint>]
let main _ = 
    let baseUrl = "http://*:9001"
    use server = WebApp.Start<Startup>(baseUrl)
    printf "Running on %s\n" baseUrl

    let waitIndefinitelyWithToken = 
        let cancelSource = new CancellationTokenSource()
        cancelSource.Token.WaitHandle.WaitOne() |> ignore
    0

