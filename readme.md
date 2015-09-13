WebAPIDemos
--

This solution is demonstration of how you might implement a shared service layer with two implementations 

- One that's bound 'directly' to a data store (e.g. database)
- One that's bound via REST services implemented via Asp.Net Web API

The end result is a service layer that can be used interchangeably, anywhere.

The REST services are consumed via a generic component that leverages the modern HTTP client provided by Asp.Net Web API

All the service layer operations are asynchronous.

The implementations of the service operations on the Web API controllers side re-use the direct implementation of the 
service layer - to ensure a one-to-one mapping between the two, making the whole system easy to maintain.