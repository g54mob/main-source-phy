using System;
using Localisation;
using UnityEngine;
using UnityEngine.Audio;

[AddComponentMenu("Blocks/Block Behaviours/SliderCompress")]
public class SliderCompress : BlockBehaviour
{
	public ConfigurableJoint myJoint;

	protected bool hasJoint;

	public float startLimit;

	public float newLimit = 1.6f;

	public float posToBe = 1f;

	public float lerpSpeed = 20f;

	public Transform jointTrigger;

	public AudioSource sfx;

	public AudioClip[] clips;

	protected bool hasSfx;

	protected AudioMixerGroup mixer;

	protected AudioMixerGroup underwaterMixer;

	[SerializeField]
	protected Collider flipCollider;

	[NonSerialized]
	private bool _setup;

	private float orgStartLimit;

	private float orgNewLimit = 1f;

	private MKey extendKey;

	private MToggle toggleMode;

	private MSlider strengthSlider;

	private MSlider speedSlider;

	private LayerMask m;

	private LayerMask m2;

	private bool counterPositionPiston;

	private bool isLoadFlip;

	private bool initializedFlip;

	private Vector3 inertiaScaler = Vector3.one;

	private bool scaleInertia;

	private JointDrive originalSettings;

	private float jointBreak;

	private bool hasClusterNeightbour;

	private Transform clusterNeighbour;

	private Vector3 neighbourStartPos;

	private bool useNewForce = true;

	private bool extendPressed;

	private bool emuExtendPressed;

	private bool emuExtendReleased;

	private bool extendHeld;

	private bool emuExtendHeld;

	private bool extendReleased;

	private float lastPlayed;

	public MToggle ToggleModeToggle
	{
		get
		{
			return toggleMode;
		}
	}

	public MSlider StrengthSlider
	{
		get
		{
			return strengthSlider;
		}
	}

	public MSlider SpeedSlider
	{
		get
		{
			return speedSlider;
		}
	}

	public MKey ExtendKey
	{
		get
		{
			return extendKey;
		}
	}

	protected void StrengthChange(float val)
	{
		if (!SimPhysics || stripped || myJoint == null)
		{
			return;
		}
		JointDrive xDrive = myJoint.xDrive;
		float value = strengthSlider.Value;
		float value2 = value;
		value2 = Mathf.Clamp(value2, 1f, 3f);
		ConfigurableJoint configurableJoint = myJoint;
		float num = value2 * jointBreak;
		myJoint.breakTorque = num;
		configurableJoint.breakForce = num;
		value2 = value;
		if (useNewForce)
		{
			xDrive.positionDamper = originalSettings.positionDamper * value2;
			xDrive.positionSpring = originalSettings.positionSpring * Mathf.Pow(value2, 1.5f);
			if (!float.IsNaN(value2))
			{
				inertiaScaler = Vector3.one * Mathf.Clamp(value2, 1f, 4f);
			}
		}
		else
		{
			value2 *= value2;
			xDrive.positionSpring = originalSettings.positionSpring * value2;
			xDrive.positionDamper = originalSettings.positionDamper * value2;
		}
		myJoint.xDrive = xDrive;
	}

