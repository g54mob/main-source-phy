using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cpp2ILInjected;
using UnityEngine;

[Serializable]
public class Zone
{
	public enum ZoneShapes
	{
		Single,
		Composite
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Predicate<ZoneRegion> _003C_003E9__14_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal bool _003CGetRandomGridPosition_003Eb__14_0(ZoneRegion r)
		{
			//IL_0044: Expected I4, but got O
			if (r != null)
			{
				return r.RegionType == ZoneRegion.RegionTypes.Add;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003C_003Ec__DisplayClass11_0
	{
		public FireMission fireMission;

		public Vector3[] gridBounds;

		public float closest;

		public Vector2 locationPos;
	}

	public string ID;

	public string Name;

	public EntityRoles Role;

	public ZoneShapes ZoneShape;

	public GridReference BottomLeft;

	public float Width = 10f;

	public float Height = 10f;

	public List<ZoneRegion> Regions;

	public unsafe bool Contains(GridReference location)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Expected O, but got Unknown
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Expected O, but got Unknown
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Expected O, but got Unknown
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Expected O, but got Unknown
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Expected O, but got Unknown
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Expected O, but got Unknown
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Expected O, but got Unknown
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Expected O, but got Unknown
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Expected O, but got Unknown
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Expected O, but got Unknown
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Expected O, but got Unknown
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Expected O, but got Unknown
		//IL_01c4: Expected O, but got Ref
		//IL_01e2: Expected O, but got I
		//IL_0201: Expected O, but got Ref
		//IL_0227: Expected O, but got I
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Expected O, but got Unknown
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Expected O, but got Unknown
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Expected O, but got Unknown
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Expected O, but got Unknown
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Expected I, but got Unknown
		//IL_02fe: Expected O, but got I
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Expected O, but got Unknown
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Expected O, but got Unknown
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Expected O, but got Unknown
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Expected O, but got Unknown
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Expected O, but got Unknown
		bool flag = location == null;
		Zone zone = this;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
			object obj = (object)location >> 2;
			object obj2 = obj >> 31;
			object obj3 = obj + obj2;
			object obj4 = obj3 * 4;
			object obj5 = obj3 + obj4;
			object obj6 = obj5 * 2;
			object obj7 = location.X + obj6;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
			object obj8 = obj3 >> 2;
			object obj9 = obj8 >> 31;
			object obj10 = obj8 + obj9;
			object obj11 = obj10 * 4;
			object obj12 = obj10 + obj11;
			object obj13 = obj12 + obj12;
			object obj14 = location.Location - obj13;
			object obj15 = obj14 * 4;
			zone = (Zone)(object)(obj14 + obj15);
			object obj16 = zone * 2;
			object obj17 = location.Y + obj16;
			if (ZoneShape != ZoneShapes.Single && Regions != null)
			{
				List<ZoneRegion> regions = Regions;
				if (regions._size != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					nint num = 0;
					bool result = false;
					List<ZoneRegion>.Enumerator enumerator = default(List<ZoneRegion>.Enumerator);
					object obj18 = default(object);
					while (true)
					{
						if (enumerator.MoveNext())
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
							bool flag2 = obj18 == null;
							zone = (Zone)(&enumerator);
							if (!flag2)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ stack_20_v5+18]");
								object obj19 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ stack_20_v5+18]");
								bool flag3 = (nint)0 == 0;
								zone = (Zone)(&enumerator);
								if (!flag3)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
									object obj20 = (nint)(&obj18) >> 2;
									object obj21 = obj20 >> 31;
									object obj22 = obj20 + obj21;
									object obj23 = obj22 * 4;
									object obj24 = obj22 + obj23;
									object obj25 = obj24 * 2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ r10_v6+14]");
									object obj26 = 0 + obj25;
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
									object obj27 = obj22 >> 2;
									object obj28 = obj27 >> 31;
									object obj29 = obj27 + obj28;
									object obj30 = obj29 * 4;
									object obj31 = obj29 + obj30;
									object obj32 = obj31 + obj31;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ r10_v6+10]");
									num = (nint)(0 - obj32);
									object obj33 = num * 4;
									object obj34 = num + obj33;
									object obj35 = obj34 * 2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ r10_v6+18]");
									object obj36 = 0 + obj35;
									if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj26))
									{
										continue;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ stack_20_v5+20]");
									object obj37 = obj26 + 0;
									if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj37) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7) || System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj17) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj36))
									{
										continue;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ stack_20_v5+24]");
									object obj38 = obj36 + 0;
									if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj38) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj17))
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ stack_20_v5+10]");
										if ((nint)0 == 1)
										{
											break;
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ stack_20_v5+10]");
										if ((nint)0 == 0)
										{
											result = true;
										}
									}
									continue;
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						enumerator.Dispose();
						return result;
					}
					enumerator.Dispose();
					goto IL_064b;
				}
			}
			GridReference bottomLeft = BottomLeft;
			if (BottomLeft != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
				object obj39 = obj10 >> 2;
				object obj40 = obj39 >> 31;
				object obj41 = obj39 + obj40;
				object obj42 = obj41 * 4;
				object obj43 = obj41 + obj42;
				object obj44 = obj43 * 2;
				object obj45 = bottomLeft.X + obj44;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
				object obj46 = obj43 >> 2;
				object obj47 = obj46 >> 31;
				object obj48 = obj46 + obj47;
				object obj49 = obj48 * 4;
				object obj50 = obj48 + obj49;
				object obj51 = obj50 + obj50;
				object obj52 = bottomLeft.Location - obj51;
				object obj53 = obj52 * 4;
				object obj54 = obj52 + obj53;
				object obj55 = obj54 * 2;
				object obj56 = bottomLeft.Y + obj55;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj45))
				{
					object obj57 = obj45 + Width;
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj57) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7) && System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj17) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj56))
					{
						object obj58 = obj56 + Height;
						bool flag4 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj58) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj17);
						object obj59 = obj58 - obj17;
						bool flag5 = obj59 == null;
						bool flag6 = !flag4;
						bool flag7 = !flag5;
						return flag7 & flag6;
					}
				}
				goto IL_064b;
			}
		}
		throw new NullReferenceException();
		IL_064b:
		return false;
	}

	private static bool ContainsRect(int x, int y, GridReference bottomLeft, float width, float height)
	{
		//IL_020b: Expected I4, but got O
		//IL_0050: Expected O, but got I4
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Expected O, but got Unknown
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Expected O, but got Unknown
		//IL_0153: Invalid comparison between F4 and I4
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Expected O, but got Unknown
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Expected O, but got Unknown
		if (bottomLeft != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r9d\"");
			int num = y >> 2;
			int num2 = num >> 31;
			object obj = num + num2;
			object obj2 = obj * 4;
			object obj3 = obj + obj2;
			object obj4 = obj3 + obj3;
			object obj5 = bottomLeft.Location - obj4;
			object obj6 = obj5 * 4;
			object obj7 = obj5 + obj6;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r9d\"");
			object obj8 = obj7 + obj7;
			object obj9 = obj5 >> 2;
			object obj10 = obj9 >> 31;
			object obj11 = obj9 + obj10;
			object obj12 = obj11 * 4;
			object obj13 = obj11 + obj12;
			object obj14 = obj13 * 2;
			object obj15 = bottomLeft.X + obj14;
			if (x >= (nint)obj15)
			{
				float num3 = (float)obj15 + width;
				if (num3 > (float)x)
				{
					object obj16 = bottomLeft.Y + obj8;
					if (y >= (nint)obj16)
					{
						object obj18 = default(object);
						object obj17 = obj16 + obj18;
						bool flag = (nint)obj17 < y;
						object obj19 = obj17 - y;
						bool flag2 = obj19 == null;
						bool flag3 = !flag;
						bool flag4 = !flag2;
						return flag4 & flag3;
					}
				}
			}
			return false;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public unsafe static float DistanceToZone(GridReference location, Zone zone, FireMission fireMission, Vector3[] gridBounds)
	{
		//IL_0215: Expected O, but got Ref
		//IL_01ee: Expected F4, but got I4
		//IL_01e0: Expected F4, but got I4
		//IL_006e: Expected O, but got Ref
		//IL_008a: Expected O, but got Ref
		//IL_01d2: Expected F4, but got I4
		//IL_018b: Expected F4, but got I4
		//IL_018b: Expected F4, but got I4
		bool flag = zone == null;
		Vector3[] gridBounds2 = default(Vector3[]);
		Zone zone2 = (Zone)(&gridBounds2);
		float num3;
		if (!flag)
		{
			if (zone.Contains(location))
			{
				return 0f;
			}
			if (location != null)
			{
				Vector3 location2 = location.GetLocation(gridBounds2);
				_003C_003Ec__DisplayClass11_0 obj = default(_003C_003Ec__DisplayClass11_0);
				bool flag2 = (object)obj == null;
				float num = default(float);
				zone2 = (Zone)(&num);
				if (!flag2)
				{
					float num2 = default(float);
					Vector2 vector = ((FireMission)obj).ToLocalSpace((Vector3)(&num2));
					if (zone.ZoneShape != ZoneShapes.Single && zone.Regions != null)
					{
						List<ZoneRegion> regions = zone.Regions;
						if (regions._size != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
							List<ZoneRegion>.Enumerator enumerator = default(List<ZoneRegion>.Enumerator);
							Zone zone3 = default(Zone);
							while (enumerator.MoveNext())
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
								bool flag3 = zone3 == null;
								num3 = num;
								if (!flag3)
								{
									if (zone3.ID == null)
									{
										_003CDistanceToZone_003Eg__CheckZoneRect_007C11_1((GridReference)(object)zone3.Name, (float)zone3.Role, (float)zone3.ZoneShape, ref obj);
									}
									continue;
								}
								throw new NullReferenceException();
							}
							enumerator.Dispose();
							goto IL_01c9;
						}
					}
					_003CDistanceToZone_003Eg__CheckZoneRect_007C11_1(zone.BottomLeft, zone.Width, zone.Height, ref obj);
					goto IL_01c9;
				}
			}
		}
		num3 = 0f;
		throw new NullReferenceException();
		IL_01c9:
		return 2.139095E+09f;
	}

	public static GridReference Offset(GridReference origin, int offsetX, int offsetY)
	{
		//IL_0038: Expected O, but got I4
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Expected O, but got Unknown
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Expected O, but got Unknown
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Expected O, but got Unknown
		//IL_017a: Expected O, but got I4
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Expected O, but got Unknown
		//IL_019f: Expected I4, but got O
		//IL_01b8: Expected O, but got I4
		//IL_01dc: Expected I4, but got O
		//IL_01f8: Expected O, but got I4
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Expected O, but got Unknown
		//IL_0220: Expected I4, but got O
		if (origin != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
			int num = offsetX >> 2;
			int num2 = num >> 31;
			object obj = num + num2;
			object obj2 = obj * 4;
			object obj3 = obj + obj2;
			object obj4 = obj3 * 2;
			object obj5 = origin.X + obj4;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
			object obj6 = obj5 + offsetX;
			object obj7 = obj >> 2;
			object obj8 = obj7 >> 31;
			object obj9 = obj7 + obj8;
			object obj10 = obj9 * 4;
			object obj11 = obj9 + obj10;
			object obj12 = obj11 + obj11;
			object obj13 = origin.Location - obj12;
			object obj14 = obj13 * 4;
			object obj15 = obj13 + obj14;
			object obj16 = obj15 * 2;
			object obj17 = origin.Y + obj16;
			object obj18 = obj17 + offsetY;
			GridReference gridReference = new GridReference();
			if (gridReference != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ebp\"");
				object obj19 = 0 * 4;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ebx\"");
				object obj20 = obj19 * 2;
				gridReference.Location = (GridLocations)obj20;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ebp\"");
				object obj21 = 0 * 4;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ebx\"");
				object obj22 = obj21 + obj21;
				int x = obj6 - obj22;
				gridReference.X = x;
				object obj23 = 0 * 4;
				object obj24 = 0 + obj23;
				object obj25 = obj24 + obj24;
				int y = obj18 - obj25;
				gridReference.Y = y;
				return gridReference;
			}
		}
		return (GridReference)(object)new NullReferenceException();
	}

	private unsafe static void Decode(GridReference bl, out int baseX, out int baseY)
	{
		//IL_0018: Expected O, but got I
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Expected O, but got Unknown
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Expected O, but got Unknown
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Expected O, but got Unknown
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r9d\"");
		object obj = (nint)System.Runtime.CompilerServices.Unsafe.AsPointer(ref baseX) >> 2;
		object obj2 = obj >> 31;
		object obj3 = obj + obj2;
		object obj4 = obj3 * 4;
		object obj5 = obj3 + obj4;
		object obj6 = obj5 * 2;
		object obj7 = bl.X + obj6;
		ref int reference = ref *(int*)obj7;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r9d\"");
		object obj8 = obj7 >> 2;
		object obj9 = obj8 >> 31;
		object obj10 = obj8 + obj9;
		object obj11 = obj10 * 4;
		object obj12 = obj10 + obj11;
		object obj13 = obj12 + obj12;
		object obj14 = bl.Location - obj13;
		object obj15 = obj14 * 4;
		object obj16 = obj14 + obj15;
		object obj17 = obj16 * 2;
		object obj18 = bl.Y + obj17;
		ref int reference2 = ref *(int*)obj18;
	}

	public unsafe GridReference GetRandomGridPosition(System.Random rng)
	{
		//IL_06c1: Invalid comparison between F8 and I4
		//IL_06e9: Expected F8, but got I4
		//IL_0700: Expected I4, but got F8
		//IL_0721: Invalid comparison between F8 and I4
		//IL_0b2b: Expected I4, but got F8
		//IL_0749: Expected F8, but got I4
		//IL_0767: Expected O, but got I4
		//IL_077a: Unknown result type (might be due to invalid IL or missing references)
		//IL_077f: Expected O, but got Unknown
		//IL_078c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0791: Expected O, but got Unknown
		//IL_0799: Unknown result type (might be due to invalid IL or missing references)
		//IL_079e: Expected O, but got Unknown
		//IL_07da: Unknown result type (might be due to invalid IL or missing references)
		//IL_07df: Expected O, but got Unknown
		//IL_0806: Unknown result type (might be due to invalid IL or missing references)
		//IL_080b: Expected O, but got Unknown
		//IL_0814: Unknown result type (might be due to invalid IL or missing references)
		//IL_0819: Expected O, but got Unknown
		//IL_0839: Unknown result type (might be due to invalid IL or missing references)
		//IL_083e: Expected O, but got Unknown
		//IL_084b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0850: Expected O, but got Unknown
		//IL_0858: Unknown result type (might be due to invalid IL or missing references)
		//IL_085d: Expected O, but got Unknown
		//IL_08a2: Expected O, but got I4
		//IL_08b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ba: Expected O, but got Unknown
		//IL_08c7: Expected I4, but got O
		//IL_08e0: Expected O, but got I4
		//IL_08fa: Expected I4, but got O
		//IL_0920: Expected O, but got I4
		//IL_0929: Unknown result type (might be due to invalid IL or missing references)
		//IL_092e: Expected O, but got Unknown
		//IL_0948: Expected I4, but got O
		//IL_012c: Expected O, but got I
		//IL_018c: Expected O, but got I4
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Expected O, but got Unknown
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Expected O, but got Unknown
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Expected O, but got Unknown
		//IL_01fd: Expected I, but got O
		//IL_020b: Expected O, but got I
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Expected O, but got Unknown
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Expected O, but got Unknown
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Expected O, but got Unknown
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Expected O, but got Unknown
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Expected O, but got Unknown
		//IL_0281: Expected O, but got I4
		//IL_028a: Expected F8, but got I4
		//IL_02c2: Expected F8, but got I
		//IL_02cf: Invalid comparison between F8 and I4
		//IL_09f5: Expected I4, but got F8
		//IL_0309: Expected F8, but got I
		//IL_0316: Invalid comparison between F8 and I4
		//IL_0a10: Expected I4, but got F8
		//IL_0a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a21: Expected O, but got Unknown
		//IL_0a29: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2e: Expected O, but got Unknown
		//IL_0375: Expected O, but got Ref
		//IL_05e2: Expected O, but got I4
		//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fa: Expected O, but got Unknown
		//IL_0607: Expected I4, but got O
		//IL_0620: Expected O, but got I4
		//IL_063a: Expected I4, but got O
		//IL_0660: Expected O, but got I4
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Expected O, but got Unknown
		//IL_0688: Expected I4, but got O
		//IL_03c1: Expected O, but got I
		//IL_03e0: Expected O, but got Ref
		//IL_0406: Expected O, but got I
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Expected O, but got Unknown
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Expected O, but got Unknown
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Expected O, but got Unknown
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Expected O, but got Unknown
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Expected I, but got Unknown
		//IL_04dd: Expected O, but got I
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Expected O, but got Unknown
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Expected O, but got Unknown
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Expected O, but got Unknown
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Expected O, but got Unknown
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Expected O, but got Unknown
		//IL_0a97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9c: Expected O, but got Unknown
		//IL_0aab: Expected F8, but got I4
		//IL_0ab3: Expected O, but got Ref
		List<ZoneRegion>.Enumerator enumerator = default(List<ZoneRegion>.Enumerator);
		object obj18;
		object obj19;
		List<ZoneRegion> list2;
		if (ZoneShape != ZoneShapes.Single && Regions != null)
		{
			List<ZoneRegion> regions = Regions;
			if (regions._size != 0)
			{
				Predicate<ZoneRegion> match = _003C_003Ec._003C_003E9__14_0;
				if (_003C_003Ec._003C_003E9__14_0 == null)
				{
					match = (_003C_003Ec._003C_003E9__14_0 = delegate(ZoneRegion r)
					{
						//IL_0044: Expected I4, but got O
						if (r == null)
						{
							NullReferenceException ex = new NullReferenceException();
							return (byte)(int)ex != 0;
						}
						return r.RegionType == ZoneRegion.RegionTypes.Add;
					});
				}
				List<ZoneRegion> list = regions.FindAll(match);
				bool flag = list == null;
				list2 = regions;
				if (!flag)
				{
					if (list._size == 0)
					{
						goto IL_069a;
					}
					bool flag2 = rng == null;
					list2 = regions;
					if (!flag2)
					{
						int num = rng.Next(list._size);
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						object obj = default(object);
						bool flag3 = obj == null;
						list2 = list;
						if (!flag3)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ stack_-88_v6+18]");
							object obj2 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ stack_-88_v6+18]");
							bool flag4 = (nint)0 == 0;
							list2 = list;
							if (!flag4)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
								int num2 = num >> 2;
								int num3 = num2 >> 31;
								object obj3 = num2 + num3;
								object obj4 = obj3 * 4;
								object obj5 = obj3 + obj4;
								object obj6 = obj5 * 2;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v362 @ r9_v14+14]");
								object obj7 = 0 + obj6;
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
								object obj8 = obj3 >> 2;
								object obj9 = obj8 >> 31;
								nint num4 = (nint)(obj8 + obj9);
								object obj10 = num4 * 4;
								object obj11 = num4 + obj10;
								object obj12 = obj11 + obj11;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v362 @ r9_v14+10]");
								object obj13 = 0 - obj12;
								object obj14 = obj13 * 4;
								list2 = (List<ZoneRegion>)(object)(obj13 + obj14);
								object obj15 = list2 * 2;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v362 @ r9_v14+18]");
								object obj16 = 0 + obj15;
								object obj17 = 0;
								object obj20 = default(object);
								for (double num5 = 1.0; (nint)obj17 < 100; enumerator.Dispose(), obj17++, num4 = 0, num5 = 1.0, list2 = (List<ZoneRegion>)(&enumerator))
								{
									if (obj != null)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ stack_-88_v6+20]");
										double num6 = Math.Floor(0.0);
										bool flag5 = num6 < 1.0;
										double num7 = num5;
										if (!flag5)
										{
											num7 = num6;
										}
										int num8 = rng.Next(0, (int)num7);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ stack_-88_v6+24]");
										double num9 = Math.Floor(0.0);
										bool flag6 = num9 < 1.0;
										double num10 = num5;
										if (!flag6)
										{
											num10 = num9;
										}
										int num11 = rng.Next(0, (int)num10);
										obj18 = obj7 + num8;
										obj19 = obj16 + num11;
										if (Regions != null)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
											nint num12 = 0;
											while (enumerator.MoveNext())
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
												bool flag7 = obj20 == null;
												list2 = (List<ZoneRegion>)(&enumerator);
												if (!flag7)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v525 @ stack_-78_v8+10]");
													bool flag8 = (nint)0 != 1;
													num12 = 0;
													if (flag8)
													{
														continue;
													}
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v525 @ stack_-78_v8+18]");
													object obj21 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v525 @ stack_-78_v8+18]");
													bool flag9 = (nint)0 == 0;
													list2 = (List<ZoneRegion>)(&enumerator);
													if (!flag9)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
														object obj22 = (nint)(&obj20) >> 2;
														object obj23 = obj22 >> 31;
														object obj24 = obj22 + obj23;
														object obj25 = obj24 * 4;
														object obj26 = obj24 + obj25;
														object obj27 = obj26 * 2;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v523 @ r10_v8+14]");
														object obj28 = 0 + obj27;
														Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
														object obj29 = obj24 >> 2;
														object obj30 = obj29 >> 31;
														object obj31 = obj29 + obj30;
														object obj32 = obj31 * 4;
														object obj33 = obj31 + obj32;
														object obj34 = obj33 + obj33;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v523 @ r10_v8+10]");
														num12 = (nint)(0 - obj34);
														object obj35 = num12 * 4;
														object obj36 = num12 + obj35;
														object obj37 = obj36 * 2;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v523 @ r10_v8+18]");
														object obj38 = 0 + obj37;
														if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj18) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj28))
														{
															continue;
														}
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v525 @ stack_-78_v8+20]");
														object obj39 = obj28 + 0;
														if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj39) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj18) && System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj19) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj38))
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v525 @ stack_-78_v8+24]");
															object obj40 = obj38 + 0;
															if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj40) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj19))
															{
																goto IL_05ad;
															}
														}
														continue;
													}
													throw new NullReferenceException();
												}
												throw new NullReferenceException();
											}
											goto IL_05bb;
										}
									}
									goto IL_095a;
									IL_05ad:;
								}
								goto IL_069a;
							}
						}
					}
				}
				goto IL_095a;
			}
		}
		double num13 = Math.Floor(Width);
		bool flag10 = !(num13 < 1.0);
		double num14 = num13;
		if (!flag10)
		{
			num14 = 1.0;
		}
		bool flag11 = rng == null;
		list2 = (List<ZoneRegion>)(object)typeof(Math);
		GridReference gridReference;
		if (!flag11)
		{
			int num15 = rng.Next(0, (int)num14);
			double num16 = Math.Floor(Height);
			bool flag12 = !(num16 < 1.0);
			double num17 = num16;
			if (!flag12)
			{
				num17 = 1.0;
			}
			int num18 = rng.Next(0, (int)num17);
			GridReference bottomLeft = BottomLeft;
			if (BottomLeft != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
				object obj41 = 0 * 4;
				GridReference bottomLeft2 = BottomLeft;
				object obj42 = obj41 * 2;
				object obj43 = bottomLeft2.X + obj42;
				object obj44 = obj43 + num15;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
				object obj45 = obj41 >> 2;
				object obj46 = obj45 >> 31;
				object obj47 = obj45 + obj46;
				object obj48 = obj47 * 4;
				object obj49 = obj47 + obj48;
				object obj50 = obj49 + obj49;
				object obj51 = bottomLeft.Location - obj50;
				object obj52 = obj51 * 4;
				object obj53 = obj51 + obj52;
				GridReference bottomLeft3 = BottomLeft;
				object obj54 = obj53 * 2;
				object obj55 = bottomLeft3.Y + obj54;
				object obj56 = obj55 + num18;
				gridReference = new GridReference();
				if (gridReference != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ebx\"");
					object obj57 = 0 * 4;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul edi\"");
					object obj58 = obj57 * 2;
					gridReference.Location = (GridLocations)obj58;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ebx\"");
					object obj59 = 0 * 4;
					object obj60 = obj59 + obj59;
					int x = obj44 - obj60;
					gridReference.X = x;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul edi\"");
					object obj61 = 0 * 4;
					object obj62 = 0 + obj61;
					object obj63 = obj62 + obj62;
					int y = obj56 - obj63;
					gridReference.Y = y;
					goto IL_0adf;
				}
			}
		}
		goto IL_095a;
		IL_069a:
		gridReference = null;
		goto IL_0adf;
		IL_0adf:
		return gridReference;
		IL_095a:
		throw new NullReferenceException();
		IL_05bb:
		enumerator.Dispose();
		gridReference = new GridReference();
		if (gridReference == null)
		{
			goto IL_095a;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul esi\"");
		object obj64 = 0 * 4;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul edi\"");
		object obj65 = obj64 * 2;
		gridReference.Location = (GridLocations)obj65;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul esi\"");
		object obj66 = 0 * 4;
		object obj67 = obj66 + obj66;
		int x2 = obj18 - obj67;
		gridReference.X = x2;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul edi\"");
		object obj68 = 0 * 4;
		object obj69 = 0 + obj68;
		object obj70 = obj69 + obj69;
		int y2 = obj19 - obj70;
		gridReference.Y = y2;
		goto IL_0adf;
	}

	public void ZoneToWorldCorners(Vector3[] gridBounds, ref Vector3[] corners)
	{
		//IL_0054: Expected O, but got I
		//IL_0071: Expected O, but got I
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Expected O, but got Unknown
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Expected O, but got Unknown
		GridReference bottomLeft = BottomLeft;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ rdx (UnityEngine.Vector3[])+4C]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ rdx (UnityEngine.Vector3[])+28]");
		object obj = num - 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ rdx (UnityEngine.Vector3[])+34]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ rdx (UnityEngine.Vector3[])+28]");
		object obj2 = num2 - 0;
		float num3 = Width * 0.005f;
		float num4 = Height * 0.01f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r9d\"");
		object obj3 = (object)gridBounds >> 2;
		float num5 = (float)bottomLeft.Y * 0.01f;
		object obj4 = obj3 >> 31;
		object obj5 = obj3 + obj4;
		object obj6 = obj5 * 4;
		object obj7 = obj5 + obj6;
		object obj8 = obj7 + obj7;
		object obj9 = bottomLeft.Location - obj8;
		Vector3[] array = corners;
		float num6 = (float)bottomLeft.X * 0.005f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r9d\"");
		object obj10 = obj5 >> 2;
		float num7 = (float)obj9 * 0.1f;
		object obj11 = obj10 >> 31;
		object obj12 = obj10 + obj11;
		float num8 = num7 + num5;
		float num9 = num4 + num8;
		float num10 = (float)obj12 * 0.05f;
		float num11 = num6 + num10;
		float num12 = num3 + num11;
		float num13 = num11 * (float)obj;
		float num14 = num13;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ rdx (UnityEngine.Vector3[])+28]");
		float num15 = num14 + 0f;
		float num16 = (float)obj2 * num8;
		float num17 = num15 + num16;
		float num18 = (float)obj * num11;
		Vector3[] array2 = corners;
		float num19 = num18;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ rdx (UnityEngine.Vector3[])+28]");
		float num20 = num19 + 0f;
		float num21 = (float)obj2 * num9;
		float num22 = num20 + num21;
		Vector3[] array3 = corners;
		float num23 = (float)obj * num12;
		float num24 = num23;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ rdx (UnityEngine.Vector3[])+28]");
		float num25 = num24 + 0f;
		float num26 = (float)obj2 * num9;
		float num27 = num25 + num26;
		float num28 = (float)obj * num12;
		float num29 = num28;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ rdx (UnityEngine.Vector3[])+28]");
		float num30 = num29 + 0f;
		float num31 = (float)obj2 * num8;
		Vector3[] array4 = corners;
		float num32 = num30 + num31;
	}

	public void RegionToWorldCorners(ZoneRegion region, Vector3[] gridBounds, ref Vector3[] corners)
	{
		//IL_0045: Expected O, but got I
		//IL_0062: Expected O, but got I
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Expected O, but got Unknown
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+4C]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+28]");
		object obj = num - 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+34]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+28]");
		object obj2 = num2 - 0;
		GridReference bottomLeft = region.BottomLeft;
		float num3 = region.Width * 0.005f;
		float num4 = region.Height * 0.01f;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
		object obj3 = (object)region >> 2;
		float num5 = (float)bottomLeft.X * 0.005f;
		object obj4 = obj3 >> 31;
		object obj5 = obj3 + obj4;
		Vector3[] array = corners;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
		float num6 = (float)obj5 * 0.05f;
		float num7 = num5 + num6;
		object obj6 = obj5 >> 2;
		object obj7 = obj6 >> 31;
		object obj8 = obj6 + obj7;
		float num8 = num3 + num7;
		object obj9 = obj8 * 4;
		object obj10 = obj8 + obj9;
		object obj11 = obj10 + obj10;
		object obj12 = bottomLeft.Location - obj11;
		float num9 = num7 * (float)obj;
		GridReference bottomLeft2 = region.BottomLeft;
		float num10 = num9;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+28]");
		float num11 = num10 + 0f;
		float num12 = (float)bottomLeft2.Y * 0.01f;
		float num13 = (float)obj12 * 0.1f;
		float num14 = num13 + num12;
		float num15 = num4 + num14;
		float num16 = (float)obj2 * num14;
		float num17 = num11 + num16;
		float num18 = (float)obj * num7;
		Vector3[] array2 = corners;
		float num19 = num18;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+28]");
		float num20 = num19 + 0f;
		float num21 = (float)obj2 * num15;
		float num22 = num20 + num21;
		Vector3[] array3 = corners;
		float num23 = (float)obj * num8;
		float num24 = num23;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+28]");
		float num25 = num24 + 0f;
		float num26 = (float)obj2 * num15;
		float num27 = num25 + num26;
		float num28 = (float)obj * num8;
		float num29 = num28;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gridBounds @ r8 (UnityEngine.Vector3[])+28]");
		float num30 = num29 + 0f;
		float num31 = (float)obj2 * num14;
		Vector3[] array4 = corners;
		float num32 = num30 + num31;
	}

	public Zone()
	{
		List<ZoneRegion> regions = new List<ZoneRegion>();
		Regions = regions;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	internal unsafe static void _003CDistanceToZone_003Eg__CheckPoint_007C11_0(GridReference point, ref _003C_003Ec__DisplayClass11_0 P_1)
	{
		//IL_001a: Expected O, but got I
		//IL_0035: Expected O, but got Ref
		//IL_0049: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ rdx (<>c__DisplayClass11_0&)+8]");
		Vector3 location = point.GetLocation((Vector3[])0);
		object obj = default(object);
		Vector2 vector = ((FireMission)P_1).ToLocalSpace((Vector3)(&obj));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ rdx (<>c__DisplayClass11_0&)+10]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407E10");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ rdx (<>c__DisplayClass11_0&)+10]");
		object obj3 = default(object);
		if (0 > (nint)obj3)
		{
			obj2 = obj3;
		}
	}

	internal unsafe static void _003CDistanceToZone_003Eg__CheckZoneRect_007C11_1(GridReference bottomLeft, float width, float height, ref _003C_003Ec__DisplayClass11_0 P_3)
	{
		//IL_0046: Invalid comparison between I4 and F8
		//IL_008f: Expected O, but got I
		//IL_00aa: Expected O, but got Ref
		//IL_00be: Expected O, but got I
		//IL_0210: Invalid comparison between I4 and F8
		//IL_0259: Expected O, but got I
		//IL_012b: Expected I4, but got F8
		//IL_0274: Expected O, but got Ref
		//IL_0288: Expected O, but got I
		//IL_014e: Expected O, but got I
		//IL_0169: Expected O, but got Ref
		//IL_017d: Expected O, but got I
		//IL_02d9: Expected I4, but got F8
		//IL_02fc: Expected O, but got I
		//IL_0317: Expected O, but got Ref
		//IL_032b: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm8\"");
		double num = Math.Floor(0.0);
		int num2 = 0;
		float x = default(float);
		object obj2 = default(object);
		float x2 = default(float);
		while (!((double)num2 > num))
		{
			GridReference gridReference = Offset(bottomLeft, num2, 0);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass11_0&)+8]");
			Vector3 location = gridReference.GetLocation((Vector3[])0);
			Vector2 vector = ((FireMission)P_3).ToLocalSpace((Vector3)(&x));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass11_0&)+10]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407E10");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass11_0&)+10]");
			if ((nint)obj2 <= 0)
			{
				obj = obj2;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm7\"");
			double num3 = Math.Floor(0.0);
			GridReference gridReference2 = Offset(bottomLeft, num2, (int)num3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass11_0&)+8]");
			Vector3 location2 = gridReference2.GetLocation((Vector3[])0);
			Vector2 vector2 = ((FireMission)P_3).ToLocalSpace((Vector3)(&x2));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass11_0&)+10]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407E10");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass11_0&)+10]");
			if ((nint)obj2 <= 0)
			{
				obj3 = obj2;
			}
			num2++;
			x2 = location2.x;
			x = location.x;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm7\"");
		double num4 = Math.Floor(0.0);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm8\"");
		double num5 = Math.Floor(0.0);
		int num6 = 0;
		while (!((double)num6 > num4))
		{
			GridReference gridReference3 = Offset(bottomLeft, 0, num6);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass11_0&)+8]");
			Vector3 location3 = gridReference3.GetLocation((Vector3[])0);
			Vector2 vector3 = ((FireMission)P_3).ToLocalSpace((Vector3)(&x2));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass11_0&)+10]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407E10");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass11_0&)+10]");
			if ((nint)obj2 <= 0)
			{
				obj4 = obj2;
			}
			GridReference gridReference4 = Offset(bottomLeft, (int)num5, num6);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass11_0&)+8]");
			Vector3 location4 = gridReference4.GetLocation((Vector3[])0);
			Vector2 vector4 = ((FireMission)P_3).ToLocalSpace((Vector3)(&x));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass11_0&)+10]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407E10");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ r9 (<>c__DisplayClass11_0&)+10]");
			if ((nint)obj2 <= 0)
			{
				obj5 = obj2;
			}
			num6++;
			x2 = location3.x;
			x = location4.x;
		}
	}
}
