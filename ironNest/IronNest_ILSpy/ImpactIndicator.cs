using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using SleepyNodes;
using UnityEngine;
using UnityEngine.Events;

public class ImpactIndicator : MonoBehaviour
{
	[Serializable]
	public class LocalSpaceEventDataUnityEvent : UnityEvent<EventData_Impact>
	{
	}

	public enum HitTestMode
	{
		CenterPointInside,
		CircleOverlapsArea
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<MapEntity, bool> _003C_003E9__21_0;

		public static Func<MapEntity, bool> _003C_003E9__21_1;

		public static Func<MapEntity, bool> _003C_003E9__21_2;

		public static Func<MapEntity, bool> _003C_003E9__21_3;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal bool _003CHandleLocalSpaceEvent_003Eb__21_0(MapEntity x)
		{
			//IL_0051: Expected I4, but got O
			//IL_0030: Expected O, but got I4
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Expected I4, but got Unknown
			if (x != null)
			{
				object obj = (int)x.Role >> 5;
				return (byte)(obj & 1) != 0;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal bool _003CHandleLocalSpaceEvent_003Eb__21_1(MapEntity x)
		{
			//IL_0051: Expected I4, but got O
			//IL_0030: Expected O, but got I4
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Expected I4, but got Unknown
			if (x != null)
			{
				object obj = (int)x.Role >> 1;
				return (byte)(obj & 1) != 0;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal bool _003CHandleLocalSpaceEvent_003Eb__21_2(MapEntity x)
		{
			//IL_0051: Expected I4, but got O
			//IL_0030: Expected O, but got I4
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Expected I4, but got Unknown
			if (x != null)
			{
				object obj = (int)x.Role >> 6;
				return (byte)(obj & 1) != 0;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal bool _003CHandleLocalSpaceEvent_003Eb__21_3(MapEntity x)
		{
			//IL_0043: Expected I4, but got O
			if (x != null)
			{
				return (byte)(x.Role & EntityRoles.Enemy) != 0;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public RectTransform regionRect;

	public bool cacheRootCanvas = true;

	public HitTestMode hitTestMode = HitTestMode.CircleOverlapsArea;

	public float regionPadding;

	public bool filterByShellId;

	public List<string> allowedShellIds;

	public bool requireAnyTargetHit;

	public bool requireAnyAllyHit;

	public bool requireAnyOptionalHit;

	public bool requireAnyEnemyHit;

	public float minSecondsBetweenInvokes;

	public LocalSpaceEventDataUnityEvent onImpactWithinRegion;

	public bool debugLogs;

	private RectTransform _cachedRootCanvasRect;

	private RectTransform _region;

	private float _lastInvokeTime;

	private void OnEnable()
	{
		RectTransform region;
		if ((bool)regionRect)
		{
			region = regionRect;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			RectTransform rectTransform = default(RectTransform);
			region = rectTransform;
		}
		_region = region;
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 68 Invalid \"Jump target not found in method: 0x180570D70\"");
	}

	private void OnDisable()
	{
	}

	private RectTransform ResolveRootCanvasRect(bool force = false)
	{
		if (!force && cacheRootCanvas != force && _cachedRootCanvasRect != null)
		{
			return _cachedRootCanvasRect;
		}
		UnityEngine.Object obj;
		if (_region != null)
		{
			obj = _region;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			UnityEngine.Object obj2 = default(UnityEngine.Object);
			obj = obj2;
		}
		object message;
		if ((bool)obj)
		{
			if ((object)obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
				UnityEngine.Object obj3 = default(UnityEngine.Object);
				if (!obj3)
				{
					if (!debugLogs)
					{
						goto IL_02e9;
					}
					message = "[ImpactIndicator] No parent Canvas found; cannot resolve root canvas. Hit checks will be skipped.";
					goto IL_032b;
				}
				if ((object)obj3 != null)
				{
					Canvas rootCanvas = ((Canvas)obj3).rootCanvas;
					RectTransform rectTransform;
					if ((bool)rootCanvas)
					{
						Canvas rootCanvas2 = ((Canvas)obj3).rootCanvas;
						if ((object)rootCanvas2 == null)
						{
							goto IL_02eb;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
						RectTransform rectTransform2 = default(RectTransform);
						rectTransform = rectTransform2;
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
						RectTransform rectTransform3 = default(RectTransform);
						rectTransform = rectTransform3;
					}
					if (cacheRootCanvas)
					{
						_cachedRootCanvasRect = rectTransform;
					}
					if (debugLogs && rectTransform != null)
					{
						if ((object)rectTransform == null)
						{
							goto IL_02eb;
						}
						string text = rectTransform.name;
						string message2 = "[ImpactIndicator] Resolved root canvas rect: " + text;
						Debug.Log(message2, this);
					}
					return rectTransform;
				}
			}
			goto IL_02eb;
		}
		if (!debugLogs)
		{
			goto IL_02e9;
		}
		message = "[ImpactIndicator] No RectTransform found; cannot resolve root canvas.";
		goto IL_032b;
		IL_02e9:
		return null;
		IL_02eb:
		return (RectTransform)(object)new NullReferenceException();
		IL_032b:
		Debug.LogWarning(message, this);
		goto IL_02e9;
	}

	private unsafe void HandleLocalSpaceEvent(EventData_Impact data)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0022: Invalid comparison between F4 and I4
		//IL_0d0f: Expected O, but got I4
		//IL_0d6d: Expected O, but got I4
		//IL_012c: Expected O, but got I
		//IL_013d: Expected O, but got I
		//IL_039b: Expected F4, but got I
		//IL_03af: Expected I, but got O
		//IL_0dcb: Expected O, but got I4
		//IL_0e29: Expected O, but got I4
		//IL_03d7: Expected O, but got Ref
		//IL_03f5: Expected O, but got I4
		//IL_0417: Expected O, but got Ref
		//IL_0441: Expected O, but got Ref
		//IL_045f: Expected O, but got I4
		//IL_0479: Expected O, but got Ref
		//IL_04c2: Invalid comparison between F4 and I4
		//IL_0513: Expected O, but got Ref
		//IL_0e48: Expected O, but got I4
		//IL_086b: Expected I, but got O
		//IL_05cd: Expected O, but got I4
		//IL_05d2: Expected I, but got O
		//IL_05f8: Invalid comparison between I4 and F4
		//IL_060a: Expected F4, but got I4
		//IL_0894: Expected O, but got Ref
		//IL_08a4: Expected I4, but got O
		//IL_0e5f: Invalid comparison between F4 and I4
		//IL_0e71: Expected O, but got I4
		//IL_0931: Expected O, but got Ref
		//IL_0637: Expected O, but got Ref
		//IL_064b: Expected F4, but got I
		//IL_0659: Expected O, but got Ref
		//IL_08d2: Expected I, but got O
		//IL_08e2: Expected O, but got I
		//IL_067d: Expected O, but got I4
		//IL_069f: Expected O, but got Ref
		//IL_096a: Expected I, but got O
		//IL_097a: Expected O, but got I
		//IL_06bc: Expected O, but got Ref
		//IL_06e7: Expected O, but got I4
		//IL_0701: Expected O, but got Ref
		//IL_09f4: Expected I, but got O
		//IL_0a04: Expected O, but got I
		//IL_071e: Expected O, but got Ref
		//IL_0730: Expected O, but got Ref
		//IL_0ae9: Expected F4, but got O
		//IL_0766: Invalid comparison between I4 and F4
		//IL_0780: Expected O, but got I4
		//IL_0a81: Expected I, but got O
		//IL_0a91: Expected O, but got I
		//IL_0b7e: Expected F4, but got I
		//IL_0b1d: Expected I, but got O
		//IL_0b2d: Expected O, but got I
		//IL_0bb2: Expected I, but got O
		//IL_0bc2: Expected O, but got I
		//IL_0f33: Expected O, but got I4
		object obj2 = default(object);
		object obj = (object)(&obj2);
		if (data == null)
		{
			return;
		}
		float num = minSecondsBetweenInvokes;
		float num2;
		if (minSecondsBetweenInvokes > 0f)
		{
			float time = Time.time;
			num = time - _lastInvokeTime;
			num2 = minSecondsBetweenInvokes;
			if (minSecondsBetweenInvokes > num)
			{
				return;
			}
		}
		if (!filterByShellId)
		{
			goto IL_01d8;
		}
		if (allowedShellIds == null)
		{
			return;
		}
		List<string> list = allowedShellIds;
		if (list._size == 0)
		{
			return;
		}
		string impactShell = (string)(object)data.ImpactShell;
		bool flag = (object)data.ImpactShell == null;
		EventData_Impact eventData_Impact = data;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1188 @ rcx_v16 (System.String)+18]");
			impactShell = (string)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1188 @ rcx_v16 (System.String)+18]");
			if (string.IsNullOrEmpty((string)0))
			{
				return;
			}
			eventData_Impact = (EventData_Impact)(object)data.ImpactShell;
			if ((object)data.ImpactShell != null)
			{
				bool flag2 = allowedShellIds == null;
				impactShell = (string)(object)allowedShellIds;
				if (!flag2)
				{
					if (allowedShellIds.Contains((string)eventData_Impact.ImpactLocation))
					{
						goto IL_01d8;
					}
					return;
				}
			}
		}
		goto IL_0c8d;
		IL_0854:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180467C90");
		bool flag4 = default(bool);
		bool flag3 = flag4;
		nint num3 = unchecked((nint)null);
		float num4 = default(float);
		num = num4;
		goto IL_102f;
		IL_01d8:
		if (requireAnyTargetHit)
		{
			Func<MapEntity, bool> predicate = _003C_003Ec._003C_003E9__21_0;
			if (_003C_003Ec._003C_003E9__21_0 == null)
			{
				predicate = (_003C_003Ec._003C_003E9__21_0 = delegate(MapEntity x)
				{
					//IL_0051: Expected I4, but got O
					//IL_0030: Expected O, but got I4
					//IL_0039: Unknown result type (might be due to invalid IL or missing references)
					//IL_003e: Expected I4, but got Unknown
					if (x == null)
					{
						NullReferenceException ex = new NullReferenceException();
						return (byte)(int)ex != 0;
					}
					object obj28 = (int)x.Role >> 5;
					return (byte)(obj28 & 1) != 0;
				});
				object obj3 = 0;
			}
			if (!Enumerable.Any(data.ImpactEntities, predicate))
			{
				return;
			}
		}
		if (requireAnyAllyHit)
		{
			Func<MapEntity, bool> predicate2 = _003C_003Ec._003C_003E9__21_1;
			if (_003C_003Ec._003C_003E9__21_1 == null)
			{
				predicate2 = (_003C_003Ec._003C_003E9__21_1 = delegate(MapEntity x)
				{
					//IL_0051: Expected I4, but got O
					//IL_0030: Expected O, but got I4
					//IL_0039: Unknown result type (might be due to invalid IL or missing references)
					//IL_003e: Expected I4, but got Unknown
					if (x == null)
					{
						NullReferenceException ex = new NullReferenceException();
						return (byte)(int)ex != 0;
					}
					object obj28 = (int)x.Role >> 1;
					return (byte)(obj28 & 1) != 0;
				});
				object obj3 = 0;
			}
			if (!Enumerable.Any(data.ImpactEntities, predicate2))
			{
				return;
			}
		}
		if (requireAnyOptionalHit)
		{
			Func<MapEntity, bool> predicate3 = _003C_003Ec._003C_003E9__21_2;
			if (_003C_003Ec._003C_003E9__21_2 == null)
			{
				predicate3 = (_003C_003Ec._003C_003E9__21_2 = delegate(MapEntity x)
				{
					//IL_0051: Expected I4, but got O
					//IL_0030: Expected O, but got I4
					//IL_0039: Unknown result type (might be due to invalid IL or missing references)
					//IL_003e: Expected I4, but got Unknown
					if (x == null)
					{
						NullReferenceException ex = new NullReferenceException();
						return (byte)(int)ex != 0;
					}
					object obj28 = (int)x.Role >> 6;
					return (byte)(obj28 & 1) != 0;
				});
				object obj3 = 0;
			}
			if (!Enumerable.Any(data.ImpactEntities, predicate3))
			{
				return;
			}
		}
		if (requireAnyEnemyHit)
		{
			Func<MapEntity, bool> predicate4 = _003C_003Ec._003C_003E9__21_3;
			if (_003C_003Ec._003C_003E9__21_3 == null)
			{
				predicate4 = (_003C_003Ec._003C_003E9__21_3 = delegate(MapEntity x)
				{
					//IL_0043: Expected I4, but got O
					if (x == null)
					{
						NullReferenceException ex = new NullReferenceException();
						return (byte)(int)ex != 0;
					}
					return (byte)(x.Role & EntityRoles.Enemy) != 0;
				});
				object obj3 = 0;
			}
			if (!Enumerable.Any(data.ImpactEntities, predicate4))
			{
				return;
			}
		}
		RectTransform rectTransform = ResolveRootCanvasRect();
		if (!(_region != null) || !(rectTransform != null))
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventData_Impact)+1C]");
		num2 = 0f;
		bool flag5 = (object)rectTransform == null;
		num3 = unchecked((nint)null);
		eventData_Impact = null;
		impactShell = (string)(object)rectTransform;
		if (!flag5)
		{
			float num5 = default(float);
			Vector3 vector = rectTransform.TransformPoint((Vector3)(&num5));
			bool flag6 = (object)_region == null;
			object obj3 = 0;
			num3 = (nint)(&num5);
			eventData_Impact = (EventData_Impact)(object)_region;
			num = num4;
			float num6 = default(float);
			impactShell = (string)(&num6);
			if (!flag6)
			{
				num = vector.x;
				Vector3 vector2 = _region.InverseTransformPoint((Vector3)(&num5));
				bool flag7 = (object)_region == null;
				obj3 = 0;
				num3 = (nint)(&num5);
				eventData_Impact = (EventData_Impact)(object)_region;
				impactShell = (string)(&num6);
				if (!flag7)
				{
					Rect rect = _region.rect;
					num2 = regionPadding;
					num = rect.m_XMin;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180570741h\"");
					float num7;
					float num8;
					float num9 = default(float);
					float num10;
					float num11 = default(float);
					float num12;
					float num13 = default(float);
					float num14;
					if (regionPadding == 0f)
					{
						num7 = rect.m_XMin;
						num8 = num9;
						num10 = num11;
						num12 = num13;
						num14 = rect.m_XMin;
						impactShell = (string)(&num14);
					}
					else
					{
						num7 = rect.m_XMin - regionPadding;
						num8 = num9 - regionPadding;
						float num15 = regionPadding + regionPadding;
						float num16 = regionPadding + regionPadding;
						num10 = num15 + num11;
						num12 = num16 + num13;
						num14 = num7;
						impactShell = (string)(object)typeof(Rect);
					}
					bool flag8 = hitTestMode == HitTestMode.CenterPointInside;
					obj3 = 0;
					if (flag8)
					{
						goto IL_0854;
					}
					ShellDefinition impactShell2 = data.ImpactShell;
					bool flag9 = (object)data.ImpactShell == null;
					obj3 = 0;
					num3 = unchecked((nint)null);
					eventData_Impact = (EventData_Impact)(object)_region;
					if (!flag9)
					{
						bool flag10 = !(0f < impactShell2.ImpactRadius);
						float num17 = 0f;
						if (!flag10)
						{
							num17 = impactShell2.ImpactRadius;
						}
						bool flag11 = !(num17 > 0f);
						obj3 = 0;
						if (flag11)
						{
							goto IL_0854;
						}
						Vector3 vector3 = rectTransform.TransformPoint((Vector3)(&num5));
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventData_Impact)+1C]");
						num2 = 0f;
						Vector3 vector4 = rectTransform.TransformPoint((Vector3)(&num5));
						bool flag12 = (object)_region == null;
						obj3 = 0;
						num3 = (nint)(&num5);
						eventData_Impact = (EventData_Impact)(object)_region;
						num = num4;
						impactShell = (string)(&num6);
						if (!flag12)
						{
							Vector3 vector5 = _region.InverseTransformPoint((Vector3)(&num5));
							num = vector5.x;
							bool flag13 = (object)_region == null;
							obj3 = 0;
							num3 = (nint)(&num5);
							eventData_Impact = (EventData_Impact)(object)_region;
							impactShell = (string)(&num6);
							if (!flag13)
							{
								Vector3 vector6 = _region.InverseTransformPoint((Vector3)(&num6));
								object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 56));
								float num18 = default(float);
								num2 = vector6.y - num18;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371560");
								bool flag14 = !(0f < vector5.x);
								float num19 = num4;
								obj3 = 0;
								if (!flag14)
								{
									float num20 = num10 + num7;
									if (!(num7 > vector2.x))
									{
										bool flag15 = !(vector2.x > num20);
										num2 = vector2.x;
										if (!flag15)
										{
											num2 = num20;
										}
									}
									else
									{
										num2 = num7;
									}
									float num21 = num12 + num8;
									float num22;
									if (!(num8 > num18))
									{
										bool flag16 = !(num18 > num21);
										num22 = num18;
										if (!flag16)
										{
											num22 = num21;
										}
									}
									else
									{
										num22 = num8;
									}
									float num23 = vector2.x - num2;
									float num24 = num * num;
									float num25 = num18 - num22;
									float num26 = num23 * num23;
									float num27 = num25 * num25;
									float num28 = num26 + num27;
									bool flag17 = num24 < num28;
									flag3 = !flag17;
									num19 = num4;
									obj3 = 0;
									num3 = (nint)(&num6);
									num = num22;
									goto IL_102f;
								}
								goto IL_0854;
							}
						}
					}
				}
			}
		}
		goto IL_0c8d;
		IL_0c8d:
		throw new NullReferenceException();
		IL_102f:
		if (debugLogs)
		{
			object[] array = new object[6];
			object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 72));
			_ = hitTestMode;
			string text = (string)(object)(HitTestMode)obj5;
			if (text != null)
			{
				nint num29 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2156 @ rdx_v63 (Il2CppClass<System.Object[]>)+40]");
				eventData_Impact = (EventData_Impact)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj6 = default(object);
				bool flag18 = obj6 == null;
				impactShell = text;
				if (flag18)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj7 = default(object);
					throw obj7;
				}
			}
			array[0] = text;
			object obj8 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 56));
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj9 = default(object);
			if (obj9 != null)
			{
				nint num30 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2215 @ rdx_v61 (Il2CppClass<System.Object[]>)+40]");
				object obj10 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj11 = default(object);
				bool flag19 = obj11 == null;
				object obj12 = obj9;
				if (flag19)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					string text2 = default(string);
					throw text2;
				}
			}
			array[1] = obj9;
			string text3 = _region.name;
			if (text3 != null)
			{
				nint num31 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2309 @ rdx_v59 (Il2CppClass<System.Object[]>)+40]");
				object obj13 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj14 = default(object);
				bool flag20 = obj14 == null;
				string text4 = text3;
				if (flag20)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					string text5 = default(string);
					throw text5;
				}
			}
			array[2] = text3;
			ShellDefinition impactShell3 = data.ImpactShell;
			if (impactShell3.ShellId != null)
			{
				nint num32 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2332 @ rdx_v57 (Il2CppClass<System.Object[]>)+40]");
				object obj15 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj16 = default(object);
				bool flag21 = obj16 == null;
				string shellId = impactShell3.ShellId;
				if (flag21)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj17 = default(object);
					throw obj17;
				}
			}
			array[3] = impactShell3.ShellId;
			num = (float)data.ImpactLocation;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj18 = default(object);
			if (obj18 != null)
			{
				nint num33 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2360 @ rdx_v55 (Il2CppClass<System.Object[]>)+40]");
				object obj19 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj20 = default(object);
				bool flag22 = obj20 == null;
				object obj21 = obj18;
				if (flag22)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj22 = default(object);
					throw obj22;
				}
			}
			array[4] = obj18;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventData_Impact)+1C]");
			num = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj23 = default(object);
			if (obj23 != null)
			{
				nint num34 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2388 @ rdx_v53 (Il2CppClass<System.Object[]>)+40]");
				object obj24 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj25 = default(object);
				bool flag23 = obj25 == null;
				object obj26 = obj23;
				if (flag23)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj27 = default(object);
					throw obj27;
				}
			}
			array[5] = obj23;
			string message = string.Format("[ImpactIndicator] HitTest={0} result={1} region='{2}' shellId='{3}' at root-local=({4:F2},{5:F2})", array);
			Debug.Log(message, this);
		}
		if (flag3)
		{
			float time2 = Time.time;
			_lastInvokeTime = time2;
			if (onImpactWithinRegion != null)
			{
				onImpactWithinRegion.Invoke(data);
			}
		}
	}

	public ImpactIndicator()
	{
		List<string> list = new List<string>();
		allowedShellIds = list;
		_lastInvokeTime = -999f;
		base._002Ector();
	}
}
