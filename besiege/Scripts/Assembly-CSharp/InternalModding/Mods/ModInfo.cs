using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using InternalModding.Assemblies;
using InternalModding.Common;
using InternalModding.Misc;
using Modding;
using Modding.Serialization;
using UnityEngine;

namespace InternalModding.Mods
{
	[XmlRoot("Mod")]
	public class ModInfo : Element
	{
		[Serializable]
		public class AssemblyInfo : Element
		{
			[XmlIgnore]
			public ModContainer Mod { get; set; }

			[XmlAttribute("path")]
			public string Path { get; internal set; }

			[DefaultValue(null)]
			[XmlAttribute("minChangeset")]
			public string MinChangeset { get; set; }

			[XmlAttribute("maxChangeset")]
			[DefaultValue(null)]
			public string MaxChangeset { get; set; }

			public virtual void Resolve()
			{
			}
		}

		[Serializable]
		public class ScriptAssemblyInfo : AssemblyInfo
		{
			public override void Resolve()
			{
				base.Path = AssemblyCompiler.ResolveScriptAssembly(base.Path, base.Mod);
			}
		}

		[Serializable]
		public class BlockInfo : Element
		{
			[XmlIgnore]
			public ModContainer Mod { get; set; }

			[XmlAttribute("path")]
			public string Path { get; internal set; }
		}

		[Serializable]
		public class EntityInfo : Element
		{
			[XmlIgnore]
			public ModContainer Mod { get; set; }

			[XmlAttribute("path")]
			public string Path { get; internal set; }
		}

		[Serializable]
		public class ResourceInfo : Element
		{
			[XmlIgnore]
			public ModContainer Mod { get; set; }

			[XmlIgnore]
			public ModResource.ResourceType Type { get; internal set; }

			[XmlAttribute("name")]
			public string Name { get; internal set; }

			[XmlAttribute("path")]
			public string Path { get; internal set; }

			[DefaultValue(true)]
			[XmlAttribute("useMipMaps")]
			public bool UseMipMaps { get; internal set; }

			[DefaultValue(false)]
			[XmlAttribute("readable")]
			public bool Readable { get; internal set; }

			public ResourceInfo()
			{
				UseMipMaps = true;
				Readable = false;
			}

			protected override bool Validate(string name)
			{
				if (!base.Validate(name))
				{
					return false;
				}
				if (Readable && Type != ModResource.ResourceType.Texture)
				{
					return InvalidData(name, "Readable attribute is only valid on Texture elements!");
				}
				return true;
			}
		}

		[Serializable]
		public class ResourcesChoices : Element
		{
			[XmlElement("AssetBundle", typeof(ResourceInfo))]
			[XmlElement("Mesh", typeof(ResourceInfo))]
			[XmlElement("AudioClip", typeof(ResourceInfo))]
			[XmlElement("Texture", typeof(ResourceInfo))]
			[RequireToValidate]
			[XmlChoiceIdentifier("ResourceTypes")]
			[CanBeEmpty]
			public ResourceInfo[] Resources;

			[XmlIgnore]
			public ModResource.ResourceType[] ResourceTypes;

			public ResourcesChoices()
			{
				Resources = new ResourceInfo[0];
				ResourceTypes = new ModResource.ResourceType[0];
			}

			public static implicit operator List<ResourceInfo>(ResourcesChoices choices)
			{
				return new List<ResourceInfo>(choices.Resources);
			}
		}

		[Serializable]
		public class TargetChoices : Element
		{
			public enum TargetType
			{
				Entity = 0,
				ModdedEntity = 1,
				AllOfficialEntities = 2,
				AllModdedEntities = 3
			}

			[XmlElement("ModdedEntity", typeof(ModIdPair))]
			[XmlElement("AllOfficialEntities", typeof(Element), IsNullable = true)]
			[XmlChoiceIdentifier("TargetTypes")]
			[XmlElement("AllModdedEntities", typeof(Element), IsNullable = true)]
			[XmlElement("Entity", typeof(VanillaEntityType))]
			public object[] Targets;

			[XmlIgnore]
			public TargetType[] TargetTypes;

			public TargetChoices()
			{
				Targets = new object[0];
				TargetTypes = new TargetType[0];
			}

			protected override bool Validate(string elementName)
			{
				if (!Targets.All((object t) => !(t is Element) || ((Element)t).InvokeValidate()))
				{
					return false;
				}
				return true;
			}
		}

		[Serializable]
		public class TriggerInfo : Element
		{
			[XmlIgnore]
			public ModContainer Mod { get; set; }

