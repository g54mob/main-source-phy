using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CartoonFX
{
	public class CFXR_Demo : MonoBehaviour
	{
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
		public GameObject ground;

		public Collider groundCollider;

		public Transform demoCamera;

		public MonoBehaviour bloom;

		public float rotationSpeed = 10f;

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
			Camera.main.transform.position = camInitialPosition;
			Camera.main.transform.rotation = camInitialRotation;
		}

		private void Awake()
		{
			camInitialPosition = Camera.main.transform.position;
			camInitialRotation = Camera.main.transform.rotation;
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < base.transform.childCount; i++)
			{
				list.Add(base.transform.GetChild(i).gameObject);
			}
			effectsList = list.ToArray();
			PlayAtIndex();
			UpdateLabels();
		}

		private void Update()
		{
			if (rotateCamera)
			{
				demoCamera.RotateAround(Vector3.zero, Vector3.up, rotationSpeed * Time.deltaTime);
			}
			if (Input.GetKeyDown(KeyCode.Space) && currentEffect != null)
			{
				ParticleSystem component = currentEffect.GetComponent<ParticleSystem>();
				if (component == null)
				{
					return;
				}
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
			if ((Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace)) && currentEffect != null)
			{
				currentEffect.SetActive(value: false);
				currentEffect.SetActive(value: true);
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				PreviousEffect();
			}
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				NextEffect();
			}
			if (Input.GetMouseButtonDown(0) && Physics.Raycast(demoCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition)) && currentEffect != null)
			{
				currentEffect.SetActive(value: false);
				currentEffect.SetActive(value: true);
			}
			if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
			{
				ResetCam();
			}
			float axis = Input.GetAxis("Mouse ScrollWheel");
			if (axis != 0f)
			{
				Camera.main.transform.Translate(Vector3.forward * ((axis < 0f) ? (-1f) : 1f), Space.Self);
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
