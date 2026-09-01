using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using Kamgam.SettingsGenerator;
using UnityEngine;

public class CatCustomizationController : MonoBehaviour
{
	private GameObject catPrefab;

	private GameObject kittenPrefab;

	private Transform defaultTransform;

	private List<Material> FurMaterials;

	private List<Material> BodyMaterials;

	private List<Material> EyesMaterials;

	private SettingsProvider settingsProvider;

	private PickUpZoomTarget clipBoard;

	private GameObject catCameraZone;

	public bool isKitten;

	public int hatType;

	public string catName;

	public int bodyColor;

	public int eyesColor;

	public bool catEnabled;

	private CatCustomization currentCatCustomization;

	private CatController currentCatController;

	private Dictionary<string, BlendShapeKey> blendShapeKeys;

	public void ChangeToKitten()
	{
		bool flag = !catEnabled;
		isKitten = true;
		if (!flag)
		{
			ChangeModel(kittenPrefab);
		}
		else if (currentCatCustomization != null)
		{
			GameObject obj = currentCatCustomization.gameObject;
			UnityEngine.Object.DestroyImmediate(obj);
		}
	}

	public void ChangeToAdultCat()
	{
		bool flag = !catEnabled;
		isKitten = false;
		if (!flag)
		{
			ChangeModel(catPrefab);
		}
		else if (currentCatCustomization != null)
		{
			GameObject obj = currentCatCustomization.gameObject;
			UnityEngine.Object.DestroyImmediate(obj);
		}
	}

	public void ChangeBodyColor(int index)
	{
		bodyColor = index;
		if (currentCatCustomization != null)
		{
			CatCustomization catCustomization = currentCatCustomization;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Material material = default(Material);
			((Renderer)catCustomization.bodyMesh).SetMaterial(material);
			Material material2 = default(Material);
			((Renderer)catCustomization.furMesh).SetMaterial(material2);
		}
	}

