using System;
using UnityEngine;
using cakeslice;

[AddComponentMenu("UI/Aero Dynamic Display")]
public class AeroDynamicDisplay : MonoBehaviour
{
	public static Vector3 MovementDirection = Vector3.forward;

	public Transform gizmo;

	public GameObject rotateGizmo;

	public Outline[] outlines = new Outline[0];

	public static AeroDynamicDisplay instance;

	private Quaternion lastRot = Quaternion.identity;

	public static bool IsSelected { get; set; }

	public static void Select(bool selected)
	{
		IsSelected = selected;
		instance.UpdateOutline(selected ? 1 : 0);
		instance.rotateGizmo.SetActive(selected);
	}

	public bool UpdateOutline(int state)
	{
		bool flag = state != 0;
		bool result = false;
		if (flag)
		{
			OutlineEffect.Instance.ChangeTargetType(1);
		}
		for (int i = 0; i < outlines.Length; i++)
		{
			Outline outline = outlines[i];
			if (outline != null)
			{
				if (flag)
				{
					outline.color = state - 1;
				}
				if (outline.enabled != flag)
				{
					outline.enabled = flag;
					OutlineEffect.ToggleOutline(flag);
				}
				result = true;
			}
		}
		return result;
	}

	private void Awake()
	{
		instance = this;
		StatMaster.Mode.AeroDisplayChanged = (Action)Delegate.Combine(StatMaster.Mode.AeroDisplayChanged, new Action(UpdateGizmoDisplay));
		ReferenceMaster.onMachineModified = (Action<Machine>)Delegate.Combine(ReferenceMaster.onMachineModified, new Action<Machine>(OnMachineModified));
		ReferenceMaster.onMachineChanged = (Action<Machine>)Delegate.Combine(ReferenceMaster.onMachineChanged, new Action<Machine>(OnMachineModified));
		ReferenceMaster.onLocalMachineSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLocalMachineSimulation, new Action<bool>(OnSimulationToggle));
		gizmo.gameObject.SetActive(StatMaster.Mode.displayDrag);
		UpdateGizmo();
	}

	protected void OnDestroy()
	{
		StatMaster.Mode.AeroDisplayChanged = (Action)Delegate.Remove(StatMaster.Mode.AeroDisplayChanged, new Action(UpdateGizmoDisplay));
		ReferenceMaster.onMachineModified = (Action<Machine>)Delegate.Remove(ReferenceMaster.onMachineModified, new Action<Machine>(OnMachineModified));
		ReferenceMaster.onMachineChanged = (Action<Machine>)Delegate.Remove(ReferenceMaster.onMachineChanged, new Action<Machine>(OnMachineModified));
		ReferenceMaster.onLocalMachineSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLocalMachineSimulation, new Action<bool>(OnSimulationToggle));
	}

	private void Update()
	{
		SetRotation(base.transform.rotation);
	}

	public void SetRotation(Quaternion rot)
	{
		if (!(base.transform.rotation == lastRot))
		{
			lastRot = base.transform.rotation;
			MovementDirection = base.transform.forward;
			StatMaster.Mode.InvokeAeroDisplayChanged();
		}
	}

	public void OnSimulationToggle(bool sim)
	{
		gizmo.gameObject.SetActive(!sim && StatMaster.Mode.displayDrag);
		rotateGizmo.SetActive(IsSelected && !sim && StatMaster.Mode.displayDrag);
	}

	public void UpdateGizmoDisplay()
	{
		bool flag = !Machine.Active().isSimulating && StatMaster.Mode.displayDrag;
		if (!flag && IsSelected)
		{
			Select(false);
		}
		gizmo.gameObject.SetActive(flag);
		UpdateGizmo();
	}

	private void OnMachineModified(Machine machine)
	{
		if (!(machine == null))
		{
			UpdateGizmo();
		}
	}

	public static void UpdateGizmo()
	{
		if (StatMaster.Mode.displayDrag)
		{
			Vector3 position = new Vector3(0f, 6f, 0f);
			if (Machine.Active().BuildingMachine != null)
			{
				position = Machine.Active().MiddlePosition;
			}
			Bounds bounds = Machine.Active().GetBounds(false);
			instance.transform.position = position;
			Ray ray = new Ray(instance.transform.TransformPoint(new Vector3(0f, 0f, 1000f)), instance.transform.forward * -1000f);
			float distance;
			if (bounds.IntersectRay(ray, out distance))
			{
				Vector3 position2 = ray.origin + ray.direction * distance;
				instance.gizmo.position = position2;
				instance.gizmo.localPosition += Vector3.forward * 5f;
			}
		}
	}

	public static void EncapsulatePoint(Vector3 center, Vector3 pos)
	{
		Debug.DrawRay(pos, Vector3.up, Color.red);
	}

	public static void Reset()
	{
		instance.transform.rotation = Quaternion.identity;
	}
}
