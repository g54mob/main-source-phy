using System.Text;
using Meta.XR.Util;
using UnityEngine;

[Feature(Feature.TrackedKeyboard)]
public static class OVRKeyboard
{
	public struct TrackedKeyboardState
	{
		public bool isPositionValid;

		public bool isPositionTracked;

		public bool isOrientationValid;

		public bool isOrientationTracked;

		public Vector3 position;

		public Quaternion rotation;

		public double timeInSeconds;
	}

	public struct TrackedKeyboardInfo
	{
		public string Name;

		public ulong Identifier;

		public Vector3 Dimensions;

		public OVRPlugin.TrackedKeyboardFlags KeyboardFlags;

		public OVRPlugin.TrackedKeyboardPresentationStyles SupportedPresentationStyles;
	}

	public static TrackedKeyboardState GetKeyboardState()
	{
		OVRPlugin.GetKeyboardState(OVRPlugin.Step.Render, out var keyboardState);
		TrackedKeyboardState result = default(TrackedKeyboardState);
		result.timeInSeconds = keyboardState.PoseState.Time;
		OVRPose oVRPose = keyboardState.PoseState.Pose.ToOVRPose();
		result.position = oVRPose.position;
		result.rotation = oVRPose.orientation;
		result.isPositionValid = keyboardState.PositionValid == OVRPlugin.Bool.True;
		result.isPositionTracked = keyboardState.PositionTracked == OVRPlugin.Bool.True;
		result.isOrientationValid = keyboardState.OrientationValid == OVRPlugin.Bool.True;
		result.isOrientationTracked = keyboardState.OrientationTracked == OVRPlugin.Bool.True;
		return result;
	}

	public static bool GetSystemKeyboardInfo(OVRPlugin.TrackedKeyboardQueryFlags keyboardQueryFlags, out TrackedKeyboardInfo keyboardInfo)
	{
		keyboardInfo = default(TrackedKeyboardInfo);
		if (OVRPlugin.GetSystemKeyboardDescription(keyboardQueryFlags, out var keyboardDescription))
		{
			keyboardInfo.Name = Encoding.UTF8.GetString(keyboardDescription.Name).TrimEnd('\0');
			keyboardInfo.Identifier = keyboardDescription.TrackedKeyboardId;
			keyboardInfo.Dimensions = new Vector3(keyboardDescription.Dimensions.x, keyboardDescription.Dimensions.y, keyboardDescription.Dimensions.z);
			keyboardInfo.KeyboardFlags = keyboardDescription.KeyboardFlags;
			keyboardInfo.SupportedPresentationStyles = keyboardDescription.SupportedPresentationStyles;
			return true;
		}
		return false;
	}

	public static bool StopKeyboardTracking(TrackedKeyboardInfo keyboardInfo)
	{
		return OVRPlugin.StopKeyboardTracking();
	}
}