	public unsafe void ChangeEyesColor(int index)
	{
		//IL_0056: Expected O, but got I4
		//IL_008d: Expected O, but got I4
		//IL_0095: Expected O, but got Ref
		//IL_00d2: Expected O, but got I4
		//IL_00da: Expected O, but got Ref
		//IL_0114: Expected O, but got Ref
		//IL_01d0: Expected O, but got Ref
		//IL_0157: Expected O, but got I
		//IL_017e: Expected O, but got Ref
		eyesColor = index;
		if (!(currentCatCustomization != null))
		{
			return;
		}
		List<Material> eyesMaterials = EyesMaterials;
		CatCustomization catCustomization = currentCatCustomization;
		bool flag = EyesMaterials == null;
		UnityEngine.Object obj = null;
		object obj2 = 0;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			bool flag2 = (object)currentCatCustomization == null;
			nint num = 0;
			obj = (UnityEngine.Object)index;
			UnityEngine.Object obj3 = default(UnityEngine.Object);
			obj2 = (object)(&obj3);
			if (!flag2)
			{
				eyesMaterials = (List<Material>)(object)catCustomization.eyesMesh;
				bool flag3 = (object)catCustomization.eyesMesh == null;
				num = 0;
				obj = (UnityEngine.Object)index;
				obj2 = (object)(&obj3);
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9A9C0");
					object obj4 = default(object);
					bool flag4 = obj4 == null;
					num = 0;
					obj = null;
					obj2 = (object)(&obj3);
					if (!flag4)
					{
						if ((object)obj3 != null)
						{
							object obj5 = obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v276 @ rdx_v12+40]");
							obj = (UnityEngine.Object)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj6 = default(object);
							bool flag5 = obj6 == null;
							num = 0;
							obj2 = (object)(&obj3);
							eyesMaterials = (List<Material>)(object)obj3;
							if (flag5)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								object obj7 = default(object);
								throw obj7;
							}
						}
						eyesMaterials = (List<Material>)(object)catCustomization.eyesMesh;
						bool flag6 = (object)catCustomization.eyesMesh == null;
						num = 0;
						obj = obj3;
						obj2 = (object)(&obj3);
						if (!flag6)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9C660");
							return;
						}
					}
				}
			}
		}
		throw new NullReferenceException();
	}

	public void ChangeHatState(int type)
	{
		hatType = type;
		if (currentCatCustomization != null)
		{
			currentCatCustomization.SetHatType(type);
		}
	}

	public void SetBlendShapeValue(int value, bool eyes, bool body, bool fur, bool whiskers, int blendShapeIndex)
	{
		BlendShapeKey blendShapeKey = new BlendShapeKey();
		blendShapeKey.eyes = eyes;
		blendShapeKey.body = body;
		bool fur2 = default(bool);
		blendShapeKey.fur = fur2;
		bool whiskers2 = default(bool);
		blendShapeKey.whiskers = whiskers2;
		int blendShapeIndex2 = default(int);
		blendShapeKey.blendShapeIndex = blendShapeIndex2;
		blendShapeKey.value = value;
		bool flag = default(bool);
		string text = flag.ToString();
		bool flag2 = default(bool);
		string text2 = flag2.ToString();
		string text3 = fur2.ToString();
		string text4 = whiskers2.ToString();
		string text5 = blendShapeIndex2.ToString();
		string key = text + text2 + text3 + text4 + text5;
		if (!blendShapeKeys.ContainsKey(key))
		{
			blendShapeKeys.Add(key, blendShapeKey);
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
		}
		if (currentCatCustomization != null)
		{
			currentCatCustomization.SetBlendShapeValue(blendShapeKey);
		}
	}

	public int GetBlendShapeValue(bool eyes, bool body, bool fur, bool whiskers, int blendShapeIndex)
	{
		//IL_0229: Expected I4, but got O
		BlendShapeKey blendShapeKey = new BlendShapeKey();
		blendShapeKey.eyes = eyes;
		blendShapeKey.body = body;
		blendShapeKey.fur = fur;
		bool whiskers2 = default(bool);
		blendShapeKey.whiskers = whiskers2;
		int blendShapeIndex2 = default(int);
		blendShapeKey.blendShapeIndex = blendShapeIndex2;
		string[] array = new string[5];
		bool flag = default(bool);
		string text = flag.ToString();
		if (array.Length > 0)
		{
			array[0] = text;
			bool flag2 = default(bool);
			string text2 = flag2.ToString();
			if (array.Length > 1)
			{
				array[1] = text2;
				bool flag3 = default(bool);
				string text3 = flag3.ToString();
				if (array.Length > 2)
				{
					array[2] = text3;
					string text4 = whiskers2.ToString();
					if (array.Length > 3)
					{
						array[3] = text4;
						string text5 = blendShapeIndex2.ToString();
						if (array.Length > 4)
						{
							array[4] = text5;
							string key = string.Concat(array);
							if (!blendShapeKeys.ContainsKey(key))
							{
								return 0;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ stack_-18+18]");
							return 0;
						}
					}
				}
			}
		}
		IndexOutOfRangeException ex = new IndexOutOfRangeException();
		return (int)ex;
	}

	public unsafe void StartCustomization()
	{
		//IL_0073: Expected O, but got Ref
		//IL_00ad: Expected O, but got Ref
		if (currentCatController != null)
		{
			currentCatController.StartCustomization();
			Transform transform = currentCatCustomization.transform;
			Vector3 position = defaultTransform.position;
			object obj = default(object);
			transform.position = (Vector3)(&obj);
			Transform transform2 = currentCatCustomization.transform;
			Quaternion rotation = defaultTransform.rotation;
			object obj2 = default(object);
			transform2.rotation = (Quaternion)(&obj2);
			Transform transform3 = currentCatCustomization.transform;
			transform3.parentInternal = defaultTransform;
		}
	}

	public void StopCustomization()
	{
		if (currentCatController != null)
		{
			currentCatController.StopCustomization();
			Transform transform = currentCatCustomization.transform;
			transform.parentInternal = null;
		}
	}

	public void ChangeCatState(bool state)
	{
		if (!state)
		{
			catEnabled = state;
			GameObject gameObject = clipBoard.gameObject;
			gameObject.SetActive(value: false);
			GameObject gameObject2 = catCameraZone.gameObject;
			gameObject2.SetActive(value: false);
			if (currentCatCustomization != null)
			{
				GameObject obj = currentCatCustomization.gameObject;
				UnityEngine.Object.DestroyImmediate(obj);
			}
		}
		else
		{
			catEnabled = true;
			GameObject gameObject3 = clipBoard.gameObject;
			gameObject3.SetActive(value: true);
			GameObject gameObject4 = catCameraZone.gameObject;
			gameObject4.SetActive(value: true);
			if (!isKitten)
			{
				ChangeModel(catPrefab);
			}
			else
			{
				ChangeModel(kittenPrefab);
			}
		}
	}

	private unsafe void ChangeModel(GameObject prefab)
	{
		//IL_0c72: Expected O, but got I4
		//IL_0c01: Expected I, but got O
		//IL_0077: Expected O, but got I4
		//IL_0163: Expected O, but got Ref
		//IL_0163: Expected O, but got Ref
		//IL_017f: Expected O, but got Ref
		//IL_0187: Expected O, but got Ref
		//IL_011d: Expected O, but got I4
		//IL_01f9: Expected O, but got I
		//IL_0220: Expected O, but got I
		//IL_0cd3: Expected O, but got I
		//IL_0261: Expected O, but got Ref
		//IL_026f: Expected O, but got Ref
		//IL_0294: Expected O, but got Ref
		//IL_02a2: Expected O, but got Ref
		//IL_05d9: Expected O, but got I4
		//IL_02cf: Expected O, but got Ref
		//IL_0617: Expected O, but got I4
		//IL_061f: Expected O, but got Ref
		//IL_0306: Expected O, but got Ref
		//IL_0739: Expected O, but got I4
		//IL_04d7: Expected O, but got I
		//IL_0653: Expected O, but got I4
		//IL_065b: Expected O, but got Ref
		//IL_0784: Expected O, but got I4
		//IL_032a: Expected F4, but got I
		//IL_0350: Expected F4, but got I
		//IL_0361: Expected O, but got I
		//IL_03d0: Expected F4, but got I
		//IL_03f6: Expected F4, but got I
		//IL_0407: Expected O, but got I
		//IL_0688: Expected O, but got I4
		//IL_0690: Expected O, but got Ref
		//IL_07b8: Expected O, but got I4
		//IL_07c0: Expected O, but got Ref
		//IL_0476: Expected F4, but got I
		//IL_049c: Expected F4, but got I
		//IL_04ad: Expected O, but got I
		//IL_052a: Expected F4, but got I
		//IL_0550: Expected F4, but got I
		//IL_055f: Expected O, but got I
		//IL_06e3: Expected O, but got I4
		//IL_07fa: Expected O, but got I4
		//IL_0802: Expected O, but got Ref
		//IL_0837: Expected O, but got Ref
		//IL_08e9: Expected O, but got Ref
		//IL_09b0: Expected O, but got I4
		//IL_086a: Expected I, but got O
		//IL_087a: Expected O, but got I
		//IL_0913: Expected O, but got I4
		//IL_0d1e: Expected O, but got I
		//IL_09fc: Expected O, but got I4
		//IL_0a3d: Expected O, but got I4
		//IL_0a7c: Expected O, but got I4
		//IL_0a84: Expected O, but got Ref
		//IL_0aa0: Expected O, but got Ref
		//IL_0ac3: Expected O, but got Ref
		//IL_0acc: Expected O, but got I4
		//IL_0b0d: Expected O, but got I4
		//IL_0b4c: Expected O, but got I4
		//IL_0b54: Expected O, but got Ref
		//IL_0b70: Expected O, but got Ref
		//IL_0b93: Expected O, but got Ref
		//IL_0b9c: Expected O, but got I4
		//IL_0bd6: Expected O, but got I4
		Transform transform = defaultTransform;
		bool flag = currentCatCustomization != null;
		bool flag2 = !flag;
		UnityEngine.Object obj = null;
		Component component = currentCatCustomization;
		Quaternion quaternion;
		if (!flag2)
		{
			component = currentCatCustomization;
			bool flag3 = (object)currentCatCustomization == null;
			obj = null;
			quaternion = (Quaternion)0;
			if (flag3)
			{
				goto IL_0bf9;
			}
			Transform transform2 = currentCatCustomization.transform;
			transform = transform2;
			obj = null;
		}
		bool flag4 = (object)transform == null;
		quaternion = (Quaternion)0;
		float num = default(float);
		float num2 = default(float);
		nint num5;
		SkinnedMeshRenderer skinnedMeshRenderer;
		if (!flag4)
		{
			Vector3 position = transform.position;
			Quaternion rotation = transform.rotation;
			if (currentCatCustomization != null)
			{
				component = currentCatCustomization;
				bool flag5 = (object)currentCatCustomization == null;
				obj = null;
				quaternion = (Quaternion)0;
				if (flag5)
				{
					goto IL_0bf9;
				}
				GameObject obj2 = currentCatCustomization.gameObject;
				UnityEngine.Object.DestroyImmediate(obj2);
			}
			GameObject gameObject = UnityEngine.Object.Instantiate(prefab, (Vector3)(&num), (Quaternion)(&num2));
			bool flag6 = (object)gameObject == null;
			obj = (UnityEngine.Object)(&num);
			quaternion = (Quaternion)(&num2);
			component = (Component)(object)prefab;
			if (!flag6)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				CatCustomization catCustomization = default(CatCustomization);
				currentCatCustomization = catCustomization;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				UnityEngine.Object obj3 = default(UnityEngine.Object);
				currentCatController = (CatController)obj3;
				component = (Component)(object)blendShapeKeys;
				bool flag7 = blendShapeKeys == null;
				obj = obj3;
				quaternion = (Quaternion)0;
				if (!flag7)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082BED0");
					float num4 = default(float);
					float num3 = num4;
					quaternion = (Quaternion)0;
					Dictionary<string, BlendShapeKey>.Enumerator enumerator = default(Dictionary<string, BlendShapeKey>.Enumerator);
					float num6 = default(float);
					while (enumerator.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						CatCustomization catCustomization2 = currentCatCustomization;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
						bool flag8 = (object)currentCatCustomization == null;
						obj = (UnityEngine.Object)(&catCustomization);
						num5 = 0;
						skinnedMeshRenderer = (SkinnedMeshRenderer)(&num6);
						if (!flag8)
						{
							bool flag9 = (object)catCustomization == null;
							obj = (UnityEngine.Object)(&catCustomization);
							num5 = 0;
							skinnedMeshRenderer = (SkinnedMeshRenderer)(&num6);
							if (!flag9)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ stack_20_v5 (CatCustomization)+10]");
								bool flag10 = (nint)0 == 0;
								obj = (UnityEngine.Object)(&catCustomization);
								if (!flag10)
								{
									skinnedMeshRenderer = catCustomization2.eyesMesh;
									bool flag11 = (object)catCustomization2.eyesMesh == null;
									obj = (UnityEngine.Object)(&catCustomization);
									num5 = 0;
									if (flag11)
									{
										throw new NullReferenceException();
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ stack_20_v5 (CatCustomization)+18]");
									float num7 = 0f;
									SkinnedMeshRenderer eyesMesh = catCustomization2.eyesMesh;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ stack_20_v5 (CatCustomization)+14]");
									nint num8 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ stack_20_v5 (CatCustomization)+18]");
									eyesMesh.SetBlendShapeWeight((int)num8, 0f);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ stack_20_v5 (CatCustomization)+14]");
									obj = (UnityEngine.Object)0;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ stack_20_v5 (CatCustomization)+11]");
								if ((nint)0 != 0)
								{
									skinnedMeshRenderer = catCustomization2.bodyMesh;
									bool flag12 = (object)catCustomization2.bodyMesh == null;
									num5 = 0;
									if (flag12)
									{
										throw new NullReferenceException();
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ stack_20_v5 (CatCustomization)+18]");
									float num7 = 0f;
									SkinnedMeshRenderer bodyMesh = catCustomization2.bodyMesh;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ stack_20_v5 (CatCustomization)+14]");
									nint num9 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ stack_20_v5 (CatCustomization)+18]");
									bodyMesh.SetBlendShapeWeight((int)num9, 0f);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ stack_20_v5 (CatCustomization)+14]");
									obj = (UnityEngine.Object)0;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ stack_20_v5 (CatCustomization)+12]");
								if ((nint)0 != 0)
								{
									skinnedMeshRenderer = catCustomization2.furMesh;
									bool flag13 = (object)catCustomization2.furMesh == null;
									num5 = 0;
									if (flag13)
									{
										throw new NullReferenceException();
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ stack_20_v5 (CatCustomization)+18]");
									float num7 = 0f;
									SkinnedMeshRenderer furMesh = catCustomization2.furMesh;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ stack_20_v5 (CatCustomization)+14]");
									nint num10 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ stack_20_v5 (CatCustomization)+18]");
									furMesh.SetBlendShapeWeight((int)num10, 0f);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ stack_20_v5 (CatCustomization)+14]");
									obj = (UnityEngine.Object)0;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ stack_20_v5 (CatCustomization)+13]");
								bool flag14 = (nint)0 == 0;
								num3 = num;
								quaternion = (Quaternion)0;
								if (!flag14)
								{
									skinnedMeshRenderer = catCustomization2.whiskersMesh;
									bool flag15 = (object)catCustomization2.whiskersMesh == null;
									num5 = 0;
									if (flag15)
									{
										throw new NullReferenceException();
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ stack_20_v5 (CatCustomization)+18]");
									float num7 = 0f;
									SkinnedMeshRenderer whiskersMesh = catCustomization2.whiskersMesh;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ stack_20_v5 (CatCustomization)+14]");
									nint num11 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v934 @ stack_20_v5 (CatCustomization)+18]");
									whiskersMesh.SetBlendShapeWeight((int)num11, 0f);
									num3 = num;
									quaternion = (Quaternion)0;
								}
								continue;
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					enumerator.Dispose();
					bool flag16 = (object)currentCatCustomization == null;
					obj = (UnityEngine.Object)0;
					component = currentCatCustomization;
					if (!flag16)
					{
						currentCatCustomization.SetHatType(hatType);
						bodyColor = bodyColor;
						if (!(currentCatCustomization != null))
						{
							goto IL_0cfc;
						}
						CatCustomization catCustomization3 = currentCatCustomization;
						component = (Component)(object)BodyMaterials;
						bool flag17 = BodyMaterials == null;
						obj = null;
						quaternion = (Quaternion)0;
						if (!flag17)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							component = (Component)(object)FurMaterials;
							bool flag18 = FurMaterials == null;
							obj = (UnityEngine.Object)bodyColor;
							Material material = default(Material);
							quaternion = (Quaternion)(&material);
							if (!flag18)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								bool flag19 = (object)currentCatCustomization == null;
								obj = (UnityEngine.Object)bodyColor;
								Material material2 = default(Material);
								quaternion = (Quaternion)(&material2);
								if (!flag19)
								{
									bool flag20 = (object)catCustomization3.bodyMesh == null;
									obj = (UnityEngine.Object)bodyColor;
									quaternion = (Quaternion)(&material2);
									component = catCustomization3.bodyMesh;
									if (!flag20)
									{
										((Renderer)catCustomization3.bodyMesh).SetMaterial(material);
										bool flag21 = (object)catCustomization3.furMesh == null;
										obj = material;
										quaternion = (Quaternion)0;
										component = catCustomization3.furMesh;
										if (!flag21)
										{
											((Renderer)catCustomization3.furMesh).SetMaterial(material2);
											goto IL_0cfc;
										}
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_0bf9;
		IL_0bf9:
		num5 = (nint)quaternion;
		skinnedMeshRenderer = (SkinnedMeshRenderer)component;
		throw new NullReferenceException();
		IL_0cfc:
		eyesColor = eyesColor;
		bool flag22 = currentCatCustomization != null;
		obj = null;
		quaternion = (Quaternion)0;
		component = currentCatCustomization;
		if (!flag22)
		{
			goto IL_0918;
		}
		CatCustomization catCustomization4 = currentCatCustomization;
		component = (Component)(object)EyesMaterials;
		bool flag23 = EyesMaterials == null;
		obj = null;
		quaternion = (Quaternion)0;
		if (!flag23)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			bool flag24 = (object)currentCatCustomization == null;
			obj = (UnityEngine.Object)eyesColor;
			SkinnedMeshRenderer skinnedMeshRenderer2 = default(SkinnedMeshRenderer);
			quaternion = (Quaternion)(&skinnedMeshRenderer2);
			if (!flag24)
			{
				component = catCustomization4.eyesMesh;
				bool flag25 = (object)catCustomization4.eyesMesh == null;
				obj = (UnityEngine.Object)eyesColor;
				quaternion = (Quaternion)(&skinnedMeshRenderer2);
				if (!flag25)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9A9C0");
					UnityEngine.Object obj4 = default(UnityEngine.Object);
					bool flag26 = (object)obj4 == null;
					obj = null;
					quaternion = (Quaternion)(&skinnedMeshRenderer2);
					if (!flag26)
					{
						if ((object)skinnedMeshRenderer2 != null)
						{
							nint num12 = (nint)obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1432 @ rdx_v42 (Il2CppClass<UnityEngine.Object>)+40]");
							obj = (UnityEngine.Object)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj5 = default(object);
							bool flag27 = obj5 == null;
							num5 = (nint)(&skinnedMeshRenderer2);
							skinnedMeshRenderer = skinnedMeshRenderer2;
							if (flag27)
							{
								GameObject gameObject2 = UnityEngine.Object.Instantiate((GameObject)(object)skinnedMeshRenderer, (Vector3)obj, (Quaternion)num5);
								throw gameObject2;
							}
						}
						component = catCustomization4.eyesMesh;
						bool flag28 = (object)catCustomization4.eyesMesh == null;
						obj = skinnedMeshRenderer2;
						quaternion = (Quaternion)(&skinnedMeshRenderer2);
						if (!flag28)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9C660");
							obj = obj4;
							quaternion = (Quaternion)0;
							goto IL_0918;
						}
					}
				}
			}
		}
		goto IL_0bf9;
		IL_0918:
		PickUpZoomTarget pickUpZoomTarget = clipBoard;
		if ((object)clipBoard != null)
		{
			if (!pickUpZoomTarget.isHeld || !(currentCatController != null))
			{
				return;
			}
			bool flag29 = (object)currentCatController == null;
			obj = null;
			quaternion = (Quaternion)0;
			component = currentCatController;
			if (!flag29)
			{
				currentCatController.StartCustomization();
				component = currentCatCustomization;
				bool flag30 = (object)currentCatCustomization == null;
				obj = null;
				quaternion = (Quaternion)0;
				if (!flag30)
				{
					Transform transform3 = currentCatCustomization.transform;
					bool flag31 = (object)defaultTransform == null;
					obj = defaultTransform;
					quaternion = (Quaternion)0;
					if (!flag31)
					{
						Vector3 position2 = defaultTransform.position;
						bool flag32 = (object)transform3 == null;
						obj = defaultTransform;
						quaternion = (Quaternion)0;
						component = (Component)(&num2);
						if (!flag32)
						{
							transform3.position = (Vector3)(&num);
							component = currentCatCustomization;
							bool flag33 = (object)currentCatCustomization == null;
							obj = (UnityEngine.Object)(&num);
							quaternion = (Quaternion)0;
							if (!flag33)
							{
								Transform transform4 = currentCatCustomization.transform;
								bool flag34 = (object)defaultTransform == null;
								obj = defaultTransform;
								quaternion = (Quaternion)0;
								if (!flag34)
								{
									Quaternion rotation2 = defaultTransform.rotation;
									bool flag35 = (object)transform4 == null;
									obj = defaultTransform;
									quaternion = (Quaternion)0;
									component = (Component)(&num2);
									if (!flag35)
									{
										transform4.rotation = (Quaternion)(&num2);
										component = currentCatCustomization;
										bool flag36 = (object)currentCatCustomization == null;
										obj = (UnityEngine.Object)(&num2);
										quaternion = (Quaternion)0;
										if (!flag36)
										{
											Transform transform5 = currentCatCustomization.transform;
											bool flag37 = (object)transform5 == null;
											obj = null;
											quaternion = (Quaternion)0;
											if (!flag37)
											{
												transform5.parentInternal = defaultTransform;
												return;
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_0bf9;
	}

	public CatCustomizationController()
	{
		Dictionary<string, BlendShapeKey> dictionary = new Dictionary<string, BlendShapeKey>();
		blendShapeKeys = dictionary;
		base._002Ector();
	}
}
