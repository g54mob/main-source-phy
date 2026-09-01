using System;
using System.Collections.Generic;
using UnityEngine;

public class DestructionBar : SingleInstanceFindOnly<DestructionBar>
{
	public class TeamBar
	{
		public Transform transform;

		public GameObject go;

		public GameObject textGO;

		public Renderer renderer;

		public float prevPerc;

		public int index;

		public bool isFull;

		public bool isUpdating;

		public bool updatingWidth;

		private float startZ;

		private float currentZ;

		private float textZ;

		private Vector3 left;

		private Vector3 right;

		private float currentWidth;

		private float width;

		private float currentOffset;

		private Transform textHolder;

		private TextMesh textMesh;

		private float updateThreshold;

		private float widthLerpSpeed;

		public TeamBar(int i, Transform barTransform, GameObject barGO, Renderer barRenderer, Vector3 leftSide, Vector3 rightSide, float barWidth, float widthLerp, float threshold)
		{
			transform = barTransform;
			isUpdating = (updatingWidth = false);
			index = i;
			updateThreshold = threshold;
			startZ = transform.position.z;
			textHolder = barTransform.FindChild("TextHolder");
			textMesh = textHolder.GetComponentInChildren<TextMesh>();
			textZ = textHolder.position.z;
			textGO = textHolder.gameObject;
			textHolder.SetParent(barTransform.parent, true);
			widthLerpSpeed = widthLerp;
			currentOffset = 0f;
			currentWidth = width;
			go = barGO;
			renderer = barRenderer;
			left = leftSide;
			right = rightSide;
			width = barWidth;
			Reset();
			transform.up = Vector3.right;
		}

		public void UpdateUIScale(Vector3 leftSide, Vector3 rightSide, float barWidth, float currentCompletion)
		{
			left = leftSide;
			right = rightSide;
			width = barWidth;
			Vector3 vector = Vector3.Lerp(leftSide, rightSide, currentCompletion / 100f);
			Vector3 offset = vector - leftSide;
			UpdateBar(float.MaxValue, offset, offset.x);
		}

		public void UpdateBar(float speed, Vector3 offset, float scale)
		{
			float num = left.x + offset.x / 2f;
			Vector3 position = transform.position;
			Vector3 localScale = transform.localScale;
			if (position.x != num || localScale.y != scale)
			{
				float t = TimeSlider.Instance.deltaTime * speed;
				transform.position = new Vector3(Mathf.Lerp(position.x, num, t), position.y, position.z);
				transform.localScale = new Vector3(localScale.x, Mathf.Lerp(localScale.y, scale, t), localScale.z);
				float num2 = right.x - left.x;
				SetPercentageText(Mathf.FloorToInt(scale / num2 * 100f));
				textHolder.position = new Vector3(left.x + scale, left.y, textZ);
			}
			if (updatingWidth)
			{
				UpdateWidth();
			}
		}

		public void SetPercentageText(int percentage)
		{
			textMesh.text = percentage + "%";
		}

		public void UpdateWidth()
		{
			float t = TimeSlider.Instance.deltaTime * widthLerpSpeed;
			Vector3 position = transform.position;
			float num = left.y + currentOffset;
			Vector3 position2 = new Vector3(position.x, Mathf.Lerp(position.y, num, t), currentZ);
			Vector3 localScale = transform.localScale;
			Vector3 localScale2 = new Vector3(Mathf.Lerp(localScale.x, currentWidth, t), localScale.y, localScale.z);
			transform.position = position2;
			transform.localScale = localScale2;
			float num2 = Mathf.Abs(num - position2.y);
			if (num2 < updateThreshold)
			{
				updatingWidth = false;
			}
		}

		public void UpdateTransform(float newWidth, float offset)
		{
			currentWidth = newWidth;
			currentOffset = offset;
			updatingWidth = true;
		}

		public void AddProgress(float newProgress)
		{
			isUpdating = true;
			currentZ = startZ + newProgress * 0.01f;
			updatingWidth = true;
		}

		public void Reset()
		{
			prevPerc = -1f;
			isFull = false;
			currentZ = startZ;
			currentOffset = 0f;
			currentWidth = width;
			isUpdating = false;
			textHolder.position = new Vector3(left.x, left.y, textZ);
			transform.position = new Vector3(left.x, left.y, startZ);
			textMesh.text = "0%";
			transform.localScale = new Vector3(currentWidth, 0f, currentWidth);
		}
	}

	public Camera hudCam;

	public float Width = 1f;

	public Transform bgObj;

	public Transform percentageBarObj;

	public Transform percentageBarObjRed;

	public GameObject percentageBarTeam;

