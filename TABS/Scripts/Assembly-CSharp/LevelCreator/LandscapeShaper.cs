using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Landfall.TABS_Input;
using NaughtyAttributes;
using UnityEngine;

namespace LevelCreator
{
	public class LandscapeShaper : Tool
	{
		private enum LandscapeEditMode
		{
			None = 0,
			Add = 1,
			Remove = 2,
			Blur = 3
		}

		private enum BrushGenerationStatus
		{
			idle = 0,
			generatingBrush = 1,
			done = 2
		}

		[SerializeField]
		private Material m_traceMaterial;

		[SerializeField]
		private Material m_thumbnailMaterial;

		[SerializeField]
		private Transform m_gunOrigin;

		[SerializeField]
		private GameObject m_voxel;

		[SerializeField]
		private ParticleSystem m_landscapeEffect;

		[SerializeField]
		private ParticleSystem m_spadeRibbonFireEffect;

		[SerializeField]
		private ParticleSystem m_armFireEffect;

		[SerializeField]
		private Color m_pathColor = Color.white;

		public static MeshFilter previewMesh;

		[SerializeField]
		private Hotbar m_hotbarPrefab;

		private static Hotbar hotbar;

		[SerializeField]
		private int m_rotationStepSize = 15;

		[SerializeField]
		[BoxGroup("Sound")]
		private ContinousSoundData m_addSound;

		private PlayContinousSound m_addSoundObject;

		[SerializeField]
		[BoxGroup("Sound")]
		private ContinousSoundData m_removeSound;

		private PlayContinousSound m_removeSoundObject;

		[SerializeField]
		[BoxGroup("Sound")]
		private ContinousSoundData m_blurSound;

		private PlayContinousSound m_blurSoundObject;

		private static List<Vector3> localPath = new List<Vector3>();

		private bool m_editing;

		private LandscapeEditMode m_editMode;

		private LandscapeEditMode m_previousEditMode;

		private bool m_editModeChanged;

		private bool m_lockY;

		private bool m_lockDistance;

		private bool m_mirrorX;

		private static Vector3 lockPosition;

		private MeshRenderer m_spadeMeshRenderer;

		private static BrushEntry currentBrushEntry;

		private static BrushInfo currentBrushInfo;

		private static Brush currentBrush;

		private static MeshData currentMeshData;

		private static BrushEntry newBrushEntry;

		private static BrushInfo newBrushInfo = new BrushInfo
		{
			mScaleSetting = StrengthSetting.medium,
			mRoughnessSetting = StrengthSetting.medium,
			mYawAngle = 0f
		};

		private static BrushGenerationStatus mBrushGenerationStatus = BrushGenerationStatus.idle;

		private static StrengthSetting mStrengthSetting = StrengthSetting.medium;

		private const float transitionTime = 0.5f;

		private float lineWidth = 0.1f;

		private void AssertionCheck()
		{
		}

		private float CalculateCurrentStrength()
		{
			return Mathf.Pow(0.5f, 1f + (1f - Utility.FromStrengthValue(mStrengthSetting)) * 4f);
		}

		private Sprite CreateBrushIcon(Brush brush)
		{
			Mesh mesh = VolumeBrushes.GenerateBrushPreview(brush);
			GameObject obj = new GameObject();
			obj.AddComponent<MeshFilter>().mesh = mesh;
			obj.AddComponent<MeshRenderer>().material = m_thumbnailMaterial;
			RuntimePreviewGenerator.BackgroundColor = Color.clear;
			RuntimePreviewGenerator.MarkTextureNonReadable = false;
			Texture2D texture = RuntimePreviewGenerator.GenerateModelPreview(obj.transform);
			UnityEngine.Object.Destroy(obj);
			return Sprite.Create(texture, new Rect(0f, 0f, 64f, 64f), new Vector2(0.5f, 0.5f));
		}

