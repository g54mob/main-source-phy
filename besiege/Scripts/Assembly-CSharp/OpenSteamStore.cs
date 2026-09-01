using System.Collections;
using BesiegeDlc;
using Steamworks;
using UnityEngine;

[AddComponentMenu("UI/Steam/OpenSteamStore")]
public class OpenSteamStore : ClickBehaviour
{
	private enum BannerType
	{
		splinteredSea = 0,
		brokenBeyond = 1
	}

	private int currentBannerType = 1;

	public float delayBetweenSwitch = 10f;

	public float switchTime = 1f;

	public float switchScaleTime = 1f;

	public Vector3 endPosOffset;

	public Vector3 frontScale = Vector3.zero;

	public Vector3 behindScale = Vector3.zero;

	private float timeKeeper;

	public GameObject splinteredSea;

	public GameObject brokenBeyond;

	private bool hasMultipleBanners = true;

	private int activeBannerCount;

	private Vector3 frontOffset = new Vector3(0f, 0f, -1f);

	private void Awake()
	{
		if (!ReferenceMaster.IsPlatformReady())
		{
			base.gameObject.SetActive(false);
			return;
		}
		if (!DlcManager.Instance.HasPurchasedDlc(DlcManager.DlcType.Water))
		{
			activeBannerCount++;
			currentBannerType = 0;
		}
		if (TutorialFileManager.GetTutorialState("BrokenBeyondClick") != 1)
		{
			activeBannerCount++;
			currentBannerType = 1;
		}
		hasMultipleBanners = activeBannerCount > 1;
		if (activeBannerCount != 0)
		{
			GameObject bannerObject = GetBannerObject((BannerType)currentBannerType);
			bannerObject.SetActive(true);
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
	}

	public void Update()
	{
		if (hasMultipleBanners)
		{
			if (timeKeeper < delayBetweenSwitch)
			{
				timeKeeper += Time.deltaTime;
				return;
			}
			timeKeeper = 0f;
			StopAllCoroutines();
			Switch();
		}
	}

	public override void OnClicked()
	{
		switch ((BannerType)currentBannerType)
		{
		case BannerType.brokenBeyond:
		{
			AppId_t nAppID = new AppId_t
			{
				m_AppId = 3639470u
			};
			SteamFriends.ActivateGameOverlayToStore(nAppID, EOverlayToStoreFlag.k_EOverlayToStoreFlag_None);
			TutorialFileManager.SetTutorialState("BrokenBeyondClick", 1);
			GetBannerObject(BannerType.brokenBeyond).SetActive(false);
			activeBannerCount--;
			hasMultipleBanners = activeBannerCount > 1;
			break;
		}
		case BannerType.splinteredSea:
		{
			AppId_t nAppID = new AppId_t
			{
				m_AppId = 2165710u
			};
			SteamFriends.ActivateGameOverlayToStore(nAppID, EOverlayToStoreFlag.k_EOverlayToStoreFlag_None);
			break;
		}
		}
	}

	private void Switch()
	{
		GameObject bannerObject = GetBannerObject((BannerType)currentBannerType);
		currentBannerType++;
		if (currentBannerType >= activeBannerCount)
		{
			currentBannerType = 0;
		}
		GameObject bannerObject2 = GetBannerObject((BannerType)currentBannerType);
		StartCoroutine(MoveBanner(bannerObject, bannerObject2));
	}

	private GameObject GetBannerObject(BannerType banner)
	{
		switch (banner)
		{
		case BannerType.splinteredSea:
			return splinteredSea;
		case BannerType.brokenBeyond:
			return brokenBeyond;
		default:
			return null;
		}
	}

	private IEnumerator MoveBanner(GameObject from, GameObject to)
	{
		Vector3 startPos = from.transform.localPosition;
		from.transform.localPosition += frontOffset;
		Vector3 secondStartPos = to.transform.localPosition;
		to.transform.localScale = behindScale;
		to.SetActive(true);
		float pct = 0f;
		float time = 0f;
		while (time < switchTime)
		{
			pct = time / switchTime;
			from.transform.localPosition = Vector3.Lerp(startPos + frontOffset, startPos + endPosOffset + frontOffset, pct);
			to.transform.localPosition = Vector3.Lerp(secondStartPos, startPos, pct);
			to.transform.localScale = Vector3.Lerp(behindScale, frontScale, time / switchScaleTime);
			time += Time.deltaTime;
			yield return null;
		}
		from.transform.localPosition = Vector3.Lerp(startPos + frontOffset, startPos + endPosOffset + frontOffset, 1f);
		to.transform.localPosition = Vector3.Lerp(secondStartPos, startPos, 1f);
		to.transform.localScale = Vector3.Lerp(behindScale, frontScale, 1f);
		from.transform.localPosition = startPos - frontOffset;
		from.SetActive(false);
	}
}
