using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using SleepyNodes;
using TMPro;
using UnityEngine;

public class EndOfMissionUIController : MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass101_0
	{
		public MedalTrackedValues.Data_ShellFired shell;

		internal bool _003Cget_MultiKillShots_003Eb__1(MedalTrackedValues.Data_KilledEntity k)
		{
			if (k != null)
			{
				MedalTrackedValues.Data_ShellFired data_ShellFired = shell;
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

	private sealed class _003C_003Ec__DisplayClass103_0
	{
		public MedalTrackedValues.Data_ShellFired shell;

		internal bool _003Cget_TripleKillShots_003Eb__1(MedalTrackedValues.Data_KilledEntity k)
		{
			if (k != null)
			{
				MedalTrackedValues.Data_ShellFired data_ShellFired = shell;
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

	public TMP_Text Text_MissionName;

	public Transform StatRoot;

	public List<MissionCardMedalSlotUI> Medals;

	public Transform PunchcardRoot;

	public UIPunchcard Prefab_Punchcard;

	public Transform GifRoot;

	public UIImageByteCycler GifImageCycler;

	public GameObject Root_PressKey;

	public GameObject Root_PressKeyProgress;

	public Action<MissionGraph> OnMissionSummaryDismissed;

	private MissionGraph mission;

	private MissionManager.MissionState state;

	private MedalTrackedValues tracker;

	public unsafe float MissionTime
	{
		get
		{
			//IL_006c: Expected O, but got I4
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Expected O, but got Unknown
			//IL_0048: Expected native int or pointer, but got O
			//IL_005d: Expected O, but got I
			//IL_00de: Expected O, but got I4
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			//IL_009c: Expected O, but got Unknown
			//IL_00b4: Expected native int or pointer, but got O
			//IL_00c9: Expected O, but got I
			//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f1: Expected O, but got Unknown
			//IL_00f9: Expected native int or pointer, but got O
			//IL_010c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0111: Expected O, but got Unknown
			//IL_0119: Expected native int or pointer, but got O
			//IL_0143: Expected O, but got I4
			//IL_0262: Unknown result type (might be due to invalid IL or missing references)
			//IL_0267: Expected O, but got Unknown
			//IL_015a: Unknown result type (might be due to invalid IL or missing references)
			//IL_015f: Expected O, but got Unknown
			//IL_0175: Unknown result type (might be due to invalid IL or missing references)
			//IL_017a: Expected O, but got Unknown
			//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bf: Expected O, but got Unknown
			//IL_01cc: Expected native int or pointer, but got O
			//IL_01e1: Expected O, but got I
			MedalTrackedValues medalTrackedValues = tracker;
			_ = 0;
			_ = 0;
			_ = 0;
			float num = default(float);
			object obj = default(object);
			if (tracker != null)
			{
				num = medalTrackedValues.MissionEndTime;
				float value = (float)obj + 24f;
				float? num2 = (float?)(object)(obj - 32);
				_ = medalTrackedValues.MissionEndTime;
				_ = 0;
				*(float?*)(nint)num2 = value;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-20]");
				object obj2 = 0;
			}
			else
			{
				object obj2 = 0;
			}
			MedalTrackedValues medalTrackedValues2 = tracker;
			if (tracker != null)
			{
				num = medalTrackedValues2.MissionStartTime;
				float value2 = (float)obj + 24f;
				float? num3 = (float?)(object)(obj - 32);
				_ = medalTrackedValues2.MissionStartTime;
				_ = 0;
				*(float?*)(nint)num3 = value2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-20]");
				object obj3 = 0;
			}
			else
			{
				_ = 0;
				object obj3 = 0;
			}
			float? num4 = (float?)(object)(obj + 40);
			float value3 = default(float);
			*(float?*)(nint)num4 = value3;
			float? num5 = (float?)(object)(obj + 48);
			*(float?*)(nint)num5 = value3;
			float? num6 = default(float?);
			float? num7 = default(float?);
			object obj4 = (object?)num6 & (object?)num7;
			bool flag = obj4 == null;
			object obj5 = 0;
			if (!flag)
			{
				float? num8 = (float?)(object)(obj + 40);
				float valueOrDefault = ((float?*)num8)->GetValueOrDefault();
				float? num9 = (float?)(object)(obj + 48);
				float valueOrDefault2 = ((float?*)num9)->GetValueOrDefault();
				float num10 = num - num;
				float value4 = (float)obj + 24f;
				_ = 0;
				float? num11 = (float?)(object)(obj - 32);
				*(float?*)(nint)num11 = value4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-20]");
				obj5 = 0;
			}
			float? num12 = (float?)(object)(obj - 40);
			float valueOrDefault3 = ((float?*)num12)->GetValueOrDefault();
			return num;
		}
	}

	public float CounterBatteryTimeRemaining
	{
		get
		{
			//IL_0039: Expected F4, but got I4
			MedalTrackedValues medalTrackedValues = tracker;
			if (tracker != null)
			{
				return medalTrackedValues.CounterBatteryTimeRemaining;
			}
			return 0f;
		}
	}

	public int Kills
	{
		get
		{
			if (tracker != null)
			{
				return tracker.Kills;
			}
			return 0;
		}
	}

	public int TargetKills
	{
		get
		{
			if (tracker != null)
			{
				return tracker.TargetKills;
			}
			return 0;
		}
	}

	public int EnemyKills
	{
		get
		{
			if (tracker != null)
			{
				return tracker.EnemyKills;
			}
			return 0;
		}
	}

	public int AllyKills
	{
		get
		{
			if (tracker != null)
			{
				return tracker.AllyKills;
			}
			return 0;
		}
	}

	public int StarsKilled
	{
		get
		{
			if (tracker != null)
			{
				return tracker.StarsKilled;
			}
			return 0;
		}
	}

	public int ShotsFiredAll
	{
		get
		{
			//IL_0066: Expected I4, but got O
			//IL_004e: Expected I4, but got O
			//IL_0015: Expected O, but got I
			//IL_005c: Expected I4, but got O
			int num = (int)tracker;
			if (tracker != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnVal1 @ rax_v2 (System.Int32)+18]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnVal1 @ rax_v2 (System.Int32)+18]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rax_v3+18]");
					return 0;
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			}
			return (int)tracker;
		}
	}

	public int ShotsFired
	{
		get
		{
			if (tracker != null)
			{
				return tracker.ShotsFired;
			}
			return 0;
		}
	}

	public int ShotsHit
	{
		get
		{
			if (tracker != null)
			{
				return tracker.ShotsHit;
			}
			return 0;
		}
	}

	public int STARUsed
	{
		get
		{
			if (tracker != null)
			{
				return tracker.STARUsed;
			}
			return 0;
		}
	}

	public float AverageImpactDistanceFromNearestTarget
	{
		get
		{
			//IL_0039: Expected F4, but got I4
			if (tracker != null)
			{
				return tracker.AverageImpactDistanceFromNearestTarget;
			}
			return 0f;
		}
	}

	public float FirstShotTime
	{
		get
		{
			//IL_0039: Expected F4, but got I4
			if (tracker != null)
			{
				return tracker.FirstShotTime;
			}
			return 0f;
		}
	}

	public float LastTargetDestroyedTime
	{
		get
		{
			//IL_0039: Expected F4, but got I4
			if (tracker != null)
			{
				return tracker.LastTargetDestroyedTime;
			}
			return 0f;
		}
	}

	public int RequisitionPointsSpent
	{
		get
		{
			if (tracker != null)
			{
				return tracker.RequisitionPointsSpent;
			}
			return 0;
		}
	}

	public int ReconUsed
	{
		get
		{
			if (tracker != null)
			{
				return tracker.ReconUsed;
			}
			return 0;
		}
	}

	public int ReconUsedAfterFirstShot
	{
		get
		{
			if (tracker != null)
			{
				return tracker.ReconUsedAfterFirstShot;
			}
			return 0;
		}
	}

	public int LongestKillStreak
	{
		get
		{
			if (tracker != null)
			{
				return tracker.LongestKillStreak;
			}
			return 0;
		}
	}

	public int MostKillsBySingleImpact
	{
		get
		{
			if (tracker != null)
			{
				return tracker.MostKillsBySingleImpact;
			}
			return 0;
		}
	}

	public float BestThreeKillWindowSeconds
	{
		get
		{
			//IL_0039: Expected F4, but got I4
			if (tracker != null)
			{
				return tracker.BestThreeKillWindowSeconds;
			}
			return 0f;
		}
	}

	public float AccuracyPercent
	{
		get
		{
			//IL_00b9: Expected F4, but got I4
			if (tracker != null)
			{
				int shotsFired = tracker.ShotsFired;
				if (shotsFired > 0)
				{
					int num;
					if (tracker != null)
					{
						int shotsHit = tracker.ShotsHit;
						num = shotsHit;
					}
					else
					{
						num = 0;
					}
					bool flag = tracker == null;
					int num2 = 0;
					if (!flag)
					{
						int shotsFired2 = tracker.ShotsFired;
						num2 = shotsFired2;
					}
					int num3 = num / num2;
					return (float)num3 * 100f;
				}
			}
			return 0f;
		}
	}

	public float MissPercent
	{
		get
		{
			//IL_00e3: Expected F4, but got I4
			//IL_0137: Expected O, but got I4
			//IL_013f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0144: Expected I4, but got Unknown
			if (tracker != null)
			{
				int shotsFired = tracker.ShotsFired;
				if (shotsFired > 0)
				{
					int num;
					if (tracker != null)
					{
						int shotsFired2 = tracker.ShotsFired;
						num = shotsFired2;
					}
					else
					{
						num = 0;
					}
					int num2;
					if (tracker != null)
					{
						int shotsHit = tracker.ShotsHit;
						num2 = shotsHit;
					}
					else
					{
						num2 = 0;
					}
					bool flag = tracker == null;
					int num3 = 0;
					if (!flag)
					{
						int shotsFired3 = tracker.ShotsFired;
						num3 = shotsFired3;
					}
					object obj = num - num2;
					int num4 = obj / num3;
					return (float)num4 * 100f;
				}
			}
			return 0f;
		}
	}

	public int ShotsMissed
	{
		get
		{
			int num;
			if (tracker != null)
			{
				int shotsFired = tracker.ShotsFired;
				num = shotsFired;
			}
			else
			{
				num = 0;
			}
			bool flag = (nint)tracker < 0;
			int num2 = ((tracker != null) ? tracker.ShotsHit : 0);
			int num3 = num - num2;
			int result = 0;
			if (!flag)
			{
				result = num3;
			}
			return result;
		}
	}

	public float KillsPerShot
	{
		get
		{
			//IL_00b9: Expected F4, but got I4
			if (tracker != null)
			{
				int shotsFired = tracker.ShotsFired;
				if (shotsFired > 0)
				{
					int num;
					if (tracker != null)
					{
						int enemyKills = tracker.EnemyKills;
						num = enemyKills;
					}
					else
					{
						num = 0;
					}
					bool flag = tracker == null;
					int num2 = 0;
					if (!flag)
					{
						int shotsFired2 = tracker.ShotsFired;
						num2 = shotsFired2;
					}
					return (float)num / (float)num2;
				}
			}
			return 0f;
		}
	}

	public float KillsPerHit
	{
		get
		{
			//IL_00b9: Expected F4, but got I4
			if (tracker != null)
			{
				int shotsHit = tracker.ShotsHit;
				if (shotsHit > 0)
				{
					int num;
					if (tracker != null)
					{
						int enemyKills = tracker.EnemyKills;
						num = enemyKills;
					}
					else
					{
						num = 0;
					}
					bool flag = tracker == null;
					int num2 = 0;
					if (!flag)
					{
						int shotsHit2 = tracker.ShotsHit;
						num2 = shotsHit2;
					}
					return (float)num / (float)num2;
				}
			}
			return 0f;
		}
	}

	public float TargetsPerShot
	{
		get
		{
			//IL_00b9: Expected F4, but got I4
			if (tracker != null)
			{
				int shotsFired = tracker.ShotsFired;
				if (shotsFired > 0)
				{
					int num;
					if (tracker != null)
					{
						int targetKills = tracker.TargetKills;
						num = targetKills;
					}
					else
					{
						num = 0;
					}
					bool flag = tracker == null;
					int num2 = 0;
					if (!flag)
					{
						int shotsFired2 = tracker.ShotsFired;
						num2 = shotsFired2;
					}
					return (float)num / (float)num2;
				}
			}
			return 0f;
		}
	}

	public float FriendlyFirePercent
	{
		get
		{
			//IL_00b9: Expected F4, but got I4
			if (tracker != null)
			{
				int kills = tracker.Kills;
				if (kills > 0)
				{
					int num;
					if (tracker != null)
					{
						int allyKills = tracker.AllyKills;
						num = allyKills;
					}
					else
					{
						num = 0;
					}
					bool flag = tracker == null;
					int num2 = 0;
					if (!flag)
					{
						int kills2 = tracker.Kills;
						num2 = kills2;
					}
					int num3 = num / num2;
					return (float)num3 * 100f;
				}
			}
			return 0f;
		}
	}

	public float EnemyKillPercent
	{
		get
		{
			//IL_00b9: Expected F4, but got I4
			if (tracker != null)
			{
				int kills = tracker.Kills;
				if (kills > 0)
				{
					int num;
					if (tracker != null)
					{
						int enemyKills = tracker.EnemyKills;
						num = enemyKills;
					}
					else
					{
						num = 0;
					}
					bool flag = tracker == null;
					int num2 = 0;
					if (!flag)
					{
						int kills2 = tracker.Kills;
						num2 = kills2;
					}
					int num3 = num / num2;
					return (float)num3 * 100f;
				}
			}
			return 0f;
		}
	}

	public float TargetKillPercent
	{
		get
		{
			//IL_00b9: Expected F4, but got I4
			if (tracker != null)
			{
				int kills = tracker.Kills;
				if (kills > 0)
				{
					int num;
					if (tracker != null)
					{
						int targetKills = tracker.TargetKills;
						num = targetKills;
					}
					else
					{
						num = 0;
					}
					bool flag = tracker == null;
					int num2 = 0;
					if (!flag)
					{
						int kills2 = tracker.Kills;
						num2 = kills2;
					}
					int num3 = num / num2;
					return (float)num3 * 100f;
				}
			}
			return 0f;
		}
	}

	public float AverageKillsPerImpact
	{
		get
		{
			//IL_00b9: Expected F4, but got I4
			if (tracker != null)
			{
				int shotsHit = tracker.ShotsHit;
				if (shotsHit > 0)
				{
					int num;
					if (tracker != null)
					{
						int enemyKills = tracker.EnemyKills;
						num = enemyKills;
					}
					else
					{
						num = 0;
					}
					bool flag = tracker == null;
					int num2 = 0;
					if (!flag)
					{
						int shotsHit2 = tracker.ShotsHit;
						num2 = shotsHit2;
					}
					return (float)num / (float)num2;
				}
			}
			return 0f;
		}
	}

	public float AverageStarsPerKill
	{
		get
		{
			//IL_00b9: Expected F4, but got I4
			if (tracker != null)
			{
				int kills = tracker.Kills;
				if (kills > 0)
				{
					int num;
					if (tracker != null)
					{
						int starsKilled = tracker.StarsKilled;
						num = starsKilled;
					}
					else
					{
						num = 0;
					}
					bool flag = tracker == null;
					int num2 = 0;
					if (!flag)
					{
						int kills2 = tracker.Kills;
						num2 = kills2;
					}
					return (float)num / (float)num2;
				}
			}
			return 0f;
		}
	}

	public float MissionTimeMinutes
	{
		get
		{
			float missionTime = MissionTime;
			return missionTime / 60f;
		}
	}

	public float TimeToFirstShot
	{
		get
		{
			//IL_00b1: Expected F4, but got I4
			//IL_0037: Invalid comparison between F4 and I4
			//IL_0068: Expected F4, but got I4
			if (tracker != null)
			{
				float firstShotTime = tracker.FirstShotTime;
				if (firstShotTime > 0f)
				{
					bool flag = tracker == null;
					float num = 0f;
					if (!flag)
					{
						float firstShotTime2 = tracker.FirstShotTime;
						num = firstShotTime2;
					}
					MedalTrackedValues medalTrackedValues = tracker;
					return num - medalTrackedValues.MissionStartTime;
				}
			}
			return 0f;
		}
	}

	public float TimeToLastTargetKill
	{
		get
		{
			//IL_00b1: Expected F4, but got I4
			//IL_0037: Invalid comparison between F4 and I4
			//IL_0068: Expected F4, but got I4
			if (tracker != null)
			{
				float lastTargetDestroyedTime = tracker.LastTargetDestroyedTime;
				if (lastTargetDestroyedTime > 0f)
				{
					bool flag = tracker == null;
					float num = 0f;
					if (!flag)
					{
						float lastTargetDestroyedTime2 = tracker.LastTargetDestroyedTime;
						num = lastTargetDestroyedTime2;
					}
					MedalTrackedValues medalTrackedValues = tracker;
					return num - medalTrackedValues.MissionStartTime;
				}
			}
			return 0f;
		}
	}

	public float TimeFromFirstShotToLastTargetKill
	{
		get
		{
			//IL_0124: Expected F4, but got I4
			//IL_0037: Invalid comparison between F4 and I4
			//IL_0092: Expected F4, but got I4
			//IL_00b4: Expected F4, but got I4
			//IL_00fd: Expected F4, but got I4
			//IL_0179: Expected F4, but got I4
			if (tracker != null)
			{
				float firstShotTime = tracker.FirstShotTime;
				if (firstShotTime > 0f)
				{
					float num;
					if (tracker != null)
					{
						float lastTargetDestroyedTime = tracker.LastTargetDestroyedTime;
						num = lastTargetDestroyedTime;
					}
					else
					{
						num = 0f;
					}
					float num2 = ((tracker == null) ? 0f : tracker.FirstShotTime);
					if (!(num < num2))
					{
						float num3;
						if (tracker != null)
						{
							float lastTargetDestroyedTime2 = tracker.LastTargetDestroyedTime;
							num3 = lastTargetDestroyedTime2;
						}
						else
						{
							num3 = 0f;
						}
						bool flag = tracker == null;
						float num4 = 0f;
						if (!flag)
						{
							float firstShotTime2 = tracker.FirstShotTime;
							num4 = firstShotTime2;
						}
						return num3 - num4;
					}
				}
			}
			return 0f;
		}
	}

	public float ShotsPerMinute
	{
		get
		{
			//IL_0013: Invalid comparison between F4 and I4
			//IL_0071: Expected F4, but got I4
			float missionTime = MissionTime;
			if (!(missionTime > 0f))
			{
				return 0f;
			}
			int num = ((tracker != null) ? tracker.ShotsFired : 0);
			float missionTime2 = MissionTime;
			float num2 = missionTime2 / 60f;
			return (float)num / num2;
		}
	}

	public float KillsPerMinute
	{
		get
		{
			//IL_0013: Invalid comparison between F4 and I4
			//IL_0071: Expected F4, but got I4
			float missionTime = MissionTime;
			if (!(missionTime > 0f))
			{
				return 0f;
			}
			int num = ((tracker != null) ? tracker.EnemyKills : 0);
			float missionTime2 = MissionTime;
			float num2 = missionTime2 / 60f;
			return (float)num / num2;
		}
	}

	public float RequisitionPerKill
	{
		get
		{
			//IL_00b9: Expected F4, but got I4
			if (tracker != null)
			{
				int enemyKills = tracker.EnemyKills;
				if (enemyKills > 0)
				{
					int num;
					if (tracker != null)
					{
						int requisitionPointsSpent = tracker.RequisitionPointsSpent;
						num = requisitionPointsSpent;
					}
					else
					{
						num = 0;
					}
					bool flag = tracker == null;
					int num2 = 0;
					if (!flag)
					{
						int enemyKills2 = tracker.EnemyKills;
						num2 = enemyKills2;
					}
					return (float)num / (float)num2;
				}
			}
			return 0f;
		}
	}

	public float RequisitionPerTarget
	{
		get
		{
			//IL_00b9: Expected F4, but got I4
			if (tracker != null)
			{
				int targetKills = tracker.TargetKills;
				if (targetKills > 0)
				{
					int num;
					if (tracker != null)
					{
						int requisitionPointsSpent = tracker.RequisitionPointsSpent;
						num = requisitionPointsSpent;
					}
					else
					{
						num = 0;
					}
					bool flag = tracker == null;
					int num2 = 0;
					if (!flag)
					{
						int targetKills2 = tracker.TargetKills;
						num2 = targetKills2;
					}
					return (float)num / (float)num2;
				}
			}
			return 0f;
		}
	}

	public float ReconAfterFirstShotPercent
	{
		get
		{
			//IL_00b9: Expected F4, but got I4
			if (tracker != null)
			{
				int reconUsed = tracker.ReconUsed;
				if (reconUsed > 0)
				{
					int num;
					if (tracker != null)
					{
						int reconUsedAfterFirstShot = tracker.ReconUsedAfterFirstShot;
						num = reconUsedAfterFirstShot;
					}
					else
					{
						num = 0;
					}
					bool flag = tracker == null;
					int num2 = 0;
					if (!flag)
					{
						int reconUsed2 = tracker.ReconUsed;
						num2 = reconUsed2;
					}
					int num3 = num / num2;
					return (float)num3 * 100f;
				}
			}
			return 0f;
		}
	}

	public float STARUsagePercent
	{
		get
		{
			//IL_0088: Expected F4, but got I4
			MedalTrackedValues medalTrackedValues = tracker;
			if (tracker != null)
			{
				List<MedalTrackedValues.Data_ShellFired> data_ShellsFired = medalTrackedValues.Data_ShellsFired;
				if (data_ShellsFired._size > 0)
				{
					int sTARUsed = tracker.STARUsed;
					MedalTrackedValues medalTrackedValues2 = tracker;
					int num;
					if (tracker != null)
					{
						List<MedalTrackedValues.Data_ShellFired> data_ShellsFired2 = medalTrackedValues2.Data_ShellsFired;
						num = data_ShellsFired2._size;
					}
					else
					{
						num = 0;
					}
					int num2 = sTARUsed / num;
					return (float)num2 * 100f;
				}
			}
			return 0f;
		}
	}

	public bool PerfectAccuracy
	{
		get
		{
			//IL_00ee: Expected O, but got I4
			if (tracker != null)
			{
				int shotsFired = tracker.ShotsFired;
				if (shotsFired > 0)
				{
					int num;
					if (tracker != null)
					{
						int shotsHit = tracker.ShotsHit;
						num = shotsHit;
					}
					else
					{
						num = 0;
					}
					bool flag = tracker == null;
					int num2 = 0;
					if (!flag)
					{
						int shotsFired2 = tracker.ShotsFired;
						num2 = shotsFired2;
					}
					object obj = num - num2;
					return obj == null;
				}
			}
			return false;
		}
	}

	public bool NoFriendlyFire
	{
		get
		{
			if (tracker != null)
			{
				int allyKills = tracker.AllyKills;
				return allyKills == 0;
			}
			return true;
		}
	}

	public bool NoReconUsed
	{
		get
		{
			if (tracker != null)
			{
				int reconUsed = tracker.ReconUsed;
				return reconUsed == 0;
			}
			return true;
		}
	}

	public bool NoSTARUsed
	{
		get
		{
			if (tracker != null)
			{
				int sTARUsed = tracker.STARUsed;
				return sTARUsed == 0;
			}
			return true;
		}
	}

	public int MultiKillShots
	{
		get
		{
			MedalTrackedValues medalTrackedValues = tracker;
			if (tracker != null)
			{
				Func<MedalTrackedValues.Data_ShellFired, bool> predicate = delegate(MedalTrackedValues.Data_ShellFired shell)
				{
					//IL_00df: Expected I4, but got O
					//IL_0078: Expected O, but got I4
					//IL_008e: Unknown result type (might be due to invalid IL or missing references)
					//IL_0093: Expected I4, but got Unknown
					_003C_003Ec__DisplayClass101_0 CS_0024_003C_003E8__locals4 = new _003C_003Ec__DisplayClass101_0();
					if (CS_0024_003C_003E8__locals4 != null)
					{
						CS_0024_003C_003E8__locals4.shell = shell;
						MedalTrackedValues medalTrackedValues2 = tracker;
						if (tracker != null)
						{
							Func<MedalTrackedValues.Data_KilledEntity, bool> predicate2 = delegate(MedalTrackedValues.Data_KilledEntity k)
							{
								if (k != null)
								{
									MedalTrackedValues.Data_ShellFired shell2 = CS_0024_003C_003E8__locals4.shell;
									if (CS_0024_003C_003E8__locals4.shell != null)
									{
										bool flag3 = k.ShellInstanceId == shell2.ShellInstanceId;
										if (!flag3)
										{
											return flag3;
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
							int num = Enumerable.Count(medalTrackedValues2.Data_KilledEntities, predicate2);
							object obj = num - 2;
							int num2 = num ^ 2;
							int num3 = num ^ obj;
							int num4 = num2 & num3;
							bool flag = num4 < 0;
							bool flag2 = (nint)obj < 0;
							return flag2 == flag;
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				};
				return Enumerable.Count(medalTrackedValues.Data_ShellsFired, predicate);
			}
			return 0;
		}
	}

	public int TripleKillShots
	{
		get
		{
			MedalTrackedValues medalTrackedValues = tracker;
			if (tracker != null)
			{
				Func<MedalTrackedValues.Data_ShellFired, bool> predicate = delegate(MedalTrackedValues.Data_ShellFired shell)
				{
					//IL_00df: Expected I4, but got O
					//IL_0078: Expected O, but got I4
					//IL_008e: Unknown result type (might be due to invalid IL or missing references)
					//IL_0093: Expected I4, but got Unknown
					_003C_003Ec__DisplayClass103_0 CS_0024_003C_003E8__locals4 = new _003C_003Ec__DisplayClass103_0();
					if (CS_0024_003C_003E8__locals4 != null)
					{
						CS_0024_003C_003E8__locals4.shell = shell;
						MedalTrackedValues medalTrackedValues2 = tracker;
						if (tracker != null)
						{
							Func<MedalTrackedValues.Data_KilledEntity, bool> predicate2 = delegate(MedalTrackedValues.Data_KilledEntity k)
							{
								if (k != null)
								{
									MedalTrackedValues.Data_ShellFired shell2 = CS_0024_003C_003E8__locals4.shell;
									if (CS_0024_003C_003E8__locals4.shell != null)
									{
										bool flag3 = k.ShellInstanceId == shell2.ShellInstanceId;
										if (!flag3)
										{
											return flag3;
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
							int num = Enumerable.Count(medalTrackedValues2.Data_KilledEntities, predicate2);
							object obj = num - 3;
							int num2 = num ^ 3;
							int num3 = num ^ obj;
							int num4 = num2 & num3;
							bool flag = num4 < 0;
							bool flag2 = (nint)obj < 0;
							return flag2 == flag;
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				};
				return Enumerable.Count(medalTrackedValues.Data_ShellsFired, predicate);
			}
			return 0;
		}
	}

	public void OnEnable()
	{
		MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
		MissionManager.MissionState missionState = (((object)MissionManager._003CInstance_003Ek__BackingField == null) ? null : missionManager.CurrentMissionState);
		state = missionState;
		MissionManager missionManager2 = MissionManager._003CInstance_003Ek__BackingField;
		MedalTrackedValues medalTrackedValues;
		if ((object)MissionManager._003CInstance_003Ek__BackingField != null)
		{
			MissionManager.MissionState currentMissionState = missionManager2.CurrentMissionState;
			if (missionManager2.CurrentMissionState != null)
			{
				medalTrackedValues = currentMissionState.TrackingValues;
				goto IL_00db;
			}
		}
		medalTrackedValues = null;
		goto IL_00db;
		IL_00db:
		tracker = medalTrackedValues;
		MissionManager missionManager3 = MissionManager._003CInstance_003Ek__BackingField;
		bool flag = (object)MissionManager._003CInstance_003Ek__BackingField == null;
		MissionGraph missionGraph = null;
		if (!flag)
		{
			missionGraph = missionManager3._003CCurrentMission_003Ek__BackingField;
		}
		mission = missionGraph;
		UpdateUI();
	}

	public void OnDisable()
	{
		//IL_009c: Expected O, but got I
		//IL_00ac: Expected O, but got I
		//IL_00bc: Expected O, but got I
		Root_PressKey.SetActive(value: false);
		Root_PressKeyProgress.SetActive(value: false);
		UnityEngine.Object.FindFirstObjectByType<RecordPlayerController>()?.DismissNewspaperMusic();
		Action<MissionGraph> onMissionSummaryDismissed = OnMissionSummaryDismissed;
		if (OnMissionSummaryDismissed != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v81 @ rcx_v8 (System.Action`1<SleepyNodes.MissionGraph>)+18]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v81 @ rcx_v8 (System.Action`1<SleepyNodes.MissionGraph>)+28]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v81 @ rcx_v8 (System.Action`1<SleepyNodes.MissionGraph>)+40]");
			object obj3 = 0;
			MissionGraph missionGraph = mission;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v77 @ rax_v9 (should have been resolved before IL gen)");
		}
	}

	public void Init()
	{
		MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
		MissionManager.MissionState missionState = (((object)MissionManager._003CInstance_003Ek__BackingField == null) ? null : missionManager.CurrentMissionState);
		state = missionState;
		MissionManager missionManager2 = MissionManager._003CInstance_003Ek__BackingField;
		MedalTrackedValues medalTrackedValues;
		if ((object)MissionManager._003CInstance_003Ek__BackingField != null)
		{
			MissionManager.MissionState currentMissionState = missionManager2.CurrentMissionState;
			if (missionManager2.CurrentMissionState != null)
			{
				medalTrackedValues = currentMissionState.TrackingValues;
				goto IL_00db;
			}
		}
		medalTrackedValues = null;
		goto IL_00db;
		IL_00db:
		tracker = medalTrackedValues;
		MissionManager missionManager3 = MissionManager._003CInstance_003Ek__BackingField;
		bool flag = (object)MissionManager._003CInstance_003Ek__BackingField == null;
		MissionGraph missionGraph = null;
		if (!flag)
		{
			missionGraph = missionManager3._003CCurrentMission_003Ek__BackingField;
		}
		mission = missionGraph;
		UpdateUI();
	}

	public unsafe void UpdateUI()
	{
		//IL_006c: Expected I, but got O
		//IL_017c: Expected O, but got I4
		//IL_009e: Expected O, but got I4
		//IL_00a3: Expected I, but got O
		//IL_0690: Expected O, but got I4
		//IL_00d8: Expected O, but got I4
		//IL_00dd: Expected I, but got O
		//IL_01cd: Expected O, but got I4
		//IL_06df: Expected O, but got I4
		//IL_0124: Expected O, but got I4
		//IL_0129: Expected I, but got O
		//IL_0219: Expected O, but got I4
		//IL_0710: Unknown result type (might be due to invalid IL or missing references)
		//IL_0715: Expected O, but got Unknown
		//IL_0153: Expected I, but got O
		//IL_025f: Expected O, but got I4
		//IL_0752: Expected O, but got I4
		//IL_05b0: Expected O, but got I4
		//IL_0788: Expected O, but got I4
		//IL_05e3: Expected O, but got I4
		//IL_02df: Expected I, but got O
		//IL_08d0: Expected I, but got O
		//IL_07ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d3: Expected O, but got Unknown
		//IL_030d: Expected O, but got I4
		//IL_0318: Expected I, but got O
		//IL_09d7: Expected I, but got O
		//IL_092f: Expected O, but got I4
		//IL_093c: Expected I, but got O
		//IL_060c: Expected I, but got O
		//IL_0a7a: Expected O, but got I4
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Expected O, but got Unknown
		//IL_0643: Expected O, but got I4
		//IL_0353: Expected O, but got I4
		//IL_035e: Expected I, but got O
		//IL_09ff: Expected O, but got I4
		//IL_0a0c: Expected I, but got O
		//IL_097b: Expected O, but got I4
		//IL_0988: Expected I, but got O
		//IL_0ab1: Expected O, but got Ref
		//IL_0ab9: Expected O, but got Ref
		//IL_03a6: Expected O, but got I4
		//IL_03b1: Expected I, but got O
		//IL_0a5b: Expected I, but got O
		//IL_03f1: Expected O, but got I4
		//IL_03fc: Expected I, but got O
		//IL_0ad0: Expected I, but got O
		//IL_0a42: Expected I, but got O
		//IL_0b5f: Expected I, but got O
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Expected O, but got Unknown
		//IL_0450: Expected O, but got I4
		//IL_0dbe: Expected O, but got I
		//IL_0fbe: Expected O, but got I4
		//IL_0d1d: Expected O, but got I4
		//IL_0d33: Expected O, but got I
		//IL_0d3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d41: Expected O, but got Unknown
		//IL_0d49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4e: Expected O, but got Unknown
		//IL_0490: Expected O, but got I4
		//IL_10d3: Expected O, but got I4
		//IL_10e1: Expected I, but got O
		//IL_0b91: Expected I, but got O
		//IL_04d6: Expected O, but got I4
		//IL_0c24: Expected O, but got I4
		//IL_108a: Expected I, but got O
		//IL_0bc9: Expected O, but got I
		//IL_0514: Expected O, but got I4
		//IL_0ff5: Expected O, but got I4
		//IL_0c31: Expected I, but got O
		//IL_0c41: Expected O, but got I
		//IL_0c68: Expected O, but got I4
		//IL_0c76: Expected I, but got O
		//IL_0d60: Expected O, but got I4
		//IL_0d76: Expected O, but got I
		//IL_0d8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d92: Expected O, but got Unknown
		//IL_0d9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9f: Expected O, but got Unknown
		//IL_114c: Expected I, but got O
		//IL_0c9c: Expected O, but got I
		//IL_0cc4: Expected O, but got I4
		//IL_0cd2: Expected I, but got O
		//IL_0593: Expected I, but got O
		//IL_0e86: Expected I, but got O
		//IL_0f41: Expected I, but got O
		if (tracker == null || !(mission != null) || state == null)
		{
			return;
		}
		bool flag = Text_MissionName != null;
		bool flag2 = !flag;
		nint num = unchecked((nint)null);
		if (flag2)
		{
			goto IL_0158;
		}
		MissionGraph missionGraph = mission;
		bool flag3 = (object)mission == null;
		List<PunchcardDefinitionV2>.Enumerator enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
		num = unchecked((nint)null);
		UnityEngine.Object text_MissionName = Text_MissionName;
		if (!flag3)
		{
			bool flag4 = missionGraph.MissionName == null;
			enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
			num = unchecked((nint)null);
			text_MissionName = (UnityEngine.Object)(object)missionGraph.MissionName;
			if (!flag4)
			{
				string text = missionGraph.MissionName.Get();
				bool flag5 = (object)Text_MissionName == null;
				enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
				num = unchecked((nint)null);
				text_MissionName = (UnityEngine.Object)(object)missionGraph.MissionName;
				if (!flag5)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181CA2AE0");
					num = unchecked((nint)null);
					goto IL_0158;
				}
			}
		}
		goto IL_0eaf;
		IL_0eaf:
		throw new NullReferenceException();
		IL_0158:
		List<MissionCardMedalSlotUI> medals = Medals;
		bool flag6 = Medals == null;
		enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
		text_MissionName = null;
		nint num3 = default(nint);
		nint num2 = num3;
		UnityEngine.Object obj = null;
		nint num4 = num;
		UnityEngine.Object obj2 = null;
		if (!flag6)
		{
			Component component = default(Component);
			UnityEngine.Object obj3 = default(UnityEngine.Object);
			object obj4 = default(object);
			List<PunchcardDefinitionV2>.Enumerator enumerator2 = default(List<PunchcardDefinitionV2>.Enumerator);
			MedalTier medalTier = default(MedalTier);
			Component component3 = default(Component);
			MedalTier medalTier4 = default(MedalTier);
			object obj13 = default(object);
			object obj14 = default(object);
			object obj22 = default(object);
			Component component4 = default(Component);
			IntPtr intPtr = default(IntPtr);
			List<PunchcardDefinitionV2>.Enumerator enumerator4 = default(List<PunchcardDefinitionV2>.Enumerator);
			PunchcardDefinitionV2 def = default(PunchcardDefinitionV2);
			while (true)
			{
				if ((nint)obj2 < medals._size)
				{
					text_MissionName = (UnityEngine.Object)(object)Medals;
					bool flag7 = Medals == null;
					enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
					num3 = num2;
					num = num4;
					if (flag7)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					MissionGraph missionGraph2 = mission;
					bool flag8 = (object)mission == null;
					enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
					num3 = 0;
					num = (nint)(&component);
					if (flag8)
					{
						break;
					}
					List<MedalCategoryDefinition> medals2 = missionGraph2.Medals;
					bool flag9 = missionGraph2.Medals == null;
					enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
					num3 = 0;
					num = (nint)(&component);
					if (flag9)
					{
						break;
					}
					bool flag10 = medals2._size <= (nint)obj;
					num3 = 0;
					num = (nint)(&component);
					if (!flag10)
					{
						MissionGraph missionGraph3 = mission;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						bool flag11 = obj3 != null;
						num3 = 0;
						num = unchecked((nint)null);
						text_MissionName = obj3;
						if (flag11)
						{
							bool flag12 = (object)component == null;
							enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
							num3 = 0;
							num = unchecked((nint)null);
							text_MissionName = obj3;
							if (flag12)
							{
								break;
							}
							GameObject gameObject = component.gameObject;
							bool flag13 = (object)gameObject == null;
							enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
							num3 = 0;
							num = unchecked((nint)null);
							text_MissionName = component;
							if (flag13)
							{
								break;
							}
							gameObject.SetActive(value: true);
							MissionGraph missionGraph4 = mission;
							bool flag14 = (object)mission == null;
							enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
							num3 = 0;
							num = unchecked((nint)null);
							text_MissionName = gameObject;
							if (flag14)
							{
								break;
							}
							text_MissionName = (UnityEngine.Object)(object)missionGraph4.Medals;
							bool flag15 = missionGraph4.Medals == null;
							enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
							num3 = 0;
							num = unchecked((nint)null);
							if (flag15)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							text_MissionName = (UnityEngine.Object)(component + 32);
							MissionManager.MissionState missionState = state;
							bool flag16 = state == null;
							enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
							num3 = 0;
							num = (nint)(&obj4);
							if (flag16)
							{
								break;
							}
							MissionGraph missionGraph5 = mission;
							bool flag17 = (object)mission == null;
							enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
							num3 = 0;
							num = (nint)(&obj4);
							if (flag17)
							{
								break;
							}
							text_MissionName = (UnityEngine.Object)(object)missionGraph5.Medals;
							bool flag18 = missionGraph5.Medals == null;
							enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
							num3 = 0;
							num = (nint)(&obj4);
							if (flag18)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							bool flag19 = (object)enumerator2 == null;
							enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
							num3 = 0;
							num = (nint)(&enumerator2);
							if (flag19)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806910A0");
							if (medalTier >= MedalTier.Unearned)
							{
								bool flag20 = medalTier > MedalTier.Gold;
								MedalTier tier = MedalTier.Gold;
								if (!flag20)
								{
									tier = medalTier;
								}
								((MissionCardMedalSlotUI)component).SetTier(tier);
								num3 = (nint)(&medalTier);
								num = unchecked((nint)null);
							}
							else
							{
								((MissionCardMedalSlotUI)component).SetTier(MedalTier.Unearned);
								num3 = (nint)(&medalTier);
								num = unchecked((nint)null);
							}
							goto IL_0611;
						}
					}
					bool flag21 = (object)component == null;
					enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
					if (flag21)
					{
						break;
					}
					GameObject gameObject2 = component.gameObject;
					bool flag22 = (object)gameObject2 == null;
					enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
					text_MissionName = component;
					if (flag22)
					{
						break;
					}
					gameObject2.SetActive(value: false);
					num = unchecked((nint)null);
					goto IL_0611;
				}
				bool flag23 = (object)StatRoot == null;
				enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
				num3 = num2;
				num = num4;
				text_MissionName = StatRoot;
				if (flag23)
				{
					break;
				}
				TextValueLoader[] componentsInChildren = StatRoot.GetComponentsInChildren<TextValueLoader>();
				bool flag24 = componentsInChildren == null;
				enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
				num3 = num2;
				num = num4;
				text_MissionName = StatRoot;
				if (flag24)
				{
					break;
				}
				object obj5 = componentsInChildren + 32;
				MedalTier medalTier2 = MedalTier.Unearned;
				MedalTier medalTier3 = MedalTier.Unearned;
				text_MissionName = StatRoot;
				while (true)
				{
					Component component2;
					if ((int)medalTier3 < componentsInChildren.Length)
					{
						bool flag25 = (int)medalTier2 >= componentsInChildren.Length;
						enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
						num3 = num2;
						nint num5 = num4;
						if (!flag25)
						{
							bool flag26 = obj5 == null;
							enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
							num3 = num2;
							num = num4;
							text_MissionName = (UnityEngine.Object)obj5;
							if (flag26)
							{
								break;
							}
							((TextValueLoader)obj5).UpdateUI();
							medalTier2++;
							obj5 += 8;
							medalTier3 = medalTier2;
							text_MissionName = (UnityEngine.Object)obj5;
							continue;
						}
						component2 = (Component)text_MissionName;
						throw new IndexOutOfRangeException();
					}
					ReplayManager instance = ReplayManager.Instance;
					IReadOnlyCollection<byte[]> imageBytes;
					bool flag33;
					if ((object)ReplayManager.Instance != null)
					{
						bool flag27 = instance.frames == null;
						imageBytes = instance.frames;
						if (!flag27)
						{
							int count = ((IReadOnlyCollection<byte[]>)instance.frames).Count;
							int num6 = count ^ count;
							int num7 = count & num6;
							bool flag28 = num7 < 0;
							bool flag29 = count < 0;
							bool flag30 = count == 0;
							bool flag31 = flag29 == flag28;
							bool flag32 = !flag30;
							flag33 = flag32 & flag31;
							num2 = (nint)typeof(IReadOnlyCollection<byte[]>);
							imageBytes = instance.frames;
							goto IL_08ec;
						}
					}
					else
					{
						imageBytes = null;
					}
					flag33 = false;
					goto IL_08ec;
					IL_08ec:
					if (GifRoot != null)
					{
						bool flag34 = (object)GifRoot == null;
						enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
						num3 = num2;
						num = unchecked((nint)null);
						text_MissionName = GifRoot;
						if (flag34)
						{
							break;
						}
						GameObject gameObject3 = GifRoot.gameObject;
						bool flag35 = (object)gameObject3 == null;
						enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
						num3 = num2;
						num = unchecked((nint)null);
						text_MissionName = GifRoot;
						if (flag35)
						{
							break;
						}
						gameObject3.SetActive(flag33);
					}
					bool flag36 = GifImageCycler != null;
					bool flag37 = !flag36;
					num = unchecked((nint)null);
					if (!flag37)
					{
						bool flag38 = (object)GifImageCycler == null;
						enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
						num3 = num2;
						num = unchecked((nint)null);
						text_MissionName = GifImageCycler;
						if (flag38)
						{
							break;
						}
						if (!flag33)
						{
							GifImageCycler.OnDestroy();
							num = unchecked((nint)null);
						}
						else
						{
							GifImageCycler.LoadAsync((IReadOnlyList<byte[]>)imageBytes);
							num = unchecked((nint)null);
						}
					}
					bool flag39 = (object)PunchcardRoot == null;
					enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
					num3 = num2;
					text_MissionName = PunchcardRoot;
					if (flag39)
					{
						break;
					}
					IEnumerator enumerator3 = PunchcardRoot.GetEnumerator();
					object obj6 = (object)(&component3);
					object obj7 = (object)(&medalTier4);
					component2 = PunchcardRoot;
					while (true)
					{
						object obj12;
						if ((object)component3 != null)
						{
							nint num8 = (nint)component3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v88 @ r10_v11 (Il2CppClass<UnityEngine.Component>)+12E]");
							if ((nint)0 >= (nint)0)
							{
								goto IL_0b48;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v88 @ r10_v11 (Il2CppClass<UnityEngine.Component>)+B0]");
							num = 0;
							MedalTier medalTier5 = MedalTier.Unearned;
							while (true)
							{
								object obj8 = (int)medalTier5 + (int)medalTier5;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v449 @ r8_v10 (Il2CppMethodInfo)+v1499 @ rax_v76*8]");
								if (0 == (nint)typeof(IEnumerator))
								{
									break;
								}
								medalTier5++;
								MedalTier num9 = medalTier5;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v88 @ r10_v11 (Il2CppClass<UnityEngine.Component>)+12E]");
								if ((nint)num9 < (nint)0)
								{
									continue;
								}
								goto IL_0b48;
							}
							object obj9 = (int)medalTier5 + (int)medalTier5;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v449 @ r8_v10 (Il2CppMethodInfo)+8+v1556 @ rcx_v62*8]");
							object obj10 = (nint)0 << 4;
							object obj11 = obj10 + 312;
							obj12 = obj11 + num8;
							goto IL_104b;
						}
						throw new NullReferenceException();
						IL_0b48:
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
						obj12 = obj13;
						num = unchecked((nint)null);
						goto IL_104b;
						IL_104b:
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1561 @ rdx_v24] (should have been resolved before IL gen)");
						if (obj14 == null)
						{
							break;
						}
						bool flag40 = (object)component3 == null;
						component2 = component3;
						object obj21;
						object obj15;
						if (!flag40)
						{
							nint num10 = (nint)component3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v829 @ r10_v12 (Il2CppClass<UnityEngine.Component>)+12E]");
							if ((nint)0 >= (nint)0)
							{
								goto IL_0c09;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v829 @ r10_v12 (Il2CppClass<UnityEngine.Component>)+B0]");
							obj15 = 0;
							MedalTier medalTier6 = MedalTier.Unearned;
							while (true)
							{
								object obj16 = (int)medalTier6 + (int)medalTier6;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1723 @ r8_v28+v1644 @ rax_v71*8]");
								if (0 == (nint)typeof(IEnumerator))
								{
									break;
								}
								medalTier6++;
								MedalTier num11 = medalTier6;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v829 @ r10_v12 (Il2CppClass<UnityEngine.Component>)+12E]");
								if ((nint)num11 < (nint)0)
								{
									continue;
								}
								goto IL_0c09;
							}
							object obj17 = (int)medalTier6 + (int)medalTier6;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1723 @ r8_v28+8+v1715 @ rcx_v54*8]");
							object obj18 = (nint)0 + (nint)1;
							object obj19 = obj18 << 4;
							object obj20 = obj19 + 312;
							obj21 = obj20 + num10;
							goto IL_1072;
						}
						throw new NullReferenceException();
						IL_0c09:
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
						obj21 = obj22;
						obj15 = 1;
						goto IL_1072;
						IL_1072:
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1722 @ rdx_v38] (should have been resolved before IL gen)");
						nint num12 = (nint)typeof(Transform);
						bool flag41 = (object)component4 == null;
						component2 = component4;
						if (!flag41)
						{
							num3 = (nint)component4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v845 @ r8_v29 (Il2CppClass<UnityEngine.Transform>)+130]");
							object obj23 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v423 @ r9_v17 (Il2CppMethodInfo)+130]");
							nint num13 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v845 @ r8_v29 (Il2CppClass<UnityEngine.Transform>)+130]");
							bool flag42 = num13 < 0;
							enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
							nint num5 = (nint)typeof(Transform);
							component2 = component4;
							if (!flag42)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v423 @ r9_v17 (Il2CppMethodInfo)+C8]");
								object obj24 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v850 @ rax_v62+FFFFFFF8+v849 @ rax_v61*8]");
								bool flag43 = 0 != (nint)typeof(Transform);
								enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
								num5 = (nint)typeof(Transform);
								component2 = component4;
								if (!flag43)
								{
									GameObject gameObject4 = component4.gameObject;
									UnityEngine.Object.Destroy(gameObject4);
									component2 = (Component)(object)gameObject4;
									continue;
								}
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
						}
						throw new NullReferenceException();
					}
					text_MissionName = (UnityEngine.Object)obj6;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					obj7 = (nint)intPtr;
					if (intPtr != (IntPtr)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						num = intPtr;
						text_MissionName = null;
					}
					MissionGraph missionGraph6 = mission;
					bool flag44 = (object)mission == null;
					enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
					num3 = (nint)typeof(IEnumerator);
					if (flag44)
					{
						break;
					}
					if (missionGraph6.UnlockedPunchcards == null)
					{
						return;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					num = 0;
					while (enumerator4.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						UIPunchcard uIPunchcard = UnityEngine.Object.Instantiate(Prefab_Punchcard, PunchcardRoot);
						if ((object)uIPunchcard != null)
						{
							uIPunchcard.Initialize(def);
							num = unchecked((nint)null);
							continue;
						}
						throw new NullReferenceException();
					}
					enumerator4.Dispose();
					bool flag45 = (object)Root_PressKey == null;
					enumerator = enumerator2;
					num3 = (nint)typeof(IEnumerator);
					text_MissionName = Root_PressKey;
					if (flag45)
					{
						break;
					}
					Root_PressKey.SetActive(value: true);
					return;
				}
				break;
				IL_0611:
				obj = (UnityEngine.Object)(obj + 1);
				medals = Medals;
				bool flag46 = Medals == null;
				enumerator = (List<PunchcardDefinitionV2>.Enumerator)0;
				text_MissionName = obj;
				if (flag46)
				{
					break;
				}
				num2 = num3;
				num4 = num;
				obj2 = obj;
			}
		}
		goto IL_0eaf;
	}

	public EndOfMissionUIController()
	{
		List<MissionCardMedalSlotUI> medals = new List<MissionCardMedalSlotUI>();
		Medals = medals;
		base._002Ector();
	}

	private bool _003Cget_MultiKillShots_003Eb__101_0(MedalTrackedValues.Data_ShellFired shell)
	{
		//IL_00df: Expected I4, but got O
		//IL_0078: Expected O, but got I4
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected I4, but got Unknown
		_003C_003Ec__DisplayClass101_0 CS_0024_003C_003E8__locals4 = new _003C_003Ec__DisplayClass101_0();
		if (CS_0024_003C_003E8__locals4 != null)
		{
			CS_0024_003C_003E8__locals4.shell = shell;
			MedalTrackedValues medalTrackedValues = tracker;
			if (tracker != null)
			{
				Func<MedalTrackedValues.Data_KilledEntity, bool> predicate = delegate(MedalTrackedValues.Data_KilledEntity k)
				{
					if (k != null)
					{
						MedalTrackedValues.Data_ShellFired shell2 = CS_0024_003C_003E8__locals4.shell;
						if (CS_0024_003C_003E8__locals4.shell != null)
						{
							bool flag3 = k.ShellInstanceId == shell2.ShellInstanceId;
							if (!flag3)
							{
								return flag3;
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
				int num = Enumerable.Count(medalTrackedValues.Data_KilledEntities, predicate);
				object obj = num - 2;
				int num2 = num ^ 2;
				int num3 = num ^ obj;
				int num4 = num2 & num3;
				bool flag = num4 < 0;
				bool flag2 = (nint)obj < 0;
				return flag2 == flag;
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private bool _003Cget_TripleKillShots_003Eb__103_0(MedalTrackedValues.Data_ShellFired shell)
	{
		//IL_00df: Expected I4, but got O
		//IL_0078: Expected O, but got I4
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected I4, but got Unknown
		_003C_003Ec__DisplayClass103_0 CS_0024_003C_003E8__locals4 = new _003C_003Ec__DisplayClass103_0();
		if (CS_0024_003C_003E8__locals4 != null)
		{
			CS_0024_003C_003E8__locals4.shell = shell;
			MedalTrackedValues medalTrackedValues = tracker;
			if (tracker != null)
			{
				Func<MedalTrackedValues.Data_KilledEntity, bool> predicate = delegate(MedalTrackedValues.Data_KilledEntity k)
				{
					if (k != null)
					{
						MedalTrackedValues.Data_ShellFired shell2 = CS_0024_003C_003E8__locals4.shell;
						if (CS_0024_003C_003E8__locals4.shell != null)
						{
							bool flag3 = k.ShellInstanceId == shell2.ShellInstanceId;
							if (!flag3)
							{
								return flag3;
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
				int num = Enumerable.Count(medalTrackedValues.Data_KilledEntities, predicate);
				object obj = num - 3;
				int num2 = num ^ 3;
				int num3 = num ^ obj;
				int num4 = num2 & num3;
				bool flag = num4 < 0;
				bool flag2 = (nint)obj < 0;
				return flag2 == flag;
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}
}
