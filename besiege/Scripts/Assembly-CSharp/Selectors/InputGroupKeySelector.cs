using System;
using System.Collections.Generic;
using UnityEngine;

namespace Selectors
{
	public class InputGroupKeySelector : KeyChangeSelector
	{
		public Action<int, KeyCode> OnChangeOther;

		public Material otherMaterial;

		public bool showOtherKeys;

		private List<KeyCode> otherKeys;

		private List<KeySelectorExtender> otherWidgets = new List<KeySelectorExtender>();

		public GameObject moreKeysGO;

		private int MAX_TOTAL_KEYS = 5;

		private int indexOffset;

		public void Init(List<KeyCode> other)
		{
			otherKeys = other;
			Init();
		}

		protected override void Add()
		{
			base.Add();
			MoveOtherKeys();
		}

		protected override void Clear()
		{
			base.Clear();
			moreKeysGO.SetActive(false);
			otherWidgets.Clear();
		}

		private void MoveOtherKeys()
		{
			int count = keys.Count;
			for (int i = 0; i < otherWidgets.Count; i++)
			{
				otherWidgets[i].transform.localPosition = GetKeyOffset(count + i);
			}
		}

		public override int ChangeKey(int index, KeyCode keyCode)
		{
			if (base.Key != null && index <= keys.Count)
			{
				return base.ChangeKey(index, keyCode);
			}
			int num = index - (base.Key.KeysCount + 1);
			if (num < otherKeys.Count && OnChangeOther != null)
			{
				OnChangeOther(num, keyCode);
			}
			return index;
		}

		public override void RemoveKey(int index)
		{
			if (base.Key != null && index < keys.Count)
			{
				base.RemoveKey(index);
				return;
			}
			int num = index - (base.Key.KeysCount + 1);
			if (num < otherKeys.Count && OnChangeOther != null)
			{
				OnChangeOther(num, KeyCode.None);
			}
		}

		protected override void UpdateVisual()
		{
			Clear();
			indexOffset = 0;
			if (base.Key.IsEmpty())
			{
				Add(KeyCode.None);
			}
			else
			{
				int num = ((MAX_TOTAL_KEYS >= base.Key.KeysCount) ? base.Key.KeysCount : MAX_TOTAL_KEYS);
				for (int i = 0; i < num; i++)
				{
					Add(base.Key.GetKey(i));
				}
				indexOffset += base.Key.KeysCount;
			}
			bool active = base.Key.KeysCount > MAX_TOTAL_KEYS;
			if (showOtherKeys)
			{
				int num2 = ((base.Key.KeysCount < MAX_TOTAL_KEYS) ? (MAX_TOTAL_KEYS - base.Key.KeysCount) : 0);
				for (int j = 0; j < otherKeys.Count; j++)
				{
					if (indexOffset > num2)
					{
						active = true;
						break;
					}
					KeyCode k = otherKeys[j];
					KeySelectorExtender keySelectorExtender = CreateKey(indexOffset, k);
					otherWidgets.Add(keySelectorExtender);
					keySelectorExtender.SetUp(this, hoverText, indexOffset + 1, otherKeys[j]);
					keySelectorExtender.SetNormalMaterial(otherMaterial);
					indexOffset++;
				}
			}
			moreKeysGO.SetActive(active);
		}

		protected override Vector3 GetAddOffset()
		{
			return GetKeyOffset(Mathf.Min(keys.Count + (showOtherKeys ? otherKeys.Count : 0), MAX_TOTAL_KEYS - 1) - 1) + container.transform.localPosition;
		}
	}
}
