using AutoMapper;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Npgsql.NameTranslation;
using UserTestingApplication.DTOs;
using UserTestingApplication.Models;

namespace UserTestingApplication.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Answer, AnswerDTO>();
            CreateMap<ApplicationUser, ApplicationUserDTO>();
            CreateMap<ApplicationUserTest, ApplicationUserTestDTO>();
            CreateMap<Question, QuestionDTO>();
            CreateMap<Test, TestDTO>();
        }
    }
}
