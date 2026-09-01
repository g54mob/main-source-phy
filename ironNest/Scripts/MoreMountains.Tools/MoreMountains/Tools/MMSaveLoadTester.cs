using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	public class MMSaveLoadTester : MonoBehaviour
	{
		[Header("Bindings")]
		[Tooltip("the text to save")]
		public InputField TargetInputField;

		[Header("Save settings")]
		[Tooltip("the chosen save method (json, encrypted json, binary, encrypted binary)")]
		public MMSaveLoadManagerMethods SaveLoadMethod;

		[Tooltip("the name of the file to save")]
		public string FileName;

		[Tooltip("the name of the destination folder")]
		public string FolderName;

		[Tooltip("the extension to use")]
		public string SaveFileExtension;

		[Tooltip("the key to use to encrypt the file (if needed)")]
		public string EncryptionKey;

		[MMInspectorButton("Save")]
		public bool TestSaveButton;

		[MMInspectorButton("Load")]
		public bool TestLoadButton;

		[MMInspectorButton("Reset")]
		public bool TestResetButton;

		protected IMMSaveLoadManagerMethod _saveLoadManagerMethod;

		public virtual void Save()
		{
		}

		public virtual void Load()
		{
		}

		protected virtual void Reset()
		{
		}

		protected virtual void InitializeSaveLoadMethod()
		{
		}
	}
}