	public float percent = 50f;

	public float fullPercent;

	public Transform percentBox;

	public float lerpSpeed = 6f;

	public TextMesh percentText;

	public Transform leftPosCap;

	public Transform rightPosCap;

	public static float percentToWin = 6f;

	public Transform completionMarker;

	public TeamBar[] teamBars;

	public float[] teamCompletion;

	private float maxCompletion = 100f;

	private bool createdTeamCompletion;

	private bool createdTeamBars;

	protected float startZPosBG;

	protected float startZPosPercentageBar;

	protected bool wasSimulating;

	protected float smoothPercentage = 6f;

	protected bool currentVis = true;

	protected float updateBarThreshold = 0.005f;

	private Renderer bgObjRenderer;

	private Renderer percentageBarObjRenderer;

	private Renderer percentageBarObjRedRenderer;

	private Renderer completionMarkerRenderer;

	private Vector3 leftSide;

	private Vector3 rightSide;

	private GameObject percentageBarGO;

	private GameObject percentageBarRedGO;

	public bool updatingBar;

	private bool forceInstantUpdate;

	public override string Name
	{
		get
		{
			return "DestructionBar";
		}
	}

	public void ResetProgress()
	{
		percentageBarObj.position = new Vector3(leftSide.x, leftSide.y, percentageBarObj.position.z);
		percentageBarObj.localScale = new Vector3(Width, 0f, Width);
		percentageBarObjRed.position = new Vector3(leftSide.x, leftSide.y, percentageBarObjRed.position.z);
		percentageBarObjRed.localScale = new Vector3(Width, 0f, Width);
		for (int i = 0; i < teamBars.Length; i++)
		{
			teamBars[i].Reset();
			teamCompletion[i] = 0f;
		}
	}

	public void ToggleTeamBars(bool toggle)
	{
		for (int i = 0; i < teamBars.Length; i++)
		{
			TeamBar teamBar = teamBars[i];
			teamBar.go.SetActive(toggle);
			teamBar.textGO.SetActive(toggle);
		}
	}

	public void SetProgress(float completion)
	{
		percent = completion;
		updatingBar = true;
	}

	public bool AddProgress(MPTeam team, float progress)
	{
		if (!createdTeamCompletion)
		{
			CreateTeamCompletion();
		}
		int num = (int)(team - 1);
		float oldCompletion = teamCompletion[num];
		float num2 = Mathf.Clamp(teamCompletion[num] + progress, 0f, maxCompletion);
		teamCompletion[num] = num2;
		bool flag = num2 == maxCompletion;
		if (createdTeamBars)
		{
			TeamBar teamBar = teamBars[num];
			teamBar.isFull = flag;
			teamBar.AddProgress(num2);
			UpdateShared(teamBar, oldCompletion);
		}
		return flag;
	}

	private void UpdateShared(TeamBar bar, float oldCompletion)
	{
		List<TeamBar> list = new List<TeamBar>();
		for (int i = 0; i < teamBars.Length; i++)
		{
			TeamBar teamBar = teamBars[i];
			if (teamCompletion[teamBar.index] == oldCompletion)
			{
				list.Add(teamBar);
			}
		}
		float num = Width * 0.5f;
		if (list.Count > 0)
		{
			float num2 = num / (float)list.Count;
			float num3 = num2 * 0.5f;
			float num4 = num + num3;
			for (int i = 0; i < list.Count; i++)
			{
				TeamBar teamBar = list[i];
				float offset = num4 - (float)(i + 1) * num2;
				teamBar.UpdateTransform(num2, offset);
			}
		}
		List<TeamBar> list2 = new List<TeamBar>();
		float num5 = teamCompletion[bar.index];
		for (int i = 0; i < teamBars.Length; i++)
		{
			TeamBar teamBar = teamBars[i];
			float num6 = teamCompletion[teamBar.index];
			if (num6 > 0f && num6 == num5)
			{
				list2.Add(teamBar);
			}
		}
		if (list2.Count > 1)
		{
			float num2 = num / (float)list2.Count;
			float num3 = num2 * 0.5f;
			float num4 = num + num3;
			for (int i = 0; i < list2.Count; i++)
			{
				TeamBar teamBar = list2[i];
				float offset = num4 - (float)(i + 1) * num2;
				teamBar.UpdateTransform(num2, offset);
			}
		}
		else
		{
			bar.UpdateTransform(Width, 0f);
		}
	}

	private void CreateTeamCompletion()
	{
		if (!createdTeamCompletion)
		{
			int num = ReferenceMaster.Instance.teamColors.Length - 1;
			teamCompletion = new float[num];
			for (int i = 0; i < num; i++)
			{
				teamCompletion[i] = 0f;
			}
			createdTeamCompletion = true;
		}
	}

