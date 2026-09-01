using System.Reflection;
using BitCode.Attributes;
using BitCode.Debug.MemberWrappers;
using DdQbeCzwvEdCSCHcDJqhScymDgUBA;
using UnityEngine;

namespace BitCode.Debug.Commands
{
	public class DebugQualitySettings
	{
		private static readonly DebugQualitySettings unkCMXdDaHlgFnStRuNbxzrbnMID = new DebugQualitySettings();

		[DebugCommand(Name = "QualitySettings", Description = "Push the QualitySettings context onto the stack.")]
		public static DebugQualitySettings PushQualitySettings()
		{
			return unkCMXdDaHlgFnStRuNbxzrbnMID;
		}

		[DebugCommand(Description = "Gets or sets QualitySettings.shadows.")]
		public IPropertyWrapper Shadows()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(QualitySettings), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "shadows");
		}

		[DebugCommand(Description = "Gets or sets QualitySettings.anisotropicFiltering.")]
		public IPropertyWrapper AnisotropicFiltering()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(QualitySettings), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "anisotropicFiltering");
		}

		[DebugCommand(Description = "Gets or sets QualitySettings.antiAliasing.")]
		public IPropertyWrapper AntiAliasing()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(QualitySettings), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "antiAliasing");
		}

		[DebugCommand(Description = "Gets or sets QualitySettings.skinWeights.")]
		public IPropertyWrapper SkinWeights()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(QualitySettings), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "skinWeights");
		}

		[DebugCommand(Description = "Gets or sets QualitySettings.lodBias.")]
		public IPropertyWrapper LodBias()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(QualitySettings), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "lodBias");
		}

		[DebugCommand(Description = "Gets or sets QualitySettings.shadowCascades.")]
		public IPropertyWrapper ShadowCascades()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(QualitySettings), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "shadowCascades");
		}

		[DebugCommand(Description = "Gets or sets QualitySettings.shadowDistance.")]
		public IPropertyWrapper ShadowDistance()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(QualitySettings), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "shadowDistance");
		}

		[DebugCommand(Description = "Gets or sets QualitySettings.shadowResolution.")]
		public IPropertyWrapper ShadowResolution()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(QualitySettings), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "shadowResolution");
		}

		[DebugCommand(Description = "Gets or sets QualitySettings.shadowProjection.")]
		public IPropertyWrapper ShadowProjection()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(QualitySettings), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "shadowProjection");
		}

		[DebugCommand(Description = "Gets or sets QualitySettings.masterTextureLimit.")]
		public IPropertyWrapper MasterTextureLimit()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(QualitySettings), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "masterTextureLimit");
		}

		[DebugCommand(Description = "Gets or sets QualitySettings.softParticles.")]
		public IPropertyWrapper SoftParticles()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(QualitySettings), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "softParticles");
		}

		[DebugCommand(Description = "Gets or sets QualitySettings.pixelLightCount.")]
		public IPropertyWrapper PixelLightCount()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(QualitySettings), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "pixelLightCount");
		}

		[DebugCommand(Description = "Gets or sets QualitySettings.vSyncCount.")]
		public IPropertyWrapper VSyncCount()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(QualitySettings), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "vSyncCount");
		}
	}
}