		protected override void Start()
		{
			AssertionCheck();
			base.Start();
			previewMesh = base.transform.GetChild(1).GetComponentInChildren<MeshFilter>();
			m_landscapeEffect.transform.SetParent(null);
			m_spadeMeshRenderer = m_gunOrigin.GetComponentInChildren<MeshRenderer>();
			SetExtrusionAmount(0.0015f, forceUpdate: true);
			SetLineWidth(0.08f, forceUpdate: true);
			SetSpadeSpinAndPlayFireEffects(0f, forceUpdate: true);
			List<BrushEntry> brushEntries;
			if (hotbar == null)
			{
				hotbar = UnityEngine.Object.Instantiate(m_hotbarPrefab, DMEditor.Instance.toolBar.transform);
				hotbar.gameObject.name = "Hotbar_Brushes";
				brushEntries = new List<BrushEntry>();
				addSDFBrush(new Vector3Int(8, 8, 8), new Vector3(1.42f, 1.42f, 1.42f), cube);
				addSDFBrush(new Vector3Int(8, 8, 8), new Vector3(1.42f, 2.84f, 1.42f), cube);
				addSDFBrush(new Vector3Int(8, 8, 8), new Vector3(1f, 1f, 1f), sphere);
				addSDFBrush(new Vector3Int(8, 8, 8), new Vector3(1f, 2f, 1f), sphere);
				addSDFBrush(new Vector3Int(8, 8, 8), new Vector3(1f, 4f, 1f), sphere);
				addSDFBrush(new Vector3Int(8, 8, 8), new Vector3(1f, 1f, 1f), cylinder);
				addSDFBrush(new Vector3Int(8, 8, 8), new Vector3(1f, 1f, 1f), vertical_capsule);
				addSDFBrush(new Vector3Int(8, 8, 8), new Vector3(2f, 1f, 2f), capped_cone);
				addSDFBrush(new Vector3Int(8, 8, 8), new Vector3(1f, 1f, 1f), capped_cone);
				addSDFBrush(new Vector3Int(8, 8, 8), new Vector3(1f, 2f, 1f), capped_cone);
				addSDFBrush(new Vector3Int(8, 8, 8), new Vector3(1.25f, 1.25f, 1.25f), torus);
				addSDFBrush(new Vector3Int(8, 8, 8), new Vector3(1.25f, 1.25f, 1.25f), staircase);
				addSDFBrush(new Vector3Int(8, 8, 8), new Vector3(1.3f, 0.7f, 1.3f), vertical_capsule);
				List<HotbarItem> list = new List<HotbarItem>();
				foreach (BrushEntry brushEntry in brushEntries)
				{
					BrushInfo arg = new BrushInfo
					{
						mScaleSetting = StrengthSetting.medium,
						mRoughnessSetting = StrengthSetting.lowest,
						mYawAngle = 0f
					};
					Brush brush = brushEntry.mCreateBrush(brushEntry.mStandardSize, arg);
					list.Add(new HotbarItem
					{
						icon = CreateBrushIcon(brush),
						callback = delegate
						{
							SetBrushEntry(brushEntry);
						}
					});
				}
				hotbar.invokeOnCycle = true;
				hotbar.SetData(list, 0);
				SetBrushEntry(brushEntries[0]);
			}
			hotbar.EnableHotbar(m_inputState);
			void addBrush(Func<Vector3Int, BrushInfo, Brush> createBrush, Vector3Int standardSize)
			{
				brushEntries.Add(new BrushEntry
				{
					mCreateBrush = createBrush,
					mStandardSize = standardSize
				});
			}
			void addSDFBrush(Vector3Int standardSize, Vector3 scale, Func<Vector3, float> sdfFunc)
			{
				addBrush(delegate(Vector3Int normalSize, BrushInfo brushInfo)
				{
					float num = brushInfo.CalculateCurrentScale();
					return VolumeBrushes.CreateSDFBrush(Vector3Int.CeilToInt(new Vector3(num * (float)normalSize.x, num * (float)normalSize.y, num * (float)normalSize.z)), Utility.FromStrengthValue(brushInfo.mRoughnessSetting) * 0.2f, 0f, scale, Quaternion.Euler(0f, 0f, 0f - brushInfo.mRollAngle) * Quaternion.Euler(0f, brushInfo.mYawAngle, 0f), sdfFunc);
				}, standardSize);
			}
			float capped_cone(Vector3 pos)
			{
				Vector2 lhs = new Vector2(new Vector2(pos.x, pos.z).magnitude, Mathf.Abs(pos.y + 0.5f)) - new Vector2(1f, 1f);
				return Mathf.Min(Mathf.Max(lhs.x, lhs.y), 0f) + Vector2.Max(lhs, Vector2.zero).magnitude + 1f * (1f - 0.5f * (pos.y + 0.5f + 1f));
			}
			float cube(Vector3 pos)
			{
				Vector3 lhs = new Vector3(Mathf.Abs(pos.x) - 1f, Mathf.Abs(pos.y) - 1f, Mathf.Abs(pos.z) - 1f);
				float a = Mathf.Max(lhs.x, Mathf.Max(lhs.y, lhs.z));
				return Vector3.Max(lhs, Vector3.zero).magnitude + Mathf.Min(a, 0f);
			}
			float cylinder(Vector3 pos)
			{
				Vector2 lhs = new Vector2(new Vector2(pos.x, pos.z).magnitude, Mathf.Abs(pos.y)) - new Vector2(1f, 1f);
				return Mathf.Min(Mathf.Max(lhs.x, lhs.y), 0f) + Vector2.Max(lhs, Vector2.zero).magnitude;
			}
			float sphere(Vector3 pos)
			{
				return pos.magnitude - 1f;
			}
			float staircase(Vector3 pos)
			{
				float num = float.PositiveInfinity;
				for (float num2 = 0f; num2 < 4f; num2 += 1f)
				{
					Vector3 vector = pos + new Vector3(0f, 0.75f - num2 / 4f, num2 / 4f * 2f - 0.75f);
					Vector3 lhs = new Vector3(Mathf.Abs(vector.x) - 1f, Mathf.Abs(vector.y) - (num2 + 1f) / 4f, Mathf.Abs(vector.z) - 0.25f);
					float a = Mathf.Max(lhs.x, Mathf.Max(lhs.y, lhs.z));
					num = Mathf.Min(Vector3.Max(lhs, Vector3.zero).magnitude + Mathf.Min(a, 0f), num);
				}
				if (!(num < 0f))
				{
					return num;
				}
				return num - 0.5f;
			}
			float torus(Vector3 pos)
			{
				Vector2 vector = new Vector2(0.75f, 0.75f);
				Vector2 vector2 = new Vector2(new Vector2(pos.x, pos.z).magnitude - vector.x, pos.y);
				return vector2.magnitude - vector.y + 0.25f;
			}
			float vertical_capsule(Vector3 pos)
			{
				float num = 0.5f;
				float num2 = 0.5f;
				return new Vector3(pos.x, pos.y - Mathf.Clamp(pos.y, 0f - num, num), pos.z).magnitude - num2;
			}
		}

