using Landfall.TABS_Input;
using NaughtyAttributes;
using UnityEngine;

namespace LevelCreator
{
	public class MaterialPainterTool : Tool
	{
		private float m_radius = 5f;

		private Vector3 m_targetPosition;

		private bool m_addMaterial1;

		private bool m_addMaterial2;

		private bool m_addMaterial3;

		private bool m_currentlyEditing;

		private Brush m_brush;

		private float m_strength = 0.3f;

		[SerializeField]
		private ParticleSystem m_paintEffect;

		[SerializeField]
		private ParticleSystem m_fireEffect;

		[SerializeField]
		private MeshRenderer m_armMesh;

		[SerializeField]
		[BoxGroup("Sound")]
		private ContinousSoundData m_paintSoundLoop;

		private PlayContinousSound m_paintSoundObject;

		protected override void Awake()
		{
			base.Awake();
		}

		protected override void Start()
		{
			base.Start();
			DMEditor.Instance.SetVisualObjectSphereRadius(m_radius);
			SetRadius(m_radius);
			m_targetPosition = Utility.GetTargetPositionOnVolume(DMEditor.Instance.playerCamera.transform.position, DMEditor.Instance.playerCamera.transform.forward, DMEditor.Instance.rayDistance);
			m_paintEffect.transform.SetParent(null);
			m_paintEffect.transform.position = m_targetPosition;
		}

		private void Update()
		{
			m_targetPosition = Utility.GetTargetPositionOnVolume(DMEditor.Instance.playerCamera.transform.position, DMEditor.Instance.playerCamera.transform.forward, DMEditor.Instance.rayDistance);
			m_paintEffect.transform.position = Vector3.Lerp(m_paintEffect.transform.position, m_targetPosition, Time.deltaTime * 10f);
			m_paintEffect.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
			new Vector3(0.5f, 0.5f, 0.5f);
			bool flag = true;
			if (m_addMaterial1)
			{
				DMEditor.Instance.VolumeRootObject.LerpMaterial(m_targetPosition, m_brush, 1f / 3f, Volume.defaultLerpIntensity);
			}
			else if (m_addMaterial2)
			{
				DMEditor.Instance.VolumeRootObject.LerpMaterial(m_targetPosition, m_brush, 2f / 3f, Volume.defaultLerpIntensity);
			}
			else if (m_addMaterial3)
			{
				DMEditor.Instance.VolumeRootObject.LerpMaterial(m_targetPosition, m_brush, 0f, Volume.defaultLerpIntensity);
			}
			else
			{
				flag = false;
			}
			if (flag != m_currentlyEditing)
			{
				EnablePaintEffect(flag);
				DMEditor.Instance.EnableSphereEmission(flag);
				EnablePaintSound(flag);
				m_currentlyEditing = flag;
				if (!m_currentlyEditing)
				{
					DMEditor.Instance.ScheduleTakeLevelSnapshot();
				}
			}
		}

		private void EnablePaintEffect(bool enabled)
		{
			if (enabled)
			{
				LevelPresetData currentPreset = DMEditor.CurrentPreset;
				if (m_addMaterial1)
				{
					SetPaintEffectColors(currentPreset.SecondColor, currentPreset.SecondMetallic, currentPreset.SecondSmoothness);
				}
				else if (m_addMaterial2)
				{
					SetPaintEffectColors(currentPreset.ThirdColor, currentPreset.ThirdMetallic, currentPreset.ThirdSmoothness);
				}
				else
				{
					SetPaintEffectColors(currentPreset.TopColor, currentPreset.BaseMetallic, currentPreset.BaseSmoothness);
				}
				m_paintEffect.Play();
				m_fireEffect.Play();
			}
			else
			{
				m_paintEffect.Stop();
				m_fireEffect.Stop();
			}
		}

		private void EnablePaintSound(bool enable)
		{
			if (enable)
			{
				m_paintSoundObject = Utility.PlayContinousSound(m_paintSoundLoop, m_paintEffect.transform);
			}
			else if (m_paintSoundObject != null)
			{
				m_paintSoundObject.Stop(0f);
			}
		}

		private void SetPaintEffectColors(Color col, float metallic, float smoothness)
		{
			ParticleSystem.MainModule main = m_paintEffect.main;
			main.startColor = col;
			main = m_fireEffect.main;
			main.startColor = col;
			foreach (Transform item in m_paintEffect.transform)
			{
				main = item.GetComponent<ParticleSystem>().main;
				main.startColor = col;
			}
			Material mat = m_armMesh.material;
			LeanTween.color(m_armMesh.gameObject, col, 0.5f);
			LeanTween.value(mat.GetFloat("_Metallic"), metallic, 0.5f).setOnUpdate(delegate(float v)
			{
				mat.SetFloat("_Metallic", v);
			});
			LeanTween.value(mat.GetFloat("_Glossiness"), smoothness, 0.5f).setOnUpdate(delegate(float v)
			{
				mat.SetFloat("_Glossiness", v);
			});
		}

		private void GenerateBrush()
		{
			m_brush = VolumeBrushes.CreateCylinderBrush((int)m_radius, 2, 0f, 1f - m_strength);
		}

		public void SetRadius(float radius)
		{
			m_radius = radius;
			DMEditor.Instance.SetVisualObjectSphereRadius(radius);
			GenerateBrush();
			ParticleSystem[] componentsInChildren = m_paintEffect.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				ParticleSystem.ShapeModule shape = componentsInChildren[i].shape;
				shape.radius = radius;
			}
		}

		public void SetBrushStrength(float strength)
		{
			m_strength = strength;
			GenerateBrush();
		}

		protected override void AssignInput(PlayerActions actions)
		{
			base.AssignInput(actions);
			m_inputState.AddOnKeyDownListener(actions.m_toolPrimary, delegate
			{
				m_addMaterial1 = true;
			}, m_contextIcons.m_primaryIcon);
			m_inputState.AddOnKeyUpListener(actions.m_toolPrimary, delegate
			{
				m_addMaterial1 = false;
			});
			m_inputState.AddOnKeyDownListener(actions.m_toolSecondary, delegate
			{
				m_addMaterial3 = true;
			}, m_contextIcons.m_secondaryIcon);
			m_inputState.AddOnKeyUpListener(actions.m_toolSecondary, delegate
			{
				m_addMaterial3 = false;
			});
			m_inputState.AddOnKeyDownListener(actions.m_toolSpecial2, delegate
			{
				m_addMaterial2 = true;
			}, m_contextIcons.m_special2Icon);
			m_inputState.AddOnKeyUpListener(actions.m_toolSpecial2, delegate
			{
				m_addMaterial2 = false;
			});
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (m_paintEffect != null)
			{
				Object.DestroyImmediate(m_paintEffect.gameObject);
			}
		}
	}
}
