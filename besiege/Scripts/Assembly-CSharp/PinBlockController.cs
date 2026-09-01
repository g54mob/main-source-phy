using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/PinBlockController")]
public class PinBlockController : BlockBehaviour
{
	public static bool ShowPinAll;

	public PinBlockKinematic pin;

	private MKey unpinKey;

	private MToggle hideToggle;

	private MToggle pinAllHit;

	public MToggle HideToggle
	{
		get
		{
			return hideToggle;
		}
	}

	public MToggle PinAllHit
	{
		get
		{
			return pinAllHit;
		}
	}

	public MKey UnpinKey
	{
		get
		{
			return unpinKey;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		unpinKey = AddKey(2498, "unpin", ControlScheme.BlockControls.Pin, 0, KeyCode.P);
		hideToggle = AddToggle(2499, "hide-visual", false);
		pinAllHit = AddToggle(4972, "pin-all-hit", false);
		pinAllHit.DisplayInMapper = ShowPinAll;
		hideToggle.Toggled += OnHideToggled;
		pinAllHit.Toggled += OnPinAllToggled;
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (unpinKey.IsPressed)
		{
			OnUnpinPressed();
		}
	}

	public override void EmulationUpdateBlock()
	{
		if (unpinKey.EmulationPressed())
		{
			OnUnpinPressed();
		}
	}

	private void OnUnpinPressed()
	{
		if (SimPhysics)
		{
			pin.Release();
		}
		else
		{
			RemoveSimBlock(false);
		}
	}

	private void OnHideToggled(bool hide)
	{
		pin.hideVisuals = hide;
	}

	private void OnPinAllToggled(bool all)
	{
		pin.allowMultiPin = all;
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (unpinKey.KeysCount == 0 || (unpinKey.KeysCount == 1 && unpinKey.GetKey(0) == KeyCode.None && !unpinKey.useMessage))
		{
			base.ParentMachine.UnregisterUpdate(this, false);
			base.ParentMachine.UnregisterEmulationUpdate(this);
		}
	}
}
