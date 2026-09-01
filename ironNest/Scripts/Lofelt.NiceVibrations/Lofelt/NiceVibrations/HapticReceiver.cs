using System.ComponentModel;
using UnityEngine;

namespace Lofelt.NiceVibrations
{
	[AddComponentMenu("Nice Vibrations/Haptic Receiver")]
	public class HapticReceiver : MonoBehaviour, ISerializationCallbackReceiver
	{
		[SerializeField]
		[Range(0f, 5f)]
		private float _outputLevel;

		[SerializeField]
		private bool _hapticsEnabled;

		[DefaultValue(1f)]
		public float outputLevel
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[DefaultValue(true)]
		public bool hapticsEnabled
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
		}

		private void Start()
		{
		}

		private void OnApplicationFocus(bool hasFocus)
		{
		}

		private void OnDestroy()
		{
		}
	}
}
