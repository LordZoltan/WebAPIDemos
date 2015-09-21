WebAPIDemos
=

More than just the name implies - this solution is demonstration of how you might implement a shared service layer 
with two implementations 

- One that's bound 'directly' to a data store (e.g. database)
- One that's bound via REST services implemented via Asp.Net Web API

The end result is a service layer that can be used interchangeably, anywhere.

The REST services are consumed via a generic component that leverages the modern HTTP client provided by Asp.Net Web API

*[Important reading about REST anti-patterns](http://www.infoq.com/articles/rest-anti-patterns).  This demonstration
attempts to provide a framework for avoiding them...*

All the service layer operation contracts are asynchronous.

The implementations of the service operations on the Web API controllers side re-use the direct implementation of the 
service layer - to ensure a one-to-one mapping between the two, making the whole system easy to maintain.

The Web API itself, however, is implemented in such a way that it can be consumed from any REST-capable client, not just
via the service code shown in this code.  This is particularly useful for developer testing and live support, but also
for future scalability, if new platforms (particularly, third party) are to be introduced/supported.

[Service Layer Concepts](ServiceLayerConcepts.md)

Projects roundup
=

WebAPIDemos.Data
-

This project is our 'fake' data layer (or 'repository') implementation.  This is used to model the data store 
that is hidden by the service layer.

WebAPIDemos.ServiceLayer
-

This contains the core abstractions for implementing and consuming services - so is intended to be referenced both by 
*implementations* of the service layer, but also applications that are *using* an implementation of the service layer.

In here, you'll find interfaces, core classes and extension methods for the service Request/Response types, plus each individual
service interface (e.g. IMyObjectService).

***Any code*** that uses a service should be compiled against the service interface - to ensure that the services can
always be swapped for each other.

WebAPIDemos.ServiceLayer.Direct
-

(TO DO)

