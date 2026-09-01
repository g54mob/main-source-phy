using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/SqrBalloonController")]
public class SqrBalloonController : BlockBehaviour
{
	private MSlider fixedHeight;

	private MSlider buoyancySlider;

	public float randomPressureRange = 0.2f;

	private MToggle automaticStartPressure;

	private bool igniteOnBreak = true;

	private MKey primaryKey;

	private MKey secondaryKey;

	private MColourSlider colourSlider;

	protected float lastTimeScale = 1f;

	private Vector3 liftVector;

	private float balloonPressure;

	private float currentPressureLayer;

	private float pressureDifference;

	private bool popped;

	public float popImpactThreshold = 450f;

	private bool wasKinematic;

	private float buoyancy;

	public float realMaxBuoyancy = 3f;

	public Vector3 centerOffset = new Vector3(0f, 0f, 1.5f);

	public float keyInputSpeed = 0.08f;

	private bool soaked;

	private float lastOffset;

	private float lastEmiss;

	private float soakingPct;

	private Color soakColor;

	[Header("References")]
	public GameObject balloonPopObj;

	public GameObject balloonPopIgnite;

	public Transform balloonVisObj;

	[Header("Light")]
	public float lightFlickerSpeed = 0.5f;

	public float lightFlickerAmount = 0.5f;

	private float randomLightValue;

	public float baseLightLevel = 0.47f;

	private float currentLightLevel;

	public float lightInputScale = 0.4f;

	public AnimationCurve hueToSaturation = AnimationCurve.Linear(0f, 0f, 1f, 0f);

	public AnimationCurve hueToHue = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	public AnimationCurve hueToLuminosity = AnimationCurve.Linear(0f, 0f, 1f, 0f);

	private Color defaultColor = new Color(1f, 0.9098039f, 0.75686276f, 1f);

	private float increaseIntensity;

	private float decreaseIntensity;

	[SerializeField]
	[Header("Forces")]
	protected float speed = 1f;

	[SerializeField]
	protected float maxBalloonPressure = 20f;

	[SerializeField]
	protected float maxForce = 10000f;

	[SerializeField]
	protected float baseForce = 120f;

	[SerializeField]
	protected float velLimit = 10f;

	[SerializeField]
	protected float pressureExponent = 2.5f;

	[SerializeField]
	protected float customDragMultiplier = 20f;

	[SerializeField]
	protected float dropMultiplier = 0.5f;

	private float prevYpos;

	private bool clientLocalSim;

	private float keyLightSpeed = 3f;

	private Color clothColor = Color.white;

	private float primaryKeyValue;

	private float primaryEmuValue;

	private float secondaryKeyValue;

	private float secondaryEmuValue;

	private float submerge;

	private float prevRandomRange;

	private float currentRandom;

	private float randomTime = 1.1f;

	private float expTime;

	[Header("Line Vis")]
	public LineRenderer line;

	public Transform bar;

	public MSlider FixedHeight
	{
		get
		{
			return fixedHeight;
		}
	}

	public MSlider PowerSlider
	{
		get
		{
			return buoyancySlider;
		}
	}

	public MToggle AutomaticStartPressure
	{
		get
		{
			return automaticStartPressure;
		}
	}

	public MKey MainKey
	{
		get
		{
			return primaryKey;
		}
	}

