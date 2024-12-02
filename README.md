

## ServiceA - WebSocket Sender
ServiceA is a WebSocket-based service responsible for sending messages to a WebSocket server (referred to as ServiceB or the receiver). It is designed to send messages to a WebSocket endpoint via HTTP POST requests. This service uses ASP.NET Core to provide an API that allows users to send messages, which will then be forwarded over a WebSocket connection to another server.

### Prerequisites
Before running ServiceA, ensure the following requirements are met:

* .NET 6.0 or higher installed on your machine.
* A WebSocket server (ServiceB) that is ready to accept WebSocket connections.
* Postman or any HTTP client to test the POST endpoint.

### Installation
1. Clone the repository or download the code for ServiceA:
```
git clone https://github.com/YoniThee/TA9_WebSocket_MessageSender.git
cd serviceA
```
2. Restore the NuGet packages:
```
dotnet restore
```
3. Build the solution:
```
dotnet build
```
4. Update the WebSocket server URL in the WebSocketService.cs file to point to the correct WebSocket server (e.g., ServiceB).
```
private static readonly Uri WebSocketServerUri = new Uri("ws://your-websocket-server-address");
```
(currently serviceB is configured to run on 5001 so you dont have to change it)

### Configuration
ServiceA uses ASP.NET Core and accepts messages through an HTTP POST request. The service will forward these messages to a WebSocket server.

#### 1. API URL:
The service exposes a single endpoint for sending messages:

POST /api/messages
#### 2.Request Body:
The request body should contain the message to be sent. For example, you can use Postman or cURL to send a message:
```
"Hello from serviceA! I am the sender here"
```

### WebSocket Communication
ServiceA opens a WebSocket connection to the WebSocket server when a message is received and forwards the message to the server. If the WebSocket connection is closed or encounters an error, the service will attempt to reconnect automatically.

### How to Run
1. Run the Application:

In Visual Studio or through the terminal, run the application:
```
dotnet run
```
This will start the service on the default port (44366 by default).
2. Test the API:

You can test the POST endpoint using Postman or cURL by sending a JSON payload:
```
"string to send to serverB"
```
Before running the requests - 
run rhe serverB (the receiver) from this link https://github.com/YoniThee/TA9_WebSocket_Receiver.git
Postman collection for example is attached
