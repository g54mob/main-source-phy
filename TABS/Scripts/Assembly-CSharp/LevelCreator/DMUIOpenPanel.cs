using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	[RequireComponent(typeof(Button))]
	public class DMUIOpenPanel : MonoBehaviour
	{
		[SerializeField]
		private bool m_isPop;

		[SerializeField]
		[HideIf("m_isPop")]
		private DMUIManager.UIPanels m_uiPanel;

		private void Awake()
		{
			Button component = GetComponent<Button>();
			if (m_isPop)
			{
				component.onClick.AddListener(delegate
				{
					DMUIManager.Instance.PopPanel();
				});
			}
			else
			{
				component.onClick.AddListener(delegate
				{
					DMUIManager.Instance.OpenPanel(m_uiPanel);
				});
			}
		}
	}
}
