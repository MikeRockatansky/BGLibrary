using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SQLite;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using BGLibrary.Models;
using BGLibrary.Services;

namespace BGLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IGetInterface GetInterface;

        public HomeController(ILogger<HomeController> logger, IGetInterface ibi)
        {
            _logger = logger;
            GetInterface = ibi;
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<BoardGame> games = GetInterface.GetBoardGames();
            List<SelectViewModel> fields = new List<SelectViewModel>() {
                new SelectViewModel() { Column = "Id", ColumnType = "string" },
                new SelectViewModel() { Column = "MaxPlayers", ColumnType = "int" },
                new SelectViewModel() { Column = "MaxPlayTime", ColumnType = "int" },
                new SelectViewModel() { Column = "MinPlayers", ColumnType = "int" },
                new SelectViewModel() { Column = "MinPlayTime", ColumnType = "int" },
                new SelectViewModel() { Column = "Name", ColumnType = "string" },
                new SelectViewModel() { Column = "YearPublished", ColumnType = "int" }
            };

            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>()
            {
                { "Fields", fields },
                { "Games", games }
            };
            return View(data);
        }

        [HttpPost]
        public IActionResult Index(RequestBuilderViewModel filters)
        {
            var games = GetInterface.Select(filters.SelectColumns)
                .Where(filters.Where)
                .OrderBy(filters.Orderby)
                .Get();

            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>()
            {
                { "Fields", filters.SelectColumns },
                { "Games", games }
            };
            return View(data);
        }
        
        public IActionResult GetFile() => 
            File("Table.json", "application/json", "Table.json");

        [HttpGet]
        public IActionResult Filtration(List<SelectViewModel> SelectColumns)
        {
            var data = new RequestBuilderViewModel()
            {
                SelectColumns = SelectColumns
            };
            return View(data);
        }
    }
}