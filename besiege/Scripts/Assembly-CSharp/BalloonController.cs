using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/BalloonController")]
public class BalloonController : BlockBehaviour
{
	public static Material ropeMaterial;

	public static Dictionary<BlockSkinLoader.SkinPack, BlockSkinLoader.SkinPack.Skin> PackRopeRef = new Dictionary<BlockSkinLoader.SkinPack, BlockSkinLoader.SkinPack.Skin>();

	public bool isFrozen;

	public float liftStrength = 40f;

	public float dragScaler = 1.5f;

	public float yVelocityCap = 20f;

	public Transform endPoint;

	public Transform cylinder;

	public Transform snapTrigger;

	public bool denestRope;

	public MeshRenderer ropeVis;

	public float ropeTiling = 1f;

	public float radius = 0.25f;

	public Vector3 centerOffset = new Vector3(0f, 0f, 1.05f);

	public AudioSource snapAudio;

	public ParticleSystem[] snappedDustParticles;

	public ConfigurableJoint myJoint;

	protected bool hasJoint;

	public float lerpInSpeed = 0.6f;

	public Transform balloonVisObj;

	public Transform balloonPopObj;

	public float popImpactThreshold = 2f;

	protected BlockSkinLoader.SkinPack.Skin ropeSkin;

	protected float lastTimeScale = 1f;

	private float lerpedSpeed;

	private bool wasKinematic;

	private bool snapped;

	private bool popped;

	private GameObject cylinderGO;

	private bool ropeEnabled = true;

	private Vector3 fixedVector = Vector3.zero;

	private float lastCylSqr;

	private Vector3 lastEnd;

	private Material ropeMat;

	private Vector3 cylScale;

	private Vector3 cylPos;

	private Vector2 texScale;

	[SerializeField]
	protected float pressureExponent = 2.5f;

	private float balloonPressure;

	private MSlider fixedHeight;

	private MSlider buoyancySlider;

	private MSlider stringLengthSlider;

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

	public MSlider BuoyancySlider
	{
		get
		{
			return buoyancySlider;
		}
	}

