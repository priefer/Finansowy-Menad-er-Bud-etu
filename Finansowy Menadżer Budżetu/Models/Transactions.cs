using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Finansowy_Menadżer_Budżetu.Models
{
    public class Transactions
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public int GroupId { get; set; }
        public virtual Group? Group { get; set; }
        public decimal Amount { get; set; }
        public string? Comment { get; set; }
        public string Title { get; set; }
        public string? FilePatch { get; set; }
        [NotMapped]
        public virtual IFormFile? File { get; set; }
        public string? UserId { get; set; }
        public virtual IdentityUser? User { get; set; }
    }
}
