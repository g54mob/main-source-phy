using Landfall.TABS.Workshop;
using ModIO;
using ModIO.API;
using UnityEngine;
using UnityEngine.UI;

public class BattleCreatorHandleCampaignRatingUI : UINavigationGroup
{
	[SerializeField]
	private Button m_RatePositiveButton;

	[SerializeField]
	private Button m_RateNegativeButton;

	[SerializeField]
	private Sprite m_PositiveActive;

	[SerializeField]
	private Sprite m_PositivePassive;

	[SerializeField]
	private Sprite m_NegativeActive;

	[SerializeField]
	private Sprite m_NegativePassive;

	private int m_ModID;

	protected override void Awake()
	{
		base.Awake();
		InitListeners();
	}

	private void InitListeners()
	{
		m_RatePositiveButton.onClick.AddListener(RatePositive);
		m_RateNegativeButton.onClick.AddListener(RateNegative);
	}

	public void Init(int modID)
	{
		m_ModID = modID;
		bool status = CheckModID();
		InternalInit(status);
	}

	private void InternalInit(bool status)
	{
		m_RateNegativeButton.transform.parent.gameObject.SetActive(status);
		m_RateNegativeButton.gameObject.SetActive(status);
		m_RatePositiveButton.gameObject.SetActive(status);
		if (status)
		{
			CheckCurrentRatings();
		}
	}

	private void CheckCurrentRatings()
	{
		ModRating[] localUserRatings = CustomContentLoaderModIO.LocalUserRatings;
		int num = localUserRatings.Length;
		for (int i = 0; i < num; i++)
		{
			if (localUserRatings[i].modId == m_ModID)
			{
				InitRating(localUserRatings[i]);
				break;
			}
		}
	}

	private void InitRating(ModRating rating)
	{
		if (rating.ratingValue == ModRatingValue.Negative)
		{
			RateDown();
		}
		else if (rating.ratingValue == ModRatingValue.Positive)
		{
			RateUp();
		}
	}

	private void ClearRatings()
	{
		m_RateNegativeButton.GetComponent<Image>().sprite = m_NegativePassive;
		m_RatePositiveButton.GetComponent<Image>().sprite = m_PositivePassive;
	}

	private void RateDown()
	{
		ClearRatings();
		m_RateNegativeButton.GetComponent<Image>().sprite = m_NegativeActive;
	}

	private void RateUp()
	{
		ClearRatings();
		m_RatePositiveButton.GetComponent<Image>().sprite = m_PositiveActive;
	}

	private bool CheckModID()
	{
		return m_ModID > 2;
	}

	private void RatePositive()
	{
		AddModRatingParameters addModRatingParameters = new AddModRatingParameters();
		addModRatingParameters.ratingValue = ModRatingValue.Positive;
		APIClient.AddModRating(m_ModID, addModRatingParameters, OnRatingSuccess, OnRatingFailed);
		RateUp();
	}

	private void RateNegative()
	{
		AddModRatingParameters addModRatingParameters = new AddModRatingParameters();
		addModRatingParameters.ratingValue = ModRatingValue.Negative;
		APIClient.AddModRating(m_ModID, addModRatingParameters, OnRatingSuccess, OnRatingFailed);
		RateDown();
	}

	private void OnRatingFailed(WebRequestError obj)
	{
	}

	private void OnRatingSuccess(APIMessage obj)
	{
		Debug.Log("SuccessfullyRated Mod: " + m_ModID);
	}
}
