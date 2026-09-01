using System;
using UnityEngine;

namespace GameGrind
{
	[Serializable]
	public class Achievement
	{
		public int id;

		public int title;

		public Sprite icon;

		public string iconPath;

		public int description;

		public int value;

		public int neededValue;

		public bool displayAsPercentage;

		public int points;

		public bool completed;

		public bool secret;

		public Achievement(int id, int title, Sprite icon, int value, int description, int neededValue, bool displayAsPercentage, int points, bool secret, string iconPath = "")
		{
			this.id = id;
			this.title = title;
			this.iconPath = iconPath;
			if (iconPath != string.Empty)
			{
				this.icon = Resources.Load<Sprite>(iconPath);
			}
			else
			{
				this.icon = icon;
			}
			Debug.Log(iconPath);
			this.value = value;
			this.description = description;
			this.neededValue = neededValue;
			this.displayAsPercentage = displayAsPercentage;
			this.points = points;
			completed = value >= neededValue;
			this.secret = secret;
		}

		public Achievement()
		{
			id = Journal.achievementMaster.Count + 1;
			title = 0;
			icon = null;
			iconPath = string.Empty;
			value = 0;
			description = 0;
			neededValue = 25;
			points = 10;
			completed = value >= neededValue;
			secret = false;
		}
	}
}
