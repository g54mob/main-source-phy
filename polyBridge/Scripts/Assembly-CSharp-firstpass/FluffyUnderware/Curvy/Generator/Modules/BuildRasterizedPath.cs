using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Build/Rasterize Path", ModuleName = "Rasterize Path", Description = "Rasterizes a virtual path")]
	[HelpURL("https://curvyeditor.com/doclink/cgbuildrasterizedpath")]
	public class BuildRasterizedPath : CGModule, IPathProvider
	{
		[HideInInspector]
		[InputSlotInfo(new Type[] { typeof(CGPath) }, Name = "Path", RequestDataOnly = true)]
		public CGModuleInputSlot InPath = new CGModuleInputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGPath), Name = "Path", DisplayName = "Rasterized Path")]
		public CGModuleOutputSlot OutPath = new CGModuleOutputSlot();

		[FloatRegion(UseSlider = true, RegionOptionsPropertyName = "RangeOptions", Precision = 4)]
		[SerializeField]
		private FloatRegion m_Range = FloatRegion.ZeroOne;

		[SerializeField]
		[RangeEx(1f, 100f, "Resolution", "Defines how densely the path spline's sampling points are. When the value is 100, the number of sampling points per world distance unit is equal to the spline's Max Points Per Unit")]
		private int m_Resolution = 50;

		[SerializeField]
		private bool m_Optimize;

		[FieldCondition("m_Optimize", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		[RangeEx(0.1f, 120f, "", "")]
		private float m_AngleTreshold = 10f;

		public float From
		{
			get
			{
				return m_Range.From;
			}
			set
			{
				float num = Mathf.Repeat(value, 1f);
				if (m_Range.From != num)
				{
					m_Range.From = num;
				}
				base.Dirty = true;
			}
		}

		public float To
		{
			get
			{
				return m_Range.To;
			}
			set
			{
				float num = Mathf.Max(From, value);
				if (PathIsClosed)
				{
					num = Mathf.Repeat(value, 1f);
				}
				if (m_Range.To != num)
				{
					m_Range.To = num;
				}
				base.Dirty = true;
			}
		}

		public float Length
		{
			get
			{
				if (!PathIsClosed)
				{
					return m_Range.To;
				}
				return m_Range.To - m_Range.From;
			}
			set
			{
				float num = (PathIsClosed ? (value - m_Range.To) : value);
				if (m_Range.To != num)
				{
					m_Range.To = num;
				}
				base.Dirty = true;
			}
		}

		public int Resolution
		{
			get
			{
				return m_Resolution;
			}
			set
			{
				int num = Mathf.Clamp(value, 1, 100);
				if (m_Resolution != num)
				{
					m_Resolution = num;
				}
				base.Dirty = true;
			}
		}

		public bool Optimize
		{
			get
			{
				return m_Optimize;
			}
			set
			{
				if (m_Optimize != value)
				{
					m_Optimize = value;
				}
				base.Dirty = true;
			}
		}

		public float AngleThreshold
		{
			get
			{
				return m_AngleTreshold;
			}
			set
			{
				float num = Mathf.Clamp(value, 0.1f, 120f);
				if (m_AngleTreshold != num)
				{
					m_AngleTreshold = num;
				}
				base.Dirty = true;
			}
		}

		public CGPath Path => OutPath.GetData<CGPath>();

		public bool PathIsClosed
		{
			get
			{
				if (!IsConfigured)
				{
					return true;
				}
				return InPath.SourceSlot().PathProvider.PathIsClosed;
			}
		}

		private RegionOptions<float> RangeOptions
		{
			get
			{
				if (!PathIsClosed)
				{
					return RegionOptions<float>.MinMax(0f, 1f);
				}
				return new RegionOptions<float>
				{
					LabelFrom = "Start",
					ClampFrom = DTValueClamping.Min,
					FromMin = 0f,
					LabelTo = "Length",
					ClampTo = DTValueClamping.Range,
					ToMin = 0f,
					ToMax = 1f
				};
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			Properties.MinWidth = 250f;
			Properties.LabelWidth = 100f;
		}

		public override void Reset()
		{
			base.Reset();
			m_Range = FloatRegion.ZeroOne;
			Resolution = 50;
			AngleThreshold = 10f;
			OutPath.ClearData();
			Optimize = false;
		}

		public override void Refresh()
		{
			base.Refresh();
			if (Length == 0f)
			{
				Reset();
				return;
			}
			List<CGDataRequestParameter> list = new List<CGDataRequestParameter>();
			list.Add(new CGDataRequestRasterization(From, Length, Resolution, AngleThreshold, Optimize ? CGDataRequestRasterization.ModeEnum.Optimized : CGDataRequestRasterization.ModeEnum.Even));
			CGPath data = InPath.GetData<CGPath>(list.ToArray());
			OutPath.SetData(data);
		}
	}
}
