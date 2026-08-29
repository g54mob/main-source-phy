using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Pine/CustomMode")]
public class CustomMode : MonoBehaviour
{
	public bool allowMove = true;

	public bool allowLook = true;

	public bool allowExit = true;

	public bool allowCursor;

	public bool overrideCamera;

	public bool instantCameraMove;

	public float cameraFOV = 70f;

	public List<GameObject> toDisableWhenInMode;

	public List<GameObject> toEnableWhenInMode;

	public bool isUICustomMode;

	public LayerMask customCameraCullingMask;

	public Vector3 eyePosition => base.transform.position;

	public Quaternion eyeRotation => base.transform.rotation;
}
