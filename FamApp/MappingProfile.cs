using AutoMapper;
using FamApp.Models;
using FamApp.ViewModels;


namespace FamApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Chat, ChatViewModel>();
            CreateMap<Message, MessageViewModel>();
            CreateMap<CreateChatViewModel, Chat>();
        }
    }
}
