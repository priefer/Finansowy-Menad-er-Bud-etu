using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace Finansowy_Menadżer_Budżetu.Models
{
    public class Transactions
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public decimal Amount { get; set; }
        public string SharedWith { get; set; } // Id użytkowników oddzielone przecinkami
        public string Comment { get; set; }
        public string Title { get; set; }
        public string FilePatch { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
