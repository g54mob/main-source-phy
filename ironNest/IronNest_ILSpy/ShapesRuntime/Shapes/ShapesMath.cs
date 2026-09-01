using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public static class ShapesMath
{
	[StructLayout((LayoutKind)3)]
	private struct _003C_003Ec__DisplayClass35_0
	{
		public PolylinePoint a;

		public PolylinePoint b;

		public Vector3 center;

		public float radius;
	}

	[StructLayout((LayoutKind)3)]
	private struct _003C_003Ec__DisplayClass36_0
	{
		public Vector3 center;

		public float radius;
	}

	[StructLayout((LayoutKind)3)]
	private struct _003C_003Ec__DisplayClass37_0
	{
		public Vector2 center;

		public float radius;
	}

	private sealed class _003CCubicBezierPointsSkipFirst_003Ed__38 : IEnumerable<PolylinePoint>, IEnumerable, IEnumerator<PolylinePoint>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private PolylinePoint _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		private int count;

		public int _003C_003E3__count;

		private PolylinePoint a;

		public PolylinePoint _003C_003E3__a;

		private PolylinePoint b;

		public PolylinePoint _003C_003E3__b;

		private PolylinePoint c;

		public PolylinePoint _003C_003E3__c;

		private PolylinePoint d;

		public PolylinePoint _003C_003E3__d;

		private int _003Ci_003E5__2;

		unsafe PolylinePoint IEnumerator<PolylinePoint>.Current
		{
			get
			{
				//IL_000a: Expected native int or pointer, but got O
				PolylinePoint polylinePoint = default(PolylinePoint);
				((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)_003C_003E2__current;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__38)+24]");
				_ = 0;
				return polylinePoint;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				object obj = default(object);
				return (PolylinePoint)obj;
			}
		}

		public _003CCubicBezierPointsSkipFirst_003Ed__38(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			_003C_003El__initialThreadId = num;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_01f2: Expected O, but got I4
			//IL_0201: Expected I4, but got I8
			//IL_002f: Expected O, but got I4
			//IL_00d6: Invalid comparison between I4 and F4
			//IL_0075: Expected I4, but got I8
			//IL_0126: Expected O, but got Ref
			//IL_0126: Expected O, but got Ref
			//IL_0126: Expected O, but got Ref
			bool flag = _003C_003E1__state == 0;
			bool result;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					bool flag2 = (nint)obj != 1;
					result = false;
					if (!flag2)
					{
						_003C_003E1__state = -1;
						return false;
					}
					goto IL_01dd;
				}
				int num = _003Ci_003E5__2 + 1;
				_003Ci_003E5__2 = num;
			}
			else
			{
				_003Ci_003E5__2 = 1;
			}
			object obj2 = count - 1;
			_003C_003E1__state = -1;
			if (_003Ci_003E5__2 < (nint)obj2)
			{
				float num2 = (float)count - 1f;
				float num3 = (float)_003Ci_003E5__2 / num2;
				if (0f < num3)
				{
					if (num3 < 1f)
					{
						object obj3 = default(object);
						object obj4 = default(object);
						object obj5 = default(object);
						PolylinePoint polylinePoint2 = default(PolylinePoint);
						PolylinePoint polylinePoint3 = default(PolylinePoint);
						PolylinePoint polylinePoint = WeightedSum((Vector4)(&obj3), (PolylinePoint)(&obj4), (PolylinePoint)(&obj5), polylinePoint2, polylinePoint3);
						_003C_003E1__state = 1;
						_003C_003E2__current = (PolylinePoint)polylinePoint.point;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v235 @ rax_v8 (Shapes.PolylinePoint)+10]");
						_ = 0;
						return true;
					}
					_003C_003E1__state = 1;
					_003C_003E2__current = d;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__38)+110]");
					_ = 0;
					return true;
				}
				_003C_003E1__state = 1;
				_003C_003E2__current = a;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__38)+50]");
				_ = 0;
				return true;
			}
			_003C_003E1__state = 2;
			_003C_003E2__current = d;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__38)+110]");
			_ = 0;
			result = true;
			goto IL_01dd;
			IL_01dd:
			return result;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}

		IEnumerator<PolylinePoint> IEnumerable<PolylinePoint>.GetEnumerator()
		{
			_003CCubicBezierPointsSkipFirst_003Ed__38 obj2;
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					obj2 = this;
					goto IL_013f;
				}
			}
			_003CCubicBezierPointsSkipFirst_003Ed__38 obj3 = new _003CCubicBezierPointsSkipFirst_003Ed__38(0);
			obj3._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj3._003C_003El__initialThreadId = num;
			obj2 = obj3;
			goto IL_013f;
			IL_013f:
			if (obj2 != null)
			{
				obj2.a = _003C_003E3__a;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__38)+70]");
				_ = 0;
				obj2.b = _003C_003E3__b;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__38)+B0]");
				_ = 0;
				obj2.c = _003C_003E3__c;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__38)+F0]");
				_ = 0;
				obj2.d = _003C_003E3__d;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__38)+130]");
				_ = 0;
				obj2.count = _003C_003E3__count;
				return obj2;
			}
			return (IEnumerator<PolylinePoint>)new NullReferenceException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			_003CCubicBezierPointsSkipFirst_003Ed__38 obj2;
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					obj2 = this;
					goto IL_013f;
				}
			}
			_003CCubicBezierPointsSkipFirst_003Ed__38 obj3 = new _003CCubicBezierPointsSkipFirst_003Ed__38(0);
			obj3._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj3._003C_003El__initialThreadId = num;
			obj2 = obj3;
			goto IL_013f;
			IL_013f:
			if (obj2 != null)
			{
				obj2.a = _003C_003E3__a;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__38)+70]");
				_ = 0;
				obj2.b = _003C_003E3__b;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__38)+B0]");
				_ = 0;
				obj2.c = _003C_003E3__c;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__38)+F0]");
				_ = 0;
				obj2.d = _003C_003E3__d;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__38)+130]");
				_ = 0;
				obj2.count = _003C_003E3__count;
				return obj2;
			}
			return (IEnumerator)new NullReferenceException();
		}
	}

	private sealed class _003CCubicBezierPointsSkipFirst_003Ed__40 : IEnumerable<Vector3>, IEnumerable, IEnumerator<Vector3>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private Vector3 _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		private int count;

		public int _003C_003E3__count;

		private Vector3 a;

		public Vector3 _003C_003E3__a;

		private Vector3 b;

		public Vector3 _003C_003E3__b;

		private Vector3 c;

		public Vector3 _003C_003E3__c;

		private Vector3 d;

		public Vector3 _003C_003E3__d;

		private int _003Ci_003E5__2;

		unsafe Vector3 IEnumerator<Vector3>.Current
		{
			get
			{
				//IL_000f: Expected F4, but got O
				//IL_000a: Expected native int or pointer, but got O
				//IL_0024: Expected F4, but got I
				//IL_001f: Expected native int or pointer, but got O
				Vector3 vector = default(Vector3);
				((Vector3*)(nint)vector)->x = (float)_003C_003E2__current;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__40)+1C]");
				((Vector3*)(nint)vector)->z = 0f;
				return vector;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				object obj = default(object);
				return (Vector3)obj;
			}
		}

		public _003CCubicBezierPointsSkipFirst_003Ed__40(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			_003C_003El__initialThreadId = num;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_02ba: Expected O, but got I4
			//IL_002f: Expected O, but got I4
			//IL_00d6: Invalid comparison between I4 and F4
			//IL_026e: Expected F4, but got I
			//IL_0075: Expected I4, but got I8
			//IL_024f: Expected F4, but got I
			bool flag = _003C_003E1__state == 0;
			bool result;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					bool flag2 = (nint)obj != 1;
					result = false;
					if (!flag2)
					{
						_003C_003E1__state = -1;
						return false;
					}
					goto IL_02a5;
				}
				int num = _003Ci_003E5__2 + 1;
				_003Ci_003E5__2 = num;
			}
			else
			{
				_003Ci_003E5__2 = 1;
			}
			object obj2 = count - 1;
			if (_003Ci_003E5__2 < (nint)obj2)
			{
				float num2 = (float)count - 1f;
				float num3 = (float)_003Ci_003E5__2 / num2;
				Vector3 vector;
				if (0f < num3)
				{
					if (num3 < 1f)
					{
						float num4 = 1f - num3;
						float num5 = num3 * num3;
						float num6 = num4 * num4;
						float num7 = num6 * 3f;
						float num8 = num6 * num4;
						float num9 = num4 * 3f;
						float num10 = num7 * num3;
						float num11 = num9 * num5;
						float num12 = num5 * num3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__40)+4C]");
						float num13 = 0f * num10;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__40)+64]");
						float num14 = 0f * num11;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__40)+34]");
						float num15 = 0f * num8;
						float num16 = num13 + num15;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__40)+7C]");
						float num17 = 0f * num12;
						float num18 = num16 + num14;
						float num19 = num18 + num17;
						Vector3 vector2 = default(Vector3);
						vector = vector2;
						float num20 = num19;
					}
					else
					{
						vector = d;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__40)+7C]");
						float num20 = 0f;
					}
				}
				else
				{
					vector = a;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__40)+34]");
					float num20 = 0f;
				}
				_003C_003E2__current = vector;
				_003C_003E1__state = 1;
				return true;
			}
			_003C_003E2__current = d;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__40)+7C]");
			_ = 0;
			_003C_003E1__state = 2;
			result = true;
			goto IL_02a5;
			IL_02a5:
			return result;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}

		IEnumerator<Vector3> IEnumerable<Vector3>.GetEnumerator()
		{
			_003CCubicBezierPointsSkipFirst_003Ed__40 obj2;
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					obj2 = this;
					goto IL_013f;
				}
			}
			_003CCubicBezierPointsSkipFirst_003Ed__40 obj3 = new _003CCubicBezierPointsSkipFirst_003Ed__40(0);
			obj3._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj3._003C_003El__initialThreadId = num;
			obj2 = obj3;
			goto IL_013f;
			IL_013f:
			if (obj2 != null)
			{
				obj2.a = _003C_003E3__a;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__40)+40]");
				_ = 0;
				obj2.b = _003C_003E3__b;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__40)+58]");
				_ = 0;
				obj2.c = _003C_003E3__c;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__40)+70]");
				_ = 0;
				obj2.d = _003C_003E3__d;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__40)+88]");
				_ = 0;
				obj2.count = _003C_003E3__count;
				return obj2;
			}
			return (IEnumerator<Vector3>)new NullReferenceException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			_003CCubicBezierPointsSkipFirst_003Ed__40 obj2;
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					obj2 = this;
					goto IL_013f;
				}
			}
			_003CCubicBezierPointsSkipFirst_003Ed__40 obj3 = new _003CCubicBezierPointsSkipFirst_003Ed__40(0);
			obj3._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj3._003C_003El__initialThreadId = num;
			obj2 = obj3;
			goto IL_013f;
			IL_013f:
			if (obj2 != null)
			{
				obj2.a = _003C_003E3__a;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__40)+40]");
				_ = 0;
				obj2.b = _003C_003E3__b;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__40)+58]");
				_ = 0;
				obj2.c = _003C_003E3__c;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__40)+70]");
				_ = 0;
				obj2.d = _003C_003E3__d;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__40)+88]");
				_ = 0;
				obj2.count = _003C_003E3__count;
				return obj2;
			}
			return (IEnumerator)new NullReferenceException();
		}
	}

	private sealed class _003CCubicBezierPointsSkipFirst_003Ed__41 : IEnumerable<Vector2>, IEnumerable, IEnumerator<Vector2>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private Vector2 _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		private int count;

		public int _003C_003E3__count;

		private Vector2 a;

		public Vector2 _003C_003E3__a;

		private Vector2 b;

		public Vector2 _003C_003E3__b;

		private Vector2 c;

		public Vector2 _003C_003E3__c;

		private Vector2 d;

		public Vector2 _003C_003E3__d;

		private int _003Ci_003E5__2;

		Vector2 IEnumerator<Vector2>.Current
		{
			get
			{
				Vector2 result = default(Vector2);
				return result;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				object obj = default(object);
				return (Vector2)obj;
			}
		}

		public _003CCubicBezierPointsSkipFirst_003Ed__41(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			_003C_003El__initialThreadId = num;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_02f7: Expected O, but got I4
			//IL_002f: Expected O, but got I4
			//IL_00d6: Invalid comparison between I4 and F4
			//IL_0075: Expected I4, but got I8
			//IL_0322: Expected O, but got F4
			bool flag = _003C_003E1__state == 0;
			bool result;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					bool flag2 = (nint)obj != 1;
					result = false;
					if (!flag2)
					{
						_003C_003E1__state = -1;
						return false;
					}
					goto IL_02e2;
				}
				int num = _003Ci_003E5__2 + 1;
				_003Ci_003E5__2 = num;
			}
			else
			{
				_003Ci_003E5__2 = 1;
			}
			object obj2 = count - 1;
			float num29;
			if (_003Ci_003E5__2 < (nint)obj2)
			{
				float num2 = (float)count - 1f;
				float num3 = (float)_003Ci_003E5__2 / num2;
				float num31;
				float num32 = default(float);
				float num30;
				if (0f < num3)
				{
					if (num3 < 1f)
					{
						float num4 = 1f - num3;
						float num5 = num3 * num3;
						float num6 = num4 * num4;
						float num7 = num6 * 3f;
						float num8 = num6 * num4;
						float num9 = num4 * 3f;
						float num10 = num7 * num3;
						float num11 = num8 * (float)a;
						float num12 = num9 * num5;
						float num13 = num10 * (float)b;
						float num14 = num10;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__41)+3C]");
						float num15 = num14 * 0f;
						float num16 = num5 * num3;
						float num17 = num13 + num11;
						float num18 = num8;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__41)+2C]");
						float num19 = num18 * 0f;
						float num20 = num12 * (float)c;
						float num21 = num12;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__41)+4C]");
						float num22 = num21 * 0f;
						float num23 = num15 + num19;
						float num24 = num16 * (float)d;
						float num25 = num17 + num20;
						float num26 = num16;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__41)+5C]");
						float num27 = num26 * 0f;
						float num28 = num23 + num22;
						num29 = num25 + num24;
						num30 = num28 + num27;
						goto IL_0318;
					}
					num31 = num32;
				}
				else
				{
					num31 = num32;
				}
				num29 = num31;
				num30 = num32;
				goto IL_0318;
			}
			_003C_003E2__current = d;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__41)+5C]");
			_ = 0;
			_003C_003E1__state = 2;
			result = true;
			goto IL_02e2;
			IL_02e2:
			return result;
			IL_0318:
			_003C_003E2__current = (Vector2)num29;
			_003C_003E1__state = 1;
			return true;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}

		IEnumerator<Vector2> IEnumerable<Vector2>.GetEnumerator()
		{
			_003CCubicBezierPointsSkipFirst_003Ed__41 obj2;
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					obj2 = this;
					goto IL_013f;
				}
			}
			_003CCubicBezierPointsSkipFirst_003Ed__41 obj3 = new _003CCubicBezierPointsSkipFirst_003Ed__41(0);
			obj3._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj3._003C_003El__initialThreadId = num;
			obj2 = obj3;
			goto IL_013f;
			IL_013f:
			if (obj2 != null)
			{
				obj2.a = _003C_003E3__a;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__41)+34]");
				_ = 0;
				obj2.b = _003C_003E3__b;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__41)+44]");
				_ = 0;
				obj2.c = _003C_003E3__c;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__41)+54]");
				_ = 0;
				obj2.d = _003C_003E3__d;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__41)+64]");
				_ = 0;
				obj2.count = _003C_003E3__count;
				return obj2;
			}
			return (IEnumerator<Vector2>)new NullReferenceException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			_003CCubicBezierPointsSkipFirst_003Ed__41 obj2;
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					obj2 = this;
					goto IL_013f;
				}
			}
			_003CCubicBezierPointsSkipFirst_003Ed__41 obj3 = new _003CCubicBezierPointsSkipFirst_003Ed__41(0);
			obj3._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj3._003C_003El__initialThreadId = num;
			obj2 = obj3;
			goto IL_013f;
			IL_013f:
			if (obj2 != null)
			{
				obj2.a = _003C_003E3__a;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__41)+34]");
				_ = 0;
				obj2.b = _003C_003E3__b;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__41)+44]");
				_ = 0;
				obj2.c = _003C_003E3__c;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__41)+54]");
				_ = 0;
				obj2.d = _003C_003E3__d;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirst>d__41)+64]");
				_ = 0;
				obj2.count = _003C_003E3__count;
				return obj2;
			}
			return (IEnumerator)new NullReferenceException();
		}
	}

	private sealed class _003CCubicBezierPointsSkipFirstMatchStyle_003Ed__39 : IEnumerable<PolylinePoint>, IEnumerable, IEnumerator<PolylinePoint>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private PolylinePoint _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		private int count;

		public int _003C_003E3__count;

		private PolylinePoint style;

		public PolylinePoint _003C_003E3__style;

		private Vector3 a;

		public Vector3 _003C_003E3__a;

		private Vector3 b;

		public Vector3 _003C_003E3__b;

		private Vector3 c;

		public Vector3 _003C_003E3__c;

		private Vector3 d;

		public Vector3 _003C_003E3__d;

		private int _003Ci_003E5__2;

		unsafe PolylinePoint IEnumerator<PolylinePoint>.Current
		{
			get
			{
				//IL_000a: Expected native int or pointer, but got O
				PolylinePoint polylinePoint = default(PolylinePoint);
				((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)_003C_003E2__current;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.ShapesMath+<CubicBezierPointsSkipFirstMatchStyle>d__39)+24]");
				_ = 0;
				return polylinePoint;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				object obj = default(object);
				return (PolylinePoint)obj;
			}
		}

		public _003CCubicBezierPointsSkipFirstMatchStyle_003Ed__39(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			_003C_003El__initialThreadId = num;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_017f: Expected O, but got I4
			//IL_002f: Expected O, but got I4
			//IL_00d6: Invalid comparison between I4 and F4
			//IL_0075: Expected I4, but got I8
			bool flag = _003C_003E1__state == 0;
			bool result;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					bool flag2 = (nint)obj != 1;
					result = false;
					if (!flag2)
					{
						_003C_003E1__state = -1;
						return false;
					}
					goto IL_016a;
				}
				int num = _003Ci_003E5__2 + 1;
				_003Ci_003E5__2 = num;
			}
			else
			{
				_003Ci_003E5__2 = 1;
			}
			object obj2 = count - 1;
			if (_003Ci_003E5__2 < (nint)obj2)
			{
				float num2 = (float)count - 1f;
				float num3 = (float)_003Ci_003E5__2 / num2;
				Vector3 vector2 = default(Vector3);
				Vector3 vector = ((!(0f < num3)) ? a : ((!(num3 < 1f)) ? d : vector2));
				_003C_003E1__state = 1;
				_003C_003E2__current = (PolylinePoint)vector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirstMatchStyle>d__39)+50]");
				_ = 0;
				return true;
			}
			_003C_003E1__state = 2;
			_003C_003E2__current = (PolylinePoint)d;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirstMatchStyle>d__39)+50]");
			_ = 0;
			result = true;
			goto IL_016a;
			IL_016a:
			return result;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}

		IEnumerator<PolylinePoint> IEnumerable<PolylinePoint>.GetEnumerator()
		{
			_003CCubicBezierPointsSkipFirstMatchStyle_003Ed__39 obj2;
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					obj2 = this;
					goto IL_015b;
				}
			}
			_003CCubicBezierPointsSkipFirstMatchStyle_003Ed__39 obj3 = new _003CCubicBezierPointsSkipFirstMatchStyle_003Ed__39(0);
			obj3._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj3._003C_003El__initialThreadId = num;
			obj2 = obj3;
			goto IL_015b;
			IL_015b:
			if (obj2 != null)
			{
				obj2.style = _003C_003E3__style;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirstMatchStyle>d__39)+70]");
				_ = 0;
				obj2.a = _003C_003E3__a;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirstMatchStyle>d__39)+94]");
				_ = 0;
				obj2.b = _003C_003E3__b;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirstMatchStyle>d__39)+AC]");
				_ = 0;
				obj2.c = _003C_003E3__c;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirstMatchStyle>d__39)+C4]");
				_ = 0;
				obj2.d = _003C_003E3__d;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirstMatchStyle>d__39)+DC]");
				_ = 0;
				obj2.count = _003C_003E3__count;
				return obj2;
			}
			return (IEnumerator<PolylinePoint>)new NullReferenceException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			_003CCubicBezierPointsSkipFirstMatchStyle_003Ed__39 obj2;
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					obj2 = this;
					goto IL_015b;
				}
			}
			_003CCubicBezierPointsSkipFirstMatchStyle_003Ed__39 obj3 = new _003CCubicBezierPointsSkipFirstMatchStyle_003Ed__39(0);
			obj3._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj3._003C_003El__initialThreadId = num;
			obj2 = obj3;
			goto IL_015b;
			IL_015b:
			if (obj2 != null)
			{
				obj2.style = _003C_003E3__style;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirstMatchStyle>d__39)+70]");
				_ = 0;
				obj2.a = _003C_003E3__a;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirstMatchStyle>d__39)+94]");
				_ = 0;
				obj2.b = _003C_003E3__b;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirstMatchStyle>d__39)+AC]");
				_ = 0;
				obj2.c = _003C_003E3__c;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirstMatchStyle>d__39)+C4]");
				_ = 0;
				obj2.d = _003C_003E3__d;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<CubicBezierPointsSkipFirstMatchStyle>d__39)+DC]");
				_ = 0;
				obj2.count = _003C_003E3__count;
				return obj2;
			}
			return (IEnumerator)new NullReferenceException();
		}
	}

	private sealed class _003CGetArcPoints_003Ed__35 : IEnumerable<PolylinePoint>, IEnumerable, IEnumerator<PolylinePoint>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private PolylinePoint _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		private PolylinePoint a;

		public PolylinePoint _003C_003E3__a;

		private PolylinePoint b;

		public PolylinePoint _003C_003E3__b;

		private Vector3 center;

		public Vector3 _003C_003E3__center;

		private float radius;

		public float _003C_003E3__radius;

		private int count;

		public int _003C_003E3__count;

		private Vector3 normA;

		public Vector3 _003C_003E3__normA;

		private Vector3 normB;

		public Vector3 _003C_003E3__normB;

		private _003C_003Ec__DisplayClass35_0 _003C_003E8__1;

		private int _003Ci_003E5__2;

		unsafe PolylinePoint IEnumerator<PolylinePoint>.Current
		{
			get
			{
				//IL_000a: Expected native int or pointer, but got O
				PolylinePoint polylinePoint = default(PolylinePoint);
				((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)_003C_003E2__current;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.ShapesMath+<GetArcPoints>d__35)+24]");
				_ = 0;
				return polylinePoint;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				object obj = default(object);
				return (PolylinePoint)obj;
			}
		}

		public _003CGetArcPoints_003Ed__35(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			_003C_003El__initialThreadId = num;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_01b5: Expected I4, but got I8
			//IL_0282: Unknown result type (might be due to invalid IL or missing references)
			//IL_0287: Expected Ref, but got Unknown
			//IL_02a3: Expected O, but got Ref
			//IL_002f: Expected O, but got I4
			//IL_024c: Expected O, but got I4
			//IL_025b: Expected I4, but got I8
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Expected O, but got Unknown
			//IL_015e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0163: Expected Ref, but got Unknown
			//IL_0175: Expected O, but got Ref
			//IL_0104: Unknown result type (might be due to invalid IL or missing references)
			//IL_0109: Expected Ref, but got Unknown
			//IL_011a: Expected O, but got Ref
			//IL_0091: Expected I4, but got I8
			bool flag = _003C_003E1__state == 0;
			bool result;
			object obj4 = default(object);
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					object obj2 = obj - 1;
					if (!flag)
					{
						bool flag2 = (nint)obj2 != 1;
						result = false;
						if (!flag2)
						{
							_003C_003E1__state = -1;
							return false;
						}
						goto IL_0237;
					}
					int num = _003Ci_003E5__2 + 1;
					_003Ci_003E5__2 = num;
				}
				else
				{
					_003Ci_003E5__2 = 1;
				}
				object obj3 = count - 1;
				_003C_003E1__state = -1;
				if (_003Ci_003E5__2 < (nint)obj3)
				{
					float num2 = (float)count - 1f;
					float t = (float)_003Ci_003E5__2 / num2;
					Vector3 vector2 = default(Vector3);
					Vector3 vector3 = default(Vector3);
					Vector3 vector = Vector3.Internal_Slerp(ref vector2, ref vector3, t);
					PolylinePoint polylinePoint = _003CGetArcPoints_003Eg__DirToPt_007C35_0((Vector3)(&obj4), t, ref *(_003C_003Ec__DisplayClass35_0*)(this + 272));
					_003C_003E1__state = 2;
					_003C_003E2__current = (PolylinePoint)polylinePoint.point;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ rax_v17 (Shapes.PolylinePoint)+10]");
					_ = 0;
					result = true;
					goto IL_0237;
				}
				PolylinePoint polylinePoint2 = _003CGetArcPoints_003Eg__DirToPt_007C35_0((Vector3)(&obj4), 1f, ref *(_003C_003Ec__DisplayClass35_0*)(this + 272));
				_003C_003E1__state = 3;
				_003C_003E2__current = (PolylinePoint)polylinePoint2.point;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v174 @ rax_v12 (Shapes.PolylinePoint)+10]");
				_ = 0;
				return true;
			}
			_003C_003E1__state = -1;
			_003C_003E8__1 = (_003C_003Ec__DisplayClass35_0)a;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__35)+48]");
			_ = 0;
			_ = b;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__35)+88]");
			_ = 0;
			_ = center;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__35)+C0]");
			_ = 0;
			_ = radius;
			int num3 = count;
			if (count < 2)
			{
				num3 = 2;
			}
			ref _003C_003Ec__DisplayClass35_0 reference = ref *(_003C_003Ec__DisplayClass35_0*)(this + 272);
			count = num3;
			PolylinePoint polylinePoint3 = _003CGetArcPoints_003Eg__DirToPt_007C35_0((Vector3)(&obj4), 0f, ref reference);
			_003C_003E1__state = 1;
			_003C_003E2__current = (PolylinePoint)polylinePoint3.point;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rax_v6 (Shapes.PolylinePoint)+10]");
			_ = 0;
			return true;
			IL_0237:
			return result;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}

		IEnumerator<PolylinePoint> IEnumerable<PolylinePoint>.GetEnumerator()
		{
			_003CGetArcPoints_003Ed__35 obj2;
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					obj2 = this;
					goto IL_016a;
				}
			}
			_003CGetArcPoints_003Ed__35 obj3 = new _003CGetArcPoints_003Ed__35(0);
			obj3._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj3._003C_003El__initialThreadId = num;
			obj2 = obj3;
			goto IL_016a;
			IL_016a:
			if (obj2 != null)
			{
				obj2.a = _003C_003E3__a;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__35)+68]");
				_ = 0;
				obj2.b = _003C_003E3__b;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__35)+A8]");
				_ = 0;
				obj2.normA = _003C_003E3__normA;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__35)+F4]");
				_ = 0;
				obj2.normB = _003C_003E3__normB;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__35)+10C]");
				_ = 0;
				obj2.center = _003C_003E3__center;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__35)+CC]");
				_ = 0;
				obj2.radius = _003C_003E3__radius;
				obj2.count = _003C_003E3__count;
				return obj2;
			}
			return (IEnumerator<PolylinePoint>)new NullReferenceException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			_003CGetArcPoints_003Ed__35 obj2;
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					obj2 = this;
					goto IL_016a;
				}
			}
			_003CGetArcPoints_003Ed__35 obj3 = new _003CGetArcPoints_003Ed__35(0);
			obj3._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj3._003C_003El__initialThreadId = num;
			obj2 = obj3;
			goto IL_016a;
			IL_016a:
			if (obj2 != null)
			{
				obj2.a = _003C_003E3__a;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__35)+68]");
				_ = 0;
				obj2.b = _003C_003E3__b;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__35)+A8]");
				_ = 0;
				obj2.normA = _003C_003E3__normA;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__35)+F4]");
				_ = 0;
				obj2.normB = _003C_003E3__normB;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__35)+10C]");
				_ = 0;
				obj2.center = _003C_003E3__center;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__35)+CC]");
				_ = 0;
				obj2.radius = _003C_003E3__radius;
				obj2.count = _003C_003E3__count;
				return obj2;
			}
			return (IEnumerator)new NullReferenceException();
		}
	}

	private sealed class _003CGetArcPoints_003Ed__36 : IEnumerable<Vector3>, IEnumerable, IEnumerator<Vector3>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private Vector3 _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		private Vector3 center;

		public Vector3 _003C_003E3__center;

		private float radius;

		public float _003C_003E3__radius;

		private int count;

		public int _003C_003E3__count;

		private Vector3 normA;

		public Vector3 _003C_003E3__normA;

		private Vector3 normB;

		public Vector3 _003C_003E3__normB;

		private _003C_003Ec__DisplayClass36_0 _003C_003E8__1;

		private int _003Ci_003E5__2;

		unsafe Vector3 IEnumerator<Vector3>.Current
		{
			get
			{
				//IL_000f: Expected F4, but got O
				//IL_000a: Expected native int or pointer, but got O
				//IL_0024: Expected F4, but got I
				//IL_001f: Expected native int or pointer, but got O
				Vector3 vector = default(Vector3);
				((Vector3*)(nint)vector)->x = (float)_003C_003E2__current;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.ShapesMath+<GetArcPoints>d__36)+1C]");
				((Vector3*)(nint)vector)->z = 0f;
				return vector;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				object obj = default(object);
				return (Vector3)obj;
			}
		}

		public _003CGetArcPoints_003Ed__36(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			_003C_003El__initialThreadId = num;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0262: Expected O, but got I
			//IL_0272: Unknown result type (might be due to invalid IL or missing references)
			//IL_0277: Expected O, but got Unknown
			//IL_002f: Expected O, but got I4
			//IL_01ee: Expected O, but got I4
			//IL_01fd: Expected I4, but got I8
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Expected O, but got Unknown
			//IL_015a: Expected O, but got I
			//IL_016a: Unknown result type (might be due to invalid IL or missing references)
			//IL_016f: Expected O, but got Unknown
			//IL_0113: Unknown result type (might be due to invalid IL or missing references)
			//IL_0118: Expected O, but got Unknown
			//IL_0128: Unknown result type (might be due to invalid IL or missing references)
			//IL_012d: Expected O, but got Unknown
			//IL_0091: Expected I4, but got I8
			bool flag = _003C_003E1__state == 0;
			bool result;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					object obj2 = obj - 1;
					if (!flag)
					{
						bool flag2 = (nint)obj2 != 1;
						result = false;
						if (!flag2)
						{
							_003C_003E1__state = -1;
							return false;
						}
						goto IL_01d9;
					}
					int num = _003Ci_003E5__2 + 1;
					_003Ci_003E5__2 = num;
				}
				else
				{
					_003Ci_003E5__2 = 1;
				}
				object obj3 = count - 1;
				_003C_003E1__state = -1;
				if (_003Ci_003E5__2 < (nint)obj3)
				{
					float num2 = (float)count - 1f;
					float t = (float)_003Ci_003E5__2 / num2;
					Vector3 a = default(Vector3);
					Vector3 b = default(Vector3);
					Vector3 vector = Vector3.Internal_Slerp(ref a, ref b, t);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__36)+88]");
					object obj4 = 0 * vector.z;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__36)+84]");
					object obj5 = obj4 + 0;
					_003C_003E1__state = 2;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__36)+88]");
					nint num3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__36)+6C]");
					object obj6 = num3 * 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__36)+84]");
					object obj5 = obj6 + 0;
					_003C_003E1__state = 3;
				}
			}
			else
			{
				_003C_003E8__1 = (_003C_003Ec__DisplayClass36_0)center;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__36)+2C]");
				_ = 0;
				_ = radius;
				int num4 = count;
				if (count < 2)
				{
					num4 = 2;
				}
				count = num4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__36)+88]");
				nint num5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__36)+54]");
				object obj7 = num5 * 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__36)+84]");
				object obj5 = obj7 + 0;
				_003C_003E1__state = 1;
			}
			Vector3 vector2 = default(Vector3);
			_003C_003E2__current = vector2;
			result = true;
			goto IL_01d9;
			IL_01d9:
			return result;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}

		IEnumerator<Vector3> IEnumerable<Vector3>.GetEnumerator()
		{
			_003CGetArcPoints_003Ed__36 obj2;
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					obj2 = this;
					goto IL_0132;
				}
			}
			_003CGetArcPoints_003Ed__36 obj3 = new _003CGetArcPoints_003Ed__36(0);
			obj3._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj3._003C_003El__initialThreadId = num;
			obj2 = obj3;
			goto IL_0132;
			IL_0132:
			if (obj2 != null)
			{
				obj2.normA = _003C_003E3__normA;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__36)+60]");
				_ = 0;
				obj2.normB = _003C_003E3__normB;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__36)+78]");
				_ = 0;
				obj2.center = _003C_003E3__center;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__36)+38]");
				_ = 0;
				obj2.radius = _003C_003E3__radius;
				obj2.count = _003C_003E3__count;
				return obj2;
			}
			return (IEnumerator<Vector3>)new NullReferenceException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			_003CGetArcPoints_003Ed__36 obj2;
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					obj2 = this;
					goto IL_0132;
				}
			}
			_003CGetArcPoints_003Ed__36 obj3 = new _003CGetArcPoints_003Ed__36(0);
			obj3._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj3._003C_003El__initialThreadId = num;
			obj2 = obj3;
			goto IL_0132;
			IL_0132:
			if (obj2 != null)
			{
				obj2.normA = _003C_003E3__normA;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__36)+60]");
				_ = 0;
				obj2.normB = _003C_003E3__normB;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__36)+78]");
				_ = 0;
				obj2.center = _003C_003E3__center;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__36)+38]");
				_ = 0;
				obj2.radius = _003C_003E3__radius;
				obj2.count = _003C_003E3__count;
				return obj2;
			}
			return (IEnumerator)new NullReferenceException();
		}
	}

	private sealed class _003CGetArcPoints_003Ed__37 : IEnumerable<Vector2>, IEnumerable, IEnumerator<Vector2>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private Vector2 _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		private Vector2 center;

		public Vector2 _003C_003E3__center;

		private float radius;

		public float _003C_003E3__radius;

		private int count;

		public int _003C_003E3__count;

		private Vector2 normA;

		public Vector2 _003C_003E3__normA;

		private Vector2 normB;

		public Vector2 _003C_003E3__normB;

		private _003C_003Ec__DisplayClass37_0 _003C_003E8__1;

		private int _003Ci_003E5__2;

		Vector2 IEnumerator<Vector2>.Current
		{
			get
			{
				Vector2 result = default(Vector2);
				return result;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				object obj = default(object);
				return (Vector2)obj;
			}
		}

		public _003CGetArcPoints_003Ed__37(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			_003C_003El__initialThreadId = num;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_01fc: Expected I4, but got I8
			//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02bc: Expected O, but got Unknown
			//IL_02d9: Expected O, but got I
			//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fd: Expected O, but got Unknown
			//IL_002f: Expected O, but got I4
			//IL_026b: Expected O, but got I4
			//IL_027a: Expected I4, but got I8
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Expected O, but got Unknown
			//IL_0187: Unknown result type (might be due to invalid IL or missing references)
			//IL_018c: Expected O, but got Unknown
			//IL_01a9: Expected O, but got I
			//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cd: Expected O, but got Unknown
			//IL_012a: Unknown result type (might be due to invalid IL or missing references)
			//IL_012f: Expected O, but got Unknown
			//IL_0150: Unknown result type (might be due to invalid IL or missing references)
			//IL_0155: Expected O, but got Unknown
			//IL_016a: Expected O, but got F4
			//IL_0091: Expected I4, but got I8
			bool flag = _003C_003E1__state == 0;
			bool result;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					object obj2 = obj - 1;
					if (!flag)
					{
						bool flag2 = (nint)obj2 != 1;
						result = false;
						if (!flag2)
						{
							_003C_003E1__state = -1;
							return false;
						}
						goto IL_0256;
					}
					int num = _003Ci_003E5__2 + 1;
					_003Ci_003E5__2 = num;
				}
				else
				{
					_003Ci_003E5__2 = 1;
				}
				object obj3 = count - 1;
				_003C_003E1__state = -1;
				if (_003Ci_003E5__2 < (nint)obj3)
				{
					float num2 = (float)count - 1f;
					float t = (float)_003Ci_003E5__2 / num2;
					Vector3 a = default(Vector3);
					Vector3 b = default(Vector3);
					Vector3 vector = Vector3.Internal_Slerp(ref a, ref b, t);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__37)+68]");
					float num3 = 0f * vector.x;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__37)+68]");
					object obj5 = default(object);
					object obj4 = 0 * obj5;
					float num4 = num3 + (float)_003C_003E8__1;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__37)+64]");
					object obj6 = obj4 + 0;
					_003C_003E1__state = 2;
					_003C_003E2__current = (Vector2)num4;
					return true;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__37)+68]");
				object obj7 = 0 * normB;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__37)+68]");
				nint num5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__37)+54]");
				object obj8 = num5 * 0;
				Vector2 vector2 = (Vector2)(obj7 + (object)_003C_003E8__1);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__37)+64]");
				object obj9 = obj8 + 0;
				_003C_003E1__state = 3;
				_003C_003E2__current = vector2;
				return true;
			}
			_003C_003E1__state = -1;
			_003C_003E8__1 = (_003C_003Ec__DisplayClass37_0)center;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__37)+24]");
			_ = 0;
			_ = radius;
			int num6 = count;
			if (count < 2)
			{
				num6 = 2;
			}
			count = num6;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__37)+68]");
			object obj10 = 0 * normA;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__37)+68]");
			nint num7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__37)+44]");
			object obj11 = num7 * 0;
			Vector2 vector3 = (Vector2)(obj10 + (object)_003C_003E8__1);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__37)+64]");
			object obj12 = obj11 + 0;
			_003C_003E1__state = 1;
			_003C_003E2__current = vector3;
			result = true;
			goto IL_0256;
			IL_0256:
			return result;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}

		IEnumerator<Vector2> IEnumerable<Vector2>.GetEnumerator()
		{
			_003CGetArcPoints_003Ed__37 obj2;
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					obj2 = this;
					goto IL_0132;
				}
			}
			_003CGetArcPoints_003Ed__37 obj3 = new _003CGetArcPoints_003Ed__37(0);
			obj3._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj3._003C_003El__initialThreadId = num;
			obj2 = obj3;
			goto IL_0132;
			IL_0132:
			if (obj2 != null)
			{
				obj2.normA = _003C_003E3__normA;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__37)+4C]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__37)+5C]");
				_ = 0;
				obj2.normB = _003C_003E3__normB;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__37)+2C]");
				_ = 0;
				obj2.center = _003C_003E3__center;
				obj2.radius = _003C_003E3__radius;
				obj2.count = _003C_003E3__count;
				return obj2;
			}
			return (IEnumerator<Vector2>)new NullReferenceException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			_003CGetArcPoints_003Ed__37 obj2;
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					obj2 = this;
					goto IL_0132;
				}
			}
			_003CGetArcPoints_003Ed__37 obj3 = new _003CGetArcPoints_003Ed__37(0);
			obj3._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj3._003C_003El__initialThreadId = num;
			obj2 = obj3;
			goto IL_0132;
			IL_0132:
			if (obj2 != null)
			{
				obj2.normA = _003C_003E3__normA;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__37)+4C]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__37)+5C]");
				_ = 0;
				obj2.normB = _003C_003E3__normB;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesMath+<GetArcPoints>d__37)+2C]");
				_ = 0;
				obj2.center = _003C_003E3__center;
				obj2.radius = _003C_003E3__radius;
				obj2.count = _003C_003E3__count;
				return obj2;
			}
			return (IEnumerator)new NullReferenceException();
		}
	}

	private const MethodImplOptions INLINE = MethodImplOptions.AggressiveInlining;

	public const float TAU = (float)Math.PI * 2f;

	public const double DEG_TO_RAD = Math.PI / 180.0;

	private const double SINC_W = 0.01;

	private const double SINC_P_C2 = -1.0 / 6.0;

	private const double SINC_P_C4 = 1.0 / 120.0;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Frac(float x)
	{
		float num = MathF.Floor(x);
		return x - num;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Eerp(float a, float b, float t)
	{
		float num = 1f - t;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		return a * b;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float SmoothCos01(float x)
	{
		float num = x * (float)Math.PI;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
		float num2 = num * -0.5f;
		return num2 + 0.5f;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 AngToDir(float angRad)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
		Vector2 result = default(Vector2);
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float DirToAng(Vector2 dir)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
		float result = default(float);
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Rotate90CW(Vector2 v)
	{
		Vector2 result = default(Vector2);
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Rotate90CCW(Vector2 v)
	{
		Vector2 result = default(Vector2);
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static Vector4 AtLeast0(Vector4 v)
	{
		//IL_000e: Invalid comparison between I4 and F4
		//IL_0020: Expected F4, but got I4
		//IL_0084: Invalid comparison between I4 and F4
		//IL_0096: Expected F4, but got I4
		//IL_00ed: Invalid comparison between I4 and F4
		//IL_00ff: Expected F4, but got I4
		//IL_00ac: Expected native int or pointer, but got O
		//IL_00bf: Invalid comparison between I4 and F4
		//IL_00d1: Expected F4, but got I4
		//IL_0115: Expected native int or pointer, but got O
		//IL_0122: Expected native int or pointer, but got O
		//IL_012f: Expected native int or pointer, but got O
		bool flag = !(0f < v.x);
		float x = 0f;
		if (!flag)
		{
			x = v.x;
		}
		bool flag2 = !(0f < v.w);
		float w = 0f;
		if (!flag2)
		{
			w = v.w;
		}
		bool flag3 = !(0f < v.y);
		float y = 0f;
		if (!flag3)
		{
			y = v.y;
		}
		Vector4 vector = default(Vector4);
		((Vector4*)(nint)vector)->x = x;
		bool flag4 = !(0f < v.z);
		float z = 0f;
		if (!flag4)
		{
			z = v.z;
		}
		((Vector4*)(nint)vector)->w = w;
		((Vector4*)(nint)vector)->y = y;
		((Vector4*)(nint)vector)->z = z;
		return vector;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float MaxComp(Vector4 v)
	{
		float num = v.y;
		if (v.y < v.x)
		{
			num = v.x;
		}
		if (num < v.z)
		{
			num = v.z;
		}
		if (num < v.w)
		{
			num = v.w;
		}
		return num;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool HasNegativeValues(Vector4 v)
	{
		//IL_000e: Invalid comparison between I4 and F4
		//IL_0030: Invalid comparison between I4 and F4
		//IL_0052: Invalid comparison between I4 and F4
		//IL_0074: Invalid comparison between I4 and F4
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		if (!(0f > v.x) && !(0f > v.y) && !(0f > v.z))
		{
			bool flag = 0f < v.w;
			object obj = 0 - v.w;
			bool flag2 = obj == null;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			return flag4 & flag3;
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Determinant(Vector2 a, Vector2 b)
	{
		object obj2 = default(object);
		object obj = obj2 * (object)a;
		object obj4 = default(object);
		object obj3 = (object)b * obj4;
		return (float)obj - (float)obj3;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Luminance(Color c)
	{
		float num = c.g * 0.7152f;
		float num2 = c.r * 0.2126f;
		float num3 = c.b * 0.0722f;
		float num4 = num + num2;
		return num4 + num3;
	}

	public static float GetLineSegmentProjectionT(Vector3 a, Vector3 b, Vector3 p)
	{
		float num = b.x - a.x;
		float num2 = b.z - a.z;
		float num3 = p.x - a.x;
		object obj = default(object);
		float num4 = b.y - (float)obj;
		float num5 = p.z - a.z;
		object obj2 = obj - obj;
		float num6 = num3 * num;
		float num7 = num * num;
		float num8 = num5 * num2;
		float num9 = (float)obj2 * num4;
		float num10 = num4 * num4;
		float num11 = num9 + num6;
		float num12 = num2 * num2;
		float num13 = num10 + num7;
		float num14 = num11 + num8;
		float num15 = num13 + num12;
		return num14 / num15;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static PolylinePoint WeightedSum(Vector4 w, PolylinePoint a, PolylinePoint b, PolylinePoint c, PolylinePoint d)
	{
		//IL_002b: Expected native int or pointer, but got O
		PolylinePoint polylinePoint = default(PolylinePoint);
		Vector3 point = default(Vector3);
		((PolylinePoint*)(nint)polylinePoint)->point = point;
		return polylinePoint;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static Vector3 WeightedSum(Vector4 w, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
	{
		//IL_009f: Expected native int or pointer, but got O
		//IL_00ac: Expected native int or pointer, but got O
		float num = w.y * b.z;
		float num2 = w.x * a.z;
		float num3 = num + num2;
		float num4 = w.w;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ stack_30+8]");
		float num5 = num4 * 0f;
		float num6 = w.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ stack_28+8]");
		float num7 = num6 * 0f;
		float num8 = num3 + num7;
		float z = num8 + num5;
		Vector3 vector = default(Vector3);
		float x = default(float);
		((Vector3*)(nint)vector)->x = x;
		((Vector3*)(nint)vector)->z = z;
		return vector;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 WeightedSum(Vector4 w, Vector2 a, Vector2 b, Vector2 c, Vector2 d)
	{
		Vector2 result = default(Vector2);
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static Color WeightedSum(Vector4 w, Color a, Color b, Color c, Color d)
	{
		//IL_0026: Expected native int or pointer, but got O
		Color color = default(Color);
		float r = default(float);
		((Color*)(nint)color)->r = r;
		return color;
	}

	public static bool PointInsideTriangle(Vector2 a, Vector2 b, Vector2 c, Vector2 point, float aMargin = 0f, float bMargin = 0f, float cMargin = 0f)
	{
		//IL_0094: Expected O, but got I4
		//IL_00c3: Expected O, but got I4
		//IL_00f2: Expected O, but got I4
		//IL_00fb: Expected O, but got I4
		//IL_0132: Expected O, but got I4
		//IL_013b: Expected O, but got I4
		//IL_016a: Expected O, but got I4
		//IL_034a: Expected O, but got I4
		//IL_0397: Expected O, but got I4
		//IL_0211: Expected O, but got I4
		//IL_041f: Expected O, but got I4
		//IL_042c: Expected I4, but got O
		object obj = b - a;
		object obj3 = default(object);
		object obj4 = default(object);
		object obj2 = obj3 - obj4;
		object obj5 = obj * obj;
		object obj6 = obj2 * obj2;
		object obj7 = obj5 + obj6;
		object obj8;
		if (0 <= (nint)obj7)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtss xmm3,xmm1\"");
			obj8 = 0;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E810");
			obj8 = obj7;
		}
		object obj10 = default(object);
		object obj9 = obj10 - obj4;
		object obj11 = point - a;
		object obj12 = obj9 * obj9;
		object obj13 = obj11 * obj11;
		object obj14 = obj13 + obj12;
		object obj15;
		if (0 <= (nint)obj14)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtss xmm4,xmm2\"");
			obj15 = 0;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E810");
			obj15 = obj14;
		}
		object obj16 = c - b;
		object obj18 = default(object);
		object obj17 = obj18 - obj3;
		object obj19 = obj16 * obj16;
		object obj20 = obj17 * obj17;
		object obj21 = obj19 + obj20;
		object obj22;
		if (0 <= (nint)obj21)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtss xmm3,xmm2\"");
			obj22 = 0;
			object obj23 = 0;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E810");
			obj22 = obj21;
			object obj23 = obj17;
		}
		object obj24 = obj10 - obj3;
		object obj25 = point - b;
		object obj26 = obj24 * obj24;
		object obj27 = obj25 * obj25;
		object obj28 = obj27 + obj26;
		object obj29;
		if (0 <= (nint)obj28)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtss xmm3,xmm2\"");
			object obj23 = 0;
			obj29 = 0;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E810");
			obj29 = obj28;
		}
		object obj30 = obj4 - obj18;
		object obj31 = a - c;
		object obj32 = obj30 * obj30;
		object obj33 = obj31 * obj31;
		object obj34 = obj33 + obj32;
		object obj35;
		if (0 <= (nint)obj34)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtss xmm0,xmm2\"");
			obj35 = 0;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E810");
			obj35 = obj34;
		}
		object obj36 = obj10 - obj18;
		object obj37 = point - c;
		object obj38 = obj31 / obj35;
		object obj39 = obj30 / obj35;
		object obj40 = obj36 * obj36;
		object obj41 = obj37 * obj37;
		object obj42 = obj41 + obj40;
		object obj43;
		if (0 <= (nint)obj42)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtss xmm0,xmm1\"");
			obj43 = 0;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E810");
			obj43 = obj42;
		}
		object obj44 = obj24 / obj29;
		object obj45 = obj9 / obj15;
		object obj46 = obj25 / obj29;
		object obj47 = obj / obj8;
		object obj48 = obj11 / obj15;
		object obj49 = obj2 / obj8;
		object obj50 = obj37 / obj43;
		object obj51 = obj36 / obj43;
		object obj52 = obj16 / obj22;
		object obj53 = obj45 * obj47;
		object obj54 = obj44 * obj52;
		object obj55 = obj17 / obj22;
		object obj56 = obj48 * obj49;
		object obj57 = obj46 * obj55;
		object obj58 = obj53 - obj56;
		object obj59 = obj54 - obj57;
		object obj60 = default(object);
		bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj60) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj59);
		object obj61 = obj60 - obj59;
		bool flag2 = obj61 == null;
		bool flag3 = !flag;
		bool flag4 = !flag2;
		object obj62 = flag4 & flag3;
		object obj63 = default(object);
		bool flag5 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj63) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj58);
		object obj64 = obj63 - obj58;
		bool flag6 = obj64 == null;
		bool flag7 = !flag5;
		bool flag8 = !flag6;
		object obj65 = flag8 & flag7;
		if (obj65 != obj62)
		{
			return false;
		}
		object obj66 = obj51 * obj38;
		object obj67 = obj50 * obj39;
		object obj68 = obj66 - obj67;
		object obj69 = default(object);
		bool flag9 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj69) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj68);
		object obj70 = obj69 - obj68;
		bool flag10 = obj70 == null;
		object obj71 = flag9 | flag10;
		return (byte)(obj71 ^ obj62) != 0;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static Vector2 Dir(Vector2 a, Vector2 b)
	{
		object obj2 = default(object);
		object obj3 = default(object);
		object obj = obj2 - obj3;
		object obj4 = b - a;
		object obj5 = obj * obj;
		object obj6 = obj4 * obj4;
		object obj7 = obj6 + obj5;
		if (0 <= (nint)obj7)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtss xmm0,xmm1\"");
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E810");
		}
		Vector2 result = default(Vector2);
		return result;
	}

	public static float PolygonSignedArea(List<Vector2> pts)
	{
		//IL_0028: Expected F4, but got I4
		//IL_0031: Expected O, but got I4
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [pts @ rcx (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
		bool flag = (nint)0 <= (nint)0;
		float result = 0f;
		object obj = 0;
		if (!flag)
		{
			object obj5 = default(object);
			object obj6 = default(object);
			object obj8 = default(object);
			object obj9 = default(object);
			object obj11;
			do
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				object obj2 = obj + 1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [pts @ rcx (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
				object obj3 = obj2 % 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				obj++;
				object obj4 = obj5 + obj6;
				object obj7 = obj8 - obj9;
				object obj10 = obj4 * obj7;
				result = 0f + (float)obj10;
				obj11 = obj;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [pts @ rcx (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
			}
			while ((nint)obj11 < 0);
		}
		return result;
	}

	public static Vector2 Rotate(Vector2 v, float angRad)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
		Vector2 result = default(Vector2);
		return result;
	}

	private static float DeltaAngleRad(float a, float b)
	{
		//IL_0064: Invalid comparison between I4 and F4
		//IL_00af: Expected F4, but got I4
		float num = b - a;
		float num2 = num + (float)Math.PI;
		float x = num2 / ((float)Math.PI * 2f);
		float num3 = MathF.Floor(x);
		float num4 = num3 * ((float)Math.PI * 2f);
		float num5 = num2 - num4;
		if (!(0f > num5))
		{
			if (num5 > (float)Math.PI * 2f)
			{
				num5 = (float)Math.PI * 2f;
			}
		}
		else
		{
			num5 = 0f;
		}
		return num5 - (float)Math.PI;
	}

	public static float InverseLerpAngleRad(float a, float b, float v)
	{
		//IL_0064: Invalid comparison between I4 and F4
		//IL_00af: Expected F4, but got I4
		//IL_0226: Invalid comparison between I4 and F4
		//IL_00eb: Expected F4, but got I4
		//IL_0134: Invalid comparison between I4 and F4
		//IL_017f: Expected F4, but got I4
		//IL_00f9: Expected F4, but got I4
		float num = b - a;
		float num2 = num + (float)Math.PI;
		float x = num2 / ((float)Math.PI * 2f);
		float num3 = MathF.Floor(x);
		float num4 = num3 * ((float)Math.PI * 2f);
		float num5 = num2 - num4;
		if (!(0f > num5))
		{
			if (num5 > (float)Math.PI * 2f)
			{
				num5 = (float)Math.PI * 2f;
			}
		}
		else
		{
			num5 = 0f;
		}
		float num6 = num5 - (float)Math.PI;
		float num7 = num6 * 0.5f;
		float num8 = num6 + a;
		float num9 = num7 + a;
		float num10 = v - num9;
		float num11 = num10 + (float)Math.PI;
		float x2 = num11 / ((float)Math.PI * 2f);
		float num12 = MathF.Floor(x2);
		float num13 = num12 * ((float)Math.PI * 2f);
		float num14 = num11 - num13;
		if (!(0f > num14))
		{
			if (num14 > (float)Math.PI * 2f)
			{
				num14 = (float)Math.PI * 2f;
			}
		}
		else
		{
			num14 = 0f;
		}
		float num15 = num14 - (float)Math.PI;
		float num16 = num15 + num9;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000181062CE5h\"");
		if (a == num8)
		{
			return 0f;
		}
		float num17 = num16 - a;
		float num18 = num8 - a;
		float num19 = num17 / num18;
		if (!(0f > num19))
		{
			if (num19 > 1f)
			{
				num19 = 1f;
			}
		}
		else
		{
			num19 = 0f;
		}
		return num19;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static Vector2 Lerp(Vector2 a, Vector2 b, Vector2 t)
	{
		//IL_0026: Invalid comparison between O and F4
		//IL_004b: Invalid comparison between O and F4
		if (0 > (nint)t || System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref t) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1f))
		{
		}
		object obj = default(object);
		if (0 > (nint)obj || System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1f))
		{
		}
		Vector2 result = default(Vector2);
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Lerp(Rect r, Vector2 t)
	{
		//IL_002b: Invalid comparison between O and F4
		//IL_0050: Invalid comparison between O and F4
		if (0 > (nint)t || System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref t) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1f))
		{
		}
		object obj = default(object);
		if (0 > (nint)obj || System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1f))
		{
		}
		Vector2 result = default(Vector2);
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static Vector2 InverseLerp(Vector2 a, Vector2 b, Vector2 v)
	{
		Vector2 result = default(Vector2);
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 InverseLerp(Rect r, Vector2 pt)
	{
		//IL_010f: Invalid comparison between I4 and F4
		//IL_011e: Expected O, but got I4
		//IL_0055: Expected O, but got I4
		//IL_0147: Expected O, but got I4
		//IL_0099: Invalid comparison between O and F4
		//IL_015e: Expected O, but got I4
		float num = r.m_Width + r.m_XMin;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000181062E53h\"");
		object obj;
		if (r.m_XMin == num)
		{
			obj = 0;
		}
		else
		{
			float num2 = num - r.m_XMin;
			float num3 = (float)pt - r.m_XMin;
			float num4 = num3 / num2;
			bool flag = 0f > num4;
			obj = 0;
			if (!flag)
			{
				bool flag2 = !(num4 > 1f);
				obj = 0;
				if (!flag2)
				{
					obj = 0;
				}
			}
		}
		float num5 = r.m_Height + r.m_YMin;
		bool flag3 = r.m_YMin == num5;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000181062E26h\"");
		Vector2 result = default(Vector2);
		if (!flag3)
		{
			float num6 = num5 - r.m_YMin;
			object obj2 = default(object);
			float num7 = (float)obj2 - r.m_YMin;
			float num8 = num7 / num6;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num8) && !(num8 > 1f))
			{
				return result;
			}
		}
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static Vector2 Remap(Vector2 iMin, Vector2 iMax, Vector2 oMin, Vector2 oMax, Vector2 value)
	{
		Vector2 t = default(Vector2);
		return Lerp(oMin, oMax, t);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Remap(Rect iRect, Rect oRect, Vector2 iPos)
	{
		Vector2 vector = default(Vector2);
		return Lerp(vector, vector, vector);
	}

	public unsafe static Vector3 Abs(Vector3 v)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected F4, but got Unknown
		//IL_0022: Expected native int or pointer, but got O
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected F4, but got Unknown
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected F4, but got Unknown
		//IL_0063: Expected native int or pointer, but got O
		//IL_0070: Expected native int or pointer, but got O
		float x = v.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		float x2 = x & 0;
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->x = x2;
		float z = v.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		float z2 = z & 0;
		float y = v.y;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		float y2 = y & 0;
		((Vector3*)(nint)vector)->z = z2;
		((Vector3*)(nint)vector)->y = y2;
		return vector;
	}

	public static float RandomGaussian(float min = 0f, float max = 1f)
	{
		//IL_0051: Invalid comparison between I4 and F4
		//IL_0078: Expected F4, but got I4
		float num = min + max;
		float num2 = num * 0.5f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FAE0");
		float num4 = default(float);
		float num3 = num4 * -2f;
		float num5 = num3 / num4;
		float num6;
		if (!(0f > num5))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtss xmm0,xmm1\"");
			num6 = 0f;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E810");
			num6 = num5;
		}
		float num8 = default(float);
		float num7 = num6 * num8;
		float num9 = max - num2;
		float num10 = num9 / 3f;
		float num11 = num7 * num10;
		float num12 = num11 + num2;
		if (!(min > num12))
		{
			if (num12 > max)
			{
				num12 = max;
			}
		}
		else
		{
			num12 = min;
		}
		return num12;
	}

	public unsafe static Vector3 GetRandomPerpendicularVector(Vector3 a)
	{
		//IL_0022: Expected native int or pointer, but got O
		//IL_0030: Expected native int or pointer, but got O
		//IL_0055: Expected native int or pointer, but got O
		//IL_0074: Expected native int or pointer, but got O
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Expected O, but got Unknown
		//IL_00e7: Invalid comparison between O and F4
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->x = 0f;
		((Vector3*)(nint)vector)->z = 0f;
		object obj2 = default(object);
		object obj3 = default(object);
		object obj4;
		do
		{
			Vector3 onUnitSphere = UnityEngine.Random.onUnitSphere;
			((Vector3*)(nint)vector)->x = onUnitSphere.x;
			object obj = obj2 * obj3;
			((Vector3*)(nint)vector)->z = onUnitSphere.z;
			float num = onUnitSphere.x * a.x;
			float num2 = (float)obj + num;
			float num3 = onUnitSphere.z * a.z;
			float num4 = num2 + num3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			obj4 = num4 & 0;
		}
		while (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.98f));
		return vector;
	}

	public static IEnumerable<PolylinePoint> GetArcPoints(PolylinePoint a, PolylinePoint b, Vector3 normA, Vector3 normB, Vector3 center, float radius, int count)
	{
		//IL_00e2: Expected I4, but got I8
		//IL_0055: Expected O, but got F4
		//IL_0071: Expected O, but got F4
		_003CGetArcPoints_003Ed__35 obj = new _003CGetArcPoints_003Ed__35(0);
		obj._003C_003E1__state = -2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
		int num = default(int);
		obj._003C_003El__initialThreadId = num;
		obj._003C_003E3__a = (PolylinePoint)a.point;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [a @ rcx (Shapes.PolylinePoint)+10]");
		_ = 0;
		obj._003C_003E3__b = (PolylinePoint)b.point;
		obj._003C_003E3__normA = (Vector3)normA.x;
		_ = normA.z;
		obj._003C_003E3__normB = (Vector3)normB.x;
		_ = normB.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [b @ rdx (Shapes.PolylinePoint)+10]");
		_ = 0;
		object obj2 = default(object);
		obj._003C_003E3__center = (Vector3)obj2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ stack_28+8]");
		_ = 0;
		int num2 = default(int);
		obj._003C_003E3__count = num2;
		float num3 = default(float);
		obj._003C_003E3__radius = num3;
		return obj;
	}

	public static IEnumerable<Vector3> GetArcPoints(Vector3 normA, Vector3 normB, Vector3 center, float radius, int count)
	{
		//IL_00a6: Expected I4, but got I8
		//IL_0024: Expected O, but got F4
		//IL_0036: Expected O, but got F4
		//IL_0052: Expected O, but got F4
		_003CGetArcPoints_003Ed__36 obj = new _003CGetArcPoints_003Ed__36(0);
		obj._003C_003E1__state = -2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
		int num = default(int);
		obj._003C_003El__initialThreadId = num;
		obj._003C_003E3__normA = (Vector3)normA.x;
		obj._003C_003E3__normB = (Vector3)normB.x;
		_ = normB.z;
		obj._003C_003E3__center = (Vector3)center.x;
		_ = center.z;
		int num2 = default(int);
		obj._003C_003E3__count = num2;
		obj._003C_003E3__radius = radius;
		_ = normA.z;
		return obj;
	}

	public static IEnumerable<Vector2> GetArcPoints(Vector2 normA, Vector2 normB, Vector2 center, float radius, int count)
	{
		//IL_0088: Expected I4, but got I8
		_003CGetArcPoints_003Ed__37 obj = new _003CGetArcPoints_003Ed__37(0);
		obj._003C_003E1__state = -2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
		obj._003C_003E3__normA = normA;
		int num = default(int);
		obj._003C_003El__initialThreadId = num;
		obj._003C_003E3__normB = normB;
		obj._003C_003E3__radius = radius;
		int num2 = default(int);
		obj._003C_003E3__count = num2;
		obj._003C_003E3__center = center;
		return obj;
	}

	public static IEnumerable<PolylinePoint> CubicBezierPointsSkipFirst(PolylinePoint a, PolylinePoint b, PolylinePoint c, PolylinePoint d, int count)
	{
		//IL_00c1: Expected I4, but got I8
		_003CCubicBezierPointsSkipFirst_003Ed__38 obj = new _003CCubicBezierPointsSkipFirst_003Ed__38(0);
		obj._003C_003E1__state = -2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
		int num = default(int);
		obj._003C_003El__initialThreadId = num;
		obj._003C_003E3__a = (PolylinePoint)a.point;
		int num2 = default(int);
		obj._003C_003E3__count = num2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [a @ rcx (Shapes.PolylinePoint)+10]");
		_ = 0;
		obj._003C_003E3__b = (PolylinePoint)b.point;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [b @ rdx (Shapes.PolylinePoint)+10]");
		_ = 0;
		obj._003C_003E3__c = (PolylinePoint)c.point;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [c @ r8 (Shapes.PolylinePoint)+10]");
		_ = 0;
		obj._003C_003E3__d = (PolylinePoint)d.point;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [d @ r9 (Shapes.PolylinePoint)+10]");
		_ = 0;
		return obj;
	}

	public static IEnumerable<PolylinePoint> CubicBezierPointsSkipFirstMatchStyle(PolylinePoint style, Vector3 a, Vector3 b, Vector3 c, Vector3 d, int count)
	{
		//IL_00d2: Expected I4, but got I8
		//IL_0036: Expected O, but got F4
		//IL_0052: Expected O, but got F4
		//IL_006e: Expected O, but got F4
		_003CCubicBezierPointsSkipFirstMatchStyle_003Ed__39 obj = new _003CCubicBezierPointsSkipFirstMatchStyle_003Ed__39(0);
		obj._003C_003E1__state = -2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
		int num = default(int);
		obj._003C_003El__initialThreadId = num;
		obj._003C_003E3__style = (PolylinePoint)style.point;
		obj._003C_003E3__a = (Vector3)a.x;
		_ = a.z;
		obj._003C_003E3__b = (Vector3)b.x;
		_ = b.z;
		obj._003C_003E3__c = (Vector3)c.x;
		_ = c.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [style @ rcx (Shapes.PolylinePoint)+10]");
		_ = 0;
		object obj2 = default(object);
		obj._003C_003E3__d = (Vector3)obj2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ stack_28+8]");
		_ = 0;
		int num2 = default(int);
		obj._003C_003E3__count = num2;
		return obj;
	}

	public static IEnumerable<Vector3> CubicBezierPointsSkipFirst(Vector3 a, Vector3 b, Vector3 c, Vector3 d, int count)
	{
		//IL_00b5: Expected I4, but got I8
		//IL_0024: Expected O, but got F4
		//IL_0036: Expected O, but got F4
		//IL_0052: Expected O, but got F4
		//IL_006e: Expected O, but got F4
		_003CCubicBezierPointsSkipFirst_003Ed__40 obj = new _003CCubicBezierPointsSkipFirst_003Ed__40(0);
		obj._003C_003E1__state = -2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
		int num = default(int);
		obj._003C_003El__initialThreadId = num;
		obj._003C_003E3__a = (Vector3)a.x;
		obj._003C_003E3__b = (Vector3)b.x;
		_ = b.z;
		obj._003C_003E3__c = (Vector3)c.x;
		_ = c.z;
		obj._003C_003E3__d = (Vector3)d.x;
		_ = d.z;
		int num2 = default(int);
		obj._003C_003E3__count = num2;
		_ = a.z;
		return obj;
	}

	public static IEnumerable<Vector2> CubicBezierPointsSkipFirst(Vector2 a, Vector2 b, Vector2 c, Vector2 d, int count)
	{
		//IL_008d: Expected I4, but got I8
		_003CCubicBezierPointsSkipFirst_003Ed__41 obj = new _003CCubicBezierPointsSkipFirst_003Ed__41(0);
		obj._003C_003E1__state = -2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
		obj._003C_003E3__a = a;
		obj._003C_003E3__b = b;
		int num = default(int);
		obj._003C_003El__initialThreadId = num;
		obj._003C_003E3__c = c;
		int num2 = default(int);
		obj._003C_003E3__count = num2;
		obj._003C_003E3__d = d;
		return obj;
	}

	public unsafe static Vector4 GetCubicBezierWeights(float t)
	{
		//IL_0092: Expected native int or pointer, but got O
		//IL_009f: Expected native int or pointer, but got O
		//IL_00ac: Expected native int or pointer, but got O
		//IL_00b9: Expected native int or pointer, but got O
		float num = 1f - t;
		float num2 = t * t;
		float num3 = num * num;
		float num4 = num3 * 3f;
		float x = num3 * num;
		float num5 = num * 3f;
		float y = num4 * t;
		float z = num5 * num2;
		float w = num2 * t;
		Vector4 vector = default(Vector4);
		((Vector4*)(nint)vector)->x = x;
		((Vector4*)(nint)vector)->y = y;
		((Vector4*)(nint)vector)->w = w;
		((Vector4*)(nint)vector)->z = z;
		return vector;
	}

	public unsafe static PolylinePoint CubicBezier(PolylinePoint a, PolylinePoint b, PolylinePoint c, PolylinePoint d, float t)
	{
		//IL_00c9: Expected native int or pointer, but got O
		//IL_0029: Invalid comparison between O and F4
		//IL_00a5: Expected native int or pointer, but got O
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_0075: Expected O, but got Ref
		//IL_0086: Expected native int or pointer, but got O
		object obj = default(object);
		PolylinePoint polylinePoint = default(PolylinePoint);
		if (0 < (nint)obj)
		{
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1f))
			{
				object obj2 = default(object);
				PolylinePoint b2 = (PolylinePoint)(obj2 - 104);
				PolylinePoint a2 = (PolylinePoint)(obj2 - 72);
				object obj3 = default(object);
				PolylinePoint c2 = default(PolylinePoint);
				PolylinePoint d2 = default(PolylinePoint);
				((PolylinePoint*)(nint)polylinePoint)->point = WeightedSum((Vector4)(&obj3), a2, b2, c2, d2).point;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rax_v6 (Shapes.PolylinePoint)+10]");
				_ = 0;
				return polylinePoint;
			}
			object point = default(object);
			((PolylinePoint*)(nint)polylinePoint)->point = (Vector3)point;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v21 @ stack_28+10]");
			_ = 0;
			return polylinePoint;
		}
		((PolylinePoint*)(nint)polylinePoint)->point = a.point;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [a @ rdx (Shapes.PolylinePoint)+10]");
		_ = 0;
		return polylinePoint;
	}

	public unsafe static Vector3 CubicBezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
	{
		//IL_0197: Expected native int or pointer, but got O
		//IL_01a9: Expected native int or pointer, but got O
		//IL_0029: Invalid comparison between O and F4
		//IL_0170: Expected F4, but got O
		//IL_016b: Expected native int or pointer, but got O
		//IL_0185: Expected F4, but got I
		//IL_0180: Expected native int or pointer, but got O
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_013d: Expected native int or pointer, but got O
		//IL_0159: Expected native int or pointer, but got O
		object obj = default(object);
		Vector3 vector = default(Vector3);
		if (0 < (nint)obj)
		{
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1f))
			{
				float num = 1f - (float)obj;
				object obj2 = obj * obj;
				float num2 = num * num;
				float num3 = num2 * 3f;
				float num4 = num2 * num;
				float num5 = num * 3f;
				float num6 = num3 * (float)obj;
				float num7 = num5 * (float)obj2;
				object obj3 = obj2 * obj;
				float num8 = b.z * num6;
				float num9 = c.z * num7;
				float num10 = a.z * num4;
				float num11 = num8 + num10;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ stack_28+8]");
				object obj4 = 0 * obj3;
				float num12 = num11 + num9;
				float x = default(float);
				((Vector3*)(nint)vector)->x = x;
				float z = num12 + (float)obj4;
				((Vector3*)(nint)vector)->z = z;
				return vector;
			}
			object obj5 = default(object);
			((Vector3*)(nint)vector)->x = (float)obj5;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ stack_28+8]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		((Vector3*)(nint)vector)->x = a.x;
		((Vector3*)(nint)vector)->z = a.z;
		return vector;
	}

	public static Vector2 CubicBezier(Vector2 a, Vector2 b, Vector2 c, Vector2 d, float t)
	{
		//IL_0029: Invalid comparison between O and F4
		object obj = default(object);
		if (0 < (nint)obj)
		{
			Vector2 result = default(Vector2);
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1f))
			{
				return result;
			}
			return d;
		}
		return a;
	}

	private unsafe static Vector3 CubicBezierDirectionIsh(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_016a: Expected native int or pointer, but got O
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Expected O, but got Unknown
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Expected O, but got Unknown
		//IL_0223: Expected native int or pointer, but got O
		//IL_0230: Expected native int or pointer, but got O
		object obj = default(object);
		float num = 1f - (float)obj;
		object obj2 = obj * obj;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj3 = num ^ 0;
		float num2 = (float)obj2 * 3f;
		float num3 = (float)obj3 * num;
		float num4 = (float)obj * 4f;
		object obj4 = obj + obj;
		float num5 = num2 - num4;
		float num6 = (float)obj4 - num2;
		float num7 = num5 + 1f;
		float num8 = num3 * a.x;
		float num9 = num6 * c.x;
		float num10 = num7 * b.x;
		float num11 = num8 + num10;
		object obj6 = default(object);
		object obj5 = obj2 * obj6;
		float num12 = num11 + num9;
		float num13 = num6 * c.y;
		float num14 = num6 * c.z;
		float x = num12 + (float)obj5;
		float num15 = num3 * a.y;
		float num16 = num3 * a.z;
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->x = x;
		float num17 = num7 * b.y;
		float num18 = num7 * b.z;
		float num19 = num17 + num15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ stack_28+8]");
		object obj7 = obj2 * 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ stack_28+4]");
		object obj8 = obj2 * 0;
		float num20 = num18 + num16;
		float num21 = num19 + num13;
		float num22 = num20 + num14;
		float y = num21 + (float)obj8;
		float z = num22 + (float)obj7;
		((Vector3*)(nint)vector)->y = y;
		((Vector3*)(nint)vector)->z = z;
		return vector;
	}

	public static float GetApproximateAngularCurveSumDegrees(Vector3 a, Vector3 b, Vector3 c, Vector3 d, int vertCount)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_006a: Expected O, but got I
		//IL_00b7: Expected F4, but got I4
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Expected O, but got Unknown
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Expected O, but got Unknown
		//IL_012c: Expected F4, but got I4
		//IL_0135: Expected O, but got I4
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Expected O, but got Unknown
		//IL_034b: Expected I, but got O
		//IL_01ad: Invalid comparison between F4 and I4
		//IL_02a3: Expected F4, but got I4
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Expected O, but got Unknown
		//IL_0418: Expected F4, but got I
		object obj2 = default(object);
		object obj = obj2 - 79;
		_ = b.x;
		float num = b.x - a.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+77]");
		object obj3 = -1;
		_ = a.x;
		object obj4 = default(object);
		float num2 = (float)obj4 - (float)obj4;
		float num3 = b.z - a.z;
		bool flag = (nint)obj3 <= 1;
		float num4 = 0f;
		if (!flag)
		{
			_ = d.x;
			_ = d.z;
			_ = c.x;
			_ = c.z;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+77]");
			float num5 = 0f - 1f;
			num4 = 0f;
			object obj5 = 1;
			object obj7 = default(object);
			object obj8 = default(object);
			float num36;
			bool flag2;
			do
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm5,ebx\"");
				float num6 = 0f / num5;
				float num7 = 1f - num6;
				float num8 = num6 * num6;
				float num9 = num8 * 3f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
				object obj6 = num7 ^ 0;
				float num10 = (float)obj6 * num7;
				float num11 = num6 * 4f;
				float num12 = num6 + num6;
				float num13 = num9 - num11;
				float num14 = a.x * num10;
				float num15 = num12 - num9;
				float num16 = num13 + 1f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-79]");
				float num17 = 0f * num15;
				float num18 = b.x * num16;
				float num19 = (float)obj7 * num16;
				float num20 = num18 + num14;
				float num21 = b.z * num16;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-69]");
				float num22 = 0f * num8;
				float num23 = num20 + num17;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-75]");
				float num24 = 0f * num15;
				float num25 = num23 + num22;
				float num26 = (float)obj8 * num10;
				float num27 = num19 + num26;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-65]");
				float num28 = 0f * num8;
				float num29 = num27 + num24;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-71]");
				float num30 = 0f * num15;
				float num31 = num29 + num28;
				float num32 = a.z * num10;
				float num33 = num21 + num32;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-61]");
				float num34 = 0f * num8;
				float num35 = num33 + num30;
				num36 = num35 + num34;
				nint num37 = (nint)typeof(Math);
				float num38 = num * num;
				float num39 = num3 * num3;
				float num40 = num2 * num2;
				float num41 = num31 * num31;
				float num42 = num40 + num38;
				float num43 = num25 * num25;
				float num44 = num42 + num39;
				float num45 = num41 + num43;
				float num46 = num36 * num36;
				float num47 = num45 + num46;
				float num48 = num47 * num44;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v305 @ rcx_v5 (Il2CppClass<System.Math>)+E4]");
				if ((nint)0 <= (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
				}
				else
				{
					double num49 = Math.Sqrt(num48);
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm2,xmm0\"");
				float num57;
				if (!(1E-15f > 0f))
				{
					float num50 = num31 * num2;
					float num51 = num25 * num;
					float num52 = num36 * num3;
					float num53 = num50 + num51;
					float num54 = num53 + num52;
					float num55 = num54 / 0f;
					if (-1f > num55 || num55 > 1f)
					{
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
					double num56 = Math.Acos(0.0);
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
					num57 = 0f * 57.29578f;
				}
				else
				{
					num57 = 0f;
				}
				obj5++;
				num4 += num57;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+77]");
				num5 = 0f;
				flag2 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3);
				num3 = num36;
				num = num25;
				num2 = num31;
			}
			while (flag2);
			num3 = num36;
		}
		object obj9 = obj - 105;
		object obj10 = obj - 121;
		float num58 = d.z - c.z;
		_ = d.x;
		object obj11 = obj4 - obj4;
		_ = c.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180ABFA60");
		return (float)obj4 + num4;
	}

	public unsafe static Matrix4x4 AffineMtxMul(Matrix4x4 lhs, Matrix4x4 rhs)
	{
		//IL_008b: Expected native int or pointer, but got O
		//IL_011b: Expected native int or pointer, but got O
		//IL_0197: Expected native int or pointer, but got O
		//IL_0231: Expected native int or pointer, but got O
		//IL_02b7: Expected native int or pointer, but got O
		//IL_033d: Expected native int or pointer, but got O
		//IL_03af: Expected native int or pointer, but got O
		//IL_043f: Expected native int or pointer, but got O
		//IL_04c5: Expected native int or pointer, but got O
		//IL_04f1: Expected native int or pointer, but got O
		//IL_0509: Expected native int or pointer, but got O
		//IL_0517: Expected native int or pointer, but got O
		//IL_0525: Expected native int or pointer, but got O
		//IL_0583: Expected native int or pointer, but got O
		//IL_05f5: Expected native int or pointer, but got O
		//IL_067b: Expected native int or pointer, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm5\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm2,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm4\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm2,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm2,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm2\"");
		Matrix4x4 matrix4x = default(Matrix4x4);
		((Matrix4x4*)(nint)matrix4x)->m00 = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm5\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm2,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm4\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm2,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm3,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm2,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm2\"");
		((Matrix4x4*)(nint)matrix4x)->m01 = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm5\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm4\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm3,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm2,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm3,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm3\"");
		((Matrix4x4*)(nint)matrix4x)->m02 = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm8\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm5\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm2,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm4\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm2,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm2,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm2\"");
		((Matrix4x4*)(nint)matrix4x)->m03 = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm5\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm2,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm4\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm2,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm2,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm2\"");
		((Matrix4x4*)(nint)matrix4x)->m10 = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm3,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm8\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm5\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm3,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm4\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm4,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm3,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm7\"");
		((Matrix4x4*)(nint)matrix4x)->m11 = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm4,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm8\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm5\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm8\"");
		((Matrix4x4*)(nint)matrix4x)->m12 = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm4,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm4,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm9\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm2,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm5\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm0\"");
		((Matrix4x4*)(nint)matrix4x)->m13 = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm4\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm5\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm2,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm2,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm2\"");
		((Matrix4x4*)(nint)matrix4x)->m20 = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm3,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,xmm0\"");
		((Matrix4x4*)(nint)matrix4x)->m30 = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm8\"");
		((Matrix4x4*)(nint)matrix4x)->m31 = 0f;
		((Matrix4x4*)(nint)matrix4x)->m32 = 0f;
		((Matrix4x4*)(nint)matrix4x)->m33 = 1f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm5\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm3,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm4\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm4,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm3,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm7\"");
		((Matrix4x4*)(nint)matrix4x)->m21 = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm4,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm8\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm5\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm8\"");
		((Matrix4x4*)(nint)matrix4x)->m22 = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm4,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm4,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm9\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm5\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm0\"");
		((Matrix4x4*)(nint)matrix4x)->m23 = 0f;
		return matrix4x;
	}

	public static float Cosinc(float x)
	{
		//IL_008a: Expected I, but got O
		//IL_007c: Expected F4, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm6,xmm0\"");
		nint num = (nint)typeof(Math);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rcx_v2 (Il2CppClass<System.Math>)+E4]");
		if ((nint)0 <= (nint)0)
		{
			double num2 = Math.Cos(0.0);
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm6\"");
			return 1f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,qword ptr [182206D18h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm0,qword ptr [1822E8448h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,xmm0\"");
		return 0f;
	}

	public static double Cosinc(double x)
	{
		//IL_007a: Expected I, but got O
		nint num = (nint)typeof(Math);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rcx_v2 (Il2CppClass<System.Math>)+E4]");
		if ((nint)0 <= (nint)0)
		{
			double num2 = Math.Cos(x);
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm6\"");
			return 1.0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [182206D18h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,qword ptr [1822E8448h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm1\"");
		return x;
	}

	public static float Sinc(float x)
	{
		//IL_008f: Expected I, but got O
		//IL_0081: Expected F4, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm6,xmm0\"");
		nint num = (nint)typeof(Math);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rcx_v2 (Il2CppClass<System.Math>)+E4]");
		if ((nint)0 <= (nint)0)
		{
			float result = (float)Math.Sin(0.0);
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm0,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
			return result;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,qword ptr [1822E8458h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [1822E8440h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,qword ptr [182206E88h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm6\"");
		return 0f;
	}

	public static double Sinc(double x)
	{
		//IL_0075: Expected I, but got O
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected F8, but got Unknown
		nint num = (nint)typeof(Math);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206ED0]");
		double num2 = x & 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rcx_v2 (Il2CppClass<System.Math>)+E4]");
		if ((nint)0 <= (nint)0)
		{
			double result = Math.Sin(num2);
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm0,xmm6\"");
			return result;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,qword ptr [1822E8458h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [1822E8440h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,qword ptr [182206E88h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm6\"");
		return num2;
	}

	internal unsafe static PolylinePoint _003CGetArcPoints_003Eg__DirToPt_007C35_0(Vector3 dir, float t, ref _003C_003Ec__DisplayClass35_0 P_2)
	{
		//IL_0009: Invalid comparison between I4 and F4
		//IL_003a: Expected O, but got I
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Expected O, but got Unknown
		//IL_00cd: Expected native int or pointer, but got O
		//IL_00df: Expected native int or pointer, but got O
		//IL_006c: Expected O, but got I
		//IL_007c: Expected O, but got I
		Vector3 point;
		Vector3 vector2 = default(Vector3);
		if (!(0f < t))
		{
			point = (Vector3)P_2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass35_0&)+10]");
			Vector3 vector = (Vector3)0;
		}
		else if (!(t < 1f))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass35_0&)+20]");
			point = (Vector3)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass35_0&)+30]");
			Vector3 vector = (Vector3)0;
		}
		else
		{
			Vector3 vector = vector2;
			point = vector2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass35_0&)+4C]");
		object obj = 0 * dir.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass35_0&)+48]");
		object obj2 = obj + 0;
		PolylinePoint polylinePoint = default(PolylinePoint);
		((PolylinePoint*)(nint)polylinePoint)->point = point;
		((PolylinePoint*)(nint)polylinePoint)->point = vector2;
		return polylinePoint;
	}

	internal unsafe static Vector3 _003CGetArcPoints_003Eg__DirToPt_007C36_0(Vector3 dir, ref _003C_003Ec__DisplayClass36_0 P_1)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		//IL_0039: Expected native int or pointer, but got O
		//IL_0046: Expected native int or pointer, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r8 (<>c__DisplayClass36_0&)+C]");
		object obj = 0 * dir.z;
		float num = (float)obj;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r8 (<>c__DisplayClass36_0&)+8]");
		float z = num + 0f;
		Vector3 vector = default(Vector3);
		float x = default(float);
		((Vector3*)(nint)vector)->x = x;
		((Vector3*)(nint)vector)->z = z;
		return vector;
	}

	internal static Vector2 _003CGetArcPoints_003Eg__DirToPt_007C37_0(Vector2 dir, ref _003C_003Ec__DisplayClass37_0 P_1)
	{
		Vector2 result = default(Vector2);
		return result;
	}
}
