using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	public static class CGResourceHandler
	{
		private static Dictionary<string, ICGResourceLoader> Loader = new Dictionary<string, ICGResourceLoader>();

		public static Component CreateResource(CGModule module, string resName, string context)
		{
			if (Loader.Count == 0)
			{
				getLoaders();
			}
			if (Loader.ContainsKey(resName))
			{
				return Loader[resName].Create(module, context);
			}
			Debug.LogError("CGResourceHandler: Missing Loader for resource '" + resName + "'");
			return null;
		}

		public static void DestroyResource(CGModule module, string resName, Component obj, string context, bool kill)
		{
			if (Loader.Count == 0)
			{
				getLoaders();
			}
			if (Loader.ContainsKey(resName))
			{
				ICGResourceLoader iCGResourceLoader = Loader[resName];
				try
				{
					iCGResourceLoader.Destroy(module, obj, context, kill);
					return;
				}
				catch (InvalidOperationException ex)
				{
					if (ex.Message.IndexOf("prefab", StringComparison.OrdinalIgnoreCase) >= 0)
					{
						DTLog.LogError("[Curvy] Error while trying to destroy the object '" + obj.name + "'. This is probably because that object is part of a prefab instance, and Unity 2018.3 and beyond forbid deleting such objects without breaking the prefab link. Please remove the corresponding object from the prefab and try the faulty operation again.");
					}
					throw;
				}
			}
			Debug.LogError("CGResourceHandler: Missing Loader for resource '" + resName + "'");
		}

		private static void getLoaders()
		{
			Type[] loadedTypes = TypeExt.GetLoadedTypes();
			Type typeFromHandle = typeof(ICGResourceLoader);
			Type[] array = loadedTypes;
			foreach (Type type in array)
			{
				if (!typeFromHandle.IsAssignableFrom(type) || !(typeFromHandle != type))
				{
					continue;
				}
				object[] customAttributes = type.GetCustomAttributes(typeof(ResourceLoaderAttribute), inherit: true);
				if (customAttributes.Length != 0)
				{
					ICGResourceLoader iCGResourceLoader = (ICGResourceLoader)Activator.CreateInstance(type);
					if (iCGResourceLoader != null)
					{
						Loader.Add(((ResourceLoaderAttribute)customAttributes[0]).ResourceName, iCGResourceLoader);
					}
				}
				else
				{
					DTLog.LogError($"[Curvy] Could not register resource loader of type {type.FullName} because it does not have a ResourceLoader attribute");
				}
			}
		}
	}
}
