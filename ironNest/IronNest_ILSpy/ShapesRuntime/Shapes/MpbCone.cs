using System.Collections.Generic;

namespace Shapes;

internal class MpbCone : MetaMpb
{
	internal readonly List<float> length;

	internal readonly List<float> radius;

	internal readonly List<float> sizeSpace;

	protected override void TransferShapeProperties()
	{
		Transfer(ShapesMaterialUtils.propLength, length);
		Transfer(ShapesMaterialUtils.propRadius, radius);
		Transfer(ShapesMaterialUtils.propSizeSpace, sizeSpace);
	}

	public MpbCone()
	{
		List<float> list = MetaMpb.InitList<float>();
		length = list;
		List<float> list2 = MetaMpb.InitList<float>();
		radius = list2;
		sizeSpace = MetaMpb.InitList<float>();
		base._002Ector();
	}
}
