using Landfall.TABS;
using UnityEngine;

namespace TFBGames
{
	public class SyncProjectileEffect : MonoBehaviour
	{
		public delegate void AddedProjectileHitEffectEventHandler(HitData hit);

		public UnitEffectBase EffectPrefab;

		private Unit unit;

		private TeamHolder rootTeamHolder;

		public event AddedProjectileHitEffectEventHandler AddedProjectileHitEffect;

		private void Start()
		{
			TeamHolder.GetTeamRelevantComponents(base.transform, ref unit, ref rootTeamHolder);
		}

		public void SyncEffect(HitData hit)
		{
			this.AddedProjectileHitEffect?.Invoke(hit);
		}

		public void DoRemoteEffect(GameObject target)
		{
			if (!(EffectPrefab == null))
			{
				UnitEffectBase componentInChildren = target.transform.root.GetComponentInChildren<UnitEffectBase>();
				if (componentInChildren == null)
				{
					UnitEffectBase unitEffectBase = Object.Instantiate(EffectPrefab, target.transform.root);
					unitEffectBase.transform.position = target.transform.root.position;
					unitEffectBase.transform.rotation = base.transform.rotation;
					unitEffectBase.DoEffect();
					TeamHolder.AddTeamHolder(unitEffectBase.gameObject, unit, rootTeamHolder);
				}
				else
				{
					componentInChildren.Ping();
				}
			}
		}
	}
}
