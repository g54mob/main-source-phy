using Landfall.TABS;
using UnityEngine;

namespace TFBGames
{
	public class AddExplosionEffectToChild : MonoBehaviour
	{
		public delegate void AddedObjectEffectToToTargetEventHandler(GameObject target);

		private Unit myUnit;

		private ExplosionEffect effect;

		public event AddedObjectEffectToToTargetEventHandler AddedObjectEffectToTarget;

		private void Start()
		{
			myUnit = base.transform.root.GetComponent<Unit>();
			effect = GetComponent<ExplosionEffect>();
		}

		public void DoExplosionEffect(GameObject target)
		{
			if (!myUnit.IsRemotelyControlled && effect != null)
			{
				effect.DoEffect(target);
				this.AddedObjectEffectToTarget?.Invoke(target);
			}
		}

		public void DoRemoteEffect(GameObject target)
		{
			if (effect != null)
			{
				effect.DoEffect(target);
			}
		}
	}
}
