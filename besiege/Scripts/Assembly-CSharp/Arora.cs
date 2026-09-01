using UnityEngine;

public class Arora : MonoBehaviour
{
	private Transform myTransform;

	private float aroraStartScale;

	private float ratio;

	private void Start()
	{
		myTransform = base.transform;
		aroraStartScale = myTransform.localScale.z;
	}

	private void Update()
	{
		ratio += Time.deltaTime / 2f;
		myTransform.localScale = new Vector3(myTransform.localScale.x, myTransform.localScale.y, aroraStartScale * Mathf.Cos(ratio));
	}
}
