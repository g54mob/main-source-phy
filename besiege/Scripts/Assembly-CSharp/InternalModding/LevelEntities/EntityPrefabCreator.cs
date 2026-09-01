using System.Collections.Generic;
using System.Linq;
using InternalModding.Common;
using InternalModding.Misc;
using Modding.Serialization;
using UnityEngine;

namespace InternalModding.LevelEntities
{
	public static class EntityPrefabCreator
	{
		public static void CreatePrefab(ModdedEntity info, Transform parent)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(SingleInstanceFindOnly<EntityLoader>.Instance.EntityTemplate, parent);
			gameObject.name = "Modded: " + info.Name;
			gameObject.SetActive(false);
			LevelPrefab prefab = gameObject.GetComponent<LevelPrefab>();
			prefab.ID = info.Id;
			prefab.LocalisationID = -info.Id;
			prefab.offset = info.Offset;
			prefab.rotation = info.Rotation;
			prefab.icon = null;
			if (info.Icon == null)
			{
				MLog.Error("Could not find Entity Icon resource: " + info.IconReference.Name);
			}
			else
			{
				info.Icon.OnLoad += delegate
				{
					prefab.icon = info.Icon;
					if (SingleInstanceFindOnly<LevelEditorUI>.hasInstance())
					{
						SingleInstanceFindOnly<LevelEditorUI>.Instance.UpdateIcons();
					}
				};
			}
			if (info.Category == StatMaster.Category.Weather)
			{
				info.Category = StatMaster.Category.Virtual;
			}
			prefab.category = info.Category;
			if (info.Triggers.Count > 0)
			{
				prefab.events = info.Triggers.OfType<TriggerType>().ToArray();
			}
			else
			{
				prefab.events = new TriggerType[4]
				{
					TriggerType.LevelStart,
					TriggerType.Activate,
					TriggerType.Deactivate,
					TriggerType.Variable
				};
			}
			Transform transform = gameObject.transform.FindChild("Object");
			transform.name = info.Name;
			SetupVis(info, transform.FindChild("Vis"));
			SetupColliders(info.Colliders, transform, info.DebugVisuals);
			prefab.canScale = info.Scale.CanScale;
			prefab.uniformScale = info.Scale.UniformScale;
			prefab.canPick = info.CanPick;
			prefab.showPhysicsToggle = info.ShowPhysicsToggle;
			prefab.ignorePhysics = !info.ShowPhysicsToggle;
			SetupFire(info, prefab, transform);
			prefab.destructable = info.IsDestructable;
			if (info.IsDestructable)
			{
				BreakOnForce component = gameObject.GetComponent<BreakOnForce>();
				component.ForceToBreak = info.Destructible.ForceToBreak;
				component.breakPower = info.BreakForce.Power;
				component.breakForceRadius = info.BreakForce.Radius;
				component.BreakInto = SetupParticles(info);
			}
			info.Prefab = gameObject;
			info.LevelPrefab = prefab;
		}

		public static void UpdatePrefab(ModdedEntity entity)
		{
			GameObject prefab = entity.Prefab;
			UpdateObject(entity, prefab);
			foreach (LevelEntity entity2 in LevelEditor.Instance.Entities)
			{
				if (entity2.behaviour.prefab.ID == entity.Id)
				{
					UpdateObject(entity, entity2.gameObject);
				}
			}
			entity.LevelPrefab.offset = entity.Offset;
			entity.LevelPrefab.rotation = entity.Rotation;
		}

		private static void UpdateObject(ModdedEntity entity, GameObject obj)
		{
			Transform transform = obj.transform.FindChild(entity.Name);
			SetupVis(entity, transform.FindChild("Vis"));
			SetupColliders(entity.Colliders, transform, entity.DebugVisuals);
			SetupFire(entity, entity.LevelPrefab, transform);
			if (entity.IsDestructable)
			{
				BreakOnForce component = obj.GetComponent<BreakOnForce>();
				component.ForceToBreak = entity.Destructible.ForceToBreak;
				component.breakPower = entity.BreakForce.Power;
				component.breakForceRadius = entity.BreakForce.Radius;
				if (obj == entity.Prefab)
				{
					Object.Destroy(component.BreakInto.gameObject);
					component.BreakInto = SetupParticles(entity);
				}
				else
				{
					component.BreakInto = entity.Prefab.GetComponent<BreakOnForce>().BreakInto;
				}
			}
		}

