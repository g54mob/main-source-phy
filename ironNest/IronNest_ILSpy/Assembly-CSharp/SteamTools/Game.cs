using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using Heathen.SteamworksIntegration;
using Heathen.SteamworksIntegration.API;
using UnityEngine;

namespace SteamTools;

public static class Game
{
	public static class Stats
	{
		public static StatData AverageSpeed;

		public static StatData FeetTraveled;

		public static StatData MaxFeetTraveled;

		public static StatData NumGames;

		public static StatData NumLosses;

		public static StatData NumWins;

		public static StatData Unused2;

		public unsafe static Dictionary<string, StatData> GetMap()
		{
			//IL_0018: Expected O, but got Ref
			//IL_002b: Expected O, but got Ref
			//IL_0043: Expected O, but got Ref
			//IL_0056: Expected O, but got Ref
			//IL_006e: Expected O, but got Ref
			//IL_0081: Expected O, but got Ref
			//IL_0098: Expected O, but got Ref
			Dictionary<string, StatData> dictionary = new Dictionary<string, StatData>();
			if (dictionary != null)
			{
				StatData statData = default(StatData);
				dictionary.Add("AverageSpeed", (StatData)(&statData));
				dictionary.Add("FeetTraveled", (StatData)(&statData));
				dictionary.Add("MaxFeetTraveled", (StatData)(&statData));
				dictionary.Add("NumGames", (StatData)(&statData));
				dictionary.Add("NumLosses", (StatData)(&statData));
				dictionary.Add("NumWins", (StatData)(&statData));
				dictionary.Add("Unused2", (StatData)(&statData));
				return dictionary;
			}
			return (Dictionary<string, StatData>)(object)new NullReferenceException();
		}