	protected override void Awake()
	{
		base.Awake();
		extendKey = AddKey(2472, "extend", ControlScheme.BlockControls.Piston, 0, KeyCode.H);
		toggleMode = AddToggle(2431, "toggle", false);
		speedSlider = AddSlider(2428, "speed", 1f, 0.1f, 2f, string.Empty);
		strengthSlider = AddSlider(2427, "push-power", 1f, 1f, 2f, string.Empty);
		if (isSimulating)
		{
			hasSfx = sfx != null;
			if (hasSfx)
			{
				mixer = sfx.outputAudioMixerGroup;
				underwaterMixer = ReferenceMaster.GetWaterMixerFrom(mixer);
			}
			if (!SimPhysics && base.HasParentMachine)
			{
				BlockLinkManager linkManager = base.ParentMachine.LinkManager;
				{
					foreach (BlockLink neighbour in linkManager.GetNeighbours(BuildingBlock.NodeIndex))
					{
						if (neighbour.isOwnLink)
						{
							hasClusterNeightbour = true;
							clusterNeighbour = neighbour.Other.Block.transform;
							neighbourStartPos = base.transform.InverseTransformPoint(clusterNeighbour.position);
						}
					}
					return;
				}
			}
		}
		initializedFlip = false;
		counterPositionPiston = false;
		if (!stripped && myJoint != null)
		{
			originalSettings = myJoint.xDrive;
			jointBreak = myJoint.breakForce;
		}
		strengthSlider.ValueChanged += StrengthChange;
		StrengthChange(strengthSlider.Value);
		m = AddPiece.CreateLayerMask(SingleInstanceFindOnly<AddPiece>.Instance.layerMasky, 2, 24, 29);
		m2 = AddPiece.CreateLayerMask(SingleInstanceFindOnly<AddPiece>.Instance.layerMasky, 24, 29);
	}

	public override void StartPhysics(bool isKinematic)
	{
		hasJoint = true;
		scaleInertia = true;
		if (!SimPhysics || myJoint == null)
		{
			hasJoint = false;
		}
		else if (myJoint.connectedBody == null)
		{
			UnityEngine.Object.Destroy(myJoint);
			hasJoint = false;
		}
	}

	private void OnJointBreak()
	{
		if (SimPhysics)
		{
			FragmentVisualController.EmitJointBreakMarker(base.transform.position);
			hasJoint = false;
			myJoint = null;
		}
	}

	protected bool InvalidJoint()
	{
		return !hasJoint || noRigidbody || (isKinematic && myJoint.connectedBody.gameObject.CompareTag("StayKinematic"));
	}

	protected override void Start()
	{
		base.Start();
		Setup();
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates(Prefab.RegisterSimUpdate, (!isSimulating) ? Prefab.RegisterSimFixedUpdate : SimPhysics, Prefab.RegisterSimLateUpdate, Prefab.RegisterEmulationUpdate);
	}

	private void Setup()
	{
		if (!_setup && (!isSimulating || SimPhysics) && !stripped)
		{
			if (!isSimulating)
			{
				orgStartLimit = myJoint.targetPosition.x;
				orgNewLimit = newLimit;
			}
			else
			{
				UnityEngine.Object.Destroy(flipCollider);
			}
			_setup = true;
		}
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (!SimPhysics || !InvalidJoint())
		{
			extendPressed = extendKey.IsPressed;
			extendHeld = extendKey.IsHeld;
			extendReleased = extendKey.IsReleased;
			CheckKeys(extendPressed, extendHeld || emuExtendHeld, extendReleased);
		}
	}

	public override void EmulationUpdateBlock()
	{
		emuExtendPressed = extendKey.EmulationPressed();
		emuExtendHeld = extendKey.EmulationHeld(true);
		emuExtendReleased = extendKey.EmulationReleased();
		CheckKeys(emuExtendPressed, emuExtendHeld || extendHeld, emuExtendReleased);
	}

	private void CheckKeys(bool pressed, bool held, bool released)
	{
		bool flag = ((!SimPhysics) ? (base.ClusterIndex >= 0) : hasJoint);
		if (toggleMode.IsActive)
		{
			if (pressed)
			{
				posToBe = ((posToBe != newLimit) ? newLimit : startLimit);
				if (hasSfx && flag)
				{
					PlaySound(posToBe == newLimit);
				}
			}
			return;
		}
		posToBe = ((!held) ? startLimit : newLimit);
		if (hasSfx && flag)
		{
			if (pressed)
			{
				PlaySound(true);
			}
			else if (released && !held)
			{
				PlaySound(false);
			}
		}
	}

