using AutoMapper;

namespace BestInvest.API.BLL
{
    public class Mapper
    {
        public List<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> sourceCollection)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>()).CreateMapper();
            return mapper.Map<IEnumerable<TSource>, List<TDestination>>(sourceCollection);
        }

        public TDestination Map<TSource, TDestination>(TSource sourceItem)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>()).CreateMapper();
            return mapper.Map<TSource, TDestination>(sourceItem);
        }
    }
}
