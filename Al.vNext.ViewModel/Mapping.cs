//-----------------------------------------------------------------------------------
// <copyright file="Mapping.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using AutoMapper;
using Al.vNext.Model;

namespace Al.vNext.ViewModel
{
    public static class Mapping
    {
        private static IMapper _mapper;

        static Mapping()
        {
            var config = new MapperConfiguration(cfg =>
            {
                ////cfg.CreateMap<Code, CodeInfo>();
                ////cfg.CreateMap<Attachment, AttachmentInfo>().ForMember(x => x.BytesContent, opt => opt.Ignore());
                ////cfg.CreateMap<AttachmentInfo, Attachment>().ConvertUsing<AttachmentConverter>();
                ////cfg.CreateMap<Product, ProductInfo>();
                ////cfg.CreateMap<ProductInfo, Product>().ForMember(p => p.ContentData, opt => opt.Ignore());
                ////cfg.CreateMap<Employee, EmployeeInfo>();
                ////cfg.CreateMap<EmployeeInfo, Employee>();
            });

            _mapper = config.CreateMapper();
        }

        public static TDestination Default<TSource, TDestination>(TSource obj)
        {
            return _mapper.Map<TSource, TDestination>(obj);
        }
    }
}
