using System;
using Landfall.TABS.AI.Systems;
using Landfall.TABS.GameMode;
using Newtonsoft.Json;
using UnityEngine;

namespace Landfall.TABS.WinConditions
{
	public abstract class WinCondition
	{
		private static Color BLUE_COLOR = new Color(59f / 85f, 0.77254903f, 1f, 1f);

		private static Color RED_COLOR = new Color(14f / 15f, 22f / 51f, 0.4f, 1f);

		private static Color TIMER_COLOR = new Color(1f, 64f / 85f, 1f / 3f, 1f);

		private static Color WHITE_COLOR = new Color(1f, 1f, 1f, 1f);

		public Guid Guid { get; set; }

		[JsonIgnore]
		public BaseGameMode GameMode { get; set; }

		[JsonIgnore]
		public TeamSystem TeamSystem { get; set; }

		[JsonIgnore]
		public Team OwningTeam { get; set; }

		public abstract void Reset();

		public abstract string GetDescription(out string[] args);

		public abstract string GetBattleDescription(bool invertDescription);

		public abstract string GetDescriptionForListItem(out string[] args);

		public abstract string GetWinDescription();

		public abstract void OnUnitDied(Unit unit);

		public abstract ReferenceRequest<Unit> GetUnitToKill();

		public abstract void OnUpdate();

		public virtual void OnMatchOver()
		{
		}

		public virtual void OnRemovedFromgGUI()
		{
		}

		protected string GetColoredText(Team team)
		{
			SettingsInstance settingsInstance = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_FLIP_COLORS");
			Color color = Color.white;
			switch (team)
			{
			case Team.Red:
				color = ((settingsInstance.currentValue == 0) ? RED_COLOR : BLUE_COLOR);
				break;
			case Team.Blue:
				color = ((settingsInstance.currentValue == 0) ? BLUE_COLOR : RED_COLOR);
				break;
			}
			return "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">";
		}

		protected string GetTeamText(Team team)
		{
			SettingsInstance settingsInstance = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_FLIP_COLORS");
			string singlePhrase = Localizer.GetSinglePhrase("LABEL_BLUE");
			string singlePhrase2 = Localizer.GetSinglePhrase("LABEL_RED");
			string text = ((team == Team.Blue) ? singlePhrase : singlePhrase2);
			if (settingsInstance.currentValue == 1)
			{
				if (text == singlePhrase2)
				{
					text = singlePhrase;
				}
				else if (text == singlePhrase)
				{
					text = singlePhrase2;
				}
			}
			return text;
		}

		protected string GetTimerColor()
		{
			return "<color=#" + ColorUtility.ToHtmlStringRGB(TIMER_COLOR) + ">";
		}

		protected string GetWhiteColor()
		{
			return "<color=#" + ColorUtility.ToHtmlStringRGB(WHITE_COLOR) + ">";
		}
	}
}
