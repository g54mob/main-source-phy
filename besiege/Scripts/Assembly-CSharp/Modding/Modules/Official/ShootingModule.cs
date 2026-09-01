using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using InternalModding.Blocks;
using InternalModding.Common;
using Modding.Serialization;
using UnityEngine;

namespace Modding.Modules.Official
{
	[XmlRoot("Shooting")]
	[Reloadable]
	public class ShootingModule : BlockModule
	{
		[Serializable]
		public class Projectile : Element
		{
			[RequireToValidate]
			[XmlElement("Mesh")]
			public MeshReference Mesh;

			[RequireToValidate]
			[XmlElement("Texture")]
			public ResourceReference Texture;

			[XmlArray("Colliders")]
			[XmlArrayItem("BoxCollider", typeof(BoxModCollider))]
			[XmlArrayItem("SphereCollider", typeof(SphereModCollider))]
			[RequireToValidate]
			[XmlArrayItem("CapsuleCollider", typeof(CapsuleModCollider))]
			public ModCollider[] Colliders;

			[XmlElement("Mass")]
			public float Mass;

			[DefaultValue(false)]
			[XmlElement("IgnoreGravity")]
			public bool IgnoreGravity;

			[DefaultValue(0f)]
			[XmlElement("Drag")]
			public float Drag;

			[XmlElement("AngularDrag")]
			[DefaultValue(5f)]
			public float AngularDrag = 5f;

			[XmlElement("EntityDamage")]
			[DefaultValue(100f)]
			public float EntityDamage = 100f;

			[XmlElement("BlockDamage")]
			[DefaultValue(1f)]
			public float BlockDamage = 1f;

			[XmlElement("Attaches")]
			public bool Attaches;

			[DefaultValue(null)]
			[RequireToValidate]
			[XmlElement("FireInteraction")]
			public FireInteraction FireInteraction;
		}

		[Serializable]
		public class CrossbowSounds
		{
		}

		[Serializable]
		public class CannonSound
		{
		}

		public class ModdedProjectileColliderComponent : MonoBehaviour
		{
			public ProjectileScript projectileScript;

			public void OnCollisionEnter(Collision col)
			{
				projectileScript.OnCollisionEnter(col);
			}
		}

		internal int ProjectileId;

		[Reloadable]
		[RequireToValidate]
		[XmlElement("Projectile")]
		public Projectile ProjectileInfo;

		[XmlElement("ProjectileStart")]
		[Reloadable]
		public TransformValues ProjectileStart;

		[DefaultValue(false)]
		[XmlElement("ShowPlaceholderProjectile")]
		public bool ShowPlaceholderProjectile;

		[Reloadable]
		[XmlElement("DefaultAmmo")]
		public int DefaultAmmo;

		[Reloadable]
		[XmlElement("AmmoType")]
		[DefaultValue(ReloadAmmoType.All)]
		public ReloadAmmoType AmmoType;

		[Reloadable]
		[DefaultValue(false)]
		[XmlElement("ProjectilesExplode")]
		public bool ProjectilesExplode;

		[Reloadable]
		[XmlElement("SupportsExplosionGodTool")]
		[DefaultValue(true)]
		public bool SupportsExplosionGodTool = true;

		[DefaultValue(false)]
		[Reloadable]
		[XmlElement("ProjectilesDespawnImmediately")]
		public bool ProjectilesDespawnImmediately;

		[Reloadable]
		[XmlElement("TriggeredByFire")]
		[DefaultValue(false)]
		public bool TriggeredByFire;

		[RequireToValidate]
		[XmlElement("FireKey")]
		public MKeyReference FireKey;

		[RequireToValidate]
		[XmlElement("PowerSlider")]
		public MSliderReference PowerSlider;

		[RequireToValidate]
		[XmlElement("RateOfFireSlider")]
		public MSliderReference RateOfFireSlider;

		[XmlElement("HoldToShootToggle")]
		[RequireToValidate]
		public MToggleReference HoldToShootToggle;

		[XmlElement("RecoilMultiplier")]
		[Reloadable]
		[DefaultValue(0.1f)]
		public float RecoilMultiplier = 0.1f;

		[DefaultValue(10)]
		[XmlElement("PoolSize")]
		public int PoolSize = 10;

