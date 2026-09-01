using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Shapes
{
	internal class MpbTriangle : MetaMpb, IDashableMpb
	{
		internal readonly List<Vector4> a;

		internal readonly List<Vector4> b;

		internal readonly List<Vector4> c;

		internal readonly List<Vector4> colorB;

		internal readonly List<Vector4> colorC;

		internal readonly List<float> hollow;

		internal readonly List<float> roundness;

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
