using FamApp.Models;

namespace FamApp.ViewModels
{
    public class ChatViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IsGroup { get; set; }
        public List<string> Participants { get; set; }
        public List<Message> Messages { get; set; }
        public string CurrentUser { get; set; }
    }
}
