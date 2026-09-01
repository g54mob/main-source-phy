using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using InternalModding.Assemblies;
using InternalModding.Common;
using InternalModding.Misc;
using InternalModding.Mods;
using Modding.Modules.Official;
using UnityEngine;

namespace Modding.Modules
{
	public static class CustomModules
	{
		private class ModuleGroup
		{
			public Type TModule;

			public Type TBehaviour;

			public ModContainer Mod;

			public bool CanReload;
		}

		private static readonly Dictionary<string, List<ModuleGroup>> registeredModules = new Dictionary<string, List<ModuleGroup>>();

		private static readonly Dictionary<string, ModuleGroup> officialModules = new Dictionary<string, ModuleGroup>
		{
			{
				"Shooting",
				new ModuleGroup
				{
					TModule = typeof(ShootingModule),
					TBehaviour = typeof(ShootingModuleBehaviour),
					CanReload = true
				}
			},
			{
				"Steering",
				new ModuleGroup
				{
					TModule = typeof(SteeringModule),
					TBehaviour = typeof(SteeringModuleBehaviour),
					CanReload = true
				}
			},
			{
				"Spewing",
				new ModuleGroup
				{
					TModule = typeof(SpewingModule),
					TBehaviour = typeof(SpewingModuleBehaviour),
					CanReload = true
				}
			},
			{
				"Spinning",
				new ModuleGroup
				{
					TModule = typeof(SpinningModule),
					TBehaviour = typeof(SpinningModuleBehaviour),
					CanReload = true
				}
			}
		};

		public static void AddBlockModule<TModule, TBehaviour>(string name, bool canReload) where TModule : BlockModule where TBehaviour : BlockModuleBehaviour<TModule>
		{
			ModContainer modByAssembly = AssemblyLoader.GetModByAssembly(Assembly.GetCallingAssembly());
			if (modByAssembly == null)
			{
				throw new InvalidOperationException("CustomModules.AddBlockModule called from an assembly not listed in the mod manifest.");
			}
			Type typeFromHandle = typeof(TModule);
			if (!typeFromHandle.IsDefined(typeof(XmlRootAttribute), false))
			{
				throw new InvalidOperationException("CustomModules: BlockModule class must have an XmlRoot attribute!");
			}
			XmlRootAttribute xmlRootAttribute = (XmlRootAttribute)typeFromHandle.GetCustomAttributes(typeof(XmlRootAttribute), false)[0];
			if (xmlRootAttribute.ElementName != name)
			{
				throw new InvalidOperationException("CustomModules: XmlRoot attribute must specify the same name as given to AddBlockModule.");
			}
			ModuleGroup moduleGroup = new ModuleGroup();
			moduleGroup.TModule = typeFromHandle;
			moduleGroup.TBehaviour = typeof(TBehaviour);
			moduleGroup.Mod = modByAssembly;
			moduleGroup.CanReload = canReload;
			ModuleGroup item = moduleGroup;
			if (!registeredModules.ContainsKey(name))
			{
				registeredModules[name] = new List<ModuleGroup>();
			}
			registeredModules[name].Add(item);
		}

		internal static Type GetBlockBehaviourType(BlockModule module)
		{
			ModuleGroup moduleGroup = GetGroup(module);
			if (moduleGroup == null)
			{
				Debug.LogError("Cannot find behaviour type for module!");
				return null;
			}
			return moduleGroup.TBehaviour;
		}

		internal static bool CanReload(BlockModule module)
		{
			ModuleGroup moduleGroup = GetGroup(module);
			if (moduleGroup == null)
			{
				return false;
			}
			return moduleGroup.CanReload;
		}

		private static ModuleGroup GetGroup(BlockModule module)
		{
			Type moduleType = module.GetType();
			IEnumerable<KeyValuePair<string, ModuleGroup>> source = officialModules.Where((KeyValuePair<string, ModuleGroup> p) => p.Value.TModule == moduleType);
			if (source.Count() != 0)
			{
				return source.First().Value;
			}
			IEnumerable<KeyValuePair<string, List<ModuleGroup>>> source2 = registeredModules.Where((KeyValuePair<string, List<ModuleGroup>> p) => p.Value.Any((ModuleGroup m) => m.TModule == moduleType));
			if (!source2.Any())
			{
				return null;
			}
			return source2.First().Value.First();
		}

		internal static BlockModule[] DeserializeBlockModules(string blockFilePath, ModContainer containingMod)
		{
			List<BlockModule> list = new List<BlockModule>();
			XDocument xDocument = XDocument.Load(blockFilePath, LoadOptions.SetLineInfo);
			XElement xElement = xDocument.Root.Element("Modules");
			BlockModule[] result = new BlockModule[0];
			if (xElement == null)
			{
				return result;
			}
			string name = new FileInfo(blockFilePath).Name;
			foreach (XElement item in xElement.Elements())
			{
				string localName = item.Name.LocalName;
				XAttribute xAttribute = item.Attribute("modid");
				ModuleGroup moduleGroup;
				if (xAttribute == null)
				{
					if (registeredModules.ContainsKey(localName))
					{
						List<ModuleGroup> source = registeredModules[localName];
						moduleGroup = source.FirstOrDefault((ModuleGroup m) => m.Mod == containingMod);
					}
					else
					{
						if (!officialModules.ContainsKey(localName))
						{
							MLog.Error("In " + name + ": No module named " + localName + " can be found in the offical modules or " + containingMod.Info.Name);
							return result;
						}
						moduleGroup = officialModules[localName];
					}
				}
				else
				{
					string modId = xAttribute.Value;
					if (!registeredModules.ContainsKey(localName))
					{
						MLog.Error("In " + name + ": Module " + localName + " is not known.");
						return result;
					}
					List<ModuleGroup> source2 = registeredModules[localName];
					moduleGroup = source2.FirstOrDefault((ModuleGroup m) => m.Mod.Info.Id.ToString() == modId);
					if (moduleGroup == null)
					{
						MLog.Error("In " + name + ": Cannot find module " + localName + " from a mod with ID " + modId);
						return result;
					}
				}
				string content = item.ToString(SaveOptions.None);
				BlockModule blockModule = ModXmlLoader.Deserialize<BlockModule>(content, true, name, ((IXmlLineInfo)item).LineNumber - 1, moduleGroup.TModule);
				if (blockModule == null)
				{
					return result;
				}
				blockModule.Guid = Guid.NewGuid().ToString();
				list.Add(blockModule);
			}
			return list.ToArray();
		}
	}
}
