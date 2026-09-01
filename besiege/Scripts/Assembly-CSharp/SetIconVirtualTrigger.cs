using UnityEngine;

public class SetIconVirtualTrigger : MonoBehaviour
{
	public Transform iconRotator;

	public Transform[] quads;

	public Transform quad;

	protected Camera cam;

	private MouseOrbit mouseOrbit;

	private int lastIndex = -1;

	private bool started;

	private void Awake()
	{
		if (StatMaster.isHeadless || StatMaster.levelSimulating)
		{
			base.enabled = false;
		}
		Init();
	}

	private void Init()
	{
		mouseOrbit = SingleInstanceFindOnly<MouseOrbit>.Instance;
		cam = Camera.main;
		started = true;
	}

	private void OnBecameVisible()
	{
		base.enabled = true;
	}

	private void OnBecameInvisible()
	{
		base.enabled = false;
	}

	private void Update()
	{
		if (!started)
		{
			Init();
		}
		Transform transform = quads[0];
		Vector3 camForward = mouseOrbit.camForward;
		Vector3 camUp = mouseOrbit.camUp;
		Vector3 vector = transform.forward;
		int num = 0;
		float num2 = Vector3.Angle(vector, camForward);
		for (int i = 1; i < quads.Length; i++)
		{
			Vector3 vector2 = quads[i].position - base.transform.position;
			float num3 = Vector3.Angle(vector2, camForward);
			if (num3 < num2)
			{
				num2 = num3;
				transform = quads[i];
				vector = vector2;
				num = i;
			}
		}
		quad = transform;
		if (num == lastIndex)
		{
			return;
		}
		Vector3 vector3 = transform.up;
		Vector3 right = transform.right;
		Vector3[] array = new Vector3[3]
		{
			right,
			-vector3,
			-right
		};
		num2 = Vector3.Angle(vector3, camUp);
		for (int j = 0; j < array.Length; j++)
		{
			float num4 = Vector3.Angle(array[j], camUp);
			if (num4 < num2)
			{
				num2 = num4;
				vector3 = array[j];
			}
		}
		Debug.DrawLine(base.transform.position, vector, new Color(0.5f, 0.5f, 1f));
		Debug.DrawLine(base.transform.position, vector, new Color(0.5f, 1f, 0.5f));
		iconRotator.rotation = Quaternion.LookRotation(vector, vector3);
		lastIndex = num;
	}
}
