using UnityEngine;

namespace CodeAnimo.GPGPU
{
	public abstract class Kernel : MonoBehaviour
	{
		public abstract void Dispatch();

		public abstract void SetTexture(string textureName, Texture simTexture);

		public abstract void SetFloat(string floatName, float floatValue);

		public abstract void SetInt(string intName, int intValue);

		public virtual bool SupportedBySystem()
		{
			return true;
		}

		public static Kernel FindCompatibleKernelOnGameObject(GameObject target)
		{
			Kernel component = target.GetComponent<Kernel>();
			if (component != null && component.SupportedBySystem())
			{
				return component;
			}
			Kernel[] components = target.GetComponents<Kernel>();
			for (int i = 0; i < components.Length; i++)
			{
				component = components[i];
				if (component.SupportedBySystem())
				{
					return component;
				}
			}
			throw new MissingComponentException("No supported kernel found on this GameObject.");
		}
	}
}
