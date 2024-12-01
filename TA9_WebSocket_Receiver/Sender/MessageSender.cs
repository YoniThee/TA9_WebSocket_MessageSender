using System.Net.WebSockets;
using System.Text;

namespace TA9_WebSocket_MessageSender.Sender
{
    public class MessageSender
    {
        private readonly string _webSocketServerUri = "wss://localhost:5001/ws";

        public async Task SendMessageToWebSocketAsync(string message)
        {
            using (var clientWebSocket = new ClientWebSocket())
            {
                // Connect to WebSocket server
                await clientWebSocket.ConnectAsync(new Uri(_webSocketServerUri), CancellationToken.None);

                // Convert the message to byte array
                var messageBytes = Encoding.UTF8.GetBytes(message);
                var buffer = new ArraySegment<byte>(messageBytes);

                // Send the message to the WebSocket server
                await clientWebSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);

                var receiveBuffer = new byte[1024];
                var result = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
                string response = Encoding.UTF8.GetString(receiveBuffer, 0, result.Count);


                // Close the WebSocket connection
                await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Done", CancellationToken.None);
            }
        }
    }
}
