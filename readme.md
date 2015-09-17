WebAPIDemos
--

More than just the name implies - this solution is demonstration of how you might implement a shared service layer 
with two implementations 

- One that's bound 'directly' to a data store (e.g. database)
- One that's bound via REST services implemented via Asp.Net Web API

The end result is a service layer that can be used interchangeably, anywhere.

The REST services are consumed via a generic component that leverages the modern HTTP client provided by Asp.Net Web API

*[Important reading about REST anti-patterns](http://www.infoq.com/articles/rest-anti-patterns).  This demonstration
attempts to provide a framework for avoiding them...*

All the service layer operations are asynchronous.

The implementations of the service operations on the Web API controllers side re-use the direct implementation of the 
service layer - to ensure a one-to-one mapping between the two, making the whole system easy to maintain.

Important
--

The concept of a 'service layer' does not directly imply that there is a web service or web API underneath it.

A service layer is any layer which sits in front of your data layer and abstracts it away from application code.
Also - service layer classes typically follow the pattern that, in order to perform some operation or get some object,
you will need to do something like this:

    IService service = new Service();
    MyObject result = service.PerformSomeOperation([parameters ...]);

Of course, this might take other forms:

    using(var service = new Service())
    {
        MyObject result = service.PerformSomeOperation([parameters ...]);
    }

(Which is popular in scenarios where the underlying resources used by the services are disposable)

And, then, for parameters, you might have something like this:

    using(IService service = new Service())
    {
        OperationResponse result = service.PerformSomeOperation(new OperationRequest(([parameters ...])))
    }

While we all like to type less text (i.e. example 1), the most *scalable* solution is this last one - the parameters
are all wrapped up in a request type so that if you need to add new parameters, etc, you don't actually need to change 
the signature of the service operations.  **NOTE** You'd have a unique `OperationRequest` and `OperationResponse` type 
for every operation.

However, we can go one step further still:

    using(IService service = new Service())
    {
        Task<ResponseType> = service.PerformSomeOperation(new OperationRequest([parameters ...]));
        // or you could await the task
    }

This allows for asynchrony on the underlying implementation of the service operation.  Any operations that aren't actually
asynchronous will simply return `Task.FromResult(result)`.

Of course, if you are taking this to its logical conclusion, then there is one issue - the fact that the code consuming the 
service needs to know what concrete type of service to construct and, more importantly, how to construct it.  This is where
Dependency Injection and Inversion of Control comes in.