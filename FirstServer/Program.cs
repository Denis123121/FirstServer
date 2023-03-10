using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using FirstServer;
using FirstServer.NetEngine;
using FirstServer.NetModel;

Random random = new Random();

ServerEngine serverEngine = new ServerEngine("127.0.0.1", 34536);
serverEngine.StartServer();
serverEngine.AcceptClient();

string messageFromClient = serverEngine.ReceiveMessage();

Request requset = JsonSerializer.Deserialize<Request>(messageFromClient);
Response response;

if (requset.Command == Commands.InvestInGoldCoins)
{
    Wallet wallet = JsonSerializer.Deserialize<Wallet>(requset.JsonData);

    WalletManager walletManager = new WalletManager(wallet);
    
    int investSilver = random.Next(1, 100);
    Console.WriteLine($"INVEST {investSilver} SILVER");
    
    walletManager.InvestInGoldCoins(investSilver);

    response = new Response()
    {
        Status = Statuses.Ok,
        JsonData = JsonSerializer.Serialize(walletManager.GetWallet())
    };
}
else if (requset.Command == Commands.InvestInSilverCoins)
{
    Wallet wallet = JsonSerializer.Deserialize<Wallet>(requset.JsonData);

    WalletManager walletManager = new WalletManager(wallet);
    walletManager.InvestInSilverCoins(random.Next(1, 100));

    response = new Response()
    {
        Status = Statuses.Ok,
        JsonData = JsonSerializer.Serialize(walletManager.GetWallet())
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