using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TFBGames
{
	public class SplitScreenHandler
	{
		private readonly float splitScreenMergeDelayTotal;

		private readonly PlayerActions[] lmpPlayerActions;

		private readonly List<IPossessController> possessControllers;

		private readonly SplitScreenController splitScreenController;

		private bool isAPlayerPossessing;

		private float currentSplitScreenMergeDelay;

		public SplitScreenHandler(float splitScreenMergeDelayTotal, PlayerActions[] lmpPlayerActions, List<IPossessController> possessControllers, SplitScreenController splitScreenController)
		{
			this.splitScreenMergeDelayTotal = splitScreenMergeDelayTotal;
			this.lmpPlayerActions = lmpPlayerActions;
			this.possessControllers = possessControllers;
			this.splitScreenController = splitScreenController;
			SceneManager.sceneLoaded += OnSceneLoaded;
			RegisterPossessUpdates();
		}

		private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
		{
			RegisterPossessUpdates();
		}

		private void RegisterPossessUpdates()
		{
			foreach (IPossessController possessController in possessControllers)
			{
				if (possessController != null)
				{
					possessController.OnPossessUpdate += HandleOnPossessUpdate;
				}
			}
		}

		public void Cleanup()
		{
			foreach (IPossessController possessController in possessControllers)
			{
				if (possessController != null)
				{
					possessController.OnPossessUpdate -= HandleOnPossessUpdate;
				}
			}
		}

		public void EndSplitScreen()
		{
			if (splitScreenController.IsSplitScreenActive)
			{
				splitScreenController.EndSplitScreen();
			}
			isAPlayerPossessing = false;
			currentSplitScreenMergeDelay = 0f;
		}

		public void Update(PlayerActions playerActions)
		{
			bool flag = HasNotableInput(Player.One);
			bool flag2 = HasNotableInput(Player.Two);
			if (!splitScreenController.IsSplitScreenActive)
			{
				if (playerActions.m_possessToggle.WasPressed)
				{
					splitScreenController.StartSplitScreen();
				}
				if (flag && flag2)
				{
					splitScreenController.StartSplitScreen();
				}
				currentSplitScreenMergeDelay = 0f;
			}
			else if (!isAPlayerPossessing && ((!flag && !flag2) || flag != flag2))
			{
				currentSplitScreenMergeDelay += Time.deltaTime;
				if (currentSplitScreenMergeDelay > splitScreenMergeDelayTotal)
				{
					splitScreenController.EndSplitScreen();
				}
			}
			else
			{
				currentSplitScreenMergeDelay = 0f;
			}
		}

		private void HandleOnPossessUpdate(bool possesses)
		{
			isAPlayerPossessing = false;
			foreach (IPossessController possessController in possessControllers)
			{
				isAPlayerPossessing |= possessController.IsPossessing;
			}
		}

		private bool HasNotableInput(Player player)
		{
			PlayerActions playerActions = lmpPlayerActions[(int)player];
			if (!playerActions.m_aim.State && !playerActions.m_move.State)
			{
				return playerActions.m_moveVertical.State;
			}
			return true;
		}
	}
}
