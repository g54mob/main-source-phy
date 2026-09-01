using UnityEngine;

namespace Selectors
{
	public class ValueHolderCentering : ValueHolder
	{
		public Renderer icon;

		public Transform pivot;

		public bool iconOnLeft = true;

		protected override void Start()
		{
			base.Start();
			base.TextInput += Recenter;
		}

		private void Recenter(string _)
		{
			float num;
			float num2;
			if (iconOnLeft)
			{
				num = icon.bounds.min.x;
				num2 = text.transform.position.x + text.bounds.max.x;
			}
			else
			{
				num2 = icon.bounds.max.x;
				num = text.transform.position.x + text.bounds.min.x;
			}
			float num3 = (num + num2) / 2f;
			float num4 = pivot.position.x - num3;
			icon.transform.position += Vector3.right * num4;
			text.transform.position += Vector3.right * num4;
		}
	}
}
