using BussinessLogic.DTOs;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Helpers
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile() 
        {
            CreateMap<Storage, FileInfoDto>();
        }
    }
}
