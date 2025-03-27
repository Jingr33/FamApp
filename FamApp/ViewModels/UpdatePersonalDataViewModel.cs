using System.ComponentModel.DataAnnotations;

namespace FamApp.ViewModels
{
    public class UpdatePersonalDataViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nick { get; set; }

        [Required]
        public string Color { get; set; }
    }
}
