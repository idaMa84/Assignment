using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Models.AutoMapper
{
    public class AutoMapperConfig
    {
        public static Mapper InitializeAutomapper()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                
                cfg.CreateMap<FacebookLikedPagesResponse, List<LikedPage>>()
                              .ConvertUsing(source => source.Data.Select(p => new LikedPage
                              {
                                  Id = p.Id == null ? "" : p.Id,
                                  Name = p.Name == null ? "" : p.Name,
                                  About = p.About == null ? "" : p.About,
                                  Description = p.Description == null ? "" : p.Description                                  
                                  
                              }).ToList()
                              );             
            });            

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
