using UnityEngine;

public class BoneNoisePosition : MonoBehaviour
{
	public float power = 10f;

	public float speed = 10f;

	public Transform[] myKids;

	public Vector3[] kidStartingPos;

	private Perlin noise;

	private Transform relevantKid;

	private void Start()
	{
		noise = new Perlin();
		myKids = new Transform[base.transform.childCount];
		kidStartingPos = new Vector3[base.transform.childCount];
		for (int i = 0; i < base.transform.childCount; i++)
		{
			myKids[i] = base.transform.GetChild(i);
			kidStartingPos[i] = myKids[i].localPosition;
		}
	}

	private void Update()
	{
		TraverseHierarchy();
	}

	private void TraverseHierarchy()
	{
		float num = Time.time * speed + 0.1365143f;
		float num2 = Time.time * speed + 1.21688f;
		float num3 = Time.time * speed + 0.5564f;
		for (int i = 0; i < base.transform.childCount; i++)
		{
			float x = kidStartingPos[i].x;
			float y = kidStartingPos[i].y;
			float z = kidStartingPos[i].z;
			float x2 = noise.Noise(num + x, num + y, num + z) * power;
			float y2 = noise.Noise(num2 + x, num2 + y, num2 + z) * power;
			float z2 = noise.Noise(num3 + x, num3 + y, num3 + z) * power;
			Vector3 localPosition = kidStartingPos[i] + new Vector3(x2, y2, z2);
			myKids[i].localPosition = localPosition;
		}
	}
}
