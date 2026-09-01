using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Kamgam.SettingsGenerator;

public class LightDetector
{
	public delegate void OnNewLightFoundDelegate(Light light);

	public static bool ScanAfterSceneLoad = true;

	public OnNewLightFoundDelegate OnNewLightFound;

	private static LightDetector _instance;

	protected List<Light> _lights;

	private List<GameObject> _tmpRootGameObjects;

	private List<Light> _tmpLights;

	public static LightDetector Instance
	{
		get
		{
			//IL_008f: Expected I, but got O
			//IL_0069: Expected I, but got O
			bool flag = _instance != null;
			nint num = (nint)typeof(LightDetector);
			if (!flag)
			{
				LightDetector instance = new LightDetector();
				_instance = instance;
				num = (nint)typeof(LightDetector);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v4 (Il2CppClass<Kamgam.SettingsGenerator.LightDetector>)+E4]");
			if ((nint)0 == 0)
			{
				return _instance;
			}
			return _instance;
		}
	}

	public List<Light> Lights => _lights;

	private LightDetector()
	{
		List<Light> lights = new List<Light>(20);
		_lights = lights;
		List<GameObject> tmpRootGameObjects = new List<GameObject>(20);
		_tmpRootGameObjects = tmpRootGameObjects;
		List<Light> tmpLights = new List<Light>(20);
		_tmpLights = tmpLights;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		if (ScanAfterSceneLoad)
		{
			int sceneCount = SceneManager.sceneCount;
			bool flag = sceneCount <= 0;
			int num = 0;
			if (!flag)
			{
				do
				{
					Scene sceneAt = SceneManager.GetSceneAt(num);
					ScanScene(sceneAt);
					num++;
				}
				while (num < sceneCount);
			}
		}
		UnityAction<Scene, LoadSceneMode> value = onSceneLoaded;
		SceneManager.sceneLoaded += value;
	}

