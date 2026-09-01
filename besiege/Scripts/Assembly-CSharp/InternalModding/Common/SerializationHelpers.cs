using System.Collections.Generic;
using InternalModding.Blocks;
using InternalModding.Misc;
using Modding.Serialization;
using UnityEngine;

namespace InternalModding.Common
{
	public static class SerializationHelpers
	{
		public static void SetOnObject(this FireInteraction interaction, GameObject tagObject, Transform controllerObject, Transform particlesObject, BasicInfo basicInfo, bool debugVisuals)
		{
			if (interaction != null)
			{
				if (controllerObject == null || particlesObject == null)
				{
					MLog.Warn("Cannot add FireInteraction during a reload, restart the game!");
					return;
				}
				FireController component = controllerObject.GetComponent<FireController>();
				component.lateBurnDuration = interaction.BurnDuration;
				component.basicInfo = basicInfo;
				component.HasBasicInfo = true;
				controllerObject.gameObject.SetActive(true);
				particlesObject.gameObject.SetActive(true);
				FireTag component2 = tagObject.GetComponent<FireTag>();
				component2.enabled = true;
				component2.igniteOnStart = interaction.IgniteOnStart;
				component2.fireControllerCode = component;
				component2.hasController = true;
				ParticleSystem.EmissionModule emission = particlesObject.GetComponent<ParticleSystem>().emission;
				emission.enabled = !interaction.DisableParticles;
				interaction.ParticleTransform.SetOnTransform(particlesObject);
				List<Transform> list = new List<Transform>();
				foreach (Transform item in controllerObject)
				{
					list.Add(item);
				}
				foreach (Transform item2 in list)
				{
					Object.DestroyImmediate(item2.gameObject);
				}
				component.overlapCenter = interaction.Trigger.Position;
				if (interaction.Trigger is SphereModCollider)
				{
					SphereModCollider sphereModCollider = interaction.Trigger as SphereModCollider;
					component.overlapRadius = sphereModCollider.Radius;
					component.overlapType = FireController.OverlapType.Sphere;
				}
				else if (interaction.Trigger is BoxModCollider)
				{
					BoxModCollider boxModCollider = interaction.Trigger as BoxModCollider;
					component.overlapType = FireController.OverlapType.Box;
					component.overlapSize = boxModCollider.Scale;
				}
				else
				{
					Collider collider = interaction.Trigger.CreateCollider(controllerObject.transform);
					Debug.LogError("Unsupported fire collider type: " + collider);
					Object.Destroy(collider);
				}
				if (debugVisuals)
				{
					Transform transform = interaction.Trigger.CreateVisual(controllerObject.transform);
					transform.gameObject.layer = 25;
					Renderer[] componentsInChildren = transform.GetComponentsInChildren<Renderer>();
					Renderer[] array = componentsInChildren;
					foreach (Renderer renderer in array)
					{
						renderer.sharedMaterial = SingleInstanceFindOnly<BlockLoader>.Instance.FireTriggerVisualMaterial;
					}
				}
			}
			else
			{
				if (particlesObject != null)
				{
					particlesObject.gameObject.SetActive(false);
				}
				if (controllerObject != null)
				{
					Object.Destroy(controllerObject.gameObject);
				}
				Object.Destroy(tagObject.GetComponent<FireTag>());
				BasicInfo.BasicInfoType infoType = basicInfo.infoType;
				if (infoType == BasicInfo.BasicInfoType.Entity)
				{
					LevelEntity component3 = tagObject.GetComponent<LevelEntity>();
					component3.hasFireController = false;
				}
			}
		}
	}
}
