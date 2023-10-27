using AutoMapper;
using Employee.DTO;
using Employee.Model;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Employe, EmployeeDTO>()
            .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses));

        CreateMap<EmployeeDTO, Employe>()
            .ForMember(dest => dest.Addresses, opt => opt.Ignore()); // Ignore mapping from EmployeeDTO to Employe

        CreateMap<CreateEmployeeDTO, Employe>()
            .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => new List<Address> { new Address { Description = src.Address.Description } }));

        CreateMap<Address, AddressDTO>();
    }
}