	public MKey SecondaryKey
	{
		get
		{
			return secondaryKey;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		primaryKey = AddKey(4412, "rise", ControlScheme.BlockControls.Balloon, 0, KeyCode.U);
		secondaryKey = AddKey(4413, "drop", ControlScheme.BlockControls.Balloon, 1, KeyCode.J);
		automaticStartPressure = AddToggle(4414, "ASP", true);
		automaticStartPressure.Toggled += AutomaticToggle;
		if (!isSimulating)
		{
			ToggleVisualisation(automaticStartPressure.IsActive);
		}
		automaticStartPressure.Toggled += ToggleVisualisation;
		fixedHeight = AddSliderUnclamped(3783, "height", (base.transform.rotation * centerOffset).y + base.transform.position.y, 0f, 250f, string.Empty);
		fixedHeight.logScaling = true;
		fixedHeight.DisplayInMapper = !automaticStartPressure.IsActive;
		fixedHeight.ValueChanged += SetFixedHeight;
		buoyancySlider = AddSlider(4613, "buoyancy", 1f, 0f, 1.5f, string.Empty);
		buoyancy = buoyancySlider.Value;
		buoyancySlider.ValueChanged += BuoyancySliderChanged;
		colourSlider = AddColourSlider(2504, "colour", defaultColor, true);
		SetSlip(colourSlider.Value);
		colourSlider.ValueChanged += SetSlip;
		if (isSimulating)
		{
			centerOffset = Vector3.Scale(centerOffset, base.transform.localScale);
			VisualController.AssignMaterialProperty("_Emission", 0f);
		}
	}

	public override void StartPhysics(bool isKinematic)
	{
		balloonPressure = ((!automaticStartPressure.IsActive) ? fixedHeight.Value : ((base.transform.rotation * centerOffset).y + base.transform.position.y));
	}

	protected override void Start()
	{
		base.Start();
		if (isSimulating)
		{
			balloonPopObj.transform.parent = ((!StatMaster.isMP) ? ReferenceMaster.physicsGoalInstance : base.transform.parent);
			balloonPopIgnite.transform.parent = ((!StatMaster.isMP) ? ReferenceMaster.physicsGoalInstance : base.transform.parent);
			soakColor = clothColor * 0.75f;
		}
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates(Prefab.RegisterSimUpdate, (!isSimulating) ? Prefab.RegisterSimFixedUpdate : SimPhysics, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	private void SetFixedHeight(float newStartPressure)
	{
		if (!automaticStartPressure.IsActive)
		{
			balloonPressure = newStartPressure;
		}
	}

	private void AutomaticToggle(bool isActive)
	{
		fixedHeight.DisplayInMapper = !isActive;
		if (isActive)
		{
			balloonPressure = (base.transform.rotation * centerOffset).y + base.transform.position.y;
		}
		else
		{
			balloonPressure = fixedHeight.Value;
		}
	}

	private void BuoyancySliderChanged(float newBuoyancy)
	{
		buoyancy = newBuoyancy;
	}

	public override void EmulationUpdateBlock()
	{
		primaryEmuValue = primaryKey.EmulationValue();
		secondaryEmuValue = ((!secondaryKey.DisplayInMapper) ? 0f : secondaryKey.EmulationValue());
		SetPressure(primaryEmuValue, secondaryEmuValue, Time.fixedDeltaTime);
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (!isSimulating)
		{
			if (!automaticStartPressure.IsActive && BlockMapper.IsOpen && BlockMapper.CurrentInstance.Current == this)
			{
				if (!line.gameObject.activeSelf)
				{
					ToggleVisualisation(automaticStartPressure.IsActive);
				}
				UpdateVisualisation();
			}
			else if (line.gameObject.activeSelf)
			{
				ToggleVisualisation(true);
			}
		}
		else
		{
			if (!_parentMachine.isReady)
			{
				return;
			}
			submerge = base.GetSubmergedPctMV;
			clientLocalSim = StatMaster.isClient && !StatMaster.isLocalSim;
			if (soaked)
			{
				if (soakingPct < 1f)
				{
					clothColor = Color.Lerp(clothColor, soakColor, soakingPct);
					VisualController.AssignMaterialProperty("_SlipColor", Color.Lerp(clothColor, Color.black, VisualController.BurnPct * 1.1f), false);
					VisualController.AssignMaterialProperty("_Emission", lastEmiss * (1f - soakingPct), false);
					VisualController.AssignMaterialProperty("_Cutoff", lastOffset * (1f - soakingPct));
					soakingPct += Time.deltaTime;
				}
				return;
			}
			primaryKeyValue = primaryKey.Value;
			secondaryKeyValue = ((!secondaryKey.DisplayInMapper) ? 0f : secondaryKey.Value);
			SetPressure(primaryKeyValue, secondaryKeyValue, Time.deltaTime);
			DisplayBurn(VisualController.BurnPct);
			if (!OptionsMaster.skinsEnabled || VisualController.selectedSkin.isDefault)
			{
				DisplayFire(Mathf.Max(primaryKeyValue, primaryEmuValue), Mathf.Max(secondaryKeyValue, secondaryEmuValue));
				DisplayPressure();
			}
			else
			{
				DisplayPressure(1f);
			}
		}
	}

	private void FixedUpdate()
	{
		if (StatMaster.levelSimulating)
		{
			if (!soaked && submergedPercent > 0.5f)
			{
				lastOffset = (float)VisualController.ReadMaterialProperty<float>("_Cutoff");
				lastEmiss = (float)VisualController.ReadMaterialProperty<float>("_Emission");
				soaked = true;
			}
			else if (clientLocalSim)
			{
				PressureBasedCalc();
				currentLightLevel = Mathf.Clamp01(liftVector.y / 5f) * baseLightLevel * 0.05f + currentLightLevel * 0.95f;
			}
		}
	}

	protected void SetPressure(float primaryKeyValue, float secondaryKeyValue, float delta)
	{
		if (!StatMaster.GodTools.GravityDisabled)
		{
			float num = primaryKeyValue - secondaryKeyValue * dropMultiplier;
			float num2 = speed * Mathf.Clamp(buoyancy, 1f, 100f);
			balloonPressure += num * keyInputSpeed * num2 * delta * 60f;
			float num3 = (base.transform.rotation * centerOffset).y + base.transform.position.y;
			float num4 = balloonPressure - num3;
			if (num < 0f && num4 > maxBalloonPressure)
			{
				balloonPressure = num3 + maxBalloonPressure;
			}
			if (num > 0f && num4 < 0f - maxBalloonPressure)
			{
				balloonPressure = num3 - maxBalloonPressure;
			}
		}
	}

	protected void HandleRigidbody()
	{
		if (!StatMaster.startingMachines)
		{
			float timeScale = Time.timeScale;
			if (!noRigidbody && lastTimeScale != timeScale)
			{
				if (timeScale <= 0f)
				{
					Rigidbody.interpolation = RigidbodyInterpolation.None;
				}
				else
				{
					Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
				}
			}
			lastTimeScale = timeScale;
		}
		if (!noRigidbody && Rigidbody.isKinematic)
		{
			wasKinematic = true;
		}
		else if (wasKinematic)
		{
			wasKinematic = false;
			StartCoroutine(TempInvulnerability());
		}
	}

	protected void DisplayBurn(float pct)
	{
		if (pct > 0f)
		{
			VisualController.AssignMaterialProperty("_SlipColor", Color.Lerp(clothColor, Color.black, pct * 1.1f), false);
			VisualController.AssignMaterialProperty("_dotEXP", Mathf.Lerp(1f, 3f, pct * 2f), false);
		}
	}

	protected void DisplayFire(float primaryKeyValue, float secondaryKeyValue)
	{
		randomLightValue = Mathf.Clamp(UnityEngine.Random.Range(0f, lightFlickerAmount) * lightFlickerSpeed + randomLightValue * (1f - lightFlickerSpeed), 0f, lightFlickerAmount);
		if (primaryKeyValue == 0f)
		{
			primaryKeyValue = -1f;
		}
		if (secondaryKeyValue == 0f)
		{
			secondaryKeyValue = -1f;
		}
		increaseIntensity += primaryKeyValue * Time.deltaTime * keyLightSpeed;
		decreaseIntensity += secondaryKeyValue * Time.deltaTime * keyLightSpeed;
		increaseIntensity = Mathf.Clamp01(increaseIntensity);
		decreaseIntensity = Mathf.Clamp01(decreaseIntensity);
		float num = 1.1f * increaseIntensity - decreaseIntensity;
		float burnPct = VisualController.BurnPct;
		float num2 = currentLightLevel + burnPct * burnPct * 2f;
		VisualController.AssignMaterialProperty("_Emission", Mathf.Clamp01(num2 + randomLightValue * Mathf.Clamp01(num2 * 4f + 0.1f) + num * lightInputScale * Mathf.Clamp01(buoyancy)) * (1f - submerge), false);
	}

	protected void DisplayPressure(float value = -1f)
	{
		VisualController.AssignMaterialProperty("_Cutoff", ((!(value > -1E-45f)) ? Mathf.Clamp01(0.9f + pressureDifference * 0.6f) : value) * (1f - submerge));
	}

	private IEnumerator TempInvulnerability()
	{
		float oldTreshold = popImpactThreshold;
		popImpactThreshold = float.PositiveInfinity;
		yield return null;
		popImpactThreshold = oldTreshold;
	}

	public override void FixedUpdateBlock()
	{
		if (noRigidbody)
		{
			_parentMachine.UnregisterFixedUpdate(this, false);
		}
		else
		{
			if (StatMaster.SimulationStartInProgress || !isSimulating || soaked)
			{
				return;
			}
			if (!StatMaster.GodTools.GravityDisabled)
			{
				PressureBasedCalc();
				return;
			}
			pressureDifference -= Time.fixedDeltaTime;
			if (pressureDifference < -0.1f)
			{
				pressureDifference = -0.1f;
			}
		}
	}

	private void PressureBasedCalc()
	{
		Vector3 up = Vector3.up;
		float num = speed * Mathf.Clamp(buoyancy, 1f, 100f);
		float num2 = ((!clientLocalSim) ? Rigidbody.velocity.y : ((base.transform.position.y - prevYpos) / Time.fixedDeltaTime));
		prevYpos = base.transform.position.y;
		float num3 = 1f - Mathf.Clamp01(num2 / (velLimit * num));
		currentPressureLayer = (base.transform.rotation * centerOffset).y + base.transform.position.y;
		pressureDifference = balloonPressure - currentPressureLayer;
		float num4 = 1f;
		if (pressureDifference < 0f)
		{
			num4 = -1f;
		}
		float p = pressureExponent * Mathf.Clamp((prevYpos - 3f) * 0.1f, 0.2f, 1f);
		pressureDifference = Mathf.Pow(Mathf.Abs(pressureDifference), p) * num4;
		if (randomPressureRange > 0f)
		{
			if (randomTime > 1f)
			{
				prevRandomRange = currentRandom;
				currentRandom = UnityEngine.Random.Range(0f - randomPressureRange, randomPressureRange);
				randomTime = 0f;
			}
			float num5 = Mathf.Lerp(prevRandomRange, currentRandom, randomTime);
			randomTime += Time.fixedDeltaTime * UnityEngine.Random.Range(0.75f, 2f);
			pressureDifference += num5 * Time.fixedDeltaTime;
		}
		float num6 = Mathf.Clamp(pressureDifference, 0f, maxBalloonPressure);
		up.y = num6 * baseForce * (0.8f + buoyancy * 0.2f) * num3;
		currentLightLevel = Mathf.Clamp01(up.y / 5f) * baseLightLevel * 0.05f + currentLightLevel * 0.95f;
		float num7 = 1f - Mathf.Clamp01(Mathf.Abs(pressureDifference) * 0.25f);
		float num8 = ((!clientLocalSim) ? Rigidbody.mass : 0.5f);
		liftVector = num8 * -Physics.gravity * num7 + up;
		float num9 = buoyancy * maxForce;
		if (liftVector.y > num9)
		{
			liftVector.y = num9;
		}
		liftVector.y *= Decrease();
		if (!clientLocalSim)
		{
			AddForce();
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(1f, 0.75f, 0f);
		Gizmos.DrawRay(Rigidbody.worldCenterOfMass, liftVector / 10f);
		Gizmos.color = new Color(1f, 0.5f, 0f);
		Gizmos.DrawWireSphere(new Vector3(Rigidbody.worldCenterOfMass.x, currentPressureLayer, Rigidbody.worldCenterOfMass.z), 0.1f);
		Gizmos.color = new Color(0f, 0.5f, 1f);
		Gizmos.DrawWireSphere(new Vector3(Rigidbody.worldCenterOfMass.x, balloonPressure, Rigidbody.worldCenterOfMass.z), 0.1f);
	}

	private float Decrease()
	{
		if (secondaryKeyValue + secondaryEmuValue > 0f)
		{
			if (expTime < 1f)
			{
				expTime += Time.fixedDeltaTime * 0.75f;
			}
			else
			{
				expTime = 1f;
			}
		}
		else if (expTime > 0f)
		{
			expTime -= Time.fixedDeltaTime;
		}
		else
		{
			expTime = 0f;
		}
		return Mathf.Lerp(1f, 0.9f, expTime);
	}

	private void AddForce()
	{
		if (StatMaster.startingMachines || !SimPhysics)
		{
			return;
		}
		float num = 1f - Mathf.Pow(submergedPercent * 2f, 2f);
		if (liftVector.y > 0f)
		{
			if (gotChildBlocks)
			{
				Rigidbody.AddForceAtPosition(liftVector * num, base.transform.rotation * centerOffset + base.transform.position);
			}
			else
			{
				Rigidbody.AddForce(liftVector * num);
			}
		}
		Vector3 velocity = Rigidbody.velocity;
		if (velocity.y < 0f)
		{
			velocity.x = (velocity.z = 0f);
			velocity.y *= Mathf.Pow(Mathf.Abs(velocity.y), 5f) * customDragMultiplier * Time.fixedDeltaTime * 100f;
			velocity.y = Mathf.Clamp(velocity.y, -100f, 0f) * Mathf.Clamp01(0.5f + expTime);
			Rigidbody.AddForce(-velocity * num, ForceMode.Acceleration);
		}
	}

	public void Pop()
	{
		if (popped || !base.HasParentMachine || _parentMachine.UnbreakableMode)
		{
			return;
		}
		balloonPopObj.transform.position = base.transform.position + base.transform.rotation * centerOffset;
		balloonPopObj.transform.rotation = balloonVisObj.rotation;
		balloonPopObj.gameObject.SetActive(true);
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
		if (igniteOnBreak)
		{
			balloonPopIgnite.transform.position = balloonPopObj.transform.position;
			balloonPopIgnite.SetActive(true);
		}
		popped = true;
		IsDestroyed = true;
		if (SimPhysics)
		{
			CreateSimLists();
			foreach (Joint item in jointsToMe)
			{
				if ((bool)item)
				{
					float breakForce = (item.breakTorque = 0f);
					item.breakForce = breakForce;
				}
			}
			foreach (Joint item2 in iJointTo)
			{
				if ((bool)item2)
				{
					float breakForce = (item2.breakTorque = 0f);
					item2.breakForce = breakForce;
				}
			}
			jointsToMe.Clear();
			iJointTo.Clear();
			Rigidbody.WakeUp();
		}
		base.gameObject.SetActive(false);
		if (SimPhysics)
		{
			_parentMachine.UnregisterUpdate(this, false);
			_parentMachine.UnregisterFixedUpdate(this, false);
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (wasKinematic || !SimPhysics || !isSimulating || !base.HasParentMachine || _parentMachine.UnbreakableMode)
		{
			return;
		}
		if (other.relativeVelocity.sqrMagnitude > popImpactThreshold)
		{
			Pop();
		}
		else if ((bool)other.collider.attachedRigidbody)
		{
			BlockBehaviour componentInParent = other.collider.GetComponentInParent<BlockBehaviour>();
			if (!object.ReferenceEquals(componentInParent, null) && componentInParent.Prefab.hasDamageType && componentInParent.Prefab.myDamageType == DamageType.Sharp)
			{
				Pop();
			}
		}
	}

	private void OnJointBreak()
	{
		if (SimPhysics && isSimulating)
		{
			FragmentVisualController.EmitJointBreakMarker(base.transform.position);
			Pop();
		}
	}

	public void FireKill()
	{
		if (SimPhysics && isSimulating)
		{
			Pop();
		}
	}

	public override void FreezeMe()
	{
		base.FreezeMe();
		if (SimPhysics && isSimulating)
		{
			Pop();
		}
	}

	protected void ToggleVisualisation(bool auto)
	{
		bool active = !auto && !isSimulating;
		line.gameObject.SetActive(active);
		bar.gameObject.SetActive(active);
	}

	protected void UpdateVisualisation()
	{
		float value = fixedHeight.Value;
		Vector3 vector = Vector3.Scale(centerOffset, base.transform.localScale);
		Vector3 pos = base.transform.position + base.transform.rotation * vector;
		Vector3 vector2 = base.transform.position + base.transform.rotation * vector;
		vector2.y = value;
		bar.position = vector2;
		SetDirectionalLine(line, pos, vector2);
	}

	private void SetDirectionalLine(LineRenderer ren, Vector3 pos1, Vector3 pos2)
	{
		ren.SetPosition(0, pos1);
		ren.SetPosition(1, pos2);
		ren.material.mainTextureScale = new Vector2(Vector3.Distance(pos1, pos2), 1f);
	}

	private void SetSlip(Color value)
	{
		float S;
		float V;
		float H;
		Color.RGBToHSV(value, out H, out S, out V);
		float H2;
		Color.RGBToHSV(defaultColor, out H2, out S, out V);
		H = hueToHue.Evaluate(H);
		clothColor = Color.HSVToRGB(H, Mathf.Clamp01(S + hueToSaturation.Evaluate(H)), Mathf.Clamp01(V + hueToLuminosity.Evaluate(H)));
		VisualController.AssignMaterialProperty("_SlipColor", clothColor);
	}
}
