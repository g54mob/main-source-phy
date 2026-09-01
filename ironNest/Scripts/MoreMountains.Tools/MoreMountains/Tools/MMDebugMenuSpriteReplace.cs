using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	public class MMDebugMenuSpriteReplace : MonoBehaviour
	{
		public Sprite OnSprite;

		public Sprite OffSprite;

		public bool StartsOn;

		protected Image _image;

		protected MMTouchButton _mmTouchButton;

		public virtual bool CurrentValue => false;

		protected virtual void Awake()
		{
		}

		public virtual void Initialization()
		{
		}

		public virtual void Swap()
		{
		}

		public virtual void SwitchToOffSprite()
		{
		}

		protected virtual void SpriteOff()
		{
		}

		public virtual void SwitchToOnSprite()
		{
		}

		protected virtual void SpriteOn()
		{
		}
	}
}
