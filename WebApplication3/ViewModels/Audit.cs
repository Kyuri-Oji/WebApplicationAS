using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModels
{
    public class Audit
    {
        public int Id { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string UserActivity { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
