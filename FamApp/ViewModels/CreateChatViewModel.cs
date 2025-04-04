using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FamApp.ViewModels
{
    public class CreateChatViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Color { get; set; } = "#cccccc";
        public string? CurrentUserId { get; set; }

        public List<SelectListItem>? Users { get; set; }
        [Required]
        public List<string> SelectedUsers { get; set; } = new List<string>();
    }
}
