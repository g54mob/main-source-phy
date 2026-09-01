using UnityEngine;

namespace Riten.Native.Cursors
{
	[CreateAssetMenu(fileName = "AnimatedVirtualCursor", menuName = "Native Cursor/Animated Virtual Cursor")]
	public class AnimatedVirtualCursor : VirtualCursorBase
	{
		[Tooltip("Frames per second")]
		public int fps;

		[field: SerializeField]
		public override VirtualCursor[] frames { get; set; }

		public override int framesPerSecond
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public override Vector2 hotspot
		{
			get
			{
				return default(Vector2);
			}
			set
			{
			}
		}

		public override bool isMask
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public override Color32 backgroundColor
		{
			get
			{
				return default(Color32);
			}
			set
			{
			}
		}

		public override Color32 foregroundColor
		{
			get
			{
				return default(Color32);
			}
			set
			{
			}
		}

		public override bool isAnimated
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public override Texture2D texture
		{
			get
			{
				return null;
			}
			set
			{
			}
		}
	}
}
