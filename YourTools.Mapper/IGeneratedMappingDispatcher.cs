namespace YourTools.Mapper;

public interface IGeneratedMappingDispatcher
{
    // bool TryMapObj(object source, System.Type sourceType, System.Type destType, out object result);
    bool TryMapSingleObject(object source, System.Type sourceType, System.Type destType, out object result);
    // bool TryMap<TSource, TDestination>(TSource source, out TDestination dest);
    bool TryMapList(object source, System.Type sourceType, System.Type destType, out object result);
    bool TryMapArray(object source, System.Type sourceType, System.Type destType, out object result);
}

internal sealed class NullGeneratedMappingDispatcher : IGeneratedMappingDispatcher
{
    public static readonly NullGeneratedMappingDispatcher Instance = new();

    // public bool TryMapObj(object source, System.Type sourceType, System.Type destType, out object result)
    // { result = null!; return false; }
    public bool TryMapSingleObject(object source, System.Type sourceType, System.Type destType, out object result)
    { result = null!; return false; }
    // public bool TryMap<TSource, TDestination>(TSource source, out TDestination dest)
    // { dest = default!; return false; }
    public bool TryMapList(object source, System.Type sourceType, System.Type destType, out object result)
    { result = null!; return false; }
    public bool TryMapArray(object source, System.Type sourceType, System.Type destType, out object result)
    { result = null!; return false; }
}
