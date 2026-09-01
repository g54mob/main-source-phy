using System;
using UnityEngine;

[ExecuteInEditMode]
public class ModuleSelector : MonoBehaviour
{
	[Serializable]
	public class Wrapper
	{
		public bool Enabled;

		public GameObject Object;

		public ModuleSelector Selector;

		public string enabledTag;

		public Wrapper(bool enabled, GameObject go, ModuleSelector selector)
		{
			Enabled = enabled;
			Object = go;
			Selector = selector;
			enabledTag = go.tag;
		}
	}

	public Wrapper[] submeshes;

	private int lastSelected;
}
