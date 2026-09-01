using System;
using System.Collections;
using System.Collections.Generic;
using InternalModding;
using Modding.Blocks;
using Modding.Common;
using Modding.Levels;
using UnityEngine.SceneManagement;

namespace Modding
{
	public class Events : SingleInstance<Events>
	{
		public override string Name
		{
			get
			{
				return "Events";
			}
		}

		public static event Action<bool> OnSimulationToggle;

		public static event Action<PlayerMachine, bool> OnMachineSimulationToggle;

		public static event Action<List<MPTeam>> OnLevelWonMP;

		public static event Action<MPTeam, float> OnProgressAddedMP;

		public static event Action<LogicChain> OnLogicChainTriggered;

		public static event Action<LogicEvent> OnLogicEventExecuted;

		public static event Action<Block> OnBlockPlaced;

		public static event Action<Block> OnBlockInit;

		public static event Action<Block> OnBlockRemoved;

		public static event Action<Block> OnSurfaceCollidersGenated;

		public static event Action<Entity> OnEntityPlaced;

		public static event Action<Entity> OnEntityRemoved;

		public static event Action<PlayerMachineInfo> OnMachineSave;

		public static event Action<PlayerMachineInfo> OnMachineLoaded;

		public static event Action<Level> OnLevelSave;

		public static event Action<Level> OnLevelLoaded;

		public static event Action<LevelSetup> OnLevelSetupChanged;

		public static event Action OnMachineDestroyed;

		public static event Action OnLevelDeleted;

		public static event Action OnConnect;

		public static event Action OnDisconnect;

		public static event Action<Player> OnPlayerJoin;

		public static event Action<Player> OnPlayerLeave;

		public static event Action OnSceneWithModsLoaded;

		public static event Action OnSceneWithModsCameraInitialised;

		public static event Action<Scene, Scene> OnActiveSceneChanged;

		internal void LevelWon(List<MPTeam> winningTeams)
		{
			if (StatMaster.isMP)
			{
				ModdingUtil.PerformCallback(Events.OnLevelWonMP, winningTeams);
			}
		}

		internal void ProgressAdded(MPTeam team, float percentage)
		{
			ModdingUtil.PerformCallback(Events.OnProgressAddedMP, team, percentage);
		}

		internal void SurfaceCollidersGenated(BuildSurface block)
		{
			ModdingUtil.PerformCallback(Events.OnSurfaceCollidersGenated, Block.From(block));
		}

		internal void CollidersChanged(BlockBehaviour block)
		{
			ModdingUtil.PerformCallback(Events.OnSurfaceCollidersGenated, Block.From(block));
		}

		internal void BlockPlaced(BlockBehaviour block)
		{
			if (!(block.ParentMachine != Machine.Active()))
			{
				ModdingUtil.PerformCallback(Events.OnBlockPlaced, Block.From(block));
			}
		}

		internal void BlockInit(BlockBehaviour block)
		{
			ModdingUtil.PerformCallback(Events.OnBlockInit, Block.From(block));
		}

		internal void BlockRemoving(BlockBehaviour block)
		{
			ModdingUtil.PerformCallback(Events.OnBlockRemoved, Block.From(block));
		}

		internal void EntityPlaced(LevelEntity entity)
		{
			ModdingUtil.PerformCallback(Events.OnEntityPlaced, Entity.From(entity));
		}

		internal void EntityRemoving(LevelEntity entity)
		{
			ModdingUtil.PerformCallback(Events.OnEntityRemoved, Entity.From(entity));
		}

		internal void LevelSetupChanged(LevelSetup setup)
		{
			ModdingUtil.PerformCallback(Events.OnLevelSetupChanged, setup);
		}

		internal void MachineDestroyed()
		{
			ModdingUtil.PerformCallback(Events.OnMachineDestroyed);
		}

		internal void LevelDeleted()
		{
			ModdingUtil.PerformCallback(Events.OnLevelDeleted);
		}

		internal void ChainTriggered(EntityLogic logic)
		{
			ModdingUtil.PerformCallback(Events.OnLogicChainTriggered, LogicChain.From(logic));
		}

		internal void EventExecuted(EntityLogic logic, EntityEvent evt)
		{
			if (!StatMaster.isClient || StatMaster.isLocalSim)
			{
				ModdingUtil.PerformCallback(Events.OnLogicEventExecuted, LogicEvent.From(evt, LogicChain.From(logic)));
			}
		}

		internal void ModSceneLoaded()
		{
			ModdingUtil.PerformCallback(Events.OnSceneWithModsLoaded);
		}

		internal void CameraInitFinished()
		{
			ModdingUtil.PerformCallback(Events.OnSceneWithModsCameraInitialised);
		}

		private void Awake()
		{
			SingleInstance<Events>.Initialize(this);
		}

		public override void SetUp()
		{
			ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggled));
			XmlLoader.OnLoad += delegate(MachineInfo machine)
			{
				ModdingUtil.PerformCallback(Events.OnMachineLoaded, PlayerMachineInfo.From(machine));
			};
			XmlSaver.OnSave += delegate(MachineInfo machine)
			{
				ModdingUtil.PerformCallback(Events.OnMachineSave, PlayerMachineInfo.From(machine));
			};
			LevelXMLLoader.OnLoaded += delegate(LevelEditor editor)
			{
				ModdingUtil.PerformCallback(Events.OnLevelLoaded, Level.From(editor));
			};
			LevelXMLSaver.OnSave += delegate(LevelEditor editor)
			{
				ModdingUtil.PerformCallback(Events.OnLevelSave, Level.From(editor));
			};
			ReferenceMaster.OnDisconnect += delegate
			{
				ModdingUtil.PerformCallback(Events.OnDisconnect);
			};
			SceneManager.activeSceneChanged += delegate(Scene current, Scene next)
			{
				ModdingUtil.PerformCallback(Events.OnActiveSceneChanged, current, next);
			};
		}

		internal void Connected()
		{
			SingleInstanceFindOnly<ModManager>.Instance.StartCoroutine(InvokeOnConnected());
		}

		private IEnumerator InvokeOnConnected()
		{
			yield return null;
			ModdingUtil.PerformCallback(Events.OnConnect);
		}

		internal void PlayerJoined(ushort playerId)
		{
			ModdingUtil.PerformCallback(Events.OnPlayerJoin, Player.From(playerId));
		}

		internal void PlayerLeave(ushort playerId)
		{
			ModdingUtil.PerformCallback(Events.OnPlayerLeave, Player.From(playerId));
		}

		internal void OnSimulationToggled(bool toggle)
		{
			ModdingUtil.PerformCallback(Events.OnSimulationToggle, toggle);
		}

		internal void MachineSimulate(Machine machine)
		{
			ModdingUtil.PerformCallback(Events.OnMachineSimulationToggle, PlayerMachine.From(machine), true);
		}

		internal void MachineStopSimulate(Machine machine)
		{
			ModdingUtil.PerformCallback(Events.OnMachineSimulationToggle, PlayerMachine.From(machine), false);
		}
	}
}
