using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace LevelCreator
{
	public class InvokeButton : MonoBehaviour
	{
		[SerializeField]
		[FormerlySerializedAs("button")]
		private Button m_button;

		private void Awake()
		{
		}

		public void Invoke()
		{
			m_button.onClick.Invoke();
		}
	}
}
