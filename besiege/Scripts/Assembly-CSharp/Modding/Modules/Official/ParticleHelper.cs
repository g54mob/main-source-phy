using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using InternalModding.Blocks;
using Modding.Serialization;
using UnityEngine;

namespace Modding.Modules.Official
{
	public static class ParticleHelper
	{
		[Serializable]
		public class CollisionSettings : Element
		{
			[XmlElement]
			[DefaultValue(0f)]
			public float Dampen = 0.25f;

			[XmlElement]
			[DefaultValue(0f)]
			public float Bounce = 0.6f;

			[XmlElement]
			[DefaultValue(0f)]
			public float LifetimeLoss = 0.01f;

			[DefaultValue(0f)]
			[XmlElement]
			public float MinKillSpeed = 0.01f;

			[DefaultValue(0f)]
			[XmlElement]
			public float MaxKillSpeed = 10000f;

			[XmlElement]
			[DefaultValue(0f)]
			public float RadiusScale = 0.01f;
		}

		[Serializable]
		public class ParticleDefinition : Element
		{
			[XmlElement]
			public Modding.Serialization.Vector3 StartPosition;

			[XmlElement]
			public Modding.Serialization.Vector3 Direction;

			[XmlElement]
			public float StartSpeed;

			[XmlElement]
			[DefaultValue(1f)]
			public float LifetimeMultiplier = 1f;

			[XmlElement]
			[RequireToValidate]
			[DefaultValue(null)]
			public BoxModCollider FireTrigger;

			[XmlElement]
			[RequireToValidate]
			[DefaultValue(null)]
			public Element DousesFire;

			[XmlElement]
			[RequireToValidate]
			[DefaultValue(null)]
			public Element StartsFire;

			[XmlElement]
			[DefaultValue(null)]
			[RequireToValidate]
			public CollisionSettings Collisions;

			[XmlElement]
			[DefaultValue(0f)]
			public float AddForce;

			protected override bool Validate(string elementName)
			{
				if (!base.Validate(elementName))
				{
					return false;
				}
				if ((FireTrigger != null || StartsFire != null) && DousesFire != null)
				{
					return InvalidData(elementName, "Cannot specify both FireTrigger and DousesFire!");
				}
				if (DousesFire != null && Collisions == null)
				{
					return InvalidData(elementName, "If DousesFire is specified, particle collisions must be enabled.");
				}
				if (StartsFire != null && Collisions == null)
				{
					return InvalidData(elementName, "If StartsFire is specified, particle collisions must be enabled.");
				}
				if (AddForce != 0f && Collisions == null)
				{
					return InvalidData(elementName, "If AddForce is specified, particle collisions must be enabled.");
				}
				return true;
			}
		}

		[Serializable]
		public class FireParticles : ParticleDefinition
		{
		}

		[Serializable]
		public class WaterParticles : ParticleDefinition
		{
		}

		[Serializable]
		public class SteamParticles : ParticleDefinition
		{
		}

		[Serializable]
		public class CustomParticles : ParticleDefinition
		{
			[XmlElement]
			[RequireToValidate]
			public ResourceReference Texture;
		}

		public class ParticleSystemsInformation
		{
			public Dictionary<ParticleDefinition, ParticleSystem[]> particles;

			public Dictionary<ParticleDefinition, Transform> fireTriggers;

			public Dictionary<ParticleDefinition, Transform> fireVisuals;
		}

		public static ParticleSystemsInformation CreateParticleSystems<T>(Transform parent, BlockModuleBehaviour<T> behaviour, ParticleDefinition[] particleSystems) where T : BlockModule
		{
			ParticleSystemsInformation particleSystemsInformation = new ParticleSystemsInformation();
			particleSystemsInformation.particles = new Dictionary<ParticleDefinition, ParticleSystem[]>();
			particleSystemsInformation.fireTriggers = new Dictionary<ParticleDefinition, Transform>();
			particleSystemsInformation.fireVisuals = new Dictionary<ParticleDefinition, Transform>();
			ParticleSystemsInformation particleSystemsInformation2 = particleSystemsInformation;
			foreach (ParticleDefinition def in particleSystems)
			{
				CreateParticleSystem(particleSystemsInformation2, parent, behaviour, def);
			}
			return particleSystemsInformation2;
		}

