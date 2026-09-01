using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using InternalModding.Common;
using InternalModding.Misc;
using InternalModding.Mods;
using Modding;
using Modding.Modules;
using Modding.Modules.Official;
using Modding.Serialization;
using UnityEngine;

namespace InternalModding.Blocks
{
	[XmlRoot("Block")]
	[Reloadable]
	public class ModdedBlock : Element, IReloadable
	{
		[Reloadable]
		[XmlIgnore]
		public bool HealthSpecified;

		[XmlIgnore]
		public bool DamageTypeSpecified;

		[XmlIgnore]
		public bool IceInteractionSpecified;

		[RequireToValidate]
		[Reloadable]
		[XmlElement]
		public TransformValues Icon;

		[RequireToValidate]
		[XmlArray]
		[Reloadable]
		[CanBeEmpty]
		[XmlArrayItem("Object", typeof(MeshTexturePair))]
		public MeshTexturePair[] ExtraIconObjects;

		[XmlIgnore]
		[Reloadable]
		public bool ArrowSpecified;

		[XmlIgnore]
		public GameObject Prefab;

		[XmlIgnore]
		public GameObject StrippedPrefab;

		[XmlIgnore]
		public List<GameObject> AddingPointJoints;

		[XmlIgnore]
		public GameObject Ghost;

		[XmlIgnore]
		public GameObject BlockButton;

		[XmlIgnore]
		public BlockPrefab BlockPrefab;

		[XmlIgnore]
		public bool PrefabCreated;

		[XmlIgnore]
		public bool PrefabRegistered;

		[XmlIgnore]
		public bool CalledOnPrefabRegistered;

		[XmlIgnore]
		public bool HideInUI;

		[XmlIgnore]
		public ModInfo.BlockInfo Info { get; set; }

		[XmlElement("Debug")]
		[Reloadable]
		[DefaultValue(false)]
		public bool DebugVisuals { get; set; }

		[XmlElement("ID")]
		public int LocalId { get; set; }

		[XmlIgnore]
		public int Id { get; set; }

		[DefaultValue(0)]
		[XmlElement("Replaces")]
		public int ReplacesBlock { get; set; }

		[XmlElement("Fallback")]
		[DefaultValue(null)]
		[RequireToValidate]
		public VanillaBlockType Fallback { get; set; }

		[XmlElement]
		public string Name { get; set; }

		[XmlElement]
		[Reloadable]
		public float Mass { get; set; }

		[DefaultValue(0f)]
		[XmlElement]
		[Reloadable]
		public float Density { get; set; }

		[XmlElement]
		[DefaultValue(false)]
		[Reloadable]
		public bool CanFlip { get; set; }

		[DefaultValue("")]
		[XmlElement("Script")]
		public string ScriptName { get; set; }

		[XmlIgnore]
		public Type ScriptType { get; set; }

		[DefaultValue(null)]
		[XmlElement("Modules")]
		public Element ModulesDummyElement { get; set; }

		[XmlIgnore]
		public BlockModule[] Modules { get; set; }

		[XmlArray("ModuleMapperTypes")]
		[RequireToValidate]
		[CanBeEmpty]
		[XmlArrayItem("Key", typeof(MKeyDefinition))]
		[XmlArrayItem("Slider", typeof(MSliderDefinition))]
		[XmlArrayItem("Toggle", typeof(MToggleDefinition))]
		[XmlArrayItem("Value", typeof(MValueDefinition))]
		[XmlArrayItem("ColourSlider", typeof(MColourSliderDefinition))]
		public MapperTypeDefinition[] ModuleMapperTypes { get; set; }

		[XmlIgnore]
		public bool CanTakeDamage
		{
			get
			{
				return HealthSpecified;
			}
		}

		[DefaultValue(0f)]
		[XmlElement]
		[Reloadable]
		public float Health { get; set; }

		[XmlIgnore]
		public bool HasDamageType
		{
			get
			{
				return DamageTypeSpecified;
			}
		}

		[XmlElement]
		[DefaultValue(DamageType.Blunt)]
		public DamageType DamageType { get; set; }

		[DefaultValue(null)]
		[XmlElement("FireInteraction")]
		[RequireToValidate]
		[Reloadable]
		public FireInteraction FireInteraction { get; set; }

		[XmlIgnore]
		public bool CanBurn
		{
			get
			{
				return FireInteraction != null;
			}
		}

		[RequireToValidate]
		[DefaultValue(null)]
		[XmlElement("IceInteraction")]
		public Element IceInteraction { get; set; }

		[XmlIgnore]
		public bool CanFreeze
		{
			get
			{
				return IceInteractionSpecified;
			}
		}

		[Reloadable]
		[XmlElement("Mesh")]
		[RequireToValidate]
		public MeshReference MeshReference { get; set; }

		[XmlIgnore]
		public ModMesh Mesh { get; private set; }

		[XmlElement("Texture")]
		[RequireToValidate]
		public ResourceReference TextureReference { get; set; }

		[XmlIgnore]
		public ModTexture Texture { get; private set; }

		[XmlIgnore]
		public Texture2D BlockTypeIcon { get; set; }

		[XmlIgnore]
		public Sprite BlockTypeSprite { get; set; }

		[Reloadable]
		[XmlElement]
		[RequireToValidate]
		public BasePoint BasePoint { get; set; }

		[CanBeEmpty]
		[RequireToValidate]
		[Reloadable]
		[XmlArray]
		[XmlArrayItem("AddingPoint")]
		public List<AddingPoint> AddingPoints { get; set; }

		[Reloadable]
		[XmlArray]
		[XmlArrayItem("BoxCollider", typeof(BoxModCollider))]
		[XmlArrayItem("CapsuleCollider", typeof(CapsuleModCollider))]
		[RequireToValidate]
		[XmlArrayItem("SphereCollider", typeof(SphereModCollider))]
		public List<ModCollider> Colliders { get; set; }

