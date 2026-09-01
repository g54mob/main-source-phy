using System.Collections;
using System.Linq;
using UnityEngine;

public class BarPositionController : SingleInstanceFindOnly<BarPositionController>
{
	public float speed;

	public Transform topBar;

	public Transform bottomBar;

	public Transform rightBar;

	public Renderer[] faders = new Renderer[0];

	public float moveAmount = 2f;

	public float rightMoveAmount = 2f;

	private float topBarStartPos;

	private float bottomBarStartPos;

	private IEnumerator lerpTopBarCoroutine;

	private IEnumerator lerpBottomBarCorroutine;

	private IEnumerator fadeCoroutine;

	private Color[] colors;

	private float currentPct = 1f;

	public override string Name
	{
		get
		{
			return "HUD ERROR";
		}
	}

	private int GetNumberOfParents(Component component)
	{
		int num = 0;
		Transform parent = component.transform.parent;
		while (parent != null)
		{
			num++;
			parent = parent.parent;
		}
		return num;
	}

	private void Start()
	{
		SetInitialPositions();
		AlignScaleElements();
	}

	private void SetInitialPositions()
	{
		if (topBar != null)
		{
			topBarStartPos = topBar.localPosition.y;
		}
		if (bottomBar != null)
		{
			bottomBarStartPos = bottomBar.localPosition.y;
		}
		colors = new Color[faders.Length];
		for (int i = 0; i < faders.Length; i++)
		{
			colors[i] = faders[i].material.GetColor("_TintColor");
		}
	}

	public void AlignScaleElements()
	{
		ResetToInitialPosition();
		AlignToScreenPoint[] componentsInChildren = base.transform.root.GetComponentsInChildren<AlignToScreenPoint>(true);
		componentsInChildren = componentsInChildren.OrderBy((AlignToScreenPoint x) => GetNumberOfParents(x)).ToArray();
		ScaleBetweenTwoPoints[] componentsInChildren2 = base.transform.root.GetComponentsInChildren<ScaleBetweenTwoPoints>(true);
		for (int num = 0; num < componentsInChildren.Length; num++)
		{
			componentsInChildren[num].Align();
		}
		for (int num2 = 0; num2 < componentsInChildren2.Length; num2++)
		{
			componentsInChildren2[num2].Align();
		}
	}

	public void Set()
	{
		if (!StatMaster.isHeadless && (!(topBar == null) || !(bottomBar == null)))
		{
			if (lerpTopBarCoroutine != null)
			{
				StopCoroutine(lerpTopBarCoroutine);
			}
			if (lerpBottomBarCorroutine != null)
			{
				StopCoroutine(lerpBottomBarCorroutine);
			}
			if (fadeCoroutine != null)
			{
				StopCoroutine(fadeCoroutine);
			}
			if ((bool)topBar)
			{
				lerpTopBarCoroutine = Lerpy(topBar);
				StartCoroutine(lerpTopBarCoroutine);
			}
			if ((bool)bottomBar)
			{
				lerpBottomBarCorroutine = Lerpy(bottomBar);
				StartCoroutine(lerpBottomBarCorroutine);
			}
			if (faders.Length > 0)
			{
				Machine machine;
				bool flag = (GetMachine(out machine) && machine.isSimulating) || SingleInstanceFindOnly<WinScreen>.Instance.Visible;
				fadeCoroutine = Fade(0.5f, (!flag) ? 1f : 0f);
				StartCoroutine(fadeCoroutine);
			}
		}
	}

	private void ResetToInitialPosition()
	{
		if (topBar != null)
		{
			topBar.localPosition = new Vector3(topBar.localPosition.x, topBarStartPos, topBar.localPosition.z);
		}
		if (bottomBar != null)
		{
			bottomBar.localPosition = new Vector3(bottomBar.localPosition.x, bottomBarStartPos, bottomBar.localPosition.z);
		}
	}

	private IEnumerator Lerpy(Transform obj)
	{
		Machine machine;
		bool hasMachine = GetMachine(out machine);
		bool hideBar = (hasMachine && machine.isSimulating) || SingleInstanceFindOnly<WinScreen>.Instance.Visible;
		float startPos;
		float endHeight;
		if (obj == topBar)
		{
			startPos = topBarStartPos;
			endHeight = ((!hideBar) ? 0f : moveAmount);
		}
		else
		{
			if (StatMaster.isMP && PlayerData.hasLocalPlayer && (PlayerData.localPlayer.isSpectator || (StatMaster.limitMachines && !LevelEditor.Instance.Settings.AllowModMachines) || (hasMachine && machine.BuildingLocked)))
			{
				hideBar = true;
			}
			startPos = bottomBarStartPos;
			endHeight = ((!hideBar) ? 0f : ((0f - moveAmount) * 2f));
		}
		if (!hideBar)
		{
			obj.gameObject.SetActive(true);
		}
		float cTime = 0f;
		float rate = 1f / speed;
		Vector3 objPos = obj.localPosition;
		float currentPos = objPos.y;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			obj.localPosition = new Vector3(objPos.x, Mathf.Lerp(currentPos, startPos + endHeight, cTime), objPos.z);
			yield return null;
		}
		obj.localPosition = new Vector3(objPos.x, startPos + endHeight, objPos.z);
		if (hideBar)
		{
			obj.gameObject.SetActive(false);
		}
	}

	private bool GetMachine(out Machine machine)
	{
		machine = null;
		bool flag = false;
		if (StatMaster.isMP)
		{
			flag = PlayerData.hasLocalPlayer && !PlayerData.localPlayer.isSpectator;
			if (flag)
			{
				machine = PlayerData.localPlayer.machine;
			}
		}
		else
		{
			machine = Machine.Active();
			flag = machine != null;
		}
		return flag;
	}

	private IEnumerator Fade(float duration, float alpha)
	{
		float start = currentPct;
		for (float t = start * duration; t < duration; t += Time.unscaledDeltaTime)
		{
			float pct = t / duration;
			for (int i = 0; i < faders.Length; i++)
			{
				Color c = colors[i];
				c.a = Mathf.Lerp(start, alpha, pct) * c.a;
				faders[i].material.SetColor("_TintColor", c);
			}
			currentPct = Mathf.Lerp(start, alpha, pct);
			yield return null;
		}
		for (int j = 0; j < faders.Length; j++)
		{
			Color c = colors[j];
			c.a = alpha * c.a;
			faders[j].material.SetColor("_TintColor", c);
		}
		currentPct = alpha;
	}
}
