using System;
using System.Collections.Generic;
using System.Linq;
using InternalModding.Loading;
using Modding.Blocks;
using Modding.Levels;
using Ordered;
using UnityEngine;

namespace Modding
{
	public static class Game
	{
		public static class UI
		{
			public enum BlockTab
			{
				Basic = 4,
				Blocks = 0,
				Locomotion = 6,
				Mechanical = 1,
				Weaponry = 2,
				Flight = 3,
				Armour = 5,
				Search = 7
			}

			public static void OpenBlockTab(BlockTab tab)
			{
				BlockTabController blockTabController = UnityEngine.Object.FindObjectOfType<BlockTabController>();
				if (!(blockTabController == null))
				{
					blockTabController.OpenTab((int)tab);
				}
			}

			public static void OpenModdedBlockTab(int tab)
			{
				OpenBlockTab((BlockTab)(Enum.GetNames(typeof(BlockTab)).Length + tab));
			}

			public static void SelectBlockPrefab(BlockType type)
			{
				BlockTabController blockTabController = UnityEngine.Object.FindObjectOfType<BlockTabController>();
				if (!(blockTabController == null))
				{
					int tabIndex = blockTabController.GetTabIndex((int)type);
					OpenBlockTab((BlockTab)tabIndex);
					BlockMenuControl component = blockTabController.tabs[tabIndex].GetComponent<BlockMenuControl>();
					component.BlockButtons.First((BlockButtonControl b) => b.myIndex == (int)type).Set();
				}
			}

			public static void SelectBlockPrefab(Guid modId, int localId)
			{
				SelectBlockPrefab((BlockType)ModIds.GetEffectiveBlockId(modId, localId));
			}

			public static void SelectBlockPrefab(BlockPrefabInfo prefab)
			{
				SelectBlockPrefab((BlockType)prefab.Type);
			}

			public static void OpenLevelCategory(StatMaster.Category cat)
			{
				if (StatMaster.isMP)
				{
					SingleInstanceFindOnly<LevelEditorUI>.Instance.OpenPage(0, cat);
				}
			}

			public static void SelectEntityPrefab(int id)
			{
				if (!StatMaster.isMP)
				{
					return;
				}
				foreach (KeyValuePair<int, Ordered.Dictionary<int, LevelPrefab>> levelPrefab in PrefabMaster.LevelPrefabs)
				{
					if (levelPrefab.Key == 10)
					{
						continue;
					}
					foreach (KeyValuePair<int, LevelPrefab> item in levelPrefab.Value)
					{
						if (item.Value.ID == id)
						{
							OpenLevelCategory((StatMaster.Category)levelPrefab.Key);
							LevelEditor.Instance.SetPrefab(item.Value);
						}
					}
				}
			}

			public static void SelectEntityPrefab(Guid modId, int localId)
			{
				SelectEntityPrefab(ModIds.GetEffectiveEntityId(modId, localId));
			}

			public static void SelectEntityPrefab(EntityPrefabInfo prefab)
			{
				SelectEntityPrefab(prefab.Id);
			}
		}

		public static LayerMask BlockEntityLayerMask;

		public static bool IsSimulatingLocal
		{
			get
			{
				return StatMaster.InLocalPlayMode;
			}
			set
			{
				if (value)
				{
					StartSimulationLocal();
				}
				else
				{
					EndSimulationLocal();
				}
			}
		}

		public static bool IsSimulatingGlobal
		{
			get
			{
				return StatMaster.InGlobalPlayMode;
			}
			set
			{
				if (value)
				{
					StartSimulationGlobal();
				}
				else
				{
					EndSimulationGlobal();
				}
			}
		}

		public static bool IsSetToLocalSim
		{
			get
			{
				return !StatMaster.Mode.LevelEditor.clientGlobalSim;
			}
			set
			{
				if (value != IsSetToLocalSim)
				{
					ToggleLocalSimState();
				}
			}
		}

		public static bool IsSimulating
		{
			get
			{
				return IsSimulatingGlobal || IsSimulatingLocal;
			}
			set
			{
				if (IsSetToLocalSim)
				{
					IsSimulatingLocal = value;
				}
				else
				{
					IsSimulatingGlobal = value;
				}
			}
		}

		public static bool IsSpectator
		{
			get
			{
				return StatMaster.PlayMode == BesiegePlayMode.Spectator;
			}
		}

