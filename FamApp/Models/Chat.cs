namespace FamApp.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsGroup { get; set; }
        public string Color { get; set; }

        public ICollection<Message> Messages { get; set; }
        public ICollection<ChatUser> UserChats { get; set; }
    }
}
