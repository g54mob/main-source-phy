using UnityEngine;

namespace ModIO.EditorCode
{
	[CreateAssetMenu(fileName = "New Editor Image Set", menuName = "ModIO/Theming/Editor Image Set")]
	public class EditorImages : ScriptableObject
	{
		private static EditorImages _instance;

		public Texture2D undoButton;

		public Texture2D clearButton;

		public Texture2D loadingPlaceholder;

		public static EditorImages Instance
		{
			get
			{
				if (!_instance)
				{
					if (Resources.LoadAll<EditorImages>(string.Empty).Length > 0)
					{
						_instance = Resources.FindObjectsOfTypeAll<EditorImages>()[0];
					}
					else
					{
						Debug.LogWarning("[mod.io] Unable to locate the mod.io EditorImages. Creating run-time instance.");
						_instance = ScriptableObject.CreateInstance<EditorImages>();
					}
				}
				return _instance;
			}
		}

		public static Texture2D UndoButton
		{
			get
			{
				return Instance.undoButton;
			}
		}

		public static Texture2D ClearButton
		{
			get
			{
				return Instance.clearButton;
			}
		}

		public static Texture2D LoadingPlaceholder
		{
			get
			{
				return Instance.loadingPlaceholder;
			}
		}
	}
}
