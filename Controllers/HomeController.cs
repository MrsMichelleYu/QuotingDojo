using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuotingDojo.Models;
using DbConnection;

namespace QuotingDojo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("add")]
        public IActionResult Add(Quote thisQuote)
        {
            string query = $"INSERT INTO quotes (name, quote) VALUES ('{thisQuote.name}', '{thisQuote.quote}')";
            DbConnector.Execute(query);
            return RedirectToAction("Quotes");
        }

        [HttpGet("quotes")]
        public IActionResult Quotes(Quote thisQuote)
        {
            List<Dictionary<string, object>> AllQuotes = DbConnector.Query("SELECT * FROM quotes ORDER BY id DESC");
            ViewBag.Quotes = AllQuotes;
            return View("Quote");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
