using System.Collections;
using InternalModding.Blocks;
using UnityEngine;

public class ModTabAnimator : MonoBehaviour
{
	public Transform[] area = new Transform[0];

	public Transform tabParent;

	public GameObject indicator;

	private Camera hudCamera;

	private bool isOpen;

	private bool animating;

	private bool AnyBlockModsPresent
	{
		get
		{
			return SingleInstanceFindOnly<BlockLoader>.Instance.VisibleBlocksCount > 0;
		}
	}

	private void Awake()
	{
		hudCamera = GameObject.FindGameObjectWithTag("hudCamera").GetComponent<Camera>();
	}

	private void LateUpdate()
	{
		if (AnyBlockModsPresent)
		{
			indicator.SetActive(true);
			bool flag = MouseOver();
			if (flag != isOpen)
			{
				StopAllCoroutines();
				StartCoroutine(IEAnimate(flag, 0.2f, 0.5f));
			}
		}
		else
		{
			indicator.SetActive(false);
		}
	}

	private bool MouseOver()
	{
		Vector3 point = hudCamera.ScreenToWorldPoint(Input.mousePosition);
		for (int i = 0; i < area.Length; i++)
		{
			Transform transform = area[i];
			Vector3 position = transform.position;
			Vector3 lossyScale = transform.lossyScale;
			point = new Vector3(point.x, point.y, position.z);
			if (new Bounds(position, lossyScale).Contains(point))
			{
				return true;
			}
		}
		return false;
	}

	private IEnumerator IEAnimate(bool open, float wait, float duration)
	{
		isOpen = open;
		if (!animating)
		{
			yield return new WaitForSecondsRealtime(wait);
		}
		animating = true;
		float target = ((!open) ? (-1.03f) : 0f);
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			float pct = t / duration;
			tabParent.localPosition = new Vector3(tabParent.localPosition.x, Mathf.Lerp(tabParent.localPosition.y, target, pct), tabParent.localPosition.z);
			yield return null;
		}
		tabParent.localPosition = new Vector3(tabParent.localPosition.x, target, tabParent.localPosition.z);
		animating = false;
	}
}