	protected void PlaySound(bool extend)
	{
		if (base.GetSubmergedPctMV > 0.7f)
		{
			sfx.outputAudioMixerGroup = underwaterMixer;
		}
		else
		{
			sfx.outputAudioMixerGroup = mixer;
		}
		float num = (Flipped ? ((!extend) ? 1f : 0.9f) : ((!extend) ? 0.9f : 1f));
		float value = speedSlider.Value;
		value = ((!float.IsNaN(value)) ? Mathf.Clamp(value, 0.1f, 4f) : 0f);
		sfx.pitch = (Mathf.Pow(value, 0.25f) * 0.25f + 0.75f + UnityEngine.Random.Range(-0.01f, 0.01f)) * 0.5f * num;
		sfx.volume = Mathf.Clamp01(strengthSlider.Value) * 0.15f * num;
		if (sfx.isPlaying)
		{
			if (!extend && !toggleMode.IsActive && !(Time.fixedTime > lastPlayed + 0.1f))
			{
				return;
			}
			sfx.Stop();
		}
		float num2 = 0f;
		if (SimPhysics)
		{
			if (hasJoint)
			{
				num2 = myJoint.targetPosition.x / newLimit;
			}
		}
		else
		{
			if (!hasClusterNeightbour)
			{
				return;
			}
			Vector3 vector = neighbourStartPos - base.transform.InverseTransformPoint(clusterNeighbour.position);
			num2 = Mathf.Clamp01(Mathf.Abs(vector.z));
			vector.z = 0f;
			if (vector.sqrMagnitude > 1f)
			{
				return;
			}
		}
		if (!extend)
		{
			num2 = 0.9999f - num2;
		}
		if (num2 < 0.75f)
		{
			lastPlayed = Time.fixedTime;
			sfx.clip = clips[UnityEngine.Random.Range(0, clips.Length)];
			sfx.Play();
			sfx.timeSamples = UnityEngine.Random.Range(0, (int)((float)sfx.clip.samples * 0.01f));
		}
	}

	public override void FixedUpdateBlock()
	{
		if (InvalidJoint())
		{
			return;
		}
		if (scaleInertia)
		{
			Rigidbody.inertiaTensor = Vector3.Scale(Rigidbody.inertiaTensor, inertiaScaler);
			scaleInertia = false;
		}
		float x = myJoint.targetPosition.x;
		if (x != posToBe)
		{
			if (Rigidbody.IsSleeping())
			{
				Rigidbody.WakeUp();
			}
			if (myJoint.connectedBody.IsSleeping())
			{
				myJoint.connectedBody.WakeUp();
			}
			float num = speedSlider.Value;
			if (useNewForce)
			{
				num *= Mathf.Lerp(1f, 0.4f, strengthSlider.Value - 1f);
			}
			myJoint.targetPosition = new Vector3(Mathf.Lerp(x, posToBe, Time.fixedDeltaTime * lerpSpeed * num), myJoint.targetPosition.y, myJoint.targetPosition.z);
		}
	}

	private void SetDefaultExtension()
	{
		if (!noRigidbody)
		{
			Rigidbody.interpolation = RigidbodyInterpolation.None;
		}
		if (!isSimulating || SimPhysics)
		{
			extendKey.DisplayName = ((!Flipped) ? LocalisationManager.GetTranslation(2472) : LocalisationManager.GetTranslation(2477));
		}
		posToBe = (startLimit = ((!Flipped) ? orgStartLimit : (1f + orgNewLimit)));
		newLimit = ((!Flipped) ? orgNewLimit : 1f);
		if (!stripped)
		{
			myJoint.anchor = new Vector3(myJoint.anchor.x, myJoint.anchor.y, (!Flipped) ? 0f : (-1f));
			jointTrigger.localPosition = new Vector3(jointTrigger.localPosition.x, jointTrigger.localPosition.y, (!Flipped) ? 0f : (-1f));
		}
		if (counterPositionPiston)
		{
			base.transform.position += base.transform.forward * ((!Flipped) ? (-1f) : 1f);
			Position = base.transform.localPosition;
		}
		counterPositionPiston = true;
		if (!isSimulating)
		{
			flipCollider.enabled = Flipped;
		}
		if (!noRigidbody)
		{
			Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		}
	}