		private static void SetupVis(ModdedEntity info, Transform vis)
		{
			MeshRenderer component = vis.GetComponent<MeshRenderer>();
			MeshFilter component2 = vis.GetComponent<MeshFilter>();
			Material material = component.material;
			component.material = SingleInstanceFindOnly<EntityLoader>.Instance.LoadingMaterial;
			component2.sharedMesh = SingleInstanceFindOnly<EntityLoader>.Instance.LoadingMesh;
			vis.localScale = new UnityEngine.Vector3(10f, 10f, 10f);
			vis.localPosition = UnityEngine.Vector3.up * 2.5f;
			if (info.Texture != null)
			{
				info.Texture.SetOnObject(vis.gameObject, material, delegate(GameObject go)
				{
					Renderer component3 = go.GetComponent<Renderer>();
					BreakOnForce component4 = go.transform.parent.parent.GetComponent<BreakOnForce>();
					if (!(component4 == null) && !(component4.BrokenInstance == null))
					{
						CopyMaterial component5 = component4.BrokenInstance.GetComponent<CopyMaterial>();
						if (!(component5 == null))
						{
							component5.CopyMat(component3);
						}
					}
				});
			}
			if (info.Mesh != null)
			{
				info.Mesh.SetOnObject(vis.gameObject, info.MeshReference);
			}
		}

		private static void SetupColliders(List<ModCollider> colliders, Transform entity, bool visuals)
		{
			Transform transform = entity.FindChild("Colliders");
			if ((bool)transform)
			{
				Object.DestroyImmediate(transform.gameObject);
			}
			transform = new GameObject("Colliders").transform;
			transform.SetParent(entity, false);
			Transform transform2 = entity.FindChild("Collider Vis");
			if ((bool)transform2)
			{
				Object.DestroyImmediate(transform2.gameObject);
			}
			if (visuals)
			{
				transform2 = new GameObject("Collider Vis").transform;
				transform2.SetParent(entity, false);
			}
			foreach (ModCollider collider in colliders)
			{
				collider.CreateCollider(transform);
				if (visuals)
				{
					collider.CreateVisual(transform2);
				}
			}
		}

		private static void SetupFire(ModdedEntity info, LevelPrefab prefab, Transform child)
		{
			prefab.inflammable = info.CanBurn;
			info.FireInteraction.SetOnObject(prefab.gameObject, child.Find("FireController"), child.Find("Fire Particles"), prefab.GetComponent<BasicInfo>(), info.DebugVisuals);
		}

		private static Transform SetupParticles(ModdedEntity info)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(SingleInstanceFindOnly<EntityLoader>.Instance.ParticlesTemplate, SingleInstanceFindOnly<EntityLoader>.Instance.ParticlesParent);
			gameObject.name = info.Name + " Particles";
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < info.Destructible.Particles.Count; i++)
			{
				Destructible.Particle particle = info.Destructible.Particles[i];
				GameObject gameObject2 = (GameObject)Object.Instantiate(SingleInstanceFindOnly<EntityLoader>.Instance.ParticleTemplate, gameObject.transform);
				Transform transform = gameObject2.transform.FindChild("Vis");
				transform.GetComponent<MeshFilter>().sharedMesh = SingleInstanceFindOnly<EntityLoader>.Instance.LoadingMesh;
				particle.Mesh.SetOnObject(transform.gameObject, particle.MeshReference);
				SetupColliders(particle.Colliders, gameObject2.transform, info.DebugVisuals);
				list.Add(gameObject2);
			}
			if (info.Destructible.SoundReference != null)
			{
				if (info.Destructible.Sound == null)
				{
					MLog.Error("Could not find Break Sound AudioClip resource: " + info.Destructible.SoundReference.Name);
				}
				else
				{
					gameObject.GetComponent<AudioSource>().clip = info.Destructible.Sound;
				}
			}
			gameObject.GetComponent<CopyMaterial>().visObjects = list.Select((GameObject p) => p.GetComponentInChildren<Renderer>()).ToArray();
			gameObject.GetComponent<LevelEntity>().children = list.Select((GameObject p) => p.GetComponent<NetworkBlock>()).ToArray();
			return gameObject.transform;
		}
	}
}
