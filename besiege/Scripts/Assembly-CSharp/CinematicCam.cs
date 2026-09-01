using System;
using System.Collections;
using System.IO;
using Selectors;
using UnityEngine;

public class CinematicCam : SingleInstanceFindOnly<CinematicCam>
{
	[Serializable]
	public class CamSetting
	{
		public bool assigned;

		public Vector3 position;

		public float distance;

		public Quaternion rotation;

		public Vector3 camPos;

		public Quaternion camRot;

		public CamSetting()
		{
			assigned = false;
		}

		public CamSetting(Vector3 position, float distance, Quaternion rotation, Vector3 camPos, Quaternion camRot, bool assigned = true)
		{
			this.position = position;
			this.distance = distance;
			this.rotation = rotation;
			this.assigned = assigned;
			this.camPos = camPos;
			this.camRot = camRot;
		}

		public static CamSetting Clone(CamSetting source)
		{
			return new CamSetting(source.position, source.distance, source.rotation, source.camPos, source.camRot, source.assigned);
		}
	}

	public static bool EmulatingKey;

	public GameObject container;

	public UIButton closeButton;

	[Header("Header")]
	public TextHolder fileName;

	public UIButtonExtended saveButton;

	public UIButtonExtended loadButton;

	public UIButtonExtended relativeSaveLoad;

	[Header("Camera Controls")]
	public UIButtonExtended posStartButton;

	public UIButtonExtended posEndButton;

	public SliderSelector slider;

	public KeySelector emulateKeyContainer;

	public SliderSelector emulateDelay;

	public UIButtonExtended shaderSpeedButton;

	public SliderSelector shaderSpeed;

	public UIButtonExtended shaderOffsetButton;

	public SliderSelector shaderOffset;

	public GameObject helpText;

	public AnimationCurve ease;

	[Header("Mode Buttons")]
	public UIButtonExtended testButton;

	public UIButtonExtended relativeToMachine;

	public UIButtonExtended easeButton;

	public UIButtonExtended hideCursor;

	[Header("Social Media Buttons")]
	public UIButtonExtended tiktokButton;

	public TextHolder aspectText;

	public UIButtonExtended tiktokUIButton;

	[Header("Begin Button")]
	public UIButtonExtended beginButton;

	public DynamicText beginText;

	[Header("References")]
	public Transform camTransform;

	public Transform startIcon;

	public Transform endIcon;

	public LineRenderer startLine;

	public LineRenderer endLine;

	public UIButton startButton;

	public UIButton endButton;

	public LineRenderer lineRenderer;

	public int numberOfSegments = 30;

	public Texture2D cursorTexture;

	public Texture2D miniCursorTexture;

	public Transform tiktokUI;

	public CamSetting start = new CamSetting();

	public CamSetting end = new CamSetting();

	protected MouseOrbit camscript;

	protected bool socialMediaMode;

	protected bool socialMediaUI;

	protected bool transitioning;

	protected bool relativeCamera;

	protected bool easing;

	protected bool showCursor = true;

	public static bool offsetShader;

	public static bool changedSpeedShader;

	protected bool relativeSaveLoadActive = true;

	private bool wasHidden;

	private static MKey key;

	private static int ratioW = 2;

	private static int ratioH = 3;

	public override string Name
	{
		get
		{
			return "CinematicCam";
		}
	}

