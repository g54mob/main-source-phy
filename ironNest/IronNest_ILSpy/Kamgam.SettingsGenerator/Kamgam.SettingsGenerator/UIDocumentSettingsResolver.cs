using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using Kamgam.LocalizationForSettings;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kamgam.SettingsGenerator;

public class UIDocumentSettingsResolver : MonoBehaviour
{
	public delegate SettingResolverForVisualElement CreateResolverDelegate(UIDocumentSettingsResolver documentResolver, VisualElement element, List<string> uniqueClassNames);

	public SettingsProvider SettingsProvider;

	public LocalizationProvider LocalizationProvider;

	[NonSerialized]
	public CreateResolverDelegate CustomCreateResolverMethod;

	protected UIDocument _document;

	public UIDocument Document
	{
		get
		{
			if (_document == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
				UIDocument document = default(UIDocument);
				_document = document;
			}
			return _document;
		}
	}

	public void CreateOrUpdateResolvers()
	{
		//IL_0092: Expected O, but got I4
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		//IL_00ea: Expected I, but got O
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Expected O, but got Unknown
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Expected O, but got Unknown
		Logger.Log("Creating resolver on UIDocument.");
		UIDocument document = Document;
		if (document != null)
		{
			UIDocument document2 = Document;
			Transform transform = document2.transform;
			SettingResolverForVisualElement[] componentsInChildren = transform.GetComponentsInChildren<SettingResolverForVisualElement>();
			bool flag = (nint)componentsInChildren < 0;
			object obj = componentsInChildren.Length - 1;
			if (!flag)
			{
				object obj2 = obj * 8;
				object obj3 = obj2 + 32;
				object obj4 = obj3 + (object)componentsInChildren;
				do
				{
					GameObject obj5 = ((Component)obj4).gameObject;
					nint num = (nint)typeof(UnityEngine.Object);
					UnityEngine.Object.Destroy(obj5);
					obj--;
					obj4 -= 8;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rcx_v34 (Il2CppClass<UnityEngine.Object>)+E4]");
				}
				while ((nint)0 >= (nint)0);
			}
			List<string> uniqueClassNames = new List<string>();
			int num2 = createOrUpdateResolvers<Toggle, ToggleUIElementResolver>(uniqueClassNames);
			int num3 = createOrUpdateResolvers<DropdownField, DropdownFieldUIElementResolver>(uniqueClassNames);
			int num4 = createOrUpdateResolvers<Slider, SliderUIElementResolver>(uniqueClassNames);
			int num5 = createOrUpdateResolvers<TextField, TextFieldUIElementResolver>(uniqueClassNames);
			if (CustomCreateResolverMethod != null)
			{
				int num6 = createOrUpdateCustomResolvers(uniqueClassNames);
			}
			int num7 = default(int);
			string text = num7.ToString();
			string message = "Created " + text + " resolvers on UIDocument.";
			Logger.LogMessage(message);
			if (num7 == 0)
			{
				string message2 = "Please add a class name starting with '" + SettingResolverForVisualElement.SettingsClassNamePrefix + "' to each element that you wish to mark as a setting.\nDon't forget to assign Settings IDs to the resolvers afterwards.";
				Logger.LogWarning(message2);
			}
		}
		else
		{
			Logger.LogError("No UIDocument found: There is no UIDocument Component on the selected object -> aborting.");
		}
	}

	public unsafe static UIDocumentSettingsResolver GetOrCreateResolversRoot(GameObject gameObjectWithUIDocument)
	{
		//IL_0181: Expected O, but got Ref
		//IL_01a5: Expected O, but got Ref
		UnityEngine.Object obj = default(UnityEngine.Object);
		Transform transform3;
		if ((object)gameObjectWithUIDocument != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			if (!(obj != null))
			{
				return null;
			}
			Transform transform = gameObjectWithUIDocument.transform;
			if ((object)transform != null)
			{
				Transform transform2 = transform.Find("SettingResolvers");
				bool flag = transform2 == null;
				bool flag2 = !flag;
				transform3 = transform2;
				if (flag2)
				{
					goto IL_0273;
				}
				GameObject gameObject = new GameObject("SettingResolvers");
				if ((object)gameObject != null)
				{
					Transform transform4 = gameObject.transform;
					if ((object)obj != null)
					{
						GameObject gameObject2 = ((Component)obj).gameObject;
						if ((object)gameObject2 != null)
						{
							Transform parentInternal = gameObject2.transform;
							if ((object)transform4 != null)
							{
								transform4.parentInternal = parentInternal;
								Transform transform5 = gameObject.transform;
								if ((object)transform5 != null)
								{
									Quaternion quaternion = default(Quaternion);
									transform5.rotation = (Quaternion)(&quaternion);
									Transform transform6 = gameObject.transform;
									if ((object)transform6 != null)
									{
										transform6.localPosition = (Vector3)(&quaternion);
										Transform transform7 = gameObject.transform;
										transform3 = transform7;
										goto IL_0273;
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_0248;
		IL_0248:
		return (UIDocumentSettingsResolver)(object)new NullReferenceException();
		IL_0273:
		if ((object)transform3 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696150");
			bool flag3 = obj == null;
			bool flag4 = !flag3;
			UnityEngine.Object result = obj;
			if (!flag4)
			{
				GameObject gameObject3 = transform3.gameObject;
				if ((object)gameObject3 == null)
				{
					goto IL_0248;
				}
				UIDocumentSettingsResolver uIDocumentSettingsResolver = gameObject3.AddComponent<UIDocumentSettingsResolver>();
				result = uIDocumentSettingsResolver;
			}
			return (UIDocumentSettingsResolver)result;
		}
		goto IL_0248;
	}

	private unsafe int createOrUpdateResolvers<TVisualElement, TResolver>(List<string> uniqueClassNames)
	{
		//IL_04b5: Expected I4, but got O
		//IL_04c5: Expected O, but got I
		//IL_03ff: Expected O, but got Ref
		//IL_0417: Expected O, but got I
		//IL_012d: Expected O, but got I
		//IL_01cd: Expected O, but got I
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Expected O, but got Unknown
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r8 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		UIDocument document = Document;
		if (!(document != null))
		{
			goto IL_042b;
		}
		UIDocument document2 = Document;
		if ((object)document2 != null)
		{
			VisualElement rootVisualElement = document2.rootVisualElement;
			if (rootVisualElement == null)
			{
				goto IL_042b;
			}
			UIDocument document3 = Document;
			if ((object)document3 != null)
			{
				VisualElement rootVisualElement2 = document3.rootVisualElement;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18076FAC0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18093E1B0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18093FA60");
				int num = 0;
				object obj2 = default(object);
				VisualElement visualElement = default(VisualElement);
				object obj6 = default(object);
				while (true)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v259 @ stack_18_v4+38]");
					object obj = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180844AA0");
					if (obj2 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v259 @ stack_18_v4+38]");
						object obj3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18084C8D0");
						if (!SettingResolverForVisualElement.HasSettingClass(visualElement))
						{
							continue;
						}
						string settingClassName = SettingResolverForVisualElement.GetSettingClassName(visualElement);
						if (uniqueClassNames != null)
						{
							if (!uniqueClassNames.Contains(settingClassName))
							{
								uniqueClassNames.Add(settingClassName);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v259 @ stack_18_v4+38]");
								object obj4 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18076E690");
								num++;
								continue;
							}
							string[] array = new string[7];
							bool flag = array == null;
							VisualElement typeFromHandle = (VisualElement)(object)typeof(string[]);
							if (!flag)
							{
								array[0] = "The class name '";
								if (array.Length <= 1)
								{
									break;
								}
								array[1] = settingClassName;
								if (array.Length > 2)
								{
									array[2] = "' on '";
									typeFromHandle = (VisualElement)(array + 48);
									if (visualElement != null)
									{
										string text = visualElement.name;
										if (array.Length > 3)
										{
											array[3] = text;
											if (array.Length > 4)
											{
												array[4] = "' has already been used. Skipping '";
												string text2 = visualElement.name;
												if (array.Length > 5)
												{
													array[5] = text2;
													typeFromHandle = (VisualElement)(array + 72);
													if (array.Length > 6)
													{
														array[6] = "'.";
														string message = string.Concat(array);
														Logger.LogError(message);
														continue;
													}
													throw new IndexOutOfRangeException();
												}
												throw new IndexOutOfRangeException();
											}
											throw new IndexOutOfRangeException();
										}
										throw new IndexOutOfRangeException();
									}
									throw new NullReferenceException();
								}
								throw new IndexOutOfRangeException();
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					object obj5 = (object)(&obj6);
					object obj7 = obj5;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v525 @ rax_v31+38]");
					object obj8 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180840240");
					return num;
				}
				throw new IndexOutOfRangeException();
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
		IL_042b:
		Logger.LogWarning("No root for document found. Maybe it's disabled or you are in PrefabMode?");
		return 0;
	}

	private int createOrUpdateCustomResolvers(List<string> uniqueClassNames)
	{
		//IL_0154: Expected I4, but got O
		if (CustomCreateResolverMethod != null)
		{
			UIDocument document = Document;
			if ((object)document != null)
			{
				VisualElement rootVisualElement = document.rootVisualElement;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18076FAC0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18093E1B0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18093FA60");
				int num = 0;
				UQueryState<VisualElement>.Enumerator enumerator = default(UQueryState<VisualElement>.Enumerator);
				UnityEngine.Object obj = default(UnityEngine.Object);
				while (true)
				{
					if (enumerator.MoveNext())
					{
						VisualElement current = enumerator.Current;
						CreateResolverDelegate customCreateResolverMethod = CustomCreateResolverMethod;
						if (CustomCreateResolverMethod == null)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v181.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
						if (obj != null)
						{
							num++;
						}
						continue;
					}
					enumerator.Dispose();
					return num;
				}
				throw new NullReferenceException();
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		return 0;
	}

	public unsafe TResolver CreateGameObjectWithResolver<TVisualElement, TResolver>(TVisualElement element)
	{
		//IL_0039: Expected O, but got I
		//IL_0149: Expected O, but got Ref
		//IL_016d: Expected O, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r8 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)0);
		if ((object)typeFromHandle != null)
		{
			string text = typeFromHandle.Name;
			string settingClassName = SettingResolverForVisualElement.GetSettingClassName((VisualElement)element);
			string text2 = text + " (" + settingClassName + ")";
			GameObject gameObject = new GameObject(text2);
			if ((object)gameObject != null)
			{
				Transform transform = gameObject.transform;
				if ((object)this != null)
				{
					Transform parentInternal = base.transform;
					if ((object)transform != null)
					{
						transform.parentInternal = parentInternal;
						Transform transform2 = gameObject.transform;
						if ((object)transform2 != null)
						{
							Quaternion quaternion = default(Quaternion);
							transform2.rotation = (Quaternion)(&quaternion);
							Transform transform3 = gameObject.transform;
							if ((object)transform3 != null)
							{
								transform3.localPosition = (Vector3)(&quaternion);
								SettingResolverForVisualElement settingResolverForVisualElement = (SettingResolverForVisualElement)gameObject.AddComponent<TResolver>();
								if ((object)settingResolverForVisualElement != null)
								{
									settingResolverForVisualElement.BindTo((VisualElement)element);
									if (SettingsProvider != null)
									{
										_ = SettingsProvider;
									}
									if (LocalizationProvider != null)
									{
										_ = LocalizationProvider;
									}
									return (TResolver)settingResolverForVisualElement;
								}
							}
						}
					}
				}
			}
		}
		return (TResolver)new NullReferenceException();
	}
}
