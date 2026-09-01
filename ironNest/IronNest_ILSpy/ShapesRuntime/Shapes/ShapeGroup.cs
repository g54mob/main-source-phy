using System;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public class ShapeGroup : MonoBehaviour
{
	public static int shapeGroupsInScene;

	private Color color;

	[NonSerialized]
	private bool _003CIsEnabled_003Ek__BackingField;

	internal bool IsEnabled
	{
		get
		{
			return _003CIsEnabled_003Ek__BackingField;
		}
		private set
		{
			_003CIsEnabled_003Ek__BackingField = value;
		}
	}

	public unsafe Color Color
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Color color = default(Color);
			((Color*)(nint)color)->r = (float)this.color;
			return color;
		}
		set
		{
			//IL_000f: Expected O, but got F4
			color = (Color)value.r;
			UpdateChildShapes();
		}
	}

	private void OnEnable()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_001c: Expected O, but got I4
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		int num = shapeGroupsInScene + 1;
		shapeGroupsInScene = num;
		_003CIsEnabled_003Ek__BackingField = true;
		ShapeRenderer[] componentsInChildren = GetComponentsInChildren<ShapeRenderer>();
		if (componentsInChildren != null)
		{
			object obj = componentsInChildren + 32;
			object obj2 = 0;
			while ((nint)obj2 < componentsInChildren.Length)
			{
				((ShapeRenderer)obj).UpdateAllMaterialProperties();
				obj2++;
				obj += 8;
			}
		}
	}

	private void OnDisable()
	{
		int num = shapeGroupsInScene - 1;
		shapeGroupsInScene = num;
		_003CIsEnabled_003Ek__BackingField = false;
		UpdateChildShapes();
	}

	private void OnValidate()
	{
		UpdateChildShapes();
	}

	private void UpdateChildShapes()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_001c: Expected O, but got I4
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		ShapeRenderer[] componentsInChildren = GetComponentsInChildren<ShapeRenderer>();
		if (componentsInChildren != null)
		{
			object obj = componentsInChildren + 32;
			object obj2 = 0;
			while ((nint)obj2 < componentsInChildren.Length)
			{
				((ShapeRenderer)obj).UpdateAllMaterialProperties();
				obj2++;
				obj += 8;
			}
		}
	}

	public ShapeGroup()
	{
		//IL_0012: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		color = (Color)0;
		base._002Ector();
	}
}