	private void OnDrawGizmos()
	{
		Vector3 vector = base.transform.position + base.transform.forward * ((!Flipped) ? 0.95f : 0f);
		Vector3 to = vector + base.transform.forward * ((!Flipped) ? 1f : (-1f)) * 0.95f;
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(vector, to);
		Gizmos.DrawWireSphere(vector, 0.1f);
	}

	public override bool OnFlip(bool sound, bool isUndo)
	{
		UpdateParentMachine();
		if (!base.HasParentMachine || (isSimulating && !SimPhysics))
		{
			return false;
		}
		Setup();
		Machine parentMachine = base.ParentMachine;
		if (isSimulating)
		{
			return true;
		}
		bool flag = parentMachine.isLocalMachine && StatMaster.Bounding.Enabled;
		bool flag2 = !StatMaster.Mode.allowIntersection && parentMachine.isLocalMachine && !isLoadFlip;
		bool flag3 = !StatMaster.Mode.allowIntersection && !isUndo && flag2;
		initializedFlip = true;
		if (!counterPositionPiston)
		{
			SetDefaultExtension();
			if (flag && flag2)
			{
				parentMachine.CheckBounds();
			}
			return true;
		}
		if (flag)
		{
			if (flag3)
			{
				RaycastHit[] array = Physics.RaycastAll(base.transform.position + base.transform.forward * ((!Flipped) ? 0f : 0.95f), base.transform.forward * ((!Flipped) ? (-1f) : 1f), 0.95f, m);
				for (int i = 0; i < array.Length; i++)
				{
					Collider collider = array[i].collider;
					if (collider.attachedRigidbody != Rigidbody && !collider.transform.root.name.Equals("_PERSISTENT"))
					{
						OnFlipIntersect();
						return false;
					}
				}
			}
			SetDefaultExtension();
			if (flag2)
			{
				parentMachine.CheckBounds();
			}
		}
		else
		{
			if (flag2)
			{
				RaycastHit[] array = Physics.RaycastAll(base.transform.position + base.transform.forward * ((!Flipped) ? 0f : 0.95f), base.transform.forward * ((!Flipped) ? (-1f) : 1f), 0.95f, m2);
				for (int i = 0; i < array.Length; i++)
				{
					Collider collider2 = array[i].collider;
					if (collider2.attachedRigidbody != Rigidbody && !collider2.transform.root.name.Equals("_PERSISTENT"))
					{
						OnFlipIntersect();
						return false;
					}
				}
			}
			SetDefaultExtension();
		}
		if (sound)
		{
			ReferenceMaster.PlayFlip();
		}
		return true;
	}

	private void OnFlipIntersect()
	{
		Flipped = !Flipped;
		IntersectWarning.Warning();
	}

	public override void OnSave(XDataHolder data)
	{
		base.OnSave(data);
		data.Write("preextended", Flipped);
		data.Write("stronger", useNewForce);
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (isSimulating && !SimPhysics)
		{
			return;
		}
		Setup();
		isLoadFlip = true;
		counterPositionPiston = false;
		if (data.WasLoadedFromFile)
		{
			string key = "stronger";
			if (data.HasKey(key))
			{
				useNewForce = data.ReadBool("stronger");
			}
			else
			{
				useNewForce = false;
			}
			key = "bmt-push-power";
			if (!data.HasKey(key))
			{
				strengthSlider.SetValue(1f);
				base.LastState.Write(key, 1f);
			}
		}
		if (data.HasKey("preextended"))
		{
			Flipped = data.ReadBool("preextended");
			PostFlip(false, false);
		}
		else if (!initializedFlip)
		{
			Flipped = false;
			PostFlip(false, false);
		}
		isLoadFlip = false;
	}
}
