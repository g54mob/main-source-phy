using System.Collections.Generic;
using Localisation;
using UnityEngine;

namespace Selectors
{
	public class ControlSelector : KeySelector
	{
		[SerializeField]
		private GameObject splitGO;

		protected ControlScheme.ControlEntry Entry;

		protected Dictionary<ControlScheme.ControlOption, List<KeySelectorOptionComp>> optionToUI;

		protected int splitLocID;

		private List<GameObject> otherWidgets = new List<GameObject>();

		protected override void Clear()
		{
			base.Clear();
			for (int i = 0; i < otherWidgets.Count; i++)
			{
				Object.Destroy(otherWidgets[i]);
			}
			otherWidgets.Clear();
		}

		protected new KeySelectorOptionComp CreateKey(int index, KeyCode k)
		{
			KeySelectorOptionComp keySelectorOptionComp = Object.Instantiate(keyPrefab, container) as KeySelectorOptionComp;
			keySelectorOptionComp.transform.localPosition = GetKeyOffset(index);
			return keySelectorOptionComp;
		}

		public void SetAllChanging(ControlScheme.ControlOption option)
		{
			for (int num = optionToUI[option].Count - 1; num >= 0; num--)
			{
				optionToUI[option][num].Changing();
			}
		}

		public void SetAllHovered(ControlScheme.ControlOption option, bool hovered)
		{
			for (int num = optionToUI[option].Count - 1; num >= 0; num--)
			{
				optionToUI[option][num].Hover(hovered);
			}
		}

		public void UpdateOptions()
		{
			Clear();
			float offset = 0f;
			int keyIndex = 0;
			int num = optionToUI.Count - 1;
			Dictionary<ControlScheme.ControlOption, List<KeySelectorOptionComp>> dictionary = new Dictionary<ControlScheme.ControlOption, List<KeySelectorOptionComp>>(optionToUI);
			optionToUI = new Dictionary<ControlScheme.ControlOption, List<KeySelectorOptionComp>>();
			foreach (KeyValuePair<ControlScheme.ControlOption, List<KeySelectorOptionComp>> item in dictionary)
			{
				DisplayOption(item.Key, num, ref keyIndex, ref offset);
				num--;
			}
		}

		public void DisplayOptions(int splitLocID, ControlScheme.ControlEntry entry)
		{
			Clear();
			Entry = entry;
			optionToUI = new Dictionary<ControlScheme.ControlOption, List<KeySelectorOptionComp>>();
			this.splitLocID = splitLocID;
			float offset = 0f;
			int keyIndex = 0;
			for (int num = entry.Options.Length - 1; num >= 0; num--)
			{
				DisplayOption(entry.Options[num], num, ref keyIndex, ref offset);
			}
		}

		protected void DisplayOption(ControlScheme.ControlOption currentOption, int i, ref int keyIndex, ref float offset)
		{
			optionToUI.Add(currentOption, new List<KeySelectorOptionComp>());
			for (int num = currentOption.Keys.Length - 1; num >= 0; num--)
			{
				KeyCode keyCode = currentOption.Keys[num];
				KeySelectorOptionComp keySelectorOptionComp = CreateKey(keyIndex, keyCode);
				keySelectorOptionComp.SetUp(this, null, keyIndex++, keyCode, Entry, currentOption, i, num);
				keySelectorOptionComp.transform.localPosition = new Vector3(0f - offset, 0f, 0f);
				offset += spacer;
				keys.Add(keySelectorOptionComp);
				optionToUI[currentOption].Add(keySelectorOptionComp);
			}
			if (i > 0)
			{
				if (splitLocID != -1)
				{
					GameObject gameObject = Object.Instantiate(splitGO, base.transform) as GameObject;
					string translation = LocalisationManager.GetTranslation(splitLocID);
					float num2 = spacer * 0.1f;
					DynamicText component = gameObject.GetComponent<DynamicText>();
					ReferenceMaster.SetDynamicText(component, translation);
					float num3 = component.GetComponent<MeshRenderer>().bounds.size.x + num2;
					gameObject.transform.localPosition = new Vector3(0f - (offset - spacer * 0.5f + num3 / 2f), 0f, 0f);
					offset += num3;
					otherWidgets.Add(gameObject);
				}
				else
				{
					offset += spacer * 0.5f;
				}
			}
		}
	}
}
