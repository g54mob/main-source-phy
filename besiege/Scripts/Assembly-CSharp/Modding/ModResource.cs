using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using InternalModding;
using InternalModding.Assemblies;
using InternalModding.Misc;
using InternalModding.Mods;
using InternalModding.Resources;
using Modding.Serialization;
using MultithreadCoroutines;
using UnityEngine;

namespace Modding
{
	public abstract class ModResource
	{
		public enum ResourceType
		{
			Texture = 0,
			Mesh = 1,
			AudioClip = 2,
			AssetBundle = 3
		}

		private static Dictionary<ModContainer, List<Action<ModResource>>> callbacks = new Dictionary<ModContainer, List<Action<ModResource>>>();

		private static Dictionary<ModContainer, List<Action>> allResourceCallbacks = new Dictionary<ModContainer, List<Action>>();

		private readonly List<Action> loadCallbacks = new List<Action>();

		private static List<KeyValuePair<ModResource, Action<GameObject>>> grabbableResources = new List<KeyValuePair<ModResource, Action<GameObject>>>();

		public static bool AllResourcesLoaded
		{
			get
			{
				ModContainer modByAssembly = AssemblyLoader.GetModByAssembly(Assembly.GetCallingAssembly());
				if (modByAssembly == null)
				{
					throw new InvalidOperationException("AllResourcesLoaded accessed from an assembly not listed in the mod manifest!");
				}
				return modByAssembly.Resources.All((ModResource r) => r.Loaded);
			}
		}

		internal ModInfo.ResourceInfo Info { get; private set; }

		public ResourceType Type { get; protected set; }

		public string Name
		{
			get
			{
				return Info.Name;
			}
		}

		public abstract bool Loaded { get; }

		public abstract bool HasError { get; }

		public abstract string Error { get; }

		public virtual bool Available
		{
			get
			{
				return Loaded && !HasError;
			}
		}

		public static event Action<ModResource> OnResourceLoaded
		{
			add
			{
				ModContainer modByAssembly = AssemblyLoader.GetModByAssembly(Assembly.GetCallingAssembly());
				if (modByAssembly == null)
				{
					throw new InvalidOperationException("OnResourceLoaded.Add called from an assembly not listed in the manifest.");
				}
				callbacks[modByAssembly].Add(value);
				foreach (ModResource item in modByAssembly.Resources.Where((ModResource r) => r.Loaded))
				{
					value(item);
				}
			}
			remove
			{
				ModContainer modByAssembly = AssemblyLoader.GetModByAssembly(Assembly.GetCallingAssembly());
				if (modByAssembly == null)
				{
					throw new InvalidOperationException("OnResourceLoaded.Remove called from an assembly not listed in the manifest.");
				}
				callbacks[modByAssembly].Remove(value);
			}
		}

		public static event Action OnAllResourcesLoaded
		{
			add
			{
				ModContainer modByAssembly = AssemblyLoader.GetModByAssembly(Assembly.GetCallingAssembly());
				if (modByAssembly == null)
				{
					throw new InvalidOperationException("OnAllResourcesLoaded.Add called form an assembly not listed in the manifest.");
				}
				allResourceCallbacks[modByAssembly].Add(value);
				if (modByAssembly.Resources.All((ModResource r) => r.Loaded))
				{
					value();
				}
			}
			remove
			{
				ModContainer modByAssembly = AssemblyLoader.GetModByAssembly(Assembly.GetCallingAssembly());
				if (modByAssembly == null)
				{
					throw new InvalidOperationException("OnAllResourcesLoaded.Remove called form an assembly not listed in the manifest.");
				}
				allResourceCallbacks[modByAssembly].Remove(value);
			}
		}

		public virtual event Action OnLoad
		{
			add
			{
				loadCallbacks.Add(value);
				if (Loaded)
				{
					value();
				}
			}
			remove
			{
				loadCallbacks.Remove(value);
			}
		}

		public static ModTexture GetTexture(string name)
		{
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			return (ModTexture)Get(name, ResourceType.Texture, callingAssembly);
		}

