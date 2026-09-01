using BitCode.SceneManagement;
using JetBrains.Annotations;
using UnityEngine;

namespace BitCode.Profiles
{
	public abstract class GameObjectSelectorBase : MonoBehaviour, ISerializationCallbackReceiver, IScenePreprocessBuild, IScenePreprocessEditor
	{
		[SerializeField]
		protected SerializedProfileSelectionStateProvider stateProvider;

		[SerializeField]
		[Tooltip("Controls when to perform selection.")]
		protected SelectorOperationMoment operationMoment;

		[SerializeField]
		[HideInInspector]
		protected GameObject buildTimeSelectedObject;

		public GameObject SelectedGameObject { get; protected set; }

		protected virtual void Awake()
		{
			SelectIf(SelectorOperationMoment.Awake, Application.platform);
		}

		protected virtual void Start()
		{
			SelectIf(SelectorOperationMoment.Start, Application.platform);
		}

		protected virtual void OnEnable()
		{
			SelectIf(SelectorOperationMoment.Enable, Application.platform);
			GetStateProvider().StateChanged += OnStateChanged;
		}

		protected void OnDisable()
		{
			GetStateProvider().StateChanged -= OnStateChanged;
		}

		public void ProcessForBuild(RuntimePlatform platform)
		{
			SelectBeforeSceneLoad(platform);
		}

		public void ProcessForEditor()
		{
			SelectBeforeSceneLoad(Application.platform);
		}

		public abstract bool Select(IProfileSelectionState newState);

		protected abstract void OnStateChanged(IProfileSelectionState state);

		[NotNull]
		protected virtual IProfileSelectionStateProvider GetStateProvider()
		{
			return stateProvider.Value;
		}

		[NotNull]
		protected virtual IProfileSelectionState GetCurrentProfileState(RuntimePlatform platform)
		{
			return GetStateProvider().GetState(platform);
		}

		protected virtual void SelectIf(SelectorOperationMoment moment, RuntimePlatform platform)
		{
			if (operationMoment != moment)
			{
				goto IL_0009;
			}
			goto IL_004a;
			IL_0009:
			int num = 1429499327;
			goto IL_000e;
			IL_000e:
			IProfileSelectionState currentProfileState = default(IProfileSelectionState);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x539896AC)) % 5)
				{
				case 2u:
					break;
				default:
					return;
				case 1u:
					Select(currentProfileState);
					num = ((int)num2 * -57609113) ^ -87448482;
					continue;
				case 4u:
					goto IL_004a;
				case 3u:
					return;
				case 0u:
					return;
				}
				break;
			}
			goto IL_0009;
			IL_004a:
			currentProfileState = GetCurrentProfileState(platform);
			num = 1093556759;
			goto IL_000e;
		}

		protected virtual void SelectBeforeSceneLoad(RuntimePlatform platform)
		{
			SelectIf(SelectorOperationMoment.Build, platform);
			if (operationMoment == SelectorOperationMoment.Build)
			{
				goto IL_0011;
			}
			goto IL_006b;
			IL_0011:
			int num = -328517040;
			goto IL_0016;
			IL_0016:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -1638715345)) % 5)
				{
				case 0u:
					break;
				default:
					return;
				case 2u:
					Debug.Log($"Spawner {base.name} chose {SelectedGameObject} at scene load time for {platform}.");
					num = (int)((num2 * 1285429486) ^ 0x6FA8AB06);
					continue;
				case 1u:
					goto IL_006b;
				case 3u:
					buildTimeSelectedObject = SelectedGameObject;
					return;
				case 4u:
					return;
				}
				break;
			}
			goto IL_0011;
			IL_006b:
			buildTimeSelectedObject = null;
			num = -295224939;
			goto IL_0016;
		}

		protected virtual void OnValidate()
		{
			if (!Application.isEditor)
			{
				goto IL_000a;
			}
			goto IL_009b;
			IL_000a:
			int num = 153412768;
			goto IL_000f;
			IL_000f:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x27B4D81B)) % 6)
				{
				case 2u:
					break;
				default:
					return;
				case 4u:
				{
					int num3;
					int num4;
					if (stateProvider.HasValue)
					{
						num3 = -211218146;
						num4 = num3;
					}
					else
					{
						num3 = -1329751466;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 1565380601);
					continue;
				}
				case 1u:
					Debug.LogError("No state provider is set on " + base.name + ", but it is required when not in manual mode.", this);
					num = (int)(num2 * 593112148) ^ -1252842698;
					continue;
				case 5u:
					return;
				case 0u:
					goto IL_009b;
				case 3u:
					return;
				}
				break;
			}
			goto IL_000a;
			IL_009b:
			int num5;
			if (operationMoment == SelectorOperationMoment.Manual)
			{
				num = 1623135778;
				num5 = num;
			}
			else
			{
				num = 1425756167;
				num5 = num;
			}
			goto IL_000f;
		}

		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
			if (operationMoment != SelectorOperationMoment.Build)
			{
				return;
			}
			while (true)
			{
				int num = -1538615114;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -104960791)) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 1u:
						goto IL_002b;
					case 2u:
						return;
					}
					break;
					IL_002b:
					SelectedGameObject = buildTimeSelectedObject;
					num = ((int)num2 * -1756162096) ^ 0x5C76EA5F;
				}
			}
		}
	}
}
