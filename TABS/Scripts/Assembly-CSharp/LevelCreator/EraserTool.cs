using System;
using System.Collections.Generic;
using Landfall.TABS_Input;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Rendering;

namespace LevelCreator
{
	public class EraserTool : Tool
	{
		[SerializeField]
		private ParticleSystem m_eraseEffect;

		[SerializeField]
		private ParticleSystem m_eraseRingEffect;

		[SerializeField]
		private ParticleSystem m_eraseEffectOnObject;

		[SerializeField]
		private Material m_dissolveMaterial;

		private static float m_radius = 5f;

		private Vector3 m_targetPosition;

		private bool m_erase;

		private bool m_eraseFoliage;

		private DMEditorObjectTable m_objectTable;

		private static string filterObjectID;

		private Brush m_foliageBrush;

		private List<DMEditorComponent> m_highlightedObjects = new List<DMEditorComponent>();

		[SerializeField]
		[BoxGroup("Sound")]
		private ContinousSoundData m_fireActiveSound;

		private PlayContinousSound m_fireActiveSoundObject;

		[SerializeField]
		[BoxGroup("Sound")]
		private string m_fireObjectSoundRef;

		protected override void Start()
		{
			base.Start();
			SetRadius(m_radius);
			m_objectTable = DMEditor.Instance.editorObjectTable;
		}

		protected override void AssignInput(PlayerActions actions)
		{
			base.AssignInput(actions);
			m_inputState.AddOnKeyDownListener(actions.m_toolPrimary, delegate
			{
				EnableErase(enabled: true);
			}, m_contextIcons.m_primaryIcon);
			m_inputState.AddOnKeyUpListener(actions.m_toolPrimary, delegate
			{
				EnableErase(enabled: false);
			});
			m_inputState.AddOnKeyDownListener(actions.m_toolSecondary, delegate
			{
				filterObjectID = null;
			});
		}

		public void SetRadius(float radius)
		{
			m_radius = radius;
			DMEditor.Instance.SetVisualObjectSphereRadius(radius);
			ParticleSystem.ShapeModule shape = m_eraseEffect.shape;
			shape.radius = radius;
			ParticleSystem.EmissionModule emission = m_eraseEffect.emission;
			emission.rateOverTime = 150f * radius;
			shape = m_eraseRingEffect.shape;
			shape.radius = radius;
			emission = m_eraseRingEffect.emission;
			emission.rateOverTime = 75f * radius;
			GenerateBrush();
		}

		private void GenerateBrush()
		{
			m_foliageBrush = VolumeBrushes.CreateCylinderBrush((int)m_radius, 2, 0f, -0.3f);
		}

