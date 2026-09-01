using UnityEngine;

public class NodeBuildingGridBehaviour : MonoBehaviour
{
	public static NodeBuildingGridBehaviour Instance = null;

	private GameObject gridObject;

	private Transform camTransform;

	private Material gridMat;

	private bool lastCtrlDown;

	public bool gridEnabled;

	private bool gridHeldOff;

	private bool objectActive;

	private NodeBuildingGridController grid;

	private static Vector3[] axes = new Vector3[6]
	{
		Vector3.right,
		Vector3.left,
		Vector3.up,
		Vector3.down,
		Vector3.forward,
		Vector3.back
	};

	private void Awake()
	{
		camTransform = Camera.main.transform;
		Instance = this;
	}

	public void SetGridEnabled(bool gridEnabled)
	{
		this.gridEnabled = gridEnabled;
	}

	protected void Update()
	{
		Machine machine = Machine.Active();
		if (machine != null)
		{
			bool flag = InputManager.AdvancedBuilding.LeftCtrlKey();
			if (flag != lastCtrlDown)
			{
				machine.nodeController.OnCtrlPressed();
				lastCtrlDown = flag;
			}
		}
		gridHeldOff = InputManager.AdvancedBuilding.LeftCtrlKey();
		bool flag2 = gridEnabled && !gridHeldOff;
		if (objectActive != flag2)
		{
			objectActive = flag2;
			gridObject.SetActive(objectActive);
			SpatialKeyHUDController.GridToggled(objectActive);
		}
		if (!objectActive)
		{
			return;
		}
		Vector3 forward = camTransform.forward;
		Vector3 position = gridObject.transform.position;
		Vector3 vector = camTransform.position - position;
		Vector3 vector2 = axes[0];
		float num = (axes[0] - vector).sqrMagnitude;
		for (int i = 1; i < axes.Length; i++)
		{
			float sqrMagnitude = (axes[i] - vector).sqrMagnitude;
			if (sqrMagnitude < num)
			{
				num = sqrMagnitude;
				vector2 = axes[i];
			}
		}
		Vector3 forward2 = ((!(Vector3.Dot(forward, vector2) < 0f)) ? vector2 : (-vector2));
		gridObject.transform.forward = forward2;
		Plane plane = new Plane(vector2, position);
		AddPiece instance = SingleInstanceFindOnly<AddPiece>.Instance;
		float enter;
		if (plane.Raycast(instance.ray, out enter))
		{
			Vector3 point = instance.ray.GetPoint(enter);
			gridMat.SetVector("_MachinePos", point);
			Vector3 vector3 = gridObject.transform.InverseTransformPoint(point);
			int num2 = Mathf.Clamp(Mathf.RoundToInt(vector3.x / grid.lastStepSize), -grid.stepCount, grid.stepCount);
			int num3 = Mathf.Clamp(Mathf.RoundToInt(vector3.y / grid.lastStepSize), -grid.stepCount, grid.stepCount);
			Vector3 position2 = new Vector3((float)num2 * grid.lastStepSize, (float)num3 * grid.lastStepSize, 0f);
			grid.gridCollider.transform.position = gridObject.transform.TransformPoint(position2);
		}
	}

	public void Init(NodeBuildingGridController nodeGrid, MeshRenderer renderer, GameObject gridObject)
	{
		grid = nodeGrid;
		gridMat = renderer.material;
		gridMat.SetTextureOffset("_MainTex", Vector2.one * 0.5f);
		gridMat.mainTextureScale = Vector2.one * 0.5f;
		this.gridObject = gridObject;
		objectActive = gridObject.activeSelf;
	}
}
