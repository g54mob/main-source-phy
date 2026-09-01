using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace MinimalVolumeCulling;

public sealed class CullingBrain : MonoBehaviour
{
	[Serializable]
	public sealed class EmbeddedProfile
	{
		private string profileId = "BackOfTurret";

		private string[] activeZoneIds;

		public string ProfileId => profileId;

		public ReadOnlySpan<string> ActiveZoneIds
		{
			get
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EFC70");
				object obj = default(object);
				EmbeddedProfile embeddedProfile = (EmbeddedProfile)obj;
				return (ReadOnlySpan<string>)this;
			}
		}

		public EmbeddedProfile()
		{
			string[] array = Array.Empty<string>();
			activeZoneIds = array;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	public enum DiscoveryMode
	{
		CacheOnEnable,
		RescanEveryUpdate
	}

	private Transform targetTransform;

	private string targetTag = "MainCamera";

	private List<EmbeddedProfile> profiles;

	private bool requireCameraVolumeTag;

	private string cameraVolumeTag;

	private bool requireCullZoneTag;

	private string cullZoneTag;

	private LayerMask overlapLayerMask;

	private float overlapPaddingRadius;

	private float insideTestEpsilon;

	private float updateIntervalSeconds;

	private DiscoveryMode discoveryMode;

	private bool includeInactiveTargets;

	private bool showDebugInfoInInspector;

	private string debugWinningCameraVolume;

	private string debugSelectedProfileId;

	private string[] debugActiveCullZoneIds;

	private int debugTargetCount;

	private int debugCulledCount;

	private bool verboseDebugLogging;

	private readonly Collider[] _overlapBuffer;

	private readonly List<CameraCullingVolume> _activeCameraVolumes;

	private readonly List<CullZone> _activeCullZones;

	private readonly List<CameraCullingVolume> _allCameraVolumes;

	private readonly List<CullZone> _allCullZones;

	private readonly List<CullTarget> _targets;

	private float _nextUpdateTime;

	private EmbeddedProfile _selectedProfile;

	private CameraCullingVolume _winningCameraVolume;

	private void OnEnable()
	{
		_nextUpdateTime = 0f;
		if (discoveryMode == DiscoveryMode.CacheOnEnable)
		{
			RebuildCaches();
		}
	}

	private unsafe void Update()
	{
		//IL_0432: Invalid comparison between F4 and I4
		//IL_0040: Invalid comparison between I4 and F4
		//IL_004f: Expected F4, but got I4
		//IL_0158: Expected O, but got Ref
		//IL_03af: Expected O, but got I
		//IL_03bf: Expected O, but got I
		//IL_03d9: Expected O, but got I
		//IL_03e9: Expected O, but got I
		//IL_0262: Expected O, but got I
		//IL_019e: Expected O, but got I
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Expected O, but got Unknown
		//IL_0504: Expected O, but got I
		//IL_028c: Expected O, but got I4
		//IL_0295: Expected O, but got I4
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Expected O, but got Unknown
		//IL_0309: Expected O, but got I
		if (updateIntervalSeconds > 0f)
		{
			float time = Time.time;
			if (_nextUpdateTime > time)
			{
				return;
			}
		}
		float time2 = Time.time;
		bool flag = 0f > updateIntervalSeconds;
		float num = 0f;
		if (!flag)
		{
			num = updateIntervalSeconds;
		}
		float nextUpdateTime = num + time2;
		_nextUpdateTime = nextUpdateTime;
		if (discoveryMode == DiscoveryMode.RescanEveryUpdate)
		{
			RebuildCaches();
		}
		UnityEngine.Object obj;
		if (targetTransform == null)
		{
			if (!string.IsNullOrWhiteSpace(targetTag))
			{
				GameObject gameObject = GameObject.FindGameObjectWithTag(targetTag);
				if (gameObject != null)
				{
					Transform transform = gameObject.transform;
					obj = transform;
					goto IL_0125;
				}
			}
			obj = null;
		}
		else
		{
			obj = targetTransform;
		}
		goto IL_0125;
		IL_0125:
		EmbeddedProfile selectedProfile;
		if (obj != null)
		{
			Vector3 position = ((Transform)obj).position;
			object obj2 = default(object);
			ComputeActiveCameraVolumes((Vector3)(&obj2));
			List<CameraCullingVolume> activeCameraVolumes = _activeCameraVolumes;
			string text = null;
			UnityEngine.Object obj3 = null;
			UnityEngine.Object obj4 = default(UnityEngine.Object);
			for (string text2 = null; (nint)text2 < activeCameraVolumes._size; activeCameraVolumes = _activeCameraVolumes, text++, text2 = text)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if (!(obj4 != null))
				{
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v444 @ stack_8_v8 (UnityEngine.Object)+28]");
				if (string.IsNullOrWhiteSpace((string)0))
				{
					continue;
				}
				if (obj3 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v444 @ stack_8_v8 (UnityEngine.Object)+20]");
					nint num2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v442 @ rsi_v11 (UnityEngine.Object)+20]");
					if (num2 <= 0)
					{
						continue;
					}
				}
				obj3 = obj4;
			}
			_winningCameraVolume = (CameraCullingVolume)obj3;
			UnityEngine.Object winningCameraVolume = _winningCameraVolume;
			if (_winningCameraVolume != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v456 @ rdi_v7 (UnityEngine.Object)+28]");
				if (!string.IsNullOrWhiteSpace((string)0))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v456 @ rdi_v7 (UnityEngine.Object)+28]");
					if (!string.IsNullOrWhiteSpace((string)0))
					{
						List<EmbeddedProfile> list = profiles;
						object obj5 = 0;
						object obj6 = 0;
						EmbeddedProfile embeddedProfile = default(EmbeddedProfile);
						while ((nint)obj6 < list._size)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							if (embeddedProfile != null && !string.IsNullOrWhiteSpace(embeddedProfile.profileId))
							{
								string a = embeddedProfile.profileId.Trim();
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v456 @ rdi_v7 (UnityEngine.Object)+28]");
								string b = ((string)0).Trim();
								bool flag2 = string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
								selectedProfile = embeddedProfile;
								if (flag2)
								{
									goto IL_0516;
								}
							}
							list = profiles;
							obj5++;
							bool flag3 = profiles != null;
							obj6 = obj5;
							if (!flag3)
							{
								throw new NullReferenceException();
							}
						}
					}
				}
			}
			selectedProfile = null;
			goto IL_0516;
		}
		if (showDebugInfoInInspector)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v638 @ rax_v12+B8]");
			object obj8 = 0;
			debugWinningCameraVolume = (string)obj8;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj9 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v680 @ rax_v14+B8]");
			object obj10 = 0;
			debugSelectedProfileId = (string)obj10;
			string[] array = Array.Empty<string>();
			debugActiveCullZoneIds = array;
			debugTargetCount = 0;
		}
		return;
		IL_0516:
		_selectedProfile = selectedProfile;
		ComputeActiveCullZones(_selectedProfile);
		ApplyCulling();
		WriteDebug();
	}

	private void RebuildCaches_ContextMenu()
	{
		RebuildCaches();
	}

	private void RebuildCaches()
	{
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Expected O, but got Unknown
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Expected O, but got Unknown
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Expected O, but got Unknown
		List<CullZone> allCullZones = _allCullZones;
		int version = allCullZones._version + 1;
		allCullZones._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			allCullZones._size = 0;
		}
		else
		{
			allCullZones._size = 0;
			if (allCullZones._size > 0)
			{
				Array.Clear(allCullZones._items, 0, allCullZones._size);
			}
		}
		CullZone[] array = UnityEngine.Object.FindObjectsByType<CullZone>(FindObjectsSortMode.None);
		object obj2 = array + 32;
		int num = 0;
		for (int num2 = 0; num2 < array.Length; num2 = num)
		{
			if ((UnityEngine.Object)obj2 != null && PassesCullZoneTagFilter((CullZone)obj2))
			{
				_allCullZones.Add((CullZone)obj2);
			}
			num++;
			obj2 += 8;
		}
		List<CameraCullingVolume> allCameraVolumes = _allCameraVolumes;
		int version2 = allCameraVolumes._version + 1;
		allCameraVolumes._version = version2;
		CullZone[] array2 = UnityEngine.Object.FindObjectsByType<CullZone>(FindObjectsSortMode.None);
		if (array2 == null)
		{
			allCameraVolumes._size = 0;
		}
		else
		{
			allCameraVolumes._size = 0;
			if (allCameraVolumes._size > 0)
			{
				Array.Clear(allCameraVolumes._items, 0, allCameraVolumes._size);
			}
		}
		CameraCullingVolume[] array3 = UnityEngine.Object.FindObjectsByType<CameraCullingVolume>(FindObjectsSortMode.None);
		object obj3 = array3 + 32;
		int num3 = 0;
		for (int num4 = 0; num4 < array3.Length; num4 = num3)
		{
			if ((UnityEngine.Object)obj3 != null && PassesCameraVolumeTagFilter((CameraCullingVolume)obj3))
			{
				_allCameraVolumes.Add((CameraCullingVolume)obj3);
			}
			num3++;
			obj3 += 8;
		}
		List<CullTarget> targets = _targets;
		int version3 = targets._version + 1;
		targets._version = version3;
		CameraCullingVolume[] array4 = UnityEngine.Object.FindObjectsByType<CameraCullingVolume>(FindObjectsSortMode.None);
		if (array4 == null)
		{
			targets._size = 0;
		}
		else
		{
			targets._size = 0;
			if (targets._size > 0)
			{
				Array.Clear(targets._items, 0, targets._size);
			}
		}
		bool flag = !includeInactiveTargets;
		bool findObjectsInactive = !flag;
		CullTarget[] collection = UnityEngine.Object.FindObjectsByType<CullTarget>(findObjectsInactive ? FindObjectsInactive.Include : FindObjectsInactive.Exclude, FindObjectsSortMode.None);
		_targets.AddRange(collection);
		if (showDebugInfoInInspector)
		{
			List<CullTarget> targets2 = _targets;
			debugTargetCount = targets2._size;
		}
	}

	private void RefreshCullZonesCache()
	{
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Expected O, but got Unknown
		List<CullZone> allCullZones = _allCullZones;
		int version = allCullZones._version + 1;
		allCullZones._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			allCullZones._size = 0;
		}
		else
		{
			allCullZones._size = 0;
			if (allCullZones._size > 0)
			{
				Array.Clear(allCullZones._items, 0, allCullZones._size);
			}
		}
		CullZone[] array = UnityEngine.Object.FindObjectsByType<CullZone>(FindObjectsSortMode.None);
		object obj2 = array + 32;
		int num = 0;
		for (int num2 = 0; num2 < array.Length; num2 = num)
		{
			if ((UnityEngine.Object)obj2 != null && PassesCullZoneTagFilter((CullZone)obj2))
			{
				_allCullZones.Add((CullZone)obj2);
			}
			num++;
			obj2 += 8;
		}
	}

	private void RefreshCameraVolumesCache()
	{
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Expected O, but got Unknown
		List<CameraCullingVolume> allCameraVolumes = _allCameraVolumes;
		int version = allCameraVolumes._version + 1;
		allCameraVolumes._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			allCameraVolumes._size = 0;
		}
		else
		{
			allCameraVolumes._size = 0;
			if (allCameraVolumes._size > 0)
			{
				Array.Clear(allCameraVolumes._items, 0, allCameraVolumes._size);
			}
		}
		CameraCullingVolume[] array = UnityEngine.Object.FindObjectsByType<CameraCullingVolume>(FindObjectsSortMode.None);
		object obj2 = array + 32;
		int num = 0;
		for (int num2 = 0; num2 < array.Length; num2 = num)
		{
			if ((UnityEngine.Object)obj2 != null && PassesCameraVolumeTagFilter((CameraCullingVolume)obj2))
			{
				_allCameraVolumes.Add((CameraCullingVolume)obj2);
			}
			num++;
			obj2 += 8;
		}
	}

	private void RefreshTargetsCache()
	{
		List<CullTarget> targets = _targets;
		int version = targets._version + 1;
		targets._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			targets._size = 0;
		}
		else
		{
			targets._size = 0;
			if (targets._size > 0)
			{
				Array.Clear(targets._items, 0, targets._size);
			}
		}
		bool flag = !includeInactiveTargets;
		bool findObjectsInactive = !flag;
		CullTarget[] collection = UnityEngine.Object.FindObjectsByType<CullTarget>(findObjectsInactive ? FindObjectsInactive.Include : FindObjectsInactive.Exclude, FindObjectsSortMode.None);
		_targets.AddRange(collection);
		if (showDebugInfoInInspector)
		{
			List<CullTarget> targets2 = _targets;
			debugTargetCount = targets2._size;
		}
	}

	private Transform ResolveTargetTransform()
	{
		if (targetTransform == null)
		{
			if (!string.IsNullOrWhiteSpace(targetTag))
			{
				GameObject gameObject = GameObject.FindGameObjectWithTag(targetTag);
				if (gameObject != null)
				{
					if ((object)gameObject != null)
					{
						return gameObject.transform;
					}
					return (Transform)(object)new NullReferenceException();
				}
			}
			return null;
		}
		return targetTransform;
	}

	private bool PassesCameraVolumeTagFilter(CameraCullingVolume v)
	{
		//IL_0092: Expected I4, but got O
		if (requireCameraVolumeTag)
		{
			if (v != null && !string.IsNullOrWhiteSpace(cameraVolumeTag))
			{
				if ((object)v != null)
				{
					return v.CompareTag(cameraVolumeTag);
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			return false;
		}
		return true;
	}

	private bool PassesCullZoneTagFilter(CullZone z)
	{
		//IL_0092: Expected I4, but got O
		if (requireCullZoneTag)
		{
			if (z != null && !string.IsNullOrWhiteSpace(cullZoneTag))
			{
				if ((object)z != null)
				{
					return z.CompareTag(cullZoneTag);
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			return false;
		}
		return true;
	}

	private unsafe void ComputeActiveCameraVolumes(Vector3 targetPosition)
	{
		//IL_00c1: Invalid comparison between I4 and F4
		//IL_00d0: Expected F4, but got I4
		//IL_0108: Expected O, but got Ref
		//IL_0135: Expected O, but got I4
		//IL_015b: Expected O, but got I
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Expected O, but got Unknown
		List<CameraCullingVolume> activeCameraVolumes = _activeCameraVolumes;
		int version = activeCameraVolumes._version + 1;
		activeCameraVolumes._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			activeCameraVolumes._size = 0;
		}
		else
		{
			activeCameraVolumes._size = 0;
			if (activeCameraVolumes._size > 0)
			{
				Array.Clear(activeCameraVolumes._items, 0, activeCameraVolumes._size);
			}
		}
		bool flag = 0f > overlapPaddingRadius;
		float radius = 0f;
		if (!flag)
		{
			radius = overlapPaddingRadius;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
		object obj2 = default(object);
		int layerMask = default(int);
		QueryTriggerInteraction queryTriggerInteraction = default(QueryTriggerInteraction);
		int num = Physics.OverlapSphereNonAlloc((Vector3)(&obj2), radius, _overlapBuffer, layerMask, queryTriggerInteraction);
		if (num <= 0)
		{
			return;
		}
		object obj3 = 32;
		int num2 = 0;
		UnityEngine.Object obj4 = default(UnityEngine.Object);
		do
		{
			Collider[] overlapBuffer = _overlapBuffer;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v85 @ r14_v7+v107 @ rax_v14 (UnityEngine.Collider[])]");
			if ((UnityEngine.Object)0 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				if (obj4 != null && PassesCameraVolumeTagFilter((CameraCullingVolume)obj4) && !_activeCameraVolumes.Contains((CameraCullingVolume)obj4))
				{
					_activeCameraVolumes.Add((CameraCullingVolume)obj4);
				}
			}
			num2++;
			obj3 += 8;
		}
		while (num2 < num);
	}

	private CameraCullingVolume SelectWinningCameraVolume()
	{
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Expected O, but got Unknown
		//IL_00a1: Expected O, but got I
		List<CameraCullingVolume> activeCameraVolumes = _activeCameraVolumes;
		bool flag = _activeCameraVolumes == null;
		CameraCullingVolume cameraCullingVolume = null;
		CameraCullingVolume cameraCullingVolume2 = null;
		CameraCullingVolume cameraCullingVolume3 = null;
		if (!flag)
		{
			UnityEngine.Object obj = default(UnityEngine.Object);
			while (true)
			{
				if ((nint)cameraCullingVolume3 < activeCameraVolumes._size)
				{
					if (_activeCameraVolumes == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if (obj != null)
					{
						if ((object)obj == null)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ stack_8_v3 (UnityEngine.Object)+28]");
						if (!string.IsNullOrWhiteSpace((string)0))
						{
							if (cameraCullingVolume2 != null)
							{
								if ((object)cameraCullingVolume2 == null)
								{
									break;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ stack_8_v3 (UnityEngine.Object)+20]");
								if ((nint)0 <= (nint)cameraCullingVolume2.priority)
								{
									goto IL_0182;
								}
							}
							cameraCullingVolume2 = (CameraCullingVolume)obj;
						}
					}
					goto IL_0182;
				}
				return cameraCullingVolume2;
				IL_0182:
				activeCameraVolumes = _activeCameraVolumes;
				cameraCullingVolume = (CameraCullingVolume)(cameraCullingVolume + 1);
				if (_activeCameraVolumes == null)
				{
					break;
				}
				cameraCullingVolume3 = cameraCullingVolume;
			}
		}
		return (CameraCullingVolume)(object)new NullReferenceException();
	}

	private EmbeddedProfile ResolveSelectedEmbeddedProfile(CameraCullingVolume winningVolume)
	{
		//IL_0087: Expected O, but got I4
		//IL_0090: Expected O, but got I4
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Expected O, but got Unknown
		EmbeddedProfile result;
		if (winningVolume != null)
		{
			if ((object)winningVolume == null)
			{
				goto IL_01f5;
			}
			if (!string.IsNullOrWhiteSpace(winningVolume.profileId))
			{
				if (!string.IsNullOrWhiteSpace(winningVolume.profileId))
				{
					List<EmbeddedProfile> list = profiles;
					bool flag = profiles == null;
					object obj = 0;
					object obj2 = 0;
					if (flag)
					{
						goto IL_01f5;
					}
					EmbeddedProfile embeddedProfile = default(EmbeddedProfile);
					while ((nint)obj2 < list._size)
					{
						if (profiles != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							if (embeddedProfile == null || string.IsNullOrWhiteSpace(embeddedProfile.profileId))
							{
								goto IL_019d;
							}
							if (embeddedProfile.profileId != null)
							{
								string a = embeddedProfile.profileId.Trim();
								if (winningVolume.profileId != null)
								{
									string b = winningVolume.profileId.Trim();
									bool flag2 = string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
									result = embeddedProfile;
									if (!flag2)
									{
										goto IL_019d;
									}
									goto IL_0228;
								}
							}
						}
						goto IL_01f5;
						IL_019d:
						list = profiles;
						obj++;
						if (profiles != null)
						{
							obj2 = obj;
							continue;
						}
						goto IL_01f5;
					}
				}
				result = null;
				goto IL_0228;
			}
		}
		return null;
		IL_0228:
		return result;
		IL_01f5:
		return (EmbeddedProfile)(object)new NullReferenceException();
	}

	private EmbeddedProfile FindProfileById(string profileId)
	{
		//IL_0029: Expected O, but got I4
		//IL_0032: Expected O, but got I4
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Expected O, but got Unknown
		if (!string.IsNullOrWhiteSpace(profileId))
		{
			List<EmbeddedProfile> list = profiles;
			bool flag = profiles == null;
			object obj = 0;
			object obj2 = 0;
			if (flag)
			{
				goto IL_01ac;
			}
			EmbeddedProfile embeddedProfile = default(EmbeddedProfile);
			while ((nint)obj2 < list._size)
			{
				if (profiles != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if (embeddedProfile == null || string.IsNullOrWhiteSpace(embeddedProfile.profileId))
					{
						goto IL_0151;
					}
					if (embeddedProfile.profileId != null)
					{
						string a = embeddedProfile.profileId.Trim();
						if (profileId != null)
						{
							string b = profileId.Trim();
							if (!string.Equals(a, b, StringComparison.OrdinalIgnoreCase))
							{
								goto IL_0151;
							}
							return embeddedProfile;
						}
					}
				}
				goto IL_01ac;
				IL_0151:
				list = profiles;
				obj++;
				if (profiles != null)
				{
					obj2 = obj;
					continue;
				}
				goto IL_01ac;
			}
		}
		return null;
		IL_01ac:
		return (EmbeddedProfile)(object)new NullReferenceException();
	}

	private unsafe void ComputeActiveCullZones(EmbeddedProfile profile)
	{
		//IL_012f: Expected O, but got I
		//IL_012f: Expected O, but got Ref
		List<CullZone> activeCullZones = _activeCullZones;
		int version = activeCullZones._version + 1;
		activeCullZones._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			activeCullZones._size = 0;
		}
		else
		{
			activeCullZones._size = 0;
			if (activeCullZones._size > 0)
			{
				Array.Clear(activeCullZones._items, 0, activeCullZones._size);
			}
		}
		UnityEngine.Object obj2 = default(UnityEngine.Object);
		if (profile == null)
		{
			List<CullZone> allCullZones = _allCullZones;
			int num = 0;
			for (int num2 = 0; num2 < allCullZones._size; num2 = num)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				bool flag = obj2 == null;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ stack_8_v4 (UnityEngine.Object)+28]");
					if ((nint)0 != (flag ? 1 : 0))
					{
						_activeCullZones.Add((CullZone)obj2);
					}
				}
				allCullZones = _allCullZones;
				num++;
			}
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EFC70");
		List<CullZone> allCullZones2 = _allCullZones;
		int num3 = 0;
		int num4 = 0;
		object obj3 = default(object);
		bool flag2;
		do
		{
			if (num4 >= allCullZones2._size)
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj2 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ stack_8_v4 (UnityEngine.Object)+20]");
				if (ZoneIdListContains((ReadOnlySpan<string>)(&obj3), (string)0))
				{
					_activeCullZones.Add((CullZone)obj2);
				}
			}
			allCullZones2 = _allCullZones;
			num3++;
			flag2 = _allCullZones != null;
			num4 = num3;
		}
		while (flag2);
		throw new NullReferenceException();
	}

	private static bool ZoneIdListContains(ReadOnlySpan<string> activeIds, string zoneId)
	{
		//IL_0036: Expected O, but got I4
		//IL_003f: Expected O, but got I4
		//IL_005d: Expected O, but got I
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Expected O, but got Unknown
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_0183: Expected I4, but got O
		//IL_00a5: Expected O, but got I
		if (!string.IsNullOrWhiteSpace(zoneId))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [activeIds @ rcx (System.ReadOnlySpan`1<System.String>)+8]");
			if ((nint)0 > (nint)0)
			{
				object obj = 0;
				object obj2 = 0;
				object obj3;
				do
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rsi_v5+v218 @ rax_v10]");
					if (!string.IsNullOrWhiteSpace((string)0))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rsi_v5+v218 @ rax_v10]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rsi_v5+v218 @ rax_v10]");
							string a = ((string)0).Trim();
							if (zoneId != null)
							{
								string b = zoneId.Trim();
								if (!string.Equals(a, b, StringComparison.OrdinalIgnoreCase))
								{
									goto IL_00fe;
								}
								return true;
							}
						}
						NullReferenceException ex = new NullReferenceException();
						return (byte)(int)ex != 0;
					}
					goto IL_00fe;
					IL_00fe:
					obj2++;
					obj += 8;
					obj3 = obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [activeIds @ rcx (System.ReadOnlySpan`1<System.String>)+8]");
				}
				while ((nint)obj3 < 0);
			}
		}
		return false;
	}

	private unsafe void ApplyCulling()
	{
		//IL_058a: Expected O, but got I
		//IL_05a9: Expected O, but got I
		//IL_0205: Expected I4, but got O
		//IL_0079: Expected O, but got I4
		//IL_068e: Expected O, but got I4
		//IL_00ac: Expected O, but got I4
		//IL_05eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Expected O, but got Unknown
		//IL_00fa: Expected O, but got I4
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Expected O, but got Unknown
		//IL_0278: Expected I, but got O
		//IL_0288: Expected O, but got I
		//IL_013b: Expected O, but got Ref
		//IL_0171: Expected O, but got I4
		//IL_017a: Expected O, but got I4
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Expected O, but got Unknown
		//IL_0345: Expected I, but got O
		//IL_0355: Expected O, but got I
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Expected O, but got Unknown
		//IL_01ab: Expected O, but got I4
		//IL_01b4: Expected O, but got I4
		//IL_0412: Expected I, but got O
		//IL_0422: Expected O, but got I
		//IL_04af: Expected I, but got O
		//IL_04bf: Expected O, but got I
		List<CullTarget> targets = _targets;
		bool flag = _targets == null;
		IntPtr intPtr = default(IntPtr);
		UnityEngine.Object obj = (UnityEngine.Object)(nint)intPtr;
		UnityEngine.Object obj2 = null;
		UnityEngine.Object obj3 = null;
		UnityEngine.Object obj4 = null;
		float num2 = default(float);
		float num = num2;
		UnityEngine.Object obj5 = (UnityEngine.Object)(nint)intPtr;
		object obj7 = default(object);
		object obj6 = obj7;
		nint num4 = default(nint);
		nint num3 = num4;
		UnityEngine.Object obj8 = null;
		if (!flag)
		{
			UnityEngine.Object obj9 = default(UnityEngine.Object);
			float x = default(float);
			object obj11 = default(object);
			UnityEngine.Object obj12 = default(UnityEngine.Object);
			UnityEngine.Object obj13 = default(UnityEngine.Object);
			object obj15 = default(object);
			object obj17 = default(object);
			object obj18 = default(object);
			object obj20 = default(object);
			object obj22 = default(object);
			object obj23 = default(object);
			object obj25 = default(object);
			object obj27 = default(object);
			while (true)
			{
				if ((nint)obj8 < targets._size)
				{
					obj2 = (UnityEngine.Object)(object)_targets;
					bool flag2 = _targets == null;
					num2 = num;
					obj = obj5;
					obj7 = obj6;
					num4 = num3;
					if (flag2)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					bool flag3 = obj9 != null;
					obj = null;
					obj7 = 0;
					if (flag3)
					{
						bool flag4 = (object)obj9 == null;
						num2 = num;
						obj = null;
						obj7 = 0;
						num4 = 0;
						obj2 = obj9;
						if (flag4)
						{
							break;
						}
						Transform transform = ((Component)obj9).transform;
						bool flag5 = (object)transform == null;
						num2 = num;
						obj = null;
						obj7 = 0;
						num4 = 0;
						obj2 = obj9;
						if (flag5)
						{
							break;
						}
						Vector3 position = transform.position;
						num = position.x;
						bool flag6 = TargetPositionInsideAnyActiveCullZone((Vector3)(&x));
						((CullTarget)obj9).ApplyCulled(flag6);
						bool flag7 = !flag6;
						x = position.x;
						obj = (UnityEngine.Object)flag6;
						obj7 = 0;
						if (!flag7)
						{
							obj4 = (UnityEngine.Object)(obj4 + 1);
							x = position.x;
							obj = (UnityEngine.Object)flag6;
							obj7 = 0;
						}
					}
					targets = _targets;
					obj3 = (UnityEngine.Object)(obj3 + 1);
					bool flag8 = _targets == null;
					num2 = num;
					num4 = 0;
					obj2 = obj3;
					if (flag8)
					{
						break;
					}
					obj5 = obj;
					obj6 = obj7;
					num3 = 0;
					obj8 = obj3;
					continue;
				}
				if (showDebugInfoInInspector)
				{
					debugCulledCount = (int)obj4;
				}
				if (!verboseDebugLogging)
				{
					return;
				}
				UnityEngine.Object obj10;
				if (_selectedProfile != null)
				{
					EmbeddedProfile selectedProfile = _selectedProfile;
					obj10 = (UnityEngine.Object)(object)selectedProfile.profileId;
				}
				else
				{
					obj10 = (UnityEngine.Object)(object)"(none / defaults)";
				}
				object[] array = new object[4];
				bool flag9 = array == null;
				obj = (UnityEngine.Object)4;
				obj2 = (UnityEngine.Object)(object)typeof(object[]);
				if (!flag9)
				{
					if ((object)obj10 != null)
					{
						nint num5 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v576 @ rdx_v33 (Il2CppClass<System.Object[]>)+40]");
						obj = (UnityEngine.Object)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						bool flag10 = obj11 == null;
						num2 = num;
						obj7 = obj6;
						num4 = num3;
						obj2 = obj10;
						if (flag10)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							throw obj12;
						}
					}
					obj2 = (UnityEngine.Object)(array + 32);
					array[0] = obj10;
					bool flag11 = _activeCullZones == null;
					obj = obj10;
					if (!flag11)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						if ((object)obj13 != null)
						{
							nint num6 = (nint)array;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v715 @ rdx_v31 (Il2CppClass<System.Object[]>)+40]");
							object obj14 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							bool flag12 = obj15 == null;
							num2 = num;
							obj7 = obj6;
							num4 = num3;
							UnityEngine.Object obj16 = obj13;
							if (flag12)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								throw obj17;
							}
						}
						obj2 = (UnityEngine.Object)(array + 40);
						array[1] = obj13;
						bool flag13 = _targets == null;
						obj = obj13;
						if (!flag13)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							if (obj18 != null)
							{
								nint num7 = (nint)array;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v742 @ rdx_v29 (Il2CppClass<System.Object[]>)+40]");
								object obj19 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								bool flag14 = obj20 == null;
								num2 = num;
								obj7 = obj6;
								num4 = num3;
								object obj21 = obj18;
								if (flag14)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
									throw obj22;
								}
							}
							array[2] = obj18;
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							if (obj23 != null)
							{
								nint num8 = (nint)array;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v770 @ rdx_v27 (Il2CppClass<System.Object[]>)+40]");
								object obj24 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								bool flag15 = obj25 == null;
								num2 = num;
								obj7 = obj6;
								num4 = num3;
								object obj26 = obj23;
								if (flag15)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
									throw obj27;
								}
							}
							array[3] = obj23;
							string message = string.Format("[CullingBrain] SelectedProfile={0}, ActiveCullZones={1}, Targets={2}, Culled={3}", array);
							Debug.Log(message);
							return;
						}
					}
				}
				throw new NullReferenceException();
			}
		}
		throw new NullReferenceException();
	}

	private unsafe bool TargetPositionInsideAnyActiveCullZone(Vector3 targetPosition)
	{
		//IL_0244: Expected O, but got I4
		//IL_024d: Expected O, but got I4
		//IL_020d: Expected I4, but got O
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Expected O, but got Unknown
		//IL_00eb: Expected O, but got Ref
		List<CullZone> activeCullZones = _activeCullZones;
		float num = insideTestEpsilon * insideTestEpsilon;
		bool flag = _activeCullZones == null;
		object obj = 0;
		object obj2 = 0;
		if (!flag)
		{
			UnityEngine.Object obj3 = default(UnityEngine.Object);
			float num2 = default(float);
			object obj4 = default(object);
			while (true)
			{
				if ((nint)obj2 < activeCullZones._size)
				{
					if (_activeCullZones == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if (obj3 != null)
					{
						if ((object)obj3 == null)
						{
							break;
						}
						Collider zoneCollider = ((CullZone)obj3).ZoneCollider;
						if (zoneCollider != null)
						{
							if ((object)zoneCollider == null)
							{
								break;
							}
							if (zoneCollider.enabled)
							{
								Vector3 vector = zoneCollider.ClosestPoint((Vector3)(&num2));
								float num3 = vector.x - targetPosition.x;
								float num4 = vector.y - (float)obj4;
								float num5 = vector.z - targetPosition.z;
								float num6 = num3 * num3;
								float num7 = num4 * num4;
								float num8 = num5 * num5;
								float num9 = num7 + num6;
								float num10 = num9 + num8;
								if (!(num < num10))
								{
									return true;
								}
							}
						}
					}
					activeCullZones = _activeCullZones;
					obj++;
					if (_activeCullZones == null)
					{
						break;
					}
					obj2 = obj;
					continue;
				}
				return false;
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private void WriteDebug()
	{
		//IL_0057: Expected O, but got I
		//IL_0067: Expected O, but got I
		//IL_0099: Expected O, but got I
		//IL_00a9: Expected O, but got I
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Expected O, but got Unknown
		//IL_0108: Expected O, but got I4
		//IL_0111: Expected O, but got I4
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Expected O, but got Unknown
		//IL_015c: Expected O, but got I
		if (!showDebugInfoInInspector)
		{
			return;
		}
		string text;
		if (_winningCameraVolume != null)
		{
			text = _winningCameraVolume.name;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v194 @ rax_v28+B8]");
			object obj2 = 0;
			text = (string)obj2;
		}
		debugWinningCameraVolume = text;
		object obj3;
		if (_selectedProfile != null)
		{
			obj3 = _selectedProfile + 16;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v352 @ rax_v25+B8]");
			obj3 = 0;
		}
		debugSelectedProfileId = (string)obj3;
		List<CullZone> activeCullZones = _activeCullZones;
		if (activeCullZones._size != 0)
		{
			string[] array = new string[activeCullZones._size];
			List<CullZone> activeCullZones2 = _activeCullZones;
			object obj5 = array + 32;
			object obj6 = 0;
			object obj7 = 0;
			UnityEngine.Object obj8 = default(UnityEngine.Object);
			while ((nint)obj7 < activeCullZones2._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				object obj9;
				if (obj8 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v204 @ stack_8_v5 (UnityEngine.Object)+20]");
					obj9 = 0;
				}
				else
				{
					obj9 = "(null)";
				}
				obj5 = obj9;
				activeCullZones2 = _activeCullZones;
				obj6++;
				obj5 += 8;
				obj7 = obj6;
			}
			debugActiveCullZoneIds = array;
		}
		else
		{
			string[] array2 = Array.Empty<string>();
			debugActiveCullZoneIds = array2;
		}
	}

	private void WriteDebugNoTarget()
	{
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		//IL_003f: Expected O, but got I
		//IL_004f: Expected O, but got I
		if (showDebugInfoInInspector)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rax_v3+B8]");
			object obj2 = 0;
			debugWinningCameraVolume = (string)obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v5+B8]");
			object obj4 = 0;
			debugSelectedProfileId = (string)obj4;
			string[] array = Array.Empty<string>();
			debugActiveCullZoneIds = array;
			debugTargetCount = 0;
		}
	}

	public CullingBrain()
	{
		List<EmbeddedProfile> list = new List<EmbeddedProfile>();
		profiles = list;
		requireCameraVolumeTag = true;
		cameraVolumeTag = "CullingCameraVolume";
		requireCullZoneTag = true;
		cullZoneTag = "CullingCullVolume";
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
		LayerMask layerMask = default(LayerMask);
		overlapLayerMask = layerMask;
		overlapPaddingRadius = 0.05f;
		insideTestEpsilon = 0.001f;
		showDebugInfoInInspector = true;
		debugActiveCullZoneIds = Array.Empty<string>();
		_overlapBuffer = new Collider[256];
		_activeCameraVolumes = new List<CameraCullingVolume>(16);
		_activeCullZones = new List<CullZone>(32);
		_allCameraVolumes = new List<CameraCullingVolume>(32);
		_allCullZones = new List<CullZone>(128);
		_targets = new List<CullTarget>(512);
		base._002Ector();
	}
}
