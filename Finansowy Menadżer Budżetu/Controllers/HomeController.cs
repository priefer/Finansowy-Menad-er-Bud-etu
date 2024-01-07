using Finansowy_Menadżer_Budżetu.Data;
using Finansowy_Menadżer_Budżetu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Finansowy_Menadżer_Budżetu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var transactions = _context.Transactions.ToList();
            var totalTransactions = transactions.Count();
            var totalAmount = transactions.Sum(t => t.Amount);
            var totalDocuments = transactions.Sum(t => t.FilePatch is not null ? 1 : 0);

            var data = transactions
        .GroupBy(t => t.Date.Date)
        .Select(group => new { Date = group.Key, TransactionCount = group.Count() })
        .OrderBy(item => item.Date)
        .ToList();

            ViewBag.ChartData = data;
            ViewBag.TotalTransactions = totalTransactions;
            ViewBag.TotalAmount = totalAmount;
            ViewBag.TotalDocuments = totalDocuments;
            return View();
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