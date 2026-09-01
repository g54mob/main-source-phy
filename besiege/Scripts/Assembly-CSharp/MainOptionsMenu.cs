using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using BesiegeDlc;
using InternalModding.Mods;
using Localisation;
using Modding;
using UnityEngine;

public class MainOptionsMenu : MonoBehaviour
{
	private class TabData
	{
		public UIButton button;

		public TextMesh text;

		public List<OptionsCategory> categories;
	}

	public class OptionsCategory
	{
		public class MenuOption
		{
			public bool hidden;

			public float sizeY = 0.2f;

			public string name = string.Empty;

			public int NameLocID;

			public virtual bool ShowReset()
			{
				return true;
			}

			public virtual void Reset()
			{
			}
		}

		public class BoolOption : EnumOption
		{
			public BoolOption()
			{
				optionLocIDs = new int[2] { 2142, 2141 };
			}
		}

		public class EnumOption : MenuOption
		{
			public enum DisplayOption
			{
				Normal = 0,
				Dropdown = 1,
				FPS = 2
			}

			public DisplayOption display;

			public int[] optionLocIDs;

			public Func<int> getDefault;

			public Func<int> getFunc;

			public Action<int> setFunc;

			public override void Reset()
			{
				int obj = getDefault();
				setFunc(obj);
			}
		}

		public class StringEnumOption : EnumOption
		{
			public string[] options;
		}

		public class ValueOption : MenuOption
		{
			public Func<float> getDefault;

			public Func<float> getFunc;

			public Func<float> getIncrement = () => 1f;

			public Action<float> setFunc;

			public float Min;

			public float Max;

			public int MinTextID = -1;

			public int MaxTextID = -1;

			public override void Reset()
			{
				setFunc(getDefault());
			}
		}

		public class ControlsOption : MenuOption
		{
			public int SplitLocID = -1;

			public Func<ControlScheme.ControlEntry> getDefault;

			public Func<ControlScheme.ControlEntry> getFunc;

			public Action<ControlScheme.ControlEntry> setFunc;

			public ControlsOption()
			{
				sizeY = 0.33f;
			}

			public override bool ShowReset()
			{
				return StatMaster.allowFullRebinding || getFunc().Rebindable;
			}

			public override void Reset()
			{
				setFunc(getDefault());
			}
		}

		public int NameLocID = -1;

		public int SubtitleLocID = -1;

		public Action SubtitleClicked;

		public List<MenuOption> options;
	}

	private enum OptionsTab
	{
		General = 0,
		Graphics = 1,
		Sound = 2,
		Controls = 3
	}

	public static string OPTIONS_FOLDER = "Prefabs/OptionsMenu/";

	[SerializeField]
	private UIScrollbar scrollbar;

	[SerializeField]
	private UIButton resetAll;

	[SerializeField]
	private UIButton closeBtn;

	private static string[] resolutions = new string[20]
	{
		"800 x 600ext:(4:3)", "1024 x 768ext:(4:3)", "1280 x 960ext:(4:3)", "1280 x 1024ext:(5:4)", "1280 x 800ext:(16:10)", "1440 x 900ext:(16:10)", "1680 x 1050ext:(16:10)", "1920 x 1200ext:(16:10)", "2560 x 1600ext:(16:10)", "2880 x 1800ext:(16:10)",
		"1280 x 720ext:(16:9)", "1366 x 768ext:(16:9)", "1536 x 864ext:(16:9)", "1600 x 900ext:(16:9)", "1920 x 1080ext:(16:9)", "2560 x 1440ext:(16:9)", "3840 x 2160ext:(16:9)", "2560 x 1080ext:(21:9)", "3440 x 1440ext:(21:9)", "4096 x 2160ext:(256:135)"
	};

	private Regex resolutionRegex = new Regex("([0-9]+) x ([0-9]+)", RegexOptions.Compiled);

	private static string[] locFiles;

	public int widgetLayer = 13;

	public UIButton[] tabs;

	public MenuOptionsContainer optionsContainer;

	private TabData[] tabData;

	private bool initialized;

	private OptionsTab currentTab;

	private bool setInMenu;

	private float disableTimer;

	public static MainOptionsMenu CurrentInstance { get; private set; }

	public static bool HasInstance { get; private set; }

	public void Awake()
	{
		resetAll.Click += OnResetAll;
		closeBtn.Click += Close;
	}

	private void OnResetAll()
	{
		OptionsMaster.FormerAntiAliasingMode = AAMode.FXAA3Console;
		TabData tabData = this.tabData[(int)currentTab];
		for (int i = 0; i < tabData.categories.Count; i++)
		{
			OptionsCategory optionsCategory = tabData.categories[i];
			for (int j = 0; j < optionsCategory.options.Count; j++)
			{
				OptionsCategory.MenuOption menuOption = optionsCategory.options[j];
				if (menuOption.ShowReset())
				{
					menuOption.Reset();
				}
			}
		}
		OpenMenu(currentTab);
	}

	private void SetInMenu(bool toggle)
	{
		if (setInMenu != toggle)
		{
			StatMaster.SetInMenu(toggle);
			setInMenu = toggle;
		}
	}

	public void OnEnable()
	{
		SetInMenu(true);
		locFiles = new string[LocalisationManager.BuiltinLocalisations.Count + LocalisationManager.ExternalLocalisations.Count];
		for (int i = 0; i < LocalisationManager.BuiltinLocalisations.Count; i++)
		{
			locFiles[i] = LocalisationManager.BuiltinLocalisations[i].LanguageName.ToUpper();
		}
		for (int j = 0; j < LocalisationManager.ExternalLocalisations.Count; j++)
		{
			locFiles[j + LocalisationManager.BuiltinLocalisations.Count] = LocalisationManager.ExternalLocalisations[j].LanguageName.ToUpper();
		}
		Initialize();
		OpenMenu(OptionsTab.General);
	}

	public void OnDisable()
	{
		SetInMenu(false);
	}

