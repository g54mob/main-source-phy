using UnityEngine;

public class GutterWaterController : SimBehaviour
{
	public Transform gutterObj;

	public ParticleSystem particleSystemObj;

	private Vector3 startPos;

	protected override void Start()
	{
		base.Start();
		startPos = RoundToIntVec3(gutterObj.position);
		if (base.isSimulating)
		{
			InvokeRepeating("CheckPos", Random.Range(0f, 2f), 2f);
		}
	}

	private void CheckPos()
	{
		if (RoundToIntVec3(gutterObj.position) != startPos && particleSystemObj != null)
		{
			particleSystemObj.Stop();
		}
	}

	private Vector3 RoundToIntVec3(Vector3 vec)
	{
		return new Vector3(Mathf.RoundToInt(vec.x), Mathf.RoundToInt(vec.y), Mathf.RoundToInt(vec.z));
	}
}
