using Landfall.TABS_Input;
using NaughtyAttributes;
using UnityEngine;

namespace LevelCreator
{
	public class MagnetTool : Tool
	{
		[SerializeField]
		private float m_radius = 10f;

		[SerializeField]
		private float m_attractionForce = 15f;

		[SerializeField]
		private AnimationCurve m_distanceForceCurve;

		[SerializeField]
		private ParticleSystem m_repelEffectPrefab;

		[SerializeField]
		private ParticleSystem m_attractEffectPrefab;

		private ParticleSystem m_visualEffect;

		private Vector3 m_targetPosition;

		private bool m_repel;

		private bool m_attract;

		private DMEditorObjectTable m_objectTable;

		private static string filterObjectID;

		[SerializeField]
		[BoxGroup("Sound")]
		private ContinousSoundData m_pushSound;

		private PlayContinousSound m_pushSoundObject;

		protected override void Start()
		{
			base.Start();
			m_objectTable = DMEditor.Instance.editorObjectTable;
		}

		protected override void AssignInput(PlayerActions actions)
		{
			base.AssignInput(actions);
			m_inputState.AddOnKeyDownListener(actions.m_toolPrimary, delegate
			{
				EnableMagnet(attract: false);
			}, m_contextIcons.m_primaryIcon);
			m_inputState.AddOnKeyUpListener(actions.m_toolPrimary, delegate
			{
				DisableMagnet();
			});
			m_inputState.AddOnKeyDownListener(actions.m_toolSecondary, delegate
			{
				EnableMagnet(attract: true);
			}, m_contextIcons.m_secondaryIcon);
			m_inputState.AddOnKeyUpListener(actions.m_toolSecondary, delegate
			{
				DisableMagnet();
			});
			m_inputState.AddOnKeyDownListener(actions.m_toolSpecial1, delegate
			{
				filterObjectID = null;
			});
		}

		private void DisableMagnet()
		{
			if (m_repel)
			{
				m_repel = false;
				FinishRepelAction();
				EnablePushSound(enable: false);
				DMEditor.Instance.ScheduleTakeLevelSnapshot();
			}
		}

		private void EnableMagnet(bool attract)
		{
			DisableMagnet();
			m_repel = true;
			m_attract = attract;
			m_visualEffect = Object.Instantiate(attract ? m_attractEffectPrefab : m_repelEffectPrefab);
			EnablePushSound(enable: true);
		}

		private void FinishRepelAction()
		{
			if ((bool)m_visualEffect)
			{
				m_visualEffect.Stop();
			}
		}

		private void EnablePushSound(bool enable)
		{
			if (enable)
			{
				m_pushSoundObject = Utility.PlayContinousSound(m_pushSound, m_visualEffect.transform);
			}
			else if (m_pushSoundObject != null)
			{
				m_pushSoundObject.Stop();
			}
		}

		private void Update()
		{
			m_targetPosition = Utility.GetTargetPositionOnVolume(DMEditor.Instance.playerCamera.transform.position, DMEditor.Instance.playerCamera.transform.forward, DMEditor.Instance.rayDistance);
			if (!m_repel)
			{
				return;
			}
			if ((bool)m_visualEffect)
			{
				m_visualEffect.transform.position = m_targetPosition + Vector3.up * 0.4f;
			}
			foreach (DMEditorComponent item in DMEditor.Instance.GetObjectsInSphere(m_targetPosition, m_radius))
			{
				MoveObject(item);
			}
		}

		private void MoveObject(DMEditorComponent targetObject)
		{
			if (!(targetObject == null) && (string.IsNullOrEmpty(filterObjectID) || !(filterObjectID != targetObject.ObjectTypeId)))
			{
				float num = Vector3.Distance(targetObject.Position, m_targetPosition);
				Vector3 vector = (targetObject.Position - m_targetPosition).normalized * m_distanceForceCurve.Evaluate(num / (m_radius * Random.Range(0.8f, 1.2f))) * m_attractionForce * Time.deltaTime;
				targetObject.Position += (m_attract ? (-vector) : vector);
				targetObject.Teleport(DMEditorComponent.TeleportMode.TeleportAll);
				DMEditor.Instance.MarkObjectForSnapping(targetObject);
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			FinishRepelAction();
		}
	}
}
