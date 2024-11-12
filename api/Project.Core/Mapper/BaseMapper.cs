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

        public IEnumerable<TDestination> MapToList(IEnumerable<TSource> source)
        {
            return _mapper.Map<IEnumerable<TDestination>>(source);
        }

        public TDestination MapToModel(TSource source)
        {
            return _mapper.Map<TDestination>(source);
        }
    }
}