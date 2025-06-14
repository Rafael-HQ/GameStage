using GameStage.Data;
using GameStage.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStage.Controllers
{
    public class LiveMessageController : Controller
    {
        private readonly AppDbContext _context;
        public LiveMessageController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public IActionResult Sendmensege(int UserId, int ProjectId, string messege)
        {
            var liveMessage = new LiveMessage
            {
                UserId = UserId,
                LiveStreamId = ProjectId,
                Message = messege,
                SentAt = DateTime.UtcNow
            };

            _context.LiveMessages.Add(liveMessage);
            _context.SaveChanges();
            return Ok("Mensagem enviada com sucesso.");
        }

        public IActionResult GetMessegeUser(int UserId, int ProjectId)
        {
            var messages = _context.LiveMessages
                .Where(m => m.UserId == UserId && m.LiveStreamId == ProjectId)
                .ToList();

            if (messages == null || !messages.Any())
            {
                return NotFound("Nenhuma mensagem encontrada para este usuário na live.");
            }

            return Ok(messages);
        }

        
        public IActionResult DeleteMessege(int id)
        {
            var message = _context.LiveMessages.Find(id);
            if (message == null)
            {
                return NotFound("Mensagem não encontrada.");
            }
            _context.LiveMessages.Remove(message);
            _context.SaveChanges();
            return Ok("Mensagem excluída com sucesso.");
        }
    }
}


    
