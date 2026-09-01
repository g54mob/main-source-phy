using System.Collections.Generic;
using UnityEngine;

namespace Shapes;

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

	private readonly List<float> _003CShapes_002EIDashableMpb_002EdashOffset_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIDashableMpb_002EdashShapeModifier_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIDashableMpb_002EdashSize_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIDashableMpb_002EdashSnap_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIDashableMpb_002EdashSpace_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIDashableMpb_002EdashSpacing_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIDashableMpb_002EdashType_003Ek__BackingField;

	List<float> IDashableMpb.dashOffset => _003CShapes_002EIDashableMpb_002EdashOffset_003Ek__BackingField;

	List<float> IDashableMpb.dashShapeModifier => _003CShapes_002EIDashableMpb_002EdashShapeModifier_003Ek__BackingField;

	List<float> IDashableMpb.dashSize => _003CShapes_002EIDashableMpb_002EdashSize_003Ek__BackingField;

	List<float> IDashableMpb.dashSnap => _003CShapes_002EIDashableMpb_002EdashSnap_003Ek__BackingField;

	List<float> IDashableMpb.dashSpace => _003CShapes_002EIDashableMpb_002EdashSpace_003Ek__BackingField;

	List<float> IDashableMpb.dashSpacing => _003CShapes_002EIDashableMpb_002EdashSpacing_003Ek__BackingField;

	List<float> IDashableMpb.dashType => _003CShapes_002EIDashableMpb_002EdashType_003Ek__BackingField;

	protected override void TransferShapeProperties()
	{
		Transfer(ShapesMaterialUtils.propA, a);
		Transfer(ShapesMaterialUtils.propB, b);
		Transfer(ShapesMaterialUtils.propC, c);
		Transfer(ShapesMaterialUtils.propColorB, colorB);
		Transfer(ShapesMaterialUtils.propColorC, colorC);
		Transfer(ShapesMaterialUtils.propBorder, hollow);
		Transfer(ShapesMaterialUtils.propRoundness, roundness);
		Transfer(ShapesMaterialUtils.propScaleMode, scaleMode);
		Transfer(ShapesMaterialUtils.propThickness, thickness);
		Transfer(ShapesMaterialUtils.propThicknessSpace, thicknessSpace);
	}

	public MpbTriangle()
	{
		List<Vector4> list = MetaMpb.InitList<Vector4>();
		a = list;
		List<Vector4> list2 = MetaMpb.InitList<Vector4>();
		b = list2;
		c = MetaMpb.InitList<Vector4>();
		colorB = MetaMpb.InitList<Vector4>();
		colorC = MetaMpb.InitList<Vector4>();
		hollow = MetaMpb.InitList<float>();
		roundness = MetaMpb.InitList<float>();
		scaleMode = MetaMpb.InitList<float>();
		thickness = MetaMpb.InitList<float>();
		thicknessSpace = MetaMpb.InitList<float>();
		_003CShapes_002EIDashableMpb_002EdashOffset_003Ek__BackingField = MetaMpb.InitList<float>();
		_003CShapes_002EIDashableMpb_002EdashShapeModifier_003Ek__BackingField = MetaMpb.InitList<float>();
		_003CShapes_002EIDashableMpb_002EdashSize_003Ek__BackingField = MetaMpb.InitList<float>();
		_003CShapes_002EIDashableMpb_002EdashSnap_003Ek__BackingField = MetaMpb.InitList<float>();
		_003CShapes_002EIDashableMpb_002EdashSpace_003Ek__BackingField = MetaMpb.InitList<float>();
		_003CShapes_002EIDashableMpb_002EdashSpacing_003Ek__BackingField = MetaMpb.InitList<float>();
		_003CShapes_002EIDashableMpb_002EdashType_003Ek__BackingField = MetaMpb.InitList<float>();
		base._002Ector();
	}
}
