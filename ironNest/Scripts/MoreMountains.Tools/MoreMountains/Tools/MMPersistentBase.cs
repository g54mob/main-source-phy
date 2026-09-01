using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu(null)]
	public class MMPersistentBase : MonoBehaviour, IMMPersistent
	{
		[Header("Save")]
		[Tooltip("whether or not this object should be saved")]
		public bool SaveActive;

		[Header("ID")]
		[Tooltip("an optional suffix to add to the GUID, to make it more readable")]
		public string UniqueIDSuffix;

		[Tooltip("the object's unique ID")]
		[SerializeField]
		[MMReadOnly]
		protected string _guid;

		[MMInspectorButton("GenerateGuid")]
		public bool GenerateGuidButton;

		protected virtual void OnValidate()
		{
		}

		public virtual string GetGuid()
		{
			return null;
		}

		public virtual void SetGuid(string newGUID)
		{
		}

		public virtual string OnSave()
		{
			return null;
		}

		public virtual void OnLoad(string data)
		{
		}

		public virtual bool ShouldBeSaved()
		{
			return false;
		}

		public virtual string GenerateGuid()
		{
			return null;
		}

		public virtual bool GuidIsUnique(string guid)
		{
			return false;
		}

		public virtual void ValidateGuid()
		{
		}
	}
}
