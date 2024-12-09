using System.Collections;

namespace Project.Core.Interfaces.IMapper
{
    public interface IBaseMapper<TSource, TDestination>
    {
        TDestination MapToModel(TSource source);
        List<TDestination> MapToList(List<TSource> source);
        void UpdateModel(TSource source, TDestination destination);
    }
}