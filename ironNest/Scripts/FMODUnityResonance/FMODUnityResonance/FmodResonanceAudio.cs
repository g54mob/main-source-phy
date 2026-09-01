using System;
using System.Collections.Generic;
using FMOD;
using UnityEngine;

namespace FMODUnityResonance
{
	public static class FmodResonanceAudio
	{
		private struct RoomProperties
		{
			public float PositionX;

			public float PositionY;

			public float PositionZ;

			public float RotationX;

			public float RotationY;

			public float RotationZ;

			public float RotationW;

			public float DimensionsX;

			public float DimensionsY;

			public float DimensionsZ;

			public FmodResonanceAudioRoom.SurfaceMaterial MaterialLeft;

			public FmodResonanceAudioRoom.SurfaceMaterial MaterialRight;

			public FmodResonanceAudioRoom.SurfaceMaterial MaterialBottom;

			public FmodResonanceAudioRoom.SurfaceMaterial MaterialTop;

			public FmodResonanceAudioRoom.SurfaceMaterial MaterialFront;

			public FmodResonanceAudioRoom.SurfaceMaterial MaterialBack;

			public float ReflectionScalar;

			public float ReverbGain;

			public float ReverbTime;

			public float ReverbBrightness;
		}

		public const float MaxGainDb = 24f;

		public const float MinGainDb = -24f;

		public const float MaxReverbBrightness = 1f;

		public const float MinReverbBrightness = -1f;

		public const float MaxReverbTime = 3f;

		public const float MaxReflectivity = 2f;

		private static readonly Matrix4x4 flipZ;

		private static readonly string listenerPluginName;

		private static readonly int roomPropertiesSize;

		private static readonly int roomPropertiesIndex;

		private static Bounds bounds;

		private static List<FmodResonanceAudioRoom> enabledRooms;

		private static VECTOR listenerPositionFmod;

		private static DSP listenerPlugin;

		private static DSP ListenerPlugin => default(DSP);

		public static void UpdateAudioRoom(FmodResonanceAudioRoom room, bool roomEnabled)
		{
		}

		public static bool IsListenerInsideRoom(FmodResonanceAudioRoom room)
		{
			return false;
		}

		private static float ConvertAmplitudeFromDb(float db)
		{
			return 0f;
		}

		private static void ConvertAudioTransformFromUnity(ref Vector3 position, ref Quaternion rotation)
		{
		}

		private static byte[] GetBytes(IntPtr ptr, int length)
		{
			return null;
		}

		private static RoomProperties GetRoomProperties(FmodResonanceAudioRoom room)
		{
			return default(RoomProperties);
		}

		private static DSP Initialize()
		{
			return default(DSP);
		}
	}
}
