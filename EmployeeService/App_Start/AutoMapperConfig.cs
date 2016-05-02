using EmployeeService.Models;
using AutoMapper;

namespace EmployeeService.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<edata, EmployeeDTO>();
            Mapper.CreateMap<EmployeeDTO, edata>();
        }
    }
}