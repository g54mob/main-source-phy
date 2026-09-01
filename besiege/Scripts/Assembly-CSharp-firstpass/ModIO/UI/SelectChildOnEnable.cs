using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class SelectChildOnEnable : MonoBehaviour
	{
		private void OnEnable()
		{
			if (NavigationManager.allowSelectionChange)
			{
				Selectable componentInChildren = base.gameObject.GetComponentInChildren<Selectable>();
				StartCoroutine(DelaySelect(componentInChildren));
			}
		}

		private IEnumerator DelaySelect(Selectable selectable)
		{
			yield return null;
			if (selectable != null)
			{
				selectable.Select();
			}
		}
	}
}