	public MSlider StringLengthSlider
	{
		get
		{
			return stringLengthSlider;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		cylScale = new Vector3(radius, 0f, radius);
		texScale = Vector2.one;
		cylPos = Vector3.zero;
		lastEnd = Vector3.zero;
		popped = false;
		cylinderGO = cylinder.gameObject;
		if (!isSimulating || SimPhysics)
		{
			buoyancySlider = AddSlider(4613, "buoyancy", 1f, 0.2f, 1.5f, string.Empty);
		}
		fixedHeight = AddSliderUnclamped(3783, "height", float.PositiveInfinity, 0f, 250f, string.Empty);
		fixedHeight.logScaling = true;
		fixedHeight.ValueChanged += SetFixedHeight;
		fixedHeight.maxInfinity = true;
		SetFixedHeight(fixedHeight.Value);
		if (ropeVis != null)
		{
			if (ropeMaterial == null)
			{
				ropeMaterial = ropeVis.material;
			}
			if (isSimulating)
			{
				VisualController.SetToNormal += NormalRope;
				VisualController.SetToCluster += ClusterRope;
				if (SimPhysics && denestRope)
				{
					snapTrigger.SetParent(base.transform.parent, true);
				}
			}
		}
		stringLengthSlider = AddSlider(2467, "string-length", 2f, 0f, 6f, string.Empty);
		SetStringLength(stringLengthSlider.Value);
		stringLengthSlider.ValueChanged += SetStringLength;
	}

	private void SetFixedHeight(float newStartPressure)
	{
		balloonPressure = newStartPressure;
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

	private void ClusterRope()
	{
		if (!popped && !IsDestroyed && ropeEnabled)
		{
			if (PackRopeRef.Count > 0)
			{
				PackRopeRef.Clear();
			}
			if (ropeVis != null)
			{
				ropeVis.sharedMaterial = VisualController.GetClusterMaterial();
				ropeMat = ropeVis.material;
			}
		}
	}

	private void NormalRope()
	{
		if (!popped && !IsDestroyed && ropeEnabled)
		{
			if (!PackRopeRef.ContainsKey(VisualController.selectedSkin.pack))
			{
				PackRopeRef.Add(VisualController.selectedSkin.pack, PrefabMaster.BlockPrefabs[45].VisualController.SafeGetVisualOptionFor(VisualController.selectedSkin.pack));
			}
			if (ropeSkin != null)
			{
				ropeSkin.Unregister(VisualController);
			}
			ropeSkin = PackRopeRef[VisualController.selectedSkin.pack].Register(VisualController);
			if (ropeSkin != null && ropeEnabled)
			{
				StartCoroutine(IENormalRope());
			}
		}
	}

	private IEnumerator IENormalRope()
	{
		while (!ropeSkin.doneLoading)
		{
			yield return null;
		}
		if (ropeVis != null)
		{
			ropeVis.material = ropeSkin.material;
			ropeMat = ropeVis.material;
			Vector3 startPoint = base.transform.TransformPoint(Prefab.rayPosition);
			ropeMat.mainTextureScale = new Vector2(ropeTiling * (startPoint - endPoint.position).magnitude, 1f);
		}
	}

	private void SetStringLength(float length)
	{
		UpdateParentMachine();
		if (base.HasParentMachine && isSimulating)
		{
			ropeEnabled = length > 0f;
			if (!ropeEnabled)
			{
				cylinder.SetParent(ReferenceMaster.physicsGoalInstance);
				fireTag.fireControllerCode.additionalFireParticles = new ParticleSystem[0];
				VisualController.renderers = new MeshRenderer[1] { VisualController.renderers[0] };
				_parentMachine.UnregisterLateUpdate(this, false);
			}
			else
			{
				_parentMachine.RegisterLateUpdate(this, false);
			}
			if (SimPhysics)
			{
				SoftJointLimit linearLimit = myJoint.linearLimit;
				linearLimit.limit = length;
				myJoint.linearLimit = linearLimit;
			}
		}
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates((!isSimulating) ? Prefab.RegisterSimUpdate : SimPhysics, (!isSimulating) ? Prefab.RegisterSimFixedUpdate : SimPhysics, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	protected override void Start()
	{
		base.Start();
		if (ropeVis != null)
		{
			Vector3 vector = base.transform.TransformPoint(Prefab.rayPosition);
			ropeMat = ropeVis.material;
			ropeMat.mainTextureScale = new Vector2(ropeTiling * (vector - endPoint.position).magnitude, 1f);
		}
		if (!isSimulating)
		{
			if (PackRopeRef.Count > 0)
			{
				PackRopeRef.Clear();
			}
		}
		else if (SimPhysics)
		{
			lerpInSpeed += UnityEngine.Random.Range(0f, 0.1f);
			StartCoroutine(LerpPowerIn());
		}
	}

	public override void StartPhysics(bool isKinematic)
	{
		hasJoint = true;
		if (!SimPhysics || myJoint == null)
		{
			hasJoint = false;
		}
		else if (myJoint.connectedBody == null)
		{
			UnityEngine.Object.Destroy(myJoint);
			hasJoint = false;
		}
		else
		{
			endPoint.parent = myJoint.connectedBody.transform;
		}
	}

	private void OnJointBreak()
	{
		if (SimPhysics && isSimulating)
		{
			FragmentVisualController.EmitJointBreakMarker(base.transform.position);
			hasJoint = false;
			myJoint = null;
			Pop();
		}
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (!isSimulating)
		{
			if (BlockMapper.IsOpen && BlockMapper.CurrentInstance.Current == this && !float.IsInfinity(fixedHeight.Value))
			{
				if (!line.gameObject.activeSelf)
				{
					ToggleVisualisation(true);
				}
				UpdateVisualisation();
			}
			else if (line.gameObject.activeSelf)
			{
				ToggleVisualisation(false);
			}
		}
		else
		{
			if (!SimPhysics)
			{
				return;
			}
			if (base.ParentMachine.isReady && !StatMaster.GodTools.UnbreakableMode && !hasJoint)
			{
				Snap();
			}
			float num = 5f;
			float num2 = (base.transform.rotation * centerOffset).y + base.transform.position.y;
			float num3 = balloonPressure + 1f - num2;
			num3 = Mathf.Pow(Mathf.Abs(Mathf.Clamp01(num3 / num)), pressureExponent);
			fixedVector.y = lerpedSpeed * buoyancySlider.Value * num3 * (1f - base.GetSubmergedPctMV * 0.25f);
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
			return;
		}
		if (!StatMaster.startingMachines && !StatMaster.GodTools.GravityDisabled)
		{
			Rigidbody.AddForce(fixedVector);
		}
		if (denestRope)
		{
			CreateCylinderBetweenPoints(snapTrigger, Prefab.rayPosition, base.transform.InverseTransformPoint(endPoint.position));
		}
	}

	public override void LateUpdateBlock()
	{
		if (!SimPhysics || hasJoint)
		{
			CreateCylinderBetweenPoints(cylinder, Prefab.rayPosition, base.transform.InverseTransformPoint(endPoint.position));
		}
	}

	private IEnumerator LerpPowerIn()
	{
		float cTime = 0f;
		float rate = 1f / lerpInSpeed;
		while (cTime < 1f)
		{
			cTime += Time.deltaTime * rate;
			lerpedSpeed = Mathf.Lerp(0f, liftStrength, cTime);
			yield return null;
		}
	}

	public void Pop()
	{
		if (popped || !base.HasParentMachine || _parentMachine.UnbreakableMode)
		{
			return;
		}
		UnityEngine.Object.Instantiate(balloonPopObj, balloonVisObj.position, balloonVisObj.rotation);
		if (!StatMaster.isMP)
		{
			base.transform.parent = ReferenceMaster.physicsGoalInstance;
		}
		else if (SimPhysics)
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
		popped = true;
		IsDestroyed = true;
		base.gameObject.SetActive(false);
		if (stringLengthSlider.Value == 0f)
		{
			cylinderGO.SetActive(false);
		}
		_parentMachine.UnregisterLateUpdate(this, false);
		if (SimPhysics)
		{
			_parentMachine.UnregisterUpdate(this, false);
			_parentMachine.UnregisterFixedUpdate(this, false);
		}
	}

	private void CreateCylinderBetweenPoints(Transform cylinder, Vector3 localStart, Vector3 localEnd)
	{
		if (!ropeEnabled)
		{
			return;
		}
		float num = localEnd.x - localStart.x;
		float num2 = localEnd.y - localStart.y;
		float num3 = localEnd.z - localStart.z;
		float num4 = num * num + num2 * num2 + num3 * num3;
		bool flag = cylinderGO.activeSelf;
		bool flag2 = false;
		if (num4 > 0.0025f)
		{
			if (!flag)
			{
				cylinderGO.SetActive(true);
				flag = true;
			}
			float num5 = 0.05f;
			float num6 = num4 - lastCylSqr;
			if (((!(num6 < 0f)) ? num6 : (0f - num6)) > num5)
			{
				float num7 = Mathf.Sqrt(num4);
				cylScale.y = num7 * 0.5f;
				cylinder.localScale = cylScale;
				if (!object.ReferenceEquals(ropeVis, null))
				{
					texScale.x = ropeTiling * num7;
					ropeMat.mainTextureScale = texScale;
				}
				lastCylSqr = num4;
				flag2 = true;
			}
		}
		else if (flag)
		{
			cylinderGO.SetActive(false);
			flag = false;
		}
		if (flag)
		{
			bool flag3;
			if (flag2)
			{
				flag3 = true;
			}
			else
			{
				float num8 = localEnd.x - lastEnd.x;
				float num9 = localEnd.y - lastEnd.y;
				float num10 = localEnd.z - lastEnd.z;
				float num11 = 0.01f;
				flag3 = ((!(num8 < 0f)) ? num8 : (0f - num8)) > num11 || ((!(num9 < 0f)) ? num9 : (0f - num9)) > num11 || ((!(num10 < 0f)) ? num10 : (0f - num10)) > num11;
			}
			if (flag3)
			{
				cylPos.Set(localStart.x + num * 0.5f, localStart.y + num2 * 0.5f, localStart.z + num3 * 0.5f);
				cylinder.localPosition = cylPos;
				cylinder.up = base.transform.TransformDirection(num, num2, num3);
				lastEnd.Set(localEnd.x, localEnd.y, localEnd.z);
			}
		}
	}

	public void Snap()
	{
		if (snapped || !isSimulating || StatMaster.GodTools.UnbreakableMode || !ropeEnabled)
		{
			return;
		}
		if (SimPhysics)
		{
			if (StatMaster.isMP)
			{
				if (NetBlock != null)
				{
					NetBlock.Event(NetworkEntity.EntityEvent.VisBreak);
				}
				else
				{
					Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
				}
			}
			cylinder.gameObject.SetActive(false);
		}
		snapped = true;
		snapAudio.pitch += UnityEngine.Random.Range(-0.1f, 0.2f);
		snapAudio.Play();
		if (snappedDustParticles.Length == 2)
		{
			Vector3 position = base.transform.TransformPoint(Prefab.rayPosition);
			snappedDustParticles[0].transform.position = position;
			snappedDustParticles[1].transform.position = endPoint.position;
			snappedDustParticles[0].Play();
			snappedDustParticles[1].Play();
		}
		ropeEnabled = false;
		VisualController.renderers = new MeshRenderer[1] { VisualController.renderers[0] };
		if (hasJoint)
		{
			UnityEngine.Object.Destroy(myJoint);
			myJoint = null;
			hasJoint = false;
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
			isFrozen = true;
			Pop();
		}
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (!isSimulating && data.WasLoadedFromFile && !data.HasKey("bmt-height"))
		{
			fixedHeight.SetValue(float.PositiveInfinity);
			fixedHeight.ApplyValue();
		}
	}

	protected void ToggleVisualisation(bool toggle)
	{
		toggle = toggle && !isSimulating;
		line.gameObject.SetActive(toggle);
		bar.gameObject.SetActive(toggle);
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
}
