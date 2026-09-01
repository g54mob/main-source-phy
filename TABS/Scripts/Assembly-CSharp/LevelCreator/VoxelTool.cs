using Landfall.TABS_Input;
using UnityEngine;

namespace LevelCreator
{
	public class VoxelTool : MonoBehaviour
	{
		private DMEditor dmEditor;

		private Volume volume;

		public GameObject voxel;

		private bool isVoxelAddDown;

		private bool isVoxelRemoveDown;

		private InputState inputState = new InputState("VoxelToolInputState");

		private void Awake()
		{
			PlayerActions instance = PlayerActions.Instance;
			inputState.ClearAllEvents();
			inputState.AddOnKeyDownListener(instance.m_toolPrimary, delegate
			{
				isVoxelAddDown = true;
			});
			inputState.AddOnKeyUpListener(instance.m_toolPrimary, delegate
			{
				isVoxelAddDown = false;
			});
			inputState.AddOnKeyDownListener(instance.m_toolSecondary, delegate
			{
				isVoxelRemoveDown = true;
			});
			inputState.AddOnKeyUpListener(instance.m_toolSecondary, delegate
			{
				isVoxelRemoveDown = false;
			});
			inputState.AddOnStateLoseFocusListener(delegate
			{
				isVoxelAddDown = false;
				isVoxelRemoveDown = false;
			});
			InputManager.PushState(inputState);
		}

		private void OnDestroy()
		{
			InputManager.RemoveState(inputState);
		}

		private void Start()
		{
			dmEditor = DMEditor.Instance;
			dmEditor.HideCursor();
			volume = dmEditor.VolumeRootObject;
		}

		private Vector3 GetVoxelPosition()
		{
			Vector3 targetPositionOnVolume = Utility.GetTargetPositionOnVolume(base.transform.position, dmEditor.playerCamera.transform.forward, 5f);
			for (int i = 0; i < 3; i++)
			{
				targetPositionOnVolume[i] = Mathf.Round(targetPositionOnVolume[i]);
			}
			return targetPositionOnVolume;
		}

		private void Update()
		{
			Vector3 voxelPosition = GetVoxelPosition();
			voxel.transform.position = voxelPosition;
			voxel.transform.rotation = Quaternion.identity;
			bool flag = false;
			if (isVoxelAddDown && volume.Get(voxelPosition) != 1f)
			{
				volume.Set(voxelPosition, 1f);
				flag = true;
			}
			if (isVoxelRemoveDown && volume.Get(voxelPosition) != 0f)
			{
				volume.Set(voxelPosition, 0f);
				flag = true;
			}
			if (flag)
			{
				dmEditor.ScheduleTakeLevelSnapshot();
			}
		}
	}
}
