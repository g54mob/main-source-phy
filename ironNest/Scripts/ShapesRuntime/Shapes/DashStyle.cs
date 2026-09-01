using System;
using UnityEngine;

namespace Shapes
{
	[Serializable]
	public struct DashStyle
	{
		public static readonly DashStyle defaultDashStyle;

		public static readonly DashStyle defaultDashStyleRing;

		public static readonly DashStyle defaultDashStyleLine;

		public DashType type;

		public DashSpace space;

		public DashSnapping snap;

		public float size;

		public float spacing;

		public float offset;

		[Range(-1f, 1f)]
		public float shapeModifier;

		public float UniformSize
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[Obsolete("Deprecated, please use defaultDashStyle instead (lowercase first letter~)")]
		public static DashStyle DefaultDashStyle
		{
			get
			{
				return default(DashStyle);
			}
			set
			{
			}
		}

		[Obsolete("Deprecated, please use defaultDashStyleRing instead (lowercase first letter~)")]
		public static DashStyle DefaultDashStyleRing
		{
			get
			{
				return default(DashStyle);
			}
			set
			{
			}
		}

		[Obsolete("Deprecated, please use defaultDashStyleLine instead (lowercase first letter~)")]
		public static DashStyle DefaultDashStyleLine
		{
			get
			{
				return default(DashStyle);
			}
			set
			{
			}
		}

		private float GetNet(float v, float thickness)
		{
			return 0f;
		}

		internal float GetNetAbsoluteSize(bool dashed, float thickness)
		{
			return 0f;
		}

		internal float GetNetAbsoluteSpacing(bool dashed, float thickness)
		{
			return 0f;
		}

		public static DashStyle RelativeDashes(DashType type, float size, float spacing, DashSnapping snap = DashSnapping.Off, float offset = 0f, float shapeModifier = 1f)
		{
			return default(DashStyle);
		}

		public static DashStyle FixedDashCount(DashType type, float count, float spacingFraction = 0.5f, DashSnapping snap = DashSnapping.Off, float offset = 0f, float shapeModifier = 1f)
		{
			return default(DashStyle);
		}

		public static DashStyle MeterDashes(DashType type, float size, float spacing, DashSnapping snap = DashSnapping.Off, float offset = 0f, float shapeModifier = 1f)
		{
			return default(DashStyle);
		}

		[Obsolete("Deprecated, please use <c>DashStyle.RelativeDashes/FixedCountDashes/MeterDashes</c> instead", true)]
		public DashStyle(float size)
		{
			type = default(DashType);
			space = default(DashSpace);
			snap = default(DashSnapping);
			this.size = 0f;
			spacing = 0f;
			offset = 0f;
			shapeModifier = 0f;
		}

		[Obsolete("Deprecated, please use <c>DashStyle.RelativeDashes/FixedCountDashes/MeterDashes</c> instead", true)]
		public DashStyle(float size, DashType type)
		{
			this.type = default(DashType);
			space = default(DashSpace);
			snap = default(DashSnapping);
			this.size = 0f;
			spacing = 0f;
			offset = 0f;
			shapeModifier = 0f;
		}

		[Obsolete("Deprecated, please use <c>DashStyle.RelativeDashes/FixedCountDashes/MeterDashes</c> instead", true)]
		public DashStyle(float size, float spacing, DashType type)
		{
			this.type = default(DashType);
			space = default(DashSpace);
			snap = default(DashSnapping);
			this.size = 0f;
			this.spacing = 0f;
			offset = 0f;
			shapeModifier = 0f;
		}

		[Obsolete("Deprecated, please use <c>DashStyle.RelativeDashes/FixedCountDashes/MeterDashes</c> instead", true)]
		public DashStyle(float size, float spacing)
		{
			type = default(DashType);
			space = default(DashSpace);
			snap = default(DashSnapping);
			this.size = 0f;
			this.spacing = 0f;
			offset = 0f;
			shapeModifier = 0f;
		}

		[Obsolete("Deprecated, please use <c>DashStyle.RelativeDashes/FixedCountDashes/MeterDashes</c> instead", true)]
		public DashStyle(float size, float spacing, float offset)
		{
			type = default(DashType);
			space = default(DashSpace);
			snap = default(DashSnapping);
			this.size = 0f;
			this.spacing = 0f;
			this.offset = 0f;
			shapeModifier = 0f;
		}
	}
}