		[XmlArrayItem("CannonSound", typeof(CannonSound))]
		[XmlArrayItem("AudioClip", typeof(ResourceReference))]
		[XmlArray("Sounds")]
		[XmlArrayItem("CrossbowSounds", typeof(CrossbowSounds))]
		[RequireToValidate]
		[CanBeEmpty]
		public object[] Sounds;

		private GameObject Prefab;

		protected override bool Validate(string elemName)
		{
			if (!base.Validate(elemName))
			{
				return false;
			}
			if (!ProjectileStart.HasNoScale().Check("ProjectileStart"))
			{
				return false;
			}
			if (AmmoType == ReloadAmmoType.Random)
			{
				return InvalidData(elemName, "AmmoType cannot be Random!");
			}
			return true;
		}

		public GameObject GetProjectilePrefab(ModBlockBehaviourHandler behaviour)
		{
			if (Prefab != null)
			{
				return Prefab;
			}
			Prefab = UnityEngine.Object.Instantiate(SingleInstanceFindOnly<BlockLoader>.Instance.ModulePrefabs.ShootingProjectile);
			ProjectileInfo component = Prefab.GetComponent<ProjectileInfo>();
			ProjectileScript component2 = Prefab.GetComponent<ProjectileScript>();
			NetworkProjectile component3 = Prefab.GetComponent<NetworkProjectile>();
			component.projectileType = (NetworkProjectileType)ProjectileId;
			MeshReference mesh = ProjectileInfo.Mesh;
			ModMesh modMesh = (ModMesh)behaviour.GetResource(ProjectileInfo.Mesh);
			component.MeshRenderer.GetComponent<MeshFilter>().mesh = null;
			modMesh.SetOnObject(component.MeshRenderer.gameObject);
			Transform transform = component.MeshRenderer.transform;
			ModTexture modTexture = (ModTexture)behaviour.GetResource(ProjectileInfo.Texture);
			Material material = component.MeshRenderer.material;
			component.MeshRenderer.material = GameMaterials.GetMaterial(GameMaterials.MiscMaterial.Loading);
			modTexture.SetOnObject(component.MeshRenderer.gameObject, material);
			transform.localPosition = mesh.Position;
			transform.localRotation = Quaternion.Euler(mesh.Rotation);
			transform.localScale = mesh.Scale;
			Rigidbody component4 = Prefab.GetComponent<Rigidbody>();
			component4.useGravity = !ProjectileInfo.IgnoreGravity;
			component4.mass = (component3.bodyMass = ProjectileInfo.Mass);
			component4.drag = (component3.bodyDrag = ProjectileInfo.Drag);
			component4.angularDrag = (component3.bodyAngularDrag = ProjectileInfo.AngularDrag);
			Transform transform2 = Prefab.transform.FindChild("Gyro");
			Transform transform3 = new GameObject("Colliders").transform;
			transform3.parent = transform2;
			transform3.localPosition = UnityEngine.Vector3.zero;
			transform3.localRotation = Quaternion.identity;
			transform3.localScale = UnityEngine.Vector3.one;
			List<Collider> list = new List<Collider>();
			ModCollider[] colliders = ProjectileInfo.Colliders;
			foreach (ModCollider modCollider in colliders)
			{
				Collider collider = modCollider.CreateCollider(transform3);
				list.Add(collider);
				collider.gameObject.AddComponent<ModdedProjectileColliderComponent>().projectileScript = component2;
				if (behaviour.moddedBlock.DebugVisuals)
				{
					Transform transform4 = modCollider.CreateVisual(transform3);
					transform4.gameObject.AddComponent<ModdedProjectileColliderComponent>().projectileScript = component2;
				}
			}
			ProjectileInfo.FireInteraction.SetOnObject(Prefab, transform2.FindChild("FireControl"), transform2.FindChild("Fire Particles"), component, behaviour.moddedBlock.DebugVisuals);
			component2.cols = list.ToArray();
			component2.canExplode = SupportsExplosionGodTool;
			component2.alwaysExplodes = ProjectilesExplode;
			component2.despawnOnCollision = ProjectilesDespawnImmediately;
			component2.attackDamage = ProjectileInfo.EntityDamage;
			component2.blockDamageAmount = ProjectileInfo.BlockDamage;
			component2.canAttach = ProjectileInfo.Attaches;
			component2.disableCollider = false;
			return Prefab;
		}

