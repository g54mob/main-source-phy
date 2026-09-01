using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkinFluentJS : MonoBehaviour
{
	public float delay = 0.1f;

	public float groupingAccuracy = 10f;

	private GameObject pickedBlock;

	private Vector3 sortPoint = new Vector3(0f, 0f, 100f);

	private void Update()
	{
		if (!StatMaster.inMenu && SingleInstanceFindOnly<AddPiece>.Instance != null && Input.GetKey(KeyCode.RightControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.V))
		{
			if (!OptionsMaster.skinsEnabled)
			{
				StartCoroutine(changeSkin());
			}
			else
			{
				SingleInstance<BlockSkinLoader>.Instance.UseAnimation = false;
				OptionsMaster.skinsEnabled = false;
			}
		}
		if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.LeftShift))
		{
			if (Input.GetKey(KeyCode.Mouse2))
			{
				PickBlock();
			}
			if (Input.GetKey(KeyCode.C))
			{
				pickedBlock = null;
			}
		}
	}

	private void PickBlock()
	{
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(InputManager.CursorPosition().x, InputManager.CursorPosition().y, 0f));
		bool flag = false;
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo, 300f, SingleInstanceFindOnly<AddPiece>.Instance.layerMasky) && (bool)hitInfo.rigidbody)
		{
			pickedBlock = hitInfo.transform.gameObject;
		}
	}

	private float GetDistance(Vector3 pos)
	{
		pos.y = sortPoint.y;
		return (pos - sortPoint).sqrMagnitude / groupingAccuracy;
	}

	private float GetDistance(BlockBehaviour b)
	{
		if (b.IsDestroyed)
		{
			return 1000f;
		}
		return GetDistance(b.UpdatedBounds.center);
	}

	private IEnumerator changeSkin()
	{
		List<BlockBehaviour> sortedList = ((!StatMaster.levelSimulating) ? ReferenceMaster.GetAllBuildingBlocks() : ReferenceMaster.GetAllSimulationBlocks());
		List<BlockBehaviour> buildingList = ((!StatMaster.levelSimulating) ? null : ReferenceMaster.GetAllBuildingBlocks());
		SingleInstance<BlockSkinLoader>.Instance.UseAnimation = true;
		OptionsMaster.skinsEnabled = true;
		BlockBehaviour firstBlock = sortedList[0];
		if (firstBlock.HasParentMachine)
		{
			ServerMachine machi = firstBlock.ParentMachine as ServerMachine;
			if (pickedBlock == null)
			{
				sortPoint = machi.MachineMovementDirection * 100f + machi.MachineCenterPos;
			}
			else
			{
				sortPoint = pickedBlock.transform.position;
			}
		}
		sortedList.Sort((BlockBehaviour a, BlockBehaviour b) => GetDistance(a).CompareTo(GetDistance(b)));
		yield return null;
		if (buildingList != null)
		{
			for (int i = 0; i < buildingList.Count; i++)
			{
				if ((bool)buildingList[i] && buildingList[i].Prefab.hasBVC)
				{
					buildingList[i].VisualController.UpdateVis();
				}
			}
		}
		yield return null;
		for (int j = 0; j < sortedList.Count; j++)
		{
			if ((bool)sortedList[j] && sortedList[j].Prefab.hasBVC)
			{
				sortedList[j].VisualController.UpdateVis();
				if (delay < Time.unscaledDeltaTime)
				{
					yield return null;
				}
				else
				{
					yield return new WaitForSecondsRealtime(delay);
				}
			}
		}
		yield return null;
		SingleInstance<BlockSkinLoader>.Instance.UseAnimation = false;
	}

	private void OnDisable()
	{
		OptionsMaster.skinsEnabled = false;
	}
}