	private void CreateTeamBars()
	{
		if (!createdTeamCompletion)
		{
			CreateTeamCompletion();
		}
		if (!StatMaster.isMP || !percentageBarTeam)
		{
			teamBars = new TeamBar[0];
			return;
		}
		int num = ReferenceMaster.Instance.teamColors.Length;
		int num2 = num - 1;
		teamBars = new TeamBar[num2];
		for (int i = 0; i < num2; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(percentageBarTeam);
			MPTeam mPTeam = (MPTeam)(i + 1);
			gameObject.name = "TeamProgress" + mPTeam;
			Transform transform = gameObject.transform;
			Renderer component = gameObject.GetComponent<Renderer>();
			component.material.SetColor("_Color", ReferenceMaster.Instance.teamColors[i + 1]);
			transform.SetParent(percentageBarObjRed.parent, true);
			TeamBar teamBar = new TeamBar(i, transform, gameObject, component, leftSide, rightSide, Width, 20f, updateBarThreshold * 0.1f);
			float num3 = teamCompletion[i];
			teamBar.isFull = num3 == maxCompletion;
			if (num3 > 0f)
			{
				UpdateShared(teamBar, 0f);
			}
			teamBars[i] = teamBar;
		}
		createdTeamBars = true;
	}

	private void UpdateTeamBars()
	{
		if (teamBars != null)
		{
			for (int i = 0; i < teamBars.Length; i++)
			{
				teamBars[i].UpdateUIScale(leftSide, rightSide, Width, teamCompletion[i]);
			}
		}
	}

	private void Start()
	{
		hudCam = GameObject.FindGameObjectWithTag("hudCamera").GetComponent<Camera>();
		wasSimulating = true;
		bgObjRenderer = bgObj.GetComponent<Renderer>();
		percentageBarObjRenderer = percentageBarObj.GetComponent<Renderer>();
		percentageBarObjRedRenderer = percentageBarObjRed.GetComponent<Renderer>();
		completionMarkerRenderer = completionMarker.GetComponent<Renderer>();
		percentageBarGO = percentageBarObj.gameObject;
		percentageBarRedGO = percentageBarObjRed.gameObject;
		MatchUIWidth();
		CreateTeamBars();
		percentageBarObj.up = Vector3.right;
		percentageBarObjRed.up = Vector3.right;
		SetVis(false);
		ReferenceMaster.onResolutionChanged = (Action)Delegate.Combine(ReferenceMaster.onResolutionChanged, new Action(OnResolutionChanged));
	}

	private void OnDestroy()
	{
		ReferenceMaster.onResolutionChanged = (Action)Delegate.Remove(ReferenceMaster.onResolutionChanged, new Action(OnResolutionChanged));
	}

	private void OnResolutionChanged()
	{
		float[] array = (float[])teamCompletion.Clone();
		ResetProgress();
		MatchUIWidth();
		forceInstantUpdate = true;
		for (int i = 0; i < array.Length; i++)
		{
			AddProgress((MPTeam)(i + 1), array[i]);
		}
	}

	public void MatchUIWidth()
	{
		startZPosBG = bgObj.position.z;
		startZPosPercentageBar = percentageBarObj.position.z;
		leftSide = hudCam.ScreenToWorldPoint(new Vector2(0f, 0f));
		rightSide = hudCam.ScreenToWorldPoint(new Vector2(hudCam.pixelWidth, 0f));
		Vector3 vector = Vector3.Lerp(leftSide, rightSide, percent / 100f);
		Vector3 offset = vector - leftSide;
		Vector3 scale = new Vector3(Width, offset.x, Width);
		SetBG(leftSide, rightSide, Width);
		UpdateBars(offset, scale, true);
		SetPercentBox();
		UpdateTeamBars();
	}

