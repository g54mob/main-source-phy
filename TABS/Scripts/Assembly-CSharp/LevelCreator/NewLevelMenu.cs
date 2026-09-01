using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InControl;
using Landfall.TABS.GameMode;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LevelCreator
{
	public class NewLevelMenu : DMUIPanel
	{
		[SerializeField]
		private string m_defaultTemplate;

		[SerializeField]
		private string m_defaultPreset;

		[SerializeField]
		private Transform m_templatesParent;

		[SerializeField]
		private GameObject m_templateLevelTemplate;

		[SerializeField]
		private Transform m_presetParent;

		[SerializeField]
		private GameObject m_presetTemplate;

		[SerializeField]
		private LocalizeText m_presetNameText;

		[SerializeField]
		private Button m_createLevelButton;

		private NewLevelTemplateComponent m_selectedTemplate;

		private NewLevelPresetTemplateComponent m_selectedPreset;

		private GameModeService m_gameModeService;

		private bool m_hasBuilt;

		private bool m_isBuildingTemplates;

		private int m_buildTemplatesId;

		private bool m_isDestroyed;

		public override void OnOpen()
		{
			base.OnOpen();
			if (!m_hasBuilt)
			{
				m_hasBuilt = true;
				BuildLevelPresets();
			}
			else
			{
				SelectDefaultPreset();
			}
		}

		private void Start()
		{
			AssertionCheck();
			AssignInput();
			m_gameModeService = ServiceLocator.GetService<GameModeService>();
		}

		private void OnDestroy()
		{
			m_isDestroyed = true;
		}

		private void OnEnable()
		{
			PlayerActions.Instance.OnLastInputTypeChanged += OnLastInputTypeChanged;
		}

		private void OnDisable()
		{
			PlayerActions.Instance.OnLastInputTypeChanged -= OnLastInputTypeChanged;
		}

		private void AssertionCheck()
		{
		}

		private void AssignInput()
		{
			PlayerActions instance = PlayerActions.Instance;
			m_inputState.AddOnKeyDownListener(instance.m_back, delegate
			{
				DMUIManager.Instance.PopPanel();
			});
			m_inputState.AddOnKeyDownListener(instance.m_enterExitBattle, delegate
			{
				DMUIManager.Instance.PopPanel();
			});
		}

		private IEnumerator BuildTemplates(NewLevelPresetTemplateComponent presetComponent)
		{
			m_isBuildingTemplates = true;
			m_createLevelButton.interactable = false;
			m_buildTemplatesId++;
			int buildTemplatesId = m_buildTemplatesId;
			Utility.DestroyChildren(m_templatesParent);
			List<Action<Texture>> textureLoadActions = new List<Action<Texture>>();
			List<string> levelThumbnailPaths = new List<string>();
			int levelsToLoad = 10000;
			int loadedLevels = 0;
			bool cancel = false;
			Utility.DelayAction(this, delegate
			{
				if (buildTemplatesId != m_buildTemplatesId || m_isDestroyed)
				{
					cancel = true;
				}
				else
				{
					LevelUtility.GetTemplateLevels(delegate(string[] templateLevels, Exception getTemplatesException)
					{
						if (buildTemplatesId != m_buildTemplatesId || m_isDestroyed)
						{
							cancel = true;
						}
						else
						{
							levelsToLoad = templateLevels.Length;
							foreach (string levelPath in templateLevels)
							{
								if (buildTemplatesId != m_buildTemplatesId || m_isDestroyed)
								{
									cancel = true;
									break;
								}
								if (string.IsNullOrEmpty(levelPath))
								{
									loadedLevels++;
								}
								else
								{
									string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(levelPath);
									if (!presetComponent.m_levelPreset.MapFilesNames.Contains(fileNameWithoutExtension))
									{
										loadedLevels++;
									}
									else
									{
										DMIOWrapper.File.ReadAllBytes(levelPath, FileHandlingFileType.StreamingAssetsOrReadOnlyFile, delegate(byte[] bytes, Exception e)
										{
											if (buildTemplatesId != m_buildTemplatesId || m_isDestroyed)
											{
												cancel = true;
											}
											else if (bytes == null || bytes.Length == 0)
											{
												int num = loadedLevels;
												loadedLevels = num + 1;
											}
											else if (LevelSerializer.Deserialize(Utility.Unzip(bytes)).settings.presetName != m_selectedPreset.m_levelPreset.name)
											{
												int num = loadedLevels;
												loadedLevels = num + 1;
											}
											else
											{
												GameObject gameObject = UnityEngine.Object.Instantiate(m_templateLevelTemplate, m_templatesParent);
												gameObject.gameObject.SetActive(value: true);
												RawImage templateImage = gameObject.GetComponentInChildren<RawImage>();
												templateImage.color = new Color(1f, 1f, 1f, 0f);
												textureLoadActions.Add(delegate(Texture tex)
												{
													if (!(templateImage == null))
													{
														templateImage.texture = tex;
														LeanTween.value(templateImage.gameObject, delegate(Color color)
														{
															templateImage.color = color;
														}, templateImage.color, Color.white, 0.15f);
													}
												});
												levelThumbnailPaths.Add(LevelUtility.GetLevelThumbnail(levelPath));
												gameObject.GetComponentInChildren<LocalizeText>().LocaleID = LevelUtility.GetLevelName(levelPath);
												NewLevelTemplateComponent templateComponent = gameObject.GetComponent<NewLevelTemplateComponent>();
												templateComponent.m_levelPath = levelPath;
												gameObject.GetComponentInChildren<Button>().onClick.AddListener(delegate
												{
													SelectTemplate(templateComponent);
												});
												int num = loadedLevels;
												loadedLevels = num + 1;
											}
										});
									}
								}
							}
						}
					});
				}
			});
			yield return new WaitUntil(() => loadedLevels >= levelsToLoad || cancel);
			if (cancel)
			{
				yield break;
			}
			TextureUtility.LoadTexturesAsyncSequential(this, levelThumbnailPaths.ToArray(), textureLoadActions.ToArray(), delegate
			{
				if (buildTemplatesId != m_buildTemplatesId || m_isDestroyed)
				{
					cancel = true;
				}
				else
				{
					SelectFirstTemplate();
					m_isBuildingTemplates = false;
				}
			});
		}

		private void SelectTemplate(NewLevelTemplateComponent templateComponent)
		{
			if (m_selectedTemplate != null)
			{
				m_selectedTemplate.Deselect();
			}
			m_selectedTemplate = templateComponent;
			if (m_selectedTemplate != null)
			{
				m_selectedTemplate.Select();
				m_createLevelButton.interactable = true;
			}
		}

		private void SelectFirstTemplate()
		{
			NewLevelTemplateComponent[] componentsInChildren = m_templatesParent.GetComponentsInChildren<NewLevelTemplateComponent>();
			if (componentsInChildren.Length != 0)
			{
				SelectTemplate(componentsInChildren[0]);
			}
		}

		private void BuildLevelPresets()
		{
			Utility.DestroyChildren(m_presetParent);
			Utility.DelayAction(this, delegate
			{
				LevelPresetData[] allPresets = LevelPresetData.GetAllPresets();
				foreach (LevelPresetData levelPreset in allPresets)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate(m_presetTemplate, m_presetParent);
					gameObject.SetActive(value: true);
					NewLevelPresetTemplateComponent presetComponent = gameObject.GetComponent<NewLevelPresetTemplateComponent>();
					presetComponent.Init(levelPreset);
					gameObject.GetComponent<Button>().onClick.AddListener(delegate
					{
						SelectPreset(presetComponent);
					});
				}
				SelectDefaultPreset();
			});
		}

		private void SelectPreset(NewLevelPresetTemplateComponent presetComponent)
		{
			if (m_selectedPreset != null)
			{
				m_selectedPreset.Deselect();
			}
			m_selectedPreset = presetComponent;
			if (m_selectedPreset != null)
			{
				m_selectedPreset.Select();
				m_selectedPreset.GetComponentInChildren<Selectable>().Select();
			}
			if (presetComponent != null)
			{
				m_presetNameText.LocaleID = presetComponent.m_levelPreset.LocalizedName;
			}
			StartCoroutine(BuildTemplates(presetComponent));
		}

		private void SelectDefaultPreset()
		{
			NewLevelPresetTemplateComponent[] componentsInChildren = m_presetParent.GetComponentsInChildren<NewLevelPresetTemplateComponent>();
			foreach (NewLevelPresetTemplateComponent newLevelPresetTemplateComponent in componentsInChildren)
			{
				if (newLevelPresetTemplateComponent.m_levelPreset.PresetName == m_defaultPreset)
				{
					newLevelPresetTemplateComponent.transform.SetAsFirstSibling();
					SelectPreset(newLevelPresetTemplateComponent);
				}
			}
		}

		private void OnLastInputTypeChanged(BindingSourceType obj)
		{
			if (PlayerActions.Instance.InputType == InputType.Controller && m_defaultObject != null)
			{
				EventSystem.current.SetSelectedGameObject(m_defaultObject.gameObject);
			}
		}

		public void CreateLevel()
		{
			if (m_isBuildingTemplates)
			{
				MessageDisplay.DisplayMessage("LC_MESSAGE_WAIT_FOR_TEMPLATES");
				return;
			}
			if (m_selectedTemplate == null)
			{
				Debug.LogError("m_selectedTemplate is null");
				return;
			}
			if (DMEditor.Instance == null)
			{
				Debug.LogError("DMEditor.Instance is null");
				return;
			}
			if (m_gameModeService == null)
			{
				Debug.LogError("m_gameModeService is null.");
				return;
			}
			m_gameModeService.SetGameMode<MapCreatorGameMode>();
			DMEditor.Instance.LoadLevel(DMEditor.StartState.New, m_selectedTemplate.m_levelPath);
			DMEditor.Instance.SetPreset(m_selectedPreset.m_levelPreset);
			DMEditor.SetLevelToLoadOnStart(string.Empty);
			DMUIManager.Instance.PopAll();
		}
	}
}
