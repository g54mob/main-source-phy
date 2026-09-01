using UnityEngine;

namespace MoreMountains.Feedbacks
{
	public class MMFVectorAttribute : PropertyAttribute
	{
		public readonly string[] Labels;

		public MMFVectorAttribute(params string[] labels)
		{
		}
	}
}
