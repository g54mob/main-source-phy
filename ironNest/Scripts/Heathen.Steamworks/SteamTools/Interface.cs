using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Heathen.SteamworksIntegration;
using UnityEngine;

namespace SteamTools
{
	public static class Interface
	{
		private static Dictionary<string, LeaderboardData> _boards;

		private static Dictionary<string, InputActionSetData> _sets;

		private static Dictionary<string, InputActionData> _actions;

		private static List<Action> _whenReadyCalls;

		public static bool IsInitialised => false;

		public static bool IsDebugging
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public static bool IsReady { get; private set; }

		public static event Action OnReady
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public static event Action<string> OnInitialisationError
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Init()
		{
		}

		public static void Initialise()
		{
		}

		public static void WhenReady(Action callback)
		{
		}

		private static void HandleInitialisedError(string arg0)
		{
		}

		public static void RaiseOnReady(Dictionary<string, LeaderboardData> boardMap, Dictionary<string, InputActionSetData> setMap, Dictionary<string, InputActionData> actionMap)
		{
		}

		public static void AddBoard(LeaderboardData board)
		{
		}

		public static LeaderboardData GetBoard(string name)
		{
			return default(LeaderboardData);
		}

		public static LeaderboardData[] GetBoards()
		{
			return null;
		}

		public static InputActionSetData GetSet(string name)
		{
			return default(InputActionSetData);
		}

		public static InputActionData GetAction(string name)
		{
			return default(InputActionData);
		}
	}
}