		public static ModMesh GetMesh(string name)
		{
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			return (ModMesh)Get(name, ResourceType.Mesh, callingAssembly);
		}

		public static ModAudioClip GetAudioClip(string name)
		{
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			return (ModAudioClip)Get(name, ResourceType.AudioClip, callingAssembly);
		}

		public static ModAssetBundle GetAssetBundle(string name)
		{
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			return (ModAssetBundle)Get(name, ResourceType.AssetBundle, callingAssembly);
		}

		public static ModResource Get(ResourceReference reference)
		{
			ModContainer modByAssembly = AssemblyLoader.GetModByAssembly(Assembly.GetCallingAssembly());
			if (modByAssembly == null)
			{
				throw new InvalidOperationException("ModResource.Get called from an assembly not listed in the manifest.");
			}
			return Get(reference, modByAssembly);
		}

		private static ModResource Get(string name, ResourceType type, Assembly calling)
		{
			ModContainer modByAssembly = AssemblyLoader.GetModByAssembly(calling);
			if (modByAssembly == null)
			{
				throw new InvalidOperationException("ModResource.Get called from an assembly not listed in the manifest.");
			}
			ModResource modResource = Get(name, modByAssembly);
			if (modResource == null)
			{
				throw new ArgumentException("Could not find resource named " + name + "!");
			}
			if (modResource.Type != type)
			{
				throw new ArgumentException(string.Concat("Resource ", name, " is not of type ", type, "!"));
			}
			return modResource;
		}

		internal static ModResource Get(ResourceReference reference, ModContainer mod)
		{
			return Get(reference.Name, mod);
		}

		internal static ModResource Get(string name, ModContainer container)
		{
			return container.Resources.FirstOrDefault((ModResource r) => r.Name == name);
		}

		public static ModTexture CreateTextureResource(string name, string path, bool data = false, bool useMipMaps = true, bool readable = false)
		{
			return (ModTexture)CreateResource(name, path, data, useMipMaps, readable, Assembly.GetCallingAssembly(), ResourceType.Texture);
		}

		public static ModMesh CreateMeshResource(string name, string path, bool data = false)
		{
			return (ModMesh)CreateResource(name, path, data, true, false, Assembly.GetCallingAssembly(), ResourceType.Mesh);
		}

		public static ModAudioClip CreateAudioClipResource(string name, string path, bool data = false)
		{
			return (ModAudioClip)CreateResource(name, path, data, true, false, Assembly.GetCallingAssembly(), ResourceType.AudioClip);
		}

		public static ModAssetBundle CreateAssetBundleResource(string name, string path, bool data = false)
		{
			return (ModAssetBundle)CreateResource(name, path, data, true, false, Assembly.GetCallingAssembly(), ResourceType.AssetBundle);
		}

		private static ModResource CreateResource(string name, string path, bool data, bool useMipMaps, bool readable, Assembly calling, ResourceType type)
		{
			ModContainer modByAssembly = AssemblyLoader.GetModByAssembly(calling);
			if (modByAssembly == null)
			{
				throw new InvalidOperationException("CreateResource called by assembly not listed in the manifest.");
			}
			if (modByAssembly.Resources.Any((ModResource r) => r.Name == name))
			{
				throw new ArgumentException("CreateResource called with duplicate name: " + name);
			}
			ModInfo.ResourceInfo resourceInfo = new ModInfo.ResourceInfo();
			resourceInfo.Mod = modByAssembly;
			resourceInfo.Name = name;
			resourceInfo.Path = ((!data) ? ModPaths.GetFilePath(modByAssembly.Info, path) : ModPaths.GetFilePathData(modByAssembly.Info, path));
			resourceInfo.UseMipMaps = useMipMaps;
			resourceInfo.Readable = readable;
			resourceInfo.Type = type;
			ModInfo.ResourceInfo info = resourceInfo;
			ModResource modResource = Load(info);
			modByAssembly.Resources.Add(modResource);
			return modResource;
		}

