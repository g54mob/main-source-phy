using System.Collections.Generic;

namespace Photon.Bolt
{
	public static class BoltScenes
	{
		internal static readonly Dictionary<string, int> nameLookup;

		internal static readonly Dictionary<int, string> indexLookup;

		public const string BootScene = "BootScene";

		public const string AprilJokeScene = "AprilJokeScene";

		public const string Credits = "Credits";

		public const string MainMenu = "MainMenu";

		public const string GameScene = "GameScene";

		public const string SafeArea = "SafeArea";

		public const string UnitCreator_GamepadUI = "UnitCreator_GamepadUI";

		public const string CustomContentPage = "CustomContentPage";

		public const string LevelScene = "LevelScene";

		public const string Editor_Scene = "Editor Scene";

		public const string LiteLoadingScreen = "LiteLoadingScreen";

		public const string MainMenu_Ancient = "MainMenu_Ancient";

		public const string MainMenu_Dynasty = "MainMenu_Dynasty";

		public const string MainMenu_Farmer = "MainMenu_Farmer";

		public const string MainMenu_Legacy = "MainMenu_Legacy";

		public const string MainMenu_Medieval = "MainMenu_Medieval";

		public const string MainMenu_Pirate = "MainMenu_Pirate";

		public const string MainMenu_Renaissance = "MainMenu_Renaissance";

		public const string MainMenu_Spooky = "MainMenu_Spooky";

		public const string MainMenu_Tribal = "MainMenu_Tribal";

		public const string MainMenu_Viking = "MainMenu_Viking";

		public const string _00_Simulation_Day_VC = "00_Simulation_Day_VC";

		public const string _00_Simulation_Night_VC = "00_Simulation_Night_VC";

		public const string _00_Simulation_01_Bridge_VC = "00_Simulation_01_Bridge_VC";

		public const string _00_Simulation_02_Bridge_VC = "00_Simulation_02_Bridge_VC";

		public const string _00_Simulation_03_Maze_VC = "00_Simulation_03_Maze_VC";

		public const string _00_Simulation_04_Hill_VC = "00_Simulation_04_Hill_VC";

		public const string _00_Simulation_05_Port_VC = "00_Simulation_05_Port_VC";

		public const string _00_Simulation_06_Holes_VC = "00_Simulation_06_Holes_VC";

		public const string _00_Simulation_07_Choke_VC = "00_Simulation_07_Choke_VC";

		public const string _00_Simulation_08_BrokenBridge_VC = "00_Simulation_08_BrokenBridge_VC";

		public const string _00_Simulation_09_DefendTheTower_VC = "00_Simulation_09_DefendTheTower_VC";

		public const string _00_Simulation_10_Castle_VC = "00_Simulation_10_Castle_VC";

		public const string _00_Simulation_11_ArcherTower_VC = "00_Simulation_11_ArcherTower_VC";

		public const string _00_Simulation_12_FinalDestination_VC = "00_Simulation_12_FinalDestination_VC";

		public const string _00_Simulation_13_Defendthetower_VC = "00_Simulation_13_Defendthetower_VC";

		public const string _00_Simulation_14_Spire_VC = "00_Simulation_14_Spire_VC";

		public const string _00_Simulation_15_TwoChokes_VC = "00_Simulation_15_TwoChokes_VC";

		public const string _00_Simulation_16_TheHighground_VC = "00_Simulation_16_TheHighground_VC";

		public const string _00_Simulation_17_Moat_VC = "00_Simulation_17_Moat_VC";

		public const string _00_Simulation_18_Arena_VC = "00_Simulation_18_Arena_VC";

		public const string _00_Simulation_19_Lowground_VC = "00_Simulation_19_Lowground_VC";

		public const string _00_Simulation_20_TieredCastle_VC = "00_Simulation_20_TieredCastle_VC";

		public const string _00_Simulation_21_Double_Battle_VC = "00_Simulation_21_Double Battle_VC";

		public const string _01_Lvl1_Tribal_VC = "01_Lvl1_Tribal_VC";

		public const string _01_Lvl2_Tribal_VC = "01_Lvl2_Tribal_VC";

		public const string _01_Sandbox_Tribal_01_VC = "01_Sandbox_Tribal_01_VC";

		public const string _02_Lvl1_Farmer_VC = "02_Lvl1_Farmer_VC";

		public const string _02_Lvl2_Farmer_VC = "02_Lvl2_Farmer_VC";

		public const string _05_Lvl1_Medieval_VC = "05_Lvl1_Medieval_VC";

		public const string _05_Lvl2_Medieval_VC = "05_Lvl2_Medieval_VC";

		public const string _03_Lvl1_Ancient_VC = "03_Lvl1_Ancient_VC";

		public const string _03_Lvl2_Ancient_VC = "03_Lvl2_Ancient_VC";

		public const string _03_Sandbox_Ancient_01_VC = "03_Sandbox_Ancient_01_VC";

		public const string _04_Lvl1_Viking_VC = "04_Lvl1_Viking_VC";

		public const string _04_Sandbox_Viking_VC = "04_Sandbox_Viking_VC";

		public const string _05_AsiaTemple_VC = "05_AsiaTemple_VC";

		public const string _07_lvl1_Renaissance_VC = "07_lvl1_Renaissance_VC";

		public const string _08_Lvl1_Pirate_VC = "08_Lvl1_Pirate_VC";

		public const string _00_Lvl1_Halloween_VC = "00_Lvl1_Halloween_VC";

		public const string _00_Lvl2_Halloween_VC = "00_Lvl2_Halloween_VC";

