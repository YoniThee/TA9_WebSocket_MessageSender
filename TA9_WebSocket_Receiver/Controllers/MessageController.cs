using Microsoft.AspNetCore.Mvc;
using TA9_WebSocket_MessageSender.Sender;

namespace WebSocketExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageSender _messageSender;
        public MessageController(MessageSender messageSender)
        {
            _messageSender = messageSender;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return BadRequest("Message cannot be empty");
            }

            try
            {
                // Open WebSocket connection and send the message
                await _messageSender.SendMessageToWebSocketAsync(message);

                return Ok("Message sent successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error while sending message: {ex.Message}");
            }
        }

        
    }
}