		public GameObject GetProjectilePlaceholder(ModBlockBehaviourHandler behaviour)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(GetProjectilePrefab(behaviour));
			UnityEngine.Object.Destroy(gameObject.GetComponent<NetworkProjectile>());
			UnityEngine.Object.Destroy(gameObject.GetComponent<ProjectileScript>());
			UnityEngine.Object.Destroy(gameObject.GetComponent<FireTag>());
			UnityEngine.Object.Destroy(gameObject.GetComponent<ProjectileInfo>());
			UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
			UnityEngine.Object.Destroy(gameObject.transform.FindChild("Gyro/Fire Particles").gameObject);
			Transform transform = gameObject.transform.FindChild("Gyro/FireControl");
			Renderer componentInChildren = transform.GetComponentInChildren<Renderer>();
			if (componentInChildren != null)
			{
				componentInChildren.transform.SetParent(gameObject.transform, true);
			}
			UnityEngine.Object.Destroy(transform.gameObject);
			return gameObject;
		}

		public void OnReload(ModBlockBehaviourHandler behaviour)
		{
			if (Prefab == null)
			{
				return;
			}
			UpdateProjectileObject(Prefab, behaviour);
			if (!ProjectileManager.Instance)
			{
				return;
			}
			ProjectileManager instance = ProjectileManager.Instance;
			ProjectileManager.ProjectilePool pool = instance.GetPool(ProjectileId);
			foreach (NetworkProjectile item in pool.Active)
			{
				UpdateProjectileObject(item.gameObject, behaviour);
			}
			foreach (NetworkProjectile item2 in pool.Pool)
			{
				UpdateProjectileObject(item2.gameObject, behaviour);
			}
		}

		internal void UpdateProjectileObject(GameObject projectile, ModBlockBehaviourHandler behaviour)
		{
			ProjectileInfo component = projectile.GetComponent<ProjectileInfo>();
			ProjectileScript component2 = projectile.GetComponent<ProjectileScript>();
			NetworkProjectile component3 = projectile.GetComponent<NetworkProjectile>();
			MeshReference mesh = ProjectileInfo.Mesh;
			Transform transform = component.MeshRenderer.transform;
			transform.localPosition = mesh.Position;
			transform.localRotation = Quaternion.Euler(mesh.Rotation);
			transform.localScale = mesh.Scale;
			Rigidbody component4 = projectile.GetComponent<Rigidbody>();
			component4.useGravity = !ProjectileInfo.IgnoreGravity;
			component4.mass = (component3.bodyMass = ProjectileInfo.Mass);
			component4.drag = (component3.bodyDrag = ProjectileInfo.Drag);
			component4.angularDrag = (component3.bodyAngularDrag = ProjectileInfo.AngularDrag);
			Transform transform2 = projectile.transform.FindChild("Gyro");
			Transform transform3 = transform2.FindChild("Colliders");
			UnityEngine.Object.DestroyImmediate(transform3.gameObject);
			transform3 = new GameObject("Colliders").transform;
			transform3.parent = transform2;
			transform3.localPosition = UnityEngine.Vector3.zero;
			transform3.localRotation = Quaternion.identity;
			transform3.localScale = UnityEngine.Vector3.one;
			List<Collider> list = new List<Collider>();
			ModCollider[] colliders = ProjectileInfo.Colliders;
			foreach (ModCollider modCollider in colliders)
			{
				Collider collider = modCollider.CreateCollider(transform3);
				list.Add(collider);
				collider.gameObject.AddComponent<ModdedProjectileColliderComponent>().projectileScript = component2;
				if (behaviour.moddedBlock.DebugVisuals)
				{
					Transform transform4 = modCollider.CreateVisual(transform3);
					transform4.gameObject.AddComponent<ModdedProjectileColliderComponent>().projectileScript = component2;
				}
			}
			component2.cols = list.ToArray();
			component2.col = list[0];
			ProjectileInfo.FireInteraction.SetOnObject(projectile, transform2.FindChild("FireControl"), transform2.FindChild("Fire Particles"), component, behaviour.moddedBlock.DebugVisuals);
			component2.canExplode = SupportsExplosionGodTool;
			component2.alwaysExplodes = ProjectilesExplode;
			component2.despawnOnCollision = ProjectilesDespawnImmediately;
			component2.attackDamage = ProjectileInfo.EntityDamage;
			component2.blockDamageAmount = ProjectileInfo.BlockDamage;
			component2.canAttach = ProjectileInfo.Attaches;
			component2.disableCollider = false;
		}
	}
}
