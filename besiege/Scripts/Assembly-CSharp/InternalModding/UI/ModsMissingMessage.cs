using System.Collections.Generic;
using System.Linq;
using InternalModding.Mods;
using Localisation;
using UnityEngine;

namespace InternalModding.UI
{
	public class ModsMissingMessage : MissingMessageBase
	{
		private static ModsMissingMessage _instance;

		public Transform bgOffsetter;

		public GameObject Entry;

		public TextMesh TitleText;

		private Vector3 startPos;

		protected override void Start()
		{
			startPos = parentObj.localPosition;
			_instance = this;
			base.Start();
		}

		public static void ShowMachine(List<ModList.Mod> missingMods)
		{
			if (_instance != null)
			{
				_instance.ShowList(missingMods, false);
			}
		}

		public static void ShowLevel(List<ModList.Mod> missingMods)
		{
			if (_instance != null)
			{
				_instance.ShowList(missingMods, true);
			}
		}

		private void ShowList(List<ModList.Mod> missingMods, bool level)
		{
			string text = LocalisationManager.GetTranslation((!level) ? 3574 : 3575);
			if (ReferenceMaster.IsPlatformReady())
			{
				text += LocalisationManager.GetTranslation(3576);
			}
			textMeshy.text = text;
			extend = 0f;
			foreach (Transform item in listContainer.transform)
			{
				Object.Destroy(item.gameObject);
			}
			entryRens.Clear();
			entryTexts.Clear();
			for (int i = 0; i < missingMods.Count; i++)
			{
				ModList.Mod mod = missingMods[i];
				GameObject gameObject = Object.Instantiate(Entry);
				gameObject.transform.position = listContainer.transform.position + Vector3.down * Entry.transform.localScale.y * i;
				gameObject.transform.parent = listContainer.transform;
				ModsMissingMessageEntry component = gameObject.GetComponent<ModsMissingMessageEntry>();
				entryRens.Add(component.Icon);
				entryTexts.Add(component.Text.GetComponent<MeshRenderer>());
				entryRens.AddRange(component.Tooltip.tooltipRenderers.Cast<MeshRenderer>());
				entryTexts.AddRange(component.Tooltip.textMeshRenderers);
				component.Setup(mod);
			}
			BG.transform.localScale = bgStartSize + Vector3.up * missingMods.Count * Entry.transform.localScale.y;
			Push(bgOffsetter.gameObject.activeInHierarchy);
			StopAllCoroutines();
			StartCoroutine(DoIt());
			if (playAudio)
			{
				GetComponent<AudioSource>().Play();
			}
		}

		public override bool Push(bool push)
		{
			float y = ((!push) ? startPos.y : (startPos.y - bgOffsetter.localScale.y - 0.1f));
			parentObj.localPosition = new Vector3(parentObj.localPosition.x, y, parentObj.localPosition.z);
			parentObjStartPos = parentObj.localPosition;
			return parentObj.gameObject.activeSelf;
		}
	}
}
