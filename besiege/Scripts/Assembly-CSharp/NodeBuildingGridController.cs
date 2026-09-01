using System;
using UnityEngine;
using UnityEngine.Rendering;

public class NodeBuildingGridController
{
	private MeshFilter filter;

	private MeshRenderer renderer;

	private GameObject gridObject;

	private NodeBuildingGridBehaviour gridBehaviour;

	public float lastStepSize = -1f;

	public float radius = 5f;

	public int stepCount;

	public Collider gridCollider;

	public static float minGrid = 0.25f;

	public static float maxGrid = 5f;

	public NodeBuildingGridController()
	{
		ReferenceMaster.onShowNodeGridToggled = (Action)Delegate.Combine(ReferenceMaster.onShowNodeGridToggled, new Action(OnShowNodeGridToggled));
		Generate();
	}

	private void Generate()
	{
		float num = Mathf.Clamp(StatMaster.Mode.Transform.Snap.position, minGrid, maxGrid);
		num = ((!StatMaster.advancedBuilding) ? 1f : num);
		if (lastStepSize == num)
		{
			return;
		}
		lastStepSize = num;
		if (gridObject == null)
		{
			gridObject = new GameObject("Node Building Grid");
			filter = gridObject.AddComponent<MeshFilter>();
			renderer = gridObject.AddComponent<MeshRenderer>();
			renderer.shadowCastingMode = ShadowCastingMode.Off;
			renderer.receiveShadows = false;
			renderer.motionVectors = false;
			renderer.material = ReferenceMaster.Instance.NodeBuildingGridMaterial;
			GameObject gameObject = new GameObject("Grid Parent");
			gridObject.transform.SetParent(gameObject.transform, true);
			gridBehaviour = gameObject.AddComponent<NodeBuildingGridBehaviour>();
			gridBehaviour.Init(this, renderer, gridObject);
			GameObject gameObject2 = new GameObject("AddPoint Trigger", typeof(SphereCollider));
			gameObject2.layer = LayerMask.NameToLayer("AddPoint");
			gameObject2.tag = "AddPointUseCenter";
			gameObject2.transform.SetParent(gridObject.transform, false);
			SphereCollider component = gameObject2.GetComponent<SphereCollider>();
			component.isTrigger = true;
			component.radius = 0.2f;
			gridCollider = component;
		}
		stepCount = Mathf.CeilToInt(radius / num);
		int num2 = stepCount * 2;
		float num3 = (float)stepCount * num;
		Vector3[] array = new Vector3[(num2 + 1) * (num2 + 1)];
		Vector2[] array2 = new Vector2[(num2 + 1) * (num2 + 1)];
		int[] array3 = new int[6 * num2 * num2];
		int i = 0;
		int num4 = 0;
		for (; i < num2 + 1; i++)
		{
			for (int j = 0; j < num2 + 1; j++)
			{
				int num5 = i * (num2 + 1) + j;
				array[num5] = new Vector3((float)i * num - num3, (float)j * num - num3, 0f);
				array2[num5] = new Vector2(i, j);
				if (i != num2 && j != num2)
				{
					array3[num4] = num5;
					array3[num4 + 1] = num5 + 1;
					array3[num4 + 2] = num5 + 1 + (num2 + 1);
					array3[num4 + 3] = num5;
					array3[num4 + 4] = num5 + 1 + (num2 + 1);
					array3[num4 + 5] = num5 + (num2 + 1);
					num4 += 6;
				}
			}
		}
		Mesh mesh = new Mesh();
		mesh.name = "Node Building Grid";
		mesh.vertices = array;
		mesh.triangles = array3;
		mesh.uv = array2;
		mesh.RecalculateNormals();
		if (filter.mesh != null)
		{
			UnityEngine.Object.Destroy(filter.mesh);
		}
		filter.mesh = mesh;
	}

	public void Dispose()
	{
		ReferenceMaster.onShowNodeGridToggled = (Action)Delegate.Remove(ReferenceMaster.onShowNodeGridToggled, new Action(OnShowNodeGridToggled));
		if ((bool)gridObject)
		{
			UnityEngine.Object.Destroy(gridObject.transform.parent.gameObject);
		}
	}

	public void SetActive(bool active)
	{
		bool flag = OptionsMaster.BesiegeConfig.ShowSurfaceNodeGrid && active;
		if (gridBehaviour.gridEnabled != flag)
		{
			if (flag)
			{
				Generate();
				ReferenceMaster.onAdvancedBuildingToggled = (Action)Delegate.Combine(ReferenceMaster.onAdvancedBuildingToggled, new Action(OnBTToggled));
			}
			else
			{
				ReferenceMaster.onAdvancedBuildingToggled = (Action)Delegate.Remove(ReferenceMaster.onAdvancedBuildingToggled, new Action(OnBTToggled));
			}
		}
		gridBehaviour.SetGridEnabled(flag);
	}

	private void OnBTToggled()
	{
		Generate();
	}

	private void OnShowNodeGridToggled()
	{
		if (gridBehaviour.gridEnabled)
		{
			Generate();
		}
		SetActive(gridBehaviour.gridEnabled);
	}

	public void FocusOn(BuildNodeBlock node)
	{
		gridObject.transform.position = node.transform.position;
	}
}
