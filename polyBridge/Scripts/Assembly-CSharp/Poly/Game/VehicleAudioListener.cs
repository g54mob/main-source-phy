using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DarkTonic.MasterAudio;
using Poly.Draw;
using Poly.Physics;
using UnityEngine;

namespace Poly.Game
{
	[RequireComponent(typeof(Poly.Physics.Vehicle))]
	public class VehicleAudioListener : TemplateForAudioListener
	{
		public enum VehicleAudioSize
		{
			small = 0,
			medium = 1,
			large = 2,
			huge = 3
		}

		private enum cooldownType
		{
			shake = 0,
			tire = 1,
			chassis = 2
		}

		public enum ImpactType
		{
			Unknown = 0,
			WithSoil = 1,
			WithConcrete = 2,
			WithMetal = 3
		}

		private static readonly Dictionary<ImpactType, string> tireImpactSoundsSmall = new Dictionary<ImpactType, string>
		{
			{
				ImpactType.WithConcrete,
				"sfx_vehicle_tire_impact_concrete_small"
			},
			{
				ImpactType.WithMetal,
				"sfx_vehicle_tire_impact_metal_small"
			},
			{
				ImpactType.WithSoil,
				"sfx_vehicle_tire_impact_soil_small"
			},
			{
				ImpactType.Unknown,
				"sfx_vehicle_tire_impact_soil_small"
			}
		};

		private static readonly Dictionary<ImpactType, string> tireImpactSoundsMedium = new Dictionary<ImpactType, string>
		{
			{
				ImpactType.WithConcrete,
				"sfx_vehicle_tire_impact_concrete_medium"
			},
			{
				ImpactType.WithMetal,
				"sfx_vehicle_tire_impact_metal_medium"
			},
			{
				ImpactType.WithSoil,
				"sfx_vehicle_tire_impact_soil_medium"
			},
			{
				ImpactType.Unknown,
				"sfx_vehicle_tire_impact_soil_medium"
			}
		};

		private static readonly Dictionary<ImpactType, string> tireImpactSoundsLarge = new Dictionary<ImpactType, string>
		{
			{
				ImpactType.WithConcrete,
				"sfx_vehicle_tire_impact_concrete_large"
			},
			{
				ImpactType.WithMetal,
				"sfx_vehicle_tire_impact_metal_large"
			},
			{
				ImpactType.WithSoil,
				"sfx_vehicle_tire_impact_soil_large"
			},
			{
				ImpactType.Unknown,
				"sfx_vehicle_tire_impact_soil_large"
			}
		};

		private static readonly Dictionary<ImpactType, string> chassisImpactSoundsSmall = new Dictionary<ImpactType, string>
		{
			{
				ImpactType.WithConcrete,
				"sfx_vehicle_body_impact_concrete_small"
			},
			{
				ImpactType.WithMetal,
				"sfx_vehicle_body_impact_metal_small"
			},
			{
				ImpactType.WithSoil,
				"sfx_vehicle_body_impact_soil_small"
			},
			{
				ImpactType.Unknown,
				"sfx_vehicle_body_impact_soil_small"
			}
		};

		private static readonly Dictionary<ImpactType, string> chassisImpactSoundsMedium = new Dictionary<ImpactType, string>
		{
			{
				ImpactType.WithConcrete,
				"sfx_vehicle_body_impact_concrete_medium"
			},
			{
				ImpactType.WithMetal,
				"sfx_vehicle_body_impact_metal_medium"
			},
			{
				ImpactType.WithSoil,
				"sfx_vehicle_body_impact_soil_medium"
			},
			{
				ImpactType.Unknown,
				"sfx_vehicle_body_impact_soil_medium"
			}
		};

		private static readonly Dictionary<ImpactType, string> chassisImpactSoundsLarge = new Dictionary<ImpactType, string>
		{
			{
				ImpactType.WithConcrete,
				"sfx_vehicle_body_impact_concrete_large"
			},
			{
				ImpactType.WithMetal,
				"sfx_vehicle_body_impact_metal_large"
			},
			{
				ImpactType.WithSoil,
				"sfx_vehicle_body_impact_soil_large"
			},
			{
				ImpactType.Unknown,
				"sfx_vehicle_body_impact_soil_large"
			}
		};

