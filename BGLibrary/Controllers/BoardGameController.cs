using BGLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SQLite;
using System.Reflection.PortableExecutable;
using BGLibrary.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BGLibrary.Controllers
{
    public class BoardGameController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IGetInterface GetInterface;

        public BoardGameController(ILogger<HomeController> logger, IGetInterface ibi)
        {
            _logger = logger;
            GetInterface = ibi;
        }

        public IActionResult Index(string id)
        {
            var data = GetInterface.GetBoardGame(id);
            return View(data);
        }
    }
}