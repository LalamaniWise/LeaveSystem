using System.ComponentModel.DataAnnotations;

namespace leaveSystem.Models.DTO
{
    public class Request
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public string? First_Name { get; set; }
        [Required]
        public string? Last_Name { get; set; }
        [Required]
        public string? Leave_Reason { get; set; }
        [Required]
        public string? Start_Date { get; set; }
        [Required]
        public string? End_Date { get; set; }

        public string status { get; set; }
    }
}
