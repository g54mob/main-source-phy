using System;
using System.IO;
using InternalModding.Mods;
using Modding;
using UnityEngine;

namespace InternalModding.Loading
{
	public class ModdingInitializer : SingleInstanceFindOnly<ModdingInitializer>, IComponentProvider
	{
		public override string Name
		{
			get
			{
				return "ModMaster";
			}
		}

		public bool ActiveInSingleplayer
		{
			get
			{
				return true;
			}
		}

		public override void SetUp()
		{
			setUp = true;
			if (!UnityMainThreadDispatcher.Exists())
			{
				GameObject gameObject = new GameObject("MainThreadDispatcher");
				gameObject.AddComponent<UnityMainThreadDispatcher>();
			}
			SetEnvVars();
		}

		public void Start()
		{
			Configuration.LoadModLoader();
			ModConsole.Initialize();
			Maintenance.Initialize();
			SingleInstance<Modding.Events>.Initialize();
			ModNetworking.Initialize();
			CompatibilityChecker.Initialize();
			ModReloading.Initialize();
			Game.BlockEntityLayerMask = ReferenceMaster.Instance.levelEditorMask;
		}

		public void OnApplicationQuit()
		{
			ModReloading.OnQuit();
		}

		private void SetEnvVars()
		{
			string text = Path.Combine(Application.dataPath, "Managed/");
			string value = text;
			Environment.SetEnvironmentVariable("BESIEGE_GAME_ASSEMBLIES", text, EnvironmentVariableTarget.User);
			Environment.SetEnvironmentVariable("BESIEGE_UNITY_ASSEMBLIES", value, EnvironmentVariableTarget.User);
		}

		public bool LoadMod(ModContainer mod)
		{
			Configuration.Load(mod);
			ModKeys.Load(mod);
			return true;
		}

		public bool ActivateMod(ModContainer mod)
		{
			return true;
		}

		public void RegisterPrefabs(ModContainer mod)
		{
		}

		public void PostRegisterPrefabs()
		{
		}

		public void UnregisterPrefabs(ModContainer mod)
		{
		}
	}
}
