using INab.Common;
using UnityEngine;

namespace INab.InteractiveDissolveDemo
{
	public class HowToUseDissolve : MonoBehaviour
	{
		private InteractiveEffect effect;

		public Vector3 initialPositionPlane;

		public Vector3 finalPositionPlane;

		public Vector3 initialScaleEllipse;

		public Vector3 finalScaleEllipse;

		public void SendPlayEvents()
		{
			effect.visualEffect.Play();
		}

		public void SendStopEvents()
		{
			effect.visualEffect.Stop();
		}

		public void PlayEffects()
		{
			effect.PlayEffect();
		}

		public void ReverseEffects()
		{
			effect.ReverseEffect(1f);
		}

		public void GetRendererMaterials()
		{
			effect.GetRendererMaterials();
		}

		public void RefreshEffects()
		{
			effect.UpdateAndSetupEffect();
		}

		public void UpdateMaskTransformManual(float effectLerp)
		{
			effect.UpdateMaskTransform(effectLerp);
		}

		public void UpdateGuideStrenght(float strength)
		{
			effect.GuideStrength = strength;
			effect.PassMaterialPropertiesToGraph();
		}

		public void UsePlaneMaskType()
		{
			effect.ChangeMaskType(InteractiveEffectMaskType.Plane);
			effect.usePositionTransform = true;
			effect.useScaleTransform = false;
			effect.initialPosition = initialPositionPlane;
			effect.finalPosition = finalPositionPlane;
		}

		public void UseEllipseMaskType()
		{
			effect.ChangeMaskType(InteractiveEffectMaskType.Ellipse);
			effect.usePositionTransform = false;
			effect.useScaleTransform = true;
			effect.initialScale = initialScaleEllipse;
			effect.finalScale = finalScaleEllipse;
		}
	}
}
