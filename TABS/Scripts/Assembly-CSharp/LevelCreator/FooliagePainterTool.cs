using Landfall.TABS_Input;
using UnityEngine;

namespace LevelCreator
{
	public class FooliagePainterTool : MonoBehaviour
	{
		private DMEditor dmEditor;

		private float radius = 5f;

		private InputState actionInputState = new InputState("FoliagePainterTool.Action");

		private Vector3 targetPosition;

		private bool addToFoliage;

		private bool subtractFromFoliage;

		private bool currentlyEditing;

		public string defaultBrush;

		private Brush brush;

		private void Start()
		{
			dmEditor = DMEditor.Instance;
			dmEditor.SetVisualTargetMode(DMEditor.VisualTargetMode.Sphere);
			dmEditor.SetVisualObjectSphereRadius(radius);
			brush = dmEditor.brushTable.GetBrush(defaultBrush);
			brush = VolumeBrushes.CreateScaledBrush(brush, new Vector3(0.65f, 0f, 0.65f));
			AssignInputStates();
			InputManager.PushState(actionInputState);
		}

		private void Update()
		{
			targetPosition = Utility.GetTargetPositionOnVolume(dmEditor.playerCamera.transform.position, dmEditor.playerCamera.transform.forward, dmEditor.rayDistance);
			bool flag = false;
			if (addToFoliage)
			{
				dmEditor.VolumeRootObject.AddFoliage(targetPosition, brush, Volume.defaultLerpIntensity);
				flag = true;
			}
			if (subtractFromFoliage)
			{
				dmEditor.VolumeRootObject.SubtractFoliage(targetPosition, brush, Volume.defaultLerpIntensity);
				flag = true;
			}
			if (flag != currentlyEditing)
			{
				dmEditor.EnableSphereEmission(flag);
				currentlyEditing = flag;
				if (!currentlyEditing)
				{
					dmEditor.ScheduleTakeLevelSnapshot();
				}
			}
			if (Input.GetMouseButtonDown(2))
			{
				dmEditor.VolumeRootObject.SetAllFoliage(1f);
				dmEditor.ScheduleTakeLevelSnapshot();
			}
		}

		private void AssignInputStates()
		{
			PlayerActions instance = PlayerActions.Instance;
			actionInputState.AddOnKeyDownListener(instance.m_toolPrimary, delegate
			{
				addToFoliage = true;
			});
			actionInputState.AddOnKeyUpListener(instance.m_toolPrimary, delegate
			{
				addToFoliage = false;
			});
			actionInputState.AddOnKeyDownListener(instance.m_toolSecondary, delegate
			{
				subtractFromFoliage = true;
			});
			actionInputState.AddOnKeyUpListener(instance.m_toolSecondary, delegate
			{
				subtractFromFoliage = false;
			});
		}

		private void OnDestroy()
		{
			InputManager.RemoveState(actionInputState);
		}
	}
}
