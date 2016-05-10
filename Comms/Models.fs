module Comms.Models

open System.Net

type Result<'Success,'Failure> = 
    | Success of 'Success
    | Failure of 'Failure

type ServerError = 
    { HttpStatus : HttpStatusCode
      Message : string }