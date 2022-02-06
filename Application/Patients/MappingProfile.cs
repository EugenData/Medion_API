using AutoMapper;
using Domain;

namespace Application.Patients
{
    public class MappingProfile : Profile
    { 
        public MappingProfile()
        {
            CreateMap<Patient, PatientDTO>();
            CreateMap<Meeting, MeetingDTO>();
        }
    }
}