using Beautify.Universal;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Beautify.Demos;

public class ToggleDoF : MonoBehaviour
{
	private unsafe void Update()
	{
		Mouse mouse = Mouse._003Ccurrent_003Ek__BackingField;
		if (Mouse._003Ccurrent_003Ek__BackingField != null && mouse._003CleftButton_003Ek__BackingField.wasPressedThisFrame)
		{
			Beautify.Universal.Beautify settings = BeautifySettings.settings;
			bool value = settings.depthOfField.value;
			Beautify.Universal.Beautify settings2 = BeautifySettings.settings;
			object obj = default(object);
			settings2.depthOfField.Override((byte)(&obj) != 0);
		}
	}
}
