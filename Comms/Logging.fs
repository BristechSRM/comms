module Comms.Logging

open Serilog

let setupLogging() = 
    Log.Logger <- LoggerConfiguration().ReadFrom.AppSettings().CreateLogger()
    Log.Information("Serilog logging initialised")
