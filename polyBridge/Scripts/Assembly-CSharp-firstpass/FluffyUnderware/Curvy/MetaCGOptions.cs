using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy
{
	[HelpURL("https://curvyeditor.com/doclink/metacgoptions")]
	public class MetaCGOptions : CurvyMetadataBase
	{
		[Positive]
		[SerializeField]
		private int m_MaterialID;

		[SerializeField]
		private bool m_HardEdge;

		[Positive(Tooltip = "Max step distance when using optimization")]
		[SerializeField]
		private float m_MaxStepDistance;

		[Section("Extended UV", true, false, 100, HelpURL = "https://curvyeditor.com/doclink/metacgoptions_extendeduv")]
		[FieldCondition("showUVEdge", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		private bool m_UVEdge;

		[Positive]
		[FieldCondition("showExplicitU", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		private bool m_ExplicitU;

		[FieldCondition("showFirstU", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[FieldAction("CBSetFirstU", ActionAttribute.ActionEnum.Callback)]
		[Positive]
		[SerializeField]
		private float m_FirstU;

		[FieldCondition("showSecondU", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[Positive]
		[SerializeField]
		private float m_SecondU;

		public int MaterialID
		{
			get
			{
				return m_MaterialID;
			}
			set
			{
				int num = Mathf.Max(0, value);
				if (m_MaterialID != num)
				{
					m_MaterialID = num;
					NotifyModification();
				}
			}
		}

		public bool HardEdge
		{
			get
			{
				return m_HardEdge;
			}
			set
			{
				if (m_HardEdge != value)
				{
					m_HardEdge = value;
					NotifyModification();
				}
			}
		}

		public bool UVEdge
		{
			get
			{
				return m_UVEdge;
			}
			set
			{
				if (m_UVEdge != value)
				{
					m_UVEdge = value;
					NotifyModification();
				}
			}
		}

		public bool ExplicitU
		{
			get
			{
				return m_ExplicitU;
			}
			set
			{
				if (m_ExplicitU != value)
				{
					m_ExplicitU = value;
					NotifyModification();
				}
			}
		}

		public float FirstU
		{
			get
			{
				return m_FirstU;
			}
			set
			{
				if (m_FirstU != value)
				{
					m_FirstU = value;
					NotifyModification();
				}
			}
		}

		public float SecondU
		{
			get
			{
				return m_SecondU;
			}
			set
			{
				if (m_SecondU != value)
				{
					m_SecondU = value;
					NotifyModification();
				}
			}
		}

		public float MaxStepDistance
		{
			get
			{
				return m_MaxStepDistance;
			}
			set
			{
				float num = Mathf.Max(0f, value);
				if (m_MaxStepDistance != num)
				{
					m_MaxStepDistance = num;
					NotifyModification();
				}
			}
		}

		public bool HasDifferentMaterial
		{
			get
			{
				MetaCGOptions previousData = GetPreviousData<MetaCGOptions>();
				if ((bool)previousData)
				{
					return previousData.MaterialID != MaterialID;
				}
				return false;
			}
		}

		private bool showUVEdge
		{
			get
			{
				if ((bool)base.ControlPoint && (base.Spline.Closed || (!(base.Spline.FirstVisibleControlPoint == base.ControlPoint) && !(base.Spline.LastVisibleControlPoint == base.ControlPoint))))
				{
					return !HasDifferentMaterial;
				}
				return false;
			}
		}

		private bool showExplicitU
		{
			get
			{
				if ((bool)base.ControlPoint && !UVEdge)
				{
					return !HasDifferentMaterial;
				}
				return false;
			}
		}

		private bool showFirstU
		{
			get
			{
				bool result = false;
				if ((bool)base.ControlPoint)
				{
					result = UVEdge || ExplicitU || HasDifferentMaterial;
				}
				return result;
			}
		}

		private bool showSecondU
		{
			get
			{
				if (!UVEdge)
				{
					return HasDifferentMaterial;
				}
				return true;
			}
		}

		public void Reset()
		{
			MaterialID = 0;
			HardEdge = false;
			MaxStepDistance = 0f;
			UVEdge = false;
			ExplicitU = false;
			FirstU = 0f;
			SecondU = 0f;
		}

		public float GetDefinedFirstU(float defaultValue)
		{
			if (!UVEdge && !ExplicitU && !HasDifferentMaterial)
			{
				return defaultValue;
			}
			return FirstU;
		}

		public float GetDefinedSecondU(float defaultValue)
		{
			if (!UVEdge && !HasDifferentMaterial)
			{
				return GetDefinedFirstU(defaultValue);
			}
			return SecondU;
		}
	}
}
