using System;
using Landfall.TABS;
using UnityEngine;

namespace Landfall.TABC
{
	public class UnitData : MonoBehaviour
	{
		public float damageDealt;

		public float damageTaken;

		public UnitDataInstance dataInstance;

		[HideInInspector]
		public UnitDataVisuals visuals;

		public Unit[] spawnedUnits;

		public float placeShake = 1f;

		public float levelUpShake = 1f;

		public bool isExcessive;

		[HideInInspector]
		public bool isBeingDestroyed;

		[HideInInspector]
		public bool isUnitButton = true;

		private void Awake()
		{
			visuals = GetComponent<UnitDataVisuals>();
		}

		private void Start()
		{
			RoundHandler instance = RoundHandler.instance;
			instance.EnterBattleAction = (Action)Delegate.Combine(instance.EnterBattleAction, new Action(ClearData));
		}

		public void PlaceUnit()
		{
			if (!base.transform)
			{
				return;
			}
			isUnitButton = false;
			bool freezeUnit = RoundHandler.instance.roundState != RoundHandler.RoundState.Battle;
			spawnedUnits = new Unit[dataInstance.unit.numberOfUnits];
			_ = base.transform.position;
			float num = Mathf.Clamp(((float)spawnedUnits.Length - 1f) * 0.7f, 0f, float.PositiveInfinity);
			Vector3 a = -Vector3.right * num * 0.5f;
			Vector3 b = Vector3.right * num * 0.5f;
			for (int i = 0; i < spawnedUnits.Length; i++)
			{
				float t = 0.5f;
				if (spawnedUnits.Length > 1)
				{
					t = (float)i / ((float)spawnedUnits.Length - 1f);
				}
				spawnedUnits[i] = UnitHandler.instance.SpawnUnit(dataInstance.unit.unitBlueprint, base.transform.position + Vector3.Lerp(a, b, t), freezeUnit, dataInstance.level);
				UnitCombinations.instance.UpdateUnit(spawnedUnits[i], dataInstance.unit, dataInstance.level);
				Unit obj = spawnedUnits[i];
				obj.DealDamageAction = (Action<float>)Delegate.Combine(obj.DealDamageAction, new Action<float>(DealDamage));
				Unit obj2 = spawnedUnits[i];
				obj2.WasDealtDamageAction = (Action<float>)Delegate.Combine(obj2.WasDealtDamageAction, new Action<float>(TakeDamage));
			}
			ScreenShake.Instance.AddForce(Vector3.up * placeShake, base.transform.position);
			TABCBoardParticles.instance.PlayPlace(base.transform.position);
		}

		public void SetExcess(bool setExcessive)
		{
			if (setExcessive != isExcessive)
			{
				visuals.SetExcess(setExcessive);
			}
			isExcessive = setExcessive;
		}

		private void ClearData()
		{
			damageDealt = 0f;
			damageTaken = 0f;
		}

		private void DealDamage(float damage)
		{
			damageDealt += damage;
		}

		private void TakeDamage(float damage)
		{
			damageTaken += damage;
		}

		public void LevelUp()
		{
			dataInstance.level++;
			if (dataInstance.unitObject != null)
			{
				RemoveUnit();
			}
			if (dataInstance.unitObject != null)
			{
				PlaceUnit();
			}
			ScreenShake.Instance.AddForce(Vector3.up * levelUpShake * dataInstance.level, base.transform.position);
			TABCBoardParticles.instance.PlayLevelUp(base.transform.position, dataInstance.level);
		}

		public void RemoveUnit()
		{
			if (spawnedUnits != null && spawnedUnits.Length != 0)
			{
				for (int i = 0; i < spawnedUnits.Length; i++)
				{
					UnitHandler.instance.RemoveUnit(spawnedUnits[i]);
				}
				GetComponent<UnitDataVisuals>().SetExcess(setExcessive: false, cutOngoingEffect: true);
			}
		}

		public void Destroy()
		{
			for (int i = 0; i < spawnedUnits.Length; i++)
			{
				spawnedUnits[i].DestroyUnit();
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}

		public void RespawnUnit()
		{
			PlaceUnit();
		}

		private void OnDestroy()
		{
			RemoveUnit();
		}
	}
}