		private void Update()
		{
			DMEditor instance = DMEditor.Instance;
			if (Physics.Raycast(instance.playerCamera.transform.position + instance.playerCamera.transform.forward, instance.playerCamera.transform.forward, out var hitInfo, instance.rayDistance, LayerMask.GetMask("Map")))
			{
				m_targetPosition = hitInfo.point;
			}
			else
			{
				m_targetPosition = instance.playerCamera.transform.position + instance.playerCamera.transform.forward * instance.rayDistance;
			}
			m_eraseRingEffect.transform.position = m_targetPosition;
			m_eraseRingEffect.transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.right);
			if (m_erase && m_eraseFoliage)
			{
				instance.VolumeRootObject.SubtractFoliage(m_targetPosition, m_foliageBrush, Volume.fullLerpIntensity);
			}
			m_eraseEffect.transform.position = m_targetPosition;
			m_eraseEffect.transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.right);
			List<DMEditorComponent> objectsInSphere = instance.GetObjectsInSphere(m_targetPosition, m_radius);
			foreach (DMEditorComponent item in objectsInSphere)
			{
				if (item == null)
				{
					continue;
				}
				DMEditorComponent componentInParent = item.GetComponentInParent<DMEditorComponent>();
				if (componentInParent != null && (string.IsNullOrEmpty(filterObjectID) || filterObjectID == componentInParent.ObjectTypeId))
				{
					Utility.SetHighlightObject(item.gameObject, highlight: true);
					if (!m_highlightedObjects.Contains(componentInParent))
					{
						m_highlightedObjects.Add(componentInParent);
					}
					if (m_erase)
					{
						DissolveObject(item.gameObject, m_eraseEffectOnObject, m_dissolveMaterial, m_fireObjectSoundRef);
					}
				}
			}
			for (int i = 0; i < m_highlightedObjects.Count; i++)
			{
				DMEditorComponent dMEditorComponent = m_highlightedObjects[i];
				if (dMEditorComponent != null && !objectsInSphere.Contains(dMEditorComponent))
				{
					Utility.SetHighlightObject(dMEditorComponent.gameObject, highlight: false);
					m_highlightedObjects.RemoveAt(i);
					i--;
				}
			}
		}

		public static void DissolveObject(GameObject obj, ParticleSystem eraseEffectOnObject, Material dissolveMaterial, string soundRef)
		{
			Transform parent = obj.transform;
			while (true)
			{
				if (parent != null && parent.parent != null && (parent.parent.name == "Level" || parent.parent.name == "Map"))
				{
					CreateFireEffect(parent);
					DMEditorComponent[] componentsInChildren = parent.GetComponentsInChildren<DMEditorComponent>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						CreateFireEffect(componentsInChildren[i].transform);
					}
					obj.transform.parent = null;
					Collider[] componentsInChildren2 = obj.GetComponentsInChildren<Collider>();
					for (int j = 0; j < componentsInChildren2.Length; j++)
					{
						if (componentsInChildren2[j] != null)
						{
							UnityEngine.Object.Destroy(componentsInChildren2[j]);
						}
					}
					MeshRenderer[] componentsInChildren3 = obj.GetComponentsInChildren<MeshRenderer>();
					foreach (MeshRenderer duplicateRenderer in componentsInChildren3)
					{
						duplicateRenderer.shadowCastingMode = ShadowCastingMode.Off;
						LeanTween.delayedCall(0.85f, (System.Action)delegate
						{
							duplicateRenderer.gameObject.layer = LayerMask.NameToLayer("TransparentFX");
						});
						for (int num = 0; num < duplicateRenderer.materials.Length; num++)
						{
							Material material = new Material(dissolveMaterial);
							Material[] materials = duplicateRenderer.materials;
							Material material2 = materials[num];
							material.SetColor("_Color", material2.GetColor("_Color"));
							material.SetFloat("_Glossiness", material2.GetFloat("_Glossiness"));
							material.SetFloat("_Metallic", material2.GetFloat("_Metallic"));
							material.SetFloat("_Metallic", material2.GetFloat("_Metallic"));
							material.SetTexture("_MainTex", material2.GetTexture("_MainTex"));
							material.SetFloat("_NoiseScale", duplicateRenderer.bounds.size.sqrMagnitude * 0.001f);
							materials[num] = material;
							duplicateRenderer.materials = materials;
							MaterialValueLerp materialValueLerp = duplicateRenderer.gameObject.AddComponent<MaterialValueLerp>();
							materialValueLerp.startDelay = 0.3f;
							materialValueLerp.speed = 0.5f;
							materialValueLerp.propertyName = "_DissolveAmount";
						}
					}
					Utility.PlaySound(soundRef, 1f, obj.transform.position);
					UnityEngine.Object.Destroy(obj, 1.3f);
					break;
				}
				if (parent != null)
				{
					parent = parent.transform.parent;
					continue;
				}
				break;
			}
			void CreateFireEffect(Transform effectTarget)
			{
				ParticleSystem particleSystem = UnityEngine.Object.Instantiate(eraseEffectOnObject, effectTarget.position, Quaternion.Euler(-90f, 0f, 0f));
				Bounds bounds = Utility.GetBounds(effectTarget);
				ParticleSystem.ShapeModule shape = particleSystem.shape;
				shape.scale = bounds.size;
				ParticleSystem.MainModule main = particleSystem.main;
				main.startSizeMultiplier = Mathf.Lerp(1f, 2f, bounds.size.magnitude / 10f);
				ParticleSystem.EmissionModule emission = particleSystem.emission;
				emission.rateOverTimeMultiplier = Mathf.Lerp(0.5f, 1f, bounds.size.magnitude / 10f);
			}
		}

		private void EnableErase(bool enabled)
		{
			m_erase = enabled;
			if (!enabled)
			{
				m_eraseEffect.Stop();
				if (m_fireActiveSoundObject != null)
				{
					m_fireActiveSoundObject.Stop();
				}
				DMEditor.Instance.ScheduleTakeLevelSnapshot();
			}
			else
			{
				m_eraseEffect.Play();
				m_fireActiveSoundObject = Utility.PlayContinousSound(m_fireActiveSound, m_eraseEffect.transform);
			}
			DMEditor.Instance.EnableSphereEmission(enabled);
		}

		public void EnableFoliageErase(bool enabled)
		{
			m_eraseFoliage = enabled;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			foreach (DMEditorComponent highlightedObject in m_highlightedObjects)
			{
				if ((bool)highlightedObject && (bool)highlightedObject.gameObject)
				{
					Utility.SetHighlightObject(highlightedObject.gameObject, highlight: false);
				}
			}
			if (m_fireActiveSoundObject != null)
			{
				m_fireActiveSoundObject.Stop();
			}
		}
	}
}
