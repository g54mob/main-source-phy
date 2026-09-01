using System.Collections.Generic;
using System.Linq;
using InternalModding.Mods;
using Localisation;
using UnityEngine;
using UnityEngine.UI;

namespace InternalModding.UI
{
	public class ModMismatchUI : MonoBehaviour
	{
		public Button CloseButton;

		public Button ExpandButton;

		public GameObject ScrollView;

		public Text TitleText;

		public GameObject TickObject;

		public GameObject CrossObject;

		public GameObject EntryTemplate;

		public GameObject MatchContainer;

		private static ModMismatchUI _instance;

		private List<ModList.Mod> mods;

		private List<ModMismatchEntry> entries;

		public static void Show(List<ModList.Mod> mismatches, bool showMatchButton)
		{
			if ((bool)_instance)
			{
				_instance.gameObject.SetActive(true);
				_instance.MatchContainer.SetActive(showMatchButton);
				_instance.SetMismatches(mismatches);
			}
		}

		public static void Hide()
		{
			if ((bool)_instance)
			{
				_instance.gameObject.SetActive(false);
			}
		}

		public static void UpdateState(ModContainer newlyLoadedMod = null)
		{
			if (!_instance || !_instance.isActiveAndEnabled)
			{
				return;
			}
			foreach (ModMismatchEntry entry in _instance.entries)
			{
				entry.UpdateSuccessState(newlyLoadedMod);
			}
		}

		public static void UpdateStateUI()
		{
			if ((bool)_instance && _instance.isActiveAndEnabled)
			{
				bool flag = _instance.entries.All((ModMismatchEntry e) => e.GetSuccessState());
				_instance.TickObject.SetActive(flag);
				_instance.CrossObject.SetActive(!flag);
				_instance.TitleText.text = LocalisationManager.GetTranslation((!flag) ? 3569 : 3568);
			}
		}

		private void OnModLoad(ModContainer mod)
		{
			UpdateState(mod);
		}

		public void Awake()
		{
			_instance = this;
			CloseButton.onClick.AddListener(delegate
			{
				ToggleScrollView(false);
			});
			ExpandButton.onClick.AddListener(delegate
			{
				ToggleScrollView(true);
			});
			ToggleScrollView(true);
			EntryTemplate.SetActive(false);
			base.gameObject.SetActive(false);
			ModManager.OnModLoad += OnModLoad;
			_instance.TitleText.text = LocalisationManager.GetTranslation(3569);
		}

		public void OnEnable()
		{
			TickObject.SetActive(false);
			CrossObject.SetActive(true);
		}

		public void OnDestroy()
		{
			ModManager.OnModLoad -= OnModLoad;
		}

		private void ToggleScrollView(bool toggle)
		{
			CloseButton.gameObject.SetActive(toggle);
			ExpandButton.gameObject.SetActive(!toggle);
			ScrollView.SetActive(toggle);
		}

		public void SetMismatches(List<ModList.Mod> mismatches)
		{
			mods = mismatches;
			RebuildList();
		}

		private void RebuildList()
		{
			if (entries != null)
			{
				foreach (ModMismatchEntry entry in entries)
				{
					Object.Destroy(entry.gameObject);
				}
			}
			entries = new List<ModMismatchEntry>();
			foreach (ModList.Mod mod in mods)
			{
				GameObject gameObject = (GameObject)Object.Instantiate(EntryTemplate, EntryTemplate.transform.parent);
				ModMismatchEntry component = gameObject.GetComponent<ModMismatchEntry>();
				component.Init(mod);
				gameObject.SetActive(true);
				entries.Add(component);
			}
		}
	}
}
