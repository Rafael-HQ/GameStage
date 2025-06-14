using GameStage.Data;
using GameStage.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStage.Controllers
{
    public class ProjectFollowerController : Controller
    {
        public readonly AppDbContext _context;
        public ProjectFollowerController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var projectFollowers = _context.ProjectFollowers.ToList();
            return View(projectFollowers);
        }

        [HttpPost]
        public IActionResult seguir(int UserId, int ProjectId) {
            var jaSeguindo = _context.ProjectFollowers
                .Any(p => p.UserId == UserId && p.ProjectId == ProjectId);

            if (jaSeguindo)
            {
                return BadRequest("Você já está seguindo este projeto.");
            }

            var projectFollower = new ProjectFollower
            {
                UserId = UserId,
                ProjectId = ProjectId,
                FollowedAt = DateTime.UtcNow
            };

            _context.ProjectFollowers.Add(projectFollower);
            _context.SaveChanges();
            return Ok("Você começou a seguir o projeto.");
        }

        [HttpPost]
        public IActionResult deixarDeSeguir(int UserId, int ProjectId) {
            var projectFollower = _context.ProjectFollowers
                .FirstOrDefault(p => p.UserId == UserId && p.ProjectId == ProjectId);
            if (projectFollower == null)
            {
                return BadRequest("Você não está seguindo este projeto.");
            }
            _context.ProjectFollowers.Remove(projectFollower);
            _context.SaveChanges();
            return Ok("Você deixou de seguir o projeto.");
        }
    }
}
