using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InternalModding.Assemblies;
using InternalModding.Blocks;
using InternalModding.Events;
using InternalModding.LevelEntities;
using InternalModding.Loading;
using InternalModding.Loading.Sources;
using InternalModding.Misc;
using InternalModding.Mods;
using InternalModding.Triggers;
using Modding;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InternalModding
{
	public class ModManager : SingleInstanceFindOnly<ModManager>
	{
		private enum State
		{
			Initializing = 0,
			ActiveInTitle = 1,
			ActiveInTitleKeepMP = 2,
			ActiveInTitleKeepMPSP = 3,
			ActiveMP = 4,
			ActiveMPSP = 5
		}

		public int BlockIdStart;

		public int EntityIdStart;

		public int TriggerIdStart;

		public Texture2D NoThumbnailTexture;

		private IModSource[] ModSources;

		private IComponentProvider[] ComponentProviders;

		private State CurrentState;

		public static List<ModContainer> Mods = new List<ModContainer>();

		public override string Name
		{
			get
			{
				return "ModMaster";
			}
		}

		public static string DefaultModPath { get; private set; }

		public static bool DisableMultiplayer
		{
			get
			{
				return SingleInstanceFindOnly<ModManager>.Instance.CurrentState == State.ActiveMPSP;
			}
		}

		public static event Action<ModContainer> OnPreModLoad;

		public static event Action<ModContainer> OnModLoad;

		public static event Action OnInitialModsLoaded;

		public override void SetUp()
		{
			setUp = true;
			ComponentProviders = new IComponentProvider[6]
			{
				SingleInstanceFindOnly<ModdingInitializer>.Instance,
				SingleInstanceFindOnly<TriggerLoader>.Instance,
				SingleInstanceFindOnly<EventLoader>.Instance,
				SingleInstanceFindOnly<AssemblyLoader>.Instance,
				SingleInstanceFindOnly<BlockLoader>.Instance,
				SingleInstanceFindOnly<EntityLoader>.Instance
			};
			ModSources = new IModSource[3]
			{
				new DefaultModSource(),
				new AdditionalDirsModSource(),
				new WorkshopModSource()
			};
			CurrentState = State.Initializing;
			DefaultModPath = StaticSettings.DataPath + "/Mods/";
			SceneManager.sceneLoaded += OnSceneLoaded;
			Modding.Events.OnConnect += delegate
			{
				if (StatMaster.isHosting)
				{
					UpdateState(State.ActiveMP, true);
				}
				SingleInstanceFindOnly<BlockLoader>.Instance.CreateUI();
				SingleInstance<Modding.Events>.Instance.ModSceneLoaded();
			};
			Modding.Events.OnDisconnect += ModStatus.ReloadBlockEntityHideStatus;
		}

		public void OnJoin()
		{
			UpdateState(State.ActiveMP, true);
		}

		public void Start()
		{
			Action<DirectoryInfo, Action<ModContainer>> registerMod = delegate(DirectoryInfo dir, Action<ModContainer> callback)
			{
				ModContainer mod = SingleInstanceFindOnly<ModLoader>.Instance.LoadModInfoFrom(dir);
				if (mod != null)
				{
					mod.CurrentState = ModContainer.State.Loaded;
					if (callback != null)
					{
						callback(mod);
					}
					if (ModManager.OnPreModLoad != null)
					{
						ModManager.OnPreModLoad(mod);
					}
					bool flag = CallTitleProviders((IComponentProvider p) => p.LoadMod(mod));
					SingleInstanceFindOnly<ModLoader>.Instance.PostLoadMod(mod);
					if (!flag)
					{
						mod.HadLoadOrActivateErrors = true;
					}
					Mods.Add(mod);
					if (mod.IsEnabled && mod.Info.DebugEnabled && !OptionsMaster.BesiegeConfig.ShowDebugLogs)
					{
						OptionsMaster.BesiegeConfig.ShowDebugLogs = true;
						MLog.Info("Enabled log output to console because a mod with Debug enabled was loaded.\nCan be disabled with 'show_logs false'.");
					}
					if (ModManager.OnModLoad != null)
					{
						ModManager.OnModLoad(mod);
					}
				}
			};
			int sourcesLeft = ModSources.Length;
			Action allModsFound = delegate
			{
				sourcesLeft--;
				if (sourcesLeft == 0)
				{
					UpdateState(CurrentState);
					if (ModManager.OnInitialModsLoaded != null)
					{
						ModManager.OnInitialModsLoaded();
					}
				}
			};
			if (BesiegeLogFilter.logDev)
			{
				Debug.LogFormat("[ModManager::Start] Loading mod sources from {0} sources", ModSources.Length);
			}
			IModSource[] modSources = ModSources;
			foreach (IModSource modSource in modSources)
			{
				if (BesiegeLogFilter.logDev)
				{
					Debug.LogFormat("[ModManager::Start] Loading mod source: {0}", modSource.GetType().Name);
				}
				modSource.GetMods(registerMod, allModsFound);
			}
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			if (scene.name.Equals("TITLE SCREEN"))
			{
				UpdateState(State.ActiveInTitle);
			}
			else if (!AddPiece.IsMenuScene(scene.name) && !scene.name.Equals("INITIALISER") && !scene.name.Equals("CREDITS") && !scene.name.Contains("Multiplayer"))
			{
				UpdateState(State.ActiveMPSP, true);
				SingleInstanceFindOnly<BlockLoader>.Instance.CreateUI();
				SingleInstance<Modding.Events>.Instance.ModSceneLoaded();
			}
		}

		public void OnApplicationQuit()
		{
			ModKeys.SaveKeysFor(Mods);
			Configuration.SaveAll();
		}

		public void RecalculateState()
		{
			UpdateState(CurrentState);
		}

		private void UpdateState(State newState, bool overrideInGameDetection = false)
		{
			if (newState == State.Initializing)
			{
				return;
			}
			State currentState = CurrentState;
			if (newState == State.ActiveInTitle)
			{
				if (CurrentState == State.Initializing)
				{
					CurrentState = State.ActiveInTitle;
				}
				else if (CurrentState == State.ActiveMP)
				{
					CurrentState = State.ActiveInTitleKeepMP;
				}
				else if (CurrentState == State.ActiveMPSP)
				{
					CurrentState = State.ActiveInTitleKeepMPSP;
				}
			}
			else
			{
				CurrentState = newState;
			}
			if (ModdingUtil.IsInGame() && !overrideInGameDetection)
			{
				return;
			}
			List<ModContainer> list = new List<ModContainer>();
			list.AddRange(Mods.Where((ModContainer m) => m.IsActive));
			list.AddRange(Mods.Where((ModContainer m) => m.Info.LoadInTitleScreen));
			if (CurrentState == State.ActiveMP)
			{
				list.AddRange(Mods.Where((ModContainer m) => m.Info.MultiplayerCompatible && !m.Info.LoadInTitleScreen));
			}
			else if (CurrentState == State.ActiveMPSP)
			{
				list.AddRange(Mods.Where((ModContainer m) => !m.Info.LoadInTitleScreen));
			}
			list = list.Where((ModContainer m) => m.IsEnabled || m.IsActive).ToList();
			if (CurrentState == State.ActiveInTitleKeepMP || CurrentState == State.ActiveInTitleKeepMPSP)
			{
				list = list.Where((ModContainer m) => m.Info.LoadInTitleScreen || m.IsActive).ToList();
			}
			if (CurrentState == State.ActiveMPSP && list.All((ModContainer m) => m.Info.MultiplayerCompatible))
			{
				CurrentState = State.ActiveMP;
			}
			if (CurrentState == currentState && list.All((ModContainer m) => m.CurrentState == ModContainer.State.Active))
			{
				return;
			}
			list = list.Distinct().ToList();
			ModContainer mod;
			foreach (ModContainer item in list.Where((ModContainer m) => m.CurrentState == ModContainer.State.Active))
			{
				mod = item;
				mod.CurrentState = ModContainer.State.ActiveUnregistered;
				CallTitleProviders(delegate(IComponentProvider p)
				{
					p.UnregisterPrefabs(mod);
				});
			}
			ModIds.AssignIds(list);
			ModContainer mod2;
			foreach (ModContainer item2 in list.OrderBy((ModContainer m) => m.Info.LoadOrder))
			{
				mod2 = item2;
				if (mod2.CurrentState == ModContainer.State.Loaded)
				{
					mod2.loadedMP = CurrentState == State.ActiveMP;
					if (!CallProviders((IComponentProvider p) => p.ActivateMod(mod2)))
					{
						mod2.HadLoadOrActivateErrors = true;
					}
					mod2.CurrentState = ModContainer.State.ActiveUnregistered;
				}
				else if (CurrentState == State.ActiveMP && !mod2.loadedMP)
				{
					mod2.loadedMP = true;
					if (!CallMPProviders((IComponentProvider p) => p.ActivateMod(mod2)))
					{
						mod2.HadLoadOrActivateErrors = true;
					}
					mod2.CurrentState = ModContainer.State.ActiveUnregistered;
					CallMPProviders(delegate(IComponentProvider p)
					{
						p.RegisterPrefabs(mod2);
					});
				}
				CallProviders(delegate(IComponentProvider p)
				{
					p.RegisterPrefabs(mod2);
				});
				mod2.CurrentState = ModContainer.State.Active;
			}
			CallProviders(delegate(IComponentProvider p)
			{
				p.PostRegisterPrefabs();
			});
		}

		private void CallProviders(Action<IComponentProvider> call)
		{
			IEnumerable<IComponentProvider> enumerable = ComponentProviders;
			if (CurrentState != State.ActiveMP)
			{
				enumerable = enumerable.Where((IComponentProvider p) => p.ActiveInSingleplayer);
			}
			CallProviders(enumerable, call);
		}

		private bool CallProviders(Predicate<IComponentProvider> call)
		{
			IEnumerable<IComponentProvider> enumerable = ComponentProviders;
			if (CurrentState != State.ActiveMP)
			{
				enumerable = enumerable.Where((IComponentProvider p) => p.ActiveInSingleplayer);
			}
			return CallProviders(enumerable, call);
		}

		private void CallTitleProviders(Action<IComponentProvider> call)
		{
			IEnumerable<IComponentProvider> enumerable = ComponentProviders;
			if (CurrentState == State.ActiveMPSP)
			{
				enumerable = enumerable.Where((IComponentProvider p) => p.ActiveInSingleplayer);
			}
			CallProviders(enumerable, call);
		}

		private bool CallTitleProviders(Predicate<IComponentProvider> call)
		{
			IEnumerable<IComponentProvider> enumerable = ComponentProviders;
			if (CurrentState == State.ActiveMPSP)
			{
				enumerable = enumerable.Where((IComponentProvider p) => p.ActiveInSingleplayer);
			}
			return CallProviders(enumerable, call);
		}

		private void CallMPProviders(Action<IComponentProvider> call)
		{
			IEnumerable<IComponentProvider> activeProviders = ComponentProviders.Where((IComponentProvider p) => !p.ActiveInSingleplayer);
			CallProviders(activeProviders, call);
		}

		private bool CallMPProviders(Predicate<IComponentProvider> call)
		{
			IEnumerable<IComponentProvider> activeProviders = ComponentProviders.Where((IComponentProvider p) => !p.ActiveInSingleplayer);
			return CallProviders(activeProviders, call);
		}

		private void CallProviders(IEnumerable<IComponentProvider> activeProviders, Action<IComponentProvider> call)
		{
			foreach (IComponentProvider activeProvider in activeProviders)
			{
				call(activeProvider);
			}
		}

		private bool CallProviders(IEnumerable<IComponentProvider> activeProviders, Predicate<IComponentProvider> call)
		{
			bool flag = true;
			foreach (IComponentProvider activeProvider in activeProviders)
			{
				flag &= call(activeProvider);
			}
			return flag;
		}
	}
}
