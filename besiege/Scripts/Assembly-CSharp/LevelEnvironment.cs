using System;
using BesiegeDlc;
using UnityEngine;

[Serializable]
public class LevelEnvironment
{
	public LevelSettings.LevelEnvironment env;

	[SerializeField]
	private DlcManager.DlcType dlcType;

	public Material floorMaterial;

	public float floorHeight = -5.05f;

	public bool hasColoredFog;

	public bool hasRenderSettingsFog;

	public Color renderSettingsFogColor = Color.white;

	public float lensFlareIntensity = 2f;

	public bool hasCustomColorCorrectionLut;

	public Texture2D customColorCorrectionLut;

	public Transform envRoot;

	public Transform envParent;

	public bool reparentEnvRoot;

	public Transform localGoalObj;

	public Transform[] physicsGoalChildren;

	public GameObject[] envSetup;

	public MonoBehaviour[] activateComponent;

	public Color dirLightColor;

	public float dirLightIntensity;

	public float dirLightCookieSize;

	public Vector3 dirLightEulerRot;
}
