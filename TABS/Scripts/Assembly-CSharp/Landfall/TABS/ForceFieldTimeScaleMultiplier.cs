using UnityEngine;

namespace Landfall.TABS
{
	public class ForceFieldTimeScaleMultiplier : MonoBehaviour
	{
		private ParticleSystemForceField forceField;

		private ParticleSystem.MinMaxCurve curve;

		private float ogMultiplier;

		private float ogConstant;

		private AnimationCurve compensastionCurve;

		private void Awake()
		{
			compensastionCurve = LandfallUnitDatabase.GetHelpingData().timeScaleForceFieldGravityCompensastion;
			forceField = GetComponent<ParticleSystemForceField>();
			curve = forceField.gravity;
			ogMultiplier = curve.curveMultiplier;
			ogConstant = curve.constant;
		}

		private void Update()
		{
			float num = compensastionCurve.Evaluate(Time.timeScale);
			curve.curveMultiplier = ogMultiplier * num;
			curve.constant = ogConstant * num;
			forceField.gravity = curve;
		}
	}
}