			[XmlElement]
			public string Name { get; set; }

			[XmlElement("ID")]
			public int LocalId { get; set; }

			[XmlElement("AvailableOn")]
			[RequireToValidate]
			public TargetChoices TargetChoices { get; set; }

			public TriggerInfo()
			{
				TargetChoices = new TargetChoices();
				LocalId = -1;
			}
		}

		[Serializable]
		[XmlRoot("Event")]
		public class EventInfo : Element
		{
			[Serializable]
			public class PropertyRow
			{
				[XmlElement("Text", typeof(EventProperty.Text))]
				[XmlElement("Picker", typeof(EventProperty.Picker))]
				[XmlElement("NumberInput", typeof(EventProperty.NumberInput))]
				[XmlElement("Choice", typeof(EventProperty.Choice))]
				[XmlElement("Toggle", typeof(EventProperty.Toggle))]
				[XmlElement("TeamButton", typeof(EventProperty.TeamButton))]
				[XmlElement("Icon", typeof(EventProperty.Icon))]
				[XmlElement("TextInput", typeof(EventProperty.TextInput))]
				public EventProperty[] Properties;
			}

			[XmlAttribute("path")]
			public string Path;

			[XmlArrayItem("Choice", typeof(EventProperty.Choice))]
			[XmlArrayItem("Toggle", typeof(EventProperty.Toggle))]
			[XmlArrayItem("TeamButton", typeof(EventProperty.TeamButton))]
			[XmlArrayItem("Icon", typeof(EventProperty.Icon))]
			[XmlArrayItem("Text", typeof(EventProperty.Text))]
			[XmlArrayItem("Row", typeof(PropertyRow))]
			[XmlArrayItem("Picker", typeof(EventProperty.Picker))]
			[XmlArray("Properties")]
			[XmlArrayItem("NumberInput", typeof(EventProperty.NumberInput))]
			[XmlArrayItem("TextInput", typeof(EventProperty.TextInput))]
			public object[] XmlProperties;

			[XmlIgnore]
			public EventProperty[] Properties;

			[XmlIgnore]
			public ModContainer Mod { get; set; }

			[XmlElement]
			public string Name { get; set; }

			[XmlElement("ID")]
			public int Id { get; set; }

			[XmlElement("Icon")]
			public ResourceReference IconReference { get; set; }

			public EventInfo()
			{
				Properties = new EventProperty[0];
				XmlProperties = new object[0];
				Id = -1;
			}

			public void CreateProperties()
			{
				int num = 0;
				List<EventProperty> list = new List<EventProperty>();
				object[] xmlProperties = XmlProperties;
				foreach (object obj in xmlProperties)
				{
					if (obj is EventProperty.Picker)
					{
						((EventProperty)obj).Row = -1;
						list.Add((EventProperty)obj);
					}
					else if (obj is PropertyRow)
					{
						PropertyRow propertyRow = (PropertyRow)obj;
						if (propertyRow.Properties == null || propertyRow.Properties.Length == 0)
						{
							continue;
						}
						EventProperty[] properties = propertyRow.Properties;
						foreach (EventProperty eventProperty in properties)
						{
							eventProperty.Row = num;
							list.Add(eventProperty);
							if (!eventProperty.XSpecified)
							{
								MLog.Warn("EventProperty in a Row without X attribute. Display will likely be broken!");
							}
						}
						num++;
					}
					else
					{
						EventProperty eventProperty2 = (EventProperty)obj;
						eventProperty2.Row = num;
						num++;
						list.Add(eventProperty2);
						if (eventProperty2.XSpecified)
						{
							MLog.Warn("X attribute of EventProperty will be ignored if it's not a child of a Row element.");
						}
					}
				}
				int num2 = 0;
				foreach (EventProperty item in list)
				{
					if (item is EventProperty.Icon || item is EventProperty.Text)
					{
						item.Name = "gen" + num2;
						num2++;
					}
				}
				Properties = list.ToArray();
			}

			protected override bool Validate(string elementName)
			{
				return true;
			}

			internal bool Validate(string elemName, bool fileLoad)
			{
				if (fileLoad && !string.IsNullOrEmpty(Path))
				{
					return InvalidData(elemName, "Event loaded from seperate file can't have path attribute!");
				}
				bool flag = !string.IsNullOrEmpty(Path);
				bool flag2 = !string.IsNullOrEmpty(Name) || Id != -1;
				if (!flag && !flag2)
				{
					return InvalidData(elemName, "Must contain either a path attribute or the event data.");
				}
				if (flag && flag2)
				{
					return InvalidData(elemName, "Can't contain both a path attribute and the event data.");
				}
				if (flag)
				{
					return true;
				}
				if (string.IsNullOrEmpty(Name))
				{
					return MissingElement(elemName, "Name");
				}
				if (Id == -1)
				{
					return MissingElement(elemName, "ID");
				}
				if (!Properties.All((EventProperty p) => p.InvokeValidate()))
				{
					return false;
				}
				return true;
			}
		}

