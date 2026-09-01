using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Shapes
{
	internal class MpbRegularPolygon : MetaMpb, IFillableMpb, IDashableMpb
	{
		internal readonly List<float> alignment;

		internal readonly List<float> angle;

		internal readonly List<float> hollow;

		internal readonly List<float> radius;

		internal readonly List<float> radiusSpace;

		internal readonly List<float> roundness;

		internal readonly List<float> scaleMode;

		internal readonly List<float> sides;

		internal readonly List<float> thickness;

		internal readonly List<float> thicknessSpace;

		List<Vector4> IFillableMpb.fillColorEnd
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		List<Vector4> IFillableMpb.fillEnd
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		List<float> IFillableMpb.fillSpace
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		List<Vector4> IFillableMpb.fillStart
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		List<float> IFillableMpb.fillType
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		List<float> IDashableMpb.dashOffset
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		List<float> IDashableMpb.dashShapeModifier
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		List<float> IDashableMpb.dashSize
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		List<float> IDashableMpb.dashSnap
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		List<float> IDashableMpb.dashSpace
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		List<float> IDashableMpb.dashSpacing
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		List<float> IDashableMpb.dashType
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		protected override void TransferShapeProperties()
		{
		}
	}
}
