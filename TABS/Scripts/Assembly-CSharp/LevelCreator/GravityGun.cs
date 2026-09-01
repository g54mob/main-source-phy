using System.Collections;
using System.Collections.Generic;
using Landfall.TABS_Input;
using UnityEngine;

namespace LevelCreator
{
	public class GravityGun : MonoBehaviour
	{
		public Transform attractionPointInitial;

		private Transform attractionPoint;

		public ParticleSystem attractionEffectInitial;

		private ParticleSystem attractionEffect;

		public GameObject actionInitial;

		private GameObject action;

		public float releaseForce = 11f;

		public float torqueForce = 2f;

		public float attractionRadius = 10f;

		public float singleAttractionRadius = 3f;

		private bool attract;

		private bool singleAttract;

		private List<DMEditorComponent> attractedObjects = new List<DMEditorComponent>();

		private DMEditor dmEditor;

		private CameraScript cameraScript;

		private static string filterObjectID;

		private InputState inputState = new InputState("GravityGunInputState");

		private void AssertionCheck()
		{
			attractionPoint = attractionPointInitial;
			attractionEffect = attractionEffectInitial;
			action = actionInitial;
		}

		private void Start()
		{
			AssertionCheck();
			dmEditor = DMEditor.Instance;
			dmEditor.SetVisualTargetMode(DMEditor.VisualTargetMode.None);
			cameraScript = dmEditor.playerCamera.GetComponent<CameraScript>();
			AssignInputStates();
		}

		private void AssignInputStates()
		{
			PlayerActions instance = PlayerActions.Instance;
			inputState.ClearAllEvents();
			inputState.AddOnKeyDownListener(instance.m_toolPrimary, delegate
			{
				attract = true;
			});
			inputState.AddOnKeyUpListener(instance.m_toolPrimary, delegate
			{
				Release();
			});
			inputState.AddOnKeyDownListener(instance.m_toolSecondary, delegate
			{
				if (!attract)
				{
					attract = true;
					singleAttract = true;
				}
			});
			inputState.AddOnKeyUpListener(instance.m_toolSecondary, delegate
			{
				Release();
			});
			inputState.AddOnKeyDownListener(instance.m_toolSpecial1, delegate
			{
				filterObjectID = null;
			});
			InputManager.PushState(inputState);
		}

		private void Update()
		{
			if (attract)
			{
				Attract();
			}
		}

		private void Attract()
		{
			ParticleSystem.MainModule main = attractionEffect.main;
			main.startSizeMultiplier = 3f;
			if (!singleAttract || attractedObjects.Count == 0)
			{
				attractedObjects = dmEditor.GetObjectsInSphere(attractionPoint.position, singleAttract ? singleAttractionRadius : attractionRadius);
			}
			foreach (DMEditorComponent attractedObject in attractedObjects)
			{
				if (string.IsNullOrEmpty(filterObjectID) || !(filterObjectID != attractedObject.ObjectTypeId))
				{
					if (attractedObject.SimulatingPhysics)
					{
						attractedObject.EndPhysicsSimulation();
					}
					attractedObject.Position += (attractionPoint.position - attractedObject.Position) * Time.deltaTime * 20f;
					if (singleAttract)
					{
						break;
					}
				}
			}
		}

		private void Release()
		{
			ParticleSystem.MainModule main = attractionEffect.main;
			main.startSizeMultiplier = 1f;
			Action component = Object.Instantiate(action, dmEditor.Actions.transform).GetComponent<Action>();
			foreach (DMEditorComponent attractedObject in attractedObjects)
			{
				if (!(attractedObject == null) && (string.IsNullOrEmpty(filterObjectID) || !(filterObjectID != attractedObject.ObjectTypeId)))
				{
					if (!singleAttract)
					{
						attractedObject.GetComponentInChildren<Collider>().enabled = false;
					}
					attractedObject.SimulatePhysics(component, releaseForce * (float)((!singleAttract) ? 1 : 0), scaleForceByMass: true, torqueForce);
					if (singleAttract)
					{
						break;
					}
				}
			}
			if (!singleAttract)
			{
				StartCoroutine(EnableColliders());
				ScreenShake.Instance.AddForce(-base.transform.forward * attractedObjects.Count, base.transform.position);
			}
			else
			{
				attractedObjects.Clear();
			}
			attract = false;
			singleAttract = false;
		}

		private IEnumerator EnableColliders()
		{
			yield return new WaitForSeconds(0.8f);
			foreach (DMEditorComponent attractedObject in attractedObjects)
			{
				if (attractedObject != null)
				{
					attractedObject.GetComponentInChildren<Collider>().enabled = true;
				}
			}
			attractedObjects.Clear();
		}
	}
}
