using System.Collections.Generic;
using UnityEngine;

namespace Shapes;

internal class MpbLine3D : MetaMpb, IDashableMpb
{
	internal readonly List<Vector4> colorEnd;

	internal readonly List<Vector4> pointEnd;

	internal readonly List<Vector4> pointStart;

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
		Transfer(ShapesMaterialUtils.propColorEnd, colorEnd);
		Transfer(ShapesMaterialUtils.propPointEnd, pointEnd);
		Transfer(ShapesMaterialUtils.propPointStart, pointStart);
		Transfer(ShapesMaterialUtils.propScaleMode, scaleMode);
		Transfer(ShapesMaterialUtils.propThickness, thickness);
		Transfer(ShapesMaterialUtils.propThicknessSpace, thicknessSpace);
	}

	public MpbLine3D()
	{
		List<Vector4> list = MetaMpb.InitList<Vector4>();
		colorEnd = list;
		List<Vector4> list2 = MetaMpb.InitList<Vector4>();
		pointEnd = list2;
		pointStart = MetaMpb.InitList<Vector4>();
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
