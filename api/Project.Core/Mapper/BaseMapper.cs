using System.Collections;
using AutoMapper;
using Project.Core.Interfaces.IMapper;

namespace Project.Core.Mapper
{
    public class BaseMapper<TSource, TDestination> : IBaseMapper<TSource, TDestination>
    {
        private readonly IMapper _mapper;

        public BaseMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<TDestination> MapToList(List<TSource> source)
        {
            return _mapper.Map<List<TDestination>>(source);
        }

        public TDestination MapToModel(TSource source)
        {
            return _mapper.Map<TDestination>(source);
        }
    }
}