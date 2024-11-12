using System.Collections;

namespace Project.Core.Interfaces.IMapper
{
    public interface IBaseMapper<TSource, TDestination>
    {
        TDestination MapToModel(TSource source);
        IEnumerable<TDestination> MapToList(IEnumerable<TSource> source);
    }
}