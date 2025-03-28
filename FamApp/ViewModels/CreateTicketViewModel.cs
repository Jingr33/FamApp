using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FamApp.ViewModels
{
    public class CreateTicketViewModel
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public bool Public { get; set; }

        public bool Priority { get; set; }

        [Required]
        public List<string> SelectedUserIds { get; set; } = new List<string>();

        public List<SelectListItem> Users { get; set; } = new List<SelectListItem>();

        [Required]
        public DateTime DeadLineDate { get; set; } = DateTime.Now;
    }
}
