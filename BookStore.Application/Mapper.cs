using AutoMapper;
using BookStore.Domain;

namespace BookStore.Application
{
  public class Mapper : Profile
  {
    public Mapper()
    {
      this.CreateMap<BookDto, Book>().ReverseMap();
      this.CreateMap<UserDto, User>()
        .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

      this.CreateMap<User, UserDto>();

    }
  }
}
