using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SQLite;
using System.Reflection.PortableExecutable;
using BGLibrary.Models;
using BGLibrary.Services;

namespace BGLibrary.Controllers
{
    public class PlotController : Controller
    {
        private readonly ILogger<PlotController> _logger;
        IPlotInterface PlotInterface;
        private List<SelectViewModel> IntColumns = new List<SelectViewModel>() {
            new SelectViewModel() { Column = "MaxPlayers", ColumnType = "int" },
            new SelectViewModel() { Column = "MaxPlayTime", ColumnType = "int" },
            new SelectViewModel() { Column = "MinPlayers", ColumnType = "int" },
            new SelectViewModel() { Column = "MinPlayTime", ColumnType = "int" },
            new SelectViewModel() { Column = "YearPublished", ColumnType = "int" },
            new SelectViewModel() { Column = "Average", ColumnType = "int" },
            new SelectViewModel() { Column = "AverageWeight", ColumnType = "int" }
        };

        public PlotController(ILogger<PlotController> logger, IPlotInterface ipi)
        {
            _logger = logger;
            PlotInterface = ipi;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetPlot()
        {
            return View(this.IntColumns);
        }

        [HttpPost]
        public IActionResult GetPlotCount(Count plot)
        {
            if (!plot.isValidLimits())
            {
                TempData["PlotAVG"] = "Wrong Limits";
                return Redirect("GetPlot");
            }
            PlotInterface.GetPlot(plot);
            return Redirect("GetPlot");
        }

        [HttpPost]
        public IActionResult GetPlotTimeBased(TimeBased plot)
        {
            if (!plot.isValidLimits())
            {
                TempData["PlotAVG"] = "Wrong Limits";
                return Redirect("GetPlot");
            }
            PlotInterface.GetPlot(plot);
            return Redirect("GetPlot");
        }

        [HttpPost]
        public IActionResult GetPlotAVG(AVG plot)
        {
            if (!plot.isValidLimits())
            {
                TempData["PlotAVG"] = "Wrong Limits";
                return Redirect("GetPlot");
            }
            PlotInterface.GetPlot(plot);
            return Redirect("GetPlot");
        }
    }
}