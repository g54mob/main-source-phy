using UnityEngine;

public class DisableBoundsButton : ToggleGodModeButton
{
	public override string GetModeName()
	{
		return "DisableBounds";
	}

	public override bool IsRuleOn()
	{
		return !StatMaster.Bounding.Enabled;
	}

	public static void SetBounds(bool state)
	{
		if (StatMaster.levelSimulating)
		{
			return;
		}
		Machine machine = Machine.Active();
		if (machine == null || machine.isSimulating)
		{
			return;
		}
		BoundingBoxController boundingBoxController = machine.boundingBoxController;
		if (!(boundingBoxController == null))
		{
			if (StatMaster.isMP && PlayerData.hasLocalPlayer)
			{
				byte[] messageData = new byte[1] { (byte)(StatMaster.Bounding.Enabled ? 1u : 0u) };
				NetworkAuxAddPiece.Instance.SendNetworkMessage(RPCMessageType.ToggleBounds, messageData);
			}
			if (!state)
			{
				boundingBoxController.DisableBounds(machine);
			}
			else
			{
				boundingBoxController.EnableBounds(machine);
			}
			DisableBoundsButton[] array = Object.FindObjectsOfType<DisableBoundsButton>();
			foreach (DisableBoundsButton disableBoundsButton in array)
			{
				disableBoundsButton.UpdateVisual();
			}
		}
	}

	public override void ToggleRule(bool toggle)
	{
		if (!StatMaster.isMP && (StatMaster.levelSimulating || StatMaster.SimulationStartInProgress))
		{
			return;
		}
		Machine machine = Machine.Active();
		if (machine == null || !machine.isSimulating)
		{
			StatMaster.Bounding.Enabled = !toggle;
		}
		if (machine == null)
		{
			return;
		}
		BoundingBoxController boundingBoxController = machine.boundingBoxController;
		if (!(boundingBoxController == null))
		{
			if (StatMaster.isMP && PlayerData.hasLocalPlayer)
			{
				byte[] messageData = new byte[1] { (byte)(StatMaster.Bounding.Enabled ? 1u : 0u) };
				NetworkAuxAddPiece.Instance.SendNetworkMessage(RPCMessageType.ToggleBounds, messageData);
			}
			if (toggle)
			{
				boundingBoxController.DisableBounds(machine);
			}
			else
			{
				boundingBoxController.EnableBounds(machine);
			}
		}
	}

	public void TurnOffBounds()
	{
		if (StatMaster.Bounding.Enabled)
		{
			Set();
		}
	}
}
