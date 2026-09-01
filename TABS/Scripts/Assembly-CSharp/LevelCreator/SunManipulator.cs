using InControl;
using Landfall.TABS_Input;
using NaughtyAttributes;
using UnityEngine;

namespace LevelCreator
{
	public class SunManipulator : Tool
	{
		private bool m_rotateSun;

		private Vector3 m_currentRotation;

		[SerializeField]
		private float m_sensitivity = 2f;

		[SerializeField]
		private float m_speed = 3f;

		public static readonly float m_maxIntensity = 1.4f;

		[SerializeField]
		private Transform m_sunRepresentation;

		[SerializeField]
		private ParticleSystem m_sunParticles;

		private ParticleSystemRenderer m_sunParticlesRenderer;

		private Transform m_sunSphere;

		[SerializeField]
		private Gradient m_temperatureGradient;

		[Header("Ambient Colors")]
		[SerializeField]
		[ColorUsage(false, true)]
		private Color m_daySkyColor = Color.white;

		[SerializeField]
		[ColorUsage(false, true)]
		private Color m_dayEquatorColor = Color.white;

		[SerializeField]
		[ColorUsage(false, true)]
		private Color m_dayGroundColor = Color.white;

		[Space]
		[SerializeField]
		[ColorUsage(false, true)]
		private Color m_nightSkyColor = Color.white;

		[SerializeField]
		[ColorUsage(false, true)]
		private Color m_nightEquatorColor = Color.white;

		[SerializeField]
		[ColorUsage(false, true)]
		private Color m_nightGroundColor = Color.white;

		[SerializeField]
		[BoxGroup("Sound")]
		private ContinousSoundData m_manipulationSound;

		private PlayContinousSound m_manipulationSoundObject;

		private Transform m_sun => DMEditor.Instance.m_sun;

		private Light m_directionalLight => DMEditor.Instance.m_directionalLight;

		private void AssertionCheck()
		{
		}

		protected override void Start()
		{
			AssertionCheck();
			base.Start();
			m_sunSphere = m_sun.transform.GetChild(0).GetChild(0);
			m_currentRotation = m_sun.transform.rotation.eulerAngles;
			UpdateSun();
			m_sunParticlesRenderer = m_sunParticles.GetComponent<ParticleSystemRenderer>();
		}

		protected override void AssignInput(PlayerActions actions)
		{
			base.AssignInput(actions);
			m_inputState.AddOnKeyDownListener(actions.m_toolPrimary, delegate
			{
				EnableSun(enabled: true);
			}, m_contextIcons.m_primaryIcon);
			m_inputState.AddOnKeyUpListener(actions.m_toolPrimary, delegate
			{
				EnableSun(enabled: false);
			});
		}

		private void EnableSun(bool enabled)
		{
			if (DMEditor.Instance != null)
			{
				if (PlayerActions.Instance.InputType == InputType.Controller)
				{
					if (enabled)
					{
						DMEditor.Instance.DisableFirstPersonMovement();
					}
					else
					{
						DMEditor.Instance.EnableFirstPersonMovement();
					}
				}
				else
				{
					DMEditor.Instance.EnableFirstPersonMovement();
				}
				if (DMEditor.Instance.playerController != null)
				{
					DMEditor.Instance.playerController.SetRotationLock(enabled);
				}
			}
			EnableManipulationSound(enabled);
			m_rotateSun = enabled;
			if (m_sunParticles != null)
			{
				if (enabled)
				{
					m_sunParticles.Play();
				}
				else
				{
					m_sunParticles.Stop();
				}
			}
		}

		private void EnableManipulationSound(bool enable)
		{
			if (enable)
			{
				m_manipulationSoundObject = Utility.PlayContinousSound(m_manipulationSound, base.transform);
			}
			else if (m_manipulationSoundObject != null)
			{
				m_manipulationSoundObject.Stop();
			}
		}

		private void Update()
		{
			PlayerTwoAxisAction playerTwoAxisAction = PlayerActions.Instance.m_aim;
			if (PlayerActions.Instance.InputType == InputType.Controller)
			{
				playerTwoAxisAction = PlayerActions.Instance.m_move;
			}
			if (m_rotateSun)
			{
				m_currentRotation += new Vector3(playerTwoAxisAction.Y, playerTwoAxisAction.X, 0f) * m_sensitivity;
				UpdateSun();
			}
		}

		private void UpdateSun()
		{
			DMEditor.Instance.SetTimeOfDay(Quaternion.Lerp(m_sun.transform.rotation, Quaternion.Euler(m_currentRotation), Time.deltaTime * m_speed));
			m_sunRepresentation.rotation = m_sun.transform.rotation;
			m_sunRepresentation.Rotate(Vector3.right, -80f);
		}

		public void SetSunIntensity(float value)
		{
			DMEditor.Instance.SetSunIntensity(value);
			if (m_sunParticlesRenderer != null)
			{
				m_sunParticlesRenderer.sharedMaterial.SetColor("_EmissionColor", m_directionalLight.color * (m_directionalLight.intensity + 1f));
			}
			if (m_sunParticles != null)
			{
				ParticleSystem.EmissionModule emission = m_sunParticles.emission;
				emission.rateOverTimeMultiplier = Mathf.Lerp(50f, 300f, m_directionalLight.intensity / m_maxIntensity);
			}
		}

		public void SetSunTemperature(float value)
		{
			Color sunColor = m_temperatureGradient.Evaluate(value);
			DMEditor.Instance.SetSunColor(sunColor);
		}

		protected override void OnDestroy()
		{
			EnableSun(enabled: false);
			base.OnDestroy();
		}
	}
}
