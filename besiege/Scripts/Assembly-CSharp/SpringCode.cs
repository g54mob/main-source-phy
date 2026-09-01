using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

[AddComponentMenu("Blocks/Block Behaviours/SpringCode")]
public class SpringCode : GenericDraggedBlock, ISnapable
{
	private const float SMALL = 1.4E-44f;

	private const float SIZE_THRESHOLD = 0.001f;

	public float springPower = 1000f;

	public float restingSpringPower = 0.02f;

	public bool dampen;

	public float dampenAmount = 100f;

	public SpringJoint springJointToAdd;

	public float velocityDamper = 1.2f;

	public float velocityClamp = 10000f;

	public bool winchMode;

	public float winchRate = 4f;

	protected Transform winchRotateVis;

	protected Transform winchRotateVis2;

	public Vector3 winchRotationAngle;

	public ParticleSystem[] snappedDustParticles;

	public float maxRopeLength = 40f;

	public MeshRenderer ropeVis;

	public float tiling = 1f;

	public float ropeThreshold;

	[FormerlySerializedAs("audioSource")]
	public AudioSource sfx;

	public AudioSource[] extraSfx = new AudioSource[0];

	protected AudioMixerGroup mixer;

	protected AudioMixerGroup underwaterMixer;

	protected bool mixerIsAbove = true;

	public GameObject cylinderGO;

	public Transform snapTrigger;

	public bool denestRope = true;

	private RaycastHit hit;

	private float startMgntd;

	private float maxMagnitude;

	private bool hasBegun;

	private float currentLength;

	private bool snapped;

	private MKey contractKey;

	private MKey unwindKey;

	private MToggle toggleMode;

	private MToggle startUnwound;

	private MSlider speedSlider;

	private MSlider massSlider;

	private MSlider stringLengthSlider;

	private bool shouldContract;

	private bool shouldUnwind;

	private float sqrRopeThreshold;

	private Vector3 contractStartVec;

	private Vector3 contractEndVec;

	private Vector3 animateWinchVec = Vector3.zero;

	private bool doDampen;

	private float doDampenAmount;

	private Vector3 doDampenDiff;

	private float doDampenMag;

	private float lastCylSqr = -1f;

	private Vector3 cylScale;

	private Vector2 cylTexScale;

	private Vector3 cylPos;

	private Vector3 cylRot;

	public ConfigurableJoint confJoint1;

	public ConfigurableJoint confJoint2;

	protected bool hasJoint1;

	protected bool hasJoint2;

	protected bool jointJustBroke;

	private float newMass = 0.25f;

	private float oldMass = 0.5f;

	private static Vector3 DEFAULT_SCALE = Vector3.one;

	private static Vector3 VECTOR_UP = Vector3.up;

	private static Vector3 VECTOR_ZERO = Vector3.zero;

	private Vector3 LOCAL_UP = Vector3.up;

	private Vector4 tilings = new Vector4(1f, 1f, 0f, 0f);

	private float sqrMagnitude;

	private float sqrd;

	private float ox;

	private float oy;

	private float oz;

	private bool cylinderLocal = true;

	private bool physicsLocal = true;

	private bool tooSmall;

	private bool contractPressed;

	private bool unwindPressed;

	private bool contractHeld;

	private bool unwindHeld;

	private bool emuContractPressed;

	private bool emuUnwindPressed;

	private bool emuContractHeld;

	private bool emuUnwindHeld;

	private float lastMag;

	public MToggle ToggleModeToggle
	{
		get
		{
			return toggleMode;
		}
	}

	public MToggle StartUnwoundToggle
	{
		get
		{
			return startUnwound;
		}
	}

	public MSlider SpeedSlider
	{
		get
		{
			return speedSlider;
		}
	}

	public MKey ContractKey
	{
		get
		{
			return contractKey;
		}
	}

	public MKey UnwindKey
	{
		get
		{
			return unwindKey;
		}
	}

	public MSlider MassSlider
	{
		get
		{
			return massSlider;
		}
	}

	public override Vector3 GetCenter()
	{
		if (isSimulating)
		{
			return cylinder.position;
		}
		return base.GetCenter();
	}

