using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace Shapes;

public class Polyline : ShapeRenderer
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<Vector3, PolylinePoint> _003C_003E9__29_0;

		public static Func<Vector3, Color, PolylinePoint> _003C_003E9__29_1;

		public static Func<Vector2, PolylinePoint> _003C_003E9__30_0;

		public static Func<Vector2, Color, PolylinePoint> _003C_003E9__30_1;

		public static Func<PolylinePoint, Vector3> _003C_003E9__49_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal unsafe PolylinePoint _003CSetPoints_003Eb__29_0(Vector3 p)
		{
			//IL_000e: Expected O, but got I4
			//IL_0009: Expected native int or pointer, but got O
			//IL_0025: Expected O, but got Ref
			//IL_0025: Expected O, but got Ref
			//IL_0020: Expected native int or pointer, but got O
			PolylinePoint polylinePoint = default(PolylinePoint);
			((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)0;
			_ = 0;
			object obj = default(object);
			object obj2 = default(object);
			*(PolylinePoint*)(nint)polylinePoint = new PolylinePoint((Vector3)(&obj), (Color)(&obj2));
			return polylinePoint;
		}

		internal unsafe PolylinePoint _003CSetPoints_003Eb__29_1(Vector3 p, Color c)
		{
			//IL_000e: Expected O, but got I4
			//IL_0009: Expected native int or pointer, but got O
			//IL_0025: Expected O, but got Ref
			//IL_0025: Expected O, but got Ref
			//IL_0020: Expected native int or pointer, but got O
			PolylinePoint polylinePoint = default(PolylinePoint);
			((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)0;
			_ = 0;
			object obj = default(object);
			object obj2 = default(object);
			*(PolylinePoint*)(nint)polylinePoint = new PolylinePoint((Vector3)(&obj), (Color)(&obj2));
			return polylinePoint;
		}

		internal unsafe PolylinePoint _003CSetPoints_003Eb__30_0(Vector2 p)
		{
			//IL_000f: Expected O, but got I4
			//IL_000a: Expected native int or pointer, but got O
			//IL_0026: Expected O, but got Ref
			//IL_0021: Expected native int or pointer, but got O
			PolylinePoint polylinePoint = default(PolylinePoint);
			((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)0;
			_ = 0;
			object obj = default(object);
			*(PolylinePoint*)(nint)polylinePoint = new PolylinePoint(p, (Color)(&obj));
			return polylinePoint;
		}

		internal unsafe PolylinePoint _003CSetPoints_003Eb__30_1(Vector2 p, Color c)
		{
			//IL_000e: Expected O, but got I4
			//IL_0009: Expected native int or pointer, but got O
			//IL_0025: Expected O, but got Ref
			//IL_0020: Expected native int or pointer, but got O
			PolylinePoint polylinePoint = default(PolylinePoint);
			((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)0;
			_ = 0;
			object obj = default(object);
			*(PolylinePoint*)(nint)polylinePoint = new PolylinePoint(p, (Color)(&obj));
			return polylinePoint;
		}

		internal unsafe Vector3 _003CGetUnpaddedLocalBounds_Internal_003Eb__49_0(PolylinePoint p)
		{
			//IL_0012: Expected F4, but got O
			//IL_000d: Expected native int or pointer, but got O
			//IL_0027: Expected F4, but got I
			//IL_0022: Expected native int or pointer, but got O
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = (float)p.point;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [p @ r8 (Shapes.PolylinePoint)+8]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
	}

	public List<PolylinePoint> points;

	private PolylineGeometry geometry;

	private PolylineJoins joins;

	private bool closed;

	private float thickness;

	private ThicknessSpace thicknessSpace;

	public PolylineGeometry Geometry
	{
		get
		{
			return geometry;
		}
		set
		{
			geometry = value;
			SetIntNow(ShapesMaterialUtils.propAlignment, (int)geometry);
			UpdateMaterial();
			ApplyProperties();
		}
	}

	public PolylineJoins Joins
	{
		get
		{
			return joins;
		}
		set
		{
			joins = value;
			meshOutOfDate = true;
			UpdateMaterial();
		}
	}

	public bool Closed
	{
		get
		{
			return closed;
		}
		set
		{
			closed = value;
			meshOutOfDate = true;
		}
	}

	public float Thickness
	{
		get
		{
			return thickness;
		}
		set
		{
			thickness = value;
			SetFloatNow(ShapesMaterialUtils.propThickness, value);
		}
	}

	public ThicknessSpace ThicknessSpace
	{
		get
		{
			return thicknessSpace;
		}
		set
		{
			thicknessSpace = value;
			SetIntNow(ShapesMaterialUtils.propThicknessSpace, (int)value);
		}
	}

	public int Count
	{
		get
		{
			//IL_0020: Expected I4, but got O
			List<PolylinePoint> list = points;
			if (points != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (System.Collections.Generic.List`1<Shapes.PolylinePoint>)+18]");
				return 0;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	// C# has no syntax for parameterized property 'Item'.
	public unsafe PolylinePoint get_Item(int i)
	{
		//IL_0017: Expected native int or pointer, but got O
		if (points != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			PolylinePoint polylinePoint = default(PolylinePoint);
			Vector3 point = default(Vector3);
			((PolylinePoint*)(nint)polylinePoint)->point = point;
			return polylinePoint;
		}
		return (PolylinePoint)new NullReferenceException();
	}

	public unsafe void set_Item(int i, PolylinePoint value)
	{
		//IL_0018: Expected O, but got Ref
		object obj = default(object);
		points.set_Item(i, (PolylinePoint)(&obj));
		meshOutOfDate = true;
	}

	private protected override bool UseCamOnPreCull => true;

	private protected override MeshUpdateMode MeshUpdateMode => MeshUpdateMode.SelfGenerated;

	private protected override int MaterialCount
	{
		get
		{
			bool flag = PolylineJoinsExtensions.HasJoinMesh(joins);
			bool flag2 = !flag;
			bool flag3 = !flag2;
			return (flag3 ? 1 : 0) + 1;
		}
	}

	public unsafe void SetPointPosition(int index, Vector3 position)
	{
		//IL_0053: Expected O, but got Ref
		if (index >= 0)
		{
			List<PolylinePoint> list = points;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rax_v13 (System.Collections.Generic.List`1<Shapes.PolylinePoint>)+18]");
			if ((nint)index < (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				object obj = default(object);
				points.set_Item(index, (PolylinePoint)(&obj));
				meshOutOfDate = true;
				return;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		IndexOutOfRangeException ex = new IndexOutOfRangeException();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public unsafe void SetPointColor(int index, Color color)
	{
		//IL_0053: Expected O, but got Ref
		if (index >= 0)
		{
			List<PolylinePoint> list = points;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rax_v13 (System.Collections.Generic.List`1<Shapes.PolylinePoint>)+18]");
			if ((nint)index < (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				object obj = default(object);
				points.set_Item(index, (PolylinePoint)(&obj));
				meshOutOfDate = true;
				return;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		IndexOutOfRangeException ex = new IndexOutOfRangeException();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public unsafe void SetPointThickness(int index, float thickness)
	{
		//IL_0053: Expected O, but got Ref
		if (index >= 0)
		{
			List<PolylinePoint> list = points;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rax_v13 (System.Collections.Generic.List`1<Shapes.PolylinePoint>)+18]");
			if ((nint)index < (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				object obj = default(object);
				points.set_Item(index, (PolylinePoint)(&obj));
				meshOutOfDate = true;
				return;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		IndexOutOfRangeException ex = new IndexOutOfRangeException();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public unsafe void SetPoints(IReadOnlyCollection<Vector3> points, IReadOnlyCollection<Color> colors = null)
	{
		//IL_0095: Expected O, but got I
		//IL_00c5: Expected O, but got I
		//IL_00cf: Expected I, but got O
		//IL_00df: Expected O, but got I
		//IL_032d: Expected O, but got I
		//IL_0309: Expected I, but got O
		List<PolylinePoint> list = this.points;
		bool flag = this.points == null;
		IReadOnlyCollection<Vector3> readOnlyCollection = points;
		IReadOnlyCollection<Color> readOnlyCollection2 = colors;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rbx_v1 (System.Collections.Generic.List`1<Shapes.PolylinePoint>)+1C]");
			_ = (nint)0 + (nint)1;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
			object obj = default(object);
			if (obj == null)
			{
				_ = 0;
				readOnlyCollection = points;
				readOnlyCollection2 = colors;
			}
			else
			{
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rbx_v1 (System.Collections.Generic.List`1<Shapes.PolylinePoint>)+18]");
				bool flag2 = (nint)0 <= (nint)0;
				readOnlyCollection = points;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rbx_v1 (System.Collections.Generic.List`1<Shapes.PolylinePoint>)+18]");
				readOnlyCollection2 = (IReadOnlyCollection<Color>)0;
				if (!flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rbx_v1 (System.Collections.Generic.List`1<Shapes.PolylinePoint>)+10]");
					nint num = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rbx_v1 (System.Collections.Generic.List`1<Shapes.PolylinePoint>)+18]");
					Array.Clear((Array)num, 0, 0);
					readOnlyCollection = null;
					nint num2 = unchecked((nint)null);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rbx_v1 (System.Collections.Generic.List`1<Shapes.PolylinePoint>)+18]");
					readOnlyCollection2 = (IReadOnlyCollection<Color>)0;
				}
			}
			if (colors != null)
			{
				if (points != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					object obj2 = default(object);
					object obj3 = default(object);
					bool flag3 = obj2 != obj3;
					readOnlyCollection = (IReadOnlyCollection<Vector3>)typeof(IReadOnlyCollection<Color>);
					readOnlyCollection2 = colors;
					if (!flag3)
					{
						Func<Vector3, Color, PolylinePoint> resultSelector = _003C_003Ec._003C_003E9__29_1;
						if (_003C_003Ec._003C_003E9__29_1 == null)
						{
							resultSelector = (_003C_003Ec._003C_003E9__29_1 = delegate
							{
								//IL_000e: Expected O, but got I4
								//IL_0009: Expected native int or pointer, but got O
								//IL_0025: Expected O, but got Ref
								//IL_0025: Expected O, but got Ref
								//IL_0020: Expected native int or pointer, but got O
								PolylinePoint polylinePoint = default(PolylinePoint);
								((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)0;
								_ = 0;
								object obj4 = default(object);
								object obj5 = default(object);
								*(PolylinePoint*)(nint)polylinePoint = new PolylinePoint((Vector3)(&obj4), (Color)(&obj5));
								return polylinePoint;
							});
						}
						IEnumerable<PolylinePoint> enumerable = Enumerable.Zip(points, colors, resultSelector);
						AddPoints(enumerable);
						return;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					ArgumentException ex = new ArgumentException("point.Count != color.Count");
					ex._002Ector("point.Count != color.Count");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					throw ex;
				}
			}
			else
			{
				Func<Vector3, PolylinePoint> func = _003C_003Ec._003C_003E9__29_0;
				if (_003C_003Ec._003C_003E9__29_0 == null)
				{
					func = (_003C_003Ec._003C_003E9__29_0 = delegate
					{
						//IL_000e: Expected O, but got I4
						//IL_0009: Expected native int or pointer, but got O
						//IL_0025: Expected O, but got Ref
						//IL_0025: Expected O, but got Ref
						//IL_0020: Expected native int or pointer, but got O
						PolylinePoint polylinePoint = default(PolylinePoint);
						((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)0;
						_ = 0;
						object obj4 = default(object);
						object obj5 = default(object);
						*(PolylinePoint*)(nint)polylinePoint = new PolylinePoint((Vector3)(&obj4), (Color)(&obj5));
						return polylinePoint;
					});
					nint num2 = unchecked((nint)null);
				}
				IEnumerable<PolylinePoint> collection = Enumerable.Select(points, func);
				bool flag4 = this.points == null;
				readOnlyCollection = (IReadOnlyCollection<Vector3>)(object)func;
				readOnlyCollection2 = (IReadOnlyCollection<Color>)0;
				if (!flag4)
				{
					this.points.AddRange(collection);
					meshOutOfDate = true;
					return;
				}
			}
		}
		throw new NullReferenceException();
	}

	public unsafe void SetPoints(IReadOnlyCollection<Vector2> points, IReadOnlyCollection<Color> colors = null)
	{
		//IL_0095: Expected O, but got I
		//IL_00c5: Expected O, but got I
		//IL_00cf: Expected I, but got O
		//IL_00df: Expected O, but got I
		//IL_0338: Expected O, but got I
		//IL_0314: Expected I, but got O
		List<PolylinePoint> list = this.points;
		meshOutOfDate = true;
		bool flag = this.points == null;
		IReadOnlyCollection<Vector2> readOnlyCollection = points;
		IReadOnlyCollection<Color> readOnlyCollection2 = colors;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rbx_v1 (System.Collections.Generic.List`1<Shapes.PolylinePoint>)+1C]");
			_ = (nint)0 + (nint)1;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
			object obj = default(object);
			if (obj == null)
			{
				_ = 0;
				readOnlyCollection = points;
				readOnlyCollection2 = colors;
			}
			else
			{
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rbx_v1 (System.Collections.Generic.List`1<Shapes.PolylinePoint>)+18]");
				bool flag2 = (nint)0 <= (nint)0;
				readOnlyCollection = points;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rbx_v1 (System.Collections.Generic.List`1<Shapes.PolylinePoint>)+18]");
				readOnlyCollection2 = (IReadOnlyCollection<Color>)0;
				if (!flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rbx_v1 (System.Collections.Generic.List`1<Shapes.PolylinePoint>)+10]");
					nint num = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rbx_v1 (System.Collections.Generic.List`1<Shapes.PolylinePoint>)+18]");
					Array.Clear((Array)num, 0, 0);
					readOnlyCollection = null;
					nint num2 = unchecked((nint)null);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rbx_v1 (System.Collections.Generic.List`1<Shapes.PolylinePoint>)+18]");
					readOnlyCollection2 = (IReadOnlyCollection<Color>)0;
				}
			}
			if (colors != null)
			{
				if (points != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					object obj2 = default(object);
					object obj3 = default(object);
					bool flag3 = obj2 != obj3;
					readOnlyCollection = (IReadOnlyCollection<Vector2>)typeof(IReadOnlyCollection<Color>);
					readOnlyCollection2 = colors;
					if (!flag3)
					{
						Func<Vector2, Color, PolylinePoint> resultSelector = _003C_003Ec._003C_003E9__30_1;
						if (_003C_003Ec._003C_003E9__30_1 == null)
						{
							resultSelector = (_003C_003Ec._003C_003E9__30_1 = delegate(Vector2 p, Color c)
							{
								//IL_000e: Expected O, but got I4
								//IL_0009: Expected native int or pointer, but got O
								//IL_0025: Expected O, but got Ref
								//IL_0020: Expected native int or pointer, but got O
								PolylinePoint polylinePoint = default(PolylinePoint);
								((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)0;
								_ = 0;
								object obj4 = default(object);
								*(PolylinePoint*)(nint)polylinePoint = new PolylinePoint(p, (Color)(&obj4));
								return polylinePoint;
							});
						}
						IEnumerable<PolylinePoint> enumerable = Enumerable.Zip(points, colors, resultSelector);
						AddPoints(enumerable);
						return;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					ArgumentException ex = new ArgumentException("point.Count != color.Count");
					ex._002Ector("point.Count != color.Count");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					throw ex;
				}
			}
			else
			{
				Func<Vector2, PolylinePoint> func = _003C_003Ec._003C_003E9__30_0;
				if (_003C_003Ec._003C_003E9__30_0 == null)
				{
					func = (_003C_003Ec._003C_003E9__30_0 = delegate(Vector2 p)
					{
						//IL_000f: Expected O, but got I4
						//IL_000a: Expected native int or pointer, but got O
						//IL_0026: Expected O, but got Ref
						//IL_0021: Expected native int or pointer, but got O
						PolylinePoint polylinePoint = default(PolylinePoint);
						((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)0;
						_ = 0;
						object obj4 = default(object);
						*(PolylinePoint*)(nint)polylinePoint = new PolylinePoint(p, (Color)(&obj4));
						return polylinePoint;
					});
					nint num2 = unchecked((nint)null);
				}
				IEnumerable<PolylinePoint> collection = Enumerable.Select(points, func);
				bool flag4 = this.points == null;
				readOnlyCollection = (IReadOnlyCollection<Vector2>)(object)func;
				readOnlyCollection2 = (IReadOnlyCollection<Color>)0;
				if (!flag4)
				{
					this.points.AddRange(collection);
					meshOutOfDate = true;
					return;
				}
			}
		}
		throw new NullReferenceException();
	}

	public void SetPoints(IEnumerable<PolylinePoint> points)
	{
		//IL_009d: Expected O, but got I
		List<PolylinePoint> list = this.points;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (System.Collections.Generic.List`1<Shapes.PolylinePoint>)+1C]");
		_ = (nint)0 + (nint)1;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			_ = 0;
		}
		else
		{
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (System.Collections.Generic.List`1<Shapes.PolylinePoint>)+18]");
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (System.Collections.Generic.List`1<Shapes.PolylinePoint>)+10]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (System.Collections.Generic.List`1<Shapes.PolylinePoint>)+18]");
				Array.Clear((Array)num, 0, 0);
			}
		}
		this.points.AddRange(points);
		meshOutOfDate = true;
	}

	public void AddPoints(IEnumerable<PolylinePoint> points)
	{
		this.points.AddRange(points);
		meshOutOfDate = true;
	}

	public unsafe void AddPoint(Vector3 position)
	{
		//IL_000d: Expected O, but got Ref
		//IL_0021: Expected O, but got Ref
		object obj = default(object);
		PolylinePoint polylinePoint = new PolylinePoint((Vector3)(&obj));
		object obj2 = default(object);
		points.Add((PolylinePoint)(&obj2));
		meshOutOfDate = true;
	}

	public unsafe void AddPoint(Vector3 position, Color color)
	{
		//IL_0011: Expected O, but got Ref
		//IL_0011: Expected O, but got Ref
		//IL_0025: Expected O, but got Ref
		object obj = default(object);
		PolylinePoint polylinePoint2 = default(PolylinePoint);
		PolylinePoint polylinePoint = new PolylinePoint((Vector3)(&obj), (Color)(&polylinePoint2));
		points.Add((PolylinePoint)(&polylinePoint2));
		meshOutOfDate = true;
	}

	public unsafe void AddPoint(Vector3 position, Color color, float thickness)
	{
		//IL_003f: Expected O, but got Ref
		//IL_003f: Expected O, but got Ref
		//IL_0053: Expected O, but got Ref
		_ = 0;
		_ = 0;
		_ = position.x;
		_ = color.r;
		object obj = default(object);
		object obj2 = default(object);
		PolylinePoint polylinePoint = new PolylinePoint((Vector3)(&obj), (Color)(&obj2), thickness);
		points.Add((PolylinePoint)(&obj2));
		meshOutOfDate = true;
	}

	public unsafe void AddPoint(Vector3 position, float thickness)
	{
		//IL_0042: Expected O, but got Ref
		//IL_0042: Expected O, but got Ref
		//IL_0056: Expected O, but got Ref
		_ = 0;
		_ = 0;
		_ = position.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		_ = 0;
		object obj = default(object);
		object obj2 = default(object);
		PolylinePoint polylinePoint = new PolylinePoint((Vector3)(&obj), (Color)(&obj2), thickness);
		points.Add((PolylinePoint)(&obj2));
		meshOutOfDate = true;
	}

	public unsafe void AddPoint(PolylinePoint point)
	{
		//IL_0014: Expected O, but got Ref
		object obj = default(object);
		points.Add((PolylinePoint)(&obj));
		meshOutOfDate = true;
	}

	internal override void CamOnPreCull()
	{
		if (meshOutOfDate)
		{
			meshOutOfDate = false;
			UpdateMesh(force: true);
		}
	}

	private protected override void GenerateMesh()
	{
		Mesh sharedMesh = base.mf.sharedMesh;
		bool flattenZ = default(bool);
		bool useColors = default(bool);
		ShapesMeshGen.GenPolylineMesh(sharedMesh, points, closed, joins, flattenZ, useColors);
	}

	private protected override void SetAllMaterialProperties()
	{
		SetFloat(ShapesMaterialUtils.propThickness, thickness);
		SetInt(ShapesMaterialUtils.propThicknessSpace, (int)thicknessSpace);
		SetInt(ShapesMaterialUtils.propAlignment, (int)geometry);
	}

	private protected override void ShapeClampRanges()
	{
		//IL_000b: Invalid comparison between I4 and F4
		//IL_001d: Expected F4, but got I4
		bool flag = !(0f < thickness);
		float num = 0f;
		if (!flag)
		{
			num = thickness;
		}
		thickness = num;
	}

	private protected override void GetMaterials(Material[] mats)
	{
		//IL_0061: Expected I4, but got O
		//IL_0075: Expected I, but got O
		//IL_007d: Expected I4, but got O
		//IL_0106: Expected O, but got I4
		//IL_00b0: Expected I, but got O
		//IL_00de: Expected I, but got O
		//IL_0158: Expected I, but got O
		//IL_01b1: Expected I, but got O
		//IL_01c1: Expected O, but got I
		//IL_01df: Expected I, but got O
		ShapesMaterials polylineMat = ShapesMaterialUtils.GetPolylineMat(joins);
		bool flag = polylineMat == null;
		ShapesBlendMode shapesBlendMode = ShapesBlendMode.Opaque;
		PolylineJoins polylineJoins = joins;
		if (!flag)
		{
			shapesBlendMode = base.blendMode;
			PolylineJoins polylineJoins2 = (PolylineJoins)polylineMat.get_Item(base.blendMode);
			bool flag2 = mats == null;
			nint num = unchecked((nint)null);
			polylineJoins = (PolylineJoins)polylineMat;
			if (!flag2)
			{
				if (polylineJoins2 != PolylineJoins.Simple)
				{
					nint num2 = (nint)mats;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v147 @ rdx_v20 (Il2CppClass<UnityEngine.Material[]>)+40]");
					shapesBlendMode = ShapesBlendMode.Opaque;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj = default(object);
					bool flag3 = obj == null;
					num = unchecked((nint)null);
					polylineJoins = polylineJoins2;
					if (flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						Material material = default(Material);
						throw material;
					}
				}
				mats[0] = (Material)polylineJoins2;
				int materialCount = MaterialCount;
				if (materialCount != 2)
				{
					return;
				}
				ShapesMaterials polylineJoinsMat = ShapesMaterialUtils.GetPolylineJoinsMat(joins);
				bool flag4 = polylineJoinsMat == null;
				num = unchecked((nint)null);
				shapesBlendMode = ShapesBlendMode.Opaque;
				polylineJoins = joins;
				if (!flag4)
				{
					Material material2 = polylineJoinsMat.get_Item(base.blendMode);
					if ((object)material2 != null)
					{
						nint num3 = (nint)mats;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v330 @ rdx_v18 (Il2CppClass<UnityEngine.Material[]>)+40]");
						object obj2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj3 = default(object);
						bool flag5 = obj3 == null;
						num = unchecked((nint)null);
						Material material3 = material2;
						if (flag5)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							object obj4 = default(object);
							throw obj4;
						}
					}
					mats[1] = material2;
					return;
				}
			}
		}
		throw new NullReferenceException();
	}

	private protected unsafe override Bounds GetUnpaddedLocalBounds_Internal()
	{
		//IL_0288: Expected O, but got I4
		//IL_0283: Expected native int or pointer, but got O
		//IL_02da: Expected I, but got O
		//IL_0331: Expected I, but got O
		//IL_00b4: Expected O, but got Ref
		//IL_0383: Expected I, but got O
		//IL_05b6: Expected native int or pointer, but got O
		//IL_046d: Invalid comparison between O and F4
		//IL_0147: Expected O, but got I
		//IL_0150: Expected O, but got I4
		//IL_0531: Invalid comparison between I and F4
		//IL_01a6: Expected F4, but got O
		//IL_021f: Expected O, but got I
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Expected O, but got Unknown
		//IL_0406: Invalid comparison between F4 and O
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Expected O, but got Unknown
		//IL_01c8: Expected F4, but got I
		//IL_042a: Invalid comparison between F4 and I
		//IL_01d5: Expected F4, but got O
		//IL_0571: Expected O, but got I
		//IL_057f: Expected I, but got O
		//IL_0587: Expected O, but got Ref
		//IL_01f7: Expected F4, but got I
		List<PolylinePoint> list = points;
		if (points != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v3 (System.Collections.Generic.List`1<Shapes.PolylinePoint>)+18]");
			Bounds bounds = default(Bounds);
			if ((nint)0 < (nint)2)
			{
				((Bounds*)(nint)bounds)->m_Center = (Vector3)0;
				_ = 0;
			}
			else
			{
				nint num = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v275 @ rdx_v5 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num2 = 0;
				float num3 = (float)Vector3.oneVector * 3.4028235E+38f;
				Vector3 vector = default(Vector3);
				float num4 = (float)vector * 3.4028235E+38f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v276 @ rax_v15 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
				float num5 = 0f * 3.4028235E+38f;
				nint num6 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v470 @ rdx_v6 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num7 = 0;
				Vector3 oneVector = Vector3.oneVector;
				float num8 = (float)Vector3.oneVector * -3.4028235E+38f;
				float num9 = (float)vector * -3.4028235E+38f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v474 @ rax_v17 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
				float num10 = 0f * -3.4028235E+38f;
				Func<PolylinePoint, Vector3> selector = _003C_003Ec._003C_003E9__49_0;
				if (_003C_003Ec._003C_003E9__49_0 == null)
				{
					Func<PolylinePoint, Vector3> func = (_003C_003Ec._003C_003E9__49_0 = delegate(PolylinePoint p)
					{
						//IL_0012: Expected F4, but got O
						//IL_000d: Expected native int or pointer, but got O
						//IL_0027: Expected F4, but got I
						//IL_0022: Expected native int or pointer, but got O
						Vector3 vector2 = default(Vector3);
						((Vector3*)(nint)vector2)->x = (float)p.point;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [p @ r8 (Shapes.PolylinePoint)+8]");
						((Vector3*)(nint)vector2)->z = 0f;
						return vector2;
					});
					nint num11 = unchecked((nint)null);
					selector = func;
				}
				IEnumerable<Vector3> enumerable = Enumerable.Select(points, selector);
				if (enumerable == null)
				{
					goto IL_0293;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj2 = default(object);
				object obj = (object)(&obj2);
				IEnumerable<PolylinePoint> enumerable2 = null;
				object obj3 = default(object);
				object obj13 = default(object);
				float num12 = default(float);
				float num13 = default(float);
				object obj14 = default(object);
				object obj15 = default(object);
				while (true)
				{
					object obj12;
					if (obj2 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						if (obj3 == null)
						{
							break;
						}
						bool flag = obj2 == null;
						enumerable2 = null;
						if (!flag)
						{
							object obj4 = obj2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v638 @ r10_v5+12E]");
							if ((nint)0 >= (nint)0)
							{
								goto IL_0187;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v638 @ r10_v5+B0]");
							object obj5 = 0;
							object obj6 = 0;
							while (true)
							{
								object obj7 = obj6 + obj6;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v705 @ r8_v17+v732 @ rax_v37*8]");
								if (0 == (nint)typeof(IEnumerator<Vector3>))
								{
									break;
								}
								obj6++;
								object obj8 = obj6;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v638 @ r10_v5+12E]");
								if ((nint)obj8 < 0)
								{
									continue;
								}
								goto IL_0187;
							}
							object obj9 = obj6 + obj6;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v705 @ r8_v17+8+v789 @ rcx_v28*8]");
							object obj10 = (nint)0 << 4;
							object obj11 = obj10 + 312;
							obj12 = obj11 + obj4;
							goto IL_045b;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
					IL_045b:
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v794 @ r8_v12] (should have been resolved before IL gen)");
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj13) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3))
					{
						num3 = (float)obj13;
					}
					if (!(num12 > num4))
					{
						num4 = num12;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v665 @ rax_v32+8]");
					if (!(0f > num5))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v665 @ rax_v32+8]");
						num5 = 0f;
					}
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num8) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj13))
					{
						num8 = (float)obj13;
					}
					if (!(num9 > num13))
					{
						num9 = num13;
					}
					float num14 = num10;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v665 @ rax_v32+8]");
					if (!(num14 > 0f))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v665 @ rax_v32+8]");
						num10 = 0f;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v665 @ rax_v32+8]");
					oneVector = (Vector3)0;
					nint num11 = (nint)typeof(IEnumerator<Vector3>);
					enumerable2 = (IEnumerable<PolylinePoint>)(&obj14);
					continue;
					IL_0187:
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
					obj12 = obj15;
					goto IL_045b;
				}
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				if (geometry == PolylineGeometry.Flat2D || joins == PolylineJoins.Miter)
				{
				}
				if (thicknessSpace == ThicknessSpace.Meters)
				{
				}
				((Bounds*)(nint)bounds)->m_Center = vector;
			}
			return bounds;
		}
		goto IL_0293;
		IL_0293:
		throw new NullReferenceException();
	}

	public unsafe Polyline()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Expected O, but got Unknown
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Expected O, but got Unknown
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Expected O, but got Unknown
		//IL_029d: Expected native int or pointer, but got O
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		//IL_00b1: Expected native int or pointer, but got O
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Expected O, but got Unknown
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Expected O, but got Unknown
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Expected O, but got Unknown
		//IL_014a: Expected native int or pointer, but got O
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Expected O, but got Unknown
		//IL_01b5: Expected O, but got I
		//IL_01fb: Expected I4, but got I8
		object obj2 = default(object);
		object obj = obj2 - 95;
		List<PolylinePoint> list = new List<PolylinePoint>();
		Color color = (Color)(obj - 89);
		Vector3 point = (Vector3)(obj - 105);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		_ = 0;
		PolylinePoint polylinePoint = (PolylinePoint)(obj - 41);
		_ = 0;
		_ = 0;
		_ = 0;
		*(PolylinePoint*)(nint)polylinePoint = new PolylinePoint(point, color);
		PolylinePoint item = (PolylinePoint)(obj - 73);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-29]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-19]");
		_ = 0;
		list.Add(item);
		Color color2 = (Color)(obj - 89);
		Vector3 point2 = (Vector3)(obj - 105);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		_ = 0;
		PolylinePoint polylinePoint2 = (PolylinePoint)(obj - 9);
		_ = 0;
		_ = 0;
		_ = 0;
		*(PolylinePoint*)(nint)polylinePoint2 = new PolylinePoint(point2, color2);
		PolylinePoint item2 = (PolylinePoint)(obj - 73);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-9]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+7]");
		_ = 0;
		list.Add(item2);
		Color color3 = (Color)(obj - 89);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		_ = 0;
		Vector3 point3 = (Vector3)(obj - 105);
		PolylinePoint polylinePoint3 = (PolylinePoint)(obj + 23);
		_ = 0;
		_ = 0;
		_ = 0;
		*(PolylinePoint*)(nint)polylinePoint3 = new PolylinePoint(point3, color3);
		PolylinePoint item3 = (PolylinePoint)(obj - 73);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+17]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+27]");
		_ = 0;
		list.Add(item3);
		points = list;
		joins = PolylineJoins.Miter;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		base.color = (Color)0;
		closed = true;
		thickness = 0.125f;
		meshOutOfDate = true;
		base.blendMode = ShapesBlendMode.Transparent;
		detailLevel = DetailLevel.Medium;
		base.renderQueue = -1;
		base.zTest = CompareFunction.LessEqual;
		base.colorMask = ColorWriteMask.All;
		base.stencilComp = CompareFunction.Always;
		base.stencilReadMask = 255;
		base.shouldUpdateMaterialPropertiesInEditor = true;
		((MonoBehaviour)this)._002Ector();
	}
}
