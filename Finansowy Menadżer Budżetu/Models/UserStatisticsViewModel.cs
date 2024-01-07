namespace Finansowy_Menadżer_Budżetu.Models
{
    public class UserStatisticsViewModel
    {
        public int TotalTransactions { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public int TotalFilesUploaded { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
