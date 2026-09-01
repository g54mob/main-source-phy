using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public class ImpactMarkerManager : MonoBehaviour
{
	private class MarkerData
	{
		public GunController gun;

		public RectTransform container;

		public GameObject activeMarkerInstance;

		public GameObject noShellMarkerInstance;

		public string lastMarkerName;

		public MarkerData()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3ABCE]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			lastMarkerName = "";
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	public TurretController turretController;

	public GameObject noShellImpactMarkerPrefab;

	public GameObject masterImpactMarkerPrefab;

	private List<MarkerData> markerDataList;

	private GameObject masterImpactMarkerInstance;

	private void Start()
	{
		if (turretController != null)
		{
			SetupAllMarkers();
			if (masterImpactMarkerPrefab != null)
			{
				Transform parent = base.transform;
				GameObject gameObject = UnityEngine.Object.Instantiate(masterImpactMarkerPrefab, parent);
				masterImpactMarkerInstance = gameObject;
			}
		}
		else
		{
			Debug.LogError("ImpactMarkerManager needs a reference to TurretController!", this);
			base.enabled = false;
		}
	}

	private unsafe void LateUpdate()
	{
		//IL_00e5: Expected O, but got Ref
		//IL_00e5: Expected O, but got Ref
		//IL_00c8: Expected O, but got Ref
		if (this.turretController != null)
		{
			UpdateAllGunMarkers();
			if (masterImpactMarkerInstance != null)
			{
				TurretController turretController = this.turretController;
				float num = CalculateProjectedRangeFromElevation(turretController._003CDesiredElevation_003Ek__BackingField);
				Vector3 euler = default(Vector3);
				Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
				object obj = default(object);
				Vector3 vector = (Quaternion)(&obj) * (Vector3)(&euler);
				TurretController turretController2 = this.turretController;
				Vector3 localPosition = turretController2.turretBase.localPosition;
				Transform transform = masterImpactMarkerInstance.transform;
				float num2 = default(float);
				transform.localPosition = (Vector3)(&num2);
			}
		}
	}

	private unsafe void SetupAllMarkers()
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_0133: Expected I, but got O
		//IL_024a: Expected O, but got Ref
		TurretController turretController = this.turretController;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<GunController>.Enumerator enumerator2 = default(List<GunController>.Enumerator);
		List<GunController>.Enumerator enumerator = enumerator2;
		List<GunController>.Enumerator enumerator3 = default(List<GunController>.Enumerator);
		GunController gunController = default(GunController);
		object obj = default(object);
		Type type = default(Type);
		RectTransform container = default(RectTransform);
		object obj2 = default(object);
		while (true)
		{
			if (enumerator3.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				MarkerData markerData = new MarkerData();
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3ABCE]");
				if ((nint)0 == 0)
				{
					_ = 1;
				}
				markerData.lastMarkerName = "";
				if (markerData == null)
				{
					break;
				}
				markerData.gun = gunController;
				MarkerData markerData2 = (MarkerData)(markerData + 16);
				if ((object)gunController != null)
				{
					string text = gunController.gunName + "_ImpactMarker";
					Type[] array = new Type[1];
					Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(RectTransform));
					bool flag = array == null;
					markerData2 = (MarkerData)(object)typeof(RectTransform);
					if (!flag)
					{
						if ((object)typeFromHandle != null)
						{
							nint num = (nint)array;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							if (obj == null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								throw type;
							}
						}
						if (array.Length > 0)
						{
							array[0] = typeFromHandle;
							GameObject gameObject = new GameObject(text, array);
							if ((object)gameObject != null)
							{
								Transform transform = gameObject.transform;
								Transform parent = base.transform;
								if ((object)transform != null)
								{
									transform.SetParent(parent, worldPositionStays: false);
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
									markerData.container = container;
									if ((object)markerData.container != null)
									{
										enumerator = (List<GunController>.Enumerator)Vector3.oneVector;
										markerData.container.localScale = (Vector3)(&obj2);
										if (noShellImpactMarkerPrefab != null)
										{
											GameObject noShellMarkerInstance = UnityEngine.Object.Instantiate(noShellImpactMarkerPrefab, markerData.container);
											markerData.noShellMarkerInstance = noShellMarkerInstance;
											if ((object)markerData.noShellMarkerInstance == null)
											{
												throw new NullReferenceException();
											}
											markerData.noShellMarkerInstance.SetActive(value: true);
										}
										if (markerDataList != null)
										{
											markerDataList.Add(markerData);
											continue;
										}
										throw new NullReferenceException();
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						throw new IndexOutOfRangeException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			enumerator3.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	private void SetupMasterMarker()
	{
		if (masterImpactMarkerPrefab != null)
		{
			Transform parent = base.transform;
			GameObject gameObject = UnityEngine.Object.Instantiate(masterImpactMarkerPrefab, parent);
			masterImpactMarkerInstance = gameObject;
		}
	}

	private unsafe void UpdateAllGunMarkers()
	{
		//IL_0052: Expected O, but got Ref
		//IL_0378: Expected O, but got Ref
		//IL_0378: Expected O, but got Ref
		//IL_039f: Expected O, but got Ref
		//IL_0090: Expected O, but got Ref
		//IL_00c7: Expected O, but got Ref
		//IL_00f4: Expected O, but got Ref
		//IL_013c: Expected O, but got Ref
		//IL_013c: Expected O, but got I
		//IL_0163: Expected O, but got I
		//IL_0182: Expected O, but got I
		//IL_01ba: Expected O, but got I
		//IL_01f2: Expected O, but got I
		//IL_0225: Expected O, but got I
		//IL_02a1: Expected O, but got I
		//IL_0284: Expected O, but got I
		//IL_02eb: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<MarkerData>.Enumerator enumerator = default(List<MarkerData>.Enumerator);
		Vector3 euler = default(Vector3);
		float num = default(float);
		object obj = default(object);
		object obj2 = default(object);
		object obj3 = default(object);
		object obj4 = default(object);
		object obj5 = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = (object)this.turretController == null;
				Transform transform = (Transform)(&enumerator);
				if (!flag)
				{
					Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
					Vector3 vector = (Quaternion)(&num) * (Vector3)(&obj);
					TurretController turretController = this.turretController;
					bool flag2 = (object)this.turretController == null;
					transform = (Transform)(&obj2);
					if (!flag2)
					{
						bool flag3 = (object)turretController.turretBase == null;
						transform = (Transform)(&obj2);
						if (!flag3)
						{
							Vector3 localPosition = turretController.turretBase.localPosition;
							bool flag4 = obj3 == null;
							transform = (Transform)(&obj4);
							if (!flag4)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ stack_8_v3+10]");
								bool flag5 = (nint)0 == 0;
								transform = (Transform)(&obj4);
								if (!flag5)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ stack_8_v3+18]");
									if ((nint)0 != 0)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ stack_8_v3+18]");
										((Transform)0).localPosition = (Vector3)(&obj5);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ stack_8_v3+10]");
										bool flag6 = (nint)0 == 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ stack_8_v3+10]");
										transform = (Transform)0;
										if (flag6)
										{
											break;
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ stack_8_v3+10]");
										ShellBlueprint chamberedShellBlueprint = ((GunController)0).ChamberedShellBlueprint;
										if ((UnityEngine.Object)null == (UnityEngine.Object)null)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ stack_8_v3+30]");
											if (!((string)0 != "NoShell"))
											{
												continue;
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ stack_8_v3+20]");
											if ((UnityEngine.Object)0 != null)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ stack_8_v3+20]");
												UnityEngine.Object.Destroy((UnityEngine.Object)0);
											}
											if (!((UnityEngine.Object)null != (UnityEngine.Object)null))
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ stack_8_v3+28]");
												if ((nint)0 != 0)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ stack_8_v3+28]");
													((GameObject)0).SetActive(value: true);
												}
												continue;
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ stack_8_v3+18]");
											GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(null, (Transform)0);
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ stack_8_v3+28]");
											if ((nint)0 != 0)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ stack_8_v3+28]");
												((GameObject)0).SetActive(value: false);
											}
											continue;
										}
										transform = null;
										throw new NullReferenceException();
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	private unsafe void UpdateMasterMarker()
	{
		//IL_00b7: Expected O, but got Ref
		//IL_00b7: Expected O, but got Ref
		//IL_009f: Expected O, but got Ref
		if (masterImpactMarkerInstance != null)
		{
			TurretController turretController = this.turretController;
			float num = CalculateProjectedRangeFromElevation(turretController._003CDesiredElevation_003Ek__BackingField);
			Vector3 euler = default(Vector3);
			Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
			object obj = default(object);
			Vector3 vector = (Quaternion)(&obj) * (Vector3)(&euler);
			TurretController turretController2 = this.turretController;
			Vector3 localPosition = turretController2.turretBase.localPosition;
			Transform transform = masterImpactMarkerInstance.transform;
			float num2 = default(float);
			transform.localPosition = (Vector3)(&num2);
		}
	}

	private unsafe float CalculateProjectedRangeFromElevation(float elevation)
	{
		//IL_0320: Expected F4, but got I4
		//IL_0045: Expected F4, but got I4
		//IL_0096: Expected F4, but got O
		//IL_00a1: Expected O, but got I4
		//IL_00aa: Expected F4, but got I4
		//IL_01a3: Expected O, but got Ref
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Expected O, but got Unknown
		//IL_02bd: Expected F4, but got I
		TurretController turretController = this.turretController;
		bool flag = (object)this.turretController == null;
		float num = 0f;
		ImpactMarkerManager impactMarkerManager = this;
		if (!flag)
		{
			List<GunController> guns = turretController.guns;
			bool flag2 = turretController.guns == null;
			num = 0f;
			impactMarkerManager = this;
			if (!flag2)
			{
				if (guns._size == 0)
				{
					goto IL_02de;
				}
				TurretController turretController2 = this.turretController;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<GunController>.Enumerator enumerator = default(List<GunController>.Enumerator);
				num = (float)enumerator;
				object obj = 0;
				float num2 = 0f;
				List<GunController>.Enumerator enumerator2 = default(List<GunController>.Enumerator);
				UnityEngine.Object obj2 = default(UnityEngine.Object);
				while (enumerator2.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if (obj2 != null)
					{
						if ((object)obj2 == null)
						{
							throw new NullReferenceException();
						}
						ShellBlueprint chamberedShellBlueprint = ((GunController)obj2).ChamberedShellBlueprint;
						if (chamberedShellBlueprint != null)
						{
							num = ((GunController)obj2).MapElevationToRange(elevation);
							num2 += num;
							obj++;
						}
					}
				}
				enumerator2.Dispose();
				if ((nint)obj > 0)
				{
					return num2 / (float)obj;
				}
				TurretController turretController3 = this.turretController;
				bool flag3 = (object)this.turretController == null;
				impactMarkerManager = (ImpactMarkerManager)(&enumerator2);
				if (!flag3)
				{
					impactMarkerManager = (ImpactMarkerManager)(object)turretController3.guns;
					if (turretController3.guns != null)
					{
						if ((nint)((MonoBehaviour)impactMarkerManager).m_CancellationTokenSource > 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							UnityEngine.Object obj3 = default(UnityEngine.Object);
							if (obj3 != null)
							{
								TurretController turretController4 = this.turretController;
								if ((object)this.turretController != null && turretController4.guns != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
									if ((object)obj3 != null)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ stack_8_v5 (UnityEngine.Object)+B8]");
										return 0f;
									}
								}
								goto IL_02ec;
							}
						}
						goto IL_02de;
					}
				}
			}
		}
		goto IL_02ec;
		IL_02ec:
		throw new NullReferenceException();
		IL_02de:
		return 500f;
	}

	public ImpactMarkerManager()
	{
		List<MarkerData> list = new List<MarkerData>();
		markerDataList = list;
		base._002Ector();
	}
}
