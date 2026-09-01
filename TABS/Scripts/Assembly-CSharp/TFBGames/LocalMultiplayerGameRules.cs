using Landfall.TABS.GameMode;
using UnityEngine;

namespace TFBGames
{
	[CreateAssetMenu(menuName = "Services/Multiplayer Game Settings")]
	public class LocalMultiplayerGameRules : ServiceAsset
	{
		public delegate void SettingsChanged();

		[SerializeField]
		private SliderData defaultMaxUnitPoints;

		[SerializeField]
		private bool defaultIsTimerToPlaceUnitsOn;

		[SerializeField]
		private SliderData defaultTimeToPlaceUnits;

		[SerializeField]
		private float defaultSplitScreenMergeDelay;

		[SerializeField]
		private TurnStyle defaultTurnStyle;

		[SerializeField]
		private SplitScreenStyle defaultSplitScreenStyle;

		private SliderData currentMaxUnitPoints;

		private bool currentIsTimerToPlaceUnitsOn;

		private SliderData currentTimeToPlaceUnits;

		private float currentSplitScreenMergeDelay;

		private TurnStyle currentTurnStyle;

		private SplitScreenStyle currentSplitScreenStyle;

		private GameModeService gameModeService;

		public SliderData MaxUnitPoints
		{
			get
			{
				return currentMaxUnitPoints;
			}
			set
			{
				currentMaxUnitPoints = value;
			}
		}

		public bool IsTimerToPlaceUnitsOn
		{
			get
			{
				return currentIsTimerToPlaceUnitsOn;
			}
			set
			{
				currentIsTimerToPlaceUnitsOn = value;
			}
		}

		public SliderData TimeToPlaceUnits
		{
			get
			{
				return currentTimeToPlaceUnits;
			}
			set
			{
				currentTimeToPlaceUnits = value;
			}
		}

		public SplitScreenStyle SplitScreenStyle
		{
			get
			{
				return currentSplitScreenStyle;
			}
			set
			{
				currentSplitScreenStyle = value;
			}
		}

		public TurnStyle TurnStyle
		{
			get
			{
				return currentTurnStyle;
			}
			set
			{
				currentTurnStyle = value;
			}
		}

		public float SplitScreenMergeDelay
		{
			get
			{
				return currentSplitScreenMergeDelay;
			}
			set
			{
				currentSplitScreenMergeDelay = value;
			}
		}

		public event SettingsChanged OnSettingsChanged;

		public override void OnStart()
		{
			base.OnStart();
			gameModeService = ServiceLocator.GetService<GameModeService>();
		}

		public void ApplySettings(LocalMultiplayerSettingsButton turnStyleUIComponent, LocalMultiplayerSettingsButton splitScreenUIComponent, LocalMultiplayerSettingsButton maxUnitPointsUIComponent, LocalMultiplayerSettingsButton timedPlacementUIComponent, LocalMultiplayerSettingsButton timeUIComponent)
		{
			if (!(turnStyleUIComponent == null) && !(splitScreenUIComponent == null) && !(maxUnitPointsUIComponent == null) && !(timedPlacementUIComponent == null) && !(timeUIComponent == null))
			{
				currentTurnStyle = (TurnStyle)turnStyleUIComponent.Index;
				currentSplitScreenStyle = (SplitScreenStyle)splitScreenUIComponent.Index;
				SliderData sliderData = currentMaxUnitPoints;
				sliderData.current = maxUnitPointsUIComponent.SliderData.current;
				currentMaxUnitPoints = sliderData;
				SliderData sliderData2 = currentTimeToPlaceUnits;
				sliderData2.current = timeUIComponent.SliderData.current;
				currentTimeToPlaceUnits = sliderData2;
				currentIsTimerToPlaceUnitsOn = timedPlacementUIComponent.Index == 1;
				if (!(gameModeService == null) && gameModeService.CurrentGameMode.GetType() == typeof(LocalMultiplayerGameMode))
				{
					this.OnSettingsChanged?.Invoke();
					TABSSceneManager.ReloadMap();
				}
			}
		}

		public void ResetGameRulesToDefault()
		{
			currentMaxUnitPoints = defaultMaxUnitPoints;
			currentIsTimerToPlaceUnitsOn = defaultIsTimerToPlaceUnitsOn;
			currentTimeToPlaceUnits = defaultTimeToPlaceUnits;
			currentSplitScreenMergeDelay = defaultSplitScreenMergeDelay;
			currentTurnStyle = defaultTurnStyle;
			currentSplitScreenStyle = defaultSplitScreenStyle;
		}
	}
}
