using System.Collections;
using Landfall.TABS_Input;
using NaughtyAttributes;
using UnityEngine;

namespace LevelCreator
{
	public class LandscapeHazardTool : Tool
	{
		public HazardTable hazardTableInitial;

		private HazardTable hazardTable;

		private GameObject hazard;

		public string defaultHazardKey;

		public Sprite primaryActionDescription;

		public Sprite secondaryActionDescription;

		[SerializeField]
		private ParticleSystem m_fireEffect;

		[SerializeField]
		[BoxGroup("Sound")]
		private string m_fireSoundRef = string.Empty;

		[SerializeField]
		[BoxGroup("Sound")]
		private ContinousSoundData m_chargeSoundLoop;

		private PlayContinousSound m_chargeSoundObject;

		public bool charge;

		[ShowIf("charge")]
		public float chargeSpeed = 0.5f;

		private float chargePrct;

		private bool charging;

		[ShowIf("charge")]
		public ParticleSystem hazardChargeEffectInitial;

		private ParticleSystem hazardChargeEffect;

		[ShowIf("charge")]
		public ParticleSystem hazardReleaseEffectInitial;

		private ParticleSystem hazardReleaseEffect;

		private Vector3 targetPosition;

		private Vector3 burstPosition;

		private static float fireCooldown;

		private float fireTimer = 10000f;

		private bool fire;

		private static int hazardBurstCount;

		private static Vector2 burstDelay;

		private static float burstRadius;

		protected override void Start()
		{
			hazardTable = hazardTableInitial;
			if (charge)
			{
				hazardChargeEffect = hazardChargeEffectInitial;
				hazardReleaseEffect = hazardReleaseEffectInitial;
			}
			base.Start();
			if (hazard == null)
			{
				SetHazard(defaultHazardKey);
			}
		}

		protected override void AssignInput(PlayerActions actions)
		{
			base.AssignInput(actions);
			m_inputState.AddOnKeyDownListener(actions.m_toolPrimary, delegate
			{
				fire = true;
			}, primaryActionDescription);
			m_inputState.AddOnKeyUpListener(actions.m_toolPrimary, delegate
			{
				fire = false;
			});
			if (!charge)
			{
				m_inputState.AddOnKeyDownListener(actions.m_toolSecondary, delegate
				{
					StartCoroutine(BurstHazards());
				}, secondaryActionDescription);
			}
			else
			{
				m_inputState.AddOnKeyDownListener(actions.m_toolSecondary, delegate
				{
					ChargeHazard();
				}, secondaryActionDescription);
				m_inputState.AddOnKeyUpListener(actions.m_toolSecondary, delegate
				{
					ReleaseHazard();
				});
			}
			m_inputState.AddOnKeyDownListener(actions.m_hazardSlowMotion, delegate
			{
				if (Time.timeScale < 1f)
				{
					Time.timeScale = 1f;
				}
				else
				{
					Time.timeScale = 0.2f;
				}
			});
		}

		private void SetHazard(string hazardKey)
		{
			HazardRow rowValue = hazardTable.GetRowValue(hazardKey);
			hazard = rowValue.hazardObject;
			fireCooldown = rowValue.cooldown;
			hazardBurstCount = rowValue.burstCount;
			burstDelay = new Vector2(rowValue.burstDelayMin, rowValue.burstDelayMax);
			burstRadius = rowValue.burstRadius;
		}

		private void FireHazard(Vector3 position, bool takeSnapshot, float radius = 0f)
		{
			if (Utility.FindBestGroundPosition(position + new Vector3(Random.insideUnitCircle.x, 0f, Random.insideUnitCircle.y) * radius, Vector3.up * 10f, out position))
			{
				if (m_fireEffect != null)
				{
					m_fireEffect.Play();
				}
				if (!string.IsNullOrEmpty(m_fireSoundRef))
				{
					Utility.PlaySound(m_fireSoundRef, 1f, targetPosition);
				}
				GameObject obj = Object.Instantiate(hazard, position, Quaternion.identity);
				LandscapeHazard componentInChildren = obj.GetComponentInChildren<LandscapeHazard>();
				componentInChildren.TakeSnapshot = takeSnapshot;
				componentInChildren.ChargePrct = chargePrct;
				Object.Destroy(obj, 10f);
			}
		}

		private IEnumerator BurstHazards()
		{
			if (fireTimer >= fireCooldown)
			{
				fireTimer = 0f;
				burstPosition = targetPosition;
				for (int i = 0; i < hazardBurstCount; i++)
				{
					FireHazard(burstPosition, takeSnapshot: false, burstRadius);
					yield return new WaitForSeconds(Random.Range(burstDelay.x, burstDelay.y));
				}
			}
			DMEditor.Instance.ScheduleTakeLevelSnapshot();
		}

		private void ChargeHazard()
		{
			if (hazardChargeEffect != null)
			{
				hazardChargeEffect.Play();
			}
			charging = true;
			chargePrct = 0f;
			m_chargeSoundObject = Utility.PlayContinousSound(m_chargeSoundLoop, base.transform);
		}

		private void ReleaseHazard()
		{
			if (hazardChargeEffect != null)
			{
				hazardChargeEffect.Stop(withChildren: true, ParticleSystemStopBehavior.StopEmittingAndClear);
			}
			if (hazardReleaseEffect != null)
			{
				hazardReleaseEffect.GetComponent<ParticleSystemPrctScale>().SetPrct(chargePrct);
				hazardReleaseEffect.Play();
			}
			FireHazard(targetPosition, takeSnapshot: true);
			float num = 0f;
			if (chargePrct > 0f)
			{
				num = Mathf.Lerp(150f, 0.5f, 0.5f / chargePrct);
			}
			ScreenShake.Instance.AddForce(Vector3.up * num, base.transform.position + Vector3.forward * 2f);
			charging = false;
			chargePrct = 0f;
			if (m_chargeSoundObject != null)
			{
				m_chargeSoundObject.Stop();
			}
		}

		private void Update()
		{
			targetPosition = Utility.GetTargetPositionOnVolume(DMEditor.Instance.playerCamera.transform.position, DMEditor.Instance.playerCamera.transform.forward, DMEditor.Instance.rayDistance);
			fireTimer += Time.deltaTime;
			if (fire && fireTimer >= fireCooldown)
			{
				fireTimer = 0f;
				FireHazard(targetPosition, takeSnapshot: true);
			}
			if (charging)
			{
				chargePrct += Time.deltaTime * chargeSpeed;
				ScreenShake.Instance.AddForce(Random.insideUnitSphere * Mathf.Lerp(0f, 1.5f, chargePrct), base.transform.position + Vector3.forward * 2f);
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			Time.timeScale = 1f;
		}
	}
}
