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
            //Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg =>
            {
                
                cfg.CreateMap<FacebookLikeResponse, List<Like>>()
                              .ConvertUsing(source => source.data.Select(p => new Like
                              {
                                  About = p.about == null ? "" : p.about,
                                  Description = p.description == null ? "" : p.description,
                                  Id = p.id == null ? "" : p.id
                              }).ToList()
                              );

                //Any Other Mapping Configuration ....
            });
            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
