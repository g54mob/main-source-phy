using UnityEngine;

namespace ModIO.UI
{
	public class ModProfileFieldDisplay : MonoBehaviour, IModViewElement
	{
		[MemberReference.DropdownDisplay(typeof(ModProfile), false, false, null, displayEnumerables = false, displayNested = true)]
		public MemberReference reference = new MemberReference("id");

		public ValueFormatting formatting;

		private GenericTextComponent m_textComponent;

		private ModView m_view;

		private ModProfile m_profile;

		GameObject IModViewElement.gameObject => base.gameObject;

		protected virtual void Awake()
		{
			Component textDisplayComponent = GenericTextComponent.FindCompatibleTextComponent(base.gameObject);
			m_textComponent.SetTextDisplayComponent(textDisplayComponent);
		}

		protected virtual void OnEnable()
		{
			DisplayProfile(m_profile);
		}

		public void SetModView(ModView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onProfileChanged.RemoveListener(DisplayProfile);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onProfileChanged.AddListener(DisplayProfile);
					DisplayProfile(m_view.profile);
				}
				else
				{
					DisplayProfile(null);
				}
			}
		}

		public void DisplayProfile(ModProfile profile)
		{
			m_profile = profile;
			string text = ValueFormatting.FormatValue(reference.GetValue(m_profile), formatting.method, formatting.toStringParameter);
			m_textComponent.text = text;
		}
	}
}
