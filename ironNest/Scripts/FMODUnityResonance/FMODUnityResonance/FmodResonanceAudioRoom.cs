using UnityEngine;
using UnityEngine.Serialization;

namespace FMODUnityResonance
{
	[AddComponentMenu("ResonanceAudio/FmodResonanceAudioRoom")]
	public class FmodResonanceAudioRoom : MonoBehaviour
	{
		public enum SurfaceMaterial
		{
			Transparent = 0,
			AcousticCeilingTiles = 1,
			BrickBare = 2,
			BrickPainted = 3,
			ConcreteBlockCoarse = 4,
			ConcreteBlockPainted = 5,
			CurtainHeavy = 6,
			FiberglassInsulation = 7,
			GlassThin = 8,
			GlassThick = 9,
			Grass = 10,
			LinoleumOnConcrete = 11,
			Marble = 12,
			Metal = 13,
			ParquetOnConcrete = 14,
			PlasterRough = 15,
			PlasterSmooth = 16,
			PlywoodPanel = 17,
			PolishedConcreteOrTile = 18,
			Sheetrock = 19,
			WaterOrIceSurface = 20,
			WoodCeiling = 21,
			WoodPanel = 22
		}

		[FormerlySerializedAs("leftWall")]
		public SurfaceMaterial LeftWall;

		[FormerlySerializedAs("rightWall")]
		public SurfaceMaterial RightWall;

		[FormerlySerializedAs("floor")]
		public SurfaceMaterial Floor;

		[FormerlySerializedAs("ceiling")]
		public SurfaceMaterial Ceiling;

		[FormerlySerializedAs("backWall")]
		public SurfaceMaterial BackWall;

		[FormerlySerializedAs("frontWall")]
		public SurfaceMaterial FrontWall;

		[FormerlySerializedAs("reflectivity")]
		public float Reflectivity;

		[FormerlySerializedAs("reverbGainDb")]
		public float ReverbGainDb;

		[FormerlySerializedAs("reverbBrightness")]
		public float ReverbBrightness;

		[FormerlySerializedAs("reverbTime")]
		public float ReverbTime;

		[FormerlySerializedAs("size")]
		public Vector3 Size;

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void Update()
		{
		}

		private void OnDrawGizmosSelected()
		{
		}
	}
}
