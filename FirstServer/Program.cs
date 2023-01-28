using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using FirstServer;
using FirstServer.NetEngine;
using FirstServer.NetModel;

ServerEngine serverEngine = new ServerEngine("127.0.0.1", 34536);
serverEngine.StartServer();
serverEngine.AcceptClient();

string messageFromClient = serverEngine.ReceiveMessage();

Request requset = JsonSerializer.Deserialize<Request>(messageFromClient);
Response response;

if (requset.Command == Commands.AddAge)
{
    Wallet wallet = JsonSerializer.Deserialize<Wallet>(requset.JsonData);

    response = new Response()
    {
        Status = Statuses.Ok,
        JsonData = JsonSerializer.Serialize(wallet)
    };
}
else
{
    response = new Response()
    {
        Status = Statuses.UnknownCommand
    };
}

string messageToClient = JsonSerializer.Serialize(response);
serverEngine.SendMessage(messageToClient);

serverEngine.CloseClientSocket();
serverEngine.CloseServerSocket();