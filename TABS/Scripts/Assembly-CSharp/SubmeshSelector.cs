using System;
using UnityEngine;

[ExecuteInEditMode]
public class SubmeshSelector : MonoBehaviour
{
	[Serializable]
	public class Wrapper
	{
		public bool Enabled;

		public GameObject Object;

		public SubmeshSelector Selector;

		public Wrapper(bool enabled, GameObject go, SubmeshSelector selector)
		{
			Enabled = enabled;
			Object = go;
			Selector = selector;
		}
	}

	public Wrapper[] submeshes;

	private int lastSelected;
}