		internal static KeyValuePair<ModResource, Action<GameObject>> GetResourceByGrabId(int id)
		{
			return grabbableResources[id];
		}

		public void SetOnObject(GameObject go, Action<GameObject> postSetAction = null, Action prefabPostSetAction = null)
		{
			if (Type == ResourceType.AssetBundle)
			{
				throw new InvalidOperationException("Called SetOnObject on a ModAssetBundle!");
			}
			int count = grabbableResources.Count;
			grabbableResources.Add(new KeyValuePair<ModResource, Action<GameObject>>(this, postSetAction));
			GrabModResource grabModResource = go.AddComponent<GrabModResource>();
			grabModResource.Set(count, this, postSetAction, prefabPostSetAction);
		}

		internal abstract void ApplyToObject(GameObject go);

		protected void TriggerOnLoad()
		{
			if (HasError)
			{
				MLog.Error("Error loading resource " + Name + ": " + Error);
			}
			loadCallbacks.ForEach(delegate(Action a)
			{
				ModdingUtil.PerformCallback(a);
			});
			callbacks[Info.Mod].ForEach(delegate(Action<ModResource> a)
			{
				ModdingUtil.PerformCallback(a, this);
			});
			if (Info.Mod.Resources.All((ModResource r) => r.Loaded))
			{
				allResourceCallbacks[Info.Mod].ForEach(delegate(Action a)
				{
					ModdingUtil.PerformCallback(a);
				});
			}
		}

		internal static ModResource Load(ModInfo.ResourceInfo info)
		{
			if (!callbacks.ContainsKey(info.Mod))
			{
				callbacks.Add(info.Mod, new List<Action<ModResource>>());
			}
			if (!allResourceCallbacks.ContainsKey(info.Mod))
			{
				allResourceCallbacks.Add(info.Mod, new List<Action>());
			}
			switch (info.Type)
			{
			case ResourceType.Texture:
				return LoadTexture(info);
			case ResourceType.Mesh:
				return LoadMesh(info);
			case ResourceType.AudioClip:
				return LoadAudioClip(info);
			case ResourceType.AssetBundle:
				return LoadAssetBundle(info);
			default:
				throw new InvalidDataException("Not a supported resource type: " + info.Type);
			}
		}

		internal abstract IEnumerator Load();

		private static ModTexture LoadTexture(ModInfo.ResourceInfo info)
		{
			ModTexture modTexture = new ModTexture();
			modTexture.Info = info;
			modTexture.Type = ResourceType.Texture;
			ModTexture modTexture2 = modTexture;
			SingleInstanceFindOnly<ModManager>.Instance.StartCoroutine(modTexture2.Load());
			return modTexture2;
		}

		private static ModMesh LoadMesh(ModInfo.ResourceInfo info)
		{
			ModMesh modMesh = new ModMesh();
			modMesh.Info = info;
			modMesh.Type = ResourceType.Mesh;
			ModMesh modMesh2 = modMesh;
			SingleInstanceFindOnly<ModManager>.Instance.StartCoroutineAsync(modMesh2.Load());
			return modMesh2;
		}

		private static ModAudioClip LoadAudioClip(ModInfo.ResourceInfo info)
		{
			ModAudioClip modAudioClip = new ModAudioClip();
			modAudioClip.Info = info;
			modAudioClip.Type = ResourceType.AudioClip;
			ModAudioClip modAudioClip2 = modAudioClip;
			SingleInstanceFindOnly<ModManager>.Instance.StartCoroutine(modAudioClip2.Load());
			return modAudioClip2;
		}

		private static ModAssetBundle LoadAssetBundle(ModInfo.ResourceInfo info)
		{
			ModAssetBundle modAssetBundle = new ModAssetBundle();
			modAssetBundle.Info = info;
			modAssetBundle.Type = ResourceType.AssetBundle;
			ModAssetBundle modAssetBundle2 = modAssetBundle;
			SingleInstanceFindOnly<ModManager>.Instance.StartCoroutine(modAssetBundle2.Load());
			return modAssetBundle2;
		}
	}
}
