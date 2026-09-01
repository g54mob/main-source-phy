using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using InternalModding.Common;
using InternalModding.Mods;
using Modding;
using Modding.Serialization;
using UnityEngine;

namespace InternalModding.LevelEntities
{
	[XmlRoot("Entity")]
	public class ModdedEntity : Element, IReloadable
	{
		[XmlElement("Fallback")]
		[RequireToValidate]
		[DefaultValue(null)]
		public VanillaEntityType Fallback;

		[DefaultValue(null)]
		[XmlElement]
		[Reloadable]
		public Modding.Serialization.Vector3 Offset;

		[XmlElement]
		[Reloadable]
		[DefaultValue(null)]
		public Modding.Serialization.Vector3 Rotation;

		[XmlIgnore]
		public GameObject Prefab;

		[XmlIgnore]
		public LevelPrefab LevelPrefab;

		[XmlIgnore]
		public bool PrefabCreated;

		[XmlIgnore]
		public bool PrefabRegistered;

		[XmlIgnore]
		public bool HideInUI;

		[XmlIgnore]
		public ModInfo.EntityInfo Info { get; set; }

		[DefaultValue(false)]
		[XmlElement("Debug")]
		public bool DebugVisuals { get; set; }

		[XmlElement("ID")]
		public int LocalId { get; set; }

		[XmlIgnore]
		public int Id { get; set; }

		[XmlElement]
		public string Name { get; set; }

		[XmlElement("Mesh")]
		[Reloadable]
		[RequireToValidate]
		public MeshReference MeshReference { get; set; }

		[XmlIgnore]
		public ModMesh Mesh { get; private set; }

		[XmlElement("Texture")]
		[RequireToValidate]
		public ResourceReference TextureReference { get; set; }

		[XmlIgnore]
		public ModTexture Texture { get; private set; }

		[RequireToValidate]
		[XmlElement("Icon")]
		public ResourceReference IconReference { get; set; }

		[XmlIgnore]
		public ModTexture Icon { get; private set; }

		[XmlElement]
		public StatMaster.Category Category { get; set; }

		[XmlElement]
		[DefaultValue(null)]
		public EntityScale Scale { get; set; }

		[DefaultValue(true)]
		[XmlElement]
		public bool CanPick { get; set; }

		[DefaultValue(true)]
		[XmlElement]
		public bool ShowPhysicsToggle { get; set; }

		[XmlArrayItem("Trigger", typeof(TriggerType))]
		[RequireToValidate]
		[XmlArrayItem("ModdedTrigger", typeof(ModIdPair))]
		[XmlArray]
		[CanBeEmpty]
		public List<object> Triggers { get; set; }

		[RequireToValidate]
		[DefaultValue(null)]
		[XmlElement("FireInteraction")]
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

		[XmlElement]
		[Reloadable]
		[DefaultValue(null)]
		public Destructible Destructible { get; set; }

		[XmlIgnore]
		public bool IsDestructable
		{
			get
			{
				return Destructible != null;
			}
		}

		[XmlIgnore]
		public Destructible.BreakForceWrapper BreakForce
		{
			get
			{
				return Destructible.BreakForce;
			}
		}

		[XmlArrayItem("CapsuleCollider", typeof(CapsuleModCollider))]
		[Reloadable]
		[XmlArray]
		[XmlArrayItem("SphereCollider", typeof(SphereModCollider))]
		[RequireToValidate]
		[XmlArrayItem("BoxCollider", typeof(BoxModCollider))]
		public List<ModCollider> Colliders { get; set; }

		public ModdedEntity()
		{
			CanPick = true;
			ShowPhysicsToggle = true;
			LocalId = -1;
			Colliders = new List<ModCollider>();
			Offset = new UnityEngine.Vector3(0f, 0f, 0f);
			Scale = new EntityScale
			{
				CanScale = true,
				UniformScale = false
			};
		}

		protected override bool Validate()
		{
			if (LocalId < 0)
			{
				return InvalidData("LocalId", "Cannot be negative!");
			}
			return Validate("Entity");
		}

		public void LoadAssets()
		{
			Mesh = (ModMesh)ModResource.Get(MeshReference, Info.Mod);
			Texture = (ModTexture)ModResource.Get(TextureReference, Info.Mod);
			Icon = (ModTexture)ModResource.Get(IconReference, Info.Mod);
			if (!IsDestructable)
			{
				return;
			}
			foreach (Destructible.Particle particle in Destructible.Particles)
			{
				particle.Mesh = (ModMesh)ModResource.Get(particle.MeshReference, Info.Mod);
			}
			if (Destructible.SoundReference != null)
			{
				Destructible.Sound = (ModAudioClip)ModResource.Get(Destructible.SoundReference, Info.Mod);
			}
		}

		public void PreprocessForReloading()
		{
			LoadAssets();
		}

		public void OnReload(IReloadable newEntity)
		{
			if (Prefab != null)
			{
				EntityPrefabCreator.UpdatePrefab(this);
			}
		}

		public bool AssetsLoaded()
		{
			return Mesh.Loaded && Texture.Loaded && Icon.Loaded && (!IsDestructable || (Destructible.Particles.TrueForAll((Destructible.Particle p) => p.Mesh.Loaded) && (!IsDestructable || Destructible.SoundReference == null || Destructible.Sound.Loaded)));
		}
	}
}
