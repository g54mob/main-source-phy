using System;
using System.Collections.Generic;
using Selectors;
using UnityEngine;

namespace BlockMapperInternal
{
	public class KeyController : MonoBehaviour
	{
		private readonly List<MKey> keys = new List<MKey>();

		private readonly List<KeyContainer> containers = new List<KeyContainer>();

		public float EndPosition { get; private set; }

		public void RegisterKey(MKey key)
		{
			keys.Add(key);
			key.KeyCountChanged = (Action)Delegate.Combine(key.KeyCountChanged, new Action(KeyCountChanged));
		}

		public void Clear()
		{
			ClearCallbacks();
			foreach (KeyContainer container in containers)
			{
				if (container != null)
				{
					container.TerminateVariable();
					UnityEngine.Object.Destroy(container.gameObject);
				}
			}
			containers.Clear();
		}

		private void ClearCallbacks()
		{
			for (int i = 0; i < keys.Count; i++)
			{
				MKey mKey = keys[i];
				mKey.KeyCountChanged = (Action)Delegate.Remove(mKey.KeyCountChanged, new Action(KeyCountChanged));
			}
			keys.Clear();
		}

		protected void OnDestroy()
		{
			ClearCallbacks();
		}

		private void KeyCountChanged()
		{
			if ((bool)BlockMapper.CurrentInstance)
			{
				BlockMapper.CurrentInstance.Rebuild();
			}
		}

		public void Display(IWidgetContainer mapper, float startPosition)
		{
			EndPosition = startPosition;
			int num = 0;
			List<MKey> list = new List<MKey>();
			for (int i = 0; i < keys.Count; i++)
			{
				if (keys[i].DisplayInMapper)
				{
					list.Add(keys[i]);
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				bool flag = j % 2 == 0;
				int num2 = j + (flag ? 1 : (-1));
				bool flag2 = num2 < list.Count;
				bool flag3 = list[j].useMessage || (flag2 && list[num2].useMessage);
				bool flag4 = list[j].KeysCount > 1 || (flag2 && list[num2].KeysCount > 1) || flag3;
				bool flag5 = flag || flag4;
				bool flag6 = j == list.Count - 1;
				bool flag7 = flag4 || flag6;
				if (flag5)
				{
					KeyContainer component = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load((!flag7) ? "Prefabs/BlockMapper/KeyContainer" : "Prefabs/BlockMapper/KeyContainerSmall"))).GetComponent<KeyContainer>();
					component.name = "KeyContainer #" + num;
					component.transform.SetParent((mapper as MonoBehaviour).transform, false);
					component.Top = ((num != 0) ? containers[num - 1].Bottom : (mapper.TopValue() - startPosition));
					num++;
					component.Z = mapper.ZValue();
					containers.Add(component);
				}
				KeyContainer keyContainer = containers[num - 1];
				MKey key = list[j];
				KeySelector[] array = ((!flag5) ? keyContainer.RightKeys : keyContainer.LeftKeys);
				foreach (KeySelector keySelector in array)
				{
					if (keySelector != null)
					{
						keySelector.Key = key;
						keySelector.Init();
					}
				}
				if (flag5)
				{
					EndPosition = mapper.TopValue() - keyContainer.Bottom;
				}
			}
		}
	}
}
