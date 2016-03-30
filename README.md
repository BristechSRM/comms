# Speaker-comms
The speaker-comms service has two endpoints, one for getting all correspondence and one for getting the last contact date for each speaker.

## Last-contacted
Request format

Perform a HTTP GET request to [service-url:8080]/last-contacted

Response Format
````javascript
{
    "email-address" : "last-contacted-date",
    ...
}
````
The date is in iso-8601 format.

Example response
````javascript
{
    "bob.builder@cartoonconstructionslimited.tv" : "2004-01-30 05:00:01",
    "chris.smith@leaddeveloper.com" : "2016-02-17 15:51:15",
    "david.wybourn@superawesomegoodcode.co.uk" : "2016-03-07 12:45:04"
}
````

## Correspondence
Request format
Perform a HTTP GET request to [service-url:8080]/correspondence

Response Format
````javascript
{
  "from"    : "sender-email-address"
  "to"      : "receiver-email-address"
  "date"    : "date of the message"
  "message" : "message sent"
}
````

The date is in iso-8601 format.
The 'from' and 'to' fields may be set to 'Unknown' if we do not have an email address.

Example Response
````javascript
{
  "from"    : "david@email.com"
  "to"      : "chris@email.com"
  "date"    : "2016-03-30"
  "message" : "Hi Chris, I would like to do a Talk"
}
````
