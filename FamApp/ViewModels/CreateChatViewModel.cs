using System.ComponentModel.DataAnnotations;

namespace FamApp.ViewModels
{
    public class CreateChatViewModel
    {
        public string Name { get; set; }

        [Required]
        public List<string> SelectedUserIds { get; set; }
    }
}