	protected override void Awake()
	{
		cylScale = new Vector3(radius, radius, radius);
		cylTexScale = DEFAULT_SCALE;
		cylPos = VECTOR_ZERO;
		cylRot = VECTOR_ZERO;
		base.Awake();
		hasBegun = false;
		LOCAL_UP = base.transform.InverseTransformDirection(Vector3.up);
		if (isSimulating)
		{
			shouldContract = false;
			shouldUnwind = false;
			winchRotateVis = startVis.transform;
			winchRotateVis2 = endVis.transform;
			if (SimPhysics)
			{
				if (winchMode && denestRope)
				{
					snapTrigger.SetParent(base.transform.parent, true);
					physicsLocal = false;
				}
				if (winchMode || base.transform.localScale == DEFAULT_SCALE)
				{
					cylinder.SetParent(base.transform.parent, true);
					cylinderLocal = false;
					startPoint.SetParent(base.transform.parent, true);
					endPoint.SetParent(base.transform.parent, true);
					cylScale *= Mathf.Min(base.transform.localScale.x, base.transform.localScale.y, base.transform.localScale.z);
				}
				if (!stripped)
				{
					Rigidbody component = startInterpolater.GetComponent<Rigidbody>();
					RigidbodyInterpolation interpolation = RigidbodyInterpolation.Interpolate;
					endInterpolater.GetComponent<Rigidbody>().interpolation = interpolation;
					component.interpolation = interpolation;
				}
			}
			Vector3 localScale = base.transform.localScale;
			localScale.x = abs(localScale.x);
			localScale.y = abs(localScale.y);
			localScale.z = abs(localScale.z);
			if (localScale.x < 0.001f && localScale.y < 0.001f && localScale.z < 0.001f)
			{
				tooSmall = true;
			}
		}
		else if (SimPhysics && (bool)startBody)
		{
			oldMass = startBody.mass;
		}
		sqrRopeThreshold = ropeThreshold * ropeThreshold;
		speedSlider = AddSlider((!winchMode) ? 2473 : 2428, "slider", 1f, 0.3f, (!winchMode) ? 10f : 2f, string.Empty);
		if (winchMode)
		{
			contractKey = AddKey(2474, "contract", ControlScheme.BlockControls.Rope, 0, KeyCode.N);
			unwindKey = AddKey(2475, "unwind", ControlScheme.BlockControls.Rope, 1, KeyCode.M);
			startUnwound = AddToggle(2476, "start-unwound", false);
			stringLengthSlider = AddSlider(2467, "rope-length", 10f, 0f, 27f, string.Empty);
			stringLengthSlider.DisplayInMapper = !startUnwound.IsActive;
			startUnwound.Toggled += delegate(bool on)
			{
				stringLengthSlider.DisplayInMapper = !on;
			};
			massSlider = AddSlider("Mass", "point-mass", newMass, 0.01f, 10f, string.Empty);
			massSlider.DisplayInMapper = false;
			massSlider.ValueChanged += SetMass;
		}
		else
		{
			contractKey = AddKey(2477, "contract", ControlScheme.BlockControls.Spring, 0, KeyCode.L);
			toggleMode = AddToggle(2431, "toggle", false);
		}
	}

	public override void StartPhysics(bool isKinematic)
	{
		if (!object.ReferenceEquals(BlockHealth, null))
		{
			BlockHealth.StartPhysics();
		}
		hasJoint1 = true;
		hasJoint2 = true;
		if (!SimPhysics)
		{
			hasJoint1 = false;
			hasJoint2 = false;
			return;
		}
		if (confJoint1 == null)
		{
			hasJoint1 = false;
		}
		else if (confJoint1.connectedBody == null)
		{
			UnityEngine.Object.Destroy(confJoint1);
			hasJoint1 = false;
		}
		if (confJoint2 == null)
		{
			hasJoint2 = false;
		}
		else if (confJoint2.connectedBody == null)
		{
			UnityEngine.Object.Destroy(confJoint2);
			hasJoint2 = false;
		}
		if (winchMode)
		{
			if (hasJoint1)
			{
				winchRotateVis.parent = confJoint1.connectedBody.transform;
				AddVisToBlock(winchRotateVis, startVis);
			}
			else
			{
				UnityEngine.Object.Destroy(startPoint.gameObject);
			}
			if (hasJoint2)
			{
				winchRotateVis2.parent = confJoint2.connectedBody.transform;
				AddVisToBlock(winchRotateVis2, endVis);
			}
			else
			{
				UnityEngine.Object.Destroy(endPoint.gameObject);
			}
		}
	}

