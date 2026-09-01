using System.Collections.Generic;
using UnityEngine;

public class AnimationEventRelay : MonoBehaviour
{
	public ArtilleryReloadController artilleryReloadController;

	public void RelayAdvanceState()
	{
		//IL_0091: Expected O, but got I4
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected I4, but got Unknown
		if (!(this.artilleryReloadController != null))
		{
			return;
		}
		ArtilleryReloadController artilleryReloadController = this.artilleryReloadController;
		if (artilleryReloadController.reloadStates != null)
		{
			List<ReloadStateDef> reloadStates = artilleryReloadController.reloadStates;
			if (reloadStates._size != 0)
			{
				List<ReloadStateDef> reloadStates2 = artilleryReloadController.reloadStates;
				object obj = artilleryReloadController.currentStateIndex + 1;
				int newIndex = obj % reloadStates2._size;
				artilleryReloadController.SetState(newIndex);
			}
		}
	}

	public void RelayRegressState()
	{
		if (!(this.artilleryReloadController != null))
		{
			return;
		}
		ArtilleryReloadController artilleryReloadController = this.artilleryReloadController;
		if (artilleryReloadController.reloadStates == null)
		{
			return;
		}
		List<ReloadStateDef> reloadStates = artilleryReloadController.reloadStates;
		if (reloadStates._size != 0)
		{
			int newIndex = artilleryReloadController.currentStateIndex - 1;
			if (reloadStates._size < 0)
			{
				newIndex = reloadStates._size - 1;
			}
			artilleryReloadController.SetState(newIndex);
		}
	}

	public void RelayMoveShellToTransferSlot()
	{
		if (artilleryReloadController != null)
		{
			artilleryReloadController.AnimationEvent_MoveShellToTransferSlot();
		}
	}

	public void RelayTransferShellToChamber()
	{
		if (artilleryReloadController != null)
		{
			artilleryReloadController.AnimationEvent_TransferShellToChamber();
		}
	}
}