		[XmlElement("Ghost")]
		[DefaultValue(null)]
		[RequireToValidate]
		public Ghost GhostInfo { get; set; }

		[XmlIgnore]
		public List<ModCollider> GhostColliders
		{
			get
			{
				return GhostInfo.GhostColliders ?? Colliders.Where((ModCollider c) => !c.IgnoreForGhost).ToList();
			}
		}

		[XmlArray]
		[CanBeEmpty]
		[XmlArrayItem("Keyword", typeof(string))]
		public string[] SearchKeywords { get; set; }

		[CanBeEmpty]
		[XmlArray]
		[XmlArrayItem("Object", typeof(string))]
		public string[] KeepWhenStripped { get; set; }

		[Reloadable]
		[XmlElement]
		[RequireToValidate]
		[DefaultValue(null)]
		public TransformValues Arrow { get; set; }

		public ModdedBlock()
		{
			DebugVisuals = false;
			Health = 0f;
			FireInteraction = null;
			IceInteraction = null;
			LocalId = -1;
			SearchKeywords = new string[0];
			KeepWhenStripped = new string[0];
			ModuleMapperTypes = new MapperTypeDefinition[0];
			GhostInfo = new Ghost
			{
				GhostColliders = null,
				Hammer = new TransformValues
				{
					Position = UnityEngine.Vector3.zero,
					Rotation = UnityEngine.Vector3.zero
				}
			};
		}

		protected override bool Validate()
		{
			if (ReplacesBlock != 0 && ReplacesBlock <= 100)
			{
				return InvalidData("ReplacesBlock", "Must be greater than 100! You may only replace modded blocks created for the old block loader!");
			}
			if (LocalId < 0)
			{
				return InvalidData("LocalId", "Cannot be negative!");
			}
			return base.Validate("Block");
		}

		public void PreprocessForReloading()
		{
			LoadAssets();
		}

		public void OnReload(IReloadable newBlock)
		{
			ModdedBlock moddedBlock = (ModdedBlock)newBlock;
			moddedBlock.LoadModules();
			Dictionary<Type, int> dictionary = new Dictionary<Type, int>();
			BlockModule[] modules = Modules;
			BlockModule oldModule;
			for (int i = 0; i < modules.Length; i++)
			{
				oldModule = modules[i];
				if (CustomModules.CanReload(oldModule))
				{
					List<BlockModule> list = moddedBlock.Modules.Where((BlockModule m) => m.GetType() == oldModule.GetType()).ToList();
					if (!dictionary.ContainsKey(oldModule.GetType()))
					{
						Serialization.Reload(oldModule, list[0]);
						dictionary[oldModule.GetType()] = 1;
						continue;
					}
					Serialization.Reload(oldModule, list[dictionary[oldModule.GetType()]]);
					Dictionary<Type, int> dictionary3;
					Dictionary<Type, int> dictionary2 = (dictionary3 = dictionary);
					Type type;
					Type key = (type = oldModule.GetType());
					int num = dictionary3[type];
					dictionary2[key] = num + 1;
				}
			}
			if (Prefab != null)
			{
				BlockModule[] modules2 = Modules;
				foreach (BlockModule blockModule in modules2)
				{
					ShootingModule shootingModule = blockModule as ShootingModule;
					if (shootingModule != null)
					{
						shootingModule.OnReload(Prefab.GetComponent<ModBlockBehaviourHandler>());
					}
				}
				BlockPrefabCreator.UpdatePrefab(this);
			}
			BlockButtonCreator.UpdateIcon(this);
		}

		public void LoadAssets()
		{
			Mesh = (ModMesh)ModResource.Get(MeshReference, Info.Mod);
			Texture = (ModTexture)ModResource.Get(TextureReference, Info.Mod);
			if (ExtraIconObjects != null)
			{
				MeshTexturePair[] extraIconObjects = ExtraIconObjects;
				foreach (MeshTexturePair meshTexturePair in extraIconObjects)
				{
					meshTexturePair.Mesh = (ModMesh)ModResource.Get(meshTexturePair.MeshReference, Info.Mod);
					meshTexturePair.Texture = (ModTexture)ModResource.Get(meshTexturePair.TextureReference, Info.Mod);
				}
			}
			if (Mesh == null)
			{
				MLog.Error("Could not find Mesh resource named " + MeshReference.Name);
			}
			if (Texture == null)
			{
				MLog.Error("Could not find Texture reference named " + TextureReference.Name);
			}
		}

		public bool AssetsLoaded()
		{
			return Mesh.Loaded && Texture.Loaded;
		}

		public void ReadyForOnPrefabCreation()
		{
			if (CalledOnPrefabRegistered)
			{
				return;
			}
			CalledOnPrefabRegistered = true;
			Action callOnPrefabCreation = delegate
			{
				BlockScript component = Prefab.GetComponent<BlockScript>();
				if (component != null)
				{
					ModdingUtil.PerformCallback(component.OnPrefabCreation);
				}
				component = StrippedPrefab.GetComponent<BlockScript>();
				if (component != null)
				{
					ModdingUtil.PerformCallback(component.OnPrefabCreation);
				}
			};
			if (AssetsLoaded())
			{
				callOnPrefabCreation();
				return;
			}
			Mesh.OnLoad += delegate
			{
				if (Texture.Loaded)
				{
					callOnPrefabCreation();
				}
				else
				{
					Texture.OnLoad += callOnPrefabCreation;
				}
			};
		}

		public void LoadModules()
		{
			Modules = CustomModules.DeserializeBlockModules(Info.Path, Info.Mod);
		}
	}
}
