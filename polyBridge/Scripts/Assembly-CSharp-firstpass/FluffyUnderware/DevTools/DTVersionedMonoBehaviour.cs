using System;
using UnityEngine;

namespace FluffyUnderware.DevTools
{
	public class DTVersionedMonoBehaviour : MonoBehaviour
	{
		[SerializeField]
		[HideInInspector]
		private string m_Version;

		public string Version
		{
			get
			{
				return m_Version;
			}
			protected set
			{
				m_Version = value;
			}
		}

		[Obsolete("This upgrading mechanism is tied to the Editor. Meaning it does not work when in Play mode. A better way to handle upgrading would be to use Unity's ISerializationCallbackReceiver")]
		protected void CheckForVersionUpgrade()
		{
		}

		[Obsolete("This upgrading mechanism is tied to the Editor. Meaning it does not work when in Play mode. A better way to handle upgrading would be to use Unity's ISerializationCallbackReceiver")]
		protected virtual bool UpgradeVersion(string oldVersion, string newVersion)
		{
			if (string.IsNullOrEmpty(oldVersion))
			{
				Debug.LogFormat("[{0}] Upgrading '{1}' to version {2}! PLEASE SAVE THE SCENE!", GetType().Name, base.name, newVersion);
			}
			else
			{
				Debug.LogFormat("[{0}] Upgrading '{1}' from version {2} to {3}! PLEASE SAVE THE SCENE!", GetType().Name, base.name, oldVersion, newVersion);
			}
			return true;
		}

		public void Destroy()
		{
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(base.gameObject);
			}
		}
	}
}
