using System;
using System.Collections.Generic;

namespace Modding.Mapper
{
	public static class CustomMapperTypes
	{
		private class MapperTypeGroup
		{
			public Type T;

			public Type TMapper;

			public Type TSelector;

			public bool supportsMultiple;
		}

		private static Dictionary<Type, MapperTypeGroup> registeredTypes = new Dictionary<Type, MapperTypeGroup>();

		public static void AddMapperType<T, TMapper, TSelector>() where TMapper : MCustom<T> where TSelector : CustomSelector<T, TMapper>
		{
			MapperTypeGroup mapperTypeGroup = new MapperTypeGroup();
			mapperTypeGroup.T = typeof(T);
			mapperTypeGroup.TMapper = typeof(TMapper);
			mapperTypeGroup.TSelector = typeof(TSelector);
			MapperTypeGroup value = mapperTypeGroup;
			registeredTypes.Add(typeof(TMapper), value);
		}

		public static void AddMapperType<T, TMapper, TSelector>(bool supportsMultiple) where TMapper : MCustom<T> where TSelector : CustomSelector<T, TMapper>
		{
			MapperTypeGroup mapperTypeGroup = new MapperTypeGroup();
			mapperTypeGroup.T = typeof(T);
			mapperTypeGroup.TMapper = typeof(TMapper);
			mapperTypeGroup.TSelector = typeof(TSelector);
			mapperTypeGroup.supportsMultiple = supportsMultiple;
			MapperTypeGroup value = mapperTypeGroup;
			registeredTypes.Add(typeof(TMapper), value);
		}

		internal static bool IsCustomMapperType(MapperType t)
		{
			return registeredTypes.ContainsKey(t.GetType());
		}

		internal static bool IsSupportsMultiple(MapperType t)
		{
			MapperTypeGroup value;
			return registeredTypes.TryGetValue(t.GetType(), out value) && value.supportsMultiple;
		}

		internal static Type GetSelectorType(MapperType t)
		{
			return registeredTypes[t.GetType()].TSelector;
		}
	}
}
