using UnityEngine;

namespace BitCode.Debug.UI
{
	public interface IDisplayableMetric
	{
		Color DisplayColor { get; }

		new string ToString();
	}
}