		protected override void AssignInput(PlayerActions actions)
		{
			base.AssignInput(actions);
			m_inputState.AddOnKeyDownListener(actions.m_toolPrimary, delegate
			{
				m_editMode = LandscapeEditMode.Add;
			}, m_contextIcons.m_primaryIcon);
			m_inputState.AddOnKeyUpListener(actions.m_toolPrimary, delegate
			{
				m_editMode = LandscapeEditMode.None;
			});
			m_inputState.AddOnKeyDownListener(actions.m_toolSecondary, delegate
			{
				m_editMode = LandscapeEditMode.Remove;
			}, m_contextIcons.m_secondaryIcon);
			m_inputState.AddOnKeyUpListener(actions.m_toolSecondary, delegate
			{
				m_editMode = LandscapeEditMode.None;
			});
			m_inputState.AddOnKeyDownListener(actions.m_toolSpecial2, delegate
			{
				m_editMode = LandscapeEditMode.Blur;
			}, m_contextIcons.m_special2Icon);
			m_inputState.AddOnKeyUpListener(actions.m_toolSpecial2, delegate
			{
				m_editMode = LandscapeEditMode.None;
			});
			m_inputState.AddOnStateLoseFocusListener(delegate
			{
				m_editMode = LandscapeEditMode.None;
			});
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (m_landscapeEffect != null)
			{
				UnityEngine.Object.Destroy(m_landscapeEffect.gameObject);
			}
			if (hotbar != null)
			{
				hotbar.DisableHotbar();
			}
		}

		private Vector3 GetVoxelPosition(Vector3 position)
		{
			return position - new Vector3(1f, 1f, 1f);
		}

		public void SetBrushRotation(float rotation)
		{
			newBrushInfo.mRollAngle = rotation * 10f - 180f;
		}

		public void SetBrushStrength(float strength)
		{
			mStrengthSetting = (StrengthSetting)strength;
		}

		public void SetBrushRoughness(float roughness)
		{
			newBrushInfo.mRoughnessSetting = (StrengthSetting)roughness;
		}

		public void SetBrushSize(float size)
		{
			newBrushInfo.mScaleSetting = (StrengthSetting)size;
		}

