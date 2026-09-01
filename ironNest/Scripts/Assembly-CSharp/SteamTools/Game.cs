using System.Collections.Generic;
using Heathen.SteamworksIntegration;

namespace SteamTools
{
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

			public static Dictionary<string, StatData> GetMap()
			{
				return null;
			}
		}

		public static class Achievements
		{
			public static AchievementData ACH_TRAVEL_FAR_ACCUM;

			public static AchievementData ACH_TRAVEL_FAR_SINGLE;

			public static AchievementData ACH_WIN_100_GAMES;

			public static AchievementData ACH_WIN_ONE_GAME;

			public static AchievementData NEW_ACHIEVEMENT_0_4;

			public static Dictionary<string, AchievementData> GetMap()
			{
				return null;
			}
		}

		public static class Leaderboards
		{
			public static LeaderboardData TestHighScore;

			public static Dictionary<string, LeaderboardData> GetMap()
			{
				return null;
			}
		}

		public static class Inputs
		{
			public static class Sets
			{
				public static InputActionSetData menu_controls;

				public static InputActionSetData ship_controls;

				public static Dictionary<string, InputActionSetData> GetMap()
				{
					return null;
				}

				public static void Initialise()
				{
				}
			}

			public static class Layers
			{
				public static InputActionSetLayerData thrust_action_layer;

				public static Dictionary<string, InputActionSetLayerData> GetMap()
				{
					return null;
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

				public static Dictionary<string, InputActionData> GetMap()
				{
					return null;
				}
			}
		}

		public const uint AppId = 2950790u;

		public static SteamGameServerConfiguration ServerConfiguration;

		public static void ServerConfigFromIni(string iniData)
		{
		}

		public static void ServerConfigFromJson(string jsonData)
		{
		}

		public static void Initialise()
		{
		}

		private static void HandleInitialised()
		{
		}
	}
}
