using GameGrind;
using Localisation;
using UnityEngine;

public class ObjectiveTrackerUI : MonoBehaviour
{
	public TextMesh intactText;

	public TextMesh diyText;

	public TextMesh achievementText;

	public TextMesh intactTooltip;

	public TextMesh diyTooltip;

	public TextMesh achievementTooltip;

	public int[] levels = new int[0];

	private static Color objectiveColor = Color.black;

	private void Start()
	{
		SetStats(levels);
	}

	internal static void ToggleObjectives(int myIndex, MeshRenderer intact, MeshRenderer diy, MeshRenderer achievement)
	{
		bool cleared = false;
		bool available = LevelHasAchievement(myIndex, out cleared);
		int objectiveState = LevelObjectiveFileManager.GetObjectiveState(myIndex);
		bool toggle = (objectiveState & 1) != 0;
		bool toggle2 = (objectiveState & 2) != 0;
		ToggleObjective(intact, toggle2, true, 0f);
		ToggleObjective(diy, toggle, true, 0f);
		ToggleObjective(achievement, cleared, available, 0.106f);
	}

	internal static void ToggleObjective(MeshRenderer icon, bool toggle, bool available, float offset)
	{
		if (icon == null)
		{
			return;
		}
		if (objectiveColor.r == 0f)
		{
			objectiveColor = icon.sharedMaterial.GetColor("_TintColor");
			objectiveColor.a = 0.5f;
		}
		Color color = objectiveColor;
		if (!available)
		{
			icon.gameObject.SetActive(false);
			icon.transform.parent.localPosition += Vector3.right * offset;
		}
		else
		{
			icon.gameObject.SetActive(true);
			if (!toggle)
			{
				color = new Color(0.2f, 0.2f, 0.2f, color.a * 0.4f) + color * 0.1f;
			}
		}
		icon.material.SetColor("_TintColor", color);
	}

	internal static bool LevelHasAchievement(int index)
	{
		bool cleared;
		return LevelHasAchievement(index, out cleared);
	}

	internal static bool LevelHasAchievement(int index, out bool cleared)
	{
		if (LevelAchievementTrigger.levelAchievements.ContainsKey(index))
		{
			cleared = LevelAchievementTrigger.levelAchievements[index].Completed();
			return true;
		}
		cleared = false;
		return false;
	}

	internal static string GetAchievementDescription(int index, int maxWidth = 30)
	{
		string empty = string.Empty;
		if (LevelAchievementTrigger.levelAchievements.ContainsKey(index))
		{
			empty = LocalisationManager.GetTranslation(Journal.GetAchievement(LevelAchievementTrigger.levelAchievements[index].AchievementId).description);
			if (string.IsNullOrEmpty(empty))
			{
				return "N/A";
			}
			if (empty.Length < maxWidth)
			{
				return empty;
			}
			empty = empty.Replace("\n", " ");
			string[] array = empty.Split(' ');
			empty = array[0];
			int num = array[0].Length;
			for (int i = 1; i < array.Length; i++)
			{
				int num2 = array[i].Length + 1;
				if (num + num2 < maxWidth)
				{
					empty = empty + " " + array[i];
					num += num2;
				}
				else
				{
					empty = empty + "\n" + array[i];
					num = array[i].Length;
				}
			}
			return empty;
		}
		return empty;
	}

	internal void SetStats(int[] levels)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		for (int i = 0; i < levels.Length; i++)
		{
			bool cleared = false;
			if (LevelHasAchievement(levels[i], out cleared))
			{
				num2++;
				if (cleared)
				{
					num++;
				}
			}
			int objectiveState = LevelObjectiveFileManager.GetObjectiveState(levels[i]);
			if ((objectiveState & 1) != 0)
			{
				num4++;
			}
			if ((objectiveState & 2) != 0)
			{
				num3++;
			}
		}
		ShowStat(achievementText, num, num2);
		ShowStat(intactText, num3, levels.Length);
		ShowStat(diyText, num4, levels.Length);
		ShowTooltip(achievementTooltip, num, num2);
		ShowTooltip(intactTooltip, num3, levels.Length);
		ShowTooltip(diyTooltip, num4, levels.Length);
	}

	internal static void ShowStat(TextMesh text, int count, int max)
	{
		string text2 = string.Format(LocalisationManager.GetTranslation(4966), count, max);
		text.text = text2;
	}

	internal static void ShowTooltip(TextMesh text, int count, int max)
	{
		string text2 = string.Format(LocalisationManager.GetTranslation(4965), count, max);
		text.text = text2;
	}
}
