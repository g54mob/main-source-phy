using UnityEngine;

[CreateAssetMenu(fileName = "Shoot Data", menuName = "TABS/Shoot Data", order = 1)]
public class ShootDataInstance : ScriptableObject
{
	public float screenShake;

	public AnimationCurve chargeUpCamCurve;

	public Vector3 chargeUpCamCurveAngle;

	public float recoil;
}