		[Serializable]
		public class KeyInfo : Element
		{
			[XmlAttribute("name")]
			public string Name;

			[XmlAttribute("defaultModifier")]
			[DefaultValue(KeyCode.None)]
			public KeyCode DefaultModifier;

			[XmlAttribute("defaultTrigger")]
			public KeyCode DefaultTrigger;

			protected override bool Validate(string elemName)
			{
				if (Name.Contains("|"))
				{
					return InvalidData(elemName, "Key name may not contain '|'!");
				}
				return base.Validate(elemName);
			}
		}

		[XmlIgnore]
		public bool IdFromFileSpecified;

		[XmlIgnore]
		public Guid Id { get; private set; }

		[XmlElement("ID")]
		[DefaultValue("")]
		public string IdFromFile { get; private set; }

		[XmlIgnore]
		public bool FromWorkshop { get; set; }

		[XmlIgnore]
		public ulong WorkshopId { get; set; }

		[XmlElement]
		public string Name { get; private set; }

		[XmlElement]
		public string Author { get; private set; }

		[XmlElement]
		public string Description { get; private set; }

		[RequireToValidate]
		[XmlElement("Icon")]
		[DefaultValue(null)]
		public ResourceReference IconReference { get; private set; }

		[XmlIgnore]
		public ModTexture Icon { get; set; }

		[RequireToValidate]
		[DefaultValue(null)]
		[XmlElement("WorkshopThumbnail")]
		public ResourceReference WorkshopThumbnailReference { get; private set; }

		[XmlIgnore]
		public ModTexture WorkshopThumbnail { get; set; }

		[XmlElement]
		public InternalModding.Common.Version Version { get; private set; }

		[XmlElement("Debug")]
		[DefaultValue(false)]
		public bool DebugEnabled { get; private set; }

		[XmlElement]
		public bool MultiplayerCompatible { get; private set; }

		[XmlElement("LoadInTitleScreen")]
		[DefaultValue(null)]
		[RequireToValidate]
		public Element LoadInTitleScreenElement { get; private set; }

		[XmlIgnore]
		public bool LoadInTitleScreen
		{
			get
			{
				return LoadInTitleScreenElement != null;
			}
		}

		[XmlElement("LoadOrder")]
		[DefaultValue(0)]
		public int LoadOrder { get; private set; }

		[XmlIgnore]
		public string Directory { get; private set; }

		[CanBeEmpty]
		[RequireToValidate]
		[XmlArrayItem("Assembly", typeof(AssemblyInfo))]
		[XmlArray]
		[XmlArrayItem("ScriptAssembly", typeof(ScriptAssemblyInfo))]
		public List<AssemblyInfo> Assemblies { get; private set; }

		[CanBeEmpty]
		[XmlArrayItem("Block", typeof(BlockInfo))]
		[XmlArray]
		[RequireToValidate]
		public List<BlockInfo> Blocks { get; private set; }

		[XmlArray]
		[XmlArrayItem("Entity", typeof(EntityInfo))]
		[RequireToValidate]
		[CanBeEmpty]
		public List<EntityInfo> Entities { get; private set; }

		[XmlIgnore]
		public bool IsOnlyEntities
		{
			get
			{
				return Entities.Count > 0 && Assemblies.Count == 0 && Blocks.Count == 0;
			}
		}

		[XmlElement("Resources")]
		[RequireToValidate]
		[DefaultValue(null)]
		public ResourcesChoices ResourceChoices { get; private set; }

		[XmlIgnore]
		public List<ResourceInfo> Resources
		{
			get
			{
				return ResourceChoices;
			}
		}

		[CanBeEmpty]
		[RequireToValidate]
		[XmlArrayItem("Trigger", typeof(TriggerInfo))]
		[XmlArray]
		public List<TriggerInfo> Triggers { get; private set; }

		[CanBeEmpty]
		[XmlArray]
		[XmlArrayItem("Event", typeof(EventInfo))]
		public List<EventInfo> Events { get; private set; }

		[CanBeEmpty]
		[XmlArray]
		[XmlArrayItem("Key", typeof(KeyInfo))]
		[RequireToValidate]
		public List<KeyInfo> Keys { get; private set; }