		private static readonly Dictionary<VehicleAudioSize, string> chassisShakeSounds = new Dictionary<VehicleAudioSize, string>
		{
			{
				VehicleAudioSize.small,
				"sfx_vehicle_shake_small"
			},
			{
				VehicleAudioSize.medium,
				"sfx_vehicle_shake_medium"
			},
			{
				VehicleAudioSize.large,
				"sfx_vehicle_shake_large"
			},
			{
				VehicleAudioSize.huge,
				"sfx_vehicle_shake_huge"
			}
		};

		public Poly.Physics.Rigidbody[] chassisParts;

		public Poly.Physics.Rigidbody[] wheels;

		public VehicleAudioSize size;

		public float maxLocalPosY_ToRejectChassisImpact = 0.1f;

		public float maxDotLocalUp_ToRejectChassisImpact = -0.707f;

		public bool ignoreWheelContactWhenRejectingChassisImpacts;

		[Range(0f, 180f)]
		public float maxChassisAbsAngle_ToRejectChassisImpact = 45f;

		public bool logImpactEvents;

		private int numContactsOnWheels;

		private int numMetalContactsOnWheels;

		private float minChassisDotUp_ToRejectChassisImpact;

		private List<Transform> chassisTransforms = new List<Transform>();

		private List<Transform> wheelTransforms = new List<Transform>();

		private const float shakeCooldown = 0.2f;

		private const float impactCooldown = 0.1f;

		private float shakeCooldownWatch;

		private float tireCooldownWatch;

		private float chassisCooldownWatch;

		public VehicleAudioListener()
		{
			impactVelocityThreshold = 1f;
		}

