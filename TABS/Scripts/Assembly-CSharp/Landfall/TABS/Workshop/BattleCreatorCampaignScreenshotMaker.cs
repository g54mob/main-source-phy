using System;
using System.Collections.Generic;
using System.IO;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS.Workshop
{
	public class BattleCreatorCampaignScreenshotMaker : MonoBehaviour
	{
		private class LoadData
		{
			public string Path;

			public int SpriteIndex;
		}

		[SerializeField]
		private SpriteRenderer m_Q1Image;

		[SerializeField]
		private SpriteRenderer m_Q2Image;

		[SerializeField]
		private SpriteRenderer m_Q3Image;

		[SerializeField]
		private SpriteRenderer m_Q4Image;

		private FileIOWrapper m_FileIO;

		private void Awake()
		{
			m_FileIO = ServiceLocator.GetService<FileIOWrapper>();
		}

		public void PopulateImages(TABSCampaignAsset campaignAsset, Action doneCallback)
		{
			List<LoadData> list = new List<LoadData>();
			Debug.Log("Populating Images for campaign: " + campaignAsset.Entity.Name + Time.unscaledTime);
			Material mat = m_Q1Image.material;
			int num = campaignAsset.LevelsInCampaign.Length;
			mat.SetInt("_TexCount", Mathf.Clamp(num, 0, 4));
			int num2 = Mathf.Clamp(num / 4, 0, num - 1);
			int first = 0;
			int second = 1;
			int third = 2;
			int fourth = 3;
			if (num >= 4)
			{
				second = Mathf.Clamp(first + num2, first, num - 1);
				third = Mathf.Clamp(second + num2, first, num - 1);
				fourth = Mathf.Clamp(third + num2, first, num - 1);
			}
			AddLevelSpriteToLoad(first, 1, campaignAsset, list, num);
			if (second != first)
			{
				AddLevelSpriteToLoad(second, 2, campaignAsset, list, num);
			}
			if (third != second)
			{
				AddLevelSpriteToLoad(third, 3, campaignAsset, list, num);
			}
			if (fourth != third)
			{
				AddLevelSpriteToLoad(fourth, 4, campaignAsset, list, num);
			}
			int count = list.Count;
			if (count <= 0)
			{
				doneCallback?.Invoke();
				return;
			}
			AsyncCounter asyncCounter = new AsyncCounter(count);
			for (int i = 0; i < count; i++)
			{
				AsyncCounter tempCounter = asyncCounter;
				LoadData data = list[i];
				GetImage(data.Path, delegate(Sprite sprite)
				{
					Texture2D value = ((sprite != null) ? sprite.texture : null);
					switch (data.SpriteIndex)
					{
					case 1:
						mat.SetTexture("_Tex1", value);
						break;
					case 2:
						mat.SetTexture("_Tex2", value);
						break;
					case 3:
						mat.SetTexture("_Tex3", value);
						break;
					case 4:
						mat.SetTexture("_Tex4", value);
						break;
					}
					if (tempCounter.OnAsyncDone())
					{
						if (second == first)
						{
							m_Q2Image.sprite = m_Q1Image.sprite;
						}
						if (third == second)
						{
							m_Q3Image.sprite = m_Q2Image.sprite;
						}
						if (fourth == third)
						{
							m_Q4Image.sprite = m_Q3Image.sprite;
						}
						base.gameObject.SetActive(value: true);
						Debug.Log("Done adding images: " + Time.unscaledTime);
						doneCallback?.Invoke();
					}
				});
			}
		}

		private void AddLevelSpriteToLoad(int levelIndex, int spriteIndex, TABSCampaignAsset campaignAsset, List<LoadData> load, int maxLevels)
		{
			if (levelIndex >= 0 && levelIndex < maxLevels && campaignAsset.LevelsInCampaign[levelIndex] != null)
			{
				load.Add(new LoadData
				{
					Path = campaignAsset.LevelsInCampaign[levelIndex].FilePath,
					SpriteIndex = spriteIndex
				});
			}
		}

		private void GetImage(string filePath, Action<Sprite> doneCallback)
		{
			string directoryName = Path.GetDirectoryName(filePath);
			if (directoryName == null)
			{
				doneCallback?.Invoke(null);
				return;
			}
			string imageFile = Path.Combine(directoryName, "Picture.png");
			m_FileIO.FileExists(imageFile, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (!exists)
				{
					doneCallback?.Invoke(null);
				}
				else
				{
					m_FileIO.ReadAllBytes(imageFile, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(byte[] bytes, Exception exception)
					{
						if (bytes == null || bytes.Length == 0)
						{
							doneCallback?.Invoke(null);
						}
						else
						{
							Texture2D texture2D = new Texture2D(2, 2);
							texture2D.LoadImage(bytes);
							Sprite obj = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 1000f);
							doneCallback?.Invoke(obj);
						}
					});
				}
			});
		}
	}
}
