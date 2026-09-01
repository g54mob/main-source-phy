using System.Runtime.CompilerServices;
using UnityEngine;

namespace VLB
{
	public abstract class VolumetricLightBeamAbstractBase : MonoBehaviour
	{
		public delegate void BeamGeometryGeneratedHandler(VolumetricLightBeamAbstractBase beam);

		public enum AttachedLightType
		{
			NoLight = 0,
			OtherLight = 1,
			SpotLight = 2
		}

		public const string ClassName = "VolumetricLightBeamAbstractBase";

		[SerializeField]
		protected int pluginVersion;

		protected Light m_CachedLightSpot;

		public bool hasGeometry => false;

		public Bounds bounds => default(Bounds);

		public int _INTERNAL_pluginVersion => 0;

		public Light lightSpotAttached => null;

		private event BeamGeometryGeneratedHandler BeamGeometryGeneratedEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public abstract BeamGeometryAbstractBase GetBeamGeometry();

		protected abstract void SetBeamGeometryNull();

		public void RegisterBeamGeometryGeneratedCallback(BeamGeometryGeneratedHandler callback)
		{
		}

		public virtual void GenerateGeometry()
		{
		}

		public abstract bool IsScalable();

		public abstract Vector3 GetLossyScale();

		public virtual void CopyPropsFrom(VolumetricLightBeamAbstractBase beamSrc, BeamProps beamProps)
		{
		}

		public Light GetLightSpotAttachedSlow(out AttachedLightType lightType)
		{
			lightType = default(AttachedLightType);
			return null;
		}

		protected void InitLightSpotAttachedCached()
		{
		}

		private void OnDestroy()
		{
		}

		protected void DestroyBeam()
		{
		}
	}
}
