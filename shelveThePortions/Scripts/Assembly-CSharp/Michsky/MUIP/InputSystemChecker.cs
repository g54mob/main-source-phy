using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace Michsky.MUIP
{
	public class InputSystemChecker : MonoBehaviour
	{
		private void Awake()
		{
			if (!base.gameObject.TryGetComponent<InputSystemUIInputModule>(out var _))
			{
				base.gameObject.AddComponent<InputSystemUIInputModule>();
				if (base.gameObject.TryGetComponent<StandaloneInputModule>(out var component2))
				{
					Object.Destroy(component2);
				}
			}
		}
	}
}
