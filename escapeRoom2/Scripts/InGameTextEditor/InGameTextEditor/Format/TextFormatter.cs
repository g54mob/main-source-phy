using UnityEngine;

namespace InGameTextEditor.Format
{
	public abstract class TextFormatter : MonoBehaviour
	{
		public abstract bool Initialized { get; }

		public abstract void Init();

		public abstract void OnLineChanged(Line line);
	}
}
