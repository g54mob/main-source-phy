using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class TMP_TextWithSoftness : TextMeshProUGUI
	{
		private RectMask2D m_rectMask;

		public override Material materialForRendering
		{
			get
			{
				if (m_rectMask == null)
				{
					return base.materialForRendering;
				}
				Material obj = new Material(base.materialForRendering);
				obj.SetFloat("_MaskSoftnessX", m_rectMask.softness.x);
				obj.SetFloat("_MaskSoftnessY", m_rectMask.softness.y);
				return obj;
			}
		}

		protected override void Start()
		{
			base.Start();
			Transform parent = base.transform.parent;
			while (parent != null)
			{
				m_rectMask = parent.GetComponent<RectMask2D>();
				if (!(m_rectMask != null))
				{
					parent = parent.parent;
					continue;
				}
				break;
			}
		}
	}
}
