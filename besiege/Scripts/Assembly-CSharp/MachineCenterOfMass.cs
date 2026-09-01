using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("UI/Tools/Machine Center Of Mass")]
public class MachineCenterOfMass : MonoBehaviour
{
	public Vector3 CoM = Vector3.zero;

	public Vector3 CoB = Vector3.zero;

	public Transform CenterOfMassVis;

	public Transform CenterOfBuoyancyVis;

	public Transform BuoyancyUp;

	public Transform BuoyancyDown;

	public bool showCOM;

	private Coroutine scheduledUpdate;

	public static bool ShowUpDownForce;

	protected void Awake()
	{
		ReferenceMaster.onMachineChanged = (Action<Machine>)Delegate.Combine(ReferenceMaster.onMachineChanged, new Action<Machine>(OnMachineChanged));
		ReferenceMaster.onCalculateMiddle = (Action<Machine>)Delegate.Combine(ReferenceMaster.onCalculateMiddle, new Action<Machine>(ScheduleCOMUpdate));
		ReferenceMaster.onMachineModified = (Action<Machine>)Delegate.Combine(ReferenceMaster.onMachineModified, new Action<Machine>(ScheduleCOMUpdate));
		BlockMapper.OnParameterChange = (Action<MapperType>)Delegate.Combine(BlockMapper.OnParameterChange, new Action<MapperType>(OnParameterChanged));
		BlockMapper.OnParameterUndo = (Action<UndoAction>)Delegate.Combine(BlockMapper.OnParameterUndo, new Action<UndoAction>(OnParameterUndo));
		ReferenceMaster.onLocalMachineSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLocalMachineSimulation, new Action<bool>(OnSimulationToggle));
	}

	private void OnMachineChanged(Machine machine)
	{
		if (!(machine != null))
		{
			ToggleCOM(false);
		}
	}

	protected void OnDestroy()
	{
		ReferenceMaster.onMachineChanged = (Action<Machine>)Delegate.Remove(ReferenceMaster.onMachineChanged, new Action<Machine>(OnMachineChanged));
		ReferenceMaster.onCalculateMiddle = (Action<Machine>)Delegate.Remove(ReferenceMaster.onCalculateMiddle, new Action<Machine>(ScheduleCOMUpdate));
		ReferenceMaster.onMachineModified = (Action<Machine>)Delegate.Remove(ReferenceMaster.onMachineModified, new Action<Machine>(ScheduleCOMUpdate));
		BlockMapper.OnParameterChange = (Action<MapperType>)Delegate.Remove(BlockMapper.OnParameterChange, new Action<MapperType>(OnParameterChanged));
		BlockMapper.OnParameterUndo = (Action<UndoAction>)Delegate.Remove(BlockMapper.OnParameterUndo, new Action<UndoAction>(OnParameterUndo));
		ReferenceMaster.onLocalMachineSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLocalMachineSimulation, new Action<bool>(OnSimulationToggle));
	}

	private void OnSimulationToggle(bool toggle)
	{
		if (toggle)
		{
			CenterOfMassVis.gameObject.SetActive(false);
			if (WaterController.Exist && (bool)CenterOfBuoyancyVis)
			{
				CenterOfBuoyancyVis.gameObject.SetActive(false);
				BuoyancyDown.gameObject.SetActive(false);
			}
		}
		else if (showCOM)
		{
			CenterOfMassVis.gameObject.SetActive(true);
			if (WaterController.Exist && (bool)CenterOfBuoyancyVis)
			{
				CenterOfBuoyancyVis.gameObject.SetActive(true);
				BuoyancyDown.gameObject.SetActive(true);
			}
		}
	}

	public void ToggleCOM(bool toggle)
	{
		showCOM = false;
		Machine machine = Machine.Active();
		if (toggle && machine != null && !machine.isSimulating)
		{
			showCOM = true;
			ScheduleCOMUpdate(machine);
		}
		CenterOfMassVis.gameObject.SetActive(showCOM);
		if (WaterController.Exist && (bool)CenterOfBuoyancyVis)
		{
			CenterOfBuoyancyVis.gameObject.SetActive(showCOM);
			BuoyancyDown.gameObject.SetActive(showCOM);
		}
	}

	public void OnParameterChanged(MapperType m)
	{
		if (!showCOM)
		{
			return;
		}
		Machine machine = Machine.Active();
		if (machine == null || machine.isSimulating)
		{
			return;
		}
		string text = m.Key.ToLower();
		if (m is MSlider)
		{
			if (text.Contains("mass") || text.Contains("buoyancy") || text.Contains("inertia"))
			{
				ScheduleCOMUpdate(machine);
			}
		}
		else if (m is MToggle)
		{
			if (text.Contains("hascolliders") || text.Contains("opt-collider"))
			{
				ScheduleCOMUpdate(machine);
			}
		}
		else if (m is MMenu && text.Contains("surfmat"))
		{
			ScheduleCOMUpdate(machine);
		}
	}

	public void OnParameterUndo(UndoAction u)
	{
		if (!showCOM)
		{
			return;
		}
		Machine machine = Machine.Active();
		if (!(machine == null) && !machine.isSimulating)
		{
			if (u is UndoActionField)
			{
				ScheduleCOMUpdate(machine);
			}
			else if (u is UndoActionEdit)
			{
				ScheduleCOMUpdate(machine);
			}
		}
	}

	private void ScheduleCOMUpdate(Machine machine)
	{
		if (!(machine == null) && !machine.isSimulating && showCOM && scheduledUpdate == null)
		{
			scheduledUpdate = ReferenceMaster.Instance.StartCoroutine(DelayCOMUpdate(machine));
		}
	}

	private IEnumerator DelayCOMUpdate(Machine machine)
	{
		yield return new WaitForEndOfFrame();
		CalculateCOM(machine);
		scheduledUpdate = null;
	}

	protected virtual void CalculateCOM(Machine machine)
	{
		if (machine == null || machine.isSimulating || !showCOM)
		{
			return;
		}
		CoM = Vector3.zero;
		CoB = Vector3.zero;
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = WaterController.waterTransformHeight + 2f;
		Vector3 zero = Vector3.zero;
		List<BlockBehaviour> buildingBlocks = machine.BuildingBlocks;
		for (int i = 0; i < buildingBlocks.Count; i++)
		{
			BlockBehaviour blockBehaviour = buildingBlocks[i];
			if (blockBehaviour.noRigidbody)
			{
				continue;
			}
			BlockType type = blockBehaviour.Prefab.Type;
			if (type == BlockType.Pin || type == BlockType.CameraBlock || type == BlockType.BuildNode || type == BlockType.BuildEdge)
			{
				continue;
			}
			Vector3 vector = blockBehaviour.GetCenter();
			Rigidbody rigidbody = blockBehaviour.Rigidbody;
			float num5 = rigidbody.mass;
			type = blockBehaviour.Prefab.Type;
			if (type == BlockType.Spring || type == BlockType.RopeWinch || type == BlockType.RopeMeasure)
			{
				GenericDraggedBlock genericDraggedBlock = blockBehaviour as GenericDraggedBlock;
				num5 = genericDraggedBlock.startBody.mass + genericDraggedBlock.endBody.mass;
			}
			if (WaterController.Exist)
			{
				float num6 = 8f;
				float num7 = 0f;
				float num8 = num5 * Physics.gravity.y;
				if (!blockBehaviour.IgnoredByWater)
				{
					if (blockBehaviour.density <= 0f)
					{
						blockBehaviour.CalculateDensity();
					}
					num6 = blockBehaviour.density;
					if (vector.y < num4)
					{
						num7 = blockBehaviour.originalMassDensity * 50f * 1.5f / num6;
					}
				}
				if (ShowUpDownForce)
				{
					num7 += num8;
				}
				if (num7 > 0f)
				{
					CoB += vector * num7;
					num2 += num7;
				}
				if (ShowUpDownForce)
				{
					float num9 = 0f - num7;
					if (num9 > 0f)
					{
						zero += vector * num9;
						num3 += num9;
					}
				}
				else if (num6 > 0f)
				{
					num6 *= 0.4377f * num5;
					zero += vector * num6;
					num3 += num6;
				}
			}
			bool flag = blockBehaviour.Prefab.hasMyBounds;
			type = blockBehaviour.Prefab.Type;
			if (type == BlockType.BuildSurface)
			{
				BuildSurface buildSurface = blockBehaviour as BuildSurface;
				if (buildSurface.CollisionActive)
				{
					vector = (buildSurface.GetCenter() + buildSurface.CornersCenter()) / 2f;
				}
				else
				{
					vector = buildSurface.transform.position;
					flag = false;
				}
			}
			if (flag)
			{
				vector = Vector3.zero;
				float num10 = 0f;
				foreach (Collider childCollider in blockBehaviour.myBounds.childColliders)
				{
					if (childCollider.enabled && childCollider.gameObject.activeInHierarchy)
					{
						float magnitude = childCollider.bounds.size.magnitude;
						if (!(magnitude <= 0f))
						{
							Vector3 zero2 = Vector3.zero;
							zero2 = ((childCollider is BoxCollider) ? (childCollider as BoxCollider).center : ((childCollider is SphereCollider) ? (childCollider as SphereCollider).center : ((!(childCollider is CapsuleCollider)) ? childCollider.transform.InverseTransformPoint(childCollider.bounds.center) : (childCollider as CapsuleCollider).center)));
							vector += childCollider.transform.TransformPoint(zero2) * magnitude;
							num10 += magnitude;
						}
					}
				}
				if (num10 > 0f)
				{
					vector /= num10;
				}
			}
			CoM += vector * num5;
			num += num5;
		}
		if (num > 0f)
		{
			CoM /= num;
			CenterOfMassVis.position = CoM;
			CenterOfMassVis.rotation = machine.BuildingMachine.rotation;
		}
		if (WaterController.Exist && (bool)CenterOfBuoyancyVis)
		{
			if (num2 != 0f)
			{
				CoB /= num2;
				CenterOfBuoyancyVis.position = CoB;
			}
			else
			{
				Transform centerOfBuoyancyVis = CenterOfBuoyancyVis;
				Vector3 position = Vector3.one * -1000f;
				BuoyancyUp.position = position;
				centerOfBuoyancyVis.position = position;
			}
			if (num3 > 0f && StatMaster.advancedBuilding)
			{
				zero /= num3;
				BuoyancyDown.position = zero;
			}
			else
			{
				BuoyancyDown.position = Vector3.one * -1000f;
			}
		}
	}

	private float Log(float density)
	{
		return Mathf.Log(density * 0.4377f) + 1f;
	}
}
