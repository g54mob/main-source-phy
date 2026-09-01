using System.Collections.Generic;
using UnityEngine;

namespace BitCode
{
	public class PrefabFactory : MonoBehaviour
	{
		[SerializeField]
		protected List<GameObject> prefabs;

		protected static Dictionary<string, GameObject> createdObjects;

		protected virtual void Awake()
		{
			CreatePrefabs();
		}

		protected virtual void CreatePrefabs()
		{
			if (createdObjects == null)
			{
				while (true)
				{
					int num = 953851586;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x3C462E27)) % 3)
						{
						case 2u:
							break;
						case 1u:
							createdObjects = new Dictionary<string, GameObject>();
							num = (int)((num2 * 625213015) ^ 0x3D1F027);
							continue;
						default:
							goto end_IL_0007;
						}
						break;
					}
					continue;
					end_IL_0007:
					break;
				}
			}
			using (List<GameObject>.Enumerator enumerator = prefabs.GetEnumerator())
			{
				while (true)
				{
					IL_008c:
					int num3;
					int num4;
					if (!enumerator.MoveNext())
					{
						num3 = 1744779336;
						num4 = num3;
					}
					else
					{
						num3 = 1828125214;
						num4 = num3;
					}
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num3 ^ 0x3C462E27)) % 4)
						{
						case 0u:
							num3 = 1828125214;
							continue;
						default:
							goto end_IL_0055;
						case 1u:
						{
							GameObject current = enumerator.Current;
							TryCreatePrefab(current);
							num3 = 2126660273;
							continue;
						}
						case 2u:
							break;
						case 3u:
							goto end_IL_0055;
						}
						goto IL_008c;
						continue;
						end_IL_0055:
						break;
					}
					break;
				}
			}
			Object.Destroy(base.gameObject);
		}

		protected virtual void TryCreatePrefab(GameObject prefab)
		{
			if (createdObjects.TryGetValue(prefab.name, out var value))
			{
				goto IL_0014;
			}
			goto IL_0049;
			IL_0014:
			int num = 1202754878;
			goto IL_0019;
			IL_0019:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x32DD57BD)) % 7)
				{
				case 4u:
					break;
				default:
					return;
				case 5u:
					goto IL_0049;
				case 1u:
					createdObjects.Add(prefab.name, value);
					num = (int)(num2 * 2081475149) ^ -530415981;
					continue;
				case 3u:
				{
					int num3;
					int num4;
					if (!(value == null))
					{
						num3 = 247235430;
						num4 = num3;
					}
					else
					{
						num3 = 2109203070;
						num4 = num3;
					}
					num = num3 ^ ((int)num2 * -1031398816);
					continue;
				}
				case 0u:
					createdObjects.Remove(prefab.name);
					num = (int)(num2 * 206178667) ^ -2144552025;
					continue;
				case 2u:
					value = Object.Instantiate(prefab);
					num = (int)((num2 * 1872801054) ^ 0x48B6BF1D);
					continue;
				case 6u:
					return;
				}
				break;
			}
			goto IL_0014;
			IL_0049:
			int num5;
			if (createdObjects.ContainsKey(prefab.name))
			{
				num = 535708711;
				num5 = num;
			}
			else
			{
				num = 2104932339;
				num5 = num;
			}
			goto IL_0019;
		}
	}
}