		public static ParticleSystemsInformation OnReloadModule<T>(Transform parent, BlockModuleBehaviour<T> behaviour, ParticleDefinition[] particleSystems, ParticleSystemsInformation info) where T : BlockModule
		{
			foreach (ParticleSystem[] value in info.particles.Values)
			{
				ParticleSystem[] array = value;
				foreach (ParticleSystem particleSystem in array)
				{
					if (particleSystem != null && particleSystem.gameObject != null)
					{
						particleSystem.Stop();
						UnityEngine.Object.DestroyImmediate(particleSystem.gameObject);
					}
				}
			}
			foreach (Transform value2 in info.fireTriggers.Values)
			{
				if (value2 != null && value2.gameObject != null)
				{
					UnityEngine.Object.DestroyImmediate(value2.gameObject);
				}
			}
			foreach (Transform value3 in info.fireVisuals.Values)
			{
				UnityEngine.Object.DestroyImmediate(value3.gameObject);
			}
			info = CreateParticleSystems(parent, behaviour, particleSystems);
			return info;
		}

		private static void CreateParticleSystem<T>(ParticleSystemsInformation info, Transform parent, BlockModuleBehaviour<T> behaviour, ParticleDefinition def) where T : BlockModule
		{
			GameObject original;
			if (def is FireParticles || def is CustomParticles)
			{
				original = SingleInstanceFindOnly<BlockLoader>.Instance.ModulePrefabs.SpewingFireParticles;
			}
			else if (def is WaterParticles)
			{
				original = SingleInstanceFindOnly<BlockLoader>.Instance.ModulePrefabs.SpewingWaterParticles;
			}
			else
			{
				if (!(def is SteamParticles))
				{
					throw new InvalidDataException("Unknown particle type!");
				}
				original = SingleInstanceFindOnly<BlockLoader>.Instance.ModulePrefabs.SpewingSteamParticles;
			}
			Transform transform = UnityEngine.Object.Instantiate(original).transform;
			transform.name = "Spewing Particles: " + def.GetType().Name;
			transform.localPosition = def.StartPosition;
			transform.localRotation = Quaternion.FromToRotation(UnityEngine.Vector3.forward, def.Direction);
			transform.SetParent(parent, false);
			if (def is CustomParticles)
			{
				transform.GetComponent<ParticleSystemRenderer>().material.mainTexture = (Texture2D)(ModTexture)behaviour.GetResource(((CustomParticles)def).Texture);
			}
			ParticleSystem[] componentsInChildren = transform.GetComponentsInChildren<ParticleSystem>();
			ParticleSystem particleSystem = null;
			particleSystem = componentsInChildren.FirstOrDefault((ParticleSystem p) => p.gameObject.name.Contains("Collider"));
			if (particleSystem == null)
			{
				particleSystem = componentsInChildren[0];
			}
			ParticleSystem[] array = componentsInChildren;
			foreach (ParticleSystem particleSystem2 in array)
			{
				particleSystem2.startLifetime = def.LifetimeMultiplier;
				particleSystem2.Stop();
				particleSystem2.randomSeed = (uint)UnityEngine.Random.Range(0f, 999999f);
			}
			if (def.Collisions != null)
			{
				ApplyCollisions(def.Collisions, particleSystem);
			}
			info.particles.Add(def, componentsInChildren);
			if (def.FireTrigger != null || def.StartsFire != null)
			{
				Transform transform2 = UnityEngine.Object.Instantiate(SingleInstanceFindOnly<BlockLoader>.Instance.ModulePrefabs.SpewingFireTrigger).transform;
				transform2.name = "Spewing FireTrigger: " + def.GetType().Name;
				transform2.parent = parent;
				if (def.FireTrigger != null)
				{
					transform2.localPosition = def.FireTrigger.Position;
					transform2.localRotation = Quaternion.Euler(def.FireTrigger.Rotation);
					transform2.localScale = def.FireTrigger.Scale;
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(transform2.GetComponent<BoxCollider>());
				}
				FireController component = transform2.GetComponent<FireController>();
				component.basicInfo = behaviour.handler;
				component.HasBasicInfo = true;
				if (def.StartsFire != null)
				{
					particleSystem.gameObject.AddComponent<SpewingModuleFireParticleTrigger>().Controller = component;
				}
				if (def.FireTrigger != null && behaviour.ShowDebugVisuals)
				{
					Transform transform3 = def.FireTrigger.CreateVisual(parent);
					transform3.GetComponent<Renderer>().sharedMaterial = SingleInstanceFindOnly<BlockLoader>.Instance.FireTriggerVisualMaterial;
					info.fireVisuals.Add(def, transform3);
				}
				info.fireTriggers.Add(def, transform2);
			}
			if (def.DousesFire != null || def.AddForce != 0f)
			{
				ParticleAddForce particleAddForce = particleSystem.GetComponentInChildren<ParticleAddForce>();
				if (!particleAddForce)
				{
					particleAddForce = particleSystem.gameObject.AddComponent<ParticleAddForce>();
				}
				particleAddForce.canDouse = def.DousesFire != null;
				particleAddForce.particleForce = def.AddForce;
			}
		}