	private int GetResolutionIndex(int width, int height)
	{
		string value = width + " x " + height;
		int result = -1;
		for (int i = 0; i < resolutions.Length; i++)
		{
			if (resolutions[i].StartsWith(value))
			{
				result = i;
				break;
			}
		}
		return result;
	}

	private bool GetResolution(string resInput, out Vector2 output)
	{
		Match match = resolutionRegex.Match(resInput);
		int result;
		int result2;
		if (match.Success && int.TryParse(match.Groups[1].Value, out result) && int.TryParse(match.Groups[2].Value, out result2))
		{
			output = new Vector2(result, result2);
			return true;
		}
		output = Vector2.zero;
		return false;
	}

	private List<OptionsCategory> GetCategoryList(OptionsTab tab)
	{
		BesiegeConfig currentConfig = OptionsMaster.BesiegeConfig;
		BesiegeConfig defaultConfig = OptionsMaster.DefaultConfig;
		List<OptionsCategory> list;
		switch (tab)
		{
		case OptionsTab.General:
		{
			OptionsCategory.BoolOption hotkeyHudOption = new OptionsCategory.BoolOption
			{
				NameLocID = 3867,
				getFunc = () => currentConfig.HotkeyHUD ? 1 : 0,
				setFunc = delegate(int x)
				{
					currentConfig.HotkeyHUD = x == 1;
					if (ReferenceMaster.onHotkeyHUDToggled != null)
					{
						ReferenceMaster.onHotkeyHUDToggled();
					}
				},
				getDefault = () => defaultConfig.HotkeyHUD ? 1 : 0,
				hidden = !currentConfig.Tooltips
			};
			OptionsCategory.EnumOption framerateOption = new OptionsCategory.EnumOption
			{
				NameLocID = 3495,
				display = OptionsCategory.EnumOption.DisplayOption.FPS,
				optionLocIDs = new int[11]
				{
					3414, 5087, 5087, 5087, 5087, 5087, 5087, 5087, 5087, 5087,
					5087
				},
				getFunc = () => (int)currentConfig.FPSLock,
				setFunc = delegate(int x)
				{
					currentConfig.FPSLock = (FPSLock)x;
					if (ReferenceMaster.onFramerateChanged != null)
					{
						ReferenceMaster.onFramerateChanged();
					}
				},
				getDefault = () => (int)defaultConfig.FPSLock,
				hidden = (currentConfig.VSync > 0)
			};
			List<OptionsCategory> list2 = new List<OptionsCategory>();
			list2.Add(new OptionsCategory
			{
				options = new List<OptionsCategory.MenuOption>
				{
					new OptionsCategory.StringEnumOption
					{
						display = OptionsCategory.EnumOption.DisplayOption.Dropdown,
						NameLocID = 50,
						options = locFiles,
						getFunc = () => LocalisationManager.FindIndex(currentConfig.Language),
						setFunc = delegate(int x)
						{
							if (x >= 0)
							{
								if (x >= LocalisationManager.BuiltinLocalisations.Count)
								{
									x -= LocalisationManager.BuiltinLocalisations.Count;
									if (x >= LocalisationManager.ExternalLocalisations.Count)
									{
										return;
									}
									currentConfig.Language = LocalisationManager.ExternalLocalisations[x].LanguageName;
									SingleInstance<LocalisationManager>.Instance.LoadLanguage(Path.GetFileNameWithoutExtension(LocalisationManager.ExternalLocalisations[x].FileName));
								}
								else
								{
									LocalisationManager.LanguageInfo languageInfo = LocalisationManager.BuiltinLocalisations[x];
									currentConfig.Language = languageInfo.LanguageFile;
									SingleInstance<LocalisationManager>.Instance.LoadLanguage(languageInfo.LanguageFile);
								}
								OpenMenu(currentTab);
							}
						},
						getDefault = () => SingleInstance<LocalisationManager>.Instance.GetSystemLanguageIndex()
					},
					new OptionsCategory.BoolOption
					{
						NameLocID = 3668,
						getFunc = () => currentConfig.CloudSaving ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.CloudSaving = x == 1;
							if (ReferenceMaster.onCloudSavingToggled != null)
							{
								ReferenceMaster.onCloudSavingToggled();
							}
						},
						getDefault = () => defaultConfig.CloudSaving ? 1 : 0
					},
					new OptionsCategory.BoolOption
					{
						NameLocID = 4967,
						getFunc = () => currentConfig.UseLeaderboards ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.UseLeaderboards = x == 1;
						},
						getDefault = () => defaultConfig.UseLeaderboards ? 1 : 0
					},
					new OptionsCategory.BoolOption
					{
						NameLocID = 3397,
						getFunc = () => currentConfig.AdvancedBuilding ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.AdvancedBuilding = x == 1;
							if (ReferenceMaster.onAdvancedBuildingToggled != null)
							{
								ReferenceMaster.onAdvancedBuildingToggled();
							}
						},
						getDefault = () => defaultConfig.AdvancedBuilding ? 1 : 0
					},
					new OptionsCategory.BoolOption
					{
						NameLocID = 48,
						getFunc = () => currentConfig.BloodEnabled ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.BloodEnabled = x == 1;
							if (ReferenceMaster.onBloodToggled != null)
							{
								ReferenceMaster.onBloodToggled();
							}
						},
						getDefault = () => defaultConfig.BloodEnabled ? 1 : 0
					}
				}
			});
			list2.Add(new OptionsCategory
			{
				NameLocID = 3405,
				options = new List<OptionsCategory.MenuOption>
				{
					new OptionsCategory.EnumOption
					{
						NameLocID = 3405,
						display = OptionsCategory.EnumOption.DisplayOption.Dropdown,
						optionLocIDs = new int[8] { 4655, 4656, 4657, 4658, 4659, 4660, 4661, 4662 },
						getFunc = () => currentConfig.Monitor,
						setFunc = delegate(int x)
						{
							currentConfig.Monitor = x;
							PlayerPrefs.SetInt("UnitySelectMonitor", Mathf.Min(x, Display.displays.Length));
						},
						getDefault = () => defaultConfig.Monitor
					},
					new OptionsCategory.StringEnumOption
					{
						NameLocID = 3406,
						display = OptionsCategory.EnumOption.DisplayOption.Dropdown,
						options = resolutions,
						getFunc = () => GetResolutionIndex(currentConfig.ScreenWidth, currentConfig.ScreenHeight),
						setFunc = delegate(int x)
						{
							if (x == -1)
							{
								currentConfig.ScreenWidth = defaultConfig.ScreenWidth;
								currentConfig.ScreenHeight = defaultConfig.ScreenHeight;
							}
							else
							{
								Vector2 output;
								if (!GetResolution(resolutions[x], out output))
								{
									return;
								}
								currentConfig.ScreenWidth = Mathf.RoundToInt(output.x);
								currentConfig.ScreenHeight = Mathf.RoundToInt(output.y);
							}
							ReferenceMaster.InvokeResolutionChange();
							if (ReferenceMaster.onFOVChanged != null)
							{
								ReferenceMaster.onFOVChanged();
							}
						},
						getDefault = () => GetResolutionIndex(defaultConfig.ScreenWidth, defaultConfig.ScreenHeight)
					},
					new OptionsCategory.BoolOption
					{
						NameLocID = 0,
						name = "VSYNC",
						getFunc = () => currentConfig.VSync,
						setFunc = delegate(int x)
						{
							currentConfig.VSync = x;
							if (ReferenceMaster.onFramerateChanged != null)
							{
								ReferenceMaster.onFramerateChanged();
							}
							framerateOption.hidden = x > 0;
							OpenMenu(currentTab);
						},
						getDefault = () => defaultConfig.VSync
					},
					framerateOption,
					new OptionsCategory.BoolOption
					{
						NameLocID = 3408,
						getFunc = () => (!currentConfig.WindowedMode) ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.WindowedMode = x == 0;
							ReferenceMaster.InvokeResolutionChange();
						},
						getDefault = () => (!defaultConfig.WindowedMode) ? 1 : 0
					}
				}
			});
			list2.Add(new OptionsCategory
			{
				NameLocID = 3887,
				options = new List<OptionsCategory.MenuOption>
				{
					new OptionsCategory.ValueOption
					{
						NameLocID = 3464,
						getFunc = () => currentConfig.UIScale,
						setFunc = delegate(float x)
						{
							currentConfig.UIScale = x;
							if (ReferenceMaster.onUIScaleChanged != null)
							{
								ReferenceMaster.onUIScaleChanged();
							}
						},
						getDefault = () => defaultConfig.UIScale,
						Min = 50f,
						Max = 100f
					},
					new OptionsCategory.ValueOption
					{
						NameLocID = 4666,
						getFunc = () => currentConfig.UIIntensity,
						setFunc = delegate(float x)
						{
							currentConfig.UIIntensity = x;
							if (ReferenceMaster.onUIIntensityChanged != null)
							{
								ReferenceMaster.onUIIntensityChanged();
							}
						},
						getDefault = () => defaultConfig.UIIntensity,
						Min = 0f,
						Max = 100f
					},
					new OptionsCategory.BoolOption
					{
						NameLocID = 4283,
						getFunc = () => currentConfig.UIBlur ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.UIBlur = x == 1;
							if (ReferenceMaster.onUIBlurToggled != null)
							{
								ReferenceMaster.onUIBlurToggled();
							}
						},
						getDefault = () => defaultConfig.UIBlur ? 1 : 0
					},
					new OptionsCategory.BoolOption
					{
						NameLocID = 3012,
						getFunc = () => currentConfig.Tutorials ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.Tutorials = x == 1;
							if (ReferenceMaster.onTutorialsToggled != null)
							{
								ReferenceMaster.onTutorialsToggled(x == 1);
							}
						},
						getDefault = () => defaultConfig.Tutorials ? 1 : 0
					},
					new OptionsCategory.BoolOption
					{
						NameLocID = 505,
						getFunc = () => currentConfig.Tooltips ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.Tooltips = x == 1;
							if (ReferenceMaster.onTooltipsToggled != null)
							{
								ReferenceMaster.onTooltipsToggled();
							}
							hotkeyHudOption.hidden = x == 0;
							OpenMenu(currentTab);
						},
						getDefault = () => defaultConfig.Tooltips ? 1 : 0
					},
					hotkeyHudOption,
					new OptionsCategory.BoolOption
					{
						NameLocID = 3865,
						getFunc = () => currentConfig.ShowSurfaceNodeGrid ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.ShowSurfaceNodeGrid = x == 1;
							if (ReferenceMaster.onShowNodeGridToggled != null)
							{
								ReferenceMaster.onShowNodeGridToggled();
							}
						},
						getDefault = () => defaultConfig.MiddleClickVFX ? 1 : 0
					},
					new OptionsCategory.BoolOption
					{
						NameLocID = 3667,
						getFunc = () => currentConfig.MiddleClickVFX ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.MiddleClickVFX = x == 1;
							if (ReferenceMaster.onMiddleClickVFXToggled != null)
							{
								ReferenceMaster.onMiddleClickVFXToggled();
							}
						},
						getDefault = () => defaultConfig.MiddleClickVFX ? 1 : 0
					},
					new OptionsCategory.BoolOption
					{
						NameLocID = 5002,
						getFunc = () => currentConfig.ShowConquered ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.ShowConquered = x == 1;
							if (ReferenceMaster.onConquerToggled != null)
							{
								ReferenceMaster.onConquerToggled();
							}
						},
						getDefault = () => defaultConfig.ShowConquered ? 1 : 0
					}
				}
			});
			list2.Add(new OptionsCategory
			{
				NameLocID = 3674,
				options = new List<OptionsCategory.MenuOption>
				{
					new OptionsCategory.EnumOption
					{
						NameLocID = 4233,
						optionLocIDs = new int[2] { 4235, 4234 },
						getFunc = () => currentConfig.UseBoundsCenter ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.UseBoundsCenter = ((x != 0) ? true : false);
						},
						getDefault = () => defaultConfig.UseBoundsCenter ? 1 : 0
					},
					new OptionsCategory.EnumOption
					{
						NameLocID = 4649,
						optionLocIDs = new int[2] { 4650, 4651 },
						getFunc = () => (int)currentConfig.SimCamFollow,
						setFunc = delegate(int x)
						{
							currentConfig.SimCamFollow = (MouseOrbit.SimOrientation)x;
						},
						getDefault = () => (int)defaultConfig.SimCamFollow
					},
					new OptionsCategory.ValueOption
					{
						NameLocID = 3895,
						getFunc = () => currentConfig.CameraSensitivity,
						setFunc = delegate(float x)
						{
							currentConfig.CameraSensitivity = x;
							if (ReferenceMaster.onCameraSensitivityChanged != null)
							{
								ReferenceMaster.onCameraSensitivityChanged();
							}
						},
						getDefault = () => defaultConfig.CameraSensitivity,
						Min = 0f,
						Max = 200f
					},
					new OptionsCategory.ValueOption
					{
						NameLocID = 3407,
						getFunc = () => currentConfig.FieldOfView,
						setFunc = delegate(float x)
						{
							currentConfig.FieldOfView = x;
							if (ReferenceMaster.onFOVChanged != null)
							{
								ReferenceMaster.onFOVChanged();
							}
						},
						getDefault = () => defaultConfig.FieldOfView,
						Min = 60f,
						Max = 110f
					},
					new OptionsCategory.BoolOption
					{
						NameLocID = 506,
						getFunc = () => currentConfig.SmoothCamera ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.SmoothCamera = x == 1;
							if (ReferenceMaster.onSmoothCamToggled != null)
							{
								ReferenceMaster.onSmoothCamToggled();
							}
						},
						getDefault = () => defaultConfig.SmoothCamera ? 1 : 0
					},
					new OptionsCategory.BoolOption
					{
						NameLocID = 4872,
						getFunc = () => (!currentConfig.LimitCamera) ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.LimitCamera = x == 0;
						},
						getDefault = () => (!defaultConfig.LimitCamera) ? 1 : 0
					}
				}
			});
			list2.Add(new OptionsCategory
			{
				NameLocID = 4902,
				options = new List<OptionsCategory.MenuOption>
				{
					new OptionsCategory.BoolOption
					{
						NameLocID = 4903,
						getFunc = () => currentConfig.AutoTimeScale ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.AutoTimeScale = x == 1;
						},
						getDefault = () => defaultConfig.AutoTimeScale ? 1 : 0
					},
					new OptionsCategory.ValueOption
					{
						NameLocID = 4904,
						getFunc = () => currentConfig.MinTimeScale,
						setFunc = delegate(float x)
						{
							currentConfig.MinTimeScale = x;
						},
						getDefault = () => defaultConfig.MinTimeScale,
						Min = 1f,
						Max = 100f
					},
					new OptionsCategory.ValueOption
					{
						NameLocID = 4905,
						getFunc = () => currentConfig.MaxTimeScale,
						setFunc = delegate(float x)
						{
							currentConfig.MaxTimeScale = x;
						},
						getDefault = () => defaultConfig.MaxTimeScale,
						Min = 1f,
						Max = 100f
					}
				}
			});
			list2.Add(new OptionsCategory
			{
				NameLocID = 5006,
				options = new List<OptionsCategory.MenuOption>
				{
					new OptionsCategory.BoolOption
					{
						NameLocID = 5007,
						getFunc = () => currentConfig.AutosaveEnabled ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.AutosaveEnabled = x == 1;
						},
						getDefault = () => defaultConfig.AutosaveEnabled ? 1 : 0
					},
					new OptionsCategory.ValueOption
					{
						NameLocID = 5008,
						getFunc = () => currentConfig.AutosaveDeleteAfterDays,
						setFunc = delegate(float x)
						{
							currentConfig.AutosaveDeleteAfterDays = Mathf.RoundToInt(x);
						},
						getDefault = () => defaultConfig.AutosaveDeleteAfterDays,
						Min = 1f,
						Max = 360f
					},
					new OptionsCategory.ValueOption
					{
						NameLocID = 5009,
						getFunc = () => currentConfig.AutosaveMaxFiles,
						setFunc = delegate(float x)
						{
							currentConfig.AutosaveMaxFiles = Mathf.RoundToInt(x);
						},
						getDefault = () => defaultConfig.AutosaveMaxFiles,
						Min = 5f,
						Max = 500f
					},
					new OptionsCategory.BoolOption
					{
						NameLocID = 5026,
						getFunc = () => currentConfig.SavePreviousVersionsEnabled ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.SavePreviousVersionsEnabled = x == 1;
						},
						getDefault = () => defaultConfig.SavePreviousVersionsEnabled ? 1 : 0
					},
					new OptionsCategory.ValueOption
					{
						NameLocID = 5008,
						getFunc = () => currentConfig.VersionDeleteAfterDays,
						setFunc = delegate(float x)
						{
							currentConfig.VersionDeleteAfterDays = Mathf.RoundToInt(x);
						},
						getDefault = () => defaultConfig.VersionDeleteAfterDays,
						Min = 1f,
						Max = 360f
					},
					new OptionsCategory.ValueOption
					{
						NameLocID = 5009,
						getFunc = () => currentConfig.VersionMaxFiles,
						setFunc = delegate(float x)
						{
							currentConfig.VersionMaxFiles = Mathf.RoundToInt(x);
						},
						getDefault = () => defaultConfig.VersionMaxFiles,
						Min = 5f,
						Max = 500f
					}
				}
			});
			list = list2;
			break;
		}
		case OptionsTab.Graphics:
		{
			List<OptionsCategory> list2 = new List<OptionsCategory>();
			list2.Add(new OptionsCategory
			{
				NameLocID = 4664,
				options = new List<OptionsCategory.MenuOption>
				{
					new OptionsCategory.EnumOption
					{
						NameLocID = 3410,
						optionLocIDs = new int[5] { 2142, 43, 3443, 3444, 3445 },
						getFunc = () => (int)currentConfig.AntiAliasingMode,
						setFunc = delegate(int x)
						{
							currentConfig.AntiAliasingMode = (AAMode)x;
							if (x != 0)
							{
								OptionsMaster.FormerAntiAliasingMode = (AAMode)x;
							}
							if (ReferenceMaster.onAAChanged != null)
							{
								ReferenceMaster.onAAChanged();
							}
						},
						getDefault = () => (int)defaultConfig.AntiAliasingMode
					},
					new OptionsCategory.BoolOption
					{
						NameLocID = 44,
						getFunc = () => currentConfig.DepthOfField ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.DepthOfField = x == 1;
							if (ReferenceMaster.onDOFChanged != null)
							{
								ReferenceMaster.onDOFChanged();
							}
						},
						getDefault = () => defaultConfig.DepthOfField ? 1 : 0
					},
					new OptionsCategory.BoolOption
					{
						NameLocID = 46,
						getFunc = () => currentConfig.Vignette ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.Vignette = x == 1;
							if (ReferenceMaster.onVignetteChanged != null)
							{
								ReferenceMaster.onVignetteChanged();
							}
						},
						getDefault = () => defaultConfig.Vignette ? 1 : 0
					},
					new OptionsCategory.ValueOption
					{
						NameLocID = 4665,
						getFunc = () => currentConfig.Saturation,
						setFunc = delegate(float x)
						{
							currentConfig.Saturation = x;
							if (ReferenceMaster.onSaturationChanged != null)
							{
								ReferenceMaster.onSaturationChanged();
							}
						},
						getDefault = () => defaultConfig.Saturation,
						Min = 0f,
						Max = 100f
					}
				}
			});
			list2.Add(new OptionsCategory
			{
				NameLocID = 3409,
				options = new List<OptionsCategory.MenuOption>
				{
					new OptionsCategory.BoolOption
					{
						NameLocID = 4411,
						getFunc = () => currentConfig.DeformMeshes ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.DeformMeshes = x == 1;
							OptionsMaster.SetShaderDeform();
						},
						getDefault = () => defaultConfig.DeformMeshes ? 1 : 0
					},
					new OptionsCategory.EnumOption
					{
						NameLocID = 4431,
						optionLocIDs = ((SystemInfo.graphicsShaderLevel < 30) ? new int[1] { 4437 } : new int[6] { 4437, 4432, 4433, 4434, 4435, 4436 }),
						getFunc = () => currentConfig.ReflectionQuality,
						setFunc = delegate(int x)
						{
							currentConfig.ReflectionQuality = x;
							if (ReferenceMaster.onReflectionQualityChanged != null)
							{
								ReferenceMaster.onReflectionQualityChanged();
							}
						},
						getDefault = () => defaultConfig.ReflectionQuality,
						hidden = (DlcManager.Instance.GetDlcStatus(DlcManager.DlcType.Water) == DlcManager.DlcStatusType.MissingDlc)
					},
					new OptionsCategory.BoolOption
					{
						NameLocID = 4619,
						getFunc = () => currentConfig.Rippling ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.Rippling = x == 1;
							OptionsMaster.SetShaderRippling();
						},
						getDefault = () => defaultConfig.Rippling ? 1 : 0
					},
					new OptionsCategory.BoolOption
					{
						NameLocID = 4873,
						getFunc = () => currentConfig.WaterCannonRippling ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.WaterCannonRippling = x == 1;
						},
						getDefault = () => defaultConfig.WaterCannonRippling ? 1 : 0
					}
				}
			});
			list2.Add(new OptionsCategory
			{
				NameLocID = 3447,
				options = new List<OptionsCategory.MenuOption>
				{
					new OptionsCategory.EnumOption
					{
						NameLocID = 3406,
						optionLocIDs = new int[4] { 3438, 3439, 3440, 3441 },
						getFunc = () => currentConfig.TextureQuality,
						setFunc = delegate(int x)
						{
							currentConfig.TextureQuality = x;
							if (ReferenceMaster.onTextureQualityChanged != null)
							{
								ReferenceMaster.onTextureQualityChanged();
							}
						},
						getDefault = () => defaultConfig.TextureQuality
					},
					new OptionsCategory.EnumOption
					{
						NameLocID = 3449,
						optionLocIDs = new int[3] { 3450, 3451, 3452 },
						getFunc = () => (int)currentConfig.AnisoFilter,
						setFunc = delegate(int x)
						{
							currentConfig.AnisoFilter = (AnisotropicFiltering)x;
							if (ReferenceMaster.onAnisoChanged != null)
							{
								ReferenceMaster.onAnisoChanged();
							}
						},
						getDefault = () => (int)defaultConfig.AnisoFilter
					}
				}
			});
			list2.Add(new OptionsCategory
			{
				NameLocID = 41,
				options = new List<OptionsCategory.MenuOption>
				{
					new OptionsCategory.BoolOption
					{
						NameLocID = 41,
						getFunc = () => currentConfig.ShadowsEnabled ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.ShadowsEnabled = x == 1;
							if (ReferenceMaster.onShadowsChanged != null)
							{
								ReferenceMaster.onShadowsChanged();
							}
						},
						getDefault = () => defaultConfig.ShadowsEnabled ? 1 : 0
					},
					new OptionsCategory.EnumOption
					{
						NameLocID = 4228,
						optionLocIDs = new int[2] { 4229, 4230 },
						getFunc = () => currentConfig.HardShadows ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.HardShadows = ((x != 0) ? true : false);
							if (ReferenceMaster.onShadowsChanged != null)
							{
								ReferenceMaster.onShadowsChanged();
							}
						},
						getDefault = () => defaultConfig.HardShadows ? 1 : 0
					},
					new OptionsCategory.EnumOption
					{
						NameLocID = 3406,
						optionLocIDs = new int[4] { 3438, 3439, 3440, 3441 },
						getFunc = () => (int)currentConfig.ShadowRes,
						setFunc = delegate(int x)
						{
							currentConfig.ShadowRes = (ShadowResolution)x;
							if (ReferenceMaster.onShadowsChanged != null)
							{
								ReferenceMaster.onShadowsChanged();
							}
						},
						getDefault = () => (int)defaultConfig.ShadowRes
					},
					new OptionsCategory.EnumOption
					{
						NameLocID = 3458,
						optionLocIDs = new int[4] { 3438, 3439, 3440, 3441 },
						getFunc = () => (!currentConfig.ShadowsDoubled) ? (currentConfig.ShadowCascades / 2) : 3,
						setFunc = delegate(int x)
						{
							if (x == 3)
							{
								if (!currentConfig.ShadowsDoubled)
								{
									currentConfig.ShadowsDoubled = true;
									if (ReferenceMaster.onBlockShadowsChanged != null)
									{
										ReferenceMaster.onBlockShadowsChanged();
									}
								}
								currentConfig.ShadowCascades = 4;
							}
							else
							{
								if (currentConfig.ShadowsDoubled)
								{
									currentConfig.ShadowsDoubled = false;
									if (ReferenceMaster.onBlockShadowsChanged != null)
									{
										ReferenceMaster.onBlockShadowsChanged();
									}
								}
								currentConfig.ShadowCascades = x * 2;
							}
							if (ReferenceMaster.onShadowsChanged != null)
							{
								ReferenceMaster.onShadowsChanged();
							}
						},
						getDefault = () => (!defaultConfig.ShadowsDoubled) ? (defaultConfig.ShadowCascades / 2) : 3
					},
					new OptionsCategory.ValueOption
					{
						NameLocID = 3453,
						getFunc = () => currentConfig.ShadowRenderDistance,
						setFunc = delegate(float x)
						{
							currentConfig.ShadowRenderDistance = x;
							if (ReferenceMaster.onShadowsChanged != null)
							{
								ReferenceMaster.onShadowsChanged();
							}
						},
						getDefault = () => defaultConfig.ShadowRenderDistance,
						Min = 0f,
						Max = 800f
					}
				}
			});
			list2.Add(new OptionsCategory
			{
				NameLocID = 42,
				options = new List<OptionsCategory.MenuOption>
				{
					new OptionsCategory.BoolOption
					{
						NameLocID = 42,
						getFunc = () => currentConfig.ScreenSpaceAmbientOcclusion ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.ScreenSpaceAmbientOcclusion = x == 1;
							if (ReferenceMaster.onSSAOChanged != null)
							{
								ReferenceMaster.onSSAOChanged();
							}
						},
						getDefault = () => defaultConfig.ScreenSpaceAmbientOcclusion ? 1 : 0
					},
					new OptionsCategory.EnumOption
					{
						NameLocID = 3448,
						optionLocIDs = new int[5] { 3437, 3438, 3439, 3440, 3441 },
						getFunc = () => (int)currentConfig.SSAOQuality,
						setFunc = delegate(int x)
						{
							currentConfig.SSAOQuality = (OptionsMaster.Tier)x;
							if (ReferenceMaster.onSSAOChanged != null)
							{
								ReferenceMaster.onSSAOChanged();
							}
						},
						getDefault = () => (int)defaultConfig.SSAOQuality
					},
					new OptionsCategory.ValueOption
					{
						NameLocID = 3446,
						getFunc = () => currentConfig.SSAOIntensity,
						setFunc = delegate(float x)
						{
							currentConfig.SSAOIntensity = x;
							if (ReferenceMaster.onSSAOChanged != null)
							{
								ReferenceMaster.onSSAOChanged();
							}
						},
						getDefault = () => defaultConfig.SSAOIntensity,
						Min = 0f,
						Max = 100f
					}
				}
			});
			list2.Add(new OptionsCategory
			{
				NameLocID = 45,
				options = new List<OptionsCategory.MenuOption>
				{
					new OptionsCategory.BoolOption
					{
						NameLocID = 45,
						getFunc = () => currentConfig.Bloom ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.Bloom = x == 1;
							if (ReferenceMaster.onBloomChanged != null)
							{
								ReferenceMaster.onBloomChanged();
							}
						},
						getDefault = () => defaultConfig.Bloom ? 1 : 0
					},
					new OptionsCategory.ValueOption
					{
						NameLocID = 3446,
						getFunc = () => currentConfig.BloomIntensity,
						setFunc = delegate(float x)
						{
							currentConfig.BloomIntensity = x;
							if (ReferenceMaster.onBloomChanged != null)
							{
								ReferenceMaster.onBloomChanged();
							}
						},
						getDefault = () => defaultConfig.BloomIntensity,
						Min = 0f,
						Max = 100f
					}
				}
			});
			list = list2;
			break;
		}
		case OptionsTab.Sound:
		{
			List<OptionsCategory> list2 = new List<OptionsCategory>();
			list2.Add(new OptionsCategory
			{
				NameLocID = 3412,
				options = new List<OptionsCategory.MenuOption>
				{
					new OptionsCategory.ValueOption
					{
						NameLocID = 3434,
						getFunc = () => currentConfig.MasterVolume,
						setFunc = delegate(float x)
						{
							AudioListener.volume = x / 100f;
							currentConfig.MasterVolume = x;
						},
						getDefault = delegate
						{
							AudioListener.volume = defaultConfig.MasterVolume / 100f;
							return defaultConfig.MasterVolume;
						},
						Min = 0f,
						Max = 100f
					}
				}
			});
			list2.Add(new OptionsCategory
			{
				NameLocID = 2103,
				options = new List<OptionsCategory.MenuOption>
				{
					new OptionsCategory.BoolOption
					{
						NameLocID = 2181,
						getFunc = () => currentConfig.MusicEnabled ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.MusicEnabled = x == 1;
							if (!currentConfig.MusicEnabled)
							{
								SingleInstance<MusicController>.Instance.Mute();
							}
							else
							{
								SingleInstance<MusicController>.Instance.Resume();
							}
						},
						getDefault = () => defaultConfig.MusicEnabled ? 1 : 0
					},
					new OptionsCategory.BoolOption
					{
						NameLocID = 4653,
						getFunc = () => currentConfig.DuckVolumeUnfocused ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.DuckVolumeUnfocused = x == 1;
						},
						getDefault = () => defaultConfig.DuckVolumeUnfocused ? 1 : 0
					},
					new OptionsCategory.ValueOption
					{
						NameLocID = 3454,
						getFunc = () => currentConfig.MusicVolume,
						setFunc = delegate(float x)
						{
							currentConfig.MusicVolume = x;
						},
						getDefault = () => defaultConfig.MusicVolume,
						Min = 0f,
						Max = 100f
					},
					new OptionsCategory.ValueOption
					{
						NameLocID = 4604,
						getFunc = () => currentConfig.AmbientVolume,
						setFunc = delegate(float x)
						{
							currentConfig.AmbientVolume = x;
						},
						getDefault = () => defaultConfig.AmbientVolume,
						Min = 0f,
						Max = 100f
					},
					new OptionsCategory.ValueOption
					{
						NameLocID = 4051,
						getFunc = () => currentConfig.UIVolume,
						setFunc = delegate(float x)
						{
							currentConfig.UIVolume = x;
						},
						getDefault = () => defaultConfig.UIVolume,
						Min = 0f,
						Max = 100f
					},
					new OptionsCategory.ValueOption
					{
						NameLocID = 4052,
						getFunc = () => currentConfig.SfxVolume,
						setFunc = delegate(float x)
						{
							currentConfig.SfxVolume = x;
						},
						getDefault = () => defaultConfig.SfxVolume,
						Min = 0f,
						Max = 100f
					},
					new OptionsCategory.ValueOption
					{
						NameLocID = 4603,
						getFunc = () => currentConfig.PhysicsVolume,
						setFunc = delegate(float x)
						{
							currentConfig.PhysicsVolume = x;
						},
						getDefault = () => defaultConfig.PhysicsVolume,
						Min = 0f,
						Max = 150f
					},
					new OptionsCategory.ValueOption
					{
						NameLocID = 4602,
						getFunc = () => currentConfig.BlockVolume,
						setFunc = delegate(float x)
						{
							currentConfig.BlockVolume = x;
						},
						getDefault = () => defaultConfig.BlockVolume,
						Min = 0f,
						Max = 150f
					},
					new OptionsCategory.BoolOption
					{
						NameLocID = 4596,
						getFunc = () => currentConfig.SfxDistanceFX ? 1 : 0,
						setFunc = delegate(int x)
						{
							currentConfig.SfxDistanceFX = x == 1;
							if (ReferenceMaster.onAudioReverbToggled != null)
							{
								ReferenceMaster.onAudioReverbToggled();
							}
						},
						getDefault = () => defaultConfig.SfxDistanceFX ? 1 : 0
					}
				}
			});
			list = list2;
			break;
		}
		case OptionsTab.Controls:
		{
			list = new List<OptionsCategory>();
			OptionsCategory optionsCategory = new OptionsCategory();
			optionsCategory.NameLocID = 3025;
			OptionsCategory optionsCategory2 = optionsCategory;
			list.Add(optionsCategory2);
			FillKeyCategory(optionsCategory2, OptionsMaster.CustomControls.General, OptionsMaster.DefaultControls.General);
			optionsCategory = new OptionsCategory();
			optionsCategory.NameLocID = 1940;
			OptionsCategory optionsCategory3 = optionsCategory;
			list.Add(optionsCategory3);
			FillKeyCategory(optionsCategory3, OptionsMaster.CustomControls.Building, OptionsMaster.DefaultControls.Building);
			optionsCategory = new OptionsCategory();
			optionsCategory.NameLocID = 3397;
			OptionsCategory optionsCategory4 = optionsCategory;
			list.Add(optionsCategory4);
			FillKeyCategory(optionsCategory4, OptionsMaster.CustomControls.AdvancedBuilding, OptionsMaster.DefaultControls.AdvancedBuilding);
			optionsCategory = new OptionsCategory();
			optionsCategory.NameLocID = 1779;
			OptionsCategory optionsCategory5 = optionsCategory;
			list.Add(optionsCategory5);
			FillKeyCategory(optionsCategory5, OptionsMaster.CustomControls.LevelEditor, OptionsMaster.DefaultControls.LevelEditor);
			optionsCategory = new OptionsCategory();
			optionsCategory.NameLocID = 5092;
			optionsCategory.SubtitleLocID = ((!StatMaster.isMainMenu) ? 5094 : 5093);
			optionsCategory.SubtitleClicked = ((!StatMaster.isMainMenu) ? new Action(OnDefaultBlocksSubtitleClicked) : null);
			OptionsCategory optionsCategory6 = optionsCategory;
			list.Add(optionsCategory6);
			FillKeyCategory(optionsCategory6, OptionsMaster.CustomControls.Blocks, OptionsMaster.DefaultControls.Blocks);
			if (ModKeys.Keys.Count > 0)
			{
				optionsCategory = new OptionsCategory();
				optionsCategory.NameLocID = 504;
				OptionsCategory optionsCategory7 = optionsCategory;
				list.Add(optionsCategory7);
				FillModKeyCategory(optionsCategory7);
			}
			break;
		}
		default:
			list = new List<OptionsCategory>();
			break;
		}
		return list;
	}

	private void OnDefaultBlocksSubtitleClicked()
	{
		Close();
		Machine machine = Machine.Active();
		if ((bool)machine && !machine.isSimulating && OverviewBlockMapper.CurrentInstance == null)
		{
			OverviewBlockMapper.Open(machine);
		}
	}

	private void FillKeyCategory(OptionsCategory cat, ControlScheme.ControlEntry[] entries, ControlScheme.ControlEntry[] defaults)
	{
		List<OptionsCategory.MenuOption> options = new List<OptionsCategory.MenuOption>();
		cat.options = options;
		for (int i = 0; i < entries.Length; i++)
		{
			ControlScheme.ControlEntry currentEntry = entries[i];
			ControlScheme.ControlEntry defaultEntry = defaults[i];
			OptionsCategory.ControlsOption item = FillControlEntry(currentEntry, defaultEntry);
			cat.options.Add(item);
		}
	}

	private void FillModKeyCategory(OptionsCategory cat)
	{
		List<OptionsCategory.MenuOption> options = new List<OptionsCategory.MenuOption>();
		cat.options = options;
		Dictionary<ModContainer, Dictionary<string, ModKey>> keys = ModKeys.Keys;
		foreach (KeyValuePair<ModContainer, Dictionary<string, ModKey>> item2 in keys)
		{
			string text = item2.Key.Info.Name;
			foreach (KeyValuePair<string, ModKey> item3 in item2.Value)
			{
				string key = item3.Key;
				ModKey value = item3.Value;
				ModInfo.KeyInfo keyInfo = null;
				List<ModInfo.KeyInfo> keys2 = item2.Key.Info.Keys;
				for (int i = 0; i < keys2.Count; i++)
				{
					if (keys2[i].Name == key)
					{
						keyInfo = keys2[i];
						break;
					}
				}
				if (keyInfo == null)
				{
					Debug.LogWarning("[MainOptionsMenu]: Modding key def is null for " + text + "/" + key);
					continue;
				}
				ControlScheme.ControlEntry currentEntry = ControlScheme.ModKeyToControlEntry(text + ": " + key, 0, value, value.Change);
				ControlScheme.ControlEntry defaultEntry = ControlScheme.ModKeyToControlEntry(text + ": " + keyInfo.Name, 0, keyInfo.DefaultModifier, keyInfo.DefaultTrigger);
				OptionsCategory.ControlsOption item = FillControlEntry(currentEntry, defaultEntry);
				cat.options.Add(item);
			}
		}
	}

	private OptionsCategory.ControlsOption FillControlEntry(ControlScheme.ControlEntry currentEntry, ControlScheme.ControlEntry defaultEntry)
	{
		OptionsCategory.ControlsOption controlsOption = new OptionsCategory.ControlsOption();
		controlsOption.name = currentEntry.Name;
		controlsOption.NameLocID = currentEntry.NameLocID;
		controlsOption.SplitLocID = currentEntry.SplitLocID;
		controlsOption.setFunc = delegate(ControlScheme.ControlEntry x)
		{
			if (x.Options.Length != currentEntry.Options.Length)
			{
				Debug.LogWarning("Keys Set to unmatching options, either the rebinding system encountered an error or the controls xml was modified.");
				ControlScheme.ControlOption[] options = currentEntry.Options;
				currentEntry.Options = new ControlScheme.ControlOption[x.Options.Length];
				for (int i = 0; i < x.Options.Length; i++)
				{
					currentEntry.Options[i] = options[i];
					currentEntry.Options[i].Set(x.Options[i].Keys);
				}
			}
			else
			{
				for (int j = 0; j < x.Options.Length; j++)
				{
					currentEntry.Options[j].Set(x.Options[j].Keys);
				}
			}
		};
		controlsOption.getFunc = () => currentEntry;
		controlsOption.getDefault = () => defaultEntry;
		return controlsOption;
	}

	private void OnTabClicked(OptionsTab tab)
	{
		if (currentTab != tab)
		{
			OpenMenu(tab);
		}
	}

	private void Initialize()
	{
		if (initialized)
		{
			return;
		}
		tabData = new TabData[tabs.Length];
		for (int i = 0; i < tabs.Length; i++)
		{
			OptionsTab tab = (OptionsTab)i;
			UIButton uIButton = tabs[i];
			List<OptionsCategory> categoryList = GetCategoryList(tab);
			tabData[i] = new TabData
			{
				button = uIButton,
				text = uIButton.GetComponent<TextMesh>(),
				categories = categoryList
			};
			uIButton.Click += delegate
			{
				OnTabClicked(tab);
			};
		}
		ClampMonitorOptions();
		optionsContainer.Initialize(OPTIONS_FOLDER);
		initialized = true;
	}

	private void ClampMonitorOptions()
	{
		int num = Mathf.Min(Display.displays.Length, 8);
		OptionsCategory.EnumOption enumOption = tabData[0].categories[1].options[0] as OptionsCategory.EnumOption;
		int[] optionLocIDs = enumOption.optionLocIDs;
		int[] array = new int[num];
		Array.Copy(optionLocIDs, array, num);
		enumOption.optionLocIDs = array;
	}

	private void Close()
	{
		if (OptionsMaster.scrollBindingEnabled)
		{
			StatMaster.allowScrollRebind = true;
		}
		base.gameObject.SetActive(false);
	}

	private void OpenMenu(OptionsTab tab)
	{
		if (tab != currentTab)
		{
			optionsContainer.optionsCategory.Close();
		}
		for (int i = 0; i < tabData.Length; i++)
		{
			Color color = tabData[i].text.color;
			tabData[i].text.color = new Color(color.r, color.g, color.b, (i != (int)tab) ? 0.7f : 1f);
			tabData[i].text.fontStyle = FontStyle.Normal;
		}
		optionsContainer.Rebuild(tabData[(int)tab].categories);
		optionsContainer.gameObject.SetLayerRecursively(widgetLayer);
		scrollbar.ResetContentPos();
		scrollbar.UpdateBounds();
		currentTab = tab;
		CurrentInstance = this;
		HasInstance = true;
	}

	protected void Update()
	{
		if (InputManager.CloseKey())
		{
			Close();
		}
	}

	protected void LateUpdate()
	{
		if (!OptionsMaster.scrollBindingEnabled || currentTab != OptionsTab.Controls)
		{
			return;
		}
		if (Input.mouseScrollDelta.y != 0f)
		{
			if (StatMaster.allowScrollRebind)
			{
				StatMaster.allowScrollRebind = false;
			}
			disableTimer = 0f;
		}
		else if (!StatMaster.allowScrollRebind)
		{
			disableTimer += Time.unscaledDeltaTime;
			if (disableTimer >= OptionsMaster.scrollDisableTime)
			{
				StatMaster.allowScrollRebind = true;
			}
		}
	}
}
