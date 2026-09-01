using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using Heathen.SteamworksIntegration;
using Steamworks;
using UnityEngine;

public class AchievementsService : MonoBehaviour
{
	private Action<AchievementType> m_OnAchievementUnlocked;

	public static AchievementsService Instance;

	protected Callback<UserStatsReceived_t> userStatsReceivedCallback;

	protected Callback<UserStatsStored_t> userStatsStoredCallback;

	protected Callback<UserAchievementStored_t> userAchievementStoredCallback;

	private CGameID gameId;

	private bool storeStatsRequested;

	private bool loadStatsRequested;

	private bool isInitialized;

	private float updateStatsTimer;

	private Dictionary<AchievementType, bool> achievementsState;

	private Dictionary<UserStat, int> statsChangesCache;

	private Dictionary<UserStat, int> statsState;

	private const int RefreshRate = 180;

	private bool requestLocked;

	public event Action<AchievementType> OnAchievementUnlocked
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 32;
			Delegate obj2 = this.m_OnAchievementUnlocked;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 32;
			Delegate obj2 = this.m_OnAchievementUnlocked;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public unsafe void Initialize()
	{
		//IL_00a4: Expected I4, but got O
		//IL_00a4: Expected I4, but got O
		//IL_00ac: Expected O, but got Ref
		//IL_00ca: Expected I4, but got O
		//IL_0236: Expected I4, but got O
		//IL_0236: Expected I4, but got O
		//IL_01a5: Expected O, but got I4
		//IL_028b: Expected I4, but got O
		//IL_028b: Expected I4, but got O
		//IL_0293: Expected O, but got Ref
		//IL_014a: Expected O, but got I
		//IL_0153: Expected O, but got I4
		//IL_01e5: Expected O, but got I
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Expected O, but got Unknown
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Expected O, but got Unknown
		//IL_02a5: Expected I, but got O
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Expected O, but got Unknown
		//IL_0338: Expected O, but got I4
		//IL_02dd: Expected O, but got I
		//IL_02e6: Expected O, but got I4
		//IL_0479: Expected O, but got I
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Expected O, but got Unknown
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Expected O, but got Unknown
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Expected O, but got Unknown
		//IL_036a: Expected I, but got O
		//IL_03fd: Expected O, but got I4
		//IL_03a2: Expected O, but got I
		//IL_03ab: Expected O, but got I4
		//IL_04bc: Expected O, but got I
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Expected O, but got Unknown
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Expected O, but got Unknown
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Expected O, but got Unknown
		AppId_t appID = SteamUtils.GetAppID();
		CGameID cGameID = (gameId = new CGameID(appID));
		loadStatsRequested = true;
		Dictionary<AchievementType, bool> dictionary = new Dictionary<AchievementType, bool>();
		dictionary._002Ector();
		achievementsState = dictionary;
		Dictionary<UserStat, int> dictionary2 = new Dictionary<UserStat, int>();
		statsState = dictionary2;
		Dictionary<UserStat, int> dictionary3 = new Dictionary<UserStat, int>();
		statsChangesCache = dictionary3;
		Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(AchievementType));
		Array values = Enum.GetValues(typeFromHandle);
		IEnumerable<AchievementType> enumerable = Enumerable.Cast<AchievementType>(values);
		((Dictionary<AchievementType, bool>)null).set_Item((AchievementType)typeof(IEnumerable<AchievementType>), (byte)(int)enumerable != 0);
		bool flag = default(bool);
		object obj = (object)(&flag);
		Dictionary<AchievementType, bool> dictionary4 = null;
		object obj2 = default(object);
		object obj11;
		Dictionary<UserStat, int> dictionary5 = default(Dictionary<UserStat, int>);
		object obj12 = default(object);
		object obj13 = default(object);
		while (true)
		{
			object obj10;
			object obj3;
			if (flag)
			{
				((Dictionary<AchievementType, bool>)null).set_Item((AchievementType)typeof(IEnumerator), flag);
				if (obj2 != null)
				{
					bool flag2 = !flag;
					dictionary4 = null;
					if (!flag2)
					{
						bool value = ((bool*)(flag ? 1 : 0))->m_value;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v162 @ r10_v13 (System.Boolean)+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_018a;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v162 @ r10_v13 (System.Boolean)+B0]");
						obj3 = 0;
						object obj4 = 0;
						while (true)
						{
							object obj5 = obj4 + obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v177 @ r8_v29+v443 @ rax_v82*8]");
							if (0 == (nint)typeof(IEnumerator<AchievementType>))
							{
								break;
							}
							obj4++;
							object obj6 = obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v162 @ r10_v13 (System.Boolean)+12E]");
							if ((nint)obj6 < 0)
							{
								continue;
							}
							goto IL_018a;
						}
						object obj7 = obj4 + obj4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v177 @ r8_v29+8+v605 @ rcx_v71*8]");
						object obj8 = (nint)0 << 4;
						object obj9 = obj8 + 312;
						obj10 = obj9 + value;
						goto IL_05e8;
					}
					throw new NullReferenceException();
				}
				if (obj != null)
				{
					((Dictionary<AchievementType, bool>)null).set_Item((AchievementType)typeof(IDisposable), (byte)(int)obj != 0);
				}
				Type typeFromHandle2 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(UserStat));
				Array values2 = Enum.GetValues(typeFromHandle2);
				IEnumerable<UserStat> enumerable2 = Enumerable.Cast<UserStat>(values2);
				((Dictionary<AchievementType, bool>)null).set_Item((AchievementType)typeof(IEnumerable<UserStat>), (byte)(int)enumerable2 != 0);
				obj11 = (object)(&dictionary5);
				Dictionary<UserStat, int> dictionary6 = null;
				break;
			}
			throw new NullReferenceException();
			IL_018a:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj10 = obj12;
			obj3 = 0;
			goto IL_05e8;
			IL_05e8:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v610 @ rdx_v53] (should have been resolved before IL gen)");
			if (achievementsState != null)
			{
				achievementsState.set_Item((AchievementType)(int)(&obj13), (byte)(&cGameID) != 0);
				continue;
			}
			throw new NullReferenceException();
		}
		object obj22 = default(object);
		object obj23 = default(object);
		object obj32 = default(object);
		while (true)
		{
			object obj21;
			object obj14;
			if (dictionary5 != null)
			{
				nint num = (nint)dictionary5;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v488 @ r10_v11 (Il2CppClass<System.Collections.Generic.Dictionary`2<UserStat, System.Int32>>)+12E]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_031d;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v488 @ r10_v11 (Il2CppClass<System.Collections.Generic.Dictionary`2<UserStat, System.Int32>>)+B0]");
				obj14 = 0;
				object obj15 = 0;
				while (true)
				{
					object obj16 = obj15 + obj15;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v500 @ r8_v16+v766 @ rax_v70*8]");
					if (0 == (nint)typeof(IEnumerator))
					{
						break;
					}
					obj15++;
					object obj17 = obj15;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v488 @ r10_v11 (Il2CppClass<System.Collections.Generic.Dictionary`2<UserStat, System.Int32>>)+12E]");
					if ((nint)obj17 < 0)
					{
						continue;
					}
					goto IL_031d;
				}
				object obj18 = obj15 + obj15;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v500 @ r8_v16+8+v822 @ rcx_v60*8]");
				object obj19 = (nint)0 << 4;
				object obj20 = obj19 + 312;
				obj21 = obj20 + num;
				goto IL_06b3;
			}
			throw new NullReferenceException();
			IL_031d:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj21 = obj22;
			obj14 = 0;
			goto IL_06b3;
			IL_06b3:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v827 @ rdx_v29] (should have been resolved before IL gen)");
			object obj31;
			object obj24;
			Dictionary<UserStat, int> dictionary6;
			if (obj23 != null)
			{
				bool flag3 = dictionary5 == null;
				dictionary6 = dictionary5;
				if (!flag3)
				{
					nint num2 = (nint)dictionary5;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v331 @ r10_v12 (Il2CppClass<System.Collections.Generic.Dictionary`2<UserStat, System.Int32>>)+12E]");
					if ((nint)0 >= (nint)0)
					{
						goto IL_03e2;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v331 @ r10_v12 (Il2CppClass<System.Collections.Generic.Dictionary`2<UserStat, System.Int32>>)+B0]");
					obj24 = 0;
					object obj25 = 0;
					while (true)
					{
						object obj26 = obj25 + obj25;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v411 @ r8_v19+v874 @ rax_v65*8]");
						if (0 == (nint)typeof(IEnumerator<UserStat>))
						{
							break;
						}
						obj25++;
						object obj27 = obj25;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v331 @ r10_v12 (Il2CppClass<System.Collections.Generic.Dictionary`2<UserStat, System.Int32>>)+12E]");
						if ((nint)obj27 < 0)
						{
							continue;
						}
						goto IL_03e2;
					}
					object obj28 = obj25 + obj25;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v411 @ r8_v19+8+v933 @ rcx_v54*8]");
					object obj29 = (nint)0 << 4;
					object obj30 = obj29 + 312;
					obj31 = obj30 + num2;
					goto IL_06da;
				}
				throw new NullReferenceException();
			}
			if (obj11 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
			break;
			IL_03e2:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj31 = obj32;
			obj24 = 0;
			goto IL_06da;
			IL_06da:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v938 @ rdx_v35] (should have been resolved before IL gen)");
			bool flag4 = statsState == null;
			dictionary6 = statsState;
			if (!flag4)
			{
				statsState.set_Item((UserStat)(int)(&obj13), (int)(&cGameID));
				dictionary6 = statsChangesCache;
				if (statsChangesCache != null)
				{
					statsChangesCache.set_Item((UserStat)(int)(&obj13), (int)(&cGameID));
					continue;
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
		}
		updateStatsTimer = -180f;
		storeStatsRequested = false;
		Instance = this;
		isInitialized = true;
	}

	private void OnEnable()
	{
		Callback<UserStatsReceived_t>.DispatchDelegate func = OnUserStatsReceived;
		Callback<UserStatsReceived_t> callback = Callback<UserStatsReceived_t>.Create(func);
		userStatsReceivedCallback = callback;
		Callback<UserStatsStored_t>.DispatchDelegate func2 = OnUserStatsStored;
		Callback<UserStatsStored_t> callback2 = Callback<UserStatsStored_t>.Create(func2);
		userStatsStoredCallback = callback2;
		Callback<UserAchievementStored_t>.DispatchDelegate func3 = OnAchievementStored;
		Callback<UserAchievementStored_t> callback3 = Callback<UserAchievementStored_t>.Create(func3);
		userAchievementStoredCallback = callback3;
	}

	private void Update()
	{
		if (!isInitialized)
		{
			return;
		}
		if (loadStatsRequested)
		{
			UserData me = UserData.Me;
			if ((object)me != null)
			{
				SteamAPICall_t steamAPICall_t = SteamUserStats.RequestUserStats(me);
				loadStatsRequested = false;
			}
		}
		float fixedUnscaledDeltaTime = Time.fixedUnscaledDeltaTime;
		if (!((updateStatsTimer = fixedUnscaledDeltaTime + updateStatsTimer) < 180f))
		{
			storeStatsRequested = true;
			updateStatsTimer = 0f;
		}
		if (storeStatsRequested && !requestLocked)
		{
			StoreStats();
			storeStatsRequested = false;
			updateStatsTimer = 0f;
		}
	}

	public void RequestUpdate()
	{
		if (isInitialized)
		{
			storeStatsRequested = true;
		}
	}

	public void SendChanges()
	{
		if (isInitialized)
		{
			StoreStats();
		}
	}

	public void SendChangesForced()
	{
		if (isInitialized)
		{
			requestLocked = false;
			StoreStats();
		}
	}

	public unsafe void UpdateStat(UserStat stat, int deltaValue)
	{
		if (isInitialized)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
			object obj = default(object);
			UserStat userStat = default(UserStat);
			statsChangesCache.set_Item((UserStat)(int)(&obj), (int)(&userStat));
		}
	}

	public unsafe void SetStat(UserStat stat, int value)
	{
		if (isInitialized)
		{
			object obj = default(object);
			object obj2 = default(object);
			statsChangesCache.set_Item((UserStat)(int)(&obj), (int)(&obj2));
		}
	}

	public unsafe void TryUnlockAchievement(AchievementType achievement)
	{
		//IL_002a: Expected O, but got Ref
		if (isInitialized && !HasUnlockedAchievement(achievement))
		{
			object obj = default(object);
			string achievement2 = ((Enum)(&obj)).ToString();
			bool flag = SteamUserStats.SetAchievement(achievement2);
			storeStatsRequested = true;
			updateStatsTimer = 0f;
		}
	}

	public unsafe bool HasUnlockedAchievement(AchievementType achievement)
	{
		object obj = default(object);
		if (isInitialized && achievementsState != null && achievementsState.TryGetValue((AchievementType)(int)(&obj), out var value))
		{
			return value;
		}
		return false;
	}

	public int GetStat(UserStat stat)
	{
		//IL_007d: Expected I4, but got O
		//IL_0064: Expected I4, but got O
		if (isInitialized)
		{
			if (statsState != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
				if (statsChangesCache != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
					object obj = default(object);
					object obj2 = default(object);
					return (int)(obj + obj2);
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		return 0;
	}

	public unsafe void ResetAll()
	{
		//IL_007a: Expected I4, but got O
		//IL_007a: Expected I4, but got O
		//IL_0082: Expected O, but got Ref
		//IL_00a0: Expected I4, but got O
		//IL_024c: Expected I4, but got O
		//IL_024c: Expected I4, but got O
		//IL_0177: Expected O, but got I4
		//IL_0124: Expected O, but got I
		//IL_012d: Expected O, but got I4
		//IL_02c6: Expected I4, but got O
		//IL_02c6: Expected I4, but got O
		//IL_02ce: Expected O, but got Ref
		//IL_01fb: Expected O, but got I
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Expected O, but got Unknown
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Expected O, but got Unknown
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Expected O, but got Unknown
		//IL_02e0: Expected I, but got O
		//IL_036b: Expected O, but got I4
		//IL_0318: Expected O, but got I
		//IL_0321: Expected O, but got I4
		//IL_0470: Expected O, but got I
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Expected O, but got Unknown
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Expected O, but got Unknown
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Expected O, but got Unknown
		//IL_039d: Expected I, but got O
		//IL_0428: Expected O, but got I4
		//IL_03d5: Expected O, but got I
		//IL_03de: Expected O, but got I4
		//IL_04b3: Expected O, but got I
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Expected O, but got Unknown
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Expected O, but got Unknown
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Expected O, but got Unknown
		if (!isInitialized)
		{
			return;
		}
		Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(UserStat));
		Array values = Enum.GetValues(typeFromHandle);
		IEnumerable<UserStat> enumerable = Enumerable.Cast<UserStat>(values);
		bool flag = enumerable == null;
		IEnumerable enumerable2 = values;
		Dictionary<UserStat, int> dictionary;
		if (!flag)
		{
			((Dictionary<UserStat, int>)null).set_Item((UserStat)typeof(IEnumerable<UserStat>), (int)enumerable);
			int num = default(int);
			object obj = (object)(&num);
			dictionary = null;
			object obj2 = default(object);
			object obj11 = default(object);
			object obj12 = default(object);
			object obj13 = default(object);
			while (true)
			{
				object obj3;
				object obj10;
				if (num != 0)
				{
					((Dictionary<UserStat, int>)null).set_Item((UserStat)typeof(IEnumerator), num);
					if (obj2 == null)
					{
						break;
					}
					bool flag2 = num == 0;
					dictionary = null;
					if (!flag2)
					{
						int value = ((int*)num)->m_value;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v287 @ r10_v14 (System.Int32)+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_0164;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v287 @ r10_v14 (System.Int32)+B0]");
						obj3 = 0;
						object obj4 = 0;
						while (true)
						{
							object obj5 = obj4 + obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v371 @ r8_v28+v602 @ rax_v72*8]");
							if (0 == (nint)typeof(IEnumerator<UserStat>))
							{
								break;
							}
							obj4++;
							object obj6 = obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v287 @ r10_v14 (System.Int32)+12E]");
							if ((nint)obj6 < 0)
							{
								continue;
							}
							goto IL_0164;
						}
						object obj7 = obj4 + obj4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v371 @ r8_v28+8+v695 @ rcx_v61*8]");
						object obj8 = (nint)0 << 4;
						object obj9 = obj8 + 312;
						obj10 = obj9 + value;
						goto IL_05fa;
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
				IL_0164:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj3 = 0;
				obj10 = obj11;
				goto IL_05fa;
				IL_05fa:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v700 @ rdx_v47] (should have been resolved before IL gen)");
				if (statsState != null)
				{
					statsState.set_Item((UserStat)(int)(&obj12), (int)(&obj13));
					dictionary = statsChangesCache;
					if (statsChangesCache != null)
					{
						statsChangesCache.set_Item((UserStat)(int)(&obj12), (int)(&obj13));
						continue;
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			if (obj != null)
			{
				((Dictionary<UserStat, int>)null).set_Item((UserStat)typeof(IDisposable), (int)obj);
			}
			Type typeFromHandle2 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(AchievementType));
			Array values2 = Enum.GetValues(typeFromHandle2);
			IEnumerable<AchievementType> enumerable3 = Enumerable.Cast<AchievementType>(values2);
			bool flag3 = enumerable3 == null;
			enumerable2 = values2;
			if (!flag3)
			{
				((Dictionary<UserStat, int>)null).set_Item((UserStat)typeof(IEnumerable<AchievementType>), (int)enumerable3);
				Dictionary<AchievementType, bool> dictionary2 = default(Dictionary<AchievementType, bool>);
				object obj14 = (object)(&dictionary2);
				Dictionary<AchievementType, bool> dictionary3 = null;
				object obj23 = default(object);
				object obj24 = default(object);
				object obj27 = default(object);
				object obj28 = default(object);
				while (true)
				{
					object obj15;
					object obj22;
					if (dictionary2 != null)
					{
						nint num2 = (nint)dictionary2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ r10_v12 (Il2CppClass<System.Collections.Generic.Dictionary`2<AchievementType, System.Boolean>>)+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_0358;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ r10_v12 (Il2CppClass<System.Collections.Generic.Dictionary`2<AchievementType, System.Boolean>>)+B0]");
						obj15 = 0;
						object obj16 = 0;
						while (true)
						{
							object obj17 = obj16 + obj16;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v567 @ r8_v16+v769 @ rax_v59*8]");
							if (0 == (nint)typeof(IEnumerator))
							{
								break;
							}
							obj16++;
							object obj18 = obj16;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ r10_v12 (Il2CppClass<System.Collections.Generic.Dictionary`2<AchievementType, System.Boolean>>)+12E]");
							if ((nint)obj18 < 0)
							{
								continue;
							}
							goto IL_0358;
						}
						object obj19 = obj16 + obj16;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v567 @ r8_v16+8+v825 @ rcx_v49*8]");
						object obj20 = (nint)0 << 4;
						object obj21 = obj20 + 312;
						obj22 = obj21 + num2;
						goto IL_06be;
					}
					throw new NullReferenceException();
					IL_0358:
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
					obj15 = 0;
					obj22 = obj23;
					goto IL_06be;
					IL_06e5:
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v952 @ rdx_v30] (should have been resolved before IL gen)");
					if (achievementsState == null)
					{
						break;
					}
					achievementsState.set_Item((AchievementType)(int)(&obj13), (byte)(&obj24) != 0);
					continue;
					IL_0415:
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
					object obj25 = 0;
					object obj26 = obj27;
					goto IL_06e5;
					IL_06be:
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v830 @ rdx_v23] (should have been resolved before IL gen)");
					if (obj28 != null)
					{
						if (dictionary2 != null)
						{
							nint num3 = (nint)dictionary2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v496 @ r10_v13 (Il2CppClass<System.Collections.Generic.Dictionary`2<AchievementType, System.Boolean>>)+12E]");
							if ((nint)0 >= (nint)0)
							{
								goto IL_0415;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v496 @ r10_v13 (Il2CppClass<System.Collections.Generic.Dictionary`2<AchievementType, System.Boolean>>)+B0]");
							obj25 = 0;
							object obj29 = 0;
							while (true)
							{
								object obj30 = obj29 + obj29;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v505 @ r8_v19+v887 @ rax_v54*8]");
								if (0 == (nint)typeof(IEnumerator<AchievementType>))
								{
									break;
								}
								obj29++;
								object obj31 = obj29;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v496 @ r10_v13 (Il2CppClass<System.Collections.Generic.Dictionary`2<AchievementType, System.Boolean>>)+12E]");
								if ((nint)obj31 < 0)
								{
									continue;
								}
								goto IL_0415;
							}
							object obj32 = obj29 + obj29;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v505 @ r8_v19+8+v947 @ rcx_v43*8]");
							object obj33 = (nint)0 << 4;
							object obj34 = obj33 + 312;
							obj26 = obj34 + num3;
							goto IL_06e5;
						}
						throw new NullReferenceException();
					}
					if (obj14 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					}
					bool flag4 = SteamUserStats.ResetAllStats(bAchievementsToo: true);
					Debug.Log("[Steamworks.NET] Reset all stats and achievements!");
					return;
				}
				throw new NullReferenceException();
			}
		}
		dictionary = (Dictionary<UserStat, int>)enumerable2;
		throw new NullReferenceException();
	}

	private unsafe void StoreStats()
	{
		//IL_008d: Expected I4, but got O
		//IL_008d: Expected I4, but got O
		//IL_0095: Expected O, but got Ref
		//IL_00b3: Expected I4, but got O
		//IL_0300: Expected I4, but got O
		//IL_0300: Expected I4, but got O
		//IL_018c: Expected O, but got I4
		//IL_0137: Expected O, but got I
		//IL_0140: Expected F4, but got I4
		//IL_022c: Expected O, but got I
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Expected O, but got Unknown
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Expected O, but got Unknown
		//IL_03e2: Expected O, but got Ref
		//IL_0165: Invalid comparison between F4 and I
		//IL_01f9: Expected I4, but got O
		//IL_02c9: Expected F4, but got I4
		if (!isInitialized)
		{
			return;
		}
		updateStatsTimer = 0f;
		if (requestLocked)
		{
			return;
		}
		requestLocked = true;
		Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(UserStat));
		Array values = Enum.GetValues(typeFromHandle);
		IEnumerable<UserStat> enumerable = Enumerable.Cast<UserStat>(values);
		((Dictionary<UserStat, int>)null).set_Item((UserStat)typeof(IEnumerable<UserStat>), (int)enumerable);
		int num = default(int);
		object obj = (object)(&num);
		string text = null;
		object obj2 = default(object);
		object obj7 = default(object);
		object obj8 = default(object);
		object obj9 = default(object);
		float num6 = default(float);
		float num7 = default(float);
		IntPtr intPtr = default(IntPtr);
		float num8 = default(float);
		int num9 = default(int);
		while (true)
		{
			object obj3;
			object obj6;
			if (num != 0)
			{
				((Dictionary<UserStat, int>)null).set_Item((UserStat)typeof(IEnumerator), num);
				if (obj2 != null)
				{
					bool flag = num == 0;
					text = null;
					if (!flag)
					{
						int value = ((int*)num)->m_value;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v276 @ r10_v9 (System.Int32)+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_0179;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v276 @ r10_v9 (System.Int32)+B0]");
						obj3 = 0;
						float num2 = 0f;
						while (true)
						{
							float num3 = num2 + num2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v396 @ r8_v13+v583 @ rax_v45 (System.Single)*8]");
							if (0 == (nint)typeof(IEnumerator<UserStat>))
							{
								break;
							}
							num2++;
							float num4 = num2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v276 @ r10_v9 (System.Int32)+12E]");
							if (num4 < 0f)
							{
								continue;
							}
							goto IL_0179;
						}
						float num5 = num2 + num2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v396 @ r8_v13+8+v639 @ rcx_v36 (System.Single)*8]");
						object obj4 = (nint)0 << 4;
						object obj5 = obj4 + 312;
						obj6 = obj5 + value;
						goto IL_0410;
					}
					throw new NullReferenceException();
				}
				if (obj != null)
				{
					((Dictionary<UserStat, int>)null).set_Item((UserStat)typeof(IDisposable), (int)obj);
				}
				bool flag2 = SteamUserStats.StoreStats();
				return;
			}
			throw new NullReferenceException();
			IL_0179:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj3 = 0;
			obj6 = obj7;
			goto IL_0410;
			IL_0410:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v644 @ rdx_v17] (should have been resolved before IL gen)");
			int nData;
			if (isInitialized)
			{
				if (statsState == null)
				{
					throw new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
				if (statsChangesCache == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
				nData = (int)(obj8 + obj9);
				num6 = num7;
			}
			else
			{
				nData = 0;
			}
			string pchName = ((Enum)(&intPtr)).ToString();
			if (SteamUserStats.SetStat(pchName, nData))
			{
				if (statsState == null)
				{
					throw new NullReferenceException();
				}
				statsState.set_Item((UserStat)(int)(&num8), (int)(&num9));
				if (statsChangesCache == null)
				{
					throw new NullReferenceException();
				}
				statsChangesCache.set_Item((UserStat)(int)(&num8), (int)(&num6));
				num6 = 0f;
			}
		}
		throw new NullReferenceException();
	}

	private unsafe void OnUserStatsReceived(UserStatsReceived_t param)
	{
		//IL_00af: Expected I4, but got O
		//IL_00af: Expected I4, but got O
		//IL_00b7: Expected O, but got Ref
		//IL_00d5: Expected I4, but got O
		//IL_01d6: Expected I4, but got O
		//IL_01d6: Expected I4, but got O
		//IL_0124: Expected I4, but got O
		//IL_012d: Expected O, but got Ref
		//IL_022b: Expected I4, but got O
		//IL_022b: Expected I4, but got O
		//IL_0233: Expected O, but got Ref
		//IL_0251: Expected I4, but got O
		//IL_0402: Expected I4, but got O
		//IL_0402: Expected I4, but got O
		//IL_0324: Expected O, but got I4
		//IL_054b: Expected O, but got Ref
		//IL_02d1: Expected O, but got I
		//IL_02da: Expected O, but got I4
		//IL_0385: Expected O, but got I
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Expected O, but got Unknown
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Expected O, but got Unknown
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Expected O, but got Unknown
		//IL_035d: Expected I4, but got I8
		if (!isInitialized)
		{
			return;
		}
		if ((long)gameId == (long)param.m_nGameID)
		{
			EResult eResult = default(EResult);
			if (param.m_eResult == EResult.k_EResultOK)
			{
				Debug.Log("[Steamworks.NET] Received stats and achievements from Steam");
				Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(AchievementType));
				Array values = Enum.GetValues(typeFromHandle);
				IEnumerable<AchievementType> enumerable = Enumerable.Cast<AchievementType>(values);
				((Dictionary<AchievementType, bool>)null).set_Item((AchievementType)typeof(IEnumerable<AchievementType>), (byte)(int)enumerable != 0);
				bool flag = default(bool);
				object obj = (object)(&flag);
				object obj2 = null;
				object obj3 = default(object);
				IntPtr intPtr = default(IntPtr);
				object obj4 = default(object);
				bool flag3 = default(bool);
				object obj5;
				bool flag4 = default(bool);
				while (true)
				{
					if (flag)
					{
						((Dictionary<AchievementType, bool>)null).set_Item((AchievementType)typeof(IEnumerator), flag);
						if (obj3 != null)
						{
							bool flag2 = !flag;
							obj2 = null;
							if (!flag2)
							{
								((Dictionary<AchievementType, bool>)null).set_Item((AchievementType)typeof(IEnumerator<AchievementType>), flag);
								string pchName = ((Enum)(&intPtr)).ToString();
								if (!SteamUserStats.GetAchievement(pchName, out var _))
								{
									object arg = (AchievementType)eResult;
									string message = $"[Steamworks.NET] SteamUserStats.GetAchievement failed for Achievement {arg}\nIs it registered on the Steam Partner site?";
									Debug.LogWarning(message);
								}
								else
								{
									achievementsState.set_Item((AchievementType)(int)(&obj4), (byte)(&flag3) != 0);
								}
								continue;
							}
							throw new NullReferenceException();
						}
						if (obj != null)
						{
							((Dictionary<AchievementType, bool>)null).set_Item((AchievementType)typeof(IDisposable), (byte)(int)obj != 0);
						}
						Type typeFromHandle2 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(UserStat));
						Array values2 = Enum.GetValues(typeFromHandle2);
						IEnumerable<UserStat> enumerable2 = Enumerable.Cast<UserStat>(values2);
						((Dictionary<AchievementType, bool>)null).set_Item((AchievementType)typeof(IEnumerable<UserStat>), (byte)(int)enumerable2 != 0);
						obj5 = (object)(&flag4);
						Dictionary<UserStat, int> dictionary = null;
						break;
					}
					throw new NullReferenceException();
				}
				object obj6 = default(object);
				object obj15 = default(object);
				IntPtr intPtr2 = default(IntPtr);
				while (true)
				{
					object obj7;
					object obj14;
					Dictionary<UserStat, int> dictionary;
					if (flag4)
					{
						((Dictionary<AchievementType, bool>)null).set_Item((AchievementType)typeof(IEnumerator), flag4);
						if (obj6 != null)
						{
							bool flag5 = !flag4;
							dictionary = null;
							if (!flag5)
							{
								bool value = ((bool*)(flag4 ? 1 : 0))->m_value;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v565 @ r10_v8 (System.Boolean)+12E]");
								if ((nint)0 >= (nint)0)
								{
									goto IL_0311;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v565 @ r10_v8 (System.Boolean)+B0]");
								obj7 = 0;
								object obj8 = 0;
								while (true)
								{
									object obj9 = obj8 + obj8;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v915 @ r8_v18+v854 @ rax_v59*8]");
									if (0 == (nint)typeof(IEnumerator<UserStat>))
									{
										break;
									}
									obj8++;
									object obj10 = obj8;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v565 @ r10_v8 (System.Boolean)+12E]");
									if ((nint)obj10 < 0)
									{
										continue;
									}
									goto IL_0311;
								}
								object obj11 = obj8 + obj8;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v915 @ r8_v18+8+v910 @ rcx_v47*8]");
								object obj12 = (nint)0 << 4;
								object obj13 = obj12 + 312;
								obj14 = obj13 + value;
								goto IL_0538;
							}
							throw new NullReferenceException();
						}
						if (obj5 != null)
						{
							((Dictionary<AchievementType, bool>)null).set_Item((AchievementType)typeof(IDisposable), (byte)(int)obj5 != 0);
						}
						return;
					}
					throw new NullReferenceException();
					IL_0311:
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
					obj7 = 0;
					obj14 = obj15;
					goto IL_0538;
					IL_0538:
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v916 @ rdx_v29] (should have been resolved before IL gen)");
					string pchName2 = ((Enum)(&intPtr2)).ToString();
					bool stat = SteamUserStats.GetStat(pchName2, out int pData);
					dictionary = statsState;
					int num;
					if (!stat)
					{
						if (statsState == null)
						{
							break;
						}
						num = -1;
					}
					else
					{
						if (statsState == null)
						{
							throw new NullReferenceException();
						}
						num = pData;
					}
					dictionary.set_Item((UserStat)(int)(&obj4), (int)(&num));
				}
				throw new NullReferenceException();
			}
			object arg2 = eResult;
			string message2 = $"[Steamworks.NET] RequestStats - failed, {arg2}";
			Debug.LogWarning(message2);
		}
		else
		{
			Debug.Log("[Steamworks.NET] ID mismatch stat received");
		}
	}

	private unsafe void OnUserStatsStored(UserStatsStored_t param)
	{
		//IL_00c6: Expected O, but got Ref
		//IL_0078: Expected I4, but got O
		if (!isInitialized)
		{
			return;
		}
		if ((long)gameId == (long)param.m_nGameID)
		{
			if (param.m_eResult != EResult.k_EResultOK)
			{
				if (param.m_eResult != EResult.k_EResultInvalidParam)
				{
					object obj = default(object);
					object arg = (EResult)obj;
					string message = $"[Steamworks.NET] StoreStats - failed, {arg}";
					Debug.LogWarning(message);
					requestLocked = false;
				}
				else
				{
					Debug.LogWarning("[Steamworks.NET] StoreStats - some failed to validate");
					object obj2 = default(object);
					OnUserStatsReceived((UserStatsReceived_t)(&obj2));
					requestLocked = false;
				}
			}
			else
			{
				Debug.Log("[Steamworks.NET] StoreStats - success");
				requestLocked = false;
			}
		}
		else
		{
			Debug.Log("[Steamworks.NET] ID mismatch stats stored");
		}
	}

	private unsafe void OnAchievementStored(UserAchievementStored_t param)
	{
		//IL_0195: Expected I, but got O
		//IL_01a2: Expected I, but got O
		if (!isInitialized)
		{
			return;
		}
		if ((long)gameId == (long)param.m_nGameID)
		{
			string message;
			int num2 = default(int);
			uint nCurProgress = default(uint);
			if ((int)param.m_nCurProgress >> 32 == 0)
			{
				string rgchAchievementName = ((UserAchievementStored_t*)param)->m_rgchAchievementName;
				message = "[Steamworks.NET] Achievement '" + rgchAchievementName + "' unlocked!";
				object obj = null;
			}
			else
			{
				string rgchAchievementName2 = ((UserAchievementStored_t*)param)->m_rgchAchievementName;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				int num = (int)param.m_nCurProgress >> 32;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				object obj2 = default(object);
				message = $"[Steamworks.NET] Achievement '{rgchAchievementName2}' progress callback, ({arg},{obj2})";
				num2 = num;
				nCurProgress = param.m_nCurProgress;
				object obj = obj2;
			}
			Debug.Log(message);
			if ((int)param.m_nCurProgress >> 32 == 0)
			{
				Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(AchievementType));
				string rgchAchievementName3 = ((UserAchievementStored_t*)param)->m_rgchAchievementName;
				object obj3 = Enum.Parse(typeFromHandle, rgchAchievementName3);
				nint num3 = (nint)typeof(AchievementType);
				nint num4 = (nint)obj3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v343 @ rdx_v11 (Il2CppClass<System.Object>)+40]");
				nint num5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ r8_v5 (Il2CppClass<AchievementType>)+40]");
				if (num5 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
					throw new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
				achievementsState.set_Item((AchievementType)(int)(&num2), (byte)(&nCurProgress) != 0);
				Action<AchievementType> onAchievementUnlocked = this.m_OnAchievementUnlocked;
				if (this.m_OnAchievementUnlocked != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v293 @ rcx_v19 (System.Action`1<AchievementType>)+18] (should have been resolved before IL gen)");
				}
			}
		}
		else
		{
			Debug.Log("[Steamworks.NET] ID mismatch achievement stored");
		}
	}
}
