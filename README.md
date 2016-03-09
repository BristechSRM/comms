# speaker-comms
The speaker-comms service returns the last contacted date for all the email addresses in the system.

Request Format
Perform a HTTP GET request to [service-url:8080]/last-contacted

Response Format
{"email-address":"last-contacted-date", ...}

The date is in iso-8601 format.

Example response

{"bob.builder@cartoonconstructionslimited.tv":"2004-01-30T05:00:01Z","chris.smith@leaddeveloper.com":"2016-02-17T15:51:15Z","david.wybourn@superawesomegoodcode.co.uk":"2016-03-07T12:45:04Z"}