	private void onSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (ScanAfterSceneLoad)
		{
			ScanScene(scene);
		}
	}

	public Light GetPrimaryLight()
	{
		List<Light> lights = _lights;
		bool flag = _lights == null;
		LightDetector lightDetector = this;
		Component component;
		if (!flag)
		{
			if (lights._size > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<Light>.Enumerator enumerator = default(List<Light>.Enumerator);
				UnityEngine.Object obj = default(UnityEngine.Object);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if (!(obj != null))
					{
						continue;
					}
					if ((object)obj != null)
					{
						if (!((Behaviour)obj).isActiveAndEnabled)
						{
							continue;
						}
						GameObject gameObject = ((Component)obj).gameObject;
						bool flag2 = (object)gameObject == null;
						component = (Component)obj;
						if (!flag2)
						{
							if (gameObject.activeInHierarchy)
							{
								LightType type = ((Light)obj).type;
								if (type == LightType.Directional)
								{
									enumerator.Dispose();
									return (Light)obj;
								}
							}
							continue;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				enumerator.Dispose();
				lightDetector = (LightDetector)(object)_lights;
				if (_lights == null)
				{
					goto IL_0251;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<Light>.Enumerator enumerator2 = default(List<Light>.Enumerator);
				UnityEngine.Object obj2 = default(UnityEngine.Object);
				while (enumerator2.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if (!(obj2 != null))
					{
						continue;
					}
					if ((object)obj2 != null)
					{
						if (((Behaviour)obj2).isActiveAndEnabled)
						{
							GameObject gameObject2 = ((Component)obj2).gameObject;
							if ((object)gameObject2 == null)
							{
								throw new NullReferenceException();
							}
							if (gameObject2.activeInHierarchy)
							{
								enumerator2.Dispose();
								return (Light)obj2;
							}
						}
						continue;
					}
					throw new NullReferenceException();
				}
				enumerator2.Dispose();
			}
			return null;
		}
		goto IL_0251;
		IL_0251:
		component = (Component)(object)lightDetector;
		throw new NullReferenceException();
	}

	public void Add(Light light)
	{
		if (light != null && !_lights.Contains(light))
		{
			_lights.Add(light);
			OnNewLightFoundDelegate onNewLightFound = OnNewLightFound;
			if (OnNewLightFound != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v88.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			}
		}
	}

	public void ScanAllScenes()
	{
		int sceneCount = SceneManager.sceneCount;
		bool flag = sceneCount <= 0;
		int num = 0;
		if (!flag)
		{
			do
			{
				Scene sceneAt = SceneManager.GetSceneAt(num);
				ScanScene(sceneAt);
				num++;
			}
			while (num < sceneCount);
		}
	}

	public void ScanActiveScene()
	{
		Scene activeScene = SceneManager.GetActiveScene();
		ScanScene(activeScene);
	}

	public unsafe void ScanScene(Scene scene)
	{
		//IL_0034: Expected O, but got I
		//IL_0034: Expected I4, but got O
		//IL_0034: Expected O, but got I
		//IL_012d: Expected O, but got Ref
		//IL_0168: Expected O, but got I
		//IL_0168: Expected O, but got I
		//IL_0490: Expected O, but got I4
		//IL_0490: Expected O, but got I
		//IL_069a: Expected O, but got I4
		List<GameObject> tmpRootGameObjects = _tmpRootGameObjects;
		if (_tmpRootGameObjects != null)
		{
			int version = tmpRootGameObjects._version + 1;
			tmpRootGameObjects._version = version;
			IntPtr intPtr = default(IntPtr);
			((GameObject)0).GetComponentsInChildren((byte)(int)scene != 0, (List<Light>)(nint)intPtr);
			object obj = default(object);
			if (obj == null)
			{
				tmpRootGameObjects._size = 0;
			}
			else
			{
				tmpRootGameObjects._size = 0;
				if (tmpRootGameObjects._size > 0)
				{
					Array.Clear(tmpRootGameObjects._items, 0, tmpRootGameObjects._size);
				}
			}
			Scene scene2 = default(Scene);
			scene2.GetRootGameObjects(_tmpRootGameObjects);
			if (_tmpRootGameObjects != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				nint num = 0;
				List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
				GameObject gameObject = default(GameObject);
				object obj2 = default(object);
				List<Light>.Enumerator enumerator2 = default(List<Light>.Enumerator);
				Light light = default(Light);
				Light light2 = default(Light);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					List<Light> tmpLights = _tmpLights;
					bool flag = _tmpLights == null;
					List<Light> list = (List<Light>)(&enumerator);
					if (!flag)
					{
						int version2 = tmpLights._version + 1;
						tmpLights._version = version2;
						((GameObject)0).GetComponentsInChildren((byte)(&gameObject) != 0, (List<Light>)0);
						if (obj2 == null)
						{
							tmpLights._size = 0;
						}
						else
						{
							tmpLights._size = 0;
							if (tmpLights._size > 0)
							{
								Array.Clear(tmpLights._items, 0, tmpLights._size);
							}
						}
						if ((object)gameObject != null)
						{
							gameObject.GetComponentsInChildren(includeInactive: true, _tmpLights);
							list = _tmpLights;
							if (_tmpLights != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
								num = 0;
								while (enumerator2.MoveNext())
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
									if (_lights != null)
									{
										bool flag2 = _lights.Contains(light);
										light2 = light;
										num = 0;
										if (!flag2)
										{
											if (_lights == null)
											{
												throw new NullReferenceException();
											}
											_lights.Add(light);
											OnNewLightFoundDelegate onNewLightFound = OnNewLightFound;
											bool flag3 = OnNewLightFound == null;
											light2 = light;
											num = 0;
											if (!flag3)
											{
												num = ((Delegate)onNewLightFound).method;
												Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v822.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
												light2 = light;
											}
										}
										continue;
									}
									throw new NullReferenceException();
								}
								enumerator2.Dispose();
								continue;
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				enumerator.Dispose();
				List<Light> tmpLights2 = _tmpLights;
				if (_tmpLights != null)
				{
					int version3 = tmpLights2._version + 1;
					tmpLights2._version = version3;
					((List<GameObject>.Enumerator*)null)->Dispose();
					object obj3 = default(object);
					int includeInactive;
					int num2;
					if (obj3 == null)
					{
						tmpLights2._size = 0;
						includeInactive = 0;
						num2 = (int)num;
					}
					else
					{
						num2 = tmpLights2._size;
						tmpLights2._size = 0;
						bool flag4 = tmpLights2._size <= 0;
						includeInactive = 0;
						if (!flag4)
						{
							Array.Clear(tmpLights2._items, 0, tmpLights2._size);
							includeInactive = 0;
						}
					}
					List<GameObject> tmpRootGameObjects2 = _tmpRootGameObjects;
					if (_tmpRootGameObjects != null)
					{
						int version4 = tmpRootGameObjects2._version + 1;
						tmpRootGameObjects2._version = version4;
						((GameObject)0).GetComponentsInChildren((byte)includeInactive != 0, (List<Light>)num2);
						object obj4 = default(object);
						if (obj4 == null)
						{
							tmpRootGameObjects2._size = 0;
						}
						else
						{
							tmpRootGameObjects2._size = 0;
							if (tmpRootGameObjects2._size > 0)
							{
								Array.Clear(tmpRootGameObjects2._items, 0, tmpRootGameObjects2._size);
							}
						}
						List<Light> lights = _lights;
						bool flag5 = (nint)_lights < 0;
						if (_lights != null)
						{
							int num3 = lights._size - 1;
							GameObject gameObject2 = (GameObject)(object)light2;
							Component component = (Component)(object)gameObject;
							if (flag5)
							{
								return;
							}
							UnityEngine.Object obj5 = default(UnityEngine.Object);
							Component component2 = default(Component);
							while (_lights != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								bool flag7;
								GameObject gameObject4;
								Component component3;
								if (obj5 != null)
								{
									if (_lights == null)
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
									if ((object)component2 == null)
									{
										break;
									}
									GameObject gameObject3 = component2.gameObject;
									bool flag6 = gameObject3 == null;
									flag7 = (flag6 ? 1 : 0) < (false ? 1 : 0);
									bool flag8 = !flag6;
									gameObject2 = gameObject3;
									component = component2;
									gameObject4 = gameObject3;
									component3 = component2;
									if (flag8)
									{
										goto IL_0681;
									}
								}
								flag7 = (nint)_lights < 0;
								if (_lights == null)
								{
									break;
								}
								_lights.RemoveAt(num3);
								gameObject4 = gameObject2;
								component3 = component;
								goto IL_0681;
								IL_0681:
								num3--;
								object obj6 = !flag7;
								gameObject2 = gameObject4;
								component = component3;
								if (obj6 == null)
								{
									return;
								}
							}
						}
					}
				}
			}
		}
		throw new NullReferenceException();
	}

	public void Defrag()
	{
		//IL_0103: Expected O, but got I4
		List<Light> lights = _lights;
		bool flag = (nint)_lights < 0;
		int num = lights._size - 1;
		if (flag)
		{
			return;
		}
		UnityEngine.Object obj = default(UnityEngine.Object);
		Component component = default(Component);
		GameObject gameObject2 = default(GameObject);
		object obj2;
		do
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			bool flag3;
			GameObject gameObject3;
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				GameObject gameObject = component.gameObject;
				bool flag2 = gameObject == null;
				flag3 = (flag2 ? 1 : 0) < (false ? 1 : 0);
				bool flag4 = !flag2;
				gameObject2 = gameObject;
				gameObject3 = gameObject;
				if (flag4)
				{
					goto IL_00ea;
				}
			}
			flag3 = (nint)_lights < 0;
			_lights.RemoveAt(num);
			gameObject3 = gameObject2;
			goto IL_00ea;
			IL_00ea:
			num--;
			obj2 = !flag3;
			gameObject2 = gameObject3;
		}
		while (obj2 != null);
	}
}
