using Landfall.TABS;
using UnityEngine;

public class AimForUnitTarget : MonoBehaviour
{
	public AnimationCurve curve;

	public float lerpSpeed;

	private Unit unit;

	private Vector3 startForwad;

	private void Start()
	{
		unit = base.transform.root.GetComponent<Unit>();
		startForwad = base.transform.parent.InverseTransformDirection(base.transform.forward);
	}

	private void Update()
	{
		if ((bool)unit && (bool)unit.data && (bool)unit.data.targetData && (bool)unit.data.targetData.mainRig)
		{
			Vector3 vector = base.transform.parent.TransformDirection(startForwad);
			Vector3 normalized = (unit.data.targetData.mainRig.position - base.transform.position).normalized;
			float time = Vector3.Angle(normalized, vector);
			base.transform.rotation = Quaternion.LookRotation(Vector3.Lerp(base.transform.forward, Vector3.Lerp(vector, normalized, curve.Evaluate(time)), lerpSpeed * Time.deltaTime));
		}
	}
}
