using System;
using UnityEngine;
using UnityEngine.Events;

namespace ModIO.UI
{
	[DisallowMultipleComponent]
	public class ModfileView : MonoBehaviour
	{
		[Serializable]
		public class ModfileChangedEvent : UnityEvent<Modfile>
		{
		}

		[SerializeField]
		private Modfile m_modfile;

		public string emptyChangelogText = string.Empty;

		public ModfileChangedEvent onModfileChanged;

		public Modfile modfile
		{
			get
			{
				return m_modfile;
			}
			set
			{
				if (m_modfile != value)
				{
					m_modfile = value;
					if (m_modfile != null && string.IsNullOrEmpty(m_modfile.changelog))
					{
						m_modfile.changelog = emptyChangelogText;
					}
					if (onModfileChanged != null)
					{
						onModfileChanged.Invoke(m_modfile);
					}
				}
			}
		}

		protected virtual void Awake()
		{
			IModfileViewElement[] componentsInChildren = base.gameObject.GetComponentsInChildren<IModfileViewElement>(true);
			IModfileViewElement[] array = componentsInChildren;
			foreach (IModfileViewElement modfileViewElement in array)
			{
				modfileViewElement.SetModfileView(this);
			}
		}
	}
}
