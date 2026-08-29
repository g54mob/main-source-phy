using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public class IOCContainer
	{
		private class Item
		{
			public object Instance;

			public MulticastDelegate Function;

			public Item(object instance)
			{
				Instance = instance;
			}

			public Item(MulticastDelegate function)
			{
				Function = function;
			}

			public T Resolve<T>()
			{
				if (Instance != null)
				{
					return (T)Instance;
				}
				return ((Func<T>)Function)();
			}
		}

		private Dictionary<Type, Dictionary<string, Item>> m_named = new Dictionary<Type, Dictionary<string, Item>>();

		private Dictionary<Type, Item> m_registered = new Dictionary<Type, Item>();

		private Dictionary<Type, Item> m_fallbacks = new Dictionary<Type, Item>();

		public bool IsRegistered<T>(string name)
		{
			if (!m_named.TryGetValue(typeof(T), out var value))
			{
				return false;
			}
			return value.ContainsKey(name);
		}

		public void Register<T>(string name, Func<T> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			if (!m_named.TryGetValue(typeof(T), out var value))
			{
				value = new Dictionary<string, Item>();
				m_named.Add(typeof(T), value);
			}
			if (value.ContainsKey(name))
			{
				Debug.LogWarningFormat("item with name {0} already registered", name);
			}
			value[name] = new Item(func);
		}

		public void Register<T>(string name, T instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("func");
			}
			if (!m_named.TryGetValue(typeof(T), out var value))
			{
				value = new Dictionary<string, Item>();
				m_named.Add(typeof(T), value);
			}
			if (value.ContainsKey(name))
			{
				Debug.LogWarningFormat("item with name {0} already registered", name);
			}
			value[name] = new Item(instance);
		}

		public bool IsRegistered<T>()
		{
			return m_registered.ContainsKey(typeof(T));
		}

		public void Register<T>(Func<T> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			if (m_registered.ContainsKey(typeof(T)))
			{
				Debug.LogWarningFormat("type {0} already registered.", typeof(T).FullName);
			}
			m_registered[typeof(T)] = new Item(func);
		}

		public void Register<T>(T instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (m_registered.ContainsKey(typeof(T)))
			{
				Debug.LogWarningFormat("type {0} already registered.", typeof(T).FullName);
			}
			m_registered[typeof(T)] = new Item(instance);
		}

		public void Unregister<T>(string name, Func<T> func)
		{
			if (m_named.TryGetValue(typeof(T), out var value) && value.TryGetValue(name, out var value2) && (object)value2.Function != null && value2.Function.Equals(func))
			{
				value.Remove(name);
				if (value.Count == 0)
				{
					m_named.Remove(typeof(T));
				}
			}
		}

		public void Unregister<T>(string name, T instance)
		{
			if (m_named.TryGetValue(typeof(T), out var value) && value.TryGetValue(name, out var value2) && value2.Instance == (object)instance)
			{
				value.Remove(name);
				if (value.Count == 0)
				{
					m_named.Remove(typeof(T));
				}
			}
		}

		public void Unregister<T>(Func<T> func)
		{
			if (m_registered.TryGetValue(typeof(T), out var value) && (object)value.Function != null && value.Function.Equals(func))
			{
				m_registered.Remove(typeof(T));
			}
		}

		public void Unregister<T>(T instance)
		{
			if (m_registered.TryGetValue(typeof(T), out var value) && value.Instance == (object)instance)
			{
				m_registered.Remove(typeof(T));
			}
		}

		public void Unregister<T>()
		{
			m_registered.Remove(typeof(T));
		}

		public bool IsFallbackRegistered<T>()
		{
			return m_fallbacks.ContainsKey(typeof(T));
		}

		public void RegisterFallback<T>(Func<T> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			if (m_fallbacks.ContainsKey(typeof(T)))
			{
				Debug.LogWarningFormat("fallback for type {0} already registered.", typeof(T).FullName);
			}
			m_fallbacks[typeof(T)] = new Item(func);
		}

		public void RegisterFallback<T>(T instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (m_fallbacks.ContainsKey(typeof(T)))
			{
				Debug.LogWarningFormat("type {0} already registered.", typeof(T).FullName);
			}
			m_fallbacks[typeof(T)] = new Item(instance);
		}

		public void UnregisterFallback<T>(Func<T> func)
		{
			if (m_fallbacks.TryGetValue(typeof(T), out var value) && (object)value.Function != null && value.Function.Equals(func))
			{
				m_fallbacks.Remove(typeof(T));
			}
		}

		public void UnregisterFallback<T>(T instance)
		{
			if (m_fallbacks.TryGetValue(typeof(T), out var value) && value.Instance == (object)instance)
			{
				m_fallbacks.Remove(typeof(T));
			}
		}

		public void UnregisterFallback<T>()
		{
			m_fallbacks.Remove(typeof(T));
		}

		public T Resolve<T>(string name)
		{
			if (m_named.TryGetValue(typeof(T), out var value) && value.TryGetValue(name, out var value2))
			{
				return value2.Resolve<T>();
			}
			return default(T);
		}

		public T Resolve<T>()
		{
			if (m_registered.TryGetValue(typeof(T), out var value))
			{
				return value.Resolve<T>();
			}
			if (m_fallbacks.TryGetValue(typeof(T), out value))
			{
				return value.Resolve<T>();
			}
			return default(T);
		}

		public void Clear()
		{
			m_registered.Clear();
			m_fallbacks.Clear();
			m_named.Clear();
		}
	}
}
