using UnityEngine;
using UnityEngine.Serialization;

namespace Kamgam.SettingsGenerator
{
	public class SettingReceiverGenericConnector : MonoBehaviour
	{
		[Header("Config")]
		[Tooltip("If enabled then the setting value will be pushed to the property/field/method at start.")]
		public bool ApplyOnStart;

		[Header("Setting")]
		[Tooltip("The settings provider used to find the setting by id.")]
		public SettingsProvider SettingsProvider;

		[Tooltip("Enter a setting id (HINT: Choose from the list below). If the input field turns green then you have entered a valid id.")]
		public string SettingId;

		[FormerlySerializedAs("PropertyPath")]
		[Header("Receiving Property")]
		[Tooltip("Enter a path to a property that matches the setting type.\nUse the selector below. If the field turns green then it is compatible with the setting you have chosen.")]
		public string Path;

		protected GameObjectInspector _inspector;

		public GameObjectInspector Inspector => null;

		public ISetting Setting => null;

		public bool IsSettingCompatibleWithPath()
		{
			return false;
		}

		public void Start()
		{
		}

		public void OnDisable()
		{
		}

		private void OnSettingChanged(ISetting setting)
		{
		}
	}
}