	public bool emulateKey
	{
		get
		{
			return key.KeysCount > 0 && key.GetKey(0) != KeyCode.None;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		camscript = SingleInstanceFindOnly<MouseOrbit>.Instance;
		closeButton.Click += Close;
		if (shaderOffsetButton != null)
		{
			shaderOffsetButton.Down += ToggleShaderOffset;
			shaderOffsetButton.ToggleBG(offsetShader);
		}
		if (shaderSpeedButton != null)
		{
			shaderSpeedButton.Down += ToggleShaderSpeed;
			shaderSpeedButton.ToggleBG(changedSpeedShader);
		}
		key = new MKey("Cinematic Camera Key", "cinematic", KeyCode.None, true);
		if (emulateKeyContainer != null)
		{
			emulateKeyContainer.Key = key;
			emulateKeyContainer.Init();
		}
		if (relativeToMachine != null)
		{
			relativeToMachine.Down += ToggleRelativeCamera;
			relativeToMachine.ToggleBG(relativeCamera);
		}
		if (easeButton != null)
		{
			easeButton.Down += ToggleEase;
			easeButton.ToggleBG(easing);
		}
		if (hideCursor != null)
		{
			hideCursor.Down += ToggleCursor;
			hideCursor.ToggleBG(showCursor);
		}
		posStartButton.Down += PosA;
		posEndButton.Down += PosB;
		beginButton.Click += Transition;
		testButton.Click += Test;
		slider.OnChanged += KillTransition;
		tiktokButton.Click += ToggleTikTok;
		aspectText.TextChanged += SetRatio;
		tiktokUIButton.Click += ToggleTikTokUI;
		SetSocialUI();
		startButton.Click += SetCamStart;
		endButton.Click += SetCamEnd;
		ReferenceMaster.onMachinePostSim = (Action<Machine>)Delegate.Combine(ReferenceMaster.onMachinePostSim, new Action<Machine>(Simulate));
		if (saveButton != null)
		{
			saveButton.Down += Save;
		}
		if (loadButton != null)
		{
			loadButton.Down += Load;
		}
		if (relativeSaveLoad != null)
		{
			relativeSaveLoad.SetIconAlpha((!relativeSaveLoadActive) ? 0.2f : 1f);
			relativeSaveLoad.Down += delegate
			{
				relativeSaveLoadActive = !relativeSaveLoadActive;
				relativeSaveLoad.SetIconAlpha((!relativeSaveLoadActive) ? 0.2f : 1f);
			};
		}
	}

	private void OnDestroy()
	{
		ReferenceMaster.onMachinePostSim = (Action<Machine>)Delegate.Remove(ReferenceMaster.onMachinePostSim, new Action<Machine>(Simulate));
		WaterController.timeOffset = 0f;
		Shader.SetGlobalFloat("_TimeOffset", 0f);
	}

	private void Simulate(Machine m)
	{
		Machine machine = Machine.Active();
		if (m == machine && emulateKey)
		{
			machine.SetHasEmulation(true);
			machine.InputController.AddMKey(Machine.Active().FirstBlock, key, KeyCode.Exclaim);
		}
	}

	public void RelativePoint(Transform pivot, CamSetting p, bool localSpace)
	{
		if (relativeSaveLoadActive)
		{
			if (localSpace)
			{
				p.position = pivot.InverseTransformPoint(p.position);
				p.rotation = Quaternion.Inverse(pivot.rotation) * p.rotation;
				p.camPos = pivot.InverseTransformPoint(p.camPos);
				p.camRot = Quaternion.Inverse(pivot.rotation) * p.camRot;
			}
			else
			{
				p.position = pivot.TransformPoint(p.position);
				p.rotation = pivot.rotation * p.rotation;
				p.camPos = pivot.TransformPoint(p.camPos);
				p.camRot = pivot.rotation * p.camRot;
			}
		}
	}

	public void Save()
	{
		string text = Path.Combine(StaticSettings.DataPath, "CameraSaves");
		Directory.CreateDirectory(text);
		string valueText = fileName.ValueText;
		text = Path.Combine(text, valueText + ".xml");
		if (!File.Exists(text))
		{
			CinematicCameraSave cinematicCameraSave = new CinematicCameraSave();
			cinematicCameraSave.TimeScale = TimeSlider.Instance.delegateTimeScale;
			cinematicCameraSave.PosA = CamSetting.Clone(start);
			cinematicCameraSave.PosB = CamSetting.Clone(end);
			cinematicCameraSave.Duration = slider.Value;
			cinematicCameraSave.Key = key.GetKey(0);
			cinematicCameraSave.KeyDelay = emulateDelay.Value;
			cinematicCameraSave.DelayShader = offsetShader;
			cinematicCameraSave.ShaderDelay = shaderOffset.Value;
			cinematicCameraSave.RetimeShader = changedSpeedShader;
			cinematicCameraSave.ShaderSpeed = shaderSpeed.Value;
			cinematicCameraSave.FollowMachine = relativeCamera;
			cinematicCameraSave.Ease = easing;
			cinematicCameraSave.ShowCursor = showCursor;
			CinematicCameraSave cinematicCameraSave2 = cinematicCameraSave;
			Transform buildingMachine = Machine.Active().BuildingMachine;
			RelativePoint(buildingMachine, cinematicCameraSave2.PosA, true);
			RelativePoint(buildingMachine, cinematicCameraSave2.PosB, true);
			CinematicCameraSave.Save(text, cinematicCameraSave2);
			Debug.Log("saved to: " + text);
		}
		else
		{
			Debug.Log("Can't overwrite file: " + text);
		}
	}

	public void Load()
	{
		string text = Path.Combine(StaticSettings.DataPath, "CameraSaves");
		Directory.CreateDirectory(text);
		string valueText = fileName.ValueText;
		text = Path.Combine(text, valueText + ".xml");
		if (File.Exists(text))
		{
			CinematicCameraSave cinematicCameraSave = new CinematicCameraSave();
			CinematicCameraSave.Load(text, cinematicCameraSave);
			TimeSliderView timeSliderView = UnityEngine.Object.FindObjectOfType<TimeSliderView>();
			if ((bool)timeSliderView)
			{
				timeSliderView.SetPercentage(cinematicCameraSave.TimeScale);
			}
			Transform buildingMachine = Machine.Active().BuildingMachine;
			RelativePoint(buildingMachine, cinematicCameraSave.PosA, false);
			RelativePoint(buildingMachine, cinematicCameraSave.PosB, false);
			PosA(cinematicCameraSave.PosA);
			PosB(cinematicCameraSave.PosB);
			slider.Value = cinematicCameraSave.Duration;
			key.AddOrReplaceKey(0, cinematicCameraSave.Key);
			emulateDelay.Value = cinematicCameraSave.KeyDelay;
			offsetShader = cinematicCameraSave.DelayShader;
			shaderOffset.Value = cinematicCameraSave.ShaderDelay;
			changedSpeedShader = cinematicCameraSave.RetimeShader;
			shaderSpeed.Value = cinematicCameraSave.ShaderSpeed;
			relativeCamera = cinematicCameraSave.FollowMachine;
			easing = cinematicCameraSave.Ease;
			showCursor = cinematicCameraSave.ShowCursor;
			Debug.Log("loaded: " + text);
			if (shaderOffsetButton != null)
			{
				shaderOffsetButton.ToggleBG(offsetShader);
			}
			if (shaderSpeedButton != null)
			{
				shaderSpeedButton.ToggleBG(changedSpeedShader);
			}
			if (relativeToMachine != null)
			{
				relativeToMachine.ToggleBG(relativeCamera);
			}
			if (easeButton != null)
			{
				easeButton.ToggleBG(easing);
			}
			if (hideCursor != null)
			{
				hideCursor.ToggleBG(showCursor);
			}
		}
		else
		{
			Debug.Log("No such file: " + text);
		}
	}

	private void ToggleShaderSpeed()
	{
		if (!StatMaster.isMP || StatMaster.IsLevelEditorOnly)
		{
			changedSpeedShader = !changedSpeedShader;
			shaderSpeedButton.ToggleBG(changedSpeedShader);
		}
	}

	private void ToggleShaderOffset()
	{
		if (!StatMaster.isMP || StatMaster.IsLevelEditorOnly)
		{
			offsetShader = !offsetShader;
			shaderOffsetButton.ToggleBG(offsetShader);
		}
	}

	private void ToggleRelativeCamera()
	{
		relativeCamera = !relativeCamera;
		relativeToMachine.ToggleBG(relativeCamera);
	}

	private void ToggleEase()
	{
		easing = !easing;
		easeButton.ToggleBG(easing);
	}

	private void ToggleCursor()
	{
		showCursor = !showCursor;
		hideCursor.ToggleBG(showCursor);
	}

	private void Update()
	{
		if (helpText.activeSelf != (posStartButton.IsHovered || posEndButton.IsHovered))
		{
			helpText.SetActive(posStartButton.IsHovered || posEndButton.IsHovered);
		}
		if (!transitioning)
		{
			return;
		}
		if (StatMaster.hudHidden && !wasHidden)
		{
			wasHidden = true;
		}
		else if (!StatMaster.hudHidden && wasHidden)
		{
			if (showCursor)
			{
				Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
			}
			else
			{
				Cursor.SetCursor(miniCursorTexture, Vector2.zero, CursorMode.Auto);
			}
			wasHidden = false;
		}
	}

	public static void Create()
	{
		if (!SingleInstanceFindOnly<CinematicCam>.hasInstance())
		{
			Debug.Log("Creating Cinematic Cam UI");
			UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/CinematicCam/CinematicCamera"));
			SingleInstanceFindOnly<CinematicCam>.Initialize();
		}
	}

	public static void Close()
	{
		if (!(SingleInstanceFindOnly<CinematicCam>.Instance == null))
		{
			SingleInstanceFindOnly<CinematicCam>.Instance.StopAllCoroutines();
			SingleInstanceFindOnly<CinematicCam>.Instance.startIcon.gameObject.SetActive(false);
			SingleInstanceFindOnly<CinematicCam>.Instance.endIcon.gameObject.SetActive(false);
			SingleInstanceFindOnly<CinematicCam>.Instance.camTransform.gameObject.SetActive(false);
			UnityEngine.Object.Destroy(SingleInstanceFindOnly<CinematicCam>.Instance.transform.parent.gameObject);
			if (EmulatingKey)
			{
				EmulatingKey = false;
				EmulateKeys(false);
			}
		}
	}

	private void PosA()
	{
		PosA(new CamSetting(camscript.PosComposite, camscript.distance, camscript.rotation, camscript.transform.position, camscript.transform.rotation));
	}

	private void PosB()
	{
		PosB(new CamSetting(camscript.PosComposite, camscript.distance, camscript.rotation, camscript.transform.position, camscript.transform.rotation));
	}

	private void PosA(CamSetting info)
	{
		start = CamSetting.Clone(info);
		posStartButton.ToggleBG(info.assigned);
		startIcon.position = info.camPos;
		startIcon.rotation = info.camRot;
		SetDirectionalLine(start, startLine, true);
		SetLine();
		KillTransition();
	}

	private void PosB(CamSetting info)
	{
		end = CamSetting.Clone(info);
		posEndButton.ToggleBG(info.assigned);
		endIcon.position = info.camPos;
		endIcon.rotation = info.camRot;
		SetDirectionalLine(end, endLine, true);
		SetLine();
		KillTransition();
	}

	private void SetDirectionalLine(CamSetting cam, LineRenderer ren, bool active)
	{
		if (!cam.assigned || !active)
		{
			ren.gameObject.SetActive(false);
			return;
		}
		ren.gameObject.SetActive(true);
		ren.SetPosition(0, cam.camPos);
		ren.SetPosition(1, cam.position);
		ren.material.mainTextureScale = new Vector2(Vector3.Distance(cam.camPos, cam.position), 1f);
	}

	public void SetCamStart()
	{
		SetCam(start);
	}

	public void SetCamEnd()
	{
		SetCam(end);
	}

	public void SetCam(CamSetting cam)
	{
		camscript.SetCameraPositionAndRotation(cam.camPos, cam.camRot);
	}

	private void Transition()
	{
		transitioning = !transitioning;
		camscript.cinematic = transitioning;
		if (!transitioning || !start.assigned || !end.assigned)
		{
			KillTransition();
			return;
		}
		if (StatMaster.hudHidden)
		{
			wasHidden = true;
		}
		else if (!StatMaster.hudHidden)
		{
			if (showCursor)
			{
				Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
			}
			else
			{
				Cursor.SetCursor(miniCursorTexture, Vector2.zero, CursorMode.Auto);
			}
			wasHidden = false;
		}
		container.gameObject.SetActive(false);
		lineRenderer.gameObject.SetActive(false);
		startIcon.gameObject.SetActive(false);
		endIcon.gameObject.SetActive(false);
		camTransform.gameObject.SetActive(false);
		StartCoroutine(IETransition(camscript.transform));
		if (emulateKey)
		{
			StartCoroutine(IEEmulateKey());
		}
	}

	private void SetRatio(string txt)
	{
		txt = txt.Replace(" ", ":").Replace("/", ":");
		string[] array = txt.Split(':');
		int result;
		int result2;
		if (array.Length == 2 && int.TryParse(array[0], out result) && int.TryParse(array[1], out result2))
		{
			ratioW = result;
			ratioH = result2;
			if (socialMediaMode)
			{
				Camera main = Camera.main;
				SetInternalAspect(main, ratioW, ratioH);
			}
		}
		aspectText.SetText(ratioW + ":" + ratioH);
		SetSocialUI();
	}

	private void ToggleTikTokUI()
	{
		socialMediaUI = !socialMediaUI;
		tiktokUI.parent.gameObject.SetActive(socialMediaUI);
		tiktokUIButton.ToggleBG(socialMediaUI);
		SetSocialUI();
	}

	private void SetSocialUI()
	{
		float num = (float)ratioW / (1f * (float)ratioH);
		float num2 = (float)Screen.height / (1f * (float)Screen.width);
		float num3 = num / num2;
		if (num3 < 1.19f)
		{
			num3 = 1f;
		}
		tiktokUI.localScale = num3 * Vector3.one;
	}

	private void ToggleTikTok()
	{
		socialMediaMode = !socialMediaMode;
		tiktokButton.ToggleBG(socialMediaMode);
		Camera main = Camera.main;
		if (socialMediaMode)
		{
			SetInternalAspect(main, ratioW, ratioH);
		}
		else
		{
			main.rect = new Rect(Vector2.zero, Vector2.one);
		}
	}

	public void SetInternalAspect(Camera cam, int W, int H)
	{
		float num = (float)W / (1f * (float)H);
		float num2 = (float)Screen.height / (1f * (float)Screen.width);
		float num3 = 0.5f;
		float num4 = num2 * num;
		float x = num3 / 2f - (num4 - num3) / 2f;
		if (num4 > 0f)
		{
			cam.rect = new Rect(new Vector2(x, 0f), new Vector2(num4, 1f));
		}
	}

	private void SetShaderTimeOffset(float offset)
	{
		WaterController.timeOffset = offset - Time.timeSinceLevelLoad;
		Shader.SetGlobalFloat("_TimeOffset", WaterController.timeOffset);
	}

	private void SetShaderSpeed(float speed)
	{
		WaterController.globalSpeed = speed;
		Shader.SetGlobalFloat("_TimeSpeed", WaterController.globalSpeed);
	}

	private void ResetShaderTimeOffset()
	{
		WaterController.timeOffset = 0f;
		Shader.SetGlobalFloat("_TimeOffset", 0f);
	}

	private void Test()
	{
		transitioning = !transitioning;
		if (!transitioning || !start.assigned || !end.assigned)
		{
			KillTransition();
			return;
		}
		testButton.ToggleBG(true);
		camTransform.gameObject.SetActive(true);
		camTransform.position = startIcon.position;
		camTransform.rotation = startIcon.rotation;
		StartCoroutine(IETransition(camTransform, true));
	}

	private void KillTransition()
	{
		camscript.cinematic = false;
		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		wasHidden = false;
		container.gameObject.SetActive(true);
		lineRenderer.gameObject.SetActive(start.assigned && end.assigned);
		startIcon.gameObject.SetActive(start.assigned);
		endIcon.gameObject.SetActive(end.assigned);
		testButton.ToggleBG(false);
		camTransform.gameObject.SetActive(false);
		beginText.SetText("BEGIN");
		StopAllCoroutines();
		if (EmulatingKey)
		{
			EmulatingKey = false;
			EmulateKeys(false);
		}
	}

	private IEnumerator IEEmulateKey()
	{
		if (emulateDelay.Value > 0f)
		{
			for (float f = 0f; f < emulateDelay.Value; f += TimeSlider.Instance.deltaTime)
			{
				if (!transitioning)
				{
					break;
				}
				yield return null;
			}
		}
		if (transitioning)
		{
			EmulatingKey = true;
			EmulateKeys(true);
		}
	}

	protected static void EmulateKeys(bool emulate)
	{
		if (Machine.Active().isSimulating)
		{
			KeyInputController inputController = Machine.Active().InputController;
			if (inputController.HasKey(key))
			{
				inputController.Emulate(Machine.Active().FirstBlock, new MKey[0], key, emulate);
			}
		}
	}

	private IEnumerator IETransition(Transform cam, bool allowRightClick = false)
	{
		if (!StatMaster.isMP || StatMaster.IsLevelEditorOnly)
		{
			if (offsetShader)
			{
				SetShaderTimeOffset(shaderOffset.Value);
			}
			if (changedSpeedShader)
			{
				SetShaderSpeed(shaderSpeed.Value);
			}
		}
		beginText.SetText("STOP");
		float pct = 0f;
		if (emulateKey)
		{
			if (emulateDelay.Value < 0f)
			{
				float time = emulateDelay.Value * -1f;
				for (float f = 0f; f < time; f += TimeSlider.Instance.deltaTime)
				{
					if (!allowRightClick && InputManager.RotateCameraKey())
					{
						break;
					}
					yield return null;
				}
			}
		}
		else
		{
			float time2 = TimeSlider.Instance.time;
			while (TimeSlider.Instance.time - time2 < 0.5f)
			{
				yield return null;
			}
		}
		Transform target = SingleInstanceFindOnly<MouseOrbit>.Instance.target;
		CamSetting start = (relativeCamera ? new CamSetting(this.start.position - target.position, this.start.distance, this.start.rotation, camscript.transform.position, camscript.transform.rotation) : this.start);
		CamSetting end = (relativeCamera ? new CamSetting(this.end.position - target.position, this.end.distance, this.end.rotation, camscript.transform.position, camscript.transform.rotation) : this.end);
		for (float f2 = 0f; f2 < slider.Value; f2 += TimeSlider.Instance.deltaTime)
		{
			pct = ((!easing) ? (f2 / slider.Value) : ease.Evaluate(f2 / slider.Value));
			if (target == null)
			{
				break;
			}
			Vector3 position = Vector3.Lerp(start.position + ((!relativeCamera) ? Vector3.zero : target.position), end.position + ((!relativeCamera) ? Vector3.zero : target.position), pct);
			float distance = Mathf.Lerp(start.distance, end.distance, pct);
			Quaternion rotation = Quaternion.Lerp(start.rotation, end.rotation, pct);
			rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, 0f);
			cam.rotation = rotation;
			cam.position = rotation * new Vector3(0f, 0f, 0f - distance) + position;
			if (MouseOrbit.CameraMoved != null)
			{
				MouseOrbit.CameraMoved(cam.position);
			}
			if (!allowRightClick && InputManager.RotateCameraKey())
			{
				break;
			}
			yield return null;
		}
		while (allowRightClick || !InputManager.RotateCameraKey())
		{
			yield return null;
		}
		testButton.ToggleBG(false);
		camTransform.gameObject.SetActive(false);
		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		wasHidden = false;
		container.gameObject.SetActive(true);
		lineRenderer.gameObject.SetActive(true);
		startIcon.gameObject.SetActive(true);
		endIcon.gameObject.SetActive(true);
		transitioning = false;
		camscript.cinematic = false;
		beginText.SetText("BEGIN");
		if (EmulatingKey)
		{
			EmulatingKey = false;
			EmulateKeys(false);
		}
		SetShaderSpeed(1f);
		ResetShaderTimeOffset();
	}

	private void SetLine()
	{
		if (start.assigned && end.assigned)
		{
			lineRenderer.gameObject.SetActive(true);
			float num = 0f;
			lineRenderer.SetVertexCount(numberOfSegments);
			for (int i = 0; i < numberOfSegments; i++)
			{
				num = (float)i / ((float)(numberOfSegments - 1) * 1f);
				Vector3 vector = Vector3.Lerp(start.position, end.position, num);
				float num2 = Mathf.Lerp(start.distance, end.distance, num);
				Quaternion quaternion = Quaternion.Lerp(start.rotation, end.rotation, num);
				quaternion.eulerAngles = new Vector3(quaternion.eulerAngles.x, quaternion.eulerAngles.y, 0f);
				vector = quaternion * new Vector3(0f, 0f, 0f - num2) + vector;
				lineRenderer.SetPosition(i, vector);
			}
		}
	}
}
