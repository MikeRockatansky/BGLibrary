using BGLibrary.Models;
using System.Data.SQLite;
using System.Diagnostics.Metrics;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace BGLibrary.Services
{
    public class IPlotService : IDBClientService, IPlotInterface
    {
        private string _script = "PlotsGenerators\\";

        private void BuildIMG(string arguments)
        {
            var psi = new ProcessStartInfo();
            psi.FileName = "C:\\Users\\Михаил\\AppData\\Local\\Programs\\Python\\Python311\\python.exe";

            psi.Arguments = arguments;
            Console.Write(psi.Arguments);

            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            var errors = "";
            var res = "";

            using (var process = Process.Start(psi))
            {
                errors = process?.StandardError.ReadToEnd();
                res= process?.StandardOutput.ReadToEnd();
            }

            Console.WriteLine();
            Console.WriteLine("ERRORS: ");
            Console.WriteLine(errors);
            Console.WriteLine();
            Console.WriteLine("RES: ");
            Console.WriteLine(res);
        }

        public void GetPlot(Count plot)
        {
            var script = this._script + "Count.py";
            string arguments = $"\"{script}\" \"{plot.Xlabel}\" \"{plot.LeftLim}\" \"{plot.RightLim}\"";
            BuildIMG(arguments);
        }

        public void GetPlot(TimeBased plot)
        {
            var script = this._script + "TimeBased.py";
            string arguments = $"\"{script}\" \"{plot.Ylabel}\" \"{plot.Value}\" \"{plot.LeftLim}\" \"{plot.RightLim}\"";
            BuildIMG(arguments);
        }

        public void GetPlot(AVG plot)
        {
            var script = this._script + "AVG.py";
            string arguments = $"\"{script}\" \"{plot.Ylabel}\" \"{plot.LeftLim}\" \"{plot.RightLim}\"";
            BuildIMG(arguments);
        }
    }
}