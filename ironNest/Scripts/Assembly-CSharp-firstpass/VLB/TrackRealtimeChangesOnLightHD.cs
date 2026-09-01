using UnityEngine;

namespace VLB
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Light), typeof(VolumetricLightBeamHD))]
	[HelpURL("http://saladgamer.com/vlb-doc/comp-trackrealtimechanges-hd/")]
	[AddComponentMenu("VLB/HD/Track Realtime Changes On Light")]
	public class TrackRealtimeChangesOnLightHD : MonoBehaviour
	{
		public const string ClassName = "TrackRealtimeChangesOnLightHD";

		private VolumetricLightBeamHD m_Master;

		private void Awake()
		{
		}

		private void Update()
		{
		}
	}
}
