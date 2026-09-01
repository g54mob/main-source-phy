using CodeAnimo.GPGPU;
using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	[AddComponentMenu("Surface Waves/Wave Sources/Mouse Wave Source")]
	public class MouseSource : WaveSource
	{
		public MouseHitFinder playerMouseTracker;

		public MouseButton mouseButtonId;

		public override void RunStep()
		{
			if (Input.GetMouseButton((int)mouseButtonId) && playerMouseTracker.MouseHitSomething())
			{
				forceUnchangedOutput = false;
				base.transform.position = playerMouseTracker.targetData.point;
			}
			else
			{
				forceUnchangedOutput = true;
			}
			base.RunStep();
		}

		protected override void Reset()
		{
			base.gameObject.layer = WaveSource.defaultWaveInputLayer;
			base.Reset();
		}

		protected override void AddMissingComponents()
		{
			MouseHitFinder mouseHitFinder = AddComponentIfMissingAndSetup<MouseHitFinder>();
			if (mouseHitFinder != null)
			{
				mouseHitFinder.UserCamera = Camera.main;
				mouseHitFinder.activeLayers = (int)mouseHitFinder.activeLayers - (1 << (WaveSource.defaultWaveInputLayer & 0x1F));
				playerMouseTracker = mouseHitFinder;
			}
			base.AddMissingComponents();
			AddComponentIfMissingAndSetup<SM3Kernel>();
			AddComponentIfMissingAndSetup<Rigidbody>();
			AddComponentIfMissingAndSetup<SphereCollider>();
		}
	}
}
