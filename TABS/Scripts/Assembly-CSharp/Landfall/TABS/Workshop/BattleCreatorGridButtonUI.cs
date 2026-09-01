using System;
using System.Collections;
using System.IO;
using ModIO;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS.Workshop
{
	public class BattleCreatorGridButtonUI : BattleCreatorAssetUICellBase, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
	{
		[Header("Settings")]
		public float m_AlphaLight = 0.2f;

		public float m_AlphaDark = 1f;

		public AnimationCurve m_AnimationCurve = AnimationCurve.EaseInOut(0f, 0.65f, 0.15f, 1f);

		[Header("Refrences")]
		public RawImage m_Fade;

		public TextMeshProUGUI m_BattleName;

		public LocalizeText m_LoadText;

		public LocalizeText m_ContextText;

		public GameObject m_HoverParent;

		public GameObject m_OptionsMenu;

		public Button m_CogButton;

		public Button m_ContextButton;

		public Button m_UploadButton;

		public Button m_DeleteButton;

		public Button m_LoadButton;

		public Image m_ContextButtonImage;

		public Sprite m_EditSprite;

		public Sprite m_UploadSprite;

		private Image m_ContentImage;

		private float timeAtLastAlphaDirty;

		private float m_TargetAlpha;

		private bool m_IsDoingCorutine;

		private string m_LevelName;

		private string m_ContextName;

		private SimpleButton m_CogSimpleButton;

		private SimpleButton m_ContextSimpleButton;

		private SimpleButton m_UploadSimpleButton;

		private SimpleButton m_DeleteSimpleButton;

		private FileIOWrapper m_FileIO;

		private ModalPanel m_modalPanel;

		private float Alpha => m_Fade.color.a;

		private void Awake()
		{
			m_CogSimpleButton = m_CogButton.GetComponent<SimpleButton>();
			m_ContextSimpleButton = m_ContextButton.GetComponent<SimpleButton>();
			m_UploadSimpleButton = m_UploadButton.GetComponent<SimpleButton>();
			m_DeleteSimpleButton = m_DeleteButton.GetComponent<SimpleButton>();
		}

		private void InitRefrences()
		{
			m_TargetAlpha = m_AlphaLight;
			SetAlpha(m_AlphaLight);
			m_ContentImage = base.transform.Find("Image").GetComponent<Image>();
			m_FileIO = ServiceLocator.GetService<FileIOWrapper>();
			m_modalPanel = ServiceLocator.GetService<ModalPanel>();
			GetComponent<Button>().onClick.AddListener(delegate
			{
				if (!m_OptionsMenu.activeSelf)
				{
					m_OptionsMenu.SetActive(value: true);
				}
			});
		}

		private void OnEnter(bool withHover = true)
		{
			m_BattleName.gameObject.SetActive(value: true);
			if (withHover)
			{
				m_TargetAlpha = m_AlphaDark;
				timeAtLastAlphaDirty = Time.time;
				if (!m_IsDoingCorutine)
				{
					StartCoroutine(UpdateAlphaRoutine());
				}
			}
			else if (m_OptionsMenu.activeSelf)
			{
				if (m_CogSimpleButton.IsHighlighted)
				{
					PressCog();
				}
				else if (m_ContextSimpleButton.IsHighlighted)
				{
					PressContext();
				}
				else if (m_UploadSimpleButton.IsHighlighted)
				{
					PressUpload();
				}
			}
		}

		private void OnExit()
		{
			timeAtLastAlphaDirty = Time.time;
			m_TargetAlpha = m_AlphaLight;
			m_HoverParent.SetActive(value: false);
			m_BattleName.gameObject.SetActive(value: true);
			m_LoadText.gameObject.SetActive(value: false);
			if (!m_IsDoingCorutine)
			{
				StartCoroutine(UpdateAlphaRoutine());
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			OnEnter();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			OnExit();
		}

		public void OnSelect(BaseEventData eventData)
		{
		}

		public void OnDeselect(BaseEventData eventData)
		{
			OnExit();
		}

		private IEnumerator UpdateAlphaRoutine()
		{
			m_IsDoingCorutine = true;
			while (timeAtLastAlphaDirty < Time.time + 0.3f)
			{
				SetAlpha(Mathf.Lerp(Alpha, m_TargetAlpha, Time.deltaTime * 20f));
				yield return null;
			}
			m_IsDoingCorutine = false;
		}

		private void SetAlpha(float a)
		{
			Color color = m_Fade.color;
			color.a = a;
			m_Fade.color = color;
		}

		public override void Init(UpdateContentData data)
		{
			InitRefrences();
			base.ContentType = data.filter;
			m_ContextName = "Update";
			m_LevelName = data.levelName;
			m_BattleName.text = m_LevelName;
			base.LevelAsset = new TABSCampaignLevelAsset();
			base.LevelAsset.SetCustomUnit(data.modProfile.id, data.modProfile);
			base.LevelAsset.InitEntity();
			base.Description = data.modProfile?.summary;
			base.ModProfile = data.modProfile;
			base.ContentName = data.levelName;
			AddListeners(data.onClick, data.onRemove, null, null, null);
			base.ModID = base.LevelAsset.ModID;
			InitImage();
		}

		public override void Init(CampaignLevelData data)
		{
			InitRefrences();
			base.ContentType = data.filter;
			base.LevelAsset = data.level;
			base.Description = base.LevelAsset.ModProfile?.summary;
			m_LevelName = data.levelName;
			m_BattleName.text = m_LevelName;
			base.FullPath = base.LevelAsset.FilePath;
			base.FolderPath = Path.GetDirectoryName(base.FullPath);
			base.ContentName = data.levelName;
			Action<BattleCreatorAssetUICellBase> onClick = data.onClick;
			Action<BattleCreatorAssetUICellBase> onRemove = data.onRemove;
			Action<BattleCreatorAssetUICellBase> onCog = data.onCog;
			Action<BattleCreatorAssetUICellBase> onUpload = data.onUpload;
			Action<BattleCreatorAssetUICellBase> onLoad = data.onLoad;
			if (base.LevelAsset.IsModIOLevel)
			{
				onCog = null;
				onUpload = null;
				onRemove = null;
			}
			AddListeners(onClick, onRemove, onCog, onUpload, onLoad);
			base.ModID = base.LevelAsset.ModID;
			switch (data.battleState)
			{
			case BattleCreatorState.Load:
				m_ContextName = "LABEL_EDITLEVEL";
				m_ContextButtonImage.sprite = m_EditSprite;
				break;
			case BattleCreatorState.Upload:
				m_ContextName = "BUTTON_UPLOAD";
				m_ContextButtonImage.sprite = m_UploadSprite;
				break;
			}
			InitImage();
		}

		public override void Init(CampaignData data)
		{
			InitRefrences();
			base.ContentType = data.filter;
			base.CampaignAsset = data.campaign;
			base.Description = base.CampaignAsset.ModProfile?.summary;
			m_ContextName = "LABEL_EDITCAMPAIGN";
			m_LevelName = data.levelName;
			m_BattleName.text = m_LevelName;
			base.ContentName = data.levelName;
			base.FolderPath = data.campaign.FolderPath;
			base.FullPath = data.campaign.FilePath;
			Action<BattleCreatorAssetUICellBase> onClick = data.onClick;
			Action<BattleCreatorAssetUICellBase> onRemove = data.onRemove;
			Action<BattleCreatorAssetUICellBase> onCog = data.onCog;
			Action<BattleCreatorAssetUICellBase> onUpload = data.onUpload;
			Action<BattleCreatorAssetUICellBase> onLoad = data.onLoad;
			switch (data.battleState)
			{
			case BattleCreatorState.Load:
				m_ContextName = "LABEL_EDITCAMPAIGN";
				m_ContextButtonImage.sprite = m_EditSprite;
				break;
			case BattleCreatorState.Upload:
				m_ContextName = "BUTTON_UPLOAD";
				m_ContextButtonImage.sprite = m_UploadSprite;
				break;
			}
			if (base.CampaignAsset.IsModCampaign)
			{
				onCog = null;
				onUpload = null;
				onRemove = null;
				base.LevelAsset = base.CampaignAsset.LevelsInCampaign[0];
			}
			AddListeners(onClick, onRemove, onCog, onUpload, onLoad);
			base.ModID = base.CampaignAsset.ModID;
			InitImage();
		}

		public override void Init(UnitData data)
		{
			InitRefrences();
			base.ContentType = data.filter;
			base.UnitBluePrint = data.unitBlueprint;
			m_ContextName = "LABEL_EDITUNIT";
			m_LevelName = data.levelName;
			m_BattleName.text = m_LevelName;
			base.ContentName = data.levelName;
			AddListeners(data.onClick, data.onRemove, data.onCog, null, null);
			base.ModID = base.UnitBluePrint.ModID;
			InitImage();
		}

		protected override void AddListeners(Action<BattleCreatorAssetUICellBase> onClick, Action<BattleCreatorAssetUICellBase> onRemove, Action<BattleCreatorAssetUICellBase> onCog, Action<BattleCreatorAssetUICellBase> onUpload, Action<BattleCreatorAssetUICellBase> onLoad)
		{
			m_ContextButton.onClick.AddListener(delegate
			{
				onClick(this);
			});
			if (onUpload != null)
			{
				m_UploadButton.onClick.AddListener(delegate
				{
					onUpload(this);
				});
			}
			else
			{
				m_UploadButton.gameObject.SetActive(value: false);
			}
			if (onRemove == null)
			{
				m_DeleteButton.gameObject.SetActive(value: false);
			}
			else
			{
				m_DeleteButton.onClick.AddListener(delegate
				{
					onRemove(this);
				});
			}
			if (onLoad != null)
			{
				m_LoadButton.onClick.AddListener(delegate
				{
					onLoad(this);
				});
			}
			else
			{
				m_LoadButton.gameObject.SetActive(value: false);
			}
		}

		private void InitImage()
		{
			if (base.ModID > 2)
			{
				ModManager.GetModLogo(base.LevelAsset.ModProfile, LogoSize.Thumbnail_320x180, OnImageSuccess, OnImageError);
				return;
			}
			string path = base.FolderPath + "/Picture.png";
			SetLocalBattleImageSprite(m_FileIO, path, m_ContentImage);
		}

		private void OnImageError(WebRequestError obj)
		{
		}

		private void OnImageSuccess(Texture2D obj)
		{
			Sprite sprite = Sprite.Create(obj, new Rect(0f, 0f, obj.width, obj.height), Vector2.zero);
			m_ContentImage.sprite = sprite;
		}

		private void SetContextText(string text)
		{
			m_ContextText.LocaleID = text;
		}

		public void OnEnterContext()
		{
			SetContextText(m_ContextName);
		}

		public void OnExitContext()
		{
			SetContextText(string.Empty);
		}

		public void OnEnterCog()
		{
			SetContextText("LABEL_SETTINGS");
		}

		public void OnExitCog()
		{
			SetContextText(string.Empty);
		}

		public void PressContext()
		{
			Press(delegate
			{
				m_ContextButton.onClick.Invoke();
			});
		}

		public void PressCog()
		{
			Press(delegate
			{
				m_CogButton.onClick.Invoke();
			});
		}

		public void PressUpload()
		{
			Press(delegate
			{
				m_UploadButton.onClick.Invoke();
			});
		}

		public void OnEnterUpload()
		{
			SetContextText("BUTTON_UPLOAD");
		}

		public void OnExitUpload()
		{
			SetContextText(string.Empty);
		}

		public void PressDelete()
		{
			Press(delegate
			{
				m_DeleteButton.onClick.Invoke();
			});
		}

		public void OnEnterDelete()
		{
			SetContextText("BUTTON_DELETE");
		}

		public void OnExitDelete()
		{
			SetContextText(string.Empty);
		}

		public void OnEnterLoad()
		{
			SetContextText("LABEL_LOAD");
		}

		public void OnExitLoad()
		{
			SetContextText(string.Empty);
		}

		public void PressButton()
		{
			Press(delegate
			{
				GetComponent<Button>().onClick.Invoke();
			});
		}

		public void PressLoad()
		{
			Press(delegate
			{
				m_LoadButton.onClick.Invoke();
			});
		}

		private void Press(Action pressAction)
		{
			if (!m_modalPanel.IsPopupOpen)
			{
				pressAction();
			}
		}
	}
}
