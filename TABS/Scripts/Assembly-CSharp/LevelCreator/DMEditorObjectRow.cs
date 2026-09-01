using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelCreator
{
	[Serializable]
	public class DMEditorObjectRow : DataTableRow
	{
		[SerializeField]
		public string ObjectName;

		[SerializeField]
		public GameObject EditorObject;

		[SerializeField]
		public GameObject GameObject;

		[SerializeField]
		public Sprite Thumbnail;

		[SerializeField]
		public float defaultSlopeAngle = 0.1f;

		[SerializeField]
		public Vector3 InitialRotation;

		[SerializeField]
		public Vector3 PivotOffset;

		[SerializeField]
		[FormerlySerializedAs("ScaleMultiplier")]
		public float InitialScale = 1f;

		[SerializeField]
		public bool CanSimulatePhysics = true;

		[SerializeField]
		public float NormalizedSize;

		[SerializeField]
		public bool IsEffect;

		[HideInInspector]
		public string Key;

		[Space]
		[SerializeField]
		public RadialMenu.RadialThemes RadialMenuTheme;

		[SerializeField]
		public string RadialMenuGroup;

		[SerializeField]
		public string RadialMenuSlotName;

		public string RadialMenuPath => RadialMenuTheme.ToString() + "/" + ((RadialMenuGroup != "") ? RadialMenuGroup : "None") + "/" + ObjectName;

		public string GetRowName()
		{
			return ObjectName;
		}

		public string GetLocalizedRowName()
		{
			return "LC_ITEMGRID_" + ObjectName.ToUpper();
		}
	}
}
