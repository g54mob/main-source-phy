using System;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

[Serializable]
public struct PolylinePoint
{
	public Vector3 point;

	public Color color;

	public float thickness;

	public unsafe static PolylinePoint operator +(PolylinePoint a, PolylinePoint b)
	{
		//IL_001d: Expected O, but got I
		//IL_0025: Expected native int or pointer, but got O
		//IL_0050: Expected native int or pointer, but got O
		//IL_005d: Expected native int or pointer, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [b @ r8 (Shapes.PolylinePoint)+8]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [a @ rdx (Shapes.PolylinePoint)+8]");
		object obj = num + 0;
		PolylinePoint polylinePoint = default(PolylinePoint);
		Vector3 vector = default(Vector3);
		((PolylinePoint*)(nint)polylinePoint)->point = vector;
		float num2 = a.thickness + b.thickness;
		((PolylinePoint*)(nint)polylinePoint)->thickness = num2;
		((PolylinePoint*)(nint)polylinePoint)->color = (Color)vector;
		return polylinePoint;
	}

	public unsafe static PolylinePoint operator *(PolylinePoint a, float b)
	{
		//IL_0033: Expected native int or pointer, but got O
		//IL_0040: Expected native int or pointer, but got O
		//IL_004d: Expected native int or pointer, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [a @ rdx (Shapes.PolylinePoint)+8]");
		float num = 0f * b;
		float num2 = b * a.thickness;
		PolylinePoint polylinePoint = default(PolylinePoint);
		Vector3 vector = default(Vector3);
		((PolylinePoint*)(nint)polylinePoint)->color = (Color)vector;
		((PolylinePoint*)(nint)polylinePoint)->thickness = num2;
		((PolylinePoint*)(nint)polylinePoint)->point = vector;
		return polylinePoint;
	}

	public unsafe static PolylinePoint operator *(float b, PolylinePoint a)
	{
		//IL_0008: Expected native int or pointer, but got O
		PolylinePoint polylinePoint = default(PolylinePoint);
		Vector3 vector = default(Vector3);
		((PolylinePoint*)(nint)polylinePoint)->point = vector;
		return polylinePoint;
	}

	public unsafe static PolylinePoint Lerp(PolylinePoint a, PolylinePoint b, float t)
	{
		//IL_0024: Expected native int or pointer, but got O
		//IL_0045: Expected native int or pointer, but got O
		//IL_008e: Expected native int or pointer, but got O
		Vector3 vector = default(Vector3);
		object obj = vector - vector;
		float num = (float)obj * t;
		PolylinePoint polylinePoint = default(PolylinePoint);
		((PolylinePoint*)(nint)polylinePoint)->point = vector;
		float num2 = num + (float)vector;
		((PolylinePoint*)(nint)polylinePoint)->color = (Color)vector;
		float num3 = b.thickness - a.thickness;
		float num4 = num3 * t;
		float num5 = num4 + a.thickness;
		((PolylinePoint*)(nint)polylinePoint)->thickness = num5;
		return polylinePoint;
	}

	public PolylinePoint(Vector3 point)
	{
		//IL_000f: Expected O, but got F4
		//IL_0021: Expected O, but got I
		this.point = (Vector3)point.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		color = (Color)0;
		_ = point.z;
		thickness = 1f;
	}

	public PolylinePoint(Vector2 point)
	{
		//IL_001c: Expected O, but got I
		Vector3 vector = default(Vector3);
		this.point = vector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		color = (Color)0;
		thickness = 1f;
		_ = 0;
	}

	public PolylinePoint(Vector3 point, Color color)
	{
		//IL_000f: Expected O, but got F4
		//IL_0033: Expected O, but got F4
		this.point = (Vector3)point.x;
		_ = point.z;
		thickness = 1f;
		this.color = (Color)color.r;
	}

	public PolylinePoint(Vector2 point, Color color)
	{
		//IL_002a: Expected O, but got F4
		Vector3 vector = default(Vector3);
		this.point = vector;
		thickness = 1f;
		_ = 0;
		this.color = (Color)color.r;
	}

	public PolylinePoint(Vector3 point, Color color, float thickness)
	{
		//IL_000f: Expected O, but got F4
		//IL_0032: Expected O, but got F4
		this.point = (Vector3)point.x;
		_ = point.z;
		this.thickness = thickness;
		this.color = (Color)color.r;
	}

	public PolylinePoint(Vector2 point, Color color, float thickness)
	{
		//IL_001f: Expected O, but got F4
		Vector3 vector = default(Vector3);
		this.point = vector;
		_ = 0;
		this.color = (Color)color.r;
		this.thickness = thickness;
	}
}
