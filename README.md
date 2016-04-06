# Correspondence
The Correspondence service has two endpoints, one for getting all correspondence and one for getting the last contact date for each speaker.

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
    "bob.builder@cartoonconstructionslimited.tv" : "2004-01-30T05:00:01Z",
    "chris.smith@leaddeveloper.com" : "2016-02-17T15:51:15Z",
    "david.wybourn@superawesomegoodcode.co.uk" : "2016-03-07T12:45:04Z"
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
  "date"    : "2016-03-30T14:10:21Z"
  "message" : "Hi Chris, I would like to do a Talk"
}
````

Note. Correspondence will require aws credentials with dynamodb read access in the file ~/.aws/credentials.
