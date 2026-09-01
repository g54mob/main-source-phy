using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMSaveLoadManagerMethod : MonoBehaviour
	{
		[Header("Save and load method")]
		[MMInformation("This component, on Awake or on demand, will force a SaveLoadMethod on the MMSaveLoadManager, changing the way it saves data to file. This will impact all classes that use the MMSaveLoadManager (unless they change that method before saving or loading).If you change the method, your previously existing data files won't be compatible, you'll need to delete them and start with new ones.", MMInformationAttribute.InformationType.Info, false)]
		[Tooltip("the method to use to save to file")]
		public MMSaveLoadManagerMethods SaveLoadMethod;

		[Tooltip("the key to use to encrypt the file (if using an encryption method)")]
		public string EncryptionKey;

		protected IMMSaveLoadManagerMethod _saveLoadManagerMethod;

		protected virtual void Awake()
		{
		}

		public virtual void SetSaveLoadMethod()
		{
		}
	}
}
