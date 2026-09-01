using System.Collections.Generic;

namespace Shapes;

internal class MpbSphere : MetaMpb
{
	internal readonly List<float> radius;

	internal readonly List<float> radiusSpace;

	protected override void TransferShapeProperties()
	{
		Transfer(ShapesMaterialUtils.propRadius, radius);
		Transfer(ShapesMaterialUtils.propRadiusSpace, radiusSpace);
	}

	public MpbSphere()
	{
		List<float> list = MetaMpb.InitList<float>();
		radius = list;
		List<float> list2 = MetaMpb.InitList<float>();
		radiusSpace = list2;
		base._002Ector();
	}
}
