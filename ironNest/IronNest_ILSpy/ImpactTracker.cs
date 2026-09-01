using System;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using SleepyNodes;
using UnityEngine;

public class ImpactTracker : MonoBehaviour
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<MapEntity, bool> _003C_003E9__6_0;

		public static Func<MapEntity, bool> _003C_003E9__6_1;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal bool _003CEvaluateImpact_003Eb__6_0(MapEntity x)
		{
			//IL_0043: Expected I4, but got O
			if (x != null)
			{
				return (byte)(x.Role & EntityRoles.Enemy) != 0;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal bool _003CEvaluateImpact_003Eb__6_1(MapEntity x)
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
	}

	private sealed class _003C_003Ec__DisplayClass8_0
	{
		public EntityRoles role;

		public bool isAlive;

		internal bool _003CGetNearest_003Eb__0(EntityLocation x)
		{
			//IL_00d1: Expected I4, but got O
			//IL_0060: Expected O, but got I4
			if ((object)x != null)
			{
				MapEntity entity = x.Entity;
				if (x.Entity != null)
				{
					object obj = entity.Role & role;
					if ((nint)obj != (nint)role)
					{
						return false;
					}
					if (!isAlive)
					{
						return true;
					}
					return x.Entity.IsAlive;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public static Dictionary<string, EntityLocation> EntityLocations;

	private static Action<Vector2, float> m_OnImpact;

	public static event Action<Vector2, float> OnImpact
	{
		add
		{
			//IL_000e: Expected O, but got I4
			//IL_0050: Expected I, but got O
			//IL_007c: Expected O, but got I
			Delegate obj = ImpactTracker.m_OnImpact;
			object obj4 = default(object);
			Delegate obj6 = default(Delegate);
			while (true)
			{
				Delegate obj2 = Delegate.Combine(obj, value);
				if ((object)obj2 == null)
				{
					object obj3 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag = obj4 == null;
					object obj3 = obj4;
					if (flag)
					{
						break;
					}
				}
				nint num = (nint)typeof(ImpactTracker);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ rcx_v7 (Il2CppClass<ImpactTracker>)+B8]");
				object obj5 = (nint)0 + (nint)8;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj6 != obj;
				obj = obj6;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_000e: Expected O, but got I4
			//IL_0050: Expected I, but got O
			//IL_007c: Expected O, but got I
			Delegate obj = ImpactTracker.m_OnImpact;
			object obj4 = default(object);
			Delegate obj6 = default(Delegate);
			while (true)
			{
				Delegate obj2 = Delegate.Remove(obj, value);
				if ((object)obj2 == null)
				{
					object obj3 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag = obj4 == null;
					object obj3 = obj4;
					if (flag)
					{
						break;
					}
				}
				nint num = (nint)typeof(ImpactTracker);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ rcx_v7 (Il2CppClass<ImpactTracker>)+B8]");
				object obj5 = (nint)0 + (nint)8;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj6 != obj;
				obj = obj6;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public static void RegisterEntity(EntityLocation entity)
	{
		MapEntity entity2 = entity.Entity;
		EntityLocations.set_Item(entity2.ID, entity);
	}

	public static void UnregisterEntity(string entityID)
	{
		bool flag = EntityLocations.Remove(entityID);
	}

	public static void EvaluateImpact(ShellDefinition shell, Vector2 impactLocation, bool triggerNormalEvents = true)
	{
		//IL_03a6: Expected I, but got O
		//IL_02db: Expected F8, but got I4
		//IL_02e8: Expected O, but got I4
		//IL_02f0: Expected O, but got I4
		//IL_0406: Expected I, but got O
		Vector2 vector = default(Vector2);
		object arg = vector;
		object arg2 = ((!shell) ? "None" : shell.DisplayName);
		string message = $"[ImpactLocation] Evaluating impact at root-local {arg} shell={arg2}";
		Debug.Log(message);
		if (!shell.IgnoreInTrackingShotsFired)
		{
			MissionManager._003CInstance_003Ek__BackingField.ModifyTrackingValue(MedalTrackedValue.ShotsFired, 1f);
			float num = 1f;
		}
		if (shell.ShellId == "STAR")
		{
			MissionManager._003CInstance_003Ek__BackingField.ModifyTrackingValue(MedalTrackedValue.STARUsed, 1f);
			float num = 1f;
		}
		bool flag = shell.Graph != null;
		bool flag2 = !flag;
		vector = impactLocation;
		Vector2 vector2 = impactLocation;
		if (!flag2)
		{
			List<MapEntity> list = shell.Graph.StartImpact(shell, impactLocation);
			EventData_Impact eventData_Impact = new EventData_Impact();
			eventData_Impact.ImpactShell = shell;
			eventData_Impact.ImpactLocation = impactLocation;
			eventData_Impact.ImpactEntities = list;
			eventData_Impact.TriggerNormalEvents = triggerNormalEvents;
			if (triggerNormalEvents)
			{
				FireMission._003CInstance_003Ek__BackingField.ProcessEvent(eventData_Impact);
			}
			if (list._size > 0)
			{
				MissionManager._003CInstance_003Ek__BackingField.ModifyTrackingValue(MedalTrackedValue.ShotsHit, 1f);
				float num = 1f;
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("Shell", shell.ShellId);
			Func<MapEntity, bool> predicate = _003C_003Ec._003C_003E9__6_0;
			bool flag3 = _003C_003Ec._003C_003E9__6_0 != null;
			nint num2 = 0;
			if (!flag3)
			{
				predicate = (_003C_003Ec._003C_003E9__6_0 = delegate(MapEntity x)
				{
					//IL_0043: Expected I4, but got O
					if (x == null)
					{
						NullReferenceException ex = new NullReferenceException();
						return (byte)(int)ex != 0;
					}
					return (byte)(x.Role & EntityRoles.Enemy) != 0;
				});
				num2 = unchecked((nint)null);
			}
			int num3 = Enumerable.Count(list, predicate);
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object value = default(object);
			dictionary.Add("Enemies", value);
			Func<MapEntity, bool> predicate2 = _003C_003Ec._003C_003E9__6_1;
			bool flag4 = _003C_003Ec._003C_003E9__6_1 != null;
			nint num4 = 0;
			if (!flag4)
			{
				predicate2 = (_003C_003Ec._003C_003E9__6_1 = delegate(MapEntity x)
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
					object obj = (int)x.Role >> 1;
					return (byte)(obj & 1) != 0;
				});
				num4 = unchecked((nint)null);
			}
			int num5 = Enumerable.Count(list, predicate2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object value2 = default(object);
			dictionary.Add("Allies", value2);
			AnalyticsManager.Analytics_Generic("ShellLanded", list._size, dictionary);
			vector = (Vector2)num5;
			vector2 = (Vector2)num3;
		}
		Action<Vector2, float> onImpact = ImpactTracker.m_OnImpact;
		if (ImpactTracker.m_OnImpact != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v567 @ rcx_v15 (System.Action`2<UnityEngine.Vector2, System.Single>)+18] (should have been resolved before IL gen)");
		}
		if ((object)ReplayManager.Instance != null)
		{
			ReplayManager.Instance.RecordFrameDelayed();
		}
	}

	public unsafe static (EntityLocation, float, float) GetNearestTargetOrEnemy(Vector2 fromPosition)
	{
		//IL_0017: Expected O, but got I4
		//IL_0070: Expected O, but got I4
		//IL_0081: Expected F4, but got O
		//IL_007c: Expected native int or pointer, but got O
		//IL_0054: Expected F4, but got O
		//IL_004f: Expected native int or pointer, but got O
		object obj = default(object);
		IntPtr intPtr = default(IntPtr);
		(EntityLocation, float, float) nearest = GetNearest((EntityRoles)(int)(&obj), (Vector2)32, (byte)(nint)intPtr != 0);
		if ((UnityEngine.Object)nearest != null)
		{
			((Vector2*)(nint)fromPosition)->x = (float)nearest;
			return ((EntityLocation, float, float))fromPosition;
		}
		(EntityLocation, float, float) nearest2 = GetNearest((EntityRoles)(int)(&obj), (Vector2)1, (byte)(nint)intPtr != 0);
		((Vector2*)(nint)fromPosition)->x = (float)nearest2;
		return ((EntityLocation, float, float))fromPosition;
	}

	public unsafe static (EntityLocation, float, float) GetNearest(EntityRoles role, Vector2 fromPosition, bool isAlive = true)
	{
		//IL_0049: Expected I4, but got O
		//IL_02ed: Expected O, but got Ref
		//IL_02f5: Expected O, but got Ref
		//IL_02fd: Expected O, but got Ref
		//IL_03f7: Expected O, but got I4
		//IL_00fa: Expected F4, but got I4
		//IL_0130: Expected O, but got Ref
		//IL_0299: Expected O, but got Ref
		//IL_02a1: Expected O, but got Ref
		//IL_02a9: Expected O, but got Ref
		//IL_02c8: Expected O, but got Ref
		//IL_02d0: Expected O, but got Ref
		//IL_02e0: Expected O, but got Ref
		//IL_0375: Expected I, but got O
		//IL_038d: Invalid comparison between F4 and I4
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Expected O, but got Unknown
		//IL_0208: Invalid comparison between I4 and F4
		//IL_0224: Expected F4, but got I4
		//IL_025e: Expected F4, but got I4
		_003C_003Ec__DisplayClass8_0 CS_0024_003C_003E8__locals6 = new _003C_003Ec__DisplayClass8_0();
		int value__ = default(int);
		if (CS_0024_003C_003E8__locals6 != null)
		{
			CS_0024_003C_003E8__locals6.role = (EntityRoles)fromPosition;
			IntPtr intPtr = default(IntPtr);
			CS_0024_003C_003E8__locals6.isAlive = (byte)(nint)intPtr != 0;
			if (EntityLocations != null)
			{
				Dictionary<string, EntityLocation>.ValueCollection values = EntityLocations.Values;
				Func<EntityLocation, bool> predicate = delegate(EntityLocation x)
				{
					//IL_00d1: Expected I4, but got O
					//IL_0060: Expected O, but got I4
					if ((object)x != null)
					{
						MapEntity entity = x.Entity;
						if (x.Entity != null)
						{
							object obj13 = entity.Role & CS_0024_003C_003E8__locals6.role;
							if ((nint)obj13 != (nint)CS_0024_003C_003E8__locals6.role)
							{
								return false;
							}
							if (!CS_0024_003C_003E8__locals6.isAlive)
							{
								return true;
							}
							return x.Entity.IsAlive;
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				};
				IEnumerable<EntityLocation> source = Enumerable.Where(values, predicate);
				List<EntityLocation> list = Enumerable.ToList(source);
				float num = default(float);
				object obj8 = default(object);
				UnityEngine.Object obj12;
				if (list != null && list._size > 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					num = 0f;
					float num2 = 3.4028235E+38f;
					UnityEngine.Object obj = null;
					List<EntityLocation>.Enumerator enumerator = default(List<EntityLocation>.Enumerator);
					EntityLocation entityLocation = default(EntityLocation);
					object obj5 = default(object);
					object obj6 = default(object);
					while (enumerator.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						bool flag = (object)entityLocation == null;
						_003C_003Ec__DisplayClass8_0 obj2 = (_003C_003Ec__DisplayClass8_0)(&enumerator);
						if (!flag)
						{
							Vector2 localPosition = entityLocation.LocalPosition;
							nint num3 = (nint)typeof(Math);
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm7\"");
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v493 @ rcx_v25 (Il2CppClass<System.Math>)+E4]");
							if ((nint)0 <= (nint)0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm1,xmm1\"");
							}
							else
							{
								double num4 = Math.Sqrt(0.0);
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm1\"");
							if (num2 > 0f)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm9,xmm1\"");
								Vector2 localPosition2 = entityLocation.LocalPosition;
								object obj3 = localPosition2 - isAlive;
								object obj4 = obj5 - obj6;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
								float num5 = (float)obj3 * 57.29578f;
								bool flag2 = !(0f > num5);
								num = num5;
								num2 = 0f;
								obj = entityLocation;
								if (!flag2)
								{
									float num6 = num5 + 360f;
									num = num6;
									num2 = 0f;
									obj = entityLocation;
								}
							}
							continue;
						}
						throw new NullReferenceException();
					}
					enumerator.Dispose();
					bool flag3 = obj != null;
					value__ = 0;
					object obj7 = (object)(&obj8);
					object obj9 = (object)(&num);
					UnityEngine.Object obj11 = default(UnityEngine.Object);
					object obj10 = (object)(&obj11);
					if (flag3)
					{
						value__ = 0;
						obj7 = (object)(&obj8);
						obj9 = (object)(&num);
						obj12 = obj;
						obj10 = (object)(&obj11);
						goto IL_03db;
					}
				}
				else
				{
					object obj7 = (object)(&obj8);
					object obj9 = (object)(&num);
					object obj10 = (object)(&value__);
				}
				obj12 = null;
				goto IL_03db;
			}
		}
		throw new NullReferenceException();
		IL_03db:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807E5B70");
		((EntityRoles*)(int)role)->value__ = value__;
		return ((EntityLocation, float, float))role;
	}

	static ImpactTracker()
	{
		Dictionary<string, EntityLocation> entityLocations = new Dictionary<string, EntityLocation>();
		EntityLocations = entityLocations;
	}
}
