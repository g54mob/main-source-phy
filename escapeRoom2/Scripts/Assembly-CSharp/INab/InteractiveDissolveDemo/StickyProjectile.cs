using INab.Common;
using UnityEngine;

namespace INab.InteractiveDissolveDemo
{
	public class StickyProjectile : MonoBehaviour
	{
		public LayerMask mask;

		private void OnCollisionEnter(Collision other)
		{
			if ((int)mask == ((int)mask | (1 << other.gameObject.layer)))
			{
				InteractiveEffect interactiveEffect = other.gameObject.GetComponentInParent<InteractiveEffect>();
				if (interactiveEffect == null)
				{
					interactiveEffect = other.gameObject.GetComponentInChildren<InteractiveEffect>();
				}
				Animator componentInParent = other.gameObject.GetComponentInParent<Animator>();
				if (interactiveEffect == null)
				{
					interactiveEffect = componentInParent.GetComponentInChildren<InteractiveEffect>();
				}
				if (interactiveEffect != null)
				{
					Transform transform = interactiveEffect.mask.transform;
					transform.parent = other.gameObject.transform;
					transform.position = other.GetContact(0).point;
					interactiveEffect.ChangeMaskType(InteractiveEffectMaskType.Ellipse);
					interactiveEffect.usePositionTransform = false;
					interactiveEffect.useScaleTransform = true;
					interactiveEffect.initialScale = Vector3.zero;
					Bounds bounds = interactiveEffect.meshRenderer.bounds;
					Vector3 point = other.GetContact(0).point;
					Vector3 center = bounds.center;
					float num = (point - center).magnitude + bounds.extents.magnitude;
					float num2 = 1f / transform.parent.lossyScale.x;
					num *= num2;
					num *= 1.35f;
					interactiveEffect.finalScale = new Vector3(num, num, num);
					interactiveEffect.PlayEffect();
					Object.Destroy(base.gameObject);
				}
				else
				{
					Debug.Log("No interactive effect detected");
				}
			}
		}
	}
}
