using UnityEngine;

namespace BitCode.UI
{
	public interface IRadialMenuItem<in TData>
	{
		Transform transform { get; }

		void Select();

		void UpdateData(TData data);

		void SetAlpha(float alpha);
	}
}