	public void BreakJoint(Joint j)
	{
		if (SimPhysics)
		{
			if (hasJoint1 && j == confJoint1)
			{
				hasJoint1 = false;
			}
			else if (hasJoint2 && j == confJoint2)
			{
				hasJoint2 = false;
			}
		}
	}

	private void SetMass(float mass)
	{
		if (SimPhysics)
		{
			Rigidbody rigidbody = startBody;
			endBody.mass = mass;
			rigidbody.mass = mass;
		}
	}

	protected override void Start()
	{
		base.Start();
		if (!isSimulating)
		{
			return;
		}
		float num = startMgntd;
		Vector3 vector = startPoint.position - endPoint.position;
		startMgntd = ((!winchMode) ? vector.sqrMagnitude : vector.magnitude);
		if (winchMode)
		{
			stringLengthSlider.SetRange(startMgntd, maxRopeLength);
		}
		maxMagnitude = (startMgntd += 0.1f);
		if (winchMode)
		{
			if (startUnwound.IsActive)
			{
				maxMagnitude = maxRopeLength;
			}
			else if (num > 0f)
			{
				maxMagnitude = stringLengthSlider.Value / num * maxMagnitude;
			}
		}
		if (sfx != null)
		{
			mixer = sfx.outputAudioMixerGroup;
			underwaterMixer = ReferenceMaster.GetWaterMixerFrom(mixer);
		}
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates(Prefab.RegisterSimUpdate, (!isSimulating) ? Prefab.RegisterSimFixedUpdate : SimPhysics, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	protected override void CreateCylinderBetweenPoints(Vector3 start, Vector3 end)
	{
		CreateCylinderBetweenPoints(cylinder, start, end, cylinderLocal);
	}

	protected void CreateCylinderBetweenPoints(Transform t, Vector3 start, Vector3 end, bool local)
	{
		if (local)
		{
			if (tooSmall)
			{
				if (t.gameObject.activeSelf)
				{
					t.gameObject.SetActive(false);
				}
				return;
			}
			start = base.transform.InverseTransformPoint(start);
			end = base.transform.InverseTransformPoint(end);
		}
		ox = end.x - start.x;
		oy = end.y - start.y;
		oz = end.z - start.z;
		sqrMagnitude = ox * ox + oy * oy + oz * oz;
		if (sqrMagnitude < 1.4E-44f || sqrMagnitude >= float.MaxValue)
		{
			cylinder.localScale = VECTOR_ZERO;
			return;
		}
		bool flag = t.gameObject.activeSelf;
		if (!isSimulating || sqrMagnitude > sqrRopeThreshold)
		{
			if (!flag)
			{
				t.gameObject.SetActive(true);
				flag = true;
			}
			sqrd = sqrMagnitude - lastCylSqr;
			if (abs(sqrd) > 0.001f)
			{
				float num = Mathf.Sqrt(sqrMagnitude);
				cylScale.z = num * 0.5f;
				t.localScale = cylScale;
				cylTexScale.x = tiling * num;
				tilings.x = cylTexScale.x;
				VisualController.SetTiling(tilings);
				lastCylSqr = sqrMagnitude;
			}
		}
		else if (flag)
		{
			t.gameObject.SetActive(false);
			flag = false;
		}
		if (flag)
		{
			cylPos.x = start.x + ox * 0.5f;
			cylPos.y = start.y + oy * 0.5f;
			cylPos.z = start.z + oz * 0.5f;
			cylRot.x = ox;
			cylRot.y = oy;
			cylRot.z = oz;
			if (local)
			{
				t.localPosition = cylPos;
				t.localRotation = Quaternion.LookRotation(cylRot, LOCAL_UP);
			}
			else
			{
				t.position = cylPos;
				t.rotation = Quaternion.LookRotation(cylRot, VECTOR_UP);
			}
		}
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (CheckKeys())
		{
			if (winchMode)
			{
				contractPressed = contractKey.IsPressed;
				unwindPressed = unwindKey.IsPressed;
				contractHeld = contractKey.IsHeld;
				unwindHeld = unwindKey.IsHeld;
				EvaluateWinchKeys(contractPressed, unwindPressed, contractHeld, unwindHeld, emuContractHeld, emuUnwindHeld);
			}
			else
			{
				contractPressed = contractKey.IsPressed;
				contractHeld = contractKey.IsHeld;
				EvaluateSpringKeys(contractPressed, contractHeld, emuContractHeld);
			}
		}
		if (!SimPhysics)
		{
			float magnitude = (startPoint.position - endPoint.position).magnitude;
			HandleSFX(magnitude);
			lastMag = magnitude;
		}
	}

	public override void EmulationUpdateBlock()
	{
		if (CheckKeys())
		{
			if (winchMode)
			{
				emuContractPressed = contractKey.EmulationPressed();
				emuUnwindPressed = unwindKey.EmulationPressed();
				emuContractHeld = contractKey.EmulationHeld(true);
				emuUnwindHeld = unwindKey.EmulationHeld(true);
				EvaluateWinchKeys(emuContractPressed, emuUnwindPressed, emuContractHeld, emuUnwindHeld, contractHeld, unwindHeld);
			}
			else
			{
				emuContractPressed = contractKey.EmulationPressed();
				emuContractHeld = contractKey.EmulationHeld(true);
				EvaluateSpringKeys(emuContractPressed, emuContractHeld, contractHeld);
			}
		}
	}

	protected bool CheckKeys()
	{
		if (!isSimulating || !isSet)
		{
			return false;
		}
		return true;
	}

	protected void EvaluateWinchKeys(bool contractPressed, bool unwindPressed, bool contractHeld, bool unwindHeld, bool altContractHeld, bool altUnwindHeld)
	{
		if (startUnwound.IsActive && (contractPressed || unwindPressed))
		{
			maxMagnitude = currentLength;
		}
		if (!snapped)
		{
			Begin();
			shouldContract = contractHeld || altContractHeld;
			shouldUnwind = unwindHeld || altUnwindHeld;
		}
	}

	protected void EvaluateSpringKeys(bool contractPressed, bool contractHeld, bool altContractHeld)
	{
		if (toggleMode.IsActive)
		{
			if (contractPressed)
			{
				shouldContract = !shouldContract;
			}
		}
		else
		{
			shouldContract = contractHeld || altContractHeld;
		}
	}

	protected void Begin()
	{
		if (!hasBegun)
		{
			hasBegun = true;
			SetCenterOfMass();
		}
	}

	public override void UpdateDragged()
	{
		SetDragged(startPoint.rotation);
	}

	public override void LateUpdateBlock()
	{
		CreateCylinderBetweenPoints(startInterpolater.position, endInterpolater.position);
	}

	public override void FixedUpdateBlock()
	{
		if (doDampen)
		{
			if (!snapped)
			{
				ContractNormalised(doDampenAmount, doDampenDiff, doDampenMag);
			}
			doDampen = false;
		}
		Vector3 delta = startPoint.position - endPoint.position;
		if (winchMode)
		{
			if (shouldContract && !shouldUnwind)
			{
				WinchContract(winchRate);
			}
			if (shouldUnwind && !shouldContract)
			{
				WinchUnwind(winchRate);
			}
			if (denestRope && !physicsLocal)
			{
				CreateCylinderBetweenPoints(snapTrigger, startPoint.position, endPoint.position, physicsLocal);
			}
		}
		else if (shouldContract)
		{
			Contract(1f, delta);
		}
		float magnitude = delta.magnitude;
		HandleSFX(magnitude);
		lastMag = magnitude;
		AutonomousLimiting(delta);
	}

	private void HandleSFX(float deltaMag)
	{
		AudioSource audioSource = ((!winchMode) ? sfx : extraSfx[0]);
		if (!audioSource.gameObject.activeInHierarchy)
		{
			return;
		}
		if (shouldContract != shouldUnwind)
		{
			if (!audioSource.isPlaying)
			{
				float num = ((!winchMode) ? 0f : Mathf.Abs(speedSlider.Value));
				num = ((!float.IsNaN(num)) ? (num * 0.02f) : 0.5f);
				audioSource.pitch = Mathf.Clamp(UnityEngine.Random.Range(0.9f, 1f) - num - ((!shouldUnwind) ? 0f : 0.15f), 0f, 8f);
				audioSource.timeSamples = UnityEngine.Random.Range(0, audioSource.clip.samples);
				audioSource.Play();
			}
			float num3;
			if (winchMode)
			{
				float num2 = 1f;
				num3 = ((!shouldUnwind) ? (Mathf.Clamp01(num2 * (0.05f + Mathf.Abs(deltaMag - lastMag)) * 20f) * 0.5f) : (Mathf.Clamp01(num2 * (0.01f + Mathf.Abs(deltaMag - lastMag)) * 10f) * 0.5f));
			}
			else
			{
				float num2 = ((!SimPhysics) ? (deltaMag * Mathf.Abs(springPower)) : contractStartVec.magnitude);
				num3 = Mathf.Clamp01(num2 * Mathf.Abs(deltaMag - lastMag)) * 0.5f;
			}
			if (num3 < 0.02f)
			{
				num3 = 0f;
			}
			audioSource.volume = num3;
			if (!WaterController.Exist)
			{
				return;
			}
			if (WaterController.IsUnderwater(cylinder.position))
			{
				if (mixerIsAbove)
				{
					audioSource.outputAudioMixerGroup = underwaterMixer;
					mixerIsAbove = false;
				}
			}
			else if (!mixerIsAbove)
			{
				audioSource.outputAudioMixerGroup = mixer;
				mixerIsAbove = true;
			}
		}
		else if (audioSource.isPlaying)
		{
			audioSource.Stop();
		}
	}

	protected void AutonomousLimiting(Vector3 delta)
	{
		if (winchMode)
		{
			currentLength = delta.magnitude;
			Damper(delta, currentLength, currentLength);
		}
		else
		{
			currentLength = delta.sqrMagnitude;
			Damper(delta, currentLength, Mathf.Sqrt(currentLength));
		}
	}

	protected void Damper(Vector3 delta, float fauxLength, float trueLength)
	{
		if (fauxLength > maxMagnitude)
		{
			if (dampen)
			{
				float num = (fauxLength - maxMagnitude) * 20f;
				ContractNormalised(num * restingSpringPower, delta, trueLength);
				doDampenAmount = num * dampenAmount;
				doDampenDiff.Set(delta.x, delta.y, delta.z);
				doDampenMag = trueLength;
				doDampen = true;
			}
			else
			{
				float num2 = (fauxLength - startMgntd) / 10f;
				Contract(((num2 < 0f) ? 0f : ((!(num2 > 1f)) ? num2 : 1f)) * restingSpringPower * speedSlider.Value, delta);
			}
		}
	}

	private void Contract(float scaler, Vector3 delta)
	{
		scaler = ((scaler < 0f - velocityClamp) ? (0f - velocityClamp) : ((!(scaler > velocityClamp)) ? scaler : velocityClamp));
		float num = springPower * scaler;
		delta *= num;
		contractStartVec.Set(delta.x, delta.y, delta.z);
		contractEndVec.Set(0f - contractStartVec.x, 0f - contractStartVec.y, 0f - contractStartVec.z);
		startBody.AddForce(contractStartVec);
		endBody.AddForce(contractEndVec);
	}

	public void Snap()
	{
		if (!isSimulating || snapped)
		{
			return;
		}
		if (base.HasParentMachine)
		{
			if (sfx != null && sfx.gameObject.activeInHierarchy)
			{
				sfx.pitch += UnityEngine.Random.Range(-0.1f, 0.2f);
				sfx.Play();
			}
			snappedDustParticles[0].transform.SetParent(base.transform.parent, true);
			snappedDustParticles[1].transform.SetParent(base.transform.parent, true);
			snappedDustParticles[0].transform.position = startPoint.position;
			snappedDustParticles[1].transform.position = endPoint.position;
			snappedDustParticles[0].Play();
			snappedDustParticles[1].Play();
		}
		snapped = true;
		if (_parentMachine != null)
		{
			if (StatMaster.isMP && SimPhysics)
			{
				NetworkBlock netBlock = NetBlock;
				if (netBlock != null)
				{
					netBlock.Event(NetworkEntity.EntityEvent.Break);
					netBlock.pollTransform = false;
				}
				else
				{
					Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
				}
			}
			_parentMachine.UnregisterUpdate(this, false);
			_parentMachine.UnregisterFixedUpdate(this, false);
			_parentMachine.UnregisterLateUpdate(this, false);
		}
		IsDestroyed = true;
		base.gameObject.SetActive(false);
		cylinderGO.SetActive(false);
		if (snapTrigger != null)
		{
			snapTrigger.gameObject.SetActive(false);
		}
	}

	private void ContractNormalised(float scaler, Vector3 delta, float magnitude)
	{
		float num = 0f - velocityClamp;
		if (magnitude > 0f)
		{
			float num2 = 1f / magnitude * springPower * ((scaler < num) ? num : ((!(scaler > velocityClamp)) ? scaler : velocityClamp));
			delta *= num2;
			contractStartVec.Set(delta.x, delta.y, delta.z);
		}
		else
		{
			contractStartVec.Set(0f, 0f, 0f);
		}
		contractEndVec.Set(0f - contractStartVec.x, 0f - contractStartVec.y, 0f - contractStartVec.z);
		startBody.AddForce(contractStartVec, ForceMode.Acceleration);
		endBody.AddForce(contractEndVec, ForceMode.Acceleration);
	}

	private void WinchContract(float rate)
	{
		maxMagnitude -= Time.fixedDeltaTime * rate * speedSlider.Value;
		maxMagnitude = Mathf.Max(maxMagnitude, 0.1f);
		AnimateWinch(1f);
	}

	private void WinchUnwind(float rate)
	{
		maxMagnitude += Time.fixedDeltaTime * rate * speedSlider.Value;
		AnimateWinch(-1f);
	}

	private void AnimateWinch(float invert)
	{
		float num = winchRotationAngle.z * Time.deltaTime * invert;
		if (num != 0f)
		{
			animateWinchVec.z = num;
			winchRotateVis.Rotate(animateWinchVec);
			animateWinchVec.z = 0f - num;
			winchRotateVis2.Rotate(animateWinchVec);
		}
	}

	private void AddVisToBlock(Transform targetTransform, Renderer r)
	{
		BlockBehaviour component = targetTransform.parent.GetComponent<BlockBehaviour>();
		if (!object.ReferenceEquals(component, null))
		{
			component.visAddedToMe.Add(r);
		}
	}

	public override bool Set(bool forceFail = false)
	{
		bool result = base.Set(forceFail);
		if (!IsDestroyed)
		{
			Vector3 vector = startPoint.position - endPoint.position;
			startMgntd = ((!winchMode) ? vector.sqrMagnitude : vector.magnitude);
			if (winchMode)
			{
				SetRopeLength(startMgntd);
			}
		}
		return result;
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (winchMode)
		{
			if (!isSimulating && data.WasLoadedFromFile && !data.HasKey("bmt-point-mass"))
			{
				massSlider.SetValue(oldMass);
				massSlider.ApplyValue();
			}
			startMgntd = (startPoint.position - endPoint.position).magnitude;
			if (!data.HasKey("bmt-rope-length"))
			{
				SetRopeLength(startMgntd);
			}
			else if (startMgntd > 0f && !isSimulating)
			{
				stringLengthSlider.SetRange(startMgntd, startMgntd * 2f);
			}
		}
	}

	private void SetRopeLength(float mag)
	{
		if (mag > 0f)
		{
			stringLengthSlider.SetRange(mag, (!startUnwound.IsActive) ? (mag * 2f) : maxRopeLength);
			stringLengthSlider.SetValue(mag);
			stringLengthSlider.ApplyValue();
		}
	}
}