		public const string _09_Lvl1_Western_VC = "09_Lvl1_Western_VC";

		public const string _05_Sandbox_Medieval_VC = "05_Sandbox_Medieval_VC";

		public const string _09_Lvl1_Fantasy_Good_VC = "09_Lvl1_Fantasy_Good_VC";

		public const string _09_Lvl1_Fantasy_Evil_VC = "09_Lvl1_Fantasy_Evil_VC";

		public const string _02_Lvl3_Farmer_Snow_VC = "02_Lvl3_Farmer_Snow_VC";

		public static IEnumerable<string> AllScenes => nameLookup.Keys;

		static BoltScenes()
		{
			nameLookup = new Dictionary<string, int>();
			indexLookup = new Dictionary<int, string>();
			AddScene(0, 0, "BootScene");
			AddScene(0, 1, "AprilJokeScene");
			AddScene(0, 2, "Credits");
			AddScene(0, 3, "MainMenu");
			AddScene(0, 4, "GameScene");
			AddScene(0, 5, "SafeArea");
			AddScene(0, 6, "UnitCreator_GamepadUI");
			AddScene(0, 7, "CustomContentPage");
			AddScene(0, 8, "LevelScene");
			AddScene(0, 9, "Editor Scene");
			AddScene(0, 10, "LiteLoadingScreen");
			AddScene(0, 11, "MainMenu_Ancient");
			AddScene(0, 12, "MainMenu_Dynasty");
			AddScene(0, 13, "MainMenu_Farmer");
			AddScene(0, 14, "MainMenu_Legacy");
			AddScene(0, 15, "MainMenu_Medieval");
			AddScene(0, 16, "MainMenu_Pirate");
			AddScene(0, 17, "MainMenu_Renaissance");
			AddScene(0, 18, "MainMenu_Spooky");
			AddScene(0, 19, "MainMenu_Tribal");
			AddScene(0, 20, "MainMenu_Viking");
			AddScene(0, 21, "00_Simulation_Day_VC");
			AddScene(0, 22, "00_Simulation_Night_VC");
			AddScene(0, 23, "00_Simulation_01_Bridge_VC");
			AddScene(0, 24, "00_Simulation_02_Bridge_VC");
			AddScene(0, 25, "00_Simulation_03_Maze_VC");
			AddScene(0, 26, "00_Simulation_04_Hill_VC");
			AddScene(0, 27, "00_Simulation_05_Port_VC");
			AddScene(0, 28, "00_Simulation_06_Holes_VC");
			AddScene(0, 29, "00_Simulation_07_Choke_VC");
			AddScene(0, 30, "00_Simulation_08_BrokenBridge_VC");
			AddScene(0, 31, "00_Simulation_09_DefendTheTower_VC");
			AddScene(0, 32, "00_Simulation_10_Castle_VC");
			AddScene(0, 33, "00_Simulation_11_ArcherTower_VC");
			AddScene(0, 34, "00_Simulation_12_FinalDestination_VC");
			AddScene(0, 35, "00_Simulation_13_Defendthetower_VC");
			AddScene(0, 36, "00_Simulation_14_Spire_VC");
			AddScene(0, 37, "00_Simulation_15_TwoChokes_VC");
			AddScene(0, 38, "00_Simulation_16_TheHighground_VC");
			AddScene(0, 39, "00_Simulation_17_Moat_VC");
			AddScene(0, 40, "00_Simulation_18_Arena_VC");
			AddScene(0, 41, "00_Simulation_19_Lowground_VC");
			AddScene(0, 42, "00_Simulation_20_TieredCastle_VC");
			AddScene(0, 43, "00_Simulation_21_Double Battle_VC");
			AddScene(0, 44, "01_Lvl1_Tribal_VC");
			AddScene(0, 45, "01_Lvl2_Tribal_VC");
			AddScene(0, 46, "01_Sandbox_Tribal_01_VC");
			AddScene(0, 47, "02_Lvl1_Farmer_VC");
			AddScene(0, 48, "02_Lvl2_Farmer_VC");
			AddScene(0, 49, "05_Lvl1_Medieval_VC");
			AddScene(0, 50, "05_Lvl2_Medieval_VC");
			AddScene(0, 51, "03_Lvl1_Ancient_VC");
			AddScene(0, 52, "03_Lvl2_Ancient_VC");
			AddScene(0, 53, "03_Sandbox_Ancient_01_VC");
			AddScene(0, 54, "04_Lvl1_Viking_VC");
			AddScene(0, 55, "04_Sandbox_Viking_VC");
			AddScene(0, 56, "05_AsiaTemple_VC");
			AddScene(0, 57, "07_lvl1_Renaissance_VC");
			AddScene(0, 58, "08_Lvl1_Pirate_VC");
			AddScene(0, 59, "00_Lvl1_Halloween_VC");
			AddScene(0, 60, "00_Lvl2_Halloween_VC");
			AddScene(0, 61, "09_Lvl1_Western_VC");
			AddScene(0, 62, "05_Sandbox_Medieval_VC");
			AddScene(0, 63, "09_Lvl1_Fantasy_Good_VC");
			AddScene(0, 64, "09_Lvl1_Fantasy_Evil_VC");
			AddScene(0, 65, "02_Lvl3_Farmer_Snow_VC");
		}

		public static void AddScene(short prefix, short id, string name)
		{
			int num = (prefix << 16) | id;
			nameLookup.Add(name, num);
			indexLookup.Add(num, name);
		}
	}
}
