using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class GameObjectInspector
	{
		private readonly Dictionary<string, object> _components;

		private readonly Dictionary<string, (object obj, PropertyInfo)> _properties;

		private readonly Dictionary<string, (object obj, FieldInfo)> _fields;

		private readonly Dictionary<string, (object obj, MethodInfo)> _getMethods;

		private readonly Dictionary<string, (object obj, MethodInfo)> _setMethods;

		private static BindingFlags BindingFlags;

		public GameObject Target;

		private static readonly Regex componentIndexRegex;

		public GameObjectInspector(GameObject go)
		{
		}

		public Type GetTypeOfPath(string path)
		{
			return null;
		}

		public void Clear()
		{
		}

		public List<string> GetPaths(string path, bool includeMethods, bool getOrSetMethods, List<Type> compatibleTypes, List<string> results = null)
		{
			return null;
		}

		public List<string> GetComponentPaths(List<string> results = null)
		{
			return null;
		}

		private List<string> GetMemberPaths(object obj, string path, bool includePropsAndFields, bool includeMethods, bool getOrSetMethods, List<Type> compatibleTypes, List<string> results = null)
		{
			return null;
		}

		public object GetAndCacheObjectAtPath(string path)
		{
			return null;
		}

		public T Get<T>(string path)
		{
			return default(T);
		}

		public void Set<T>(string path, T value)
		{
		}

		private string getParentPath(string path)
		{
			return null;
		}

		private bool isInspectableType(Type type)
		{
			return false;
		}

		private bool isStruct(object obj)
		{
			return false;
		}

		public bool IsSettingCompatibleWithPath(SettingsProvider provider, string settingId, string path, bool defaultResult = true)
		{
			return false;
		}
	}
}
