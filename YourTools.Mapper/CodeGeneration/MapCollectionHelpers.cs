// Permanent (non-generated) collection mapping helpers formerly emitted by the source generator

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace YourTools.Mapper.CodeGeneration
{
    public static class MapCollectionHelpers
    {
        // Caches for compiled delegates
        //private static readonly ConcurrentDictionary<(Type, Type), Delegate> SingleObjectMapCache = new();

        // Specialized cache for direct property/field mapping delegates
        private static readonly ConcurrentDictionary<(Type, Type), Delegate> DirectMapCache = new();

        // Generates and caches a compiled delegate for single object mapping
        // private static Func<TSrc, TDest> GetOrCreateSingleObjectMapDelegate<TSrc, TDest>(Func<TSrc, TDest> map)
        // {
        //     var key = (typeof(TSrc), typeof(TDest));
        //     if (SingleObjectMapCache.TryGetValue(key, out var cached))
        //         return (Func<TSrc, TDest>)cached;
        //     // Just cache the provided delegate for now
        //     SingleObjectMapCache[key] = map;
        //     return map;
        // }

        // Generates and caches a direct property/field mapping delegate for single object mapping
        private static Func<TSrc, TDest> GetOrCreateDirectMapDelegate<TSrc, TDest>()
        {
            var key = (typeof(TSrc), typeof(TDest));
            if (DirectMapCache.TryGetValue(key, out var cached))
                return (Func<TSrc, TDest>)cached;

            var srcType = typeof(TSrc);
            var destType = typeof(TDest);
            var srcParam = Expression.Parameter(srcType, "src");
            var destVar = Expression.Variable(destType, "dest");
            var assignExprs = new List<Expression>();

            // Create new destination object
            var newDest = Expression.New(destType);
            assignExprs.Add(Expression.Assign(destVar, newDest));

            // Map matching writable properties
            var srcProps = srcType.GetProperties();
            var destProps = destType.GetProperties();
            foreach (var destProp in destProps)
            {
                if (!destProp.CanWrite) continue;
                var srcProp = srcProps.FirstOrDefault(p =>
                    p.Name == destProp.Name && p.PropertyType == destProp.PropertyType && p.CanRead);
                if (srcProp != null)
                {
                    assignExprs.Add(
                        Expression.Assign(
                            Expression.Property(destVar, destProp),
                            Expression.Property(srcParam, srcProp)
                        )
                    );
                }
            }

            // Map matching public fields
            var srcFields = srcType.GetFields();
            var destFields = destType.GetFields();
            foreach (var destField in destFields)
            {
                var srcField =
                    srcFields.FirstOrDefault(f => f.Name == destField.Name && f.FieldType == destField.FieldType);
                if (srcField != null)
                {
                    assignExprs.Add(
                        Expression.Assign(
                            Expression.Field(destVar, destField),
                            Expression.Field(srcParam, srcField)
                        )
                    );
                }
            }

            assignExprs.Add(destVar); // return dest
            var block = Expression.Block(new[] { destVar }, assignExprs);
            var lambda = Expression.Lambda<Func<TSrc, TDest>>(block, srcParam);
            var compiled = lambda.Compile();
            DirectMapCache[key] = compiled;
            return compiled;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<TDest> MapList<TSrc, TDest>(List<TSrc> source, Func<TSrc, TDest> map)
        {
            if (source == null) return null;
            var count = source.Count;
            var dest = new List<TDest>(count);
            for (int i = 0; i < count; i++)
            {
                var item = source[i];
                //if (item == null) continue;
                dest.Add(map(item));
            }

            return dest;
        }

        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public static TDest[] MapListToArray<TSrc, TDest>(List<TSrc> source, Func<TSrc, TDest> map = null)
        // {
        //     if (source == null) return null;
        //     int len = source.Count;
        //     var directMap = GetOrCreateDirectMapDelegate<TSrc, TDest>();
        //     TDest[] dest;
        //     dest = new TDest[len];
        //     for (int i = 0; i < len; i++)
        //     {
        //         var item = source[i];
        //         //if (item == null) continue;
        //         dest[i] = directMap(item);
        //     }
        //
        //     return dest;
        // }
        //
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public static TDest[] MapArray<TSrc, TDest>(TSrc[] source, Func<TSrc, TDest> map = null)
        // {
        //     if (source == null) return null;
        //     int len = source.Length;
        //     var directMap = GetOrCreateDirectMapDelegate<TSrc, TDest>();
        //     TDest[] dest;
        //     dest = new TDest[len];
        //     for (int i = 0; i < len; i++)
        //         dest[i] = directMap(source[i]);
        //     return dest;
        // }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<TDest> MapArrayToList<TSrc, TDest>(TSrc[] source, Func<TSrc, TDest> map = null)
        {
            if (source == null) return null;
            int len = source.Length;
            var directMap = GetOrCreateDirectMapDelegate<TSrc, TDest>();

            var resultList = new List<TDest>(len);
            for (int i = 0; i < len; i++)
                resultList.Add(directMap(source[i]));
            return resultList;
        }

        // // Direct call for single object mapping using cached delegate
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public static TDest MapSingle<TSrc, TDest>(TSrc source, Func<TSrc, TDest> map)
        // {
        //     if (source == null) return default;
        //     // Try direct mapping first
        //     var directMap = GetOrCreateDirectMapDelegate<TSrc, TDest>();
        //     if (directMap != null)
        //         return directMap(source);
        //     // Fallback to user-provided delegate
        //     var compiled = GetOrCreateSingleObjectMapDelegate(map);
        //     return compiled(source);
        // }
    }
}