		public void LockDistance(bool locked)
		{
			m_lockDistance = locked;
		}

		public void LockHeight(bool locked)
		{
			if (localPath == null || localPath.Count <= 0)
			{
				ConstructPath();
			}
			if (locked)
			{
				lockPosition = m_gunOrigin.TransformPoint(localPath[localPath.Count - 1]);
			}
			m_lockY = locked;
		}

		private void ConstructPath()
		{
			Vector3 vector = m_gunOrigin.TransformPoint(Vector3.zero);
			Vector3 velocity = m_gunOrigin.TransformPoint(Vector3.zero + new Vector3(0f, 0f, 0.5f)) - vector;
			Utility.ConstructPath(vector, velocity, localPath);
		}

		private void Update()
		{
			bool editing = m_editing;
			if (!m_editing)
			{
				SetExtrusionAmount(0.0015f);
				SetLineWidth(0.08f);
				SetSpadeSpinAndPlayFireEffects(0f);
				if (!m_lockDistance)
				{
					ConstructPath();
					for (int i = 0; i < localPath.Count; i++)
					{
						localPath[i] = m_gunOrigin.InverseTransformPoint(localPath[i]);
					}
				}
				if (m_editMode != LandscapeEditMode.None)
				{
					m_editing = true;
				}
			}
			if (localPath.Count <= 0)
			{
				m_editing = false;
			}
			else
			{
				Vector3 vector = m_gunOrigin.TransformPoint(localPath[localPath.Count - 1]);
				if (m_lockY)
				{
					vector.y = lockPosition.y;
				}
				m_voxel.transform.position = GetVoxelPosition(vector);
				m_voxel.transform.rotation = Quaternion.identity;
				if (currentBrushEntry != null && currentBrush != null)
				{
					Vector3 position = vector;
					position.x -= 135f;
					position.x *= -1f;
					if (m_editing)
					{
						if (!m_landscapeEffect.isPlaying)
						{
							ParticleSystem.EmissionModule emission = m_landscapeEffect.emission;
							emission.rateOverDistance = 25f / (currentBrushInfo.CalculateCurrentScale() * currentBrushEntry.mStandardSize.magnitude);
							m_landscapeEffect.Play();
						}
						m_landscapeEffect.transform.position = m_voxel.transform.position;
						float lerpIntensity = CalculateCurrentStrength();
						switch (m_editMode)
						{
						case LandscapeEditMode.Add:
							DMEditor.Instance.VolumeRootObject.AddVolume(vector, currentBrush, lerpIntensity);
							if (m_mirrorX)
							{
								DMEditor.Instance.VolumeRootObject.AddVolume(position, currentBrush, lerpIntensity);
							}
							SetExtrusionAmount(0.004f);
							SetLineWidth(0.11f);
							SetSpadeSpinAndPlayFireEffects(250f);
							if (!m_addSoundObject)
							{
								m_addSoundObject = Utility.PlayContinousSound(m_addSound, m_landscapeEffect.transform);
							}
							break;
						case LandscapeEditMode.Remove:
							DMEditor.Instance.VolumeRootObject.SubtractVolume(vector, currentBrush, lerpIntensity);
							if (m_mirrorX)
							{
								DMEditor.Instance.VolumeRootObject.SubtractVolume(position, currentBrush, lerpIntensity);
							}
							SetExtrusionAmount(-0.006f);
							SetLineWidth(0.05f);
							SetSpadeSpinAndPlayFireEffects(250f);
							if (!m_removeSoundObject)
							{
								m_removeSoundObject = Utility.PlayContinousSound(m_removeSound, m_landscapeEffect.transform);
							}
							break;
						case LandscapeEditMode.Blur:
							DMEditor.Instance.VolumeRootObject.BlurVolume(vector, currentBrush);
							if (m_mirrorX)
							{
								DMEditor.Instance.VolumeRootObject.BlurVolume(position, currentBrush);
							}
							SetExtrusionAmount(0.0015f);
							SetLineWidth(0.1f);
							SetSpadeSpinAndPlayFireEffects(250f);
							if (!m_blurSoundObject)
							{
								m_blurSoundObject = Utility.PlayContinousSound(m_blurSound, m_landscapeEffect.transform);
							}
							break;
						case LandscapeEditMode.None:
							if (m_landscapeEffect.isPlaying)
							{
								m_landscapeEffect.Stop();
							}
							m_editing = false;
							SetExtrusionAmount(0.0015f);
							SetLineWidth(0.08f);
							SetSpadeSpinAndPlayFireEffects(0f);
							if ((bool)m_addSoundObject)
							{
								m_addSoundObject.Stop(0f);
							}
							if ((bool)m_removeSoundObject)
							{
								m_removeSoundObject.Stop(0f);
							}
							if ((bool)m_blurSoundObject)
							{
								m_blurSoundObject.Stop(0f);
							}
							break;
						}
						DMEditor.Instance.MarkObjectsForSnapping(DMEditor.Instance.VolumeRootObject.GetBounds(vector, currentBrush));
					}
				}
			}
			if (editing && !m_editing)
			{
				DMEditor.Instance.ScheduleTakeLevelSnapshot();
			}
			if (m_previousEditMode != m_editMode)
			{
				m_editModeChanged = true;
			}
			else
			{
				m_editModeChanged = false;
			}
			m_previousEditMode = m_editMode;
			Vector3 forward = DMEditor.Instance.playerCamera.transform.forward;
			newBrushInfo.mYawAngle = Mathf.Atan2(forward.x, 0f - forward.z) * 180f / (float)Math.PI;
			if (mBrushGenerationStatus == BrushGenerationStatus.done)
			{
				mBrushGenerationStatus = BrushGenerationStatus.idle;
				previewMesh.mesh = VolumeBrushes.GenerateBrushPreview(currentBrush, currentMeshData);
				previewMesh.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.6f, 0.6f, 0.6f, Mathf.Lerp(0.1f, 0.5f, (float)mStrengthSetting / 4f)));
			}
			UpdateBrushWhenNeeded();
		}

		public void UpdateBrushWhenNeeded()
		{
			if (mBrushGenerationStatus == BrushGenerationStatus.idle && (!BrushInfo.AlmostEqual(newBrushInfo, currentBrushInfo) || newBrushEntry != currentBrushEntry))
			{
				mBrushGenerationStatus = BrushGenerationStatus.generatingBrush;
				currentBrushEntry = newBrushEntry;
				currentBrushInfo = newBrushInfo;
				Task.Run(delegate
				{
					currentBrush = currentBrushEntry.mCreateBrush(currentBrushEntry.mStandardSize, currentBrushInfo);
					currentMeshData = VolumeBrushes.GenerateBrushMeshData(currentBrush);
					mBrushGenerationStatus = BrushGenerationStatus.done;
				});
			}
		}

		public void SetBrushEntry(BrushEntry brushEntry)
		{
			newBrushEntry = brushEntry;
		}

		private void SetExtrusionAmount(float amount, bool forceUpdate = false)
		{
			if (!m_editModeChanged && !forceUpdate)
			{
				return;
			}
			LeanTween.value(m_spadeMeshRenderer.material.GetFloat("_ExtrusionAmount"), amount, 0.5f).setOnUpdate(delegate(float value)
			{
				if (m_spadeMeshRenderer != null)
				{
					m_spadeMeshRenderer.material.SetFloat("_ExtrusionAmount", value);
				}
			}).setEaseOutExpo();
		}

		private void SetLineWidth(float amount, bool forceUpdate = false)
		{
			if (m_editModeChanged || forceUpdate)
			{
				LeanTween.value(lineWidth, amount, 0.5f).setOnUpdate(delegate(float value)
				{
					lineWidth = value;
				}).setEaseOutExpo();
			}
		}

		private void SetSpadeSpinAndPlayFireEffects(float amount, bool forceUpdate = false)
		{
			if (m_editModeChanged || forceUpdate)
			{
				if (amount > 0f)
				{
					m_spadeRibbonFireEffect.Play();
					m_armFireEffect.Play();
				}
				else
				{
					m_spadeRibbonFireEffect.Stop();
					m_armFireEffect.Stop();
				}
				Rotate rotateComp = m_spadeMeshRenderer.GetComponent<Rotate>();
				LeanTween.value(m_spadeMeshRenderer.gameObject, rotateComp.rotation, UnityEngine.Random.insideUnitSphere.normalized * Mathf.Max(30f, amount), 1f).setOnUpdateVector3(delegate(Vector3 val)
				{
					rotateComp.rotation = val;
				});
			}
		}

		public void OnPostRender()
		{
			m_traceMaterial.SetPass(0);
			Utility.RenderLocalPath(m_gunOrigin.transform, lineWidth, localPath, m_pathColor);
		}
	}
}
