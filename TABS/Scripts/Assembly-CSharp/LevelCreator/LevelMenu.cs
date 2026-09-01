using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace LevelCreator
{
	public class LevelMenu : DMUIPanel
	{
		public GameObject levelButtonPrefabInitial;

		private GameObject levelButtonPrefab;

		public Transform levelButtonsParentInitial;

		private Transform levelButtonsParent;

		public Transform levelTemplatesButtonsParentInitial;

		private Transform levelTemplatesButtonsParent;

		public Transform recentLevelButtonsParentInitial;

		private Transform recentLevelButtonsParent;

		public Transform levelActionsParentInitial;

		private Transform levelActionsParent;

		public Transform templateActionsParentInitial;

		private Transform templateActionsParent;

		public Transform deletionCheckParentInitial;

		private Transform deletionCheckParent;

		public InputField saveLevelNameInputFieldInitial;

		private InputField saveLevelNameInputField;

		public Text saveLevelNameTextInitial;

		private Text saveLevelNameText;

		public Button saveCurrentLevelButtonInitial;

		private Button saveCurrentLevelButton;

		public GameObject overwritePanelInitial;

		private GameObject overwritePanel;

		[Space]
		public RawImage currentThumbnailInitial;

		private RawImage currentThumbnail;

		public Transform screenshotsContentTransformInitial;

		private Transform screenshotsContentTransform;

		public Transform thumbnailPanelInitial;

		private Transform thumbnailPanel;

		public Texture2D defaultThumbnailInitial;

		private Texture2D defaultThumbnail;

		[Space]
		public RectTransform backgroundPanelInitial;

		private RectTransform backgroundPanel;

		private LevelPresetData selectedPreset;

		private DMEditor dmEditor;

		private string selectedLevelPath;

		private string loadedLevelPath;

		private string directory;

		private string templateDirectory;

		private List<GameObject> levelButtons = new List<GameObject>();

		private List<GameObject> templateButtons = new List<GameObject>();

		private ControllerManager controllerManager;

		private void AssertReferences()
		{
			levelButtonPrefab = levelButtonPrefabInitial;
			levelButtonsParent = levelButtonsParentInitial;
			levelTemplatesButtonsParent = levelTemplatesButtonsParentInitial;
			recentLevelButtonsParent = recentLevelButtonsParentInitial;
			levelActionsParent = levelActionsParentInitial;
			templateActionsParent = templateActionsParentInitial;
			deletionCheckParent = deletionCheckParentInitial;
			saveLevelNameInputField = saveLevelNameInputFieldInitial;
			saveLevelNameText = saveLevelNameTextInitial;
			saveCurrentLevelButton = saveCurrentLevelButtonInitial;
			overwritePanel = overwritePanelInitial;
			currentThumbnail = currentThumbnailInitial;
			screenshotsContentTransform = screenshotsContentTransformInitial;
			thumbnailPanel = thumbnailPanelInitial;
			defaultThumbnail = defaultThumbnailInitial;
			backgroundPanel = backgroundPanelInitial;
		}

		private void Start()
		{
			AssertReferences();
			dmEditor = DMEditor.Instance;
			PlayerActions instance = PlayerActions.Instance;
			m_inputState.AddOnKeyDownListener(instance.m_enterExitBattle, delegate
			{
				if (!saveLevelNameInputField.isFocused)
				{
					DMUIManager.Instance.PopPanel();
				}
			});
			m_inputState.AddOnKeyDownListener(instance.m_back, delegate
			{
				DMUIManager.Instance.PopPanel();
			});
		}

		public override void OnOpen()
		{
			base.OnOpen();
			directory = GamePaths.PersistentDataPath + "/SavedLevels/";
			templateDirectory = GamePaths.PersistentDataPath + "/LevelTemplates";
			base.transform.parent.SetAsLastSibling();
			GenerateLevelButtons();
			GenerateTemplateButtons();
			ShowLevelMenu();
		}

		public override void OnClose()
		{
			base.OnClose();
			ClearLevelSelection();
			overwritePanel.SetActive(value: false);
			thumbnailPanel.gameObject.SetActive(value: false);
			RemoveScreenshots();
			HideLevelMenu();
		}

		private void ShowLevelMenu()
		{
			Color color = backgroundPanel.GetComponent<Image>().color;
			LeanTween.color(backgroundPanel, new Color(color.r, color.g, color.b, 1f), 0.5f);
		}

		private void HideLevelMenu()
		{
			Color color = backgroundPanel.GetComponent<Image>().color;
			LeanTween.color(backgroundPanel, new Color(color.r, color.g, color.b, 0f), 0.5f);
		}

		public void Quit()
		{
			Application.Quit();
		}

		public void GenerateLevelButtons()
		{
			if (string.IsNullOrEmpty(directory))
			{
				return;
			}
			DMIOWrapper.Directory.EnsureDirectoryExists(directory, FileHandlingFileType.StreamingAssetsOrReadOnlyFile, delegate
			{
				DestroyLevelButtons();
				DMIOWrapper.Directory.GetFiles(directory, FileHandlingFileType.StreamingAssetsOrReadOnlyFile, delegate(string[] levelPaths, Exception getFilesException)
				{
					foreach (string levelPath in levelPaths)
					{
						if (!(levelPath.Substring(levelPath.Length - 4, 4) != ".tld"))
						{
							GameObject button = UnityEngine.Object.Instantiate(levelButtonPrefab, levelButtonsParent);
							levelButtons.Add(button);
							button.GetComponent<Button>().onClick.AddListener(delegate
							{
								SelectLevel(levelPath);
								EnableLevelActions(button.transform.position);
							});
							button.GetComponentInChildren<Text>().text = levelPath.Substring(directory.Length, levelPath.Length - directory.Length - 4);
							string levelThumbnailPath = levelPath.Substring(0, levelPath.Length - 4) + ".png";
							DMIOWrapper.File.Exists(levelThumbnailPath, FileHandlingFileType.StreamingAssetsOrReadOnlyFile, delegate(bool exists)
							{
								if (exists)
								{
									StartCoroutine(LoadTextureAsync(levelThumbnailPath, button.GetComponentInChildren<RawImage>()));
								}
								else
								{
									button.GetComponentInChildren<RawImage>().texture = defaultThumbnail;
								}
							});
						}
					}
					if (levelButtons.Count > 0)
					{
						EventSystem.current.SetSelectedGameObject(levelButtons[0].gameObject);
					}
				});
			});
		}

		public void GenerateTemplateButtons()
		{
			if (string.IsNullOrEmpty(templateDirectory))
			{
				return;
			}
			DestroyTemplateButtons();
			DMIOWrapper.Directory.GetFiles(templateDirectory, FileHandlingFileType.StreamingAssetsOrReadOnlyFile, delegate(string[] levelPaths, Exception e)
			{
				foreach (string levelPath in levelPaths)
				{
					if (!(levelPath.Substring(levelPath.Length - 4, 4) != ".tld"))
					{
						GameObject button = UnityEngine.Object.Instantiate(levelButtonPrefab, levelTemplatesButtonsParent);
						templateButtons.Add(button);
						button.GetComponent<Button>().onClick.AddListener(delegate
						{
							SelectLevel(levelPath);
							EnableTemplateActions(button.transform.position);
						});
						button.GetComponentInChildren<Text>().text = levelPath.Substring(templateDirectory.Length + 1, levelPath.Length - templateDirectory.Length - 5);
						string thumbnailPath = levelPath.Substring(0, levelPath.Length - 4) + ".png";
						DMIOWrapper.File.Exists(thumbnailPath, FileHandlingFileType.StreamingAssetsOrReadOnlyFile, delegate(bool exists)
						{
							if (exists)
							{
								StartCoroutine(LoadTextureAsync(thumbnailPath, button.GetComponentInChildren<RawImage>()));
							}
							else
							{
								button.GetComponentInChildren<RawImage>().texture = defaultThumbnail;
							}
						});
					}
				}
				if (templateButtons.Count > 0)
				{
					EventSystem.current.SetSelectedGameObject(templateButtons[0].gameObject);
				}
			});
		}

		private void DestroyLevelButtons()
		{
			foreach (GameObject levelButton in levelButtons)
			{
				UnityEngine.Object.Destroy(levelButton.gameObject);
			}
			levelButtons.Clear();
		}

		private void DestroyTemplateButtons()
		{
			foreach (GameObject templateButton in templateButtons)
			{
				UnityEngine.Object.Destroy(templateButton.gameObject);
			}
			templateButtons.Clear();
		}

		public void SelectLevel(string levelName)
		{
			selectedLevelPath = levelName;
		}

		public void ClearLevelSelection()
		{
			selectedLevelPath = null;
			foreach (GameObject levelButton in levelButtons)
			{
				levelButton.GetComponent<RawImage>().color = Color.white;
			}
		}

		public void SelectPreset(LevelPresetData levelPresetData)
		{
			selectedPreset = levelPresetData;
		}

		public void EnableLevelActions(Vector3 position)
		{
			deletionCheckParent.gameObject.SetActive(value: false);
			levelActionsParent.transform.position = position;
			levelActionsParent.gameObject.SetActive(value: true);
			levelActionsParent.SetAsLastSibling();
		}

		public void EnableTemplateActions(Vector3 position)
		{
			templateActionsParent.transform.position = position;
			templateActionsParent.gameObject.SetActive(value: true);
			templateActionsParent.SetAsLastSibling();
		}

		public void DeletionCehck()
		{
			deletionCheckParent.transform.position = levelActionsParent.position;
			deletionCheckParent.gameObject.SetActive(value: true);
			deletionCheckParent.SetAsLastSibling();
		}

		public void LoadLevelAndApplyPreset()
		{
			LoadLevel();
			if (selectedPreset != null)
			{
				dmEditor.SetPreset(selectedPreset);
			}
		}

		public void LoadLevel()
		{
			if (string.IsNullOrEmpty(selectedLevelPath))
			{
				return;
			}
			bool flag = selectedLevelPath.Contains("Template");
			if (!flag)
			{
				loadedLevelPath = selectedLevelPath;
				if (!string.IsNullOrEmpty(loadedLevelPath))
				{
					string text = loadedLevelPath.Split('/').Last();
					text = text.Substring(0, text.Length - 4);
					saveLevelNameInputField.text = text;
					DMIOWrapper.File.Exists(loadedLevelPath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
					{
						saveCurrentLevelButton.interactable = exists;
					});
				}
			}
			else
			{
				saveCurrentLevelButton.interactable = false;
			}
			string thumbnailPath = selectedLevelPath.Substring(0, selectedLevelPath.Length - 4) + ".png";
			DMIOWrapper.File.Exists(thumbnailPath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (exists)
				{
					StartCoroutine(LoadTextureAsync(thumbnailPath, currentThumbnail));
				}
			});
			dmEditor.LoadLevel((!flag) ? DMEditor.StartState.Edit : DMEditor.StartState.New, selectedLevelPath);
			DMUIManager.Instance.PopAll();
		}

		public void SaveLevel(bool isOverwritePanel)
		{
			if (string.IsNullOrEmpty(saveLevelNameText.text))
			{
				MessageDisplay.DisplayMessage("LC_ENTER_A_MAP_NAME");
				return;
			}
			string filepath = directory + saveLevelNameText.text + ".tld";
			DMIOWrapper.File.Exists(filepath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (!isOverwritePanel && exists)
				{
					overwritePanel.SetActive(value: true);
				}
				else
				{
					DeleteLevel();
					dmEditor.SaveLevel(filepath, saveLevelNameText.text, currentThumbnail.texture as Texture2D);
					saveCurrentLevelButton.interactable = true;
					loadedLevelPath = filepath;
					LeanTween.delayedCall(0.2f, (System.Action)delegate
					{
						PopUp.CreatePopUp(Vector3.zero, "Saved Level!", demandFocus: false, 1f).Show();
					});
					GenerateLevelButtons();
				}
			});
		}

		public void SaveLastLoadedLevel()
		{
			if (!string.IsNullOrEmpty(loadedLevelPath))
			{
				dmEditor.SaveLevel(loadedLevelPath, "", currentThumbnail.texture as Texture2D);
				LeanTween.delayedCall(0.2f, (System.Action)delegate
				{
					PopUp.CreatePopUp(Vector3.zero, "Saved Level!", demandFocus: false, 1f).Show();
				});
				GenerateLevelButtons();
			}
		}

		public void DeleteLevel()
		{
			if (string.IsNullOrEmpty(selectedLevelPath) || selectedLevelPath.Contains(templateDirectory))
			{
				return;
			}
			DMIOWrapper.File.Delete(selectedLevelPath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate
			{
				DMIOWrapper.File.Delete(selectedLevelPath.Substring(0, selectedLevelPath.Length - 4) + ".png", FileHandlingFileType.CustomContentOrLocalStorageFile, delegate
				{
					GenerateLevelButtons();
				});
			});
		}

		public void SetSelectionToButtonChild(GameObject go)
		{
			EventSystem.current.SetSelectedGameObject(go.GetComponentInChildren<Button>().gameObject);
		}

		public void LoadScreenshots()
		{
			string directoryPath = Application.persistentDataPath + "/Screenshots";
			DMIOWrapper.Directory.EnsureDirectoryExists(directoryPath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate
			{
				DMIOWrapper.Directory.GetFiles(directoryPath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(string[] screenshots, Exception getFilesException)
				{
					screenshotsContentTransform.parent.parent.GetChild(screenshotsContentTransform.parent.parent.childCount - 1).gameObject.SetActive(screenshots.Length == 0);
					if (screenshots.Length != 0)
					{
						for (int i = 0; i < screenshots.Length; i++)
						{
							GameObject screenshotItem = UnityEngine.Object.Instantiate(levelButtonPrefab, screenshotsContentTransform);
							StartCoroutine(LoadTextureAsync(screenshots[i], screenshotItem.GetComponent<RawImage>()));
							screenshotItem.GetComponentInChildren<Button>().onClick.AddListener(delegate
							{
								SetThumbnail(screenshotItem.GetComponent<RawImage>().texture as Texture2D);
								screenshotItem.transform.parent.parent.parent.parent.gameObject.SetActive(value: false);
								RemoveScreenshots();
							});
						}
					}
				});
			});
		}

		public void RemoveScreenshots()
		{
			foreach (Transform item in screenshotsContentTransform)
			{
				UnityEngine.Object.Destroy(item.gameObject);
			}
		}

		private IEnumerator LoadTextureAsync(string path, RawImage image)
		{
			UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path);
			yield return uwr.SendWebRequest();
			Texture texture = ((DownloadHandlerTexture)uwr.downloadHandler).texture;
			if (image != null)
			{
				image.texture = texture;
				image.color = Color.white;
			}
		}

		public void SetThumbnail(Texture2D tex)
		{
			currentThumbnail.texture = tex;
		}
	}
}
