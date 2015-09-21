Service Layers in Applications : Rules
-

When you have a service layer in your application, then the golden rule is that any application sitting 'in front' of 
that layer (i.e. using it) should *never* access the underlying data source (database) directly - everything should go
through that service layer.  As a developer of an application that uses the service layer, you must effectively forget
that any database exists.

Equally, individual services will likely be created on a per-object basis, but that doesn't mean one service for every
database table.  Your service layer should present and fulfil your business' requirements as granular operations, so that 
your application code contains as little business logic as possible.

So, whilst an `IProjectService` interface might expose an `UpdateProject()` method, allowing the caller fine-grained control
over updating the underlying data; it should probably also have a `StartProject` method - which is specifically geared
around performing the necessary database updates, to mark a project as 'started'.

The service layer implementation might
then choose to prevent direct updates of the project object which could change the project's state

With layered architecture, if different layers find themselves competing for the same responsibilities, then bad things
happen.

The concept of a 'service layer' does not have to imply that there is a web service or Web API underneath it.

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