		private static void ApplyCollisions(CollisionSettings settings, ParticleSystem system)
		{
			ParticleSystem.CollisionModule collision = system.collision;
			collision.dampen = settings.Dampen;
			collision.bounce = settings.Bounce;
			collision.lifetimeLoss = settings.LifetimeLoss;
			collision.minKillSpeed = settings.MinKillSpeed;
			collision.maxKillSpeed = settings.MaxKillSpeed;
			collision.radiusScale = settings.RadiusScale;
			collision.sendCollisionMessages = true;
			collision.enableDynamicColliders = true;
			collision.enabled = true;
		}

		public static void SetParticleRange<T>(ParticleSystemsInformation info, BlockModuleBehaviour<T> behaviour, float value) where T : BlockModule
		{
			foreach (KeyValuePair<ParticleDefinition, ParticleSystem[]> particle in info.particles)
			{
				ParticleSystem[] value2 = particle.Value;
				foreach (ParticleSystem particleSystem in value2)
				{
					particleSystem.startSpeed = particle.Key.StartSpeed * value + 0.5f;
					if (value > 1f)
					{
						ParticleSystem.EmissionModule emission = particleSystem.emission;
						emission.rate = emission.rate.constant * 1.125f;
					}
				}
			}
			if (behaviour.IsStripped)
			{
				return;
			}
			foreach (KeyValuePair<ParticleDefinition, Transform> fireTrigger in info.fireTriggers)
			{
				if (fireTrigger.Key.FireTrigger != null)
				{
					fireTrigger.Value.localScale = fireTrigger.Key.FireTrigger.Scale + value * (UnityEngine.Vector3)fireTrigger.Key.Direction;
					fireTrigger.Value.localPosition = fireTrigger.Key.FireTrigger.Position + value * (UnityEngine.Vector3)fireTrigger.Key.Direction / 2f;
				}
			}
			foreach (KeyValuePair<ParticleDefinition, Transform> fireVisual in info.fireVisuals)
			{
				fireVisual.Value.localScale = fireVisual.Key.FireTrigger.Scale + value * (UnityEngine.Vector3)fireVisual.Key.Direction;
				fireVisual.Value.localPosition = fireVisual.Key.FireTrigger.Position + value * (UnityEngine.Vector3)fireVisual.Key.Direction / 2f;
			}
		}

		public static void ParticlesOn(ParticleSystemsInformation info, bool SimPhysics)
		{
			if (SimPhysics)
			{
				foreach (Transform value in info.fireTriggers.Values)
				{
					value.gameObject.SetActive(true);
				}
			}
			if (StatMaster.isHeadless)
			{
				return;
			}
			foreach (ParticleSystem[] value2 in info.particles.Values)
			{
				ParticleSystem[] array = value2;
				foreach (ParticleSystem particleSystem in array)
				{
					particleSystem.Play();
				}
			}
		}

		public static void ParticlesOff(ParticleSystemsInformation info, bool SimPhysics)
		{
			if (SimPhysics)
			{
				foreach (Transform value in info.fireTriggers.Values)
				{
					value.gameObject.SetActive(false);
				}
			}
			if (StatMaster.isHeadless)
			{
				return;
			}
			foreach (ParticleSystem[] value2 in info.particles.Values)
			{
				ParticleSystem[] array = value2;
				foreach (ParticleSystem particleSystem in array)
				{
					particleSystem.Stop();
				}
			}
		}
	}
}
