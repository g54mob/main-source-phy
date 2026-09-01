using UnityEngine;

namespace TFBGames
{
	[CreateAssetMenu(menuName = "Services/Project Mars Game Settings")]
	public class ProjectMarsGameSettingsAsset : ServiceAsset
	{
		public enum TurnStyle
		{
			Normal = 0,
			Blind = 1
		}

		[SerializeField]
		private SliderData defaultGameBudget;

		[SerializeField]
		private SliderData defaultUnitCap;

		[SerializeField]
		private TurnStyle defaultProjectMarsTurnStyle;

		private SliderData unitCap;

		private SliderData gameBudget;

		private TurnStyle projectMarsTurnStyle;

		private SettingsProfileManager settingsProfileManager;

		public SliderData UnitCap
		{
			get
			{
				return unitCap;
			}
			set
			{
				unitCap = value;
			}
		}

		public SliderData GameBudget
		{
			get
			{
				return gameBudget;
			}
			set
			{
				gameBudget = value;
			}
		}

		public TurnStyle ProjectMarsTurnStyle
		{
			get
			{
				return projectMarsTurnStyle;
			}
			set
			{
				projectMarsTurnStyle = value;
			}
		}

		public override void OnRegister()
		{
			base.OnRegister();
			ResetSettings();
		}

		public override void OnStart()
		{
			base.OnStart();
			settingsProfileManager = ServiceLocator.GetService<SettingsProfileManager>();
			if (settingsProfileManager.CurrentSettingsProfile.MultiplayerMaxUnits.HasValue)
			{
				defaultUnitCap.max = settingsProfileManager.CurrentSettingsProfile.MultiplayerMaxUnits.Value;
			}
			settingsProfileManager.CurrentSettingsProfile.CurrentMartians = (int)defaultUnitCap.current;
		}

		public void ResetSettings()
		{
			unitCap = defaultUnitCap;
			projectMarsTurnStyle = defaultProjectMarsTurnStyle;
			gameBudget = defaultGameBudget;
		}
	}
}
