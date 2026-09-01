using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundBank", menuName = "TABS/SoundBank", order = 0)]
public class SoundBank : SerializedScriptableObject
{
	[Space(10f)]
	public SoundBankCategory[] Categories;

	private SoundEffectInstance instance;

	private SoundEffectInstance prevInstance;

	public SoundEffectInstance GetSoundEffectNonAlloc(string soundCategory, string soundEffect, float volume, float pitch)
	{
		if (string.IsNullOrWhiteSpace(soundCategory) || string.IsNullOrWhiteSpace(soundEffect))
		{
			return null;
		}
		instance = null;
		for (int i = 0; i < Categories.Length; i++)
		{
			SoundBankCategory soundBankCategory = Categories[i];
			if (!(Categories[i].categoryName == soundCategory))
			{
				continue;
			}
			for (int j = 0; j < soundBankCategory.soundEffects.Length; j++)
			{
				SoundEffectInstance soundEffectInstance = soundBankCategory.soundEffects[j];
				if (soundEffectInstance.soundRef == soundEffect)
				{
					instance = soundEffectInstance;
					break;
				}
			}
			if (instance != null)
			{
				instance.category = default(SoundBackSettings);
				instance.category.maxDistanceMultiplier = soundBankCategory.maxDistanceMultiplier;
				instance.category.minDistanceMultiplier = soundBankCategory.minDistanceMultiplier;
				instance.category.pitchMultiplier = soundBankCategory.pitchMultiplier * pitch;
				instance.category.volumeMultiplier = soundBankCategory.volumeMultiplier * volume;
				instance.categoryMixerGroup = soundBankCategory.categoryMixerGroup;
				prevInstance = instance;
			}
			break;
		}
		return instance;
	}

	public SoundEffectInstance GetSoundEffect(string soundPath)
	{
		if (string.IsNullOrWhiteSpace(soundPath))
		{
			return null;
		}
		string[] array = soundPath.Split(char.Parse("/"));
		if (array.Length < 2)
		{
			Debug.LogErrorFormat("Sound effect have invalid path: {0}", soundPath);
			return null;
		}
		string text = array[0];
		string text2 = string.Empty;
		if (array.Length > 1)
		{
			text2 = array[1];
		}
		float num = 1f;
		float num2 = 1f;
		bool flag = false;
		List<string> list = new List<string>();
		int num3 = 0;
		for (int i = 0; i < text2.Length; i++)
		{
			string text3 = text2[i].ToString();
			if (text3 == ")")
			{
				flag = false;
				list[num3] += text3;
				num3++;
			}
			if (flag)
			{
				list[num3] += text3;
			}
			if (text3 == "(")
			{
				flag = true;
				list.Add("");
				list[num3] += text3;
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			if (text2.Contains(list[j]))
			{
				if (list[j].ToUpper().Contains("P"))
				{
					num = GetValueFromCommand(list[j]);
				}
				else if (list[j].ToUpper().Contains("V"))
				{
					num2 = GetValueFromCommand(list[j]);
				}
				text2 = text2.Replace(list[j], "");
			}
		}
		instance = null;
		for (int k = 0; k < Categories.Length; k++)
		{
			if (!(Categories[k].categoryName == text))
			{
				continue;
			}
			for (int l = 0; l < Categories[k].soundEffects.Length; l++)
			{
				if (Categories[k].soundEffects[l].soundRef == text2)
				{
					instance = Categories[k].soundEffects[l];
					break;
				}
			}
			if (instance != null)
			{
				instance.category = default(SoundBackSettings);
				instance.category.maxDistanceMultiplier = Categories[k].maxDistanceMultiplier;
				instance.category.minDistanceMultiplier = Categories[k].minDistanceMultiplier;
				instance.category.pitchMultiplier = Categories[k].pitchMultiplier * num;
				instance.category.volumeMultiplier = Categories[k].volumeMultiplier * num2;
				instance.categoryMixerGroup = Categories[k].categoryMixerGroup;
			}
			break;
		}
		return instance;
	}

	private float GetValueFromCommand(string command)
	{
		string text = "";
		for (int i = 0; i < command.Length; i++)
		{
			string text2 = command[i].ToString();
			if (text2 == ",")
			{
				text2 = ".";
			}
			if (text2 != "(" && text2 != "=" && text2 != ")" && text2.ToUpper() != "P" && text2.ToUpper() != "V")
			{
				text += text2;
			}
		}
		text = text.Trim();
		if (!float.TryParse(text, out var result))
		{
			return 1f;
		}
		return result;
	}
}
