using System;
using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using TMPro;
using UnityEngine;

namespace TFBGames
{
	public class LocalMultiplayerUIController : MonoBehaviour, ILocalMultiplayerUI
	{
		[Header("Player Names UI")]
		[SerializeField]
		private Color noneColor;

		[SerializeField]
		private Color waitingColor;

		[SerializeField]
		private Color buildingColor;

		[SerializeField]
		private Color readyColor;

		[Header("Timer UI")]
		[SerializeField]
		private TextMeshProUGUI timerText;

		[SerializeField]
		private Color redColor;

		[SerializeField]
		private Color blueColor;

		private const string FlipColors = "GAMEPLAY_FLIP_COLORS";

		private const string Waiting = "MP_LABEL_WAITING";

		private const string Building = "MP_LABEL_BUILDING_ARMY";

		private const string Ready = "MP_LABEL_READY";

		private LocalMultiplayerGameMode gameMode;

		private SettingsInstance flipColorsSetting;

		private bool isTimedPlacement;

		private Team currentTeam;

		private Color currentColor;

		private readonly Dictionary<int, PlayerProfile> profiles = new Dictionary<int, PlayerProfile>(2);

		private Dictionary<int, string> text;

		private void Awake()
		{
			if (ServiceLocator.GetService<GameModeService>().CurrentGameMode is LocalMultiplayerGameMode localMultiplayerGameMode)
			{
				gameMode = localMultiplayerGameMode;
				gameMode.SetLocalMultiplayerUI(this);
				text = new Dictionary<int, string>
				{
					{
						0,
						GetLocalizedColoredString(string.Empty, noneColor)
					},
					{
						1,
						GetLocalizedColoredString("MP_LABEL_WAITING", waitingColor)
					},
					{
						2,
						GetLocalizedColoredString("MP_LABEL_BUILDING_ARMY", buildingColor)
					},
					{
						3,
						GetLocalizedColoredString("MP_LABEL_READY", readyColor)
					}
				};
				flipColorsSetting = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_FLIP_COLORS");
				flipColorsSetting.OnValueChanged += OnColorsChangedPlacementMode;
				currentColor = ((flipColorsSetting.currentValue == 1) ? blueColor : redColor);
				isTimedPlacement = gameMode.IsTimedPlacement;
				if (timerText != null)
				{
					timerText.enabled = isTimedPlacement;
				}
			}
			else
			{
				UnityEngine.Object.Destroy(this);
			}
		}

		private void OnDestroy()
		{
			if (flipColorsSetting != null)
			{
				flipColorsSetting.OnValueChanged -= OnColorsChangedPlacementMode;
			}
		}

		public void SetCurrentTeam(Team team)
		{
			currentTeam = team;
			if (flipColorsSetting.currentValue == 1)
			{
				currentColor = ((currentTeam == Team.Red) ? blueColor : redColor);
			}
			else
			{
				currentColor = ((currentTeam == Team.Red) ? redColor : blueColor);
			}
		}

		public void SetPlayerProfile(Player player, PlayerProfile profile)
		{
			profiles[(int)player] = profile;
		}

		public void SetPlayerStatus(Player player, LocalMultiplayerPlayerStatus status)
		{
			profiles[(int)player].SetStatus(text[(int)status]);
		}

		public void UpdateGameState(GameState state)
		{
			if (!(timerText == null) && isTimedPlacement)
			{
				switch (state)
				{
				case GameState.PlacementState:
					timerText.enabled = true;
					break;
				case GameState.BattleState:
					timerText.enabled = false;
					break;
				default:
					throw new ArgumentOutOfRangeException("state", state, null);
				}
			}
		}

		public void UpdateTime(float time)
		{
			if (!(timerText == null))
			{
				time = Mathf.Clamp(time, 0f, time);
				timerText.text = GetColoredString(time.ToString("F1"), currentColor);
			}
		}

		private void OnColorsChangedPlacementMode(int swapValue)
		{
			SetCurrentTeam(currentTeam);
		}

		private static string GetLocalizedColoredString(string key, Color color)
		{
			return GetColoredString(Localizer.GetSinglePhrase(key), color);
		}

		private static string GetColoredString(string text, Color color)
		{
			return "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + text + "</color>";
		}
	}
}
