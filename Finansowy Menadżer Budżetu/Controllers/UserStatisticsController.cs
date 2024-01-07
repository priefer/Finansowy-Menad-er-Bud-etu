using Finansowy_Menadżer_Budżetu.Data;
using Finansowy_Menadżer_Budżetu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Finansowy_Menadżer_Budżetu.Controllers
{
    [Authorize]
    public class UserStatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserStatisticsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate)
        {
            IdentityUser użytkownik = await _userManager.FindByNameAsync(User.Identity.Name);

            var transactionsQuery = _context.Transactions
                .Where(t => t.UserId == użytkownik.Id);

            if (fromDate.HasValue)
            {
                transactionsQuery = transactionsQuery.Where(t => t.Date >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                transactionsQuery = transactionsQuery.Where(t => t.Date <= toDate.Value.AddDays(1));
            }

            var model = new UserStatisticsViewModel
            {
                TotalTransactions =  transactionsQuery.Count(),
                TotalAmount =  transactionsQuery.Sum(t => t.Category.Name == "Wydatek" ? t.Amount : 0),
                TotalIncome =  transactionsQuery.Sum(t => t.Category.Name == "Wpływ" ? t.Amount : 0),
                TotalExpenses = transactionsQuery.Sum(t => t.Category.Name == "Wpływ" ? t.Amount : 0) - transactionsQuery.Sum(t => t.Category.Name == "Wydatek" ? t.Amount : 0),
                TotalFilesUploaded =  transactionsQuery.Count(t => !string.IsNullOrEmpty(t.FilePatch)),
                FromDate = fromDate,
                ToDate = toDate
            };

            return View(model);
        }

    }
}