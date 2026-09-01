using UnityEngine;

namespace ModIO.UI
{
	public class ModUserRatingDisplay : MonoBehaviour, IModRatingAddedReceiver, IModViewElement
	{
		public StateToggleDisplay positiveRatingDisplay;

		public StateToggleDisplay negativeRatingDisplay;

		private ModView m_view;

		private int m_modId;

		virtual GameObject IModViewElement.gameObject
		{
			get
			{
				return base.gameObject;
			}
		}

		public void SetModView(ModView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onProfileChanged.RemoveListener(DisplayModRating);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onProfileChanged.AddListener(DisplayModRating);
					DisplayModRating(m_view.profile);
				}
				else
				{
					DisplayModRating(null);
				}
			}
		}

		public void DisplayModRating(ModProfile profile)
		{
			int modId = 0;
			if (profile != null)
			{
				modId = profile.id;
			}
			DisplayModRating(modId);
		}

		public void DisplayModRating(int modId)
		{
			m_modId = modId;
			ModRatingValue modRating = ModBrowser.instance.GetModRating(modId);
			if (positiveRatingDisplay != null)
			{
				positiveRatingDisplay.isOn = modRating == ModRatingValue.Positive;
			}
			if (negativeRatingDisplay != null)
			{
				negativeRatingDisplay.isOn = modRating == ModRatingValue.Negative;
			}
		}

		public void OnModRatingAdded(int modId, ModRatingValue rating)
		{
			if (modId == m_modId)
			{
				if (positiveRatingDisplay != null)
				{
					positiveRatingDisplay.isOn = rating == ModRatingValue.Positive;
				}
				if (negativeRatingDisplay != null)
				{
					negativeRatingDisplay.isOn = rating == ModRatingValue.Negative;
				}
			}
		}
	}
}
