using UnityEngine;

namespace BitCode
{
	public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
	{
		public static T Instance { get; protected set; }

		public static bool InstanceExists => Instance != null;

		public static bool TryGetInstance(out T result)
		{
			result = Instance;
			return result != null;
		}

		protected virtual void Awake()
		{
			if (Instance != null)
			{
				while (true)
				{
					int num = -192193246;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -183276521)) % 4)
						{
						case 3u:
							break;
						case 1u:
							Debug.LogWarningFormat("Trying to create a second instance of {0}", typeof(T));
							num = (int)((num2 * 523670859) ^ 0xDD7649E);
							continue;
						case 2u:
							Object.Destroy(base.gameObject);
							return;
						default:
							goto end_IL_0012;
						}
						break;
					}
					continue;
					end_IL_0012:
					break;
				}
			}
			Instance = (T)this;
		}

		protected virtual void OnDestroy()
		{
			if (!(Instance == this))
			{
				return;
			}
			T instance = default(T);
			while (true)
			{
				int num = 609710594;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x278224EF)) % 4)
					{
					case 3u:
						break;
					default:
						return;
					case 1u:
						instance = null;
						num = ((int)num2 * -848411201) ^ 0x7F511E2;
						continue;
					case 2u:
						Instance = instance;
						num = (int)(num2 * 1577301768) ^ -1089153429;
						continue;
					case 0u:
						return;
					}
					break;
				}
			}
		}
	}
}
