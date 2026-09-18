using UnityEngine;
using UnityEngine.Serialization;

namespace Synty.Interface.FantasyMenus.Samples
{
	public class SampleSettingsArrayObject : MonoBehaviour
	{
		[Header("References")]
		public SampleSettingsArray arraySetting;

		[Tooltip("Populated from arraySettings")]
		public string[] options;

		[FormerlySerializedAs("gameObejects")]
		public GameObject[] gameObjects;

		private void OnValidate()
		{
			if (!(arraySetting == null))
			{
				options = arraySetting.options;
			}
		}

		private void Awake()
		{
			if (arraySetting != null)
			{
				arraySetting.onValueChanged.AddListener(OnValueChanged);
				OnValueChanged(arraySetting.OptionIndex, arraySetting.Option);
			}
		}

		private void OnDestroy()
		{
			if (arraySetting != null)
			{
				arraySetting.onValueChanged.RemoveListener(OnValueChanged);
			}
		}

		private void OnValueChanged(int index, string option)
		{
			for (int i = 0; i < gameObjects.Length; i++)
			{
				if (gameObjects[i] != null)
				{
					gameObjects[i].SetActive(index == i);
				}
			}
		}
	}
}
