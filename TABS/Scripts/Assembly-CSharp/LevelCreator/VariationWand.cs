using System.Collections.Generic;
using Landfall.TABS_Input;
using UnityEngine;

namespace LevelCreator
{
	public class VariationWand : MonoBehaviour
	{
		private DMEditor dmEditor;

		private Vector3 targetPosition;

		private DMEditorComponent lastHoveredObject;

		private DMEditorComponent hoveredObject;

		public GameObject augmentationEffectInitial;

		private GameObject augmentationEffect;

		private InputState inputState = new InputState("VariationWandInputState");

		private void Start()
		{
			augmentationEffect = augmentationEffectInitial;
			dmEditor = DMEditor.Instance;
			dmEditor.SetVisualTargetMode(DMEditor.VisualTargetMode.Dot);
			AssignInputStates();
		}

		private void AssignInputStates()
		{
			PlayerActions instance = PlayerActions.Instance;
			inputState.ClearAllEvents();
			inputState.AddOnKeyDownListener(instance.m_toolPrimary, delegate
			{
				if ((bool)hoveredObject)
				{
					List<string> groupObjectsIDs = dmEditor.editorObjectTable.GetGroupObjectsIDs(hoveredObject.ObjectTypeId, excludeInputObjectID: true);
					if (groupObjectsIDs.Count > 0)
					{
						DMEditorComponent dMEditorComponent = dmEditor.InstantiateEditorObject(groupObjectsIDs[Random.Range(0, groupObjectsIDs.Count)], hoveredObject, dmEditor.LevelRootObject, animatedSpawn: false);
						ParticleSystem component = Object.Instantiate(augmentationEffect, hoveredObject.transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
						ParticleSystem.ShapeModule shape = component.shape;
						shape.meshRenderer = dMEditorComponent.GetComponentInChildren<MeshRenderer>();
						component.Play();
						Object.Destroy(hoveredObject.gameObject);
						dmEditor.ScheduleTakeLevelSnapshot();
					}
				}
			});
			InputManager.PushState(inputState);
		}

		private void Update()
		{
			targetPosition = Utility.GetTargetPosition(dmEditor.playerCamera.transform.position, dmEditor.playerCamera.transform.forward, dmEditor.rayDistance);
			hoveredObject = dmEditor.GetAnyObjectOrChildInSphere(targetPosition, 0.1f);
			if ((bool)hoveredObject)
			{
				Utility.SetHighlightObject(hoveredObject.gameObject, highlight: true);
				if (hoveredObject != lastHoveredObject)
				{
					if (lastHoveredObject != null)
					{
						Utility.SetHighlightObject(lastHoveredObject.gameObject, highlight: false);
					}
					lastHoveredObject = hoveredObject;
				}
			}
			else if ((bool)lastHoveredObject)
			{
				Utility.SetHighlightObject(lastHoveredObject.gameObject, highlight: false);
			}
		}

		private void OnDestroy()
		{
			if (hoveredObject != null)
			{
				Utility.SetHighlightObject(hoveredObject.gameObject, highlight: false);
			}
			InputManager.RemoveState(inputState);
		}
	}
}
