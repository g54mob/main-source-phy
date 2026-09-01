using UnityEngine;

namespace ModIO.EditorCode
{
	[CreateAssetMenu(fileName = "New Mod Profile", menuName = "ModIO/Create Mod Profile")]
	public class ScriptableModProfile : ScriptableObject
	{
		public const int UNINITIALIZED_MOD_ID = -1;

		public int modId = -1;

		public EditableModProfile editableModProfile;
	}
}