		static Stats()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
			StatData averageSpeed = default(StatData);
			AverageSpeed = averageSpeed;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
			StatData feetTraveled = default(StatData);
			FeetTraveled = feetTraveled;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
			StatData maxFeetTraveled = default(StatData);
			MaxFeetTraveled = maxFeetTraveled;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
			StatData numGames = default(StatData);
			NumGames = numGames;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
			StatData numLosses = default(StatData);
			NumLosses = numLosses;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
			StatData numWins = default(StatData);
			NumWins = numWins;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
			StatData unused = default(StatData);
			Unused2 = unused;
		}
	}

	public static class Achievements
	{
		public static AchievementData ACH_TRAVEL_FAR_ACCUM;

		public static AchievementData ACH_TRAVEL_FAR_SINGLE;

		public static AchievementData ACH_WIN_100_GAMES;

		public static AchievementData ACH_WIN_ONE_GAME;

		public static AchievementData NEW_ACHIEVEMENT_0_4;

		public unsafe static Dictionary<string, AchievementData> GetMap()
		{
			//IL_0018: Expected O, but got Ref
			//IL_002b: Expected O, but got Ref
			//IL_0043: Expected O, but got Ref
			//IL_0056: Expected O, but got Ref
			//IL_006d: Expected O, but got Ref
			Dictionary<string, AchievementData> dictionary = new Dictionary<string, AchievementData>();
			if (dictionary != null)
			{
				AchievementData achievementData = default(AchievementData);
				dictionary.Add("ACH_TRAVEL_FAR_ACCUM", (AchievementData)(&achievementData));
				dictionary.Add("ACH_TRAVEL_FAR_SINGLE", (AchievementData)(&achievementData));
				dictionary.Add("ACH_WIN_100_GAMES", (AchievementData)(&achievementData));
				dictionary.Add("ACH_WIN_ONE_GAME", (AchievementData)(&achievementData));
				dictionary.Add("NEW_ACHIEVEMENT_0_4", (AchievementData)(&achievementData));
				return dictionary;
			}
			return (Dictionary<string, AchievementData>)(object)new NullReferenceException();
		}

		static Achievements()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
			AchievementData aCH_TRAVEL_FAR_ACCUM = default(AchievementData);
			ACH_TRAVEL_FAR_ACCUM = aCH_TRAVEL_FAR_ACCUM;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
			AchievementData aCH_TRAVEL_FAR_SINGLE = default(AchievementData);
			ACH_TRAVEL_FAR_SINGLE = aCH_TRAVEL_FAR_SINGLE;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
			AchievementData aCH_WIN_100_GAMES = default(AchievementData);
			ACH_WIN_100_GAMES = aCH_WIN_100_GAMES;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
			AchievementData aCH_WIN_ONE_GAME = default(AchievementData);
			ACH_WIN_ONE_GAME = aCH_WIN_ONE_GAME;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
			AchievementData nEW_ACHIEVEMENT_0_ = default(AchievementData);
			NEW_ACHIEVEMENT_0_4 = nEW_ACHIEVEMENT_0_;
		}
	}

	public static class Leaderboards
	{
		public static LeaderboardData TestHighScore;

		public unsafe static Dictionary<string, LeaderboardData> GetMap()
		{
			//IL_0017: Expected O, but got Ref
			Dictionary<string, LeaderboardData> dictionary = new Dictionary<string, LeaderboardData>();
			if (dictionary != null)
			{
				object obj = default(object);
				dictionary.Add("TestHighScore", (LeaderboardData)(&obj));
				return dictionary;
			}
			return (Dictionary<string, LeaderboardData>)(object)new NullReferenceException();
		}
	}

	public static class Inputs
	{
		public static class Sets
		{
			public static InputActionSetData menu_controls;

			public static InputActionSetData ship_controls;

			public unsafe static Dictionary<string, InputActionSetData> GetMap()
			{
				//IL_0018: Expected O, but got Ref
				//IL_002a: Expected O, but got Ref
				Dictionary<string, InputActionSetData> dictionary = new Dictionary<string, InputActionSetData>();
				if (dictionary != null)
				{
					InputActionSetData inputActionSetData = default(InputActionSetData);
					dictionary.Add("menu_controls", (InputActionSetData)(&inputActionSetData));
					dictionary.Add("ship_controls", (InputActionSetData)(&inputActionSetData));
					return dictionary;
				}
				return (Dictionary<string, InputActionSetData>)(object)new NullReferenceException();
			}

			public static void Initialise()
			{
				InputActionSetData inputActionSetData = InputActionSetData.Get("menu_controls");
				menu_controls = inputActionSetData;
				InputActionSetData inputActionSetData2 = InputActionSetData.Get("ship_controls");
				ship_controls = inputActionSetData2;
			}
		}

		public static class Layers
		{
			public static InputActionSetLayerData thrust_action_layer = (InputActionSetLayerData)"thrust_action_layer";

			public unsafe static Dictionary<string, InputActionSetLayerData> GetMap()
			{
				//IL_0017: Expected O, but got Ref
				Dictionary<string, InputActionSetLayerData> dictionary = new Dictionary<string, InputActionSetLayerData>();
				if (dictionary != null)
				{
					object obj = default(object);
					dictionary.Add("thrust_action_layer", (InputActionSetLayerData)(&obj));
					return dictionary;
				}
				return (Dictionary<string, InputActionSetLayerData>)(object)new NullReferenceException();
			}
		}

		public static class Actions
		{
			public static InputActionData analog_controls;

			public static InputActionData backward_thrust;

			public static InputActionData fire_lasers;

			public static InputActionData forward_thrust;

			public static InputActionData menu_cancel;

			public static InputActionData menu_down;

			public static InputActionData menu_left;

			public static InputActionData menu_right;

			public static InputActionData menu_select;

			public static InputActionData menu_up;

			public static InputActionData pause_menu;

			public static InputActionData turn_left;

			public static InputActionData turn_right;

			public unsafe static Dictionary<string, InputActionData> GetMap()
			{
				//IL_0008: Expected O, but got Ref
				//IL_001b: Expected O, but got Ref
				//IL_0041: Expected O, but got Ref
				//IL_006c: Expected O, but got Ref
				//IL_0092: Expected O, but got Ref
				//IL_00bd: Expected O, but got Ref
				//IL_00e3: Expected O, but got Ref
				//IL_010e: Expected O, but got Ref
				//IL_0134: Expected O, but got Ref
				//IL_015f: Expected O, but got Ref
				//IL_0185: Expected O, but got Ref
				//IL_01b0: Expected O, but got Ref
				//IL_01d6: Expected O, but got Ref
				//IL_0201: Expected O, but got Ref
				object obj2 = default(object);
				object obj = (object)(&obj2);
				Dictionary<string, InputActionData> dictionary = new Dictionary<string, InputActionData>();
				if (dictionary != null)
				{
					InputActionData value = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 121));
					_ = analog_controls;
					dictionary.Add("analog_controls", value);
					InputActionData value2 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
					_ = backward_thrust;
					dictionary.Add("backward_thrust", value2);
					InputActionData value3 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
					_ = fire_lasers;
					dictionary.Add("fire_lasers", value3);
					InputActionData value4 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
					_ = forward_thrust;
					dictionary.Add("forward_thrust", value4);
					InputActionData value5 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
					_ = menu_cancel;
					dictionary.Add("menu_cancel", value5);
					InputActionData value6 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
					_ = menu_down;
					dictionary.Add("menu_down", value6);
					InputActionData value7 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
					_ = menu_left;
					dictionary.Add("menu_left", value7);
					InputActionData value8 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
					_ = menu_right;
					dictionary.Add("menu_right", value8);
					InputActionData value9 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
					_ = menu_select;
					dictionary.Add("menu_select", value9);
					InputActionData value10 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 23));
					_ = menu_up;
					dictionary.Add("menu_up", value10);
					InputActionData value11 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 39));
					_ = pause_menu;
					dictionary.Add("pause_menu", value11);
					InputActionData value12 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 55));
					_ = turn_left;
					dictionary.Add("turn_left", value12);
					InputActionData value13 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 71));
					_ = turn_right;
					dictionary.Add("turn_right", value13);
					return dictionary;
				}
				return (Dictionary<string, InputActionData>)(object)new NullReferenceException();
			}

			unsafe static Actions()
			{
				//IL_0008: Expected O, but got Ref
				//IL_0269: Expected O, but got Ref
				//IL_028a: Expected O, but got I
				//IL_0298: Expected O, but got Ref
				//IL_001e: Expected O, but got I
				//IL_002c: Expected O, but got Ref
				//IL_0052: Expected O, but got I
				//IL_0060: Expected O, but got Ref
				//IL_0086: Expected O, but got I
				//IL_0094: Expected O, but got Ref
				//IL_00ba: Expected O, but got I
				//IL_00c8: Expected O, but got Ref
				//IL_00ee: Expected O, but got I
				//IL_00fc: Expected O, but got Ref
				//IL_0122: Expected O, but got I
				//IL_0130: Expected O, but got Ref
				//IL_0156: Expected O, but got I
				//IL_0164: Expected O, but got Ref
				//IL_018a: Expected O, but got I
				//IL_0198: Expected O, but got Ref
				//IL_01be: Expected O, but got I
				//IL_01d2: Expected O, but got Ref
				//IL_01f2: Expected O, but got I
				//IL_0200: Expected O, but got Ref
				//IL_0226: Expected O, but got I
				//IL_0234: Expected O, but got Ref
				//IL_025a: Expected O, but got I
				object obj2 = default(object);
				object obj = (object)(&obj2);
				object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 121));
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1-79]");
				analog_controls = (InputActionData)0;
				object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1-69]");
				backward_thrust = (InputActionData)0;
				object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1-59]");
				fire_lasers = (InputActionData)0;
				object obj6 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1-49]");
				forward_thrust = (InputActionData)0;
				object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1-39]");
				menu_cancel = (InputActionData)0;
				object obj8 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1-29]");
				menu_down = (InputActionData)0;
				object obj9 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1-19]");
				menu_left = (InputActionData)0;
				object obj10 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1-9]");
				menu_right = (InputActionData)0;
				object obj11 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+7]");
				menu_select = (InputActionData)0;
				object obj12 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 23));
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+17]");
				menu_up = (InputActionData)0;
				_ = 0;
				object obj13 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 39));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+27]");
				pause_menu = (InputActionData)0;
				object obj14 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 55));
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+37]");
				turn_left = (InputActionData)0;
				object obj15 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 71));
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rbp_v1+47]");
				turn_right = (InputActionData)0;
			}
		}
	}

	private sealed class _003C_003Ec__DisplayClass5_0
	{
		public int returnedBoards;

		public int boardCount;

		internal unsafe void _003CHandleInitialised_003Eb__0(LeaderboardData result, bool ioError)
		{
			//IL_0067: Expected O, but got Ref
			//IL_007f: Expected O, but got Ref
			//IL_0091: Expected O, but got Ref
			if (!ioError)
			{
				Leaderboards.TestHighScore = (LeaderboardData)result.apiName;
			}
			if (++returnedBoards >= boardCount)
			{
				Dictionary<string, LeaderboardData> dictionary = new Dictionary<string, LeaderboardData>();
				object obj = default(object);
				dictionary.Add("TestHighScore", (LeaderboardData)(&obj));
				Dictionary<string, InputActionSetData> dictionary2 = new Dictionary<string, InputActionSetData>();
				InputActionSetData inputActionSetData = default(InputActionSetData);
				dictionary2.Add("menu_controls", (InputActionSetData)(&inputActionSetData));
				dictionary2.Add("ship_controls", (InputActionSetData)(&inputActionSetData));
				Dictionary<string, InputActionData> map = Inputs.Actions.GetMap();
				Interface.RaiseOnReady(dictionary, dictionary2, map);
			}
		}
	}

	public const uint AppId = 2950790u;

	public static SteamGameServerConfiguration ServerConfiguration;

	public static void ServerConfigFromIni(string iniData)
	{
		//IL_002a: Expected I, but got O
		//IL_004d: Expected O, but got I4
		SteamGameServerConfiguration steamGameServerConfiguration = SteamGameServerConfiguration.ParseIniString(iniData);
		nint num = (nint)typeof(Game);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ rax_v5 (Il2CppClass<SteamTools.Game>)+B8]");
		nint num2 = 0;
		ServerConfiguration = (SteamGameServerConfiguration)steamGameServerConfiguration.autoInitialise;
		_ = steamGameServerConfiguration.serverVersion;
		_ = steamGameServerConfiguration.spectatorServerName;
		_ = steamGameServerConfiguration.gameServerToken;
		_ = steamGameServerConfiguration.serverName;
		_ = steamGameServerConfiguration.gameDirectory;
		_ = steamGameServerConfiguration.botPlayerCount;
		_ = steamGameServerConfiguration.gameData;
	}

	public static void ServerConfigFromJson(string jsonData)
	{
		//IL_001d: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070BE60");
		nint num = (nint)typeof(Game);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v4 (Il2CppClass<SteamTools.Game>)+B8]");
		nint num2 = 0;
		SteamGameServerConfiguration serverConfiguration = default(SteamGameServerConfiguration);
		ServerConfiguration = serverConfiguration;
	}

	public unsafe static void Initialise()
	{
		//IL_0008: Expected O, but got Ref
		//IL_03ad: Expected O, but got Ref
		//IL_0066: Expected O, but got Ref
		//IL_0091: Expected native int or pointer, but got O
		//IL_00a3: Expected O, but got Ref
		//IL_00c0: Expected O, but got Ref
		//IL_00f0: Expected O, but got Ref
		//IL_0120: Expected O, but got Ref
		//IL_0150: Expected O, but got Ref
		//IL_0171: Expected O, but got Ref
		//IL_01a1: Expected O, but got Ref
		//IL_01bc: Expected O, but got Ref
		//IL_01df: Expected O, but got Ref
		//IL_0207: Expected O, but got Ref
		//IL_022a: Expected O, but got Ref
		//IL_0252: Expected O, but got Ref
		//IL_0275: Expected O, but got Ref
		//IL_029d: Expected O, but got Ref
		//IL_02c0: Expected O, but got Ref
		//IL_02e8: Expected O, but got Ref
		//IL_030b: Expected O, but got Ref
		//IL_0333: Expected O, but got Ref
		//IL_0356: Expected O, but got Ref
		object obj = default(object);
		InputActionData inputActionData = (InputActionData)(&obj);
		object obj2 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 216));
		_ = 2950790;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg = default(object);
		string message = $"Initialising for app {arg}";
		Debug.Log(message);
		Action value = HandleInitialised;
		Events.OnSteamInitialised += value;
		List<InputActionData> list = new List<InputActionData>();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
		InputActionData item = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj, 16));
		_ = 0;
		list.Add(item);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
		((InputActionData*)(nint)inputActionData)->type = InputActionType.Analog;
		list.Add((InputActionData)(&obj));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
		InputActionData item2 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 16));
		_ = 0;
		list.Add(item2);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
		InputActionData item3 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 32));
		_ = 0;
		list.Add(item3);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
		InputActionData item4 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 48));
		_ = 0;
		list.Add(item4);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
		InputActionData item5 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 64));
		_ = 0;
		list.Add(item5);
		object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj, 128));
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (Heathen.SteamworksIntegration.InputActionData)-80]");
		_ = 0;
		InputActionData item6 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 80));
		list.Add(item6);
		object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj, 112));
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
		InputActionData item7 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 96));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (Heathen.SteamworksIntegration.InputActionData)-70]");
		_ = 0;
		list.Add(item7);
		object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj, 96));
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
		InputActionData item8 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 112));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (Heathen.SteamworksIntegration.InputActionData)-60]");
		_ = 0;
		list.Add(item8);
		object obj6 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj, 80));
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
		InputActionData item9 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 128));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (Heathen.SteamworksIntegration.InputActionData)-50]");
		_ = 0;
		list.Add(item9);
		object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj, 64));
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
		InputActionData item10 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 144));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (Heathen.SteamworksIntegration.InputActionData)-40]");
		_ = 0;
		list.Add(item10);
		object obj8 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj, 48));
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
		InputActionData item11 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 160));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (Heathen.SteamworksIntegration.InputActionData)-30]");
		_ = 0;
		list.Add(item11);
		object obj9 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj, 32));
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3D0");
		InputActionData item12 = (InputActionData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 176));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (Heathen.SteamworksIntegration.InputActionData)-20]");
		_ = 0;
		list.Add(item12);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18039A070");
		InputActionData[] actions = list.ToArray();
		AppData appId = default(AppData);
		App.Client.Initialise(appId, actions);
	}

	private unsafe static void HandleInitialised()
	{
		_003C_003Ec__DisplayClass5_0 CS_0024_003C_003E8__locals4 = new _003C_003Ec__DisplayClass5_0();
		InputActionSetData menu_controls = InputActionSetData.Get("menu_controls");
		Inputs.Sets.menu_controls = menu_controls;
		InputActionSetData ship_controls = InputActionSetData.Get("ship_controls");
		Inputs.Sets.ship_controls = ship_controls;
		CS_0024_003C_003E8__locals4.boardCount = 1;
		CS_0024_003C_003E8__locals4.returnedBoards = 0;
		Action<LeaderboardData, bool> callback = delegate(LeaderboardData result, bool ioError)
		{
			//IL_0067: Expected O, but got Ref
			//IL_007f: Expected O, but got Ref
			//IL_0091: Expected O, but got Ref
			if (!ioError)
			{
				Leaderboards.TestHighScore = (LeaderboardData)result.apiName;
			}
			if (++CS_0024_003C_003E8__locals4.returnedBoards >= CS_0024_003C_003E8__locals4.boardCount)
			{
				Dictionary<string, LeaderboardData> dictionary = new Dictionary<string, LeaderboardData>();
				object obj = default(object);
				dictionary.Add("TestHighScore", (LeaderboardData)(&obj));
				Dictionary<string, InputActionSetData> dictionary2 = new Dictionary<string, InputActionSetData>();
				InputActionSetData inputActionSetData = default(InputActionSetData);
				dictionary2.Add("menu_controls", (InputActionSetData)(&inputActionSetData));
				dictionary2.Add("ship_controls", (InputActionSetData)(&inputActionSetData));
				Dictionary<string, InputActionData> map = Inputs.Actions.GetMap();
				Interface.RaiseOnReady(dictionary, dictionary2, map);
			}
		};
		LeaderboardData.Get("TestHighScore", callback);
	}

	static Game()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_0070: Expected I, but got O
		//IL_0091: Expected O, but got I
		object obj2 = default(object);
		object obj = obj2 - 95;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		nint num = (nint)typeof(Game);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rax_v12 (Il2CppClass<SteamTools.Game>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-49]");
		ServerConfiguration = (SteamGameServerConfiguration)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-39]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-29]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-19]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-9]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+7]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+17]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+27]");
		_ = 0;
	}
}
