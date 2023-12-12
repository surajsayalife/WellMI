namespace WellMI.Models
{
    public class UpdateData
    {
        public string? Username { get; set; }

        public string EmailId { get; set; } = null!;

        public string? Password { get; set; }

        public int? AccountType { get; set; }
    }
}
