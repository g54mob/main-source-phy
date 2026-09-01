using System.Collections.Generic;
using UnityEngine;

namespace Landfall.TABS
{
	public class UnitRig : MonoBehaviour
	{
		public class BoneInfo
		{
			public Vector3 m_localPosition;

			public Quaternion m_localRotation;

			public Vector3 m_localScale;
		}

		public enum ItemType
		{
			GEAR = 0,
			WEAPON = 1
		}

		public enum GearType
		{
			HEAD = 0,
			NECK = 1,
			SHOULDER = 2,
			TORSO = 3,
			ARMS = 4,
			WRISTS = 5,
			HANDS = 6,
			WAIST = 7,
			LEGS = 8,
			FEET = 9,
			WEAPON = 10,
			CUSTOM = 11
		}

		public enum EquipType
		{
			BOTH = 0,
			LEFT = 1,
			RIGHT = 2
		}

		public Transform m_head;

		public Transform m_neck;

		public Transform m_shoulderLeft;

		public Transform m_shoulderRight;

		public Transform m_armLeft;

		public Transform m_armRight;

		public Transform m_wristLeft;

		public Transform m_wristRight;

		public Transform m_torso;

		public Transform m_waist;

		public Transform m_legLeft;

		public Transform m_legRight;

		public Transform m_footLeft;

		public Transform m_footRight;

		private List<CharacterItem> m_items = new List<CharacterItem>();

		private EyeSpawner m_eyeSpawner;

		public int m_numHideEyes;

		public Dictionary<string, BoneInfo> RigInfo
		{
			get
			{
				Dictionary<string, BoneInfo> dictionary = new Dictionary<string, BoneInfo>();
				List<string> boneNames = Stitcher.TransformCatalog.GetBoneNames(Stitcher.TransformCatalog.RigType.Human);
				for (int i = 0; i < boneNames.Count; i++)
				{
					Transform transform = base.transform.FindChildRecursive("M_" + boneNames[i]);
					if (!(transform == null))
					{
						transform = transform.parent;
						BoneInfo boneInfo = new BoneInfo();
						boneInfo.m_localPosition = transform.localPosition;
						boneInfo.m_localRotation = transform.localRotation;
						boneInfo.m_localScale = transform.localScale;
						dictionary.Add(boneNames[i], boneInfo);
					}
				}
				return dictionary;
			}
			set
			{
				List<string> boneNames = Stitcher.TransformCatalog.GetBoneNames(Stitcher.TransformCatalog.RigType.Human);
				for (int i = 0; i < boneNames.Count; i++)
				{
					Transform transform = base.transform.FindChildRecursive("M_" + boneNames[i]);
					if (!(transform == null))
					{
						transform = transform.parent;
						BoneInfo boneInfo = value[boneNames[i]];
						if (boneInfo != null)
						{
							transform.localPosition = boneInfo.m_localPosition;
							transform.localRotation = boneInfo.m_localRotation;
							transform.localScale = boneInfo.m_localScale;
						}
					}
				}
			}
		}

		public Transform[] boneTransforms { get; private set; }

		private void Awake()
		{
			SetTransformBones();
			m_eyeSpawner = GetComponentInChildren<EyeSpawner>();
		}

		private void SetTransformBones()
		{
			List<Transform> list = new List<Transform>();
			list.Add(m_head);
			list.Add(m_neck);
			list.Add(m_shoulderLeft);
			list.Add(m_shoulderRight);
			list.Add(m_armLeft);
			list.Add(m_armRight);
			list.Add(m_wristLeft);
			list.Add(m_wristRight);
			list.Add(m_torso);
			list.Add(m_waist);
			list.Add(m_legLeft);
			list.Add(m_legRight);
			list.Add(m_footLeft);
			list.Add(m_footRight);
			boneTransforms = list.ToArray();
		}

		public void SpawnProps(GameObject[] props, PropItemData[] propData, Stitcher.TransformCatalog.RigType rigType, Team team)
		{
			Stitcher.TransformCatalog catalog = new Stitcher.TransformCatalog(base.gameObject, rigType, "M_");
			for (int i = 0; i < props.Length; i++)
			{
				if (!(props[i] == null))
				{
					PropItemData propData2 = ((i < propData.Length && propData[i] != null) ? propData[i] : new PropItemData());
					SpawnProp(props[i].GetComponent<CharacterItem>(), propData2, rigType, team, catalog);
				}
			}
		}

		public GameObject SpawnProp(CharacterItem prop, PropItemData propData, Stitcher.TransformCatalog.RigType rigType, Team team, Stitcher.TransformCatalog catalog = null, bool isUnitEditor = false)
		{
			if (prop == null)
			{
				return null;
			}
			if (catalog == null)
			{
				catalog = new Stitcher.TransformCatalog(base.gameObject, rigType, "M_");
			}
			CharacterItem characterItem = Object.Instantiate(prop);
			characterItem.Equip(base.gameObject, propData, catalog, team, isUnitEditor);
			m_items.Add(characterItem);
			if ((prop as PropItem).disableGooglyEyesOfParent)
			{
				m_numHideEyes++;
				characterItem.EOnSetVisibility += delegate(bool visible)
				{
					m_numHideEyes += (visible ? 1 : (-1));
					UpdateHideEyes();
				};
				characterItem.EOnRemove += delegate
				{
					m_numHideEyes--;
					UpdateHideEyes();
				};
				UpdateHideEyes();
			}
			return characterItem.gameObject;
		}

		private void UpdateHideEyes()
		{
			if (!(m_eyeSpawner == null))
			{
				m_eyeSpawner.SetEyesActive(m_numHideEyes == 0);
			}
		}

		public void SpawnWeapon(GameObject weapon)
		{
		}

		public void SetGearVisability(GearType gearType, bool visability)
		{
			for (int i = 0; i < m_items.Count; i++)
			{
				if (m_items[i].GearT == gearType)
				{
					m_items[i].SetVisibility(visability);
				}
			}
		}

		public void GetBonesForEquip(GearType gearType, out Transform bone1, out Transform bone2)
		{
			bone1 = null;
			bone2 = null;
			switch (gearType)
			{
			case GearType.HEAD:
				bone1 = m_head;
				break;
			case GearType.NECK:
				bone1 = m_neck;
				break;
			case GearType.SHOULDER:
				bone1 = m_shoulderLeft;
				bone2 = m_shoulderRight;
				break;
			case GearType.ARMS:
				bone1 = m_armLeft;
				bone2 = m_armRight;
				break;
			case GearType.TORSO:
				bone1 = m_torso;
				break;
			case GearType.WRISTS:
				bone1 = m_wristLeft;
				bone2 = m_wristRight;
				break;
			case GearType.WAIST:
				bone1 = m_waist;
				break;
			case GearType.LEGS:
				bone1 = m_legLeft;
				bone2 = m_legRight;
				break;
			case GearType.FEET:
				bone1 = m_footLeft;
				bone2 = m_footRight;
				break;
			case GearType.HANDS:
				break;
			}
		}
	}
}
