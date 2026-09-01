using UnityEngine;

namespace TFBGames
{
	public class BattleRadialButtonIconManager
	{
		private const float MaxIconsToLoadsPerSecond = 10f;

		private const float IconLoadIntervals = 0.1f;

		private const float FirstMaxIconsToLoadsPerSecond = 100f;

		private const float FirstIconLoadIntervals = 0.01f;

		private const int FirstIconsCountMax = 10;

		private BattleRadialButton[] m_Buttons;

		private float loadingStartTime;

		private float loadingEndTime;

		private int totalIconsLoadedCount;

		private RadialMenuShouldDestroyIconTextureCallback shouldDestroyIconTexture;

		public void Initialize(BattleRadialButton[] buttons, RadialMenuShouldDestroyIconTextureCallback shouldDestroyFunc)
		{
			shouldDestroyIconTexture = shouldDestroyFunc;
			Clear();
			m_Buttons = buttons;
			TrySubscribeToButtonEvents(subscribe: true);
		}

		public void Clear()
		{
			TrySubscribeToButtonEvents(subscribe: false);
			m_Buttons = null;
			totalIconsLoadedCount = 0;
		}

		private void TrySubscribeToButtonEvents(bool subscribe)
		{
			if (m_Buttons == null || m_Buttons.Length == 0)
			{
				return;
			}
			int i = 0;
			for (int num = m_Buttons.Length; i < num; i++)
			{
				BattleRadialButton battleRadialButton = m_Buttons[i];
				if (!(battleRadialButton == null))
				{
					battleRadialButton.StartedLoadingIcon -= OnStartedLoadingIcon;
					battleRadialButton.DoneLoadingIcon -= OnDoneLoadingIcon;
					battleRadialButton.SetIconFunctions(null, shouldDestroyIconTexture);
					if (subscribe)
					{
						battleRadialButton.StartedLoadingIcon += OnStartedLoadingIcon;
						battleRadialButton.DoneLoadingIcon += OnDoneLoadingIcon;
						battleRadialButton.SetIconFunctions(CanLoadAnotherIcon, shouldDestroyIconTexture);
					}
				}
			}
		}

		private bool CanLoadAnotherIcon()
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			float a = realtimeSinceStartup - loadingStartTime;
			float b = realtimeSinceStartup - loadingEndTime;
			return Mathf.Min(a, b) > GetIconLoadIntervals();
		}

		private float GetIconLoadIntervals()
		{
			if (totalIconsLoadedCount >= 10)
			{
				return 0.1f;
			}
			return 0.01f;
		}

		private void OnStartedLoadingIcon()
		{
			totalIconsLoadedCount++;
			loadingStartTime = Time.realtimeSinceStartup;
		}

		private void OnDoneLoadingIcon()
		{
			loadingEndTime = Time.realtimeSinceStartup;
		}
	}
}