		private void OnEnable()
		{
			Poly.Physics.Rigidbody[] array = chassisParts;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].collisionListeners.Add(this);
			}
			array = wheels;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].collisionListeners.Add(this);
			}
			chassisTransforms.AddRange(chassisParts.Select((Poly.Physics.Rigidbody r) => r.transform));
			wheelTransforms.AddRange(wheels.Select((Poly.Physics.Rigidbody r) => r.transform));
			UpdateMinChassisDot();
		}

		private void OnDisable()
		{
			Poly.Physics.Rigidbody[] array = chassisParts;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].collisionListeners.Remove(this);
			}
			array = wheels;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].collisionListeners.Remove(this);
			}
			Clear();
		}

		private void OnValidate()
		{
			UpdateMinChassisDot();
		}

		private void UpdateMinChassisDot()
		{
			if (90f == maxChassisAbsAngle_ToRejectChassisImpact)
			{
				minChassisDotUp_ToRejectChassisImpact = 0f;
			}
			else if (180f == maxChassisAbsAngle_ToRejectChassisImpact)
			{
				minChassisDotUp_ToRejectChassisImpact = -1f;
			}
			else
			{
				minChassisDotUp_ToRejectChassisImpact = Mathf.Cos(maxChassisAbsAngle_ToRejectChassisImpact * (MathF.PI / 180f));
			}
		}

		public override bool OnImpact(ref ContactData data, int pointIdx, in ContactPointInfo point)
		{
			bool flag = chassisTransforms.Contains(data.receivingObject);
			ImpactType impactType = CalculateImpactType(ref data, flag);
			if (flag)
			{
				float num = Vec2.Dot((Vec2)data.receivingObject.up, (Vec2)Vector2.up);
				float num2 = Vec2.Dot((Vec2)data.receivingObject.up, point.normal * data.normalSign);
				Vec2 position = point.position;
				if (data.normalSign == -1f)
				{
					position += point.normal * point.distance;
				}
				float num3 = Vec2.Dot((Vec2)data.receivingObject.up, position - (Vec2)data.receivingObject.position);
				if ((ignoreWheelContactWhenRejectingChassisImpacts || (0 < numContactsOnWheels && impactType != ImpactType.WithMetal) || 0 < numMetalContactsOnWheels) && num2 <= maxDotLocalUp_ToRejectChassisImpact && num3 <= maxLocalPosY_ToRejectChassisImpact && minChassisDotUp_ToRejectChassisImpact <= num)
				{
					if (logImpactEvents)
					{
						Debug.Log("Rejecting: " + data.receivingObject.name);
					}
					if (logImpactEvents)
					{
						GlDrawer.color = Color.red;
						GlDrawer.DrawArrow(point.position, point.normal, Color.red);
					}
					return false;
				}
				if (logImpactEvents)
				{
					Debug.Log($"{data.receivingObject.name}, type: {impactType}, velocity: {point.relativePointVelocityBeforeCollision.magnitude}, outputVol: {GetCollisionSoundVolume(size, point.relativePointVelocityBeforeCollision.magnitude)}");
				}
				if (chassisCooldownWatch == 0f)
				{
					MasterAudio.PlaySound3DAtVector3AndForget(GetChassisImpactSound(size, impactType), point.position, GetCollisionSoundVolume(size, point.relativePointVelocityBeforeCollision.magnitude));
					StartCoroutine(StartCooldown(cooldownType.chassis, 0.2f));
				}
				if (shakeCooldownWatch == 0f)
				{
					MasterAudio.PlaySound3DAtTransformAndForget(chassisShakeSounds[size], chassisTransforms[0], GetCollisionSoundVolume(size, point.relativePointVelocityBeforeCollision.magnitude));
					StartCoroutine(StartCooldown(cooldownType.shake, 0.2f));
				}
				return base.OnImpact(ref data, pointIdx, in point);
			}
			if (logImpactEvents)
			{
				Debug.Log($"{data.receivingObject.name}, type: {impactType}, velocity: {point.relativePointVelocityBeforeCollision.magnitude}, outputVol: {GetCollisionSoundVolume(size, point.relativePointVelocityBeforeCollision.magnitude)}");
			}
			if (tireCooldownWatch == 0f)
			{
				MasterAudio.PlaySound3DAtVector3AndForget(GetTireImpactSound(size, impactType), point.position, GetCollisionSoundVolume(size, point.relativePointVelocityBeforeCollision.magnitude));
				StartCoroutine(StartCooldown(cooldownType.tire, 0.2f));
			}
			if (shakeCooldownWatch == 0f)
			{
				MasterAudio.PlaySound3DAtTransformAndForget(chassisShakeSounds[size], chassisTransforms[0], GetCollisionSoundVolume(size, point.relativePointVelocityBeforeCollision.magnitude));
				StartCoroutine(StartCooldown(cooldownType.shake, 0.2f));
			}
			return base.OnImpact(ref data, pointIdx, in point);
		}

		public override void OnTouchingPointEnter(ref ContactData data, int pointIdx, in ContactPointInfo point)
		{
			if (wheelTransforms.Contains(data.receivingObject))
			{
				numContactsOnWheels++;
				if (IsOtherObjectChassis(ref data))
				{
					numMetalContactsOnWheels++;
				}
			}
		}

		public override bool OnTouchingPointStay(ref ContactData data, int pointIdx, in ContactPointInfo point)
		{
			return false;
		}

		public override void OnTouchingPointExit(ref ContactData data, int pointIdx)
		{
			if (wheelTransforms.Contains(data.receivingObject))
			{
				numContactsOnWheels--;
				if (IsOtherObjectChassis(ref data))
				{
					numMetalContactsOnWheels--;
				}
			}
		}

		protected override void Clear()
		{
			chassisTransforms.Clear();
			wheelTransforms.Clear();
			numContactsOnWheels = 0;
			numMetalContactsOnWheels = 0;
			base.Clear();
		}

		private ImpactType CalculateImpactType(ref ContactData data, bool isReceivingObjectChassis)
		{
			ImpactType impactType = ImpactType.Unknown;
			if (data.otherLayer == Layer.Terrain || data.otherLayer == Layer.Rock || data.otherLayer == Layer.Balloon)
			{
				impactType = ImpactType.WithSoil;
			}
			else if (data.otherLayer == Layer.PlatformSurface || data.otherLayer == Layer.PlatformBase_unused || data.otherLayer == Layer.CustomShape || data.otherLayer == Layer.RoadEdge || data.otherLayer == Layer.RoadEdgeConnectedToSplitNode || (22 <= (int)data.otherLayer && (int)data.otherLayer <= 52))
			{
				impactType = ImpactType.WithConcrete;
			}
			else if (data.otherLayer == Layer.Vehicle)
			{
				VehicleSyncSource component = data.otherObject.GetComponent<VehicleSyncSource>();
				impactType = ((!component || (component.m_VehicleSyncPart != VehicleSyncPart.CHASSIS && component.m_VehicleSyncPart != VehicleSyncPart.CHASSIS_TRAILER)) ? ImpactType.WithSoil : ImpactType.WithMetal);
			}
			if (impactType == ImpactType.Unknown)
			{
				impactType = ImpactType.WithSoil;
			}
			return impactType;
		}

		private bool IsOtherObjectChassis(ref ContactData data)
		{
			bool result = false;
			if (data.otherLayer == Layer.Vehicle)
			{
				VehicleSyncSource component = data.otherObject.GetComponent<VehicleSyncSource>();
				result = (bool)component && (component.m_VehicleSyncPart == VehicleSyncPart.CHASSIS || component.m_VehicleSyncPart == VehicleSyncPart.CHASSIS_TRAILER);
			}
			return result;
		}

		private string GetTireImpactSound(VehicleAudioSize size, ImpactType surface)
		{
			return size switch
			{
				VehicleAudioSize.small => tireImpactSoundsSmall[surface], 
				VehicleAudioSize.medium => tireImpactSoundsMedium[surface], 
				VehicleAudioSize.large => tireImpactSoundsLarge[surface], 
				VehicleAudioSize.huge => tireImpactSoundsLarge[surface], 
				_ => string.Empty, 
			};
		}

		private string GetChassisImpactSound(VehicleAudioSize size, ImpactType surface)
		{
			return size switch
			{
				VehicleAudioSize.small => chassisImpactSoundsSmall[surface], 
				VehicleAudioSize.medium => chassisImpactSoundsMedium[surface], 
				VehicleAudioSize.large => chassisImpactSoundsLarge[surface], 
				VehicleAudioSize.huge => chassisImpactSoundsLarge[surface], 
				_ => string.Empty, 
			};
		}

		private float GetCollisionSoundVolume(VehicleAudioSize size, float velocity)
		{
			return size switch
			{
				VehicleAudioSize.small => Mathf.Clamp01(velocity / 10f), 
				VehicleAudioSize.medium => Mathf.Clamp01(velocity / 10f), 
				VehicleAudioSize.large => Mathf.Clamp01(velocity / 10f), 
				VehicleAudioSize.huge => Mathf.Clamp01(velocity / 5f), 
				_ => 0f, 
			};
		}

		private IEnumerator StartCooldown(cooldownType type, float duration)
		{
			switch (type)
			{
			case cooldownType.shake:
			{
				for (int i = 0; i < 1000; i++)
				{
					if (!(shakeCooldownWatch < duration))
					{
						break;
					}
					shakeCooldownWatch += Time.unscaledDeltaTime;
					yield return null;
				}
				shakeCooldownWatch = 0f;
				break;
			}
			case cooldownType.chassis:
			{
				for (int i = 0; i < 1000; i++)
				{
					if (!(chassisCooldownWatch < duration))
					{
						break;
					}
					chassisCooldownWatch += Time.unscaledDeltaTime;
					yield return null;
				}
				chassisCooldownWatch = 0f;
				break;
			}
			case cooldownType.tire:
			{
				for (int i = 0; i < 1000; i++)
				{
					if (!(tireCooldownWatch < duration))
					{
						break;
					}
					tireCooldownWatch += Time.unscaledDeltaTime;
					yield return null;
				}
				tireCooldownWatch = 0f;
				break;
			}
			}
		}
	}
}
