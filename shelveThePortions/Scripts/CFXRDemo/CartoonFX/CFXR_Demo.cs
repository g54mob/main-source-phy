using System;
using System.Collections.Generic;
using Kino;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace CartoonFX
{
	public class CFXR_Demo : MonoBehaviour
	{
		private static class ButtonsPressed
		{
			internal static bool PlayEffect
			{
				get
				{
					if (Keyboard.current != null)
					{
						return Keyboard.current.spaceKey.wasPressedThisFrame;
					}
					return false;
				}
			}

			internal static bool RestartEffect
			{
				get
				{
					if (Keyboard.current != null)
					{
						if (!Keyboard.current.deleteKey.wasPressedThisFrame)
						{
							return Keyboard.current.backspaceKey.wasPressedThisFrame;
						}
						return true;
					}
					return false;
				}
			}

			internal static bool Left
			{
				get
				{
					if (Keyboard.current != null)
					{
						return Keyboard.current.leftArrowKey.wasPressedThisFrame;
					}
					return false;
				}
			}

			internal static bool Right
			{
				get
				{
					if (Keyboard.current != null)
					{
						return Keyboard.current.rightArrowKey.wasPressedThisFrame;
					}
					return false;
				}
			}

			internal static bool Mouse0
			{
				get
				{
					if (Mouse.current != null)
					{
						return Mouse.current.leftButton.wasPressedThisFrame;
					}
					return false;
				}
			}

			internal static bool Mouse1
			{
				get
				{
					if (Mouse.current != null)
					{
						return Mouse.current.rightButton.wasPressedThisFrame;
					}
					return false;
				}
			}

			internal static bool Mouse2
			{
				get
				{
					if (Mouse.current != null)
					{
						return Mouse.current.middleButton.wasPressedThisFrame;
					}
					return false;
				}
			}

			internal static Vector2 MousePosition
			{
				get
				{
					if (Mouse.current == null)
					{
						return Vector2.zero;
					}
					return Mouse.current.position.value;
				}
			}

			internal static float MouseScrollY
			{
				get
				{
					if (Mouse.current == null)
					{
						return 0f;
					}
					return Mouse.current.scroll.value.y;
				}
			}
		}

		public Image btnSlowMotion;

		public Text lblSlowMotion;

		public Image btnCameraRotation;

		public Text lblCameraRotation;

		public Image btnShowGround;

		public Text lblShowGround;

		public Image btnCamShake;

		public Text lblCamShake;

		public Image btnLights;

		public Text lblLights;

		public Image btnBloom;

		public Text lblBloom;

		[Space]
		public Text labelEffect;

		public Text labelIndex;

		[Space]
		public GameObject groundURP;

		public GameObject groundBIRP;

		private GameObject ground;

		public Transform demoCamera;

		public GameObject eventSystem;

		public float rotationSpeed = 10f;

		public float zoomFactor = 1f;

		private MonoBehaviour bloom;

		private bool slowMotion;

		private bool rotateCamera;

		private bool showGround = true;

		[NonSerialized]
		public GameObject currentEffect;

		private GameObject[] effectsList;

		private int index;

		private Vector3 camInitialPosition;

		private Quaternion camInitialRotation;

		public void NextEffect()
		{
			index++;
			WrapIndex();
			PlayAtIndex();
		}

		public void PreviousEffect()
		{
			index--;
			WrapIndex();
			PlayAtIndex();
		}

		public void ToggleSlowMo()
		{
			slowMotion = !slowMotion;
			Time.timeScale = (slowMotion ? 0.33f : 1f);
			Color white = Color.white;
			white.a = (slowMotion ? 1f : 0.33f);
			btnSlowMotion.color = white;
			lblSlowMotion.color = white;
		}

		public void ToggleCamera()
		{
			rotateCamera = !rotateCamera;
			Color white = Color.white;
			white.a = (rotateCamera ? 1f : 0.33f);
			btnCameraRotation.color = white;
			lblCameraRotation.color = white;
		}

		public void ToggleGround()
		{
			showGround = !showGround;
			ground.SetActive(showGround);
			Color white = Color.white;
			white.a = (showGround ? 1f : 0.33f);
			btnShowGround.color = white;
			lblShowGround.color = white;
		}

		public void ToggleCameraShake()
		{
			CFXR_Effect.GlobalDisableCameraShake = !CFXR_Effect.GlobalDisableCameraShake;
			Color white = Color.white;
			white.a = (CFXR_Effect.GlobalDisableCameraShake ? 0.33f : 1f);
			btnCamShake.color = white;
			lblCamShake.color = white;
		}

		public void ToggleEffectsLights()
		{
			CFXR_Effect.GlobalDisableLights = !CFXR_Effect.GlobalDisableLights;
			Color white = Color.white;
			white.a = (CFXR_Effect.GlobalDisableLights ? 0.33f : 1f);
			btnLights.color = white;
			lblLights.color = white;
		}

		public void ToggleBloom()
		{
			bloom.enabled = !bloom.enabled;
			Color white = Color.white;
			white.a = ((!bloom.enabled) ? 0.33f : 1f);
			btnBloom.color = white;
			lblBloom.color = white;
		}

		public void ResetCam()
		{
			demoCamera.transform.position = camInitialPosition;
			demoCamera.transform.rotation = camInitialRotation;
		}

		private void Awake()
		{
			camInitialPosition = demoCamera.transform.position;
			camInitialRotation = demoCamera.transform.rotation;
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < base.transform.childCount; i++)
			{
				GameObject gameObject = base.transform.GetChild(i).gameObject;
				list.Add(gameObject);
				CFXR_Effect component = gameObject.GetComponent<CFXR_Effect>();
				if (component != null)
				{
					component.clearBehavior = CFXR_Effect.ClearBehavior.Disable;
				}
			}
			effectsList = list.ToArray();
			PlayAtIndex();
			UpdateLabels();
			bool flag = GraphicsSettings.currentRenderPipeline != null;
			ground = (flag ? groundURP : groundBIRP);
			groundURP.SetActive(flag);
			groundBIRP.SetActive(!flag);
			bloom = demoCamera.GetComponent<Kino.Bloom>();
			if (flag)
			{
				bloom = demoCamera.GetComponent<Volume>();
				UniversalAdditionalCameraData universalAdditionalCameraData = demoCamera.GetComponent<UniversalAdditionalCameraData>();
				if (universalAdditionalCameraData == null)
				{
					universalAdditionalCameraData = demoCamera.gameObject.AddComponent<UniversalAdditionalCameraData>();
				}
				universalAdditionalCameraData.renderPostProcessing = true;
			}
			UnityEngine.Object.Destroy(eventSystem.GetComponent<StandaloneInputModule>());
			eventSystem.AddComponent<InputSystemUIInputModule>();
		}

		private void Update()
		{
			if (rotateCamera)
			{
				demoCamera.RotateAround(Vector3.zero, Vector3.up, rotationSpeed * Time.deltaTime);
			}
			if (ButtonsPressed.PlayEffect && currentEffect != null)
			{
				ParticleSystem component = currentEffect.GetComponent<ParticleSystem>();
				if (component.isEmitting)
				{
					component.Stop(withChildren: true);
				}
				else if (!currentEffect.gameObject.activeSelf)
				{
					currentEffect.SetActive(value: true);
				}
				else
				{
					component.Play(withChildren: true);
					CFXR_Effect[] componentsInChildren = currentEffect.GetComponentsInChildren<CFXR_Effect>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].ResetState();
					}
				}
			}
			if (ButtonsPressed.RestartEffect && currentEffect != null)
			{
				currentEffect.SetActive(value: false);
				currentEffect.SetActive(value: true);
			}
			if (ButtonsPressed.Left)
			{
				PreviousEffect();
			}
			if (ButtonsPressed.Right)
			{
				NextEffect();
			}
			if (ButtonsPressed.Mouse0 && Physics.Raycast(demoCamera.GetComponent<Camera>().ScreenPointToRay(ButtonsPressed.MousePosition)) && currentEffect != null)
			{
				currentEffect.SetActive(value: false);
				currentEffect.SetActive(value: true);
			}
			if (ButtonsPressed.Mouse1 || ButtonsPressed.Mouse2)
			{
				ResetCam();
			}
			float mouseScrollY = ButtonsPressed.MouseScrollY;
			if (mouseScrollY != 0f)
			{
				demoCamera.transform.Translate(Vector3.forward * ((mouseScrollY < 0f) ? (-1f) : 1f) * zoomFactor, Space.Self);
			}
		}

		public void PlayAtIndex()
		{
			if (currentEffect != null)
			{
				currentEffect.SetActive(value: false);
			}
			currentEffect = effectsList[index];
			currentEffect.SetActive(value: true);
			UpdateLabels();
		}

		private void WrapIndex()
		{
			if (index < 0)
			{
				index = effectsList.Length - 1;
			}
			if (index >= effectsList.Length)
			{
				index = 0;
			}
		}

		private void UpdateLabels()
		{
			labelEffect.text = currentEffect.name;
			labelIndex.text = $"{index + 1}/{effectsList.Length}";
		}
	}
}
