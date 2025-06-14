using GameStage.Data;
using GameStage.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStage.Controllers
{
    public class ProjectController : Controller
    {
        public readonly AppDbContext _context;
        public ProjectController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var projects = _context.Projects.ToList();
            return View(projects);
        }
        public IActionResult Detalhes(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Project project) 
        {
            if (project == null)
            {
                return NotFound();
            }
            _context.Projects.Add(project);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }
        [HttpPost]
        public IActionResult Editar(Project project)
        {
            if (project == null)
            {
                return NotFound();
            }
            _context.Projects.Update(project);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Deletar(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }



        public IActionResult GetMessegeProject(int ProjectID)
        {
            var existMessages = _context.LiveMessages
                .Any(m => m.Message == null);
            if (existMessages)
            {
                return BadRequest("Não existem mensagens nessa live");
            }

            var messeges = _context.LiveMessages
                .Where(m => m.LiveStreamId == ProjectID)
                .ToList();

            return Ok(messeges);
        }



    }
}
