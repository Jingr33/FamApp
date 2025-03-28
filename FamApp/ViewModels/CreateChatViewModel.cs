using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FamApp.ViewModels
{
    public class CreateChatViewModel
    {
        public string Name { get; set; }

        public string? CurrentUserId { get; set; }

        [Required]
        public List<SelectListItem>? Users { get; set; }
    }
}
