using AutoMapper;
using FamousQuoteQuiz.Models;
using FamousQuoteQuiz.Models.Authentication;
using FamousQuoteQuiz.ViewModels;

namespace FamousQuoteQuiz.Mapping
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            this.CreateMap<QuestionModel, QuestionViewModel>()
               .ForMember(x => x.Id, y => y.MapFrom(src => src.Id))
               .ForMember(x => x.Text, y => y.MapFrom(src => src.Text));

            this.CreateMap<AnswerModel, AnswerViewModel>()
               .ForMember(x => x.Id, y => y.MapFrom(src => src.Id))
               .ForMember(x => x.Question, y => y.MapFrom(src => src.Question))
               .ForMember(x => x.Text, y => y.MapFrom(src => src.Text))
               .ForMember(x => x.IsCorrect, y => y.MapFrom(src => src.IsCorrect));

            this.CreateMap<ApplicationUser, UsersViewModel>()
               .ForMember(x => x.Id, y => y.MapFrom(src => src.Id))
               .ForMember(x => x.UserName, y => y.MapFrom(src => src.UserName))
               .ForMember(x => x.Email, y => y.MapFrom(src => src.Email))
               .ForMember(x => x.IsDisabled, y => y.MapFrom(src => src.IsDisabled));
        }
    }
}