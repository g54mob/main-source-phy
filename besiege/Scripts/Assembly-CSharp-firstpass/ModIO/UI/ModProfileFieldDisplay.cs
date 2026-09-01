using UnityEngine;

namespace ModIO.UI
{
	public class ModProfileFieldDisplay : MonoBehaviour, IModViewElement
	{
		[MemberReference.DropdownDisplay(typeof(ModProfile), false, false, null, displayEnumerables = false, displayNested = true)]
		public MemberReference reference = new MemberReference("id");

		public ValueFormatting formatting = default(ValueFormatting);

		public bool useUppercase;

		private GenericTextComponent m_textComponent = default(GenericTextComponent);

		private ModView m_view;

		private ModProfile m_profile;

		virtual GameObject IModViewElement.gameObject
		{
			get
			{
				return base.gameObject;
			}
		}

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
			string displayString = GetDisplayString();
			string memberPath = reference.MemberPath;
			if (memberPath.Equals("name") || memberPath.Equals("descriptionAsText"))
			{
				ModBrowser.filterMethod(displayString, delegate
				{
					UpdateDisplay(displayString);
				});
			}
			else
			{
				UpdateDisplay(displayString);
			}
		}

		public void UpdateDisplay(string displayString)
		{
			m_textComponent.text = displayString;
		}

		public string GetDisplayString()
		{
			object value = reference.GetValue(m_profile);
			string text = ValueFormatting.FormatValue(value, formatting.method, formatting.toStringParameter);
			if (useUppercase)
			{
				text = text.ToUpper();
			}
			return text;
		}
	}
}
