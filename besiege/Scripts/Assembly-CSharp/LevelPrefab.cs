using System;
using BesiegeDlc;
using UnityEngine;

[AddComponentMenu("LevelEditor/LevelPrefab")]
public class LevelPrefab : MonoBehaviour
{
	[Serializable]
	public class ThumbnailSettings
	{
		public Vector3 offset = Vector3.zero;

		public Vector3 euler = Vector3.zero;

		public Vector3 scale = Vector3.one;

		public Vector3 shadowOffset = Vector3.zero;

		public Vector3 shadowScale = Vector3.one;
	}

	[Serializable]
	public class MirrorAxisAdditions
	{
		public Vector3 x = Vector3.zero;

		public Vector3 y = Vector3.zero;

		public Vector3 z = Vector3.zero;
	}

	public int ID;

	public int LocalisationID = 2215;

	[EnumMask]
	[SerializeField]
	private DlcManager.DlcType dlcType;

	public Texture2D icon;

	public Texture2D tencentIcon;

	public bool hidden;

	public ThumbnailSettings iconSettings;

	public StatMaster.Category category;

	public string[] keywords;

	public bool inflammable;

	public bool destructable;

	public bool damager;

	public bool ignorePhysics;

	public bool stayKinematic;

	public bool placeOnWater = true;

	public TriggerType[] events;

	public int[] moddedEvents;

	public bool canScale = true;

	public bool uniformScale;

	public bool canPick = true;

	public bool ignoreInPlaceMode;

	public bool showPhysicsToggle = true;

	public bool playFireWhenStatic;

	public bool PlayFireWhenBuilding;

	public Material staticMaterial;

	public bool hasStaticMaterial;

	public bool batchWhenStatic = true;

	public bool ignoreDecal;

	public bool ignoreOutline;

	public bool hasCustomGhost;

	public bool applyGhostMaterial = true;

	public GameObject ghostPrefab;

	public static long INVALID_ID = long.MinValue;

	public static long UNASSIGNED_ID;

	public float groundOffset;

	public Vector3 offset;

	public Vector3 placementScale = Vector3.one;

	public Vector3 rotation;

	public Vector3 additiveMirrorAxis = Vector3.zero;

	public MirrorAxisAdditions additiveMirrorValues;

	public bool swapScaleOnMirror;

	public bool ignoreYSwap;

	public bool hasBoundingBox = true;

	public Vector3 boundObjPos = Vector3.zero;

	public Vector3 boundObjSize = Vector3.one;
}
