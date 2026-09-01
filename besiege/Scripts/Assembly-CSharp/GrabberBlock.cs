using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/GrabberBlock")]
public class GrabberBlock : BlockBehaviour
{
	public JoinOnTriggerBlock joinOnTriggerBlock;

	private MKey detachKey;

	private MKey attachKey;

	private MToggle canGrabStaticToggle;

	private MToggle flexibleToggle;

	private MToggle grabStaticOnly;

	private MToggle autoGrabToggle;

	private bool attachDetachEqual = true;

	public MToggle CanGrabStaticToggle
	{
		get
		{
			return canGrabStaticToggle;
		}
	}

	public MToggle GrabStaticOnlyToggle
	{
		get
		{
			return grabStaticOnly;
		}
	}

	public MToggle AutoGrabToggle
	{
		get
		{
			return autoGrabToggle;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (!isSimulating || SimPhysics)
		{
			attachKey = AddKey(2840, "attach", ControlScheme.BlockControls.Detach, 0, KeyCode.V);
			detachKey = AddKey(2494, "detach", ControlScheme.BlockControls.Detach, 0, KeyCode.V);
			attachKey.DisplayInMapper = false;
			canGrabStaticToggle = AddToggle(2495, "grab-static", false);
			grabStaticOnly = AddToggle(2496, "grab-static-only", false);
			autoGrabToggle = AddToggle(2497, "auto-grab", true);
			flexibleToggle = AddToggle(4422, "flexible", false);
			canGrabStaticToggle.Toggled += OnToggleGrabStatic;
			grabStaticOnly.Toggled += OnToggleStaticOnly;
			autoGrabToggle.Toggled += OnAutoGrabToggled;
			flexibleToggle.Toggled += OnToggleFlexible;
			grabStaticOnly.DisplayInMapper = false;
		}
	}

	protected override void Start()
	{
		base.Start();
		if (isSimulating)
		{
			joinOnTriggerBlock.SetupMixers();
			joinOnTriggerBlock.SetMixer(base.InWater);
			if (SimPhysics)
			{
				joinOnTriggerBlock.SetCanGrabStatic(canGrabStaticToggle.IsActive, grabStaticOnly.IsActive);
			}
		}
	}

	public override void StartPhysics(bool isKinematic)
	{
		base.StartPhysics(isKinematic);
		attachDetachEqual = attachKey.Compare(detachKey);
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates((!isSimulating) ? Prefab.RegisterSimUpdate : SimPhysics, SimPhysics && ((!isSimulating) ? Prefab.RegisterBuildFixedUpdate : Prefab.RegisterSimFixedUpdate), Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	private void OnToggleStaticOnly(bool isActive)
	{
		joinOnTriggerBlock.SetCanGrabStatic(canGrabStaticToggle.IsActive, isActive);
	}

	private void OnToggleGrabStatic(bool isActive)
	{
		grabStaticOnly.DisplayInMapper = isActive;
		joinOnTriggerBlock.SetCanGrabStatic(isActive, isActive && grabStaticOnly.IsActive);
	}

	private void OnToggleFlexible(bool isActive)
	{
		joinOnTriggerBlock.SetFlexible(isActive);
	}

	private void OnAutoGrabToggled(bool active)
	{
		attachKey.DisplayInMapper = !active;
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (isSimulating && !SimPhysics)
		{
			return;
		}
		float getSubmergedPctMV = base.GetSubmergedPctMV;
		if (autoGrabToggle.IsActive)
		{
			if (detachKey.IsPressed)
			{
				joinOnTriggerBlock.SetMixer(getSubmergedPctMV > 0.6f);
				joinOnTriggerBlock.OnKeyPressed();
			}
			return;
		}
		if (attachKey.IsPressed)
		{
			joinOnTriggerBlock.SetMixer(getSubmergedPctMV > 0.6f);
			joinOnTriggerBlock.OnKeyGrab();
		}
		if (detachKey.IsPressed)
		{
			joinOnTriggerBlock.SetMixer(getSubmergedPctMV > 0.6f);
			joinOnTriggerBlock.OnKeyRelease();
			if (!attachDetachEqual)
			{
				joinOnTriggerBlock.StopGrab();
			}
		}
	}

	public override void FixedUpdateBlock()
	{
		if (autoGrabToggle.IsActive && !joinOnTriggerBlock.isStarting)
		{
			joinOnTriggerBlock.allowGrabTimer = 1f;
		}
		joinOnTriggerBlock.FixedUpdateBlock();
	}

	public override void EmulationUpdateBlock()
	{
		float getSubmergedPctMV = base.GetSubmergedPctMV;
		if (autoGrabToggle.IsActive)
		{
			if (detachKey.EmulationPressed())
			{
				joinOnTriggerBlock.SetMixer(getSubmergedPctMV > 0.6f);
				joinOnTriggerBlock.OnKeyPressed();
			}
			return;
		}
		if (attachKey.EmulationPressed())
		{
			joinOnTriggerBlock.SetMixer(getSubmergedPctMV > 0.6f);
			joinOnTriggerBlock.OnKeyGrab();
		}
		if (detachKey.EmulationPressed())
		{
			joinOnTriggerBlock.SetMixer(getSubmergedPctMV > 0.6f);
			joinOnTriggerBlock.OnKeyRelease();
			if (!attachDetachEqual)
			{
				joinOnTriggerBlock.StopGrab();
			}
		}
	}

	public void OnJointBreak()
	{
		if (SimPhysics)
		{
			joinOnTriggerBlock.BlockJointBreak();
			FragmentVisualController.EmitJointBreakMarker(base.transform.position);
		}
	}

	public override int CheckJoints()
	{
		ConfigurableJoint[] components = base.gameObject.GetComponents<ConfigurableJoint>();
		int num = components.Length;
		for (int i = 0; i < components.Length; i++)
		{
			if (components[i].connectedBody == null && joinOnTriggerBlock.currentJoint != components[i])
			{
				Object.Destroy(components[i]);
				num--;
			}
		}
		return num;
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (!isSimulating && data.WasLoadedFromFile && !data.HasKey("bmt-attach"))
		{
			attachKey.MatchKeys(detachKey);
			attachKey.ApplyValue();
		}
	}
}