		public static void StartSimulationLocal()
		{
			if (IsSimulatingLocal)
			{
				return;
			}
			if (IsSimulatingGlobal)
			{
				throw new InvalidOperationException("Can't start local sim while in global sim!");
			}
			if (IsSpectator)
			{
				throw new InvalidOperationException("Can't simulate as spectator!");
			}
			if (SingleInstanceFindOnly<AddPiece>.hasInstance())
			{
				if (StatMaster.Mode.LevelEditor.clientGlobalSim)
				{
					ToggleLocalSimState();
				}
				SingleInstanceFindOnly<AddPiece>.Instance.ToggleSimulate();
			}
		}

		public static void EndSimulationLocal()
		{
			if (IsSimulatingGlobal)
			{
				throw new InvalidOperationException("Tried to end local sim but is in global sim!");
			}
			if (IsSimulatingLocal && SingleInstanceFindOnly<AddPiece>.hasInstance())
			{
				SingleInstanceFindOnly<AddPiece>.Instance.ToggleSimulate();
			}
		}

		public static void StartSimulationGlobal()
		{
			if (IsSimulatingGlobal)
			{
				return;
			}
			if (IsSimulatingLocal)
			{
				throw new InvalidOperationException("Can't start global sim while in local sim!");
			}
			if (IsSpectator)
			{
				throw new InvalidOperationException("Can't simulate as spectator!");
			}
			if (SingleInstanceFindOnly<AddPiece>.hasInstance())
			{
				if (!StatMaster.Mode.LevelEditor.clientGlobalSim)
				{
					ToggleLocalSimState();
				}
				SingleInstanceFindOnly<AddPiece>.Instance.ToggleSimulate();
			}
		}

		public static void EndSimulationGlobal()
		{
			if (IsSimulatingLocal)
			{
				throw new InvalidOperationException("Tried to end global sim but is in local sim!");
			}
			if (IsSimulatingGlobal && SingleInstanceFindOnly<AddPiece>.hasInstance())
			{
				SingleInstanceFindOnly<AddPiece>.Instance.ToggleSimulate();
			}
		}

		public static void ToggleLocalSimState()
		{
			if (StatMaster.isMP)
			{
				if (IsSimulatingGlobal || IsSimulatingLocal)
				{
					throw new InvalidOperationException("Tried to toggle local sim state while in sim!");
				}
				SingleInstanceFindOnly<LevelEditorUI>.Instance.options.ToggleLocalSimButton();
			}
		}

		public static void NextPlaylistLevel()
		{
			if (!StatMaster.isMP)
			{
				return;
			}
			ServerSettings serverSettings = NetworkScene.ServerSettings;
			if (serverSettings.playList.Count != 0)
			{
				serverSettings.playListIndex++;
				if (serverSettings.playListIndex >= serverSettings.playList.Count)
				{
					serverSettings.playListIndex = 0;
				}
				LevelEditor.Instance.LoadPlaylistLevel(serverSettings.playListIndex);
			}
		}

		public static void PreviousPlaylistLevel()
		{
			if (!StatMaster.isMP)
			{
				return;
			}
			ServerSettings serverSettings = NetworkScene.ServerSettings;
			if (serverSettings.playList.Count != 0)
			{
				serverSettings.playListIndex--;
				if (serverSettings.playListIndex < 0)
				{
					serverSettings.playListIndex = serverSettings.playList.Count - 1;
				}
				LevelEditor.Instance.LoadPlaylistLevel(serverSettings.playListIndex);
			}
		}

		public static List<string> GetPlaylist()
		{
			if (!StatMaster.isMP)
			{
				return new List<string>();
			}
			return new List<string>(NetworkScene.ServerSettings.playList);
		}

		public static float GetCurrentProgress(MPTeam team)
		{
			return LevelEditor.Instance.winCondition.GetTeamProgress(team);
		}

		public static System.Collections.Generic.Dictionary<MPTeam, float> GetCurrentProgress()
		{
			MPTeam[] source = new MPTeam[5]
			{
				MPTeam.None,
				MPTeam.Blue,
				MPTeam.Green,
				MPTeam.Orange,
				MPTeam.Red
			};
			return source.ToDictionary((MPTeam t) => t, (MPTeam t) => GetCurrentProgress(t));
		}

		public static bool BlockEntityRaycast(Ray ray, out RaycastHit hit, float distance)
		{
			bool flag = Physics.Raycast(ray, out hit, distance, BlockEntityLayerMask);
			return !StatMaster.hudOccluding && flag;
		}

		public static bool BlockEntityMouseRaycast(out RaycastHit hit, float distance)
		{
			return BlockEntityRaycast(Camera.main.ScreenPointToRay(InputManager.CursorPosition()), out hit, distance);
		}
	}
}
