using System;
using UnityEngine;

namespace Landfall.TABS
{
	[Serializable]
	public class SerializedUnitBlueprint
	{
		public string m_name;

		public string m_icon;

		public DatabaseID m_id;

		public DatabaseID m_unitBase;

		public bool m_holdingWithTwoHands;

		public DatabaseID m_rightWeapon;

		public PropItemData m_rightWeaponData;

		public DatabaseID m_leftWeapon;

		public PropItemData m_leftWeaponData;

		[Obsolete("Now called objectsToSpawnAsChildren in usages")]
		public DatabaseID[] m_combatMoves;

		public bool m_hasRider;

		public DatabaseID m_rider;

		public DatabaseID m_faction;

		public float m_health;

		public float m_movementSpeedMuiltiplier = 1f;

		public float m_animationMultiplier = 1f;

		public float m_stepMultiplier = 1f;

		public float m_armLimitMultiplier = 1f;

		public float m_targetingPriorityMultiplier = 1f;

		public float m_weaponSeparation = 0.5f;

		public AnimationCurve m_attackDistanceCurve;

		public float m_massMultiplier = 1f;

		public float m_sizeMultiplier = 1f;

		public DatabaseID m_fistRef;

		public DatabaseID[] m_props;

		public PropItemData[] m_propData;

		public DatabaseID m_turningData;

		public float m_turnSpeed = 18f;

		public AnimationCurve m_turnSpeedCurve;

		public string[] m_movementTypes;

		public string m_targetingType;

		public int customCost = -1;

		public bool useCustomCost;

		public string unitDescription;

		public DatabaseID voiceBundle;

		public float voicePitch = 1f;

		public float voiceVolume = 1f;

		public DatabaseID rightProjectile;

		public DatabaseID leftProjectile;

		public float attackSpeedMultiplier;

		public float damageMultiplier;
	}
}
