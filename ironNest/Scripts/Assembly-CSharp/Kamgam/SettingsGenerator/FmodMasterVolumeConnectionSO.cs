using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "FmodMasterVolumeConnection", menuName = "SettingsGenerator/Connection/FMOD/MasterVolumeConnection", order = 4)]
	public class FmodMasterVolumeConnectionSO : FloatConnectionSO
	{
		[Tooltip("Input Range (Setting/UI)\n- How the incoming setting value should be interpreted.\n- Most volume sliders use 0..100 (percent).\nBehavior:\n- The incoming value is normalized within this range and mapped into Output Linear Range.\nFormat Rules:\n- X must be <= Y (if not, values will be swapped at runtime).\nSafe Examples:\n- (0, 100) for a percent-based slider.\nNotes:\n- Ensure your UI slider uses the same range to avoid confusing results.")]
		public Vector2 InputRange;

		[Tooltip("Output Linear Range (FMOD)\n- The linear volume range applied to the FMOD bus.\n- 0.0 = mute, 1.0 = unity gain (100%).\nBehavior & Safety:\n- This range is NOT capped. Values greater than 1.0 can clip if your mix has limited headroom.\nFormat Rules:\n- X must be <= Y (if not, values will be swapped at runtime).\nSafe Examples:\n- (0, 2) to allow 200%.\n- (0, 4) to allow 400% (not recommended unless you know your headroom).\nNotes:\n- If you want a classic '0..100% only' volume slider, set this to (0, 1).")]
		public Vector2 OutputLinearRange;

		[Tooltip("FMOD Bus Path\n- The FMOD bus address to control.\n- Default is 'bus:/' (Master Bus).\nTokens/Codes:\n- (none) No tokens are supported. This must be a literal FMOD bus path.\nFormat Rules:\n- Must match an existing bus path from your FMOD Studio project.\n- Usually starts with 'bus:/'.\nSafe Examples:\n- bus:/\n- bus:/SFX\nNotes:\n- If you later add separate sliders for Music/SFX, create more connection assets with different paths.")]
		public string BusPath;

		protected FmodMasterVolumeConnection _connection;

		public override IConnection<float> GetConnection()
		{
			return null;
		}

		public void Create()
		{
		}

		public override void DestroyConnection()
		{
		}
	}
}
