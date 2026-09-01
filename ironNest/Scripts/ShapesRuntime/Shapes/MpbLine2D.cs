using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Shapes
{
	internal class MpbLine2D : MetaMpb, IDashableMpb
	{
		internal readonly List<float> alignment;

		internal readonly List<Vector4> colorEnd;

		internal readonly List<Vector4> pointEnd;

		internal readonly List<Vector4> pointStart;

		internal readonly List<float> scaleMode;

		internal readonly List<float> thickness;

		internal readonly List<float> thicknessSpace;

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
