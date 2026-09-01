using UnityEngine;
using UnityEngine.UI;

namespace SRF.UI
{
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("SRF/UI/Copy Size Into Layout Element")]
	[ExecuteInEditMode]
	public class CopySizeIntoLayoutElement : LayoutElement
	{
		public RectTransform CopySource;

		public float PaddingHeight;

		public float PaddingWidth;

		public bool SetPreferredSize;

		public bool SetMinimumSize;

		public override float preferredWidth
		{
			get
			{
				if (!SetPreferredSize || CopySource == null || !IsActive())
				{
					return -1f;
				}
				return CopySource.rect.width + PaddingWidth;
			}
		}

		public override float preferredHeight
		{
			get
			{
				if (!SetPreferredSize || CopySource == null || !IsActive())
				{
					return -1f;
				}
				return CopySource.rect.height + PaddingHeight;
			}
		}

		public override float minWidth
		{
			get
			{
				if (!SetMinimumSize || CopySource == null || !IsActive())
				{
					return -1f;
				}
				return CopySource.rect.width + PaddingWidth;
			}
		}

		public override float minHeight
		{
			get
			{
				if (!SetMinimumSize || CopySource == null || !IsActive())
				{
					return -1f;
				}
				return CopySource.rect.height + PaddingHeight;
			}
		}

		public override int layoutPriority
		{
			get
			{
				return 2;
			}
		}
	}
}