	private void Update()
	{
		if (StatMaster.levelSimulating)
		{
			wasSimulating = true;
			SetVis(true);
			lerpSpeed = 6f;
		}
		else if (wasSimulating)
		{
			wasSimulating = false;
			SetProgress(0f);
			lerpSpeed = 10000f;
			fullPercent = 0f;
			SetVis(false);
			percentBox.position = new Vector3(leftSide.x, percentBox.position.y, percentBox.position.z);
		}
		float num = lerpSpeed;
		if (forceInstantUpdate)
		{
			lerpSpeed = 10000f;
		}
		if (StatMaster.isMP)
		{
			bool flag = false;
			Machine machine = null;
			if (PlayerData.hasLocalPlayer && !PlayerData.localPlayer.isSpectator)
			{
				machine = PlayerData.localPlayer.machine;
				flag = true;
			}
			if ((!flag || !machine.isSimulating) && (flag || !StatMaster.levelSimulating) && !SingleInstanceFindOnly<WinScreen>.Instance.Visible)
			{
				if (percentageBarGO.activeSelf)
				{
					percentageBarGO.SetActive(false);
					percentageBarRedGO.SetActive(false);
				}
				ToggleTeamBars(false);
				forceInstantUpdate = false;
				return;
			}
			if (!percentageBarGO.activeSelf)
			{
				percentageBarGO.SetActive(true);
				percentageBarRedGO.SetActive(true);
			}
			ToggleTeamBars(true);
		}
		for (int i = 0; i < teamBars.Length; i++)
		{
			TeamBar teamBar = teamBars[i];
			float num2 = teamCompletion[teamBar.index];
			if (teamBar.isUpdating)
			{
				Vector3 vector = Vector3.Lerp(leftSide, rightSide, num2 / 100f);
				Vector3 offset = vector - leftSide;
				float num3 = Mathf.Abs(offset.x - teamBar.transform.localScale.y);
				if (num3 > updateBarThreshold)
				{
					teamBar.UpdateBar(lerpSpeed, offset, offset.x);
					continue;
				}
				teamBar.SetPercentageText(Mathf.RoundToInt(num2));
				teamBar.isUpdating = false;
			}
			else if (teamBar.updatingWidth)
			{
				teamBar.UpdateWidth();
			}
		}
		if (updatingBar)
		{
			if (StatMaster.levelSimulating)
			{
				SetPercentBox();
				SetBG(leftSide, rightSide, Width);
			}
			Vector3 vector2 = Vector3.Lerp(leftSide, rightSide, percent / 100f);
			Vector3 offset2 = vector2 - leftSide;
			Vector3 scale = new Vector3(Width, offset2.x, Width);
			float num4 = Mathf.Abs(scale.y - percentageBarObjRed.localScale.y);
			if (num4 > updateBarThreshold)
			{
				UpdateBars(offset2, scale);
			}
			else
			{
				updatingBar = false;
			}
		}
		if (forceInstantUpdate)
		{
			lerpSpeed = num;
			forceInstantUpdate = false;
		}
	}

	private void SetPercentBox()
	{
		if (percentBox.gameObject.activeInHierarchy)
		{
			float x = Vector3.Lerp(leftSide, rightSide, percent / 100f).x;
			x = Mathf.Clamp(x, leftPosCap.position.x, rightPosCap.position.x);
			float t = TimeSlider.Instance.deltaTime * lerpSpeed;
			float x2 = percentBox.position.x;
			if (x2 != x)
			{
				percentBox.position = new Vector3(Mathf.Lerp(x2, x, t), percentBox.position.y, percentBox.position.z);
			}
			smoothPercentage = Mathf.Lerp(smoothPercentage, percent, t);
			percentText.text = smoothPercentage.ToString("f0") + "%";
		}
	}

	private void SetBG(Vector3 start, Vector3 end, float width)
	{
		Vector3 up = end - start;
		Vector3 localScale = new Vector3(width, up.x, width);
		Vector3 vector = start + new Vector3(up.x / 2f, 0f, 0f);
		bgObj.position = new Vector3(vector.x, vector.y, startZPosBG);
		bgObj.transform.up = up;
		bgObj.transform.localScale = localScale;
	}

	private void UpdateBars(Vector3 offset, Vector3 scale, bool force = false)
	{
		float x = leftSide.x + offset.x / 2f;
		float num = TimeSlider.Instance.deltaTime * lerpSpeed;
		Vector3 vector = new Vector3(x, leftSide.y, startZPosPercentageBar);
		if (force || percentageBarObj.position != vector)
		{
			percentageBarObj.position = vector;
			percentageBarObj.localScale = scale;
		}
		if (force || percentageBarObjRed.position != vector)
		{
			percentageBarObjRed.position = Vector3.Lerp(percentageBarObjRed.position, vector, (!force) ? num : 1f);
			percentageBarObjRed.localScale = Vector3.Lerp(percentageBarObjRed.localScale, scale + new Vector3(0f, 0f, 0.1f), (!force) ? num : 1f);
		}
	}

	private void SetVis(bool toggle)
	{
		if (toggle != currentVis)
		{
			currentVis = toggle;
			bgObjRenderer.enabled = toggle;
			percentageBarObjRenderer.enabled = toggle;
			percentageBarObjRedRenderer.enabled = toggle;
			completionMarkerRenderer.enabled = toggle;
		}
	}
}
