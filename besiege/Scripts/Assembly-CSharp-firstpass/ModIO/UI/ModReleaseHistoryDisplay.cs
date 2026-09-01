using UnityEngine;

namespace ModIO.UI
{
	[RequireComponent(typeof(ModfileContainer))]
	public class ModReleaseHistoryDisplay : MonoBehaviour, IModViewElement
	{
		[Tooltip("Enabling this will display the modfiles in reverse chronological order.")]
		public bool reverseChronological = true;

		private ModView m_view;

		private int m_modId;

		private int m_requestedModId;

		public ModfileContainer container
		{
			get
			{
				return base.gameObject.GetComponent<ModfileContainer>();
			}
		}

		virtual GameObject IModViewElement.gameObject
		{
			get
			{
				return base.gameObject;
			}
		}

		protected virtual void OnEnable()
		{
			RequestReleaseHistory(m_modId);
		}

		public void SetModView(ModView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onProfileChanged.RemoveListener(RequestReleaseHistory);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onProfileChanged.AddListener(RequestReleaseHistory);
					RequestReleaseHistory(m_view.profile);
				}
				else
				{
					RequestReleaseHistory(null);
				}
			}
		}

		public void RequestReleaseHistory(ModProfile modProfile)
		{
			int modId = 0;
			if (modProfile != null)
			{
				modId = modProfile.id;
			}
			RequestReleaseHistory(modId);
		}

		public void RequestReleaseHistory(int modId)
		{
			m_modId = modId;
			if (!base.isActiveAndEnabled || modId == m_requestedModId)
			{
				return;
			}
			m_requestedModId = modId;
			int num = container.itemLimit;
			if (num < 0)
			{
				num = 100;
			}
			container.DisplayModfiles(null);
			APIPaginationParameters aPIPaginationParameters = new APIPaginationParameters();
			aPIPaginationParameters.offset = 0;
			aPIPaginationParameters.limit = num;
			APIPaginationParameters pagination = aPIPaginationParameters;
			RequestFilter requestFilter = new RequestFilter();
			requestFilter.sortFieldName = "date_added";
			requestFilter.isSortAscending = !reverseChronological;
			RequestFilter filter = requestFilter;
			APIClient.GetAllModfiles(modId, filter, pagination, delegate(RequestPage<Modfile> r)
			{
				if (this != null && modId == m_modId)
				{
					container.DisplayModfiles(r.items);
				}
			}, null);
		}
	}
}
