using System;
using UnityEngine;

[AddComponentMenu("Water/Objects/Water Static Bob")]
[ExecuteInEditMode]
public class WaterStaticBob : MonoBehaviour
{
	[HideInInspector]
	public Vector3 startPos = Vector3.zero;

	public Vector3[] pivots = new Vector3[1] { Vector3.zero };

	[HideInInspector]
	public float waterStart;

	public bool onlyInBuildMode = true;

	public float viewRange = 1000f;

	[Range(0f, 1f)]
	public float smooth = 0.5f;

	[Range(0f, 1f)]
	public float intensity = 1f;

	public Vector3 rotateAxis = Vector3.forward;

	public float rotateDegrees = 5f;

	public float rotateDuration = 4f;

	private bool isSelected;

	private float time;

	public bool onlyWhenVisible;

	protected bool updateHeight;

	private float rangeSqr = 1000000f;

	private Camera cam;

	private Quaternion orgRotation = Quaternion.identity;

	private Vector3 orgPosition = Vector3.zero;

	private bool defaultsSet;

	protected void OnBecameVisible()
	{
		if (!StatMaster.levelSimulating)
		{
			updateHeight = true;
		}
	}

	protected void OnBecameInvisible()
	{
		updateHeight = false;
	}

	protected void Start()
	{
		cam = Camera.main;
		rangeSqr = viewRange * viewRange;
		if (!StatMaster.levelSimulating)
		{
			base.transform.Rotate(rotateAxis * (0f - rotateDegrees) * 0.4f);
		}
		if (StatMaster.levelSimulating && onlyInBuildMode)
		{
			UnityEngine.Object.Destroy(this);
		}
		if (onlyWhenVisible && onlyInBuildMode && !StatMaster.levelSimulating)
		{
			ReferenceMaster.onPreSimulateMachine = (Action<Machine>)Delegate.Combine(ReferenceMaster.onPreSimulateMachine, new Action<Machine>(StartSimulation));
		}
		SetDefaults();
	}

	public void SetDefaults()
	{
		if (!defaultsSet)
		{
			orgRotation = base.transform.localRotation;
			orgPosition = base.transform.localPosition;
		}
	}

	public void SetPivot()
	{
		if (WaterController.Exist || !Application.isPlaying)
		{
			waterStart = 0f;
			for (int i = 0; i < pivots.Length; i++)
			{
				Vector3 vector = base.transform.TransformPoint(pivots[i]);
				waterStart += WaterController.CheckHeightMap(vector.x, vector.z);
			}
			waterStart /= pivots.Length;
			startPos = base.transform.position;
		}
	}

	protected void OnDisable()
	{
		updateHeight = false;
		if (!Application.isPlaying && base.gameObject.activeInHierarchy && orgPosition != Vector3.zero)
		{
			base.transform.localRotation = orgRotation;
			base.transform.localPosition = orgPosition;
		}
	}

	protected void OnDestroy()
	{
		ReferenceMaster.onPreSimulateMachine = (Action<Machine>)Delegate.Remove(ReferenceMaster.onPreSimulateMachine, new Action<Machine>(StartSimulation));
	}

	protected void Update()
	{
		if (StatMaster.isMP && LevelEditor.Instance.environmentManager.currentEnv != LevelSettings.LevelEnvironment.Water)
		{
			base.transform.localRotation = orgRotation;
			base.transform.localPosition = orgPosition;
		}
		else if ((!StatMaster.levelSimulating || !onlyInBuildMode) && (!onlyWhenVisible || updateHeight) && (cam.transform.position - base.transform.position).sqrMagnitude < rangeSqr)
		{
			SetHeight();
		}
	}

	protected void StartSimulation(Machine m)
	{
		if (!StatMaster.levelSimulating && !updateHeight)
		{
			SetHeight(false);
		}
	}

	protected void SetHeight(bool lerp = true)
	{
		if (StatMaster.isMP && WaterController.Exist && Mathf.Abs(WaterController.waterTransformHeight - base.transform.parent.TransformPoint(orgPosition).y) > 10f)
		{
			base.transform.localRotation = orgRotation;
			base.transform.localPosition = orgPosition;
			return;
		}
		float num = 0f;
		float num2 = 0f;
		Vector3 vector;
		for (int i = 0; i < pivots.Length; i++)
		{
			vector = base.transform.TransformPoint(pivots[i]);
			float num3 = ((i >= 3) ? 0.5f : 1f);
			num2 += num3;
			num += WaterController.CheckHeightMap(vector.x, vector.z) * num3;
		}
		num /= num2;
		float num4 = startPos.y - waterStart;
		num += num4;
		vector = base.transform.position;
		if (lerp)
		{
			vector.y = Mathf.Lerp(num, vector.y, Mathf.Sqrt(smooth));
			vector = Vector3.Lerp(startPos, vector, intensity);
		}
		else
		{
			vector.y = num;
		}
		if (!Application.isPlaying || vector != Vector3.zero)
		{
			base.transform.position = vector;
		}
		float deltaTime = Time.deltaTime;
		if (Application.isPlaying && rotateDegrees != 0f)
		{
			base.transform.Rotate(rotateAxis * Mathf.Sin(time) * rotateDegrees * deltaTime * UnityEngine.Random.Range(0.95f, 1.05f) / rotateDuration);
		}
		time += deltaTime;
	}
}
