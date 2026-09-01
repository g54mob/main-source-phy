using UnityEngine;

namespace ModIO.UI
{
	public class ModStatisticsFieldDisplay : MonoBehaviour, IModViewElement
	{
		[MemberReference.DropdownDisplay(typeof(ModStatistics), false, false, null, displayEnumerables = false, displayNested = true)]
		public MemberReference reference = new MemberReference("modId");

		public ValueFormatting formatting;

		private GenericTextComponent m_textComponent;

		private ModView m_view;

		private ModStatistics m_statistics;

		GameObject IModViewElement.gameObject => base.gameObject;

		protected virtual void Awake()
		{
			Component textDisplayComponent = GenericTextComponent.FindCompatibleTextComponent(base.gameObject);
			m_textComponent.SetTextDisplayComponent(textDisplayComponent);
		}

		protected virtual void OnEnable()
		{
			DisplayStatistics(m_statistics);
		}

		public void SetModView(ModView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onStatisticsChanged.RemoveListener(DisplayStatistics);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onStatisticsChanged.AddListener(DisplayStatistics);
					DisplayStatistics(m_view.statistics);
				}
				else
				{
					DisplayStatistics(null);
				}
			}
		}

		public void DisplayStatistics(ModStatistics statistics)
		{
			m_statistics = statistics;
			string text = ValueFormatting.FormatValue(reference.GetValue(m_statistics), formatting.method, formatting.toStringParameter);
			m_textComponent.text = text;
		}
	}
}