		public ModInfo()
		{
			Assemblies = new List<AssemblyInfo>();
			Blocks = new List<BlockInfo>();
			Entities = new List<EntityInfo>();
			ResourceChoices = new ResourcesChoices();
			Triggers = new List<TriggerInfo>();
			Events = new List<EventInfo>();
			Keys = new List<KeyInfo>();
			Id = Guid.Empty;
			LoadOrder = 0;
			FromWorkshop = false;
			WorkshopId = 0uL;
		}

		protected override bool Validate(string elemName)
		{
			if (!base.Validate(elemName))
			{
				return false;
			}
			if (!MultiplayerCompatible && LoadInTitleScreen)
			{
				return InvalidData("LoadInTitleScreen", "Cannot specify LoadInTitleScreen in a singleplayer-only mod!");
			}
			if (!Events.All((EventInfo e) => e.Validate("Event", true)))
			{
				return false;
			}
			return true;
		}

		public override bool Equals(object obj)
		{
			ModInfo modInfo = obj as ModInfo;
			if (modInfo == null)
			{
				return false;
			}
			return modInfo.Id == Id;
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public static ModInfo LoadFromFile(string path, bool debugInfo)
		{
			ModInfo modInfo = ModXmlLoader.Deserialize<ModInfo>(path, false);
			if (modInfo == null)
			{
				MLog.Error("Could not load " + new FileInfo(path));
				return null;
			}
			modInfo.Directory = new FileInfo(path).Directory.FullName;
			for (int i = 0; i < modInfo.Events.Count; i++)
			{
				EventInfo eventInfo = modInfo.Events[i];
				if (!eventInfo.Validate("Event", false))
				{
					MLog.Error("Not loading the mod manifest.");
					return null;
				}
				if (!string.IsNullOrEmpty(eventInfo.Path))
				{
					modInfo.Events[i] = ModXmlLoader.Deserialize<EventInfo>(ModPaths.GetFilePath(modInfo, eventInfo.Path), false);
					if (modInfo.Events[i] == null)
					{
						MLog.Error("Not loading the mod manifest.");
						return null;
					}
				}
			}
			foreach (EventInfo @event in modInfo.Events)
			{
				@event.CreateProperties();
			}
			if (modInfo.IdFromFileSpecified)
			{
				modInfo.Id = new Guid(modInfo.IdFromFile);
			}
			else
			{
				modInfo.Id = Guid.NewGuid();
				MLog.Info(string.Concat("Generated an ID for ", modInfo.Name, " (", modInfo.Id, "), writing it to the file."));
				WriteIdElement(modInfo);
			}
			if (!modInfo.Validate())
			{
				MLog.Error("There was an error loading the mod manifest: " + path);
				return null;
			}
			modInfo.Description = modInfo.Description.Trim();
			foreach (AssemblyInfo assembly in modInfo.Assemblies)
			{
				assembly.Path = ModPaths.GetFilePath(modInfo, assembly.Path);
			}
			foreach (BlockInfo block in modInfo.Blocks)
			{
				block.Path = ModPaths.GetFilePath(modInfo, block.Path);
			}
			foreach (EntityInfo entity in modInfo.Entities)
			{
				entity.Path = ModPaths.GetFilePath(modInfo, entity.Path);
			}
			if (modInfo.ResourceChoices.Resources == null)
			{
				modInfo.ResourceChoices.Resources = new ResourceInfo[0];
				modInfo.ResourceChoices.ResourceTypes = new ModResource.ResourceType[0];
			}
			for (int j = 0; j < modInfo.ResourceChoices.Resources.Length; j++)
			{
				ResourceInfo resourceInfo = modInfo.ResourceChoices.Resources[j];
				resourceInfo.Path = ModPaths.GetFilePath(modInfo, resourceInfo.Path, true);
				resourceInfo.Type = modInfo.ResourceChoices.ResourceTypes[j];
			}
			return modInfo;
		}

		private static void WriteIdElement(ModInfo info)
		{
			string path = Path.Combine(info.Directory, "Mod.xml");
			string text = File.ReadAllText(path);
			text = text.Replace("</Mod>", string.Empty);
			text += GetNewIdText(info.Id);
			text += "</Mod>";
			File.WriteAllText(path, text, Encoding.UTF8);
		}

		public static string GetNewIdText(Guid id)
		{
			return string.Concat("\r\n<!-- This value is automatically generated. Do not change it or you may break machine&level save files. -->\r\n<ID>", id, "</ID>\r\n");
		}
	}
}
