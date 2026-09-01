using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public class PolylinePath : PointPath<PolylinePoint>
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<Vector3, PolylinePoint> _003C_003E9__19_0;

		public static Func<Vector3, PolylinePoint> _003C_003E9__20_0;

		public static Func<Vector2, PolylinePoint> _003C_003E9__21_0;

		public static Func<Vector2, PolylinePoint> _003C_003E9__22_0;

		public static Func<Vector3, Color, PolylinePoint> _003C_003E9__25_0;

		public static Func<Vector2, Color, PolylinePoint> _003C_003E9__26_0;

		public static Func<Vector3, float, PolylinePoint> _003C_003E9__27_0;

		public static Func<Vector2, float, PolylinePoint> _003C_003E9__28_0;

		public static Func<Vector3, Color, float, PolylinePoint> _003C_003E9__29_0;

		public static Func<Vector2, Color, float, PolylinePoint> _003C_003E9__30_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal unsafe PolylinePoint _003CAddPoints_003Eb__19_0(Vector3 point)
		{
			//IL_0012: Expected O, but got F4
			//IL_000d: Expected native int or pointer, but got O
			//IL_0031: Expected O, but got I
			//IL_002c: Expected native int or pointer, but got O
			//IL_003a: Expected native int or pointer, but got O
			PolylinePoint polylinePoint = default(PolylinePoint);
			((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)point.x;
			_ = point.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
			((PolylinePoint*)(nint)polylinePoint)->color = (Color)0;
			((PolylinePoint*)(nint)polylinePoint)->thickness = 1f;
			return polylinePoint;
		}

		internal unsafe PolylinePoint _003CAddPoints_003Eb__20_0(Vector3 point)
		{
			//IL_0012: Expected O, but got F4
			//IL_000d: Expected native int or pointer, but got O
			//IL_0031: Expected O, but got I
			//IL_002c: Expected native int or pointer, but got O
			//IL_003a: Expected native int or pointer, but got O
			PolylinePoint polylinePoint = default(PolylinePoint);
			((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)point.x;
			_ = point.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
			((PolylinePoint*)(nint)polylinePoint)->color = (Color)0;
			((PolylinePoint*)(nint)polylinePoint)->thickness = 1f;
			return polylinePoint;
		}

		internal unsafe PolylinePoint _003CAddPoints_003Eb__21_0(Vector2 point)
		{
			//IL_0008: Expected native int or pointer, but got O
			//IL_0028: Expected O, but got I
			//IL_0023: Expected native int or pointer, but got O
			//IL_0031: Expected native int or pointer, but got O
			PolylinePoint polylinePoint = default(PolylinePoint);
			Vector3 point2 = default(Vector3);
			((PolylinePoint*)(nint)polylinePoint)->point = point2;
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
			((PolylinePoint*)(nint)polylinePoint)->color = (Color)0;
			((PolylinePoint*)(nint)polylinePoint)->thickness = 1f;
			return polylinePoint;
		}

		internal unsafe PolylinePoint _003CAddPoints_003Eb__22_0(Vector2 point)
		{
			//IL_0008: Expected native int or pointer, but got O
			//IL_0028: Expected O, but got I
			//IL_0023: Expected native int or pointer, but got O
			//IL_0031: Expected native int or pointer, but got O
			PolylinePoint polylinePoint = default(PolylinePoint);
			Vector3 point2 = default(Vector3);
			((PolylinePoint*)(nint)polylinePoint)->point = point2;
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
			((PolylinePoint*)(nint)polylinePoint)->color = (Color)0;
			((PolylinePoint*)(nint)polylinePoint)->thickness = 1f;
			return polylinePoint;
		}

		internal unsafe PolylinePoint _003CAddPoints_003Eb__25_0(Vector3 p, Color c)
		{
			//IL_0012: Expected O, but got F4
			//IL_000d: Expected native int or pointer, but got O
			//IL_0025: Expected native int or pointer, but got O
			//IL_003c: Expected O, but got F4
			//IL_0037: Expected native int or pointer, but got O
			PolylinePoint polylinePoint = default(PolylinePoint);
			((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)p.x;
			_ = p.z;
			((PolylinePoint*)(nint)polylinePoint)->thickness = 1f;
			((PolylinePoint*)(nint)polylinePoint)->color = (Color)c.r;
			return polylinePoint;
		}

		internal unsafe PolylinePoint _003CAddPoints_003Eb__26_0(Vector2 p, Color c)
		{
			//IL_0008: Expected native int or pointer, but got O
			//IL_0016: Expected native int or pointer, but got O
			//IL_0033: Expected O, but got F4
			//IL_002e: Expected native int or pointer, but got O
			PolylinePoint polylinePoint = default(PolylinePoint);
			Vector3 point = default(Vector3);
			((PolylinePoint*)(nint)polylinePoint)->point = point;
			((PolylinePoint*)(nint)polylinePoint)->thickness = 1f;
			_ = 0;
			((PolylinePoint*)(nint)polylinePoint)->color = (Color)c.r;
			return polylinePoint;
		}

		internal unsafe PolylinePoint _003CAddPoints_003Eb__27_0(Vector3 p, float t)
		{
			//IL_0012: Expected O, but got F4
			//IL_000d: Expected native int or pointer, but got O
			//IL_0024: Expected native int or pointer, but got O
			//IL_003e: Expected O, but got I
			//IL_0039: Expected native int or pointer, but got O
			PolylinePoint polylinePoint = default(PolylinePoint);
			((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)p.x;
			_ = p.z;
			((PolylinePoint*)(nint)polylinePoint)->thickness = t;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
			((PolylinePoint*)(nint)polylinePoint)->color = (Color)0;
			return polylinePoint;
		}

		internal unsafe PolylinePoint _003CAddPoints_003Eb__28_0(Vector2 p, float t)
		{
			//IL_0008: Expected native int or pointer, but got O
			//IL_001b: Expected native int or pointer, but got O
			//IL_0035: Expected O, but got I
			//IL_0030: Expected native int or pointer, but got O
			PolylinePoint polylinePoint = default(PolylinePoint);
			Vector3 point = default(Vector3);
			((PolylinePoint*)(nint)polylinePoint)->point = point;
			_ = 0;
			((PolylinePoint*)(nint)polylinePoint)->thickness = t;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
			((PolylinePoint*)(nint)polylinePoint)->color = (Color)0;
			return polylinePoint;
		}

		internal unsafe PolylinePoint _003CAddPoints_003Eb__29_0(Vector3 p, Color c, float t)
		{
			//IL_0012: Expected O, but got F4
			//IL_000d: Expected native int or pointer, but got O
			//IL_0024: Expected native int or pointer, but got O
			//IL_003b: Expected O, but got F4
			//IL_0036: Expected native int or pointer, but got O
			PolylinePoint polylinePoint = default(PolylinePoint);
			((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)p.x;
			_ = p.z;
			float thickness = default(float);
			((PolylinePoint*)(nint)polylinePoint)->thickness = thickness;
			((PolylinePoint*)(nint)polylinePoint)->color = (Color)c.r;
			return polylinePoint;
		}

		internal unsafe PolylinePoint _003CAddPoints_003Eb__30_0(Vector2 p, Color c, float t)
		{
			//IL_0008: Expected native int or pointer, but got O
			//IL_0025: Expected O, but got F4
			//IL_0020: Expected native int or pointer, but got O
			//IL_002d: Expected native int or pointer, but got O
			PolylinePoint polylinePoint = default(PolylinePoint);
			Vector3 point = default(Vector3);
			((PolylinePoint*)(nint)polylinePoint)->point = point;
			_ = 0;
			((PolylinePoint*)(nint)polylinePoint)->color = (Color)c.r;
			float thickness = default(float);
			((PolylinePoint*)(nint)polylinePoint)->thickness = thickness;
			return polylinePoint;
		}
	}

	private sealed class _003C_003Ec__DisplayClass23_0
	{
		public Color color;

		internal unsafe PolylinePoint _003CAddPoints_003Eb__0(Vector3 point)
		{
			//IL_0012: Expected O, but got F4
			//IL_000d: Expected native int or pointer, but got O
			//IL_0025: Expected native int or pointer, but got O
			//IL_0034: Expected native int or pointer, but got O
			PolylinePoint polylinePoint = default(PolylinePoint);
			((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)point.x;
			_ = point.z;
			((PolylinePoint*)(nint)polylinePoint)->thickness = 1f;
			((PolylinePoint*)(nint)polylinePoint)->color = color;
			return polylinePoint;
		}
	}

	private sealed class _003C_003Ec__DisplayClass24_0
	{
		public Color color;

		internal unsafe PolylinePoint _003CAddPoints_003Eb__0(Vector2 point)
		{
			//IL_0008: Expected native int or pointer, but got O
			//IL_0016: Expected native int or pointer, but got O
			//IL_002b: Expected native int or pointer, but got O
			PolylinePoint polylinePoint = default(PolylinePoint);
			Vector3 point2 = default(Vector3);
			((PolylinePoint*)(nint)polylinePoint)->point = point2;
			((PolylinePoint*)(nint)polylinePoint)->thickness = 1f;
			_ = 0;
			((PolylinePoint*)(nint)polylinePoint)->color = color;
			return polylinePoint;
		}
	}

	private sealed class _003C_003Ec__DisplayClass49_0
	{
		public PolylinePath _003C_003E4__this;

		public bool closed;

		public PolylineJoins renderJoins;

		internal void _003CEnsureMeshIsReadyToRender_003Eb__0()
		{
			//IL_0045: Expected O, but got I
			//IL_0045: Expected O, but got I
			PolylinePath polylinePath = _003C_003E4__this;
			polylinePath.lastUsedClosed = closed;
			polylinePath.lastUsedJoins = renderJoins;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbx_v1 (Shapes.PolylinePath)+10]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbx_v1 (Shapes.PolylinePath)+28]");
			bool flattenZ = default(bool);
			bool useColors = default(bool);
			ShapesMeshGen.GenPolylineMesh((Mesh)num, (IList<PolylinePoint>)0, closed, renderJoins, flattenZ, useColors);
		}
	}

	private const MethodImplOptions INLINE = MethodImplOptions.AggressiveInlining;

	private bool lastUsedClosed;

	private PolylineJoins lastUsedJoins = PolylineJoins.Miter;

	public unsafe void SetPoint(int index, Vector3 point)
	{
		//IL_001d: Expected O, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		object obj = default(object);
		SetPoint(index, (PolylinePoint)(&obj));
	}

	public unsafe void SetPoint(int index, Vector2 point)
	{
		//IL_001d: Expected O, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		object obj = default(object);
		SetPoint(index, (PolylinePoint)(&obj));
	}

	public unsafe void SetColor(int index, Color color)
	{
		//IL_001d: Expected O, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		object obj = default(object);
		SetPoint(index, (PolylinePoint)(&obj));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoint(float x, float y)
	{
		//IL_000f: Expected O, but got Ref
		object obj = default(object);
		AddPoint((PolylinePoint)(&obj));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoint(float x, float y, float z)
	{
		//IL_000f: Expected O, but got Ref
		object obj = default(object);
		AddPoint((PolylinePoint)(&obj));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoint(float x, float y, Color color)
	{
		//IL_000f: Expected O, but got Ref
		object obj = default(object);
		AddPoint((PolylinePoint)(&obj));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoint(float x, float y, float z, Color color)
	{
		//IL_000f: Expected O, but got Ref
		object obj = default(object);
		AddPoint((PolylinePoint)(&obj));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoint(Vector3 pos)
	{
		//IL_000f: Expected O, but got Ref
		object obj = default(object);
		AddPoint((PolylinePoint)(&obj));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoint(Vector3 pos, Color color)
	{
		//IL_000f: Expected O, but got Ref
		object obj = default(object);
		AddPoint((PolylinePoint)(&obj));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoint(Vector3 pos, float thickness)
	{
		//IL_000f: Expected O, but got Ref
		object obj = default(object);
		AddPoint((PolylinePoint)(&obj));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoint(Vector3 pos, float thickness, Color color)
	{
		//IL_000f: Expected O, but got Ref
		object obj = default(object);
		AddPoint((PolylinePoint)(&obj));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoint(Vector2 pos)
	{
		//IL_000f: Expected O, but got Ref
		object obj = default(object);
		AddPoint((PolylinePoint)(&obj));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoint(Vector2 pos, Color color)
	{
		//IL_000f: Expected O, but got Ref
		object obj = default(object);
		AddPoint((PolylinePoint)(&obj));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoint(Vector2 pos, float thickness)
	{
		//IL_000f: Expected O, but got Ref
		object obj = default(object);
		AddPoint((PolylinePoint)(&obj));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoint(Vector2 pos, float thickness, Color color)
	{
		//IL_000f: Expected O, but got Ref
		object obj = default(object);
		AddPoint((PolylinePoint)(&obj));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoints(IEnumerable<Vector3> pts)
	{
		Func<Vector3, PolylinePoint> selector = _003C_003Ec._003C_003E9__19_0;
		if (_003C_003Ec._003C_003E9__19_0 == null)
		{
			selector = (_003C_003Ec._003C_003E9__19_0 = delegate(Vector3 point)
			{
				//IL_0012: Expected O, but got F4
				//IL_000d: Expected native int or pointer, but got O
				//IL_0031: Expected O, but got I
				//IL_002c: Expected native int or pointer, but got O
				//IL_003a: Expected native int or pointer, but got O
				PolylinePoint polylinePoint = default(PolylinePoint);
				((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)point.x;
				_ = point.z;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
				((PolylinePoint*)(nint)polylinePoint)->color = (Color)0;
				((PolylinePoint*)(nint)polylinePoint)->thickness = 1f;
				return polylinePoint;
			});
		}
		IEnumerable<PolylinePoint> ptsToAdd = Enumerable.Select(pts, selector);
		AddPoints(ptsToAdd);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoints(Vector3[] pts)
	{
		Func<Vector3, PolylinePoint> selector = _003C_003Ec._003C_003E9__20_0;
		if (_003C_003Ec._003C_003E9__20_0 == null)
		{
			selector = (_003C_003Ec._003C_003E9__20_0 = delegate(Vector3 point)
			{
				//IL_0012: Expected O, but got F4
				//IL_000d: Expected native int or pointer, but got O
				//IL_0031: Expected O, but got I
				//IL_002c: Expected native int or pointer, but got O
				//IL_003a: Expected native int or pointer, but got O
				PolylinePoint polylinePoint = default(PolylinePoint);
				((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)point.x;
				_ = point.z;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
				((PolylinePoint*)(nint)polylinePoint)->color = (Color)0;
				((PolylinePoint*)(nint)polylinePoint)->thickness = 1f;
				return polylinePoint;
			});
		}
		IEnumerable<PolylinePoint> ptsToAdd = Enumerable.Select(pts, selector);
		AddPoints(ptsToAdd);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoints(IEnumerable<Vector2> pts)
	{
		Func<Vector2, PolylinePoint> selector = _003C_003Ec._003C_003E9__21_0;
		if (_003C_003Ec._003C_003E9__21_0 == null)
		{
			selector = (_003C_003Ec._003C_003E9__21_0 = delegate
			{
				//IL_0008: Expected native int or pointer, but got O
				//IL_0028: Expected O, but got I
				//IL_0023: Expected native int or pointer, but got O
				//IL_0031: Expected native int or pointer, but got O
				PolylinePoint polylinePoint = default(PolylinePoint);
				Vector3 point2 = default(Vector3);
				((PolylinePoint*)(nint)polylinePoint)->point = point2;
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
				((PolylinePoint*)(nint)polylinePoint)->color = (Color)0;
				((PolylinePoint*)(nint)polylinePoint)->thickness = 1f;
				return polylinePoint;
			});
		}
		IEnumerable<PolylinePoint> ptsToAdd = Enumerable.Select(pts, selector);
		AddPoints(ptsToAdd);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoints(Vector2[] pts)
	{
		Func<Vector2, PolylinePoint> selector = _003C_003Ec._003C_003E9__22_0;
		if (_003C_003Ec._003C_003E9__22_0 == null)
		{
			selector = (_003C_003Ec._003C_003E9__22_0 = delegate
			{
				//IL_0008: Expected native int or pointer, but got O
				//IL_0028: Expected O, but got I
				//IL_0023: Expected native int or pointer, but got O
				//IL_0031: Expected native int or pointer, but got O
				PolylinePoint polylinePoint = default(PolylinePoint);
				Vector3 point2 = default(Vector3);
				((PolylinePoint*)(nint)polylinePoint)->point = point2;
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
				((PolylinePoint*)(nint)polylinePoint)->color = (Color)0;
				((PolylinePoint*)(nint)polylinePoint)->thickness = 1f;
				return polylinePoint;
			});
		}
		IEnumerable<PolylinePoint> ptsToAdd = Enumerable.Select(pts, selector);
		AddPoints(ptsToAdd);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoints(IEnumerable<Vector3> pts, Color color)
	{
		//IL_0017: Expected O, but got F4
		_003C_003Ec__DisplayClass23_0 CS_0024_003C_003E8__locals2 = new _003C_003Ec__DisplayClass23_0();
		CS_0024_003C_003E8__locals2.color = (Color)color.r;
		Func<Vector3, PolylinePoint> selector = delegate(Vector3 point)
		{
			//IL_0012: Expected O, but got F4
			//IL_000d: Expected native int or pointer, but got O
			//IL_0025: Expected native int or pointer, but got O
			//IL_0034: Expected native int or pointer, but got O
			PolylinePoint polylinePoint = default(PolylinePoint);
			((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)point.x;
			_ = point.z;
			((PolylinePoint*)(nint)polylinePoint)->thickness = 1f;
			((PolylinePoint*)(nint)polylinePoint)->color = CS_0024_003C_003E8__locals2.color;
			return polylinePoint;
		};
		IEnumerable<PolylinePoint> ptsToAdd = Enumerable.Select(pts, selector);
		AddPoints(ptsToAdd);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoints(IEnumerable<Vector2> pts, Color color)
	{
		//IL_0017: Expected O, but got F4
		_003C_003Ec__DisplayClass24_0 CS_0024_003C_003E8__locals2 = new _003C_003Ec__DisplayClass24_0();
		CS_0024_003C_003E8__locals2.color = (Color)color.r;
		Func<Vector2, PolylinePoint> selector = delegate
		{
			//IL_0008: Expected native int or pointer, but got O
			//IL_0016: Expected native int or pointer, but got O
			//IL_002b: Expected native int or pointer, but got O
			PolylinePoint polylinePoint = default(PolylinePoint);
			Vector3 point2 = default(Vector3);
			((PolylinePoint*)(nint)polylinePoint)->point = point2;
			((PolylinePoint*)(nint)polylinePoint)->thickness = 1f;
			_ = 0;
			((PolylinePoint*)(nint)polylinePoint)->color = CS_0024_003C_003E8__locals2.color;
			return polylinePoint;
		};
		IEnumerable<PolylinePoint> ptsToAdd = Enumerable.Select(pts, selector);
		AddPoints(ptsToAdd);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoints(IEnumerable<Vector3> pts, IEnumerable<Color> colors)
	{
		Func<Vector3, Color, PolylinePoint> resultSelector = _003C_003Ec._003C_003E9__25_0;
		if (_003C_003Ec._003C_003E9__25_0 == null)
		{
			resultSelector = (_003C_003Ec._003C_003E9__25_0 = delegate(Vector3 p, Color c)
			{
				//IL_0012: Expected O, but got F4
				//IL_000d: Expected native int or pointer, but got O
				//IL_0025: Expected native int or pointer, but got O
				//IL_003c: Expected O, but got F4
				//IL_0037: Expected native int or pointer, but got O
				PolylinePoint polylinePoint = default(PolylinePoint);
				((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)p.x;
				_ = p.z;
				((PolylinePoint*)(nint)polylinePoint)->thickness = 1f;
				((PolylinePoint*)(nint)polylinePoint)->color = (Color)c.r;
				return polylinePoint;
			});
		}
		IEnumerable<PolylinePoint> ptsToAdd = Enumerable.Zip(pts, colors, resultSelector);
		AddPoints(ptsToAdd);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoints(IEnumerable<Vector2> pts, IEnumerable<Color> colors)
	{
		Func<Vector2, Color, PolylinePoint> resultSelector = _003C_003Ec._003C_003E9__26_0;
		if (_003C_003Ec._003C_003E9__26_0 == null)
		{
			resultSelector = (_003C_003Ec._003C_003E9__26_0 = delegate(Vector2 p, Color c)
			{
				//IL_0008: Expected native int or pointer, but got O
				//IL_0016: Expected native int or pointer, but got O
				//IL_0033: Expected O, but got F4
				//IL_002e: Expected native int or pointer, but got O
				PolylinePoint polylinePoint = default(PolylinePoint);
				Vector3 point = default(Vector3);
				((PolylinePoint*)(nint)polylinePoint)->point = point;
				((PolylinePoint*)(nint)polylinePoint)->thickness = 1f;
				_ = 0;
				((PolylinePoint*)(nint)polylinePoint)->color = (Color)c.r;
				return polylinePoint;
			});
		}
		IEnumerable<PolylinePoint> ptsToAdd = Enumerable.Zip(pts, colors, resultSelector);
		AddPoints(ptsToAdd);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoints(IEnumerable<Vector3> pts, IEnumerable<float> thicknesses)
	{
		Func<Vector3, float, PolylinePoint> resultSelector = _003C_003Ec._003C_003E9__27_0;
		if (_003C_003Ec._003C_003E9__27_0 == null)
		{
			resultSelector = (_003C_003Ec._003C_003E9__27_0 = delegate(Vector3 p, float t)
			{
				//IL_0012: Expected O, but got F4
				//IL_000d: Expected native int or pointer, but got O
				//IL_0024: Expected native int or pointer, but got O
				//IL_003e: Expected O, but got I
				//IL_0039: Expected native int or pointer, but got O
				PolylinePoint polylinePoint = default(PolylinePoint);
				((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)p.x;
				_ = p.z;
				((PolylinePoint*)(nint)polylinePoint)->thickness = t;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
				((PolylinePoint*)(nint)polylinePoint)->color = (Color)0;
				return polylinePoint;
			});
		}
		IEnumerable<PolylinePoint> ptsToAdd = Enumerable.Zip(pts, thicknesses, resultSelector);
		AddPoints(ptsToAdd);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoints(IEnumerable<Vector2> pts, IEnumerable<float> thicknesses)
	{
		Func<Vector2, float, PolylinePoint> resultSelector = _003C_003Ec._003C_003E9__28_0;
		if (_003C_003Ec._003C_003E9__28_0 == null)
		{
			resultSelector = (_003C_003Ec._003C_003E9__28_0 = delegate(Vector2 p, float t)
			{
				//IL_0008: Expected native int or pointer, but got O
				//IL_001b: Expected native int or pointer, but got O
				//IL_0035: Expected O, but got I
				//IL_0030: Expected native int or pointer, but got O
				PolylinePoint polylinePoint = default(PolylinePoint);
				Vector3 point = default(Vector3);
				((PolylinePoint*)(nint)polylinePoint)->point = point;
				_ = 0;
				((PolylinePoint*)(nint)polylinePoint)->thickness = t;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
				((PolylinePoint*)(nint)polylinePoint)->color = (Color)0;
				return polylinePoint;
			});
		}
		IEnumerable<PolylinePoint> ptsToAdd = Enumerable.Zip(pts, thicknesses, resultSelector);
		AddPoints(ptsToAdd);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoints(IEnumerable<Vector3> pts, IEnumerable<float> thicknesses, IEnumerable<Color> colors)
	{
		Func<Vector3, Color, float, PolylinePoint> func = _003C_003Ec._003C_003E9__29_0;
		if (_003C_003Ec._003C_003E9__29_0 == null)
		{
			func = (_003C_003Ec._003C_003E9__29_0 = delegate(Vector3 p, Color c, float t)
			{
				//IL_0012: Expected O, but got F4
				//IL_000d: Expected native int or pointer, but got O
				//IL_0024: Expected native int or pointer, but got O
				//IL_003b: Expected O, but got F4
				//IL_0036: Expected native int or pointer, but got O
				PolylinePoint polylinePoint = default(PolylinePoint);
				((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)p.x;
				_ = p.z;
				float thickness = default(float);
				((PolylinePoint*)(nint)polylinePoint)->thickness = thickness;
				((PolylinePoint*)(nint)polylinePoint)->color = (Color)c.r;
				return polylinePoint;
			});
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180747720");
		IEnumerable<PolylinePoint> ptsToAdd = default(IEnumerable<PolylinePoint>);
		AddPoints(ptsToAdd);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void AddPoints(IEnumerable<Vector2> pts, IEnumerable<float> thicknesses, IEnumerable<Color> colors)
	{
		Func<Vector2, Color, float, PolylinePoint> func = _003C_003Ec._003C_003E9__30_0;
		if (_003C_003Ec._003C_003E9__30_0 == null)
		{
			func = (_003C_003Ec._003C_003E9__30_0 = delegate(Vector2 p, Color c, float t)
			{
				//IL_0008: Expected native int or pointer, but got O
				//IL_0025: Expected O, but got F4
				//IL_0020: Expected native int or pointer, but got O
				//IL_002d: Expected native int or pointer, but got O
				PolylinePoint polylinePoint = default(PolylinePoint);
				Vector3 point = default(Vector3);
				((PolylinePoint*)(nint)polylinePoint)->point = point;
				_ = 0;
				((PolylinePoint*)(nint)polylinePoint)->color = (Color)c.r;
				float thickness = default(float);
				((PolylinePoint*)(nint)polylinePoint)->thickness = thickness;
				return polylinePoint;
			});
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180747720");
		IEnumerable<PolylinePoint> ptsToAdd = default(IEnumerable<PolylinePoint>);
		AddPoints(ptsToAdd);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void BezierTo(Vector3 startTangent, Vector3 endTangent, Vector3 end)
	{
		//IL_0025: Expected O, but got Ref
		//IL_0025: Expected O, but got Ref
		//IL_0025: Expected O, but got Ref
		ShapesConfig instance = ShapesConfig.Instance;
		object obj = default(object);
		object obj2 = default(object);
		float num = default(float);
		float pointsPerTurn = default(float);
		BezierTo((Vector3)(&obj), (Vector3)(&obj2), (Vector3)(&num), pointsPerTurn);
	}

	public unsafe void BezierTo(Vector3 startTangent, Vector3 endTangent, Vector3 end, float pointsPerTurn)
	{
		//IL_0038: Expected O, but got Ref
		//IL_0038: Expected O, but got Ref
		//IL_0038: Expected O, but got Ref
		//IL_0038: Expected O, but got Ref
		//IL_01a3: Expected I4, but got I8
		//IL_0104: Expected O, but got F4
		//IL_011b: Expected O, but got F4
		//IL_0141: Expected O, but got F4
		if (!CheckCanAddContinuePoint("BezierTo"))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808D6AE0");
			ShapesConfig instance = ShapesConfig.Instance;
			object obj = default(object);
			object obj2 = default(object);
			object obj3 = default(object);
			object obj4 = default(object);
			int vertCount = default(int);
			float approximateAngularCurveSumDegrees = ShapesMath.GetApproximateAngularCurveSumDegrees((Vector3)(&obj), (Vector3)(&obj2), (Vector3)(&obj3), (Vector3)(&obj4), vertCount);
			float num = approximateAngularCurveSumDegrees / 360f;
			object obj5 = default(object);
			float num2 = num * (float)obj5;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
			int num3 = default(int);
			bool flag = num3 >= 2;
			int num4 = num3;
			if (!flag)
			{
				num4 = 2;
			}
			if (!CheckCanAddContinuePoint("BezierTo"))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808D6AE0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808D6AE0");
				ShapesMath._003CCubicBezierPointsSkipFirstMatchStyle_003Ed__39 obj6 = new ShapesMath._003CCubicBezierPointsSkipFirstMatchStyle_003Ed__39(0);
				obj6._003C_003E1__state = -2;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				int num5 = default(int);
				obj6._003C_003El__initialThreadId = num5;
				obj6._003C_003E3__count = num4;
				PolylinePoint polylinePoint = default(PolylinePoint);
				obj6._003C_003E3__style = polylinePoint;
				Vector3 vector = default(Vector3);
				obj6._003C_003E3__a = vector;
				obj6._003C_003E3__b = (Vector3)startTangent.x;
				obj6._003C_003E3__c = (Vector3)endTangent.x;
				_ = startTangent.z;
				_ = endTangent.z;
				obj6._003C_003E3__d = (Vector3)end.x;
				_ = end.z;
				AddPoints(obj6);
			}
		}
	}

	public void BezierTo(Vector3 startTangent, Vector3 endTangent, Vector3 end, int pointCount)
	{
		//IL_0103: Expected I4, but got I8
		//IL_0061: Expected O, but got F4
		//IL_007d: Expected O, but got F4
		//IL_0099: Expected O, but got F4
		if (!CheckCanAddContinuePoint("BezierTo"))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808D6AE0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808D6AE0");
			ShapesMath._003CCubicBezierPointsSkipFirstMatchStyle_003Ed__39 obj = new ShapesMath._003CCubicBezierPointsSkipFirstMatchStyle_003Ed__39(0);
			obj._003C_003E1__state = -2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj._003C_003El__initialThreadId = num;
			PolylinePoint polylinePoint = default(PolylinePoint);
			obj._003C_003E3__style = polylinePoint;
			Vector3 vector = default(Vector3);
			obj._003C_003E3__a = vector;
			obj._003C_003E3__b = (Vector3)startTangent.x;
			_ = startTangent.z;
			obj._003C_003E3__c = (Vector3)endTangent.x;
			_ = endTangent.z;
			obj._003C_003E3__d = (Vector3)end.x;
			_ = end.z;
			int num2 = default(int);
			obj._003C_003E3__count = num2;
			AddPoints(obj);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void BezierTo(Vector3 startTangent, Vector3 endTangent, PolylinePoint end)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Expected O, but got Unknown
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Expected O, but got Unknown
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = obj2 - 87;
		ShapesConfig instance = ShapesConfig.Instance;
		if (!CheckCanAddContinuePoint("BezierTo"))
		{
			object obj3 = obj - 17;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808D6AE0");
			ShapesConfig instance2 = ShapesConfig.Instance;
			Vector3 d = (Vector3)(obj - 81);
			Vector3 c = (Vector3)(obj - 65);
			_ = startTangent.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-9]");
			_ = 0;
			Vector3 b = (Vector3)(obj - 49);
			_ = endTangent.x;
			_ = end.point;
			_ = startTangent.x;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm6,8\"");
			_ = endTangent.z;
			Vector3 a = (Vector3)(obj - 33);
			_ = end.point;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-11]");
			_ = 0;
			int num = default(int);
			float approximateAngularCurveSumDegrees = ShapesMath.GetApproximateAngularCurveSumDegrees(a, b, c, d, num);
			float num2 = approximateAngularCurveSumDegrees / 360f;
			float num3 = num2 * instance.polylineDefaultPointsPerTurn;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
			object obj4 = default(object);
			if ((nint)obj4 >= 2)
			{
				PolylinePoint end2 = (PolylinePoint)(obj - 17);
				Vector3 endTangent2 = (Vector3)(obj - 33);
				_ = endTangent.z;
				_ = end.point;
				Vector3 startTangent2 = (Vector3)(obj - 49);
				_ = endTangent.x;
				_ = startTangent.x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [end @ r9 (Shapes.PolylinePoint)+10]");
				_ = 0;
				_ = startTangent.z;
				BezierTo(startTangent2, endTangent2, end2, num);
			}
		}
	}

	public unsafe void BezierTo(Vector3 startTangent, Vector3 endTangent, PolylinePoint end, float pointsPerTurn)
	{
		//IL_0008: Expected O, but got Ref
		//IL_001b: Expected O, but got Ref
		//IL_0041: Expected O, but got Ref
		//IL_004f: Expected O, but got Ref
		//IL_0074: Expected O, but got Ref
		//IL_00b4: Expected O, but got Ref
		//IL_0165: Expected O, but got Ref
		//IL_0173: Expected O, but got Ref
		//IL_0195: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		int num = default(int);
		object obj4 = default(object);
		do
		{
			if (!CheckCanAddContinuePoint("BezierTo"))
			{
				object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808D6AE0");
				ShapesConfig instance = ShapesConfig.Instance;
				Vector3 d = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
				Vector3 c = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
				_ = startTangent.z;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-1]");
				_ = 0;
				Vector3 b = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
				_ = endTangent.x;
				_ = end.point;
				_ = startTangent.x;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm6,8\"");
				_ = endTangent.z;
				Vector3 a = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
				_ = end.point;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-9]");
				_ = 0;
				float approximateAngularCurveSumDegrees = ShapesMath.GetApproximateAngularCurveSumDegrees(a, b, c, d, num);
				float num2 = approximateAngularCurveSumDegrees / 360f;
				float num3 = num2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+77]");
				float num4 = num3 * 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
				continue;
			}
			return;
		}
		while ((nint)obj4 < 2);
		PolylinePoint end2 = (PolylinePoint)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
		Vector3 endTangent2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
		_ = endTangent.z;
		_ = end.point;
		Vector3 startTangent2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
		_ = endTangent.x;
		_ = startTangent.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [end @ r9 (Shapes.PolylinePoint)+10]");
		_ = 0;
		_ = startTangent.z;
		BezierTo(startTangent2, endTangent2, end2, num);
	}

	public unsafe void BezierTo(Vector3 startTangent, Vector3 endTangent, PolylinePoint end, int pointCount)
	{
		//IL_0008: Expected O, but got Ref
		//IL_004c: Expected O, but got Ref
		//IL_0088: Expected O, but got Ref
		//IL_0088: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		if (!CheckCanAddContinuePoint("BezierTo"))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808D6AE0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [end @ r9 (Shapes.PolylinePoint)+10]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808D6AE0");
			PolylinePoint end2 = (PolylinePoint)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 96));
			_ = startTangent.x;
			_ = end.point;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [end @ r9 (Shapes.PolylinePoint)+10]");
			_ = 0;
			object obj3 = default(object);
			Vector3 vector = default(Vector3);
			int pointCount2 = default(int);
			BezierTo((PolylinePoint)(&obj3), (PolylinePoint)(&vector), end2, pointCount2);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void BezierTo(PolylinePoint startTangent, PolylinePoint endTangent, PolylinePoint end)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_00bf: Expected O, but got Ref
		//IL_00bf: Expected O, but got Ref
		//IL_00bf: Expected O, but got Ref
		//IL_00bf: Expected O, but got Ref
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Expected O, but got Unknown
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Expected O, but got Unknown
		//IL_019c: Expected O, but got Ref
		object obj2 = default(object);
		object obj = obj2 - 24;
		ShapesConfig instance = ShapesConfig.Instance;
		if (!CheckCanAddContinuePoint("BezierTo"))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808D6AE0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [startTangent @ rdx (Shapes.PolylinePoint)+10]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [endTangent @ r8 (Shapes.PolylinePoint)+10]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [end @ r9 (Shapes.PolylinePoint)+10]");
			_ = 0;
			ShapesConfig instance2 = ShapesConfig.Instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm8,8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm7,8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm6,8\"");
			object obj3 = default(object);
			object obj4 = default(object);
			object obj5 = default(object);
			object obj6 = default(object);
			int num = default(int);
			float approximateAngularCurveSumDegrees = ShapesMath.GetApproximateAngularCurveSumDegrees((Vector3)(&obj3), (Vector3)(&obj4), (Vector3)(&obj5), (Vector3)(&obj6), num);
			float num2 = approximateAngularCurveSumDegrees / 360f;
			float num3 = num2 * instance.polylineDefaultPointsPerTurn;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
			object obj7 = default(object);
			if ((nint)obj7 >= 2)
			{
				PolylinePoint endTangent2 = (PolylinePoint)(obj - 80);
				PolylinePoint startTangent2 = (PolylinePoint)(obj - 112);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [end @ r9 (Shapes.PolylinePoint)+10]");
				_ = 0;
				_ = endTangent.point;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [endTangent @ r8 (Shapes.PolylinePoint)+10]");
				_ = 0;
				_ = startTangent.point;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [startTangent @ rdx (Shapes.PolylinePoint)+10]");
				_ = 0;
				object obj8 = default(object);
				BezierTo(startTangent2, endTangent2, (PolylinePoint)(&obj8), num);
			}
		}
	}

	public unsafe void BezierTo(PolylinePoint startTangent, PolylinePoint endTangent, PolylinePoint end, float pointsPerTurn)
	{
		//IL_0008: Expected O, but got Ref
		//IL_001b: Expected O, but got Ref
		//IL_0068: Expected O, but got Ref
		//IL_0083: Expected O, but got Ref
		//IL_0091: Expected O, but got Ref
		//IL_00fd: Expected O, but got Ref
		//IL_017e: Expected O, but got Ref
		//IL_018c: Expected O, but got Ref
		//IL_01a4: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		object obj4 = default(object);
		int num = default(int);
		object obj5 = default(object);
		do
		{
			if (!CheckCanAddContinuePoint("BezierTo"))
			{
				object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808D6AE0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [startTangent @ rdx (Shapes.PolylinePoint)+10]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [endTangent @ r8 (Shapes.PolylinePoint)+10]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [end @ r9 (Shapes.PolylinePoint)+10]");
				_ = 0;
				ShapesConfig instance = ShapesConfig.Instance;
				Vector3 c = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 121));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-41]");
				_ = 0;
				Vector3 b = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
				Vector3 a = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
				_ = endTangent.point;
				_ = startTangent.point;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm8,8\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm7,8\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm6,8\"");
				_ = endTangent.point;
				_ = startTangent.point;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-49]");
				_ = 0;
				float approximateAngularCurveSumDegrees = ShapesMath.GetApproximateAngularCurveSumDegrees(a, b, c, (Vector3)(&obj4), num);
				float num2 = approximateAngularCurveSumDegrees / 360f;
				float num3 = num2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+77]");
				float num4 = num3 * 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
				continue;
			}
			return;
		}
		while ((nint)obj5 < 2);
		PolylinePoint end2 = (PolylinePoint)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
		PolylinePoint endTangent2 = (PolylinePoint)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
		_ = end.point;
		PolylinePoint startTangent2 = (PolylinePoint)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [end @ r9 (Shapes.PolylinePoint)+10]");
		_ = 0;
		_ = endTangent.point;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [endTangent @ r8 (Shapes.PolylinePoint)+10]");
		_ = 0;
		_ = startTangent.point;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [startTangent @ rdx (Shapes.PolylinePoint)+10]");
		_ = 0;
		BezierTo(startTangent2, endTangent2, end2, num);
	}

	public void BezierTo(PolylinePoint startTangent, PolylinePoint endTangent, PolylinePoint end, int pointCount)
	{
		//IL_00eb: Expected I4, but got I8
		if (!CheckCanAddContinuePoint("BezierTo"))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808D6AE0");
			ShapesMath._003CCubicBezierPointsSkipFirst_003Ed__38 obj = new ShapesMath._003CCubicBezierPointsSkipFirst_003Ed__38(0);
			obj._003C_003E1__state = -2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj._003C_003El__initialThreadId = num;
			PolylinePoint polylinePoint = default(PolylinePoint);
			obj._003C_003E3__a = polylinePoint;
			int num2 = default(int);
			obj._003C_003E3__count = num2;
			obj._003C_003E3__b = (PolylinePoint)startTangent.point;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [startTangent @ rdx (Shapes.PolylinePoint)+10]");
			_ = 0;
			obj._003C_003E3__c = (PolylinePoint)endTangent.point;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [endTangent @ r8 (Shapes.PolylinePoint)+10]");
			_ = 0;
			obj._003C_003E3__d = (PolylinePoint)end.point;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [end @ r9 (Shapes.PolylinePoint)+10]");
			_ = 0;
			AddPoints(obj);
		}
	}

	private unsafe static int CalcBezierPointCount(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float pointsPerTurn)
	{
		//IL_00ae: Expected I4, but got O
		//IL_0040: Expected O, but got Ref
		//IL_0040: Expected O, but got Ref
		//IL_0040: Expected O, but got Ref
		//IL_0040: Expected O, but got Ref
		ShapesConfig instance = ShapesConfig.Instance;
		if ((object)instance != null)
		{
			object obj = default(object);
			object obj2 = default(object);
			object obj3 = default(object);
			float num = default(float);
			int vertCount = default(int);
			float approximateAngularCurveSumDegrees = ShapesMath.GetApproximateAngularCurveSumDegrees((Vector3)(&obj), (Vector3)(&obj2), (Vector3)(&obj3), (Vector3)(&num), vertCount);
			float num2 = approximateAngularCurveSumDegrees / 360f;
			object obj4 = default(object);
			float num3 = num2 * (float)obj4;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
			int num4 = default(int);
			if (num4 < 2)
			{
				return 2;
			}
			return num4;
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void ArcTo(Vector3 corner, Vector3 next, float radius, int pointCount)
	{
		//IL_001e: Expected O, but got Ref
		//IL_001e: Expected O, but got Ref
		object obj = default(object);
		object obj2 = default(object);
		bool useDensity = default(bool);
		int targetPointCount = default(int);
		float pointsPerTurn = default(float);
		AddArcPoints((Vector3)(&obj), (Vector3)(&obj2), radius, useDensity, targetPointCount, pointsPerTurn);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void ArcTo(Vector3 corner, PolylinePoint next, float radius, int pointCount)
	{
		//IL_001e: Expected O, but got Ref
		//IL_001e: Expected O, but got Ref
		object obj = default(object);
		object obj2 = default(object);
		bool useDensity = default(bool);
		int targetPointCount = default(int);
		float pointsPerTurn = default(float);
		AddArcPoints((Vector3)(&obj), (PolylinePoint)(&obj2), radius, useDensity, targetPointCount, pointsPerTurn);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void ArcTo(Vector3 corner, Vector3 next, float radius)
	{
		//IL_002d: Expected O, but got Ref
		//IL_002d: Expected O, but got Ref
		ShapesConfig instance = ShapesConfig.Instance;
		object obj = default(object);
		float num = default(float);
		bool useDensity = default(bool);
		int targetPointCount = default(int);
		float pointsPerTurn = default(float);
		AddArcPoints((Vector3)(&obj), (Vector3)(&num), radius, useDensity, targetPointCount, pointsPerTurn);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void ArcTo(Vector3 corner, PolylinePoint next, float radius)
	{
		//IL_002c: Expected O, but got Ref
		//IL_002c: Expected O, but got Ref
		ShapesConfig instance = ShapesConfig.Instance;
		object obj = default(object);
		object obj2 = default(object);
		bool useDensity = default(bool);
		int targetPointCount = default(int);
		float pointsPerTurn = default(float);
		AddArcPoints((Vector3)(&obj), (PolylinePoint)(&obj2), radius, useDensity, targetPointCount, pointsPerTurn);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void ArcTo(Vector3 corner, Vector3 next, float radius, float pointsPerTurn)
	{
		//IL_001e: Expected O, but got Ref
		//IL_001e: Expected O, but got Ref
		object obj = default(object);
		object obj2 = default(object);
		bool useDensity = default(bool);
		int targetPointCount = default(int);
		float pointsPerTurn2 = default(float);
		AddArcPoints((Vector3)(&obj), (Vector3)(&obj2), radius, useDensity, targetPointCount, pointsPerTurn2);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void ArcTo(Vector3 corner, PolylinePoint next, float radius, float pointsPerTurn)
	{
		//IL_001e: Expected O, but got Ref
		//IL_001e: Expected O, but got Ref
		object obj = default(object);
		object obj2 = default(object);
		bool useDensity = default(bool);
		int targetPointCount = default(int);
		float pointsPerTurn2 = default(float);
		AddArcPoints((Vector3)(&obj), (PolylinePoint)(&obj2), radius, useDensity, targetPointCount, pointsPerTurn2);
	}

	private unsafe void AddArcPoints(Vector3 corner, Vector3 next, float radius, bool useDensity, int targetPointCount, float pointsPerTurn)
	{
		//IL_002d: Expected O, but got Ref
		//IL_002d: Expected O, but got Ref
		if (!CheckCanAddContinuePoint("AddArcPoints"))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808D6AE0");
			object obj = default(object);
			object obj2 = default(object);
			bool useDensity2 = default(bool);
			int targetPointCount2 = default(int);
			float pointsPerTurn2 = default(float);
			AddArcPoints((Vector3)(&obj), (PolylinePoint)(&obj2), radius, useDensity2, targetPointCount2, pointsPerTurn2);
		}
	}

	private unsafe void AddArcPoints(Vector3 corner, PolylinePoint next, float radius, bool useDensity, int targetPointCount, float pointsPerTurn)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Expected O, but got Unknown
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Expected O, but got Unknown
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Expected O, but got Unknown
		//IL_0154: Invalid comparison between F4 and O
		//IL_04d6: Invalid comparison between I4 and F4
		//IL_0521: Expected F4, but got I4
		//IL_06cc: Invalid comparison between I4 and F4
		//IL_0281: Invalid comparison between F4 and I
		//IL_055d: Expected F4, but got I4
		//IL_06fb: Invalid comparison between I4 and F4
		//IL_02b0: Expected F4, but got I
		//IL_0599: Expected F4, but got I4
		//IL_06a0: Expected O, but got F4
		//IL_06ae: Expected O, but got F4
		//IL_07ba: Invalid comparison between I4 and F4
		//IL_0848: Expected I4, but got I8
		//IL_07dc: Expected O, but got Ref
		//IL_0806: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		if (CheckCanAddContinuePoint("AddArcPoints"))
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808D6AE0");
		Vector3 vector = default(Vector3);
		Vector3 normalized = vector.normalized;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [next @ r8 (Shapes.PolylinePoint)+10]");
		_ = 0;
		Vector3 normalized2 = vector.normalized;
		Vector3 vector2 = default(Vector3);
		float num = normalized2.z * (float)vector2;
		float num2 = (float)vector2 * normalized.z;
		float num3 = normalized2.x * normalized.z;
		float num4 = num - num2;
		float num5 = (float)vector2 * normalized.x;
		float num6 = normalized2.z * normalized.x;
		float num7 = normalized2.x * (float)vector2;
		float num8 = num3 - num6;
		float num9 = num5 - num7;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj3 = num8 & 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj4 = num4 & 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj5 = num9 & 0;
		object obj6 = obj3 + obj4;
		object obj7 = obj6 + obj5;
		PolylinePoint polylinePoint = default(PolylinePoint);
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.001f) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7))
		{
			Vector3 vector3 = default(Vector3);
			Vector3 normalized3 = vector3.normalized;
			float num10 = (float)vector2 * normalized.x;
			float num11 = normalized3.x * (float)vector2;
			float num12 = num11 - num10;
			object obj8 = default(object);
			float num13 = normalized3.z * (float)obj8;
			float num14 = normalized3.z * normalized2.x;
			float num15 = (float)vector2 * normalized2.x;
			float num16 = (float)vector2 * normalized2.z;
			float num17 = num16 - num13;
			float num18 = normalized3.x * (float)obj8;
			float num19 = normalized3.x * normalized2.z;
			float num20 = num18 - num15;
			float num21 = num14 - num19;
			Vector3 normalized4 = vector.normalized;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC0]");
			bool flag = !(radius < 0f);
			float num22 = radius;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206BC0]");
				num22 = 0f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+B0]");
			bool flag2 = (nint)0 == 0;
			float num23 = num17 * normalized4.x;
			float num24 = num21 * (float)vector2;
			float num25 = num24 + num23;
			float num26 = num20 * normalized4.z;
			float num27 = num25 + num26;
			float num28 = num22 / num27;
			float num29 = num28 * normalized4.z;
			float num30 = num29 + corner.z;
			int num34;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180ABFA60");
				float num31 = (float)vector2 / 360f;
				float num32 = num31;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+C0]");
				float num33 = num32 * 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
				int num35 = default(int);
				num34 = num35;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+B8]");
				num34 = 0;
			}
			object obj9 = num20 ^ -0f;
			object obj10 = num12 ^ -0f;
			ShapesMath._003CGetArcPoints_003Ed__35 obj11 = new ShapesMath._003CGetArcPoints_003Ed__35(0);
			obj11._003C_003E1__state = -2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num36 = default(int);
			obj11._003C_003El__initialThreadId = num36;
			obj11._003C_003E3__count = num34;
			obj11._003C_003E3__a = polylinePoint;
			obj11._003C_003E3__b = (PolylinePoint)next.point;
			obj11._003C_003E3__normA = vector2;
			obj11._003C_003E3__normB = vector2;
			obj11._003C_003E3__center = vector2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [next @ r8 (Shapes.PolylinePoint)+10]");
			_ = 0;
			obj11._003C_003E3__radius = num22;
			AddPoints(obj11);
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [next @ r8 (Shapes.PolylinePoint)+10]");
		_ = 0;
		object obj13 = default(object);
		object obj12 = (object)vector2 - obj13;
		float num37 = corner.x - (float)polylinePoint;
		object obj14 = (object)vector2 - obj13;
		object obj15 = (object)next.point - (object)polylinePoint;
		object obj17 = default(object);
		object obj16 = (object)vector2 - obj17;
		object obj18 = obj14 * obj12;
		float num38 = num37 * (float)obj15;
		object obj19 = obj12 * obj12;
		float num39 = (float)obj18 + num38;
		object obj20 = obj15 * obj15;
		float num40 = corner.z - (float)obj17;
		object obj21 = obj19 + obj20;
		float num41 = num40 * (float)obj16;
		object obj22 = obj16 * obj16;
		float num42 = num39 + num41;
		object obj23 = obj21 + obj22;
		float num43 = num42 / (float)obj23;
		float num44 = num43 - 0.0001f;
		if (!(0f > num44))
		{
			if (num44 > 1f)
			{
				num44 = 1f;
			}
		}
		else
		{
			num44 = 0f;
		}
		float num45 = num43 + 0.0001f;
		if (!(0f > num45))
		{
			if (num45 > 1f)
			{
				num45 = 1f;
			}
		}
		else
		{
			num45 = 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [next @ r8 (Shapes.PolylinePoint)+10]");
		_ = 0;
		if (!(0f > num44))
		{
			if (num44 > 1f)
			{
				num44 = 1f;
			}
		}
		else
		{
			num44 = 0f;
		}
		object obj24 = (object)next.point - (object)polylinePoint;
		object obj25 = (object)vector2 - obj13;
		object obj26 = (object)vector2 - obj17;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [next @ r8 (Shapes.PolylinePoint)+10]");
		_ = 0;
		float num46 = (float)obj24 * num44;
		float num47 = (float)obj26 * num44;
		float num48 = (float)obj25 * num44;
		float num49 = num46 + (float)polylinePoint;
		float num50 = num47 + (float)obj17;
		float num51 = num48 + (float)obj13;
		if (0f > num45 || num45 > 1f)
		{
		}
		PolylinePoint p = (PolylinePoint)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 112));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-70]");
		_ = 0;
		AddPoint(p);
		PolylinePoint p2 = (PolylinePoint)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 80));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [next @ r8 (Shapes.PolylinePoint)+10]");
		_ = 0;
		AddPoint(p2);
	}

	public bool EnsureMeshIsReadyToRender(bool closed, PolylineJoins renderJoins, out Mesh outMesh)
	{
		//IL_00ae: Expected I4, but got O
		_003C_003Ec__DisplayClass49_0 CS_0024_003C_003E8__locals9 = new _003C_003Ec__DisplayClass49_0();
		if (CS_0024_003C_003E8__locals9 != null)
		{
			CS_0024_003C_003E8__locals9._003C_003E4__this = this;
			CS_0024_003C_003E8__locals9.closed = closed;
			CS_0024_003C_003E8__locals9.renderJoins = renderJoins;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.PolylinePath)+18]");
			if ((nint)0 == 0 && (renderJoins != lastUsedJoins || closed != lastUsedClosed))
			{
				_ = 1;
			}
			Action updateMesh = delegate
			{
				//IL_0045: Expected O, but got I
				//IL_0045: Expected O, but got I
				PolylinePath polylinePath = CS_0024_003C_003E8__locals9._003C_003E4__this;
				polylinePath.lastUsedClosed = CS_0024_003C_003E8__locals9.closed;
				polylinePath.lastUsedJoins = CS_0024_003C_003E8__locals9.renderJoins;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbx_v1 (Shapes.PolylinePath)+10]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbx_v1 (Shapes.PolylinePath)+28]");
				bool flattenZ = default(bool);
				bool useColors = default(bool);
				ShapesMeshGen.GenPolylineMesh((Mesh)num, (IList<PolylinePoint>)0, CS_0024_003C_003E8__locals9.closed, CS_0024_003C_003E8__locals9.renderJoins, flattenZ, useColors);
			};
			return EnsureMeshIsReadyToRender(out outMesh, updateMesh);
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private void TryUpdateMesh(bool closed, PolylineJoins joins)
	{
		//IL_0032: Expected O, but got I
		//IL_0032: Expected O, but got I
		lastUsedClosed = closed;
		lastUsedJoins = joins;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.PolylinePath)+10]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.PolylinePath)+28]");
		bool flattenZ = default(bool);
		bool useColors = default(bool);
		ShapesMeshGen.GenPolylineMesh((Mesh)num, (IList<PolylinePoint>)0, closed, joins, flattenZ, useColors);
	}

	public unsafe void ArcTo(Vector3 corner, Vector3 next, float radius, int pointCount, Color color)
	{
		//IL_001e: Expected O, but got Ref
		//IL_001e: Expected O, but got Ref
		object obj = default(object);
		object obj2 = default(object);
		bool useDensity = default(bool);
		int targetPointCount = default(int);
		float pointsPerTurn = default(float);
		AddArcPoints((Vector3)(&obj), (Vector3)(&obj2), radius, useDensity, targetPointCount, pointsPerTurn);
	}

	public unsafe void ArcTo(Vector3 corner, Vector3 next, float radius, Color color)
	{
		//IL_002d: Expected O, but got Ref
		//IL_002d: Expected O, but got Ref
		ShapesConfig instance = ShapesConfig.Instance;
		object obj = default(object);
		float num = default(float);
		bool useDensity = default(bool);
		int targetPointCount = default(int);
		float pointsPerTurn = default(float);
		AddArcPoints((Vector3)(&obj), (Vector3)(&num), radius, useDensity, targetPointCount, pointsPerTurn);
	}

	public unsafe void ArcTo(Vector3 corner, Vector3 next, float radius, float pointsPerTurn, Color color)
	{
		//IL_001e: Expected O, but got Ref
		//IL_001e: Expected O, but got Ref
		object obj = default(object);
		object obj2 = default(object);
		bool useDensity = default(bool);
		int targetPointCount = default(int);
		float pointsPerTurn2 = default(float);
		AddArcPoints((Vector3)(&obj), (Vector3)(&obj2), radius, useDensity, targetPointCount, pointsPerTurn2);
	}

	public void BezierTo(Vector3 startTangent, Vector3 endTangent, Vector3 end, float pointsPerTurn, Color color)
	{
	}

	public void BezierTo(Vector3 startTangent, Vector3 endTangent, Vector3 end, int pointCount, Color color)
	{
	}

	public void BezierTo(Vector3 startTangent, Vector3 endTangent, Vector3 end, Color color)
	{
	}
}
