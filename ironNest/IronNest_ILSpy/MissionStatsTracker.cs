using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class MissionStatsTracker : MonoBehaviour
{
	[Serializable]
	public class Stats
	{
		public int shotsFired;

		public int targetsDestroyed;

		public int hitsOnTargets;

		public int missedShots;

		public float missionTime;

		public float accuracy;

		public int directHits;

		public int hitStreak;

		public int maxHitStreak;
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<MapEntity, bool> _003C_003E9__41_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal bool _003CEndMission_003Eb__41_0(MapEntity entity)
		{
			//IL_007d: Expected I4, but got O
			//IL_005c: Expected O, but got I4
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Expected I4, but got Unknown
			if (entity != null)
			{
				if (entity.IsAlive)
				{
					return false;
				}
				object obj = (int)entity.Role >> 5;
				return (byte)(obj & 1) != 0;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public static MissionStatsTracker Instance;

	public Stats mission;

	public Stats campaign;

	private int requisitionPoints;

	private ProtectedInt reqPoints;

	private bool requisitionPointsTampered;

	public GameObject summaryDisplay;

	public OdometerDisplay shotsFiredOdometer_mission;

	public OdometerDisplay targetsDestroyedOdometer_mission;

	public OdometerDisplay hitsOnTargetsOdometer_mission;

	public OdometerDisplay missedShotsOdometer_mission;

	public OdometerDisplay missionTimeOdometer_mission;

	public OdometerDisplay accuracyOdometer_mission;

	public OdometerDisplay directHitsOdometer_mission;

	public OdometerDisplay hitStreakOdometer_mission;

	public OdometerDisplay maxHitStreakOdometer_mission;

	public OdometerDisplay requisitionPointsOdometer_mission;

	public OdometerDisplay shotsFiredOdometer_campaign;

	public OdometerDisplay targetsDestroyedOdometer_campaign;

	public OdometerDisplay hitsOnTargetsOdometer_campaign;

	public OdometerDisplay missedShotsOdometer_campaign;

	public OdometerDisplay missionTimeOdometer_campaign;

	public OdometerDisplay accuracyOdometer_campaign;

	public OdometerDisplay directHitsOdometer_campaign;

	public OdometerDisplay hitStreakOdometer_campaign;

	public OdometerDisplay maxHitStreakOdometer_campaign;

	public OdometerDisplay requisitionPointsOdometer_campaign;

	public float directHitRadius;

	public float defaultImpactRadius;

	private bool timerRunning;

	private float timerValue;

	private bool missionEnded;

	public int ShotsFired_Mission
	{
		get
		{
			//IL_0041: Expected I4, but got O
			Stats stats = mission;
			if (mission != null)
			{
				return stats.shotsFired;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public int TargetsDestroyed_Mission
	{
		get
		{
			//IL_0041: Expected I4, but got O
			Stats stats = mission;
			if (mission != null)
			{
				return stats.targetsDestroyed;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public int HitsOnTargets_Mission
	{
		get
		{
			//IL_0041: Expected I4, but got O
			Stats stats = mission;
			if (mission != null)
			{
				return stats.hitsOnTargets;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public int MissedShots_Mission
	{
		get
		{
			//IL_0041: Expected I4, but got O
			Stats stats = mission;
			if (mission != null)
			{
				return stats.missedShots;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public float MissionTime_Mission
	{
		get
		{
			Stats stats = mission;
			return stats.missionTime;
		}
	}

	public float Accuracy_Mission
	{
		get
		{
			Stats stats = mission;
			return stats.accuracy;
		}
	}

	public int DirectHits_Mission
	{
		get
		{
			//IL_0041: Expected I4, but got O
			Stats stats = mission;
			if (mission != null)
			{
				return stats.directHits;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public int HitStreak_Mission
	{
		get
		{
			//IL_0041: Expected I4, but got O
			Stats stats = mission;
			if (mission != null)
			{
				return stats.hitStreak;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public int MaxHitStreak_Mission
	{
		get
		{
			//IL_0041: Expected I4, but got O
			Stats stats = mission;
			if (mission != null)
			{
				return stats.maxHitStreak;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public int ShotsFired_Campaign
	{
		get
		{
			//IL_0041: Expected I4, but got O
			Stats stats = campaign;
			if (campaign != null)
			{
				return stats.shotsFired;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public int TargetsDestroyed_Campaign
	{
		get
		{
			//IL_0041: Expected I4, but got O
			Stats stats = campaign;
			if (campaign != null)
			{
				return stats.targetsDestroyed;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public int HitsOnTargets_Campaign
	{
		get
		{
			//IL_0041: Expected I4, but got O
			Stats stats = campaign;
			if (campaign != null)
			{
				return stats.hitsOnTargets;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public int MissedShots_Campaign
	{
		get
		{
			//IL_0041: Expected I4, but got O
			Stats stats = campaign;
			if (campaign != null)
			{
				return stats.missedShots;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public float MissionTime_Campaign
	{
		get
		{
			Stats stats = campaign;
			return stats.missionTime;
		}
	}

	public float Accuracy_Campaign
	{
		get
		{
			Stats stats = campaign;
			return stats.accuracy;
		}
	}

	public int DirectHits_Campaign
	{
		get
		{
			//IL_0041: Expected I4, but got O
			Stats stats = campaign;
			if (campaign != null)
			{
				return stats.directHits;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public int MaxHitStreak_Campaign
	{
		get
		{
			//IL_0041: Expected I4, but got O
			Stats stats = campaign;
			if (campaign != null)
			{
				return stats.maxHitStreak;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public unsafe int RequisitionPoints
	{
		get
		{
			//IL_0009: Expected O, but got Ref
			object obj = default(object);
			return (ProtectedInt)(&obj);
		}
	}

	public bool RQT => requisitionPointsTampered;

	private void Awake()
	{
		UpdateMissionOdometers();
		UpdateCampaignOdometers();
	}

	private void OnEnable()
	{
		Instance = this;
		Action<Vector2, float> value = OnImpact;
		ImpactTracker.OnImpact += value;
	}

	private void OnDisable()
	{
		Instance = null;
		Action<Vector2, float> value = OnImpact;
		ImpactTracker.OnImpact -= value;
	}

	private void Update()
	{
		if (timerRunning)
		{
			float deltaTime = Time.deltaTime;
			Stats stats = mission;
			stats.missionTime = (timerValue = deltaTime + timerValue);
			UpdateMissionOdometers();
		}
	}

	public unsafe void OnImpact(Vector2 ImpactLocalPosition, float ImpactRadius)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_0064: Invalid comparison between F4 and I4
		//IL_00df: Expected O, but got I4
		//IL_0105: Expected O, but got Ref
		//IL_012f: Expected O, but got Ref
		//IL_041e: Expected I4, but got O
		//IL_016b: Expected O, but got Ref
		//IL_018c: Expected O, but got I4
		//IL_01ce: Invalid comparison between F4 and O
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Expected O, but got Unknown
		//IL_04ed: Invalid comparison between F4 and O
		//IL_0505: Expected O, but got F4
		//IL_0510: Expected O, but got I4
		//IL_0339: Expected O, but got F4
		//IL_0343: Expected O, but got F4
		//IL_034e: Expected O, but got I4
		if (!timerRunning)
		{
			timerRunning = true;
			timerValue = 0f;
		}
		Dictionary<string, EntityLocation> dictionary = (Dictionary<string, EntityLocation>)(object)mission;
		if (mission != null)
		{
			int[] buckets = (int[])(dictionary._buckets + 1);
			dictionary._buckets = buckets;
			bool flag = ImpactRadius > 0f;
			float num = ImpactRadius;
			if (!flag)
			{
				num = defaultImpactRadius;
			}
			dictionary = ImpactTracker.EntityLocations;
			if (ImpactTracker.EntityLocations != null)
			{
				Dictionary<string, EntityLocation>.ValueCollection values = ImpactTracker.EntityLocations.Values;
				if (values != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D9820");
					Dictionary<string, EntityLocation>.ValueCollection.Enumerator enumerator2 = default(Dictionary<string, EntityLocation>.ValueCollection.Enumerator);
					Dictionary<string, EntityLocation>.ValueCollection.Enumerator enumerator = enumerator2;
					Dictionary<string, EntityLocation>.ValueCollection.Enumerator enumerator4 = default(Dictionary<string, EntityLocation>.ValueCollection.Enumerator);
					Dictionary<string, EntityLocation>.ValueCollection.Enumerator enumerator3 = enumerator4;
					object obj = 0;
					Dictionary<string, EntityLocation>.ValueCollection.Enumerator enumerator5 = default(Dictionary<string, EntityLocation>.ValueCollection.Enumerator);
					EntityLocation entityLocation = default(EntityLocation);
					object obj2 = default(object);
					object obj3 = default(object);
					while (enumerator5.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						bool flag2 = (object)entityLocation == null;
						dictionary = (Dictionary<string, EntityLocation>)(&enumerator5);
						if (!flag2)
						{
							bool flag3 = entityLocation.Entity == null;
							dictionary = (Dictionary<string, EntityLocation>)(&enumerator5);
							if (flag3)
							{
								throw new NullReferenceException();
							}
							if (FlagExtensions.Has((MapEntityStates)(int)(&obj2), (MapEntityStates)(int)(&obj3)))
							{
								continue;
							}
							MapEntity entity = entityLocation.Entity;
							bool flag4 = entityLocation.Entity == null;
							dictionary = (Dictionary<string, EntityLocation>)(&obj2);
							if (!flag4)
							{
								object obj4 = entity.Role & EntityRoles.Target;
								if (obj4 == null)
								{
									continue;
								}
								Vector2 localPosition = entityLocation.LocalPosition;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407E10");
								bool flag5 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num) < System.Runtime.CompilerServices.Unsafe.As<Dictionary<string, EntityLocation>.ValueCollection.Enumerator, UIntPtr>(ref enumerator3);
								enumerator = enumerator3;
								if (flag5)
								{
									continue;
								}
								dictionary = (Dictionary<string, EntityLocation>)(object)mission;
								if (mission != null)
								{
									Dictionary<_00210, _00211>.Entry[] entries = (Dictionary<_00210, _00211>.Entry[])(dictionary._entries + 1);
									dictionary._entries = (Dictionary<string, EntityLocation>.Entry[])(object)entries;
									dictionary = (Dictionary<string, EntityLocation>)(object)mission;
									if (mission != null)
									{
										int version = dictionary._version + 1;
										dictionary._version = version;
										Stats stats = mission;
										if (mission != null)
										{
											if (stats.hitStreak > stats.maxHitStreak)
											{
												stats.maxHitStreak = stats.hitStreak;
											}
											float num2 = directHitRadius;
											bool flag6 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2) < System.Runtime.CompilerServices.Unsafe.As<Dictionary<string, EntityLocation>.ValueCollection.Enumerator, UIntPtr>(ref enumerator3);
											enumerator = enumerator3;
											enumerator3 = (Dictionary<string, EntityLocation>.ValueCollection.Enumerator)directHitRadius;
											obj = 1;
											if (!flag6)
											{
												dictionary = (Dictionary<string, EntityLocation>)(object)mission;
												if (mission == null)
												{
													throw new NullReferenceException();
												}
												int freeCount = dictionary._freeCount + 1;
												dictionary._freeCount = freeCount;
												enumerator = (Dictionary<string, EntityLocation>.ValueCollection.Enumerator)directHitRadius;
												enumerator3 = (Dictionary<string, EntityLocation>.ValueCollection.Enumerator)directHitRadius;
												obj = 1;
											}
											continue;
										}
										throw new NullReferenceException();
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					enumerator5.Dispose();
					if (obj != null)
					{
						goto IL_05ad;
					}
					dictionary = (Dictionary<string, EntityLocation>)(object)mission;
					if (mission != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v586 @ rcx_v5 (System.Collections.Generic.Dictionary`2<System.String, EntityLocation>)+1C]");
						_ = (nint)0 + (nint)1;
						Stats stats2 = mission;
						if (mission != null)
						{
							stats2.hitStreak = 0;
							goto IL_05ad;
						}
					}
				}
			}
		}
		goto IL_0423;
		IL_05ad:
		dictionary = (Dictionary<string, EntityLocation>)(object)mission;
		if (mission != null)
		{
			bool flag7 = (nint)dictionary._buckets <= 0;
			int freeList = 0;
			if (!flag7)
			{
				freeList = (object)dictionary._entries / (object)dictionary._buckets;
			}
			dictionary._freeList = freeList;
			UpdateMissionOdometers();
			return;
		}
		goto IL_0423;
		IL_0423:
		throw new NullReferenceException();
	}

	public unsafe void AddRequisitionPoints(int amount, string source = null)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_0136: Expected O, but got I4
		ProtectedInt protectedInt = (ProtectedInt)(this + 52);
		if (!((ProtectedInt*)protectedInt)->CheckTampered())
		{
			ProtectedInt protectedInt2 = (ProtectedInt)(this + 52);
			int value = ((ProtectedInt*)protectedInt2)->Value;
			if (requisitionPoints == value)
			{
				goto IL_00c4;
			}
		}
		requisitionPointsTampered = true;
		goto IL_00c4;
		IL_00c4:
		bool flag = amount < 0;
		if (amount != 0)
		{
			ProtectedInt protectedInt3 = (ProtectedInt)(this + 52);
			int value2 = ((ProtectedInt*)protectedInt3)->Value;
			int num = value2 + amount;
			int num2 = 0;
			if (!flag)
			{
				num2 = num;
			}
			if (num2 > 2147483647)
			{
				num2 = 2147483647;
			}
			requisitionPoints = num2;
			reqPoints = (ProtectedInt)((ProtectedInt)num2).encryptedValue;
			UpdateMissionOdometers();
			UpdateCampaignOdometers();
		}
	}

	public unsafe void SetRequisitionPoints(int amount, bool inital = true)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Expected O, but got Unknown
		//IL_00ef: Expected O, but got I4
		if (inital)
		{
			requisitionPointsTampered = false;
		}
		else
		{
			ProtectedInt protectedInt = (ProtectedInt)(this + 52);
			if (!((ProtectedInt*)protectedInt)->CheckTampered())
			{
				ProtectedInt protectedInt2 = (ProtectedInt)(this + 52);
				int value = ((ProtectedInt*)protectedInt2)->Value;
				if (requisitionPoints == value)
				{
					goto IL_00a3;
				}
			}
			requisitionPointsTampered = true;
		}
		goto IL_00a3;
		IL_00a3:
		bool flag = amount < 0;
		int num = 0;
		if (!flag)
		{
			num = amount;
		}
		requisitionPoints = num;
		reqPoints = (ProtectedInt)((ProtectedInt)num).encryptedValue;
		UpdateMissionOdometers();
		UpdateCampaignOdometers();
	}

	public unsafe bool SpendPoints(int amount)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_00f9: Expected O, but got I4
		ProtectedInt protectedInt = (ProtectedInt)(this + 52);
		if (!((ProtectedInt*)protectedInt)->CheckTampered())
		{
			ProtectedInt protectedInt2 = (ProtectedInt)(this + 52);
			int value = ((ProtectedInt*)protectedInt2)->Value;
			if (requisitionPoints == value)
			{
				goto IL_0110;
			}
		}
		requisitionPointsTampered = true;
		goto IL_0110;
		IL_0110:
		if (amount > 0)
		{
			ProtectedInt protectedInt3 = (ProtectedInt)(this + 52);
			int value2 = ((ProtectedInt*)protectedInt3)->Value;
			if (value2 < amount)
			{
				return false;
			}
			ProtectedInt protectedInt4 = (ProtectedInt)(this + 52);
			int value3 = ((ProtectedInt*)protectedInt4)->Value;
			reqPoints = (ProtectedInt)((ProtectedInt)(requisitionPoints = value3 - amount)).encryptedValue;
			UpdateMissionOdometers();
			UpdateCampaignOdometers();
		}
		return true;
	}

	public unsafe void EndMission(bool applyBaseFormula = true)
	{
		//IL_02fc: Expected F4, but got I4
		//IL_0155: Expected O, but got I4
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Expected O, but got Unknown
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Expected O, but got Unknown
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Expected I4, but got Unknown
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Expected O, but got Unknown
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Expected O, but got Unknown
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Expected O, but got Unknown
		//IL_0218: Expected O, but got I4
		//IL_0411: Expected O, but got I4
		if (missionEnded)
		{
			return;
		}
		Stats stats = mission;
		missionEnded = true;
		timerRunning = false;
		stats.missionTime = timerValue;
		Stats stats2 = mission;
		int missedShots = stats2.shotsFired - stats2.hitsOnTargets;
		stats2.missedShots = missedShots;
		Stats stats3 = mission;
		int num = ((stats3.shotsFired > 0) ? (stats3.hitsOnTargets / stats3.shotsFired) : 0);
		stats3.accuracy = num;
		Stats stats4 = mission;
		FireMission fireMission = FireMission._003CInstance_003Ek__BackingField;
		Dictionary<string, MapEntity>.ValueCollection values = fireMission.Entities.Values;
		Func<MapEntity, bool> predicate = _003C_003Ec._003C_003E9__41_0;
		if (_003C_003Ec._003C_003E9__41_0 == null)
		{
			predicate = (_003C_003Ec._003C_003E9__41_0 = delegate(MapEntity entity)
			{
				//IL_007d: Expected I4, but got O
				//IL_005c: Expected O, but got I4
				//IL_0065: Unknown result type (might be due to invalid IL or missing references)
				//IL_006a: Expected I4, but got Unknown
				if (entity == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				if (entity.IsAlive)
				{
					return false;
				}
				object obj6 = (int)entity.Role >> 5;
				return (byte)(obj6 & 1) != 0;
			});
		}
		int targetsDestroyed = Enumerable.Count(values, predicate);
		stats4.targetsDestroyed = targetsDestroyed;
		int num3;
		bool flag5;
		if (applyBaseFormula)
		{
			Stats stats5 = mission;
			bool flag = (nint)mission < 0;
			object obj = stats5.targetsDestroyed * 4;
			object obj2 = stats5.targetsDestroyed + obj;
			object obj3 = obj2 + obj2;
			object obj4 = obj3 - stats5.missedShots;
			int num2 = stats5.directHits + obj4;
			num3 = 0;
			if (!flag)
			{
				num3 = num2;
			}
			if (num3 > 0)
			{
				ProtectedInt protectedInt = (ProtectedInt)(this + 52);
				bool flag2 = ((ProtectedInt*)protectedInt)->CheckTampered();
				bool flag3 = (flag2 ? 1 : 0) < (false ? 1 : 0);
				if (!flag2)
				{
					ProtectedInt protectedInt2 = (ProtectedInt)(this + 52);
					int value = ((ProtectedInt*)protectedInt2)->Value;
					object obj5 = requisitionPoints - value;
					flag3 = (nint)obj5 < 0;
					bool flag4 = requisitionPoints == value;
					flag5 = flag3;
					if (flag4)
					{
						goto IL_0380;
					}
				}
				requisitionPointsTampered = true;
				flag5 = flag3;
				goto IL_0380;
			}
		}
		goto IL_0285;
		IL_0285:
		if ((bool)summaryDisplay)
		{
			summaryDisplay.SetActive(value: true);
		}
		UpdateMissionOdometers();
		return;
		IL_0380:
		ProtectedInt protectedInt3 = (ProtectedInt)(this + 52);
		int value2 = ((ProtectedInt*)protectedInt3)->Value;
		int num4 = value2 + num3;
		int num5 = 0;
		if (!flag5)
		{
			num5 = num4;
		}
		if (num5 > 2147483647)
		{
			num5 = 2147483647;
		}
		requisitionPoints = num5;
		reqPoints = (ProtectedInt)((ProtectedInt)num5).encryptedValue;
		UpdateMissionOdometers();
		UpdateCampaignOdometers();
		goto IL_0285;
	}

	public void CommitMissionStatsToCampaign()
	{
		//IL_0298: Expected F4, but got I4
		Stats stats = campaign;
		Stats stats2 = mission;
		int shotsFired = stats.shotsFired + stats2.shotsFired;
		stats.shotsFired = shotsFired;
		Stats stats3 = campaign;
		Stats stats4 = mission;
		int targetsDestroyed = stats3.targetsDestroyed + stats4.targetsDestroyed;
		stats3.targetsDestroyed = targetsDestroyed;
		Stats stats5 = campaign;
		Stats stats6 = mission;
		int hitsOnTargets = stats5.hitsOnTargets + stats6.hitsOnTargets;
		stats5.hitsOnTargets = hitsOnTargets;
		Stats stats7 = campaign;
		Stats stats8 = mission;
		int missedShots = stats7.missedShots + stats8.missedShots;
		stats7.missedShots = missedShots;
		Stats stats9 = campaign;
		Stats stats10 = mission;
		float missionTime = stats10.missionTime + stats9.missionTime;
		stats9.missionTime = missionTime;
		Stats stats11 = mission;
		int directHits = stats9.directHits + stats11.directHits;
		stats9.directHits = directHits;
		Stats stats12 = campaign;
		int num = ((stats12.shotsFired > 0) ? (stats12.hitsOnTargets / stats12.shotsFired) : 0);
		stats12.accuracy = num;
		Stats stats13 = mission;
		if (stats13.maxHitStreak > stats12.maxHitStreak)
		{
			Stats stats14 = campaign;
			stats14.maxHitStreak = stats13.maxHitStreak;
		}
		Stats stats15 = campaign;
		stats15.hitStreak = 0;
		Stats stats16 = new Stats();
		mission = stats16;
		timerRunning = false;
		timerValue = 0f;
		missionEnded = false;
		UpdateMissionOdometers();
		UpdateCampaignOdometers();
	}

	private int CalculateBaseMissionRequisitionPoints()
	{
		//IL_00c2: Expected I4, but got O
		//IL_004d: Expected O, but got I4
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected I4, but got Unknown
		Stats stats = mission;
		bool flag = (nint)mission < 0;
		if (mission != null)
		{
			object obj = stats.targetsDestroyed * 4;
			object obj2 = stats.targetsDestroyed + obj;
			object obj3 = obj2 + obj2;
			object obj4 = obj3 - stats.missedShots;
			int num = stats.directHits + obj4;
			int result = 0;
			if (!flag)
			{
				result = num;
			}
			return result;
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public unsafe void UpdateMissionOdometers()
	{
		//IL_005d: Expected F4, but got I4
		//IL_00ba: Expected F4, but got I4
		//IL_0117: Expected F4, but got I4
		//IL_0174: Expected F4, but got I4
		//IL_028b: Expected F4, but got I4
		//IL_02e8: Expected F4, but got I4
		//IL_0385: Expected O, but got Ref
		//IL_0345: Expected F4, but got I4
		//IL_039b: Expected F4, but got I4
		if ((bool)shotsFiredOdometer_mission)
		{
			Stats stats = mission;
			OdometerDisplay odometerDisplay = shotsFiredOdometer_mission;
			odometerDisplay.targetNumber = stats.shotsFired;
		}
		if ((bool)targetsDestroyedOdometer_mission)
		{
			Stats stats2 = mission;
			OdometerDisplay odometerDisplay2 = targetsDestroyedOdometer_mission;
			odometerDisplay2.targetNumber = stats2.targetsDestroyed;
		}
		if ((bool)hitsOnTargetsOdometer_mission)
		{
			Stats stats3 = mission;
			OdometerDisplay odometerDisplay3 = hitsOnTargetsOdometer_mission;
			odometerDisplay3.targetNumber = stats3.hitsOnTargets;
		}
		if ((bool)missedShotsOdometer_mission)
		{
			Stats stats4 = mission;
			OdometerDisplay odometerDisplay4 = missedShotsOdometer_mission;
			odometerDisplay4.targetNumber = stats4.missedShots;
		}
		if ((bool)missionTimeOdometer_mission)
		{
			Stats stats5 = mission;
			OdometerDisplay odometerDisplay5 = missionTimeOdometer_mission;
			odometerDisplay5.targetNumber = stats5.missionTime;
		}
		if ((bool)accuracyOdometer_mission)
		{
			Stats stats6 = mission;
			OdometerDisplay odometerDisplay6 = accuracyOdometer_mission;
			odometerDisplay6.targetNumber = stats6.accuracy;
		}
		if ((bool)directHitsOdometer_mission)
		{
			Stats stats7 = mission;
			OdometerDisplay odometerDisplay7 = directHitsOdometer_mission;
			odometerDisplay7.targetNumber = stats7.directHits;
		}
		if ((bool)hitStreakOdometer_mission)
		{
			Stats stats8 = mission;
			OdometerDisplay odometerDisplay8 = hitStreakOdometer_mission;
			odometerDisplay8.targetNumber = stats8.hitStreak;
		}
		if ((bool)maxHitStreakOdometer_mission)
		{
			Stats stats9 = mission;
			OdometerDisplay odometerDisplay9 = maxHitStreakOdometer_mission;
			odometerDisplay9.targetNumber = stats9.maxHitStreak;
		}
		if ((bool)requisitionPointsOdometer_mission)
		{
			OdometerDisplay odometerDisplay10 = requisitionPointsOdometer_mission;
			object obj = default(object);
			int num = (ProtectedInt)(&obj);
			odometerDisplay10.targetNumber = num;
		}
	}

	public unsafe void UpdateCampaignOdometers()
	{
		//IL_005d: Expected F4, but got I4
		//IL_00ba: Expected F4, but got I4
		//IL_0117: Expected F4, but got I4
		//IL_0174: Expected F4, but got I4
		//IL_028b: Expected F4, but got I4
		//IL_02e8: Expected F4, but got I4
		//IL_0385: Expected O, but got Ref
		//IL_0345: Expected F4, but got I4
		//IL_039b: Expected F4, but got I4
		if ((bool)shotsFiredOdometer_campaign)
		{
			Stats stats = campaign;
			OdometerDisplay odometerDisplay = shotsFiredOdometer_campaign;
			odometerDisplay.targetNumber = stats.shotsFired;
		}
		if ((bool)targetsDestroyedOdometer_campaign)
		{
			Stats stats2 = campaign;
			OdometerDisplay odometerDisplay2 = targetsDestroyedOdometer_campaign;
			odometerDisplay2.targetNumber = stats2.targetsDestroyed;
		}
		if ((bool)hitsOnTargetsOdometer_campaign)
		{
			Stats stats3 = campaign;
			OdometerDisplay odometerDisplay3 = hitsOnTargetsOdometer_campaign;
			odometerDisplay3.targetNumber = stats3.hitsOnTargets;
		}
		if ((bool)missedShotsOdometer_campaign)
		{
			Stats stats4 = campaign;
			OdometerDisplay odometerDisplay4 = missedShotsOdometer_campaign;
			odometerDisplay4.targetNumber = stats4.missedShots;
		}
		if ((bool)missionTimeOdometer_campaign)
		{
			Stats stats5 = campaign;
			OdometerDisplay odometerDisplay5 = missionTimeOdometer_campaign;
			odometerDisplay5.targetNumber = stats5.missionTime;
		}
		if ((bool)accuracyOdometer_campaign)
		{
			Stats stats6 = campaign;
			OdometerDisplay odometerDisplay6 = accuracyOdometer_campaign;
			odometerDisplay6.targetNumber = stats6.accuracy;
		}
		if ((bool)directHitsOdometer_campaign)
		{
			Stats stats7 = campaign;
			OdometerDisplay odometerDisplay7 = directHitsOdometer_campaign;
			odometerDisplay7.targetNumber = stats7.directHits;
		}
		if ((bool)hitStreakOdometer_campaign)
		{
			Stats stats8 = campaign;
			OdometerDisplay odometerDisplay8 = hitStreakOdometer_campaign;
			odometerDisplay8.targetNumber = stats8.hitStreak;
		}
		if ((bool)maxHitStreakOdometer_campaign)
		{
			Stats stats9 = campaign;
			OdometerDisplay odometerDisplay9 = maxHitStreakOdometer_campaign;
			odometerDisplay9.targetNumber = stats9.maxHitStreak;
		}
		if ((bool)requisitionPointsOdometer_campaign)
		{
			OdometerDisplay odometerDisplay10 = requisitionPointsOdometer_campaign;
			object obj = default(object);
			int num = (ProtectedInt)(&obj);
			odometerDisplay10.targetNumber = num;
		}
	}

	public MissionStatsTracker()
	{
		//IL_0056: Expected O, but got I4
		Stats stats = new Stats();
		mission = stats;
		campaign = new Stats();
		ProtectedInt protectedInt = 0;
		directHitRadius = 10f;
		defaultImpactRadius = 50f;
		reqPoints = (ProtectedInt)protectedInt.encryptedValue;
		base._002Ector();
	}
}
