using System.Net.Sockets;
using System.Net;
using System.Text;

static void Log(string msg)
{
    Console.WriteLine($"Log: {DateTime.Now}  {msg}");
}

Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("192.168.8.162"), 36546);

listener.Bind(ipEndPoint);
listener.Listen(1);

Log("Program start");

Socket handler = listener.Accept(); 

Log($"Client accept from {handler.RemoteEndPoint}");

StringBuilder messageBuilder = new StringBuilder();
do
{
    byte[] inputBytes = new byte[1024];
    int countBytes = handler.Receive(inputBytes);
    messageBuilder.Append(Encoding.Unicode.GetString(inputBytes, 0, countBytes));
} while (handler.Available > 0);

string messageFromClient = messageBuilder.ToString();

Log($"Message from client received: {messageFromClient}");

string messageToClient = messageFromClient + "message received";

if (messageFromClient == "время")
{
    messageToClient = DateTime.Now.ToString();
}

byte[] outputBytes = Encoding.Unicode.GetBytes(messageToClient);
handler.Send(outputBytes);

Log($"Message send: {messageToClient}");