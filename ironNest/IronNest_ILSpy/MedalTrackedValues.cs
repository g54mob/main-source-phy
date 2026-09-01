using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

[Serializable]
public class MedalTrackedValues
{
	public class Data_KilledEntity
	{
		public MapEntity Entity;

		public float KilledAtTime;

		public string ShellInstanceId;
	}

	public class Data_ShellFired
	{
		public string ShellInstanceId;

		public ShellDefinition Shell;

		public List<MapEntity> Hits;

		public float? DistanceFromNearestTarget;

		public float ShotAtTime;

		public Data_ShellFired()
		{
			List<MapEntity> hits = new List<MapEntity>();
			Hits = hits;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	public class Data_PunchcardUsed
	{
		public PunchcardDefinitionV2 Punchcard;

		public float UsedAtTime;
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<Data_KilledEntity, bool> _003C_003E9__15_0;

		public static Func<Data_KilledEntity, bool> _003C_003E9__17_0;

		public static Func<Data_KilledEntity, bool> _003C_003E9__19_0;

		public static Func<Data_KilledEntity, int> _003C_003E9__21_0;

		public static Func<Data_ShellFired, bool> _003C_003E9__23_0;

		public static Func<Data_ShellFired, bool> _003C_003E9__25_0;

		public static Func<Data_ShellFired, bool> _003C_003E9__27_0;

		public static Func<Data_ShellFired, bool> _003C_003E9__29_0;

		public static Func<Data_ShellFired, float> _003C_003E9__29_1;

		public static Func<Data_KilledEntity, bool> _003C_003E9__33_0;

		public static Func<Data_KilledEntity, float> _003C_003E9__33_1;

		public static Func<Data_PunchcardUsed, int> _003C_003E9__35_0;

		public static Func<Data_PunchcardUsed, bool> _003C_003E9__37_0;

		public static Func<Data_PunchcardUsed, bool> _003C_003E9__39_1;

		public static Func<Data_ShellFired, float> _003C_003E9__41_0;

		public static Func<Data_KilledEntity, bool> _003C_003E9__54_0;

		public static Func<Data_KilledEntity, float> _003C_003E9__54_1;

		public static Func<float, float> _003C_003E9__54_2;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal bool _003Cget_TargetKills_003Eb__15_0(Data_KilledEntity x)
		{
			//IL_0080: Expected I4, but got O
			//IL_005f: Expected O, but got I4
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			//IL_006d: Expected I4, but got Unknown
			if (x != null)
			{
				MapEntity entity = x.Entity;
				if (x.Entity != null)
				{
					object obj = (int)entity.Role >> 5;
					return (byte)(obj & 1) != 0;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal bool _003Cget_EnemyKills_003Eb__17_0(Data_KilledEntity x)
		{
			//IL_0072: Expected I4, but got O
			if (x != null)
			{
				MapEntity entity = x.Entity;
				if (x.Entity != null)
				{
					return (byte)(entity.Role & EntityRoles.Enemy) != 0;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal bool _003Cget_AllyKills_003Eb__19_0(Data_KilledEntity x)
		{
			//IL_0080: Expected I4, but got O
			//IL_005f: Expected O, but got I4
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			//IL_006d: Expected I4, but got Unknown
			if (x != null)
			{
				MapEntity entity = x.Entity;
				if (x.Entity != null)
				{
					object obj = (int)entity.Role >> 1;
					return (byte)(obj & 1) != 0;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal int _003Cget_StarsKilled_003Eb__21_0(Data_KilledEntity x)
		{
			//IL_0064: Expected I4, but got O
			if (x != null)
			{
				MapEntity entity = x.Entity;
				if (x.Entity != null)
				{
					return entity.Stars;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}

		internal bool _003Cget_ShotsFired_003Eb__23_0(Data_ShellFired x)
		{
			//IL_0073: Expected I4, but got O
			if (x != null)
			{
				ShellDefinition shell = x.Shell;
				if ((object)x.Shell != null)
				{
					return !shell.IgnoreInTrackingShotsFired;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal bool _003Cget_ShotsHit_003Eb__25_0(Data_ShellFired x)
		{
			//IL_0124: Expected I4, but got O
			if (x != null)
			{
				ShellDefinition shell = x.Shell;
				if ((object)x.Shell != null)
				{
					if (shell.IgnoreInTrackingShotsFired)
					{
						return false;
					}
					List<MapEntity> hits = x.Hits;
					if (x.Hits != null)
					{
						int num = hits._size ^ hits._size;
						int num2 = hits._size & num;
						bool flag = num2 < 0;
						bool flag2 = hits._size < 0;
						bool flag3 = hits._size == 0;
						bool flag4 = flag2 == flag;
						bool flag5 = !flag3;
						return flag5 & flag4;
					}
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal bool _003Cget_STARUsed_003Eb__27_0(Data_ShellFired x)
		{
			//IL_008c: Expected I4, but got O
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A150]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			if (x != null)
			{
				ShellDefinition shell = x.Shell;
				if ((object)x.Shell != null)
				{
					return shell.ShellId == "STAR";
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal bool _003Cget_AverageImpactDistanceFromNearestTarget_003Eb__29_0(Data_ShellFired x)
		{
			//IL_003b: Expected I4, but got O
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Expected O, but got Unknown
			//IL_002d: Expected I4, but got O
			if (x != null)
			{
				object obj = x + 40;
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj2 = default(object);
				return (byte)(int)obj2 != 0;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal float _003Cget_AverageImpactDistanceFromNearestTarget_003Eb__29_1(Data_ShellFired x)
		{
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Expected O, but got Unknown
			object obj = x + 40;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
			float result = default(float);
			return result;
		}

		internal bool _003Cget_LastTargetDestroyedTime_003Eb__33_0(Data_KilledEntity x)
		{
			//IL_0080: Expected I4, but got O
			//IL_005f: Expected O, but got I4
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			//IL_006d: Expected I4, but got Unknown
			if (x != null)
			{
				MapEntity entity = x.Entity;
				if (x.Entity != null)
				{
					object obj = (int)entity.Role >> 5;
					return (byte)(obj & 1) != 0;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal float _003Cget_LastTargetDestroyedTime_003Eb__33_1(Data_KilledEntity x)
		{
			return x.KilledAtTime;
		}

		internal int _003Cget_RequisitionPointsSpent_003Eb__35_0(Data_PunchcardUsed x)
		{
			//IL_0064: Expected I4, but got O
			if (x != null)
			{
				PunchcardDefinitionV2 punchcard = x.Punchcard;
				if ((object)x.Punchcard != null)
				{
					return punchcard.Cost;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}

		internal bool _003Cget_ReconUsed_003Eb__37_0(Data_PunchcardUsed x)
		{
			//IL_0064: Expected I4, but got O
			if (x != null)
			{
				PunchcardDefinitionV2 punchcard = x.Punchcard;
				if ((object)x.Punchcard != null)
				{
					return punchcard.IsRecon;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal bool _003Cget_ReconUsedAfterFirstShot_003Eb__39_1(Data_PunchcardUsed x)
		{
			//IL_0064: Expected I4, but got O
			if (x != null)
			{
				PunchcardDefinitionV2 punchcard = x.Punchcard;
				if ((object)x.Punchcard != null)
				{
					return punchcard.IsRecon;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal float _003Cget_LongestKillStreak_003Eb__41_0(Data_ShellFired x)
		{
			return x.ShotAtTime;
		}

		internal bool _003CGetBestXKillsInSeconds_003Eb__54_0(Data_KilledEntity k)
		{
			//IL_0072: Expected I4, but got O
			if (k != null)
			{
				MapEntity entity = k.Entity;
				if (k.Entity != null)
				{
					return (byte)(entity.Role & EntityRoles.Enemy) != 0;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal float _003CGetBestXKillsInSeconds_003Eb__54_1(Data_KilledEntity x)
		{
			return x.KilledAtTime;
		}

		internal float _003CGetBestXKillsInSeconds_003Eb__54_2(float x)
		{
			return x;
		}
	}

	private sealed class _003C_003Ec__DisplayClass41_0
	{
		public Data_ShellFired shell;

		internal bool _003Cget_LongestKillStreak_003Eb__1(Data_KilledEntity k)
		{
			if (k != null)
			{
				Data_ShellFired data_ShellFired = shell;
				if (shell != null)
				{
					bool flag = k.ShellInstanceId == data_ShellFired.ShellInstanceId;
					if (!flag)
					{
						return flag;
					}
					MapEntity entity = k.Entity;
					if (k.Entity != null)
					{
						return (byte)(entity.Role & EntityRoles.Enemy) != 0;
					}
				}
			}
			throw new NullReferenceException();
		}
	}

	private sealed class _003C_003Ec__DisplayClass43_0
	{
		public Data_ShellFired shell;

		internal bool _003Cget_MostKillsBySingleImpact_003Eb__1(Data_KilledEntity k)
		{
			if (k != null)
			{
				Data_ShellFired data_ShellFired = shell;
				if (shell != null)
				{
					bool flag = k.ShellInstanceId == data_ShellFired.ShellInstanceId;
					if (!flag)
					{
						return flag;
					}
					MapEntity entity = k.Entity;
					if (k.Entity != null)
					{
						return (byte)(entity.Role & EntityRoles.Enemy) != 0;
					}
				}
			}
			throw new NullReferenceException();
		}
	}

	private sealed class _003C_003Ec__DisplayClass55_0
	{
		public EntityRoles role;

		internal bool _003CGetKillsByRole_003Eb__0(Data_KilledEntity x)
		{
			//IL_0091: Expected I4, but got O
			//IL_0060: Expected O, but got I4
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Expected O, but got Unknown
			if (x != null)
			{
				MapEntity entity = x.Entity;
				if (x.Entity != null)
				{
					object obj = entity.Role & role;
					object obj2 = obj - role;
					return obj2 == null;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass56_0
	{
		public MapEntityStates state;

		internal bool _003CGetKillsByState_003Eb__0(Data_KilledEntity x)
		{
			//IL_0091: Expected I4, but got O
			//IL_0060: Expected O, but got I4
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Expected O, but got Unknown
			if (x != null)
			{
				MapEntity entity = x.Entity;
				if (x.Entity != null)
				{
					object obj = entity.State & state;
					object obj2 = obj - state;
					return obj2 == null;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private const string KillsByRolePrefix = "KillsByRole_";

	private const string KillsByStatePrefix = "KillsByState_";

	public List<Data_KilledEntity> Data_KilledEntities;

	public List<Data_ShellFired> Data_ShellsFired;

	public List<Data_PunchcardUsed> Data_PunchcardsUsed;

	public float MissionStartTime;

	public float MissionCompleteTime;

	public float MissionEndTime;

	public float CounterBatteryTimeRemaining;

	[NonSerialized]
	public Dictionary<string, float> CustomValues;

	public int Kills
	{
		get
		{
			//IL_001d: Expected I4, but got O
			List<Data_KilledEntity> data_KilledEntities = Data_KilledEntities;
			if (Data_KilledEntities != null)
			{
				return data_KilledEntities._size;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public int TargetKills
	{
		get
		{
			Func<Data_KilledEntity, bool> predicate = _003C_003Ec._003C_003E9__15_0;
			if (_003C_003Ec._003C_003E9__15_0 == null)
			{
				predicate = (_003C_003Ec._003C_003E9__15_0 = delegate(Data_KilledEntity x)
				{
					//IL_0080: Expected I4, but got O
					//IL_005f: Expected O, but got I4
					//IL_0068: Unknown result type (might be due to invalid IL or missing references)
					//IL_006d: Expected I4, but got Unknown
					if (x != null)
					{
						MapEntity entity = x.Entity;
						if (x.Entity != null)
						{
							object obj = (int)entity.Role >> 5;
							return (byte)(obj & 1) != 0;
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				});
			}
			return Enumerable.Count(Data_KilledEntities, predicate);
		}
	}

	public int EnemyKills
	{
		get
		{
			Func<Data_KilledEntity, bool> predicate = _003C_003Ec._003C_003E9__17_0;
			if (_003C_003Ec._003C_003E9__17_0 == null)
			{
				predicate = (_003C_003Ec._003C_003E9__17_0 = delegate(Data_KilledEntity x)
				{
					//IL_0072: Expected I4, but got O
					if (x != null)
					{
						MapEntity entity = x.Entity;
						if (x.Entity != null)
						{
							return (byte)(entity.Role & EntityRoles.Enemy) != 0;
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				});
			}
			return Enumerable.Count(Data_KilledEntities, predicate);
		}
	}

	public int AllyKills
	{
		get
		{
			Func<Data_KilledEntity, bool> predicate = _003C_003Ec._003C_003E9__19_0;
			if (_003C_003Ec._003C_003E9__19_0 == null)
			{
				predicate = (_003C_003Ec._003C_003E9__19_0 = delegate(Data_KilledEntity x)
				{
					//IL_0080: Expected I4, but got O
					//IL_005f: Expected O, but got I4
					//IL_0068: Unknown result type (might be due to invalid IL or missing references)
					//IL_006d: Expected I4, but got Unknown
					if (x != null)
					{
						MapEntity entity = x.Entity;
						if (x.Entity != null)
						{
							object obj = (int)entity.Role >> 1;
							return (byte)(obj & 1) != 0;
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				});
			}
			return Enumerable.Count(Data_KilledEntities, predicate);
		}
	}

	public int StarsKilled
	{
		get
		{
			Func<Data_KilledEntity, int> selector = _003C_003Ec._003C_003E9__21_0;
			if (_003C_003Ec._003C_003E9__21_0 == null)
			{
				selector = (_003C_003Ec._003C_003E9__21_0 = delegate(Data_KilledEntity x)
				{
					//IL_0064: Expected I4, but got O
					if (x != null)
					{
						MapEntity entity = x.Entity;
						if (x.Entity != null)
						{
							return entity.Stars;
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (int)ex;
				});
			}
			return Enumerable.Sum(Data_KilledEntities, selector);
		}
	}

	public int ShotsFired
	{
		get
		{
			Func<Data_ShellFired, bool> predicate = _003C_003Ec._003C_003E9__23_0;
			if (_003C_003Ec._003C_003E9__23_0 == null)
			{
				predicate = (_003C_003Ec._003C_003E9__23_0 = delegate(Data_ShellFired x)
				{
					//IL_0073: Expected I4, but got O
					if (x != null)
					{
						ShellDefinition shell = x.Shell;
						if ((object)x.Shell != null)
						{
							return !shell.IgnoreInTrackingShotsFired;
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				});
			}
			return Enumerable.Count(Data_ShellsFired, predicate);
		}
	}

	public int ShotsHit
	{
		get
		{
			Func<Data_ShellFired, bool> predicate = _003C_003Ec._003C_003E9__25_0;
			if (_003C_003Ec._003C_003E9__25_0 == null)
			{
				predicate = (_003C_003Ec._003C_003E9__25_0 = delegate(Data_ShellFired x)
				{
					//IL_0124: Expected I4, but got O
					if (x != null)
					{
						ShellDefinition shell = x.Shell;
						if ((object)x.Shell != null)
						{
							if (shell.IgnoreInTrackingShotsFired)
							{
								return false;
							}
							List<MapEntity> hits = x.Hits;
							if (x.Hits != null)
							{
								int num = hits._size ^ hits._size;
								int num2 = hits._size & num;
								bool flag = num2 < 0;
								bool flag2 = hits._size < 0;
								bool flag3 = hits._size == 0;
								bool flag4 = flag2 == flag;
								bool flag5 = !flag3;
								return flag5 & flag4;
							}
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				});
			}
			return Enumerable.Count(Data_ShellsFired, predicate);
		}
	}

	public int STARUsed
	{
		get
		{
			Func<Data_ShellFired, bool> predicate = _003C_003Ec._003C_003E9__27_0;
			if (_003C_003Ec._003C_003E9__27_0 == null)
			{
				predicate = (_003C_003Ec._003C_003E9__27_0 = delegate(Data_ShellFired x)
				{
					//IL_008c: Expected I4, but got O
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A150]");
					if ((nint)0 == 0)
					{
						_ = 1;
					}
					if (x != null)
					{
						ShellDefinition shell = x.Shell;
						if ((object)x.Shell != null)
						{
							return shell.ShellId == "STAR";
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				});
			}
			return Enumerable.Count(Data_ShellsFired, predicate);
		}
	}

	public unsafe float AverageImpactDistanceFromNearestTarget
	{
		get
		{
			//IL_0045: Expected F4, but got Ref
			Func<Data_ShellFired, bool> predicate = _003C_003Ec._003C_003E9__29_0;
			if (_003C_003Ec._003C_003E9__29_0 == null)
			{
				predicate = (_003C_003Ec._003C_003E9__29_0 = delegate(Data_ShellFired x)
				{
					//IL_003b: Expected I4, but got O
					//IL_0013: Unknown result type (might be due to invalid IL or missing references)
					//IL_0018: Expected O, but got Unknown
					//IL_002d: Expected I4, but got O
					if (x == null)
					{
						NullReferenceException ex = new NullReferenceException();
						return (byte)(int)ex != 0;
					}
					object obj2 = x + 40;
					nint num = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
					object obj3 = default(object);
					return (byte)(int)obj3 != 0;
				});
			}
			IEnumerable<Data_ShellFired> source = Enumerable.Where(Data_ShellsFired, predicate);
			Func<Data_ShellFired, float> selector = _003C_003Ec._003C_003E9__29_1;
			if (_003C_003Ec._003C_003E9__29_1 == null)
			{
				selector = (_003C_003Ec._003C_003E9__29_1 = delegate(Data_ShellFired x)
				{
					//IL_000e: Unknown result type (might be due to invalid IL or missing references)
					//IL_0013: Expected O, but got Unknown
					object obj2 = x + 40;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
					float result = default(float);
					return result;
				});
			}
			IEnumerable<float> source2 = Enumerable.Select(source, selector);
			object obj = default(object);
			IEnumerable<float> source3 = Enumerable.DefaultIfEmpty(source2, (nint)(&obj));
			return Enumerable.Average(source3);
		}
	}

	public float FirstShotTime
	{
		get
		{
			//IL_0018: Expected F4, but got I4
			//IL_0012: Expected F4, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF410");
			object obj = default(object);
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ stack_8_v1+30]");
				return 0f;
			}
			return 0f;
		}
	}

	public unsafe float LastTargetDestroyedTime
	{
		get
		{
			//IL_0045: Expected F4, but got Ref
			Func<Data_KilledEntity, bool> predicate = _003C_003Ec._003C_003E9__33_0;
			if (_003C_003Ec._003C_003E9__33_0 == null)
			{
				predicate = (_003C_003Ec._003C_003E9__33_0 = delegate(Data_KilledEntity x)
				{
					//IL_0080: Expected I4, but got O
					//IL_005f: Expected O, but got I4
					//IL_0068: Unknown result type (might be due to invalid IL or missing references)
					//IL_006d: Expected I4, but got Unknown
					if (x != null)
					{
						MapEntity entity = x.Entity;
						if (x.Entity != null)
						{
							object obj2 = (int)entity.Role >> 5;
							return (byte)(obj2 & 1) != 0;
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				});
			}
			IEnumerable<Data_KilledEntity> source = Enumerable.Where(Data_KilledEntities, predicate);
			Func<Data_KilledEntity, float> selector = _003C_003Ec._003C_003E9__33_1;
			if (_003C_003Ec._003C_003E9__33_1 == null)
			{
				selector = (_003C_003Ec._003C_003E9__33_1 = (Data_KilledEntity x) => x.KilledAtTime);
			}
			IEnumerable<float> source2 = Enumerable.Select(source, selector);
			object obj = default(object);
			IEnumerable<float> source3 = Enumerable.DefaultIfEmpty(source2, (nint)(&obj));
			return Enumerable.Max(source3);
		}
	}

	public int RequisitionPointsSpent
	{
		get
		{
			Func<Data_PunchcardUsed, int> selector = _003C_003Ec._003C_003E9__35_0;
			if (_003C_003Ec._003C_003E9__35_0 == null)
			{
				selector = (_003C_003Ec._003C_003E9__35_0 = delegate(Data_PunchcardUsed x)
				{
					//IL_0064: Expected I4, but got O
					if (x != null)
					{
						PunchcardDefinitionV2 punchcard = x.Punchcard;
						if ((object)x.Punchcard != null)
						{
							return punchcard.Cost;
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (int)ex;
				});
			}
			return Enumerable.Sum(Data_PunchcardsUsed, selector);
		}
	}

	public int ReconUsed
	{
		get
		{
			Func<Data_PunchcardUsed, bool> predicate = _003C_003Ec._003C_003E9__37_0;
			if (_003C_003Ec._003C_003E9__37_0 == null)
			{
				predicate = (_003C_003Ec._003C_003E9__37_0 = delegate(Data_PunchcardUsed x)
				{
					//IL_0064: Expected I4, but got O
					if (x != null)
					{
						PunchcardDefinitionV2 punchcard = x.Punchcard;
						if ((object)x.Punchcard != null)
						{
							return punchcard.IsRecon;
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				});
			}
			return Enumerable.Count(Data_PunchcardsUsed, predicate);
		}
	}

	public int ReconUsedAfterFirstShot
	{
		get
		{
			Func<Data_PunchcardUsed, bool> predicate = delegate(Data_PunchcardUsed x)
			{
				//IL_005d: Expected I4, but got O
				//IL_004a: Expected O, but got I4
				//IL_0087: Invalid comparison between F4 and O
				//IL_00aa: Invalid comparison between F4 and I4
				//IL_003c: Expected O, but got I
				if (x == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF410");
				object obj = default(object);
				object obj2;
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ stack_10_v1+30]");
					obj2 = 0;
				}
				else
				{
					obj2 = 0;
				}
				float usedAtTime = x.UsedAtTime;
				bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)usedAtTime) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2);
				float num = x.UsedAtTime - (float)obj2;
				bool flag2 = num == 0f;
				bool flag3 = !flag;
				bool flag4 = !flag2;
				return flag4 & flag3;
			};
			IEnumerable<Data_PunchcardUsed> source = Enumerable.Where(Data_PunchcardsUsed, predicate);
			Func<Data_PunchcardUsed, bool> predicate2 = _003C_003Ec._003C_003E9__39_1;
			if (_003C_003Ec._003C_003E9__39_1 == null)
			{
				predicate2 = (_003C_003Ec._003C_003E9__39_1 = delegate(Data_PunchcardUsed x)
				{
					//IL_0064: Expected I4, but got O
					if (x != null)
					{
						PunchcardDefinitionV2 punchcard = x.Punchcard;
						if ((object)x.Punchcard != null)
						{
							return punchcard.IsRecon;
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				});
			}
			return Enumerable.Count(source, predicate2);
		}
	}

	public unsafe int LongestKillStreak
	{
		get
		{
			//IL_003c: Expected O, but got Ref
			//IL_0065: Expected I, but got O
			//IL_00c5: Expected I, but got O
			//IL_0156: Expected O, but got I4
			//IL_00fc: Expected O, but got I
			//IL_02d3: Expected O, but got I4
			//IL_01d0: Expected O, but got I4
			//IL_01e6: Expected O, but got I
			//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f4: Expected O, but got Unknown
			//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0201: Expected O, but got Unknown
			//IL_0240: Expected I, but got O
			//IL_01be: Expected I, but got O
			Func<Data_ShellFired, float> keySelector = _003C_003Ec._003C_003E9__41_0;
			if (_003C_003Ec._003C_003E9__41_0 == null)
			{
				keySelector = (_003C_003Ec._003C_003E9__41_0 = (Data_ShellFired x) => x.ShotAtTime);
			}
			IOrderedEnumerable<Data_ShellFired> orderedEnumerable = Enumerable.OrderBy(Data_ShellsFired, keySelector);
			IEnumerator<Data_ShellFired> enumerator = orderedEnumerable.GetEnumerator();
			IEnumerable<Data_ShellFired> enumerable = default(IEnumerable<Data_ShellFired>);
			object obj = (object)(&enumerable);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			nint num4 = (nint)typeof(IEnumerable<Data_ShellFired>);
			object obj2 = default(object);
			object obj9 = default(object);
			Data_ShellFired shell = default(Data_ShellFired);
			while (true)
			{
				_003C_003Ec__DisplayClass41_0 CS_0024_003C_003E8__locals4;
				object obj8;
				object obj3;
				if (enumerable != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					if (obj2 != null)
					{
						CS_0024_003C_003E8__locals4 = new _003C_003Ec__DisplayClass41_0();
						if (enumerable != null)
						{
							nint num5 = (nint)enumerable;
							int num6 = num3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ r10_v6 (Il2CppClass<System.Collections.Generic.IEnumerable`1<MedalTrackedValues+Data_ShellFired>>)+12E]");
							if ((nint)num6 >= (nint)0)
							{
								goto IL_013b;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ r10_v6 (Il2CppClass<System.Collections.Generic.IEnumerable`1<MedalTrackedValues+Data_ShellFired>>)+B0]");
							obj3 = 0;
							int num7 = num3;
							while (true)
							{
								object obj4 = num7 + num7;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v224 @ r8_v12+v550 @ rcx_v30*8]");
								if (0 == (nint)typeof(IEnumerator<Data_ShellFired>))
								{
									break;
								}
								num7++;
								int num8 = num7;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ r10_v6 (Il2CppClass<System.Collections.Generic.IEnumerable`1<MedalTrackedValues+Data_ShellFired>>)+12E]");
								if ((nint)num8 < (nint)0)
								{
									continue;
								}
								goto IL_013b;
							}
							object obj5 = num7 + num7;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v224 @ r8_v12+8+v604 @ rcx_v32*8]");
							object obj6 = (nint)0 << 4;
							object obj7 = obj6 + 312;
							obj8 = obj7 + num5;
							goto IL_0325;
						}
						throw new NullReferenceException();
					}
					if (obj != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					}
					break;
				}
				throw new NullReferenceException();
				IL_013b:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj8 = obj9;
				obj3 = 0;
				goto IL_0325;
				IL_0325:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v609 @ rdx_v15] (should have been resolved before IL gen)");
				if (CS_0024_003C_003E8__locals4 != null)
				{
					CS_0024_003C_003E8__locals4.shell = shell;
					Func<Data_KilledEntity, bool> predicate = delegate(Data_KilledEntity k)
					{
						if (k != null)
						{
							Data_ShellFired shell2 = CS_0024_003C_003E8__locals4.shell;
							if (CS_0024_003C_003E8__locals4.shell != null)
							{
								bool flag = k.ShellInstanceId == shell2.ShellInstanceId;
								if (!flag)
								{
									return flag;
								}
								MapEntity entity = k.Entity;
								if (k.Entity != null)
								{
									return (byte)(entity.Role & EntityRoles.Enemy) != 0;
								}
							}
						}
						throw new NullReferenceException();
					};
					if (!Enumerable.Any(Data_KilledEntities, predicate))
					{
						num2 = 0;
						num3 = 0;
						num4 = unchecked((nint)null);
					}
					else
					{
						num2++;
						int num9 = Math.Max(num, num2);
						num = num9;
						num3 = 0;
						num4 = unchecked((nint)null);
					}
					continue;
				}
				throw new NullReferenceException();
			}
			return num;
		}
	}

	public unsafe int MostKillsBySingleImpact
	{
		get
		{
			Func<Data_ShellFired, int> selector = delegate(Data_ShellFired shell)
			{
				//IL_0056: Expected I4, but got O
				_003C_003Ec__DisplayClass43_0 CS_0024_003C_003E8__locals4 = new _003C_003Ec__DisplayClass43_0();
				if (CS_0024_003C_003E8__locals4 == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (int)ex;
				}
				CS_0024_003C_003E8__locals4.shell = shell;
				Func<Data_KilledEntity, bool> predicate = delegate(Data_KilledEntity k)
				{
					if (k != null)
					{
						Data_ShellFired shell2 = CS_0024_003C_003E8__locals4.shell;
						if (CS_0024_003C_003E8__locals4.shell != null)
						{
							bool flag = k.ShellInstanceId == shell2.ShellInstanceId;
							if (!flag)
							{
								return flag;
							}
							MapEntity entity = k.Entity;
							if (k.Entity != null)
							{
								return (byte)(entity.Role & EntityRoles.Enemy) != 0;
							}
						}
					}
					throw new NullReferenceException();
				};
				return Enumerable.Count(Data_KilledEntities, predicate);
			};
			IEnumerable<int> source = Enumerable.Select(Data_ShellsFired, selector);
			object obj = default(object);
			IEnumerable<int> source2 = Enumerable.DefaultIfEmpty(source, (int)(&obj));
			return Enumerable.Max(source2);
		}
	}

	public float BestThreeKillWindowSeconds => GetBestXKillsInSeconds(3);

	public float GetValue(MedalTrackedValue valueId)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 19 Invalid \"Jump target not found in method: 0x180424E6F\"");
		float result = default(float);
		return result;
	}

	public void SetValue(MedalTrackedValue valueId, float value)
	{
		//IL_000e: Expected O, but got I4
		//IL_0039: Expected O, but got I4
		//IL_0046: Expected O, but got I8
		//IL_0060: Expected O, but got I8
		object obj = valueId - 12;
		if ((nint)obj <= 5)
		{
			object obj2 = valueId - 12;
			object obj3 = 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ r8_v1+42505C+v15 @ rax_v2*4]");
			object obj4 = 0 + 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v19 @ rax_v4 (should have been resolved before IL gen)");
		}
	}

	public float GetCustomValue(string key)
	{
		//IL_039e: Expected F4, but got I4
		//IL_03f7: Expected F4, but got I4
		Func<Data_KilledEntity, bool> predicate;
		IEnumerable<Data_KilledEntity> data_KilledEntities;
		if (!string.IsNullOrWhiteSpace(key))
		{
			if (key.StartsWith("Best") && key.EndsWith("KillWindowSeconds"))
			{
				string text = key.Replace("Best", "");
				string s = text.Replace("KillWindowSeconds", "");
				if (int.TryParse(s, out var result))
				{
					return GetBestXKillsInSeconds(result);
				}
			}
			if (key.StartsWith("KillsByRole_"))
			{
				object obj = "KillsByRole_";
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ rdx_v27+10]");
				string text2 = key.Substring(0);
				if (Enum.TryParse<EntityRoles>(text2, out var result2))
				{
					_003C_003Ec__DisplayClass55_0 obj2 = new _003C_003Ec__DisplayClass55_0();
					obj2.role = result2;
					predicate = null;
					_003C_003Ec__DisplayClass55_0 obj3 = obj2;
					nint num = 0;
					data_KilledEntities = Data_KilledEntities;
					goto IL_03d7;
				}
				string message = "Misconfigured Medal Custom Value. '" + key + "' | " + text2 + " not a valid Role";
				Debug.LogError(message);
			}
			if (key.StartsWith("KillsByState_"))
			{
				object obj4 = "KillsByState_";
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v174 @ rdx_v15+10]");
				string text3 = key.Substring(0);
				if (Enum.TryParse<MapEntityStates>(text3, out var result3))
				{
					_003C_003Ec__DisplayClass56_0 obj5 = new _003C_003Ec__DisplayClass56_0();
					obj5.state = result3;
					data_KilledEntities = Data_KilledEntities;
					predicate = null;
					_003C_003Ec__DisplayClass55_0 obj3 = (_003C_003Ec__DisplayClass55_0)(object)obj5;
					nint num = 0;
					goto IL_03d7;
				}
				string message2 = "Misconfigured Medal Custom Value. '" + key + "' | " + text3 + " not a valid State";
				Debug.LogError(message2);
			}
			if (CustomValues != null && CustomValues.TryGetValue(key, out var value))
			{
				return value;
			}
		}
		return 0f;
		IL_03d7:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180873A30");
		int num2 = Enumerable.Count(data_KilledEntities, predicate);
		return num2;
	}

	public unsafe void SetCustomValue(string key, float value)
	{
		//IL_0058: Expected F4, but got Ref
		if (!string.IsNullOrWhiteSpace(key))
		{
			if (CustomValues == null)
			{
				Dictionary<string, float> customValues = new Dictionary<string, float>();
				CustomValues = customValues;
			}
			object obj = default(object);
			CustomValues.set_Item(key, (float)(nint)(&obj));
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string message = $"[MedalTracking] Setting custom variable | {key} -> {arg}";
			Debug.LogError(message);
		}
	}

	public void TrackKill(Data_KilledEntity value)
	{
		if (Data_KilledEntities == null)
		{
			List<Data_KilledEntity> data_KilledEntities = new List<Data_KilledEntity>();
			Data_KilledEntities = data_KilledEntities;
		}
		Data_KilledEntities.Add(value);
	}

	public void TrackShell(Data_ShellFired value)
	{
		if (Data_ShellsFired == null)
		{
			List<Data_ShellFired> data_ShellsFired = new List<Data_ShellFired>();
			Data_ShellsFired = data_ShellsFired;
		}
		Data_ShellsFired.Add(value);
	}

	public void TrackPunchcard(Data_PunchcardUsed value)
	{
		if (Data_PunchcardsUsed == null)
		{
			List<Data_PunchcardUsed> data_PunchcardsUsed = new List<Data_PunchcardUsed>();
			Data_PunchcardsUsed = data_PunchcardsUsed;
		}
		Data_PunchcardsUsed.Add(value);
	}

	public float GetBestXKillsInSeconds(int count)
	{
		//IL_009c: Expected O, but got I4
		//IL_00ae: Expected O, but got I4
		//IL_0258: Expected O, but got I
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Expected O, but got Unknown
		if (count > 0)
		{
			Func<Data_KilledEntity, bool> predicate = _003C_003Ec._003C_003E9__54_0;
			if (_003C_003Ec._003C_003E9__54_0 == null)
			{
				predicate = (_003C_003Ec._003C_003E9__54_0 = delegate(Data_KilledEntity k)
				{
					//IL_0072: Expected I4, but got O
					if (k != null)
					{
						MapEntity entity = k.Entity;
						if (k.Entity != null)
						{
							return (byte)(entity.Role & EntityRoles.Enemy) != 0;
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				});
			}
			IEnumerable<Data_KilledEntity> source = Enumerable.Where(Data_KilledEntities, predicate);
			Func<Data_KilledEntity, float> selector = _003C_003Ec._003C_003E9__54_1;
			if (_003C_003Ec._003C_003E9__54_1 == null)
			{
				selector = (_003C_003Ec._003C_003E9__54_1 = (Data_KilledEntity x) => x.KilledAtTime);
			}
			IEnumerable<float> source2 = Enumerable.Select(source, selector);
			Func<float, float> keySelector = _003C_003Ec._003C_003E9__54_2;
			if (_003C_003Ec._003C_003E9__54_2 == null)
			{
				keySelector = (_003C_003Ec._003C_003E9__54_2 = (float x) => x);
			}
			IOrderedEnumerable<float> source3 = Enumerable.OrderBy(source2, keySelector);
			List<float> list = Enumerable.ToList(source3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rax_v15 (System.Collections.Generic.List`1<System.Single>)+18]");
			if ((nint)0 >= (nint)count)
			{
				object obj = count - 1;
				float num = 3.4028235E+38f;
				object obj2 = 0;
				object obj5 = default(object);
				object obj6 = default(object);
				while (true)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rax_v15 (System.Collections.Generic.List`1<System.Single>)+18]");
					object obj3 = -count;
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
					{
						break;
					}
					object obj4 = obj + obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					float val = (float)obj5 - (float)obj6;
					float num2 = Math.Min(num, val);
					obj2++;
					num = num2;
				}
				return num;
			}
		}
		return 3.4028235E+38f;
	}

	public int GetKillsByRole(EntityRoles role)
	{
		//IL_0051: Expected I4, but got O
		_003C_003Ec__DisplayClass55_0 CS_0024_003C_003E8__locals4 = new _003C_003Ec__DisplayClass55_0();
		if (CS_0024_003C_003E8__locals4 != null)
		{
			CS_0024_003C_003E8__locals4.role = role;
			Func<Data_KilledEntity, bool> predicate = delegate(Data_KilledEntity x)
			{
				//IL_0091: Expected I4, but got O
				//IL_0060: Expected O, but got I4
				//IL_006a: Unknown result type (might be due to invalid IL or missing references)
				//IL_006f: Expected O, but got Unknown
				if (x != null)
				{
					MapEntity entity = x.Entity;
					if (x.Entity != null)
					{
						object obj = entity.Role & CS_0024_003C_003E8__locals4.role;
						object obj2 = obj - CS_0024_003C_003E8__locals4.role;
						return obj2 == null;
					}
				}
				NullReferenceException ex2 = new NullReferenceException();
				return (byte)(int)ex2 != 0;
			};
			return Enumerable.Count(Data_KilledEntities, predicate);
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public int GetKillsByState(MapEntityStates state)
	{
		//IL_0051: Expected I4, but got O
		_003C_003Ec__DisplayClass56_0 CS_0024_003C_003E8__locals4 = new _003C_003Ec__DisplayClass56_0();
		if (CS_0024_003C_003E8__locals4 != null)
		{
			CS_0024_003C_003E8__locals4.state = state;
			Func<Data_KilledEntity, bool> predicate = delegate(Data_KilledEntity x)
			{
				//IL_0091: Expected I4, but got O
				//IL_0060: Expected O, but got I4
				//IL_006a: Unknown result type (might be due to invalid IL or missing references)
				//IL_006f: Expected O, but got Unknown
				if (x != null)
				{
					MapEntity entity = x.Entity;
					if (x.Entity != null)
					{
						object obj = entity.State & CS_0024_003C_003E8__locals4.state;
						object obj2 = obj - CS_0024_003C_003E8__locals4.state;
						return obj2 == null;
					}
				}
				NullReferenceException ex2 = new NullReferenceException();
				return (byte)(int)ex2 != 0;
			};
			return Enumerable.Count(Data_KilledEntities, predicate);
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public MedalTrackedValues()
	{
		List<Data_KilledEntity> data_KilledEntities = new List<Data_KilledEntity>();
		Data_KilledEntities = data_KilledEntities;
		List<Data_ShellFired> data_ShellsFired = new List<Data_ShellFired>();
		Data_ShellsFired = data_ShellsFired;
		List<Data_PunchcardUsed> data_PunchcardsUsed = new List<Data_PunchcardUsed>();
		Data_PunchcardsUsed = data_PunchcardsUsed;
		Dictionary<string, float> customValues = new Dictionary<string, float>();
		CustomValues = customValues;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	private bool _003Cget_ReconUsedAfterFirstShot_003Eb__39_0(Data_PunchcardUsed x)
	{
		//IL_005d: Expected I4, but got O
		//IL_004a: Expected O, but got I4
		//IL_0087: Invalid comparison between F4 and O
		//IL_00aa: Invalid comparison between F4 and I4
		//IL_003c: Expected O, but got I
		if (x != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF410");
			object obj = default(object);
			object obj2;
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ stack_10_v1+30]");
				obj2 = 0;
			}
			else
			{
				obj2 = 0;
			}
			float usedAtTime = x.UsedAtTime;
			bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)usedAtTime) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2);
			float num = x.UsedAtTime - (float)obj2;
			bool flag2 = num == 0f;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			return flag4 & flag3;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private int _003Cget_MostKillsBySingleImpact_003Eb__43_0(Data_ShellFired shell)
	{
		//IL_0056: Expected I4, but got O
		_003C_003Ec__DisplayClass43_0 CS_0024_003C_003E8__locals4 = new _003C_003Ec__DisplayClass43_0();
		if (CS_0024_003C_003E8__locals4 != null)
		{
			CS_0024_003C_003E8__locals4.shell = shell;
			Func<Data_KilledEntity, bool> predicate = delegate(Data_KilledEntity k)
			{
				if (k != null)
				{
					Data_ShellFired shell2 = CS_0024_003C_003E8__locals4.shell;
					if (CS_0024_003C_003E8__locals4.shell != null)
					{
						bool flag = k.ShellInstanceId == shell2.ShellInstanceId;
						if (!flag)
						{
							return flag;
						}
						MapEntity entity = k.Entity;
						if (k.Entity != null)
						{
							return (byte)(entity.Role & EntityRoles.Enemy) != 0;
						}
					}
				}
				throw new NullReferenceException();
			};
			return Enumerable.Count(Data_KilledEntities, predicate);
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}
}
