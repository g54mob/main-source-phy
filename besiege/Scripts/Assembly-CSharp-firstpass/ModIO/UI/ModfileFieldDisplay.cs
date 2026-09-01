using UnityEngine;

namespace ModIO.UI
{
	public class ModfileFieldDisplay : MonoBehaviour, IModfileViewElement
	{
		[MemberReference.DropdownDisplay(typeof(Modfile), false, false, null, displayEnumerables = false, displayNested = true)]
		public MemberReference reference = new MemberReference("id");

		public ValueFormatting formatting = default(ValueFormatting);

		public bool useUppercase;

		private GenericTextComponent m_textComponent = default(GenericTextComponent);

		private ModfileView m_view;

		private Modfile m_modfile;

		protected virtual void Awake()
		{
			Component textDisplayComponent = GenericTextComponent.FindCompatibleTextComponent(base.gameObject);
			m_textComponent.SetTextDisplayComponent(textDisplayComponent);
		}

		protected virtual void OnEnable()
		{
			DisplayModfile(m_modfile);
		}

		public void SetModfileView(ModfileView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onModfileChanged.RemoveListener(DisplayModfile);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onModfileChanged.AddListener(DisplayModfile);
					DisplayModfile(m_view.modfile);
				}
				else
				{
					DisplayModfile(null);
				}
			}
		}

		public void DisplayModfile(Modfile modfile)
		{
			m_modfile = modfile;
			object value = reference.GetValue(m_modfile);
			string text = ValueFormatting.FormatValue(value, formatting.method, formatting.toStringParameter);
			if (useUppercase)
			{
				text = text.ToUpper();
			}
			m_textComponent.text = text;
		}
	}
}
