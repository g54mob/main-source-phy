using System;
using UnityEngine;

[AddComponentMenu("UI/Tools/Symmetry Pivot Handler")]
public class SymmetryPivotHandler : SingleInstance<SymmetryPivotHandler>
{
	[HideInInspector]
	public bool active;

	public float displacement = 1.25f;

	public GameObject pivot;

	public GameObject pivotGhost;

	public Renderer[] xRen;

	public Renderer[] yRen;

	public Renderer[] zRen;

	public Material xMat;

	public Material yMat;

	public Material zMat;

	public Material offMat;

	public Transform shadow;

	protected SymmetryController sc;

	protected Vector3 prevAxis = Vector3.zero;

	protected Vector3 xPos = new Vector3(0f, 1f, 1f);

	protected Vector3 yPos = new Vector3(1f, 0f, 1f);

	protected Vector3 zPos = new Vector3(1f, 1f, 0f);

	public override string Name
	{
		get
		{
			return "SymmetryPivotHandler";
		}
	}

	private void Start()
	{
		ReferenceMaster.onLocalMachineSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLocalMachineSimulation, new Action<bool>(OnSimulationToggle));
		sc = SingleInstanceFindOnly<AddPiece>.Instance.symmetryController;
		pivot.SetActive(active);
		pivotGhost.SetActive(active);
		Renderer[] array = xRen;
		foreach (Renderer renderer in array)
		{
			renderer.material = offMat;
		}
		Renderer[] array2 = yRen;
		foreach (Renderer renderer2 in array2)
		{
			renderer2.material = offMat;
		}
		Renderer[] array3 = zRen;
		foreach (Renderer renderer3 in array3)
		{
			renderer3.material = offMat;
		}
	}

	private void OnSimulationToggle(bool isSim)
	{
		base.gameObject.SetActive(!isSim);
		sc.enabled = !isSim;
	}

	private void OnDestroy()
	{
		ReferenceMaster.onLocalMachineSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLocalMachineSimulation, new Action<bool>(OnSimulationToggle));
	}

	private void LateUpdate()
	{
		Machine machine = Machine.Active();
		if (machine == null)
		{
			return;
		}
		if (machine.isSimulating)
		{
			if (pivot.activeSelf)
			{
				pivot.SetActive(false);
			}
			if (pivotGhost.activeSelf)
			{
				pivotGhost.SetActive(false);
			}
			return;
		}
		if (sc == null)
		{
			base.gameObject.SetActive(false);
			return;
		}
		if (StatMaster.Mode.selectSymmetryPivot && (bool)SingleInstanceFindOnly<AddPiece>.Instance.HoveredBlock)
		{
			if (!pivotGhost.activeSelf)
			{
				pivotGhost.SetActive(true);
			}
			if (!SingleInstanceFindOnly<AddPiece>.Instance.mouseHasHit)
			{
				return;
			}
			pivotGhost.transform.position = SingleInstanceFindOnly<AddPiece>.Instance.mouseHit.collider.bounds.center;
		}
		else if (pivotGhost.activeSelf)
		{
			pivotGhost.SetActive(false);
		}
		if ((bool)sc.symmetryTransform)
		{
			pivot.transform.position = sc.symmetryTransform.position;
		}
		else
		{
			pivot.transform.position = Vector3.up * 6f;
		}
		if (Machine.Active() != null && (bool)Machine.Active().BuildingMachine)
		{
			pivot.transform.rotation = Machine.Active().BuildingMachine.rotation;
		}
		else
		{
			pivot.transform.rotation = Quaternion.identity;
		}
		if (StatMaster.Mode.selectSymmetryPivot)
		{
			active = true;
		}
		else
		{
			active = false;
		}
		if (sc.axis[0] != 0f)
		{
			active = true;
			if (prevAxis[0] == 0f)
			{
				Renderer[] array = xRen;
				foreach (Renderer renderer in array)
				{
					renderer.material = xMat;
				}
			}
		}
		else if (sc.axis[0] == 0f && prevAxis[0] != 0f)
		{
			Renderer[] array2 = xRen;
			foreach (Renderer renderer2 in array2)
			{
				renderer2.material = offMat;
			}
		}
		if (sc.axis[1] != 0f)
		{
			active = true;
			if (prevAxis[1] == 0f)
			{
				Renderer[] array3 = yRen;
				foreach (Renderer renderer3 in array3)
				{
					renderer3.material = yMat;
				}
			}
		}
		else if (sc.axis[1] == 0f && prevAxis[1] != 0f)
		{
			Renderer[] array4 = yRen;
			foreach (Renderer renderer4 in array4)
			{
				renderer4.material = offMat;
			}
		}
		if (sc.axis[2] != 0f)
		{
			active = true;
			if (prevAxis[2] == 0f)
			{
				Renderer[] array5 = zRen;
				foreach (Renderer renderer5 in array5)
				{
					renderer5.material = zMat;
				}
			}
		}
		else if (sc.axis[2] == 0f && prevAxis[2] != 0f)
		{
			Renderer[] array6 = zRen;
			foreach (Renderer renderer6 in array6)
			{
				renderer6.material = offMat;
			}
		}
		if (Machine.Active() == null)
		{
			active = false;
		}
		switch (StatMaster.Mode.selectedTool)
		{
		case StatMaster.Tool.Translate:
		case StatMaster.Tool.Rotate:
		case StatMaster.Tool.Mirror:
		case StatMaster.Tool.Modify:
			if (!StatMaster.advancedBuilding || !StatMaster.Mode.Symmetry.selection)
			{
				active = false;
			}
			break;
		case StatMaster.Tool.Erase:
			if (!StatMaster.advancedBuilding || !StatMaster.Mode.Symmetry.eraser)
			{
				active = false;
			}
			break;
		case StatMaster.Tool.None:
			if (!StatMaster.advancedBuilding || !StatMaster.Mode.Symmetry.placement)
			{
				active = false;
			}
			break;
		default:
			active = false;
			break;
		}
		if (AddPiece.disableBlockPlacement)
		{
			active = false;
		}
		if (pivot.activeSelf != active)
		{
			pivot.SetActive(active);
		}
		if (active)
		{
			if (shadow != null)
			{
				shadow.LookAt(Camera.main.transform);
			}
			Vector3 vector = Machine.Active().BuildingMachine.InverseTransformPoint(Camera.main.transform.position) - Machine.Active().BuildingMachine.InverseTransformPoint(pivot.transform.position);
			Vector3 vector2 = new Vector3((!(vector.x > 0f)) ? (-1f) : 1f, (!(vector.y > 0f)) ? (-1f) : 1f, (!(vector.z > 0f)) ? (-1f) : 1f);
			xRen[0].transform.localPosition = new Vector3(xPos.x * vector2.x, xPos.y * vector2.y, xPos.z * vector2.z) * displacement;
			yRen[0].transform.localPosition = new Vector3(yPos.x * vector2.x, yPos.y * vector2.y, yPos.z * vector2.z) * displacement;
			zRen[0].transform.localPosition = new Vector3(zPos.x * vector2.x, zPos.y * vector2.y, zPos.z * vector2.z) * displacement;
		}
		prevAxis = sc.axis;
	}
}
