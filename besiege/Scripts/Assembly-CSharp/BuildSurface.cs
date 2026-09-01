using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Localisation;
using Modding;
using Mono.CSharp;
using Selectors;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using cakeslice;

[AddComponentMenu("Blocks/Block Behaviours/Surface/BuildSurface")]
public class BuildSurface : BlockBehaviour
{
	[Serializable]
	public class SurfaceMaterialType
	{
		public bool burnable;

		public bool shadows = true;

		public bool breakable = true;

		public bool hasHealth;

		public bool dentable;

		public bool hasAerodynamics;

		public bool adjustedTriangleUVs;

		[Header("Physics Options")]
		public float density = 3f;

		public float buoyancy = 1f;

		public float minimumMass = 0.1f;

		public float maximumMass = 900f;

		[Header("Material Options")]
		public MaterialSizePair[] materials;

		public FracturePattern[] fracturePatterns;

		public GameObject[] breakParticleSystems;

		[Header("Breaking Options")]
		public float jointBaseBreakForce = 3000f;

		public float jointBreakForceScaler = 3000f;

		public float jointBreakTorqueScaler = 10000f;

		public BreakImpactSettings breakImpactSettings;

		public float breakImpactThreshold = 1f;

		public float fragmentBreakImpactThreshold = 1f;

		[Range(0f, 1f)]
		public float momentumPreservation = 1f;

		[Header("Sfx")]
		public float pitch = 1f;

		public AudioClip[] impactSfx = new AudioClip[0];

		public AudioClip[] breakSfx = new AudioClip[0];

		[Header("Aerodynamics")]
		public float dragMultiplier = 0.0001f;

		public float dragVelocityCap = 90000f;

		[Header("Fire")]
		public float destroyTimer = 2.5f;

		public float onFireDuration = 2f;

		public float fireParticleDensity = 4.5f;
	}

	public enum BreakImpactSettings
	{
		Disabled = 0,
		ProjectilesOnly = 1,
		Everything = 2
	}

	[Serializable]
	public class MaterialSizePair
	{
		public Material[] materials;

		public Material[] fractureMaterials;

		public Material[] paintedMaterials;

		public float maximumSurfaceArea;
	}

	private const float thickness = 0.08f;

	private const float colliderThickness = 0.036f;

	public int version = 2;

	public BuildNodeBlock[] nodes;

	public BuildEdgeBlock[] edges;

	public bool needsSort;

	public bool isValid;

	public float originalMassDensityOverrideWithCustomMass = 5f;

	[SerializeField]
	protected Vector3 localCenter = Vector3.zero;

	[SerializeField]
	protected Vector3 localCenterNormal = Vector3.zero;

	public SurfaceFragmentController FragmentController;

	protected SurfaceVisualController surfaceVisController;

	public MeshCollider meshCollider;

	public TriggerSetJointSurface[] JointTriggers;

	public BoxCollider[] AddingPoints;

	public ConfigurableJoint[] Joints;

	public ParticleSystem FireParticles;

	public cakeslice.Outline outline;

	public MMenu material;

	public MToggle paint;

	public MToggle aero;

	public MToggle hasCollision;

	public MColourSlider hue;

	public MSlider saturation;

	public MSlider luminosity;

	public MSlider thickSlider;

	public MSlider massSlider;

	public MSlider tintSlider;

	[FormerlySerializedAs("audio")]
	public AudioSource sfx;

	public float cutoff = 6f;

	public AnimationCurve saturationCurve;

	public AnimationCurve luminosityCurve;

	[Header("Surface Options")]
	public SurfaceMaterialType wood;

	public SurfaceMaterialType metal;

	public SurfaceMaterialType glass;

	public SurfaceMaterialType wing;

	[HideInInspector]
	public SurfaceMaterialType currentType;

	public GameObject simColliderParent;

	private int materialIndex = -1;

	private bool isLoading;

	private FracturePattern currentFracturePattern;

	private int materialSizeIndex;

	private SurfaceInfo info;

	private bool meshGenerated;

	private bool fragGenerated = true;

	[HideInInspector]
	[SerializeField]
	private Mesh colliderMesh;

	[SerializeField]
	private List<BoxCollider> colliders;

	[SerializeField]
	[HideInInspector]
	private ParticleSystem[] breakParticleSystems;

	[SerializeField]
	[HideInInspector]
	private float startBreakForces;

	[SerializeField]
	[HideInInspector]
	private float startBreakTorques;

	[HideInInspector]
	[SerializeField]
	private bool collisionActive = true;

	private bool backupHue;

	private bool backupExists;

	private float backupHueColor;

	public Vector3 lastVelocity;

	public Vector3 lastAngularVelocity;

	private Dictionary<Rigidbody, Tuple<Vector3, float>> storedVelocities;

	private GameObject[] connectedBlocks;

	private float currentThickness = 0.08f;

	public float fragmentExplosionForceMultiplier = 0.55f;

	private Color currentColor;

	private float hueVal;

	private bool setup;

	private float volume = 1f;

	private float fauxVolume = 1f;

	public LineRenderer[] dragVisualisers = new LineRenderer[0];

	public AnimationCurve dragVisScale;

	protected Color glassColor = Color.grey;

	public static bool ShowCollisionToggle;

	public static bool ShowMassSlider;

	public static bool ShowGlassTintSlider;

	public static bool AllowThicknessChange;

	protected AudioMixerGroup mixer;

	protected AudioMixerGroup underwaterMixer;

	private bool hasNoThickness;

	private bool buildColliderWasActiveBeforeClone;

	private float targetPitch;

	private Vector3 transformedDirection;

	private Vector3[] lastDragForces;

	private bool _isDirty;

	private Vector3 bestEdge = Vector3.zero;

	private float lowestDot = 10f;

	protected float[] currentStresses = new float[4];

	public BuildNodeBlock Root
	{
		get
		{
			BuildEdgeBlock buildEdgeBlock = edges[0];
			BuildEdgeBlock buildEdgeBlock2 = edges[1];
			return (!(buildEdgeBlock.endNode == buildEdgeBlock2.startNode) && !(buildEdgeBlock.endNode == buildEdgeBlock2.endNode)) ? buildEdgeBlock.endNode : buildEdgeBlock.startNode;
		}
	}

	public bool CollisionActive
	{
		get
		{
			return collisionActive;
		}
	}

	public bool IsCollidable
	{
		get
		{
			return hasCollision == null || hasCollision.IsActive;
		}
	}

	public bool IsBreakable
	{
		get
		{
			return currentType.breakable;
		}
	}

	public override bool CanBurn
	{
		get
		{
			return currentType.burnable;
		}
	}

	public Material CurrentMaterial
	{
		get
		{
			return currentType.materials[materialSizeIndex].materials[materialIndex];
		}
	}

	public Material CurrentPaintedMaterial
	{
		get
		{
			return currentType.materials[materialSizeIndex].paintedMaterials[materialIndex];
		}
	}

	public Mesh ColliderMesh
	{
		get
		{
			return colliderMesh;
		}
	}

	public bool UseMeshColliderInSim
	{
		get
		{
			return isValid && version > 0 && !isDestroyed && info.EdgesPlanar && info.Colliders.Count > 4;
		}
	}

	public override bool ShouldInitHealth
	{
		get
		{
			return currentType.hasHealth;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		SetupVisualDependencies();
		if (isSimulating)
		{
			mixer = sfx.outputAudioMixerGroup;
			underwaterMixer = ReferenceMaster.GetWaterMixerFrom(mixer);
			BuildSurface buildSurface = (BuildSurface)BuildingBlock;
			info = buildSurface.info;
			materialSizeIndex = buildSurface.materialSizeIndex;
			UpdateTiling(buildSurface);
			if (StatMaster.stressCoded)
			{
				AssignStressCorners();
			}
			if (SimPhysics)
			{
				storedVelocities = new Dictionary<Rigidbody, Tuple<Vector3, float>>();
			}
		}
		else
		{
			StatMaster.Mode.ToolChanged += ToggleAddpoints;
			ReferenceMaster.onMachineSimulation = (Action<Machine, bool>)System.Delegate.Combine(ReferenceMaster.onMachineSimulation, new Action<Machine, bool>(ToggleSim));
			ReferenceMaster.onPreSimulateMachine = (Action<Machine>)System.Delegate.Combine(ReferenceMaster.onPreSimulateMachine, new Action<Machine>(PreSim));
			StatMaster.hudHiddenChanged += OnHudHide;
			StatMaster.Mode.AeroDisplayChanged = (Action)System.Delegate.Combine(StatMaster.Mode.AeroDisplayChanged, new Action(UpdateDragDisplay));
			Machine parentMachine = base.ParentMachine;
			parentMachine.OnBeforeClone = (Action)System.Delegate.Combine(parentMachine.OnBeforeClone, new Action(OnBeforeClone));
			parentMachine.OnAfterClone = (Action)System.Delegate.Combine(parentMachine.OnAfterClone, new Action(OnAfterClone));
			meshCollider.enabled = !stripped;
		}
	}

	public override void StartPhysics(bool isKinematic)
	{
		base.StartPhysics(isKinematic);
		IgnoredByWater = !collisionActive;
		density /= currentType.buoyancy;
		if (!UseMeshColliderInSim || !collisionActive || colliderMesh == null)
		{
			simColliderParent.SetActive(true);
		}
		else
		{
			UpdateMeshCollider();
			FragmentVisualController fragmentVisualController = VisualController as FragmentVisualController;
			Transform[] array = new Transform[fragmentVisualController.disableOnBreak.Length - 1];
			int num = 0;
			for (int i = 0; i < fragmentVisualController.disableOnBreak.Length; i++)
			{
				if (simColliderParent.transform != fragmentVisualController.disableOnBreak[i])
				{
					array[num] = fragmentVisualController.disableOnBreak[i];
					num++;
				}
			}
			fragmentVisualController.disableOnBreak = array;
			UnityEngine.Object.Destroy(simColliderParent);
		}
		if (FragmentController != null)
		{
			FragmentController.Initialize();
		}
	}

	private void PreSim(Machine m)
	{
		if (!isValid || m != base.ParentMachine)
		{
			return;
		}
		for (int i = 0; i < AddingPoints.Length; i++)
		{
			if (info.IsQuad || i < 3)
			{
				AddingPoints[i].enabled = false;
			}
		}
	}

	private void ToggleSim(Machine m, bool s)
	{
		if (isValid && !(m != base.ParentMachine))
		{
			if (s)
			{
				ToggleAddpoints(StatMaster.Tool.None);
			}
			else if (!isSimulating)
			{
				ToggleAddpoints(StatMaster.Mode.selectedTool);
			}
		}
	}

	private void ToggleAddpoints(StatMaster.Tool t)
	{
		if (_parentMachine.analyzing || _parentMachine.isLoadingInfo || !isValid)
		{
			return;
		}
		bool flag = t == StatMaster.Tool.None;
		for (int i = 0; i < AddingPoints.Length; i++)
		{
			if (info.IsQuad || i < 3)
			{
				AddingPoints[i].enabled = flag;
			}
		}
	}

	public void SetupVisualDependencies()
	{
		if (setup)
		{
			return;
		}
		setup = true;
		surfaceVisController = (SurfaceVisualController)VisualController;
		material = AddMenu("surfMat", 0, new List<string>
		{
			LocalisationManager.GetTranslation(3861),
			LocalisationManager.GetTranslation(3863)
		});
		material.ValueChanged += ChangeMat;
		UpdateIsGlass(material.Value == 1);
		paint = AddToggle(3875, "painted", false);
		paint.Toggled += UpdatePainted;
		aero = AddToggle(3886, "aero", false);
		aero.Toggled += UpdateAerodynamic;
		if (!isSimulating)
		{
			hasCollision = AddToggle("COLLISION", "hasColliders", true);
			hasCollision.Toggled += ToggledCollision;
			hasCollision.DisplayInMapper = ShowCollisionToggle;
			massSlider = AddSlider(2420, "custom-mass", 0f, 0f, 10f, string.Empty, string.Empty);
			massSlider.ValueChanged += delegate
			{
				UpdateMass();
			};
			massSlider.DisplayInMapper = ShowMassSlider;
		}
		tintSlider = AddSlider(4423, "tint", 1f, 0f, 1f, string.Empty);
		tintSlider.ValueChanged += UpdateTint;
		tintSlider.DisplayInMapper = material.Value == 1 && ShowGlassTintSlider;
		currentColor = new Color(0.447f, 0.2f, 0.2f);
		hue = AddColourSlider(3872, "hue", currentColor, false, true);
		saturation = AddSlider(3873, "sat", 0.6f, 0f, 1f, string.Empty, string.Empty);
		luminosity = AddSlider(3874, "lum", 0.5f, 0f, 1f, string.Empty, string.Empty);
		UpdatePainted(paint.IsActive);
		hue.ValueChanged += UpdateHue;
		saturation.ValueChanged += UpdateSaturation;
		luminosity.ValueChanged += UpdateLuminosity;
		if (!isSimulating)
		{
			thickSlider = AddSlider(3889, "thickness", 0.08f, 0.01f, 0.08f, string.Empty, string.Empty);
			thickSlider.DisplayInMapper = AllowThicknessChange;
			thickSlider.ValueChanged += UpdateThickness;
		}
		ChangeMat(material.Value);
		VisualController.SetToNormal += UpdateTiling;
	}

	private float Thickness()
	{
		if (float.IsNaN(thickSlider.Value))
		{
			hasNoThickness = true;
			return float.Epsilon;
		}
		return Mathf.Clamp(thickSlider.Value, 0.005f, (version >= 2) ? 10f : 0.12f);
	}

	private void UpdateIsGlass(bool b)
	{
		surfaceVisController.UpdateIsGlass(b);
	}

	private void UpdatePainted(bool b)
	{
		b = b && paint.DisplayInMapper;
		if (materialIndex != -1 && currentType.materials[materialSizeIndex].paintedMaterials.Length >= materialIndex)
		{
			hue.DisplayInMapper = b;
			saturation.DisplayInMapper = b;
			luminosity.DisplayInMapper = b;
			surfaceVisController.UpdatePaintedMaterial(b);
		}
	}

	public override void OnMapperOpen()
	{
		base.OnMapperOpen();
		VisualController.outlines[0].useFill = false;
	}

	public override void OnMapperClose()
	{
		base.OnMapperClose();
		SetOutlineForMapper(false);
		VisualController.outlines[0].useFill = true;
	}

	private void UpdateHue(Color c)
	{
		float S;
		float V;
		Color.RGBToHSV(c, out hueVal, out S, out V);
		backupHueColor = hueVal;
		if (ColourHolder.IsTypingColour)
		{
			UpdateColour(hueVal, S, V);
			saturation.SetValue(S);
			saturation.ApplyValue();
			luminosity.SetValue(V);
			luminosity.ApplyValue();
		}
		else
		{
			UpdateColour(hueVal, saturation.Value, luminosity.Value);
		}
	}

	private void UpdateSaturation(float s)
	{
		UpdateColour(hueVal, s, luminosity.Value);
	}

	private void UpdateLuminosity(float l)
	{
		UpdateColour(hueVal, saturation.Value, l);
	}

	private void UpdateColour(float h, float s, float l)
	{
		if (float.IsNaN(h))
		{
			Debug.Log("hue is NaN");
			return;
		}
		if (float.IsNaN(s))
		{
			Debug.Log("saturation is NaN");
			s = 0.5f;
		}
		if (float.IsNaN(l))
		{
			Debug.Log("luminosity is NaN");
			l = 1f;
		}
		Color color = Color.HSVToRGB(h, saturationCurve.Evaluate(Mathf.Lerp(0f, s, l * 4f)), luminosityCurve.Evaluate(l));
		if (!(color == currentColor))
		{
			currentColor = color;
			surfaceVisController.AssignColor(color);
			backupHue = l == 0f || s == 0f;
			if (backupHue && !backupExists)
			{
				backupExists = true;
			}
			if (backupExists && !backupHue)
			{
				backupExists = false;
				h = backupHueColor;
			}
			if (!backupHue)
			{
				hue.Value = Color.HSVToRGB(h, s, l);
				hue.ApplyValue();
			}
		}
	}

	private void UpdateTint(float v)
	{
		v = Mathf.Clamp01(v) + Mathf.Clamp01(v - 1f) * 10f;
		surfaceVisController.AssignTint(v);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (!isSimulating)
		{
			Machine parentMachine = base.ParentMachine;
			parentMachine.OnBeforeClone = (Action)System.Delegate.Remove(parentMachine.OnBeforeClone, new Action(OnBeforeClone));
			parentMachine.OnAfterClone = (Action)System.Delegate.Remove(parentMachine.OnAfterClone, new Action(OnAfterClone));
			StatMaster.hudHiddenChanged -= OnHudHide;
			StatMaster.Mode.ToolChanged -= ToggleAddpoints;
			ReferenceMaster.onPreSimulateMachine = (Action<Machine>)System.Delegate.Remove(ReferenceMaster.onPreSimulateMachine, new Action<Machine>(PreSim));
			ReferenceMaster.onMachineSimulation = (Action<Machine, bool>)System.Delegate.Remove(ReferenceMaster.onMachineSimulation, new Action<Machine, bool>(ToggleSim));
			StatMaster.Mode.AeroDisplayChanged = (Action)System.Delegate.Remove(StatMaster.Mode.AeroDisplayChanged, new Action(UpdateDragDisplay));
		}
	}

	private void OnBeforeClone()
	{
		buildColliderWasActiveBeforeClone = meshCollider.enabled;
		meshCollider.convex = true;
		meshCollider.enabled = UseMeshColliderInSim && collisionActive && !stripped;
	}

	private void OnAfterClone()
	{
		meshCollider.convex = false;
		meshCollider.enabled = buildColliderWasActiveBeforeClone;
	}

	public override bool OnFlip(bool playSound, bool isUndo)
	{
		if (!isValid)
		{
			return false;
		}
		if (playSound)
		{
			ReferenceMaster.PlayFlip();
		}
		Mirror();
		UpdateMesh();
		return true;
	}

	public override bool OnSpin(bool playSound, bool forward)
	{
		if (!isValid)
		{
			return false;
		}
		if (playSound)
		{
			ReferenceMaster.PlayFlip();
		}
		Rotate(forward);
		UpdateMesh();
		return true;
	}

	public void Mirror()
	{
		int num = 2;
		List<BuildEdgeBlock> list = edges.ToList();
		List<BuildNodeBlock> list2 = nodes.ToList();
		int num2 = 0;
		for (int num3 = num; num3 >= 0; num3--)
		{
			edges[num3] = list[num2];
			num2++;
		}
		num2 = 0;
		num = ((!info.IsQuad) ? 2 : 3);
		for (int num4 = num; num4 >= 0; num4--)
		{
			nodes[num4] = list2[num2];
			num2++;
		}
	}

	public void Rotate(bool forward = true)
	{
		int num = ((!info.IsQuad) ? 3 : 4);
		if (forward)
		{
			BuildEdgeBlock buildEdgeBlock = edges[num - 1];
			for (int i = 0; i < num; i++)
			{
				BuildEdgeBlock buildEdgeBlock2 = edges[i];
				edges[i] = buildEdgeBlock;
				buildEdgeBlock = buildEdgeBlock2;
			}
			BuildNodeBlock buildNodeBlock = nodes[num - 1];
			for (int j = 0; j < num; j++)
			{
				BuildNodeBlock buildNodeBlock2 = nodes[j];
				nodes[j] = buildNodeBlock;
				buildNodeBlock = buildNodeBlock2;
			}
			return;
		}
		BuildEdgeBlock buildEdgeBlock3 = edges[0];
		for (int num2 = num - 1; num2 >= 0; num2--)
		{
			BuildEdgeBlock buildEdgeBlock4 = edges[num2];
			edges[num2] = buildEdgeBlock3;
			buildEdgeBlock3 = buildEdgeBlock4;
		}
		BuildNodeBlock buildNodeBlock3 = nodes[0];
		for (int num3 = num - 1; num3 >= 0; num3--)
		{
			BuildNodeBlock buildNodeBlock4 = nodes[num3];
			nodes[num3] = buildNodeBlock3;
			buildNodeBlock3 = buildNodeBlock4;
		}
	}

	public override LevelBoundingBox.GroundResult Ground(LayerMask layerMask)
	{
		myBounds.childColliders[0].enabled = false;
		simColliderParent.SetActive(true);
		LevelBoundingBox.GroundResult result = base.Ground(layerMask);
		simColliderParent.SetActive(false);
		myBounds.childColliders[0].enabled = true;
		return result;
	}

	protected void ChangeMat(int i)
	{
		switch (i)
		{
		case 0:
			SetMat(wood);
			paint.DisplayInMapper = true;
			tintSlider.DisplayInMapper = false;
			UpdateTint(1f);
			break;
		case 1:
			SetMat(glass);
			paint.DisplayInMapper = false;
			tintSlider.DisplayInMapper = ShowGlassTintSlider;
			UpdateTint(tintSlider.Value);
			break;
		default:
			paint.DisplayInMapper = false;
			tintSlider.DisplayInMapper = false;
			SetMat(wood);
			UpdateTint(1f);
			break;
		}
		aero.DisplayInMapper = currentType.hasAerodynamics;
		UpdateIsGlass(i == 1);
		UpdatePainted(paint.IsActive);
		UpdateTiling();
	}

	public void SetMat(SurfaceMaterialType mat)
	{
		if (isSimulating)
		{
			currentType = mat;
			return;
		}
		currentType = mat;
		bool flag = info != null && !info.IsQuad && currentType.adjustedTriangleUVs != mat.adjustedTriangleUVs;
		if (!flag)
		{
			CalculateMaterialSizeIndex();
		}
		if (materialSizeIndex >= mat.materials.Length)
		{
			if (flag)
			{
				CalculateMaterialSizeIndex();
			}
			else
			{
				materialSizeIndex = Mathf.Clamp(materialIndex, 0, mat.materials.Length - 1);
			}
		}
		if (!isLoading || materialIndex == -1)
		{
			materialIndex = UnityEngine.Random.Range(0, mat.materials[materialSizeIndex].materials.Length);
		}
		else if (materialIndex >= mat.materials[materialSizeIndex].materials.Length)
		{
			materialIndex = Mathf.Clamp(materialIndex, 0, mat.materials[materialSizeIndex].materials.Length - 1);
		}
		if (!noRigidbody)
		{
		}
		if (flag)
		{
			UpdateSurface(true);
			return;
		}
		UpdateMass();
		SetupFire();
		SetupBreakParticles();
		if (meshGenerated)
		{
			UpdateHealth();
			GenerateFractureFragments();
		}
	}

	private void CalculateMaterialSizeIndex()
	{
		if (info == null)
		{
			materialSizeIndex = currentType.materials.Length - 1;
			return;
		}
		float num3;
		if (info.IsQuad)
		{
			float num = Mathf.Max(edges[0].Length, edges[2].Length);
			float num2 = Mathf.Max(edges[1].Length, edges[3].Length);
			num3 = num * num2;
		}
		else
		{
			float num4 = Mathf.Min(edges[0].Length, edges[1].Length, edges[2].Length);
			if (num4 == edges[0].Length)
			{
				num3 = 0.5f * edges[1].Length * edges[2].Length;
			}
			else if (num4 == edges[1].Length)
			{
				num3 = 0.5f * edges[0].Length * edges[2].Length;
			}
			else if (num4 == edges[2].Length)
			{
				num3 = 0.5f * edges[0].Length * edges[1].Length;
			}
			else
			{
				Debug.LogError("Min didn't return float-equal value!");
				num3 = 0.5f * edges[0].Length * edges[1].Length;
			}
		}
		materialSizeIndex = 0;
		while (materialSizeIndex < currentType.materials.Length && !(num3 <= currentType.materials[materialSizeIndex].maximumSurfaceArea))
		{
			materialSizeIndex++;
		}
	}

	private void UpdateSurfaceMaterial()
	{
		surfaceVisController.UpdateSurfaceMaterial();
	}

	public void SurfaceChanged(BuildNodeBlock changedNode)
	{
		NodeController nodeController = base.ParentMachine.nodeController;
		for (int i = 0; i < edges.Length; i++)
		{
			BuildEdgeBlock buildEdgeBlock = edges[i];
			if (buildEdgeBlock.isValid && (buildEdgeBlock.startNode == changedNode || buildEdgeBlock.endNode == changedNode))
			{
				nodeController.Refresh(buildEdgeBlock);
			}
		}
		nodeController.Refresh(this);
	}

	private void UpdateTiling(BuildSurface source)
	{
		Vector2 vector = Vector2.one;
		if (VisualController.selectedSkin.pack.settings.allowTiling && source.info != null)
		{
			if (source.currentType != source.wood)
			{
				surfaceVisController.SetTiling(vector);
				return;
			}
			if (source.info.IsQuad)
			{
				float num = Mathf.Max(source.edges[0].Length, source.edges[2].Length);
				float num2 = Mathf.Max(source.edges[1].Length, source.edges[3].Length);
				vector.y = Mathf.Ceil(num2 - 0.0001f) / 4f;
				vector.x = Mathf.Ceil(num - 0.0001f) / 4f;
			}
			else
			{
				float length = source.edges[1].Length;
				Vector3 direction = source.edges[1].Direction;
				Vector3 direction2 = source.edges[0].Direction;
				Vector3 direction3 = source.edges[2].Direction;
				float f = Mathf.Max(Vector3.Angle(direction, direction2), Vector3.Angle(direction, direction3)) * ((float)Math.PI / 180f);
				float f2 = Vector3.Angle(direction2, direction3) * ((float)Math.PI / 180f);
				float num3 = length * Mathf.Pow(Mathf.Sin(f), 2f) / Mathf.Sin(f2);
				vector.y = Mathf.Ceil(2f * num3 - 0.0001f) / 8f;
				vector.x = Mathf.Ceil(length - 0.0001f) / 4f;
			}
		}
		surfaceVisController.SetTiling(vector);
	}

	public void SurfaceChanged(BuildEdgeBlock edge)
	{
		NodeController nodeController = base.ParentMachine.nodeController;
		nodeController.Refresh(this);
	}

	public void BreakSurface(Collision collision)
	{
		if (FragmentController != null)
		{
			FragmentController.CalculateBreakImpulses(collision);
		}
		surfaceVisController.OnJointBreak(0f);
	}

	public void BreakSurface(float power, float upPower, float torquePower, Vector3 explosionPos, float radius)
	{
		if (FragmentController != null)
		{
			FragmentController.CalculateBreakImpulses(power, upPower, torquePower, explosionPos, radius);
		}
		surfaceVisController.OnJointBreak(0f);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!isSimulating || !SimPhysics || !isValid || !base.ParentMachine.finishedPhysics)
		{
			return;
		}
		if (collision.relativeVelocity.sqrMagnitude > cutoff)
		{
			PlaySound();
			if (StatMaster.isMP && !StatMaster.IsLevelEditorOnly)
			{
				if (NetBlock != null)
				{
					NetBlock.Event(NetworkEntity.EntityEvent.SoundOnCollide, 0);
				}
				else
				{
					Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
				}
			}
		}
		if (!currentType.breakable || surfaceVisController.hasBroken || base.ParentMachine.UnbreakableMode)
		{
			return;
		}
		Rigidbody rigidbody = collision.rigidbody;
		bool flag = rigidbody != null;
		if (flag)
		{
			storedVelocities[rigidbody] = new Tuple<Vector3, float>(rigidbody.velocity, Time.fixedTime);
		}
		if (currentType.breakImpactSettings == BreakImpactSettings.Disabled)
		{
			return;
		}
		if (currentType.breakImpactSettings == BreakImpactSettings.ProjectilesOnly)
		{
			if (collision.rigidbody == null)
			{
				return;
			}
			BasicInfo component = collision.rigidbody.GetComponent<BasicInfo>();
			if (component == null || component.infoType != BasicInfoType.Projectile)
			{
				return;
			}
		}
		float magnitude = collision.relativeVelocity.magnitude;
		float num = ((!flag) ? (magnitude / Time.fixedDeltaTime) : (rigidbody.mass * magnitude / Time.fixedDeltaTime));
		if (num > currentType.breakImpactThreshold)
		{
			BreakSurface(collision);
		}
	}

	public void PlaySound()
	{
		if (!sfx.isPlaying)
		{
			float pitch = currentType.pitch;
			if (Joints.Length == 0)
			{
				PlaySound(currentType.impactSfx, 1.4f * pitch, 1.8f * pitch);
			}
			else
			{
				PlaySound(currentType.impactSfx, 1.1f * pitch, 1.4f * pitch);
			}
		}
	}

	protected void PlaySound(AudioClip[] sfx, float pitchMin, float pitchMax)
	{
		if (sfx.Length > 0)
		{
			AudioClip s = sfx[UnityEngine.Random.Range(0, sfx.Length)];
			PlaySound(s, pitchMin, pitchMax);
		}
	}

	protected void PlaySound(AudioClip s, float pitchMin, float pitchMax)
	{
		if (base.GetSubmergedPctMV > 0.9f)
		{
			sfx.outputAudioMixerGroup = underwaterMixer;
		}
		else
		{
			sfx.outputAudioMixerGroup = mixer;
		}
		float num = Mathf.InverseLerp(10f, 0.25f, volume * 3f) * 0.5f + 0.5f;
		targetPitch = UnityEngine.Random.Range(pitchMin, pitchMax) * num;
		sfx.pitch = targetPitch;
		sfx.volume = UnityEngine.Random.Range(0.05f, 0.2f) * num;
		sfx.clip = s;
		sfx.Play();
	}

	private void UpdateTiling()
	{
		if (isSimulating)
		{
			BuildSurface source = (BuildSurface)BuildingBlock;
			UpdateTiling(source);
		}
		else
		{
			UpdateTiling(this);
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (SimPhysics && currentType.breakable && !surfaceVisController.hasBroken && base.ParentMachine.finishedPhysics && !base.ParentMachine.UnbreakableMode)
		{
			Rigidbody rigidbody = collision.rigidbody;
			if (rigidbody != null && storedVelocities.ContainsKey(rigidbody))
			{
				storedVelocities.Remove(rigidbody);
			}
		}
	}

	public void RestoreStoredVelocities()
	{
		StartCoroutine(RestoreStoredVelocitiesIE());
	}

	private IEnumerator RestoreStoredVelocitiesIE()
	{
		for (int i = 0; i < 3; i++)
		{
			yield return new WaitForFixedUpdate();
			foreach (KeyValuePair<Rigidbody, Tuple<Vector3, float>> storedVel in storedVelocities)
			{
				if (!(Time.fixedTime - storedVel.Value.Item2 > 0.5f) && storedVel.Key != null)
				{
					storedVel.Key.velocity = storedVel.Value.Item1 * currentType.momentumPreservation;
				}
			}
		}
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates(Prefab.RegisterSimUpdate, SimPhysics && (currentType.hasAerodynamics || currentType.breakable), Prefab.RegisterSimLateUpdate, Prefab.RegisterEmulationUpdate);
	}

	public override void FixedUpdateBlock()
	{
		if (!isValid || noRigidbody || surfaceVisController.hasBroken)
		{
			_parentMachine.UnregisterFixedUpdate(this, false);
			return;
		}
		lastVelocity = Rigidbody.velocity;
		lastAngularVelocity = Rigidbody.angularVelocity;
		if (currentType.hasAerodynamics && aero.IsActive)
		{
			if (lastDragForces == null || lastDragForces.Length != nodes.Length)
			{
				lastDragForces = new Vector3[nodes.Length];
			}
			float surfaceArea = info.SurfaceArea;
			int[] array = new int[4]
			{
				0,
				(info.Width + 1) * info.Height,
				(info.Width + 1) * (info.Height + 1) - 1,
				info.Width
			};
			Transform transform = base.transform;
			float num = 1f / (1f * (float)nodes.Length);
			num *= currentType.dragMultiplier * surfaceArea;
			for (int i = 0; i < nodes.Length; i++)
			{
				SurfaceInfo.Vertex vertex = info.Vertices[array[i]];
				Vector3 vector = transform.TransformPoint(vertex.Position);
				Vector3 normal = vertex.Normal;
				Vector3 pointVelocity = Rigidbody.GetPointVelocity(vector);
				transformedDirection = transform.InverseTransformDirection(pointVelocity);
				float num2 = Vector3.Dot(normal, -transformedDirection);
				float num3 = Mathf.Min(pointVelocity.sqrMagnitude, currentType.dragVelocityCap);
				float num4 = num2 * num3 * num;
				lastDragForces[i] = num4 * normal;
				Rigidbody.AddForceAtPosition(transform.TransformVector(lastDragForces[i]), vector);
			}
		}
	}

	private Vector3 EdgeInterp(SurfaceInfo info, int i, float v)
	{
		if (info.Edges[i].Invert)
		{
			v = 1f - v;
		}
		return edges[i].Interp(v);
	}

	private Vector3 GetPointOnSurface(float u, float v)
	{
		Vector3 a = info.Nodes[0];
		Vector3 vector = info.Nodes[1];
		Vector3 b = info.Nodes[2];
		Vector3 vector2;
		if (!info.IsQuad)
		{
			vector2 = Vector3.Lerp(a, Vector3.Lerp(vector, b, u), v);
			if (!info.EdgesStraight)
			{
				if (edges.Length < 3)
				{
					Debug.LogError("surface edges are empty");
				}
				else if (info.Edges.Length < 3)
				{
					Debug.LogError("surface-info edge index out of range");
				}
				else
				{
					Vector3 b2 = info.localMatrix.MultiplyPoint3x4(EdgeInterp(info, 1, u));
					Vector3 b3 = info.localMatrix.MultiplyPoint3x4(EdgeInterp(info, 2, 1f - v));
					Vector3 a2 = info.localMatrix.MultiplyPoint3x4(EdgeInterp(info, 0, v));
					vector2 = Vector3.Lerp(a, b2, v) + Vector3.Lerp(a2, b3, u) - vector2;
				}
			}
		}
		else
		{
			Vector3 a3 = info.Nodes[3];
			vector2 = Vector3.Lerp(Vector3.Lerp(a, vector, u), Vector3.Lerp(a3, b, u), v);
			if (!info.EdgesStraight)
			{
				if (edges.Length < 4)
				{
					Debug.LogError("surface edges are empty");
				}
				else if (info.Edges.Length < 4)
				{
					Debug.LogError("surface-info edge index out of range");
				}
				else
				{
					Vector3 a4 = info.localMatrix.MultiplyPoint3x4(EdgeInterp(info, 0, u));
					Vector3 b4 = info.localMatrix.MultiplyPoint3x4(EdgeInterp(info, 2, 1f - u));
					Vector3 b5 = info.localMatrix.MultiplyPoint3x4(EdgeInterp(info, 1, v));
					Vector3 a5 = info.localMatrix.MultiplyPoint3x4(EdgeInterp(info, 3, 1f - v));
					vector2 = Vector3.Lerp(a4, b4, v) + Vector3.Lerp(a5, b5, u) - vector2;
				}
			}
		}
		return vector2;
	}

	private Vector3 GetSurfaceNormal(Vector3 point, float u, float v)
	{
		if (info == null)
		{
			Debug.LogError("Missing SurfaceInfo on GetSurfaceNormal");
			return Vector3.zero;
		}
		float num = 0.05f;
		float u2 = u + num;
		float v2 = v + num;
		return Vector3.Cross(GetPointOnSurface(u, v2) - point, GetPointOnSurface(u2, v) - point).normalized;
	}

	private void UpdateThickness(float b)
	{
		if (!isSimulating && isValid && b != currentThickness && info != null)
		{
			GenerateMesh();
			UpdateMeshCollider();
			GenerateFractureFragments();
		}
	}

	private void GenerateMesh()
	{
		currentThickness = Thickness();
		if (hasNoThickness)
		{
			AssignMesh(BlockSkinnedVisualController.empty);
			return;
		}
		Mesh mesh = new Mesh();
		mesh.name = "BuildSurface";
		int num = info.Vertices.Length;
		int num2 = (info.Height + 1 + (info.Width + 1)) * 4;
		if (!info.IsQuad)
		{
			num2 -= (info.Width + 1) * 2;
		}
		int num3 = num * 2;
		Vector3[] array = new Vector3[num3 + num2];
		Vector2[] array2 = new Vector2[array.Length];
		Vector3[] array3 = new Vector3[(info.Width + 1) * 2];
		Vector2[] array4 = new Vector2[array3.Length];
		Vector3[] array5 = null;
		Vector2[] array6 = null;
		if (info.IsQuad)
		{
			array5 = new Vector3[array3.Length];
			array6 = new Vector2[array3.Length];
		}
		Vector3[] array7 = new Vector3[(info.Height + 1) * 2];
		Vector2[] array8 = new Vector2[array7.Length];
		Vector3[] array9 = new Vector3[array7.Length];
		Vector2[] array10 = new Vector2[array7.Length];
		float num4 = 1f - currentThickness;
		float num5 = 1f;
		float num6 = 0f;
		float num7 = 1f;
		if (info.IsQuad)
		{
			fauxVolume = (info.Edges[0].Length + info.Edges[2].Length) * 0.5f;
			fauxVolume *= (info.Edges[1].Length + info.Edges[3].Length) * 0.5f;
		}
		else
		{
			fauxVolume = (info.Edges[0].Length + info.Edges[1].Length + info.Edges[2].Length) * 0.334f;
		}
		for (int i = 0; i < info.Vertices.Length; i++)
		{
			SurfaceInfo.Vertex vertex = info.Vertices[i];
			Vector3 vector = vertex.Position + vertex.Normal * currentThickness;
			array[i] = vector;
			array2[i] = vertex.TextureUV;
			Vector3 vector2 = vertex.Position + -vertex.Normal * currentThickness;
			array[num + i] = vector2;
			array2[num + i] = vertex.TextureUV;
			if (info.IsQuad && vertex.Y == 0)
			{
				int num8 = vertex.X * 2;
				array5[num8] = vector;
				array5[num8 + 1] = vector2;
				float x = Mathf.Lerp(num6, num7, (float)vertex.X / (float)info.Width);
				array6[num8] = new Vector2(x, num4);
				array6[num8 + 1] = new Vector2(x, num5);
			}
			if (vertex.Y == info.Height)
			{
				int num9 = (info.Width - vertex.X) * 2;
				array3[num9] = vector;
				array3[num9 + 1] = vector2;
				float x2 = Mathf.Lerp(num7, num6, (float)(info.Width - vertex.X) / (float)info.Width);
				array4[num9] = new Vector2(x2, num4);
				array4[num9 + 1] = new Vector2(x2, num5);
			}
			if (vertex.X == 0)
			{
				int num10 = (info.Height - vertex.Y) * 2;
				array9[num10] = vector;
				array9[num10 + 1] = vector2;
				if (info.IsQuad)
				{
					float y = Mathf.Lerp(num6, num7, (float)(info.Height - vertex.Y) / (float)info.Height);
					array10[num10] = new Vector2(num4, y);
					array10[num10 + 1] = new Vector2(num5, y);
				}
				else
				{
					float x3 = Mathf.Lerp(num7, num6 + 0.5f * num7, (float)(info.Height - vertex.Y) / (float)info.Height);
					array10[num10] = new Vector2(x3, num5);
					array10[num10 + 1] = new Vector2(x3, num4);
				}
			}
			if (vertex.X == info.Width)
			{
				int num11 = vertex.Y * 2;
				array7[num11] = vector;
				array7[num11 + 1] = vector2;
				if (info.IsQuad)
				{
					float y2 = Mathf.Lerp(num6, num7, (float)vertex.Y / (float)info.Height);
					array8[num11] = new Vector2(num4, y2);
					array8[num11 + 1] = new Vector2(num5, y2);
				}
				else
				{
					float x4 = Mathf.Lerp(num7, num6 + 0.5f * num7, (float)(info.Height - vertex.Y) / (float)info.Height);
					array8[num11] = new Vector2(x4, num5);
					array8[num11 + 1] = new Vector2(x4, num4);
				}
			}
		}
		if (info.IsQuad)
		{
			Array.Copy(array5, 0, array, num3, array5.Length);
			Array.Copy(array6, 0, array2, num3, array6.Length);
			num3 += array5.Length;
		}
		Array.Copy(array7, 0, array, num3, array7.Length);
		Array.Copy(array8, 0, array2, num3, array8.Length);
		num3 += array7.Length;
		Array.Copy(array3, 0, array, num3, array3.Length);
		Array.Copy(array4, 0, array2, num3, array4.Length);
		num3 += array3.Length;
		Array.Copy(array9, 0, array, num3, array9.Length);
		Array.Copy(array10, 0, array2, num3, array10.Length);
		num3 = num * 2;
		int num12 = info.Width * info.Height * 6;
		int num13 = num12 * 2;
		int num14 = (info.Width + 1 + (info.Height + 1)) * 12;
		if (!info.IsQuad)
		{
			num14 -= (info.Width + 1) * 6;
		}
		int[] array11 = new int[num13 + num14];
		int num15 = 0;
		int num16 = 0;
		int num17 = 0;
		while (num17 < info.Height)
		{
			int num18 = 0;
			while (num18 < info.Width)
			{
				array11[num15] = num16;
				array11[num15 + 3] = (array11[num15 + 2] = num16 + 1);
				array11[num15 + 4] = (array11[num15 + 1] = num16 + info.Width + 1);
				array11[num15 + 5] = num16 + info.Width + 2;
				array11[num12 + num15] = num + num16;
				array11[num12 + num15 + 3] = (array11[num12 + num15 + 2] = num + num16 + info.Width + 1);
				array11[num12 + num15 + 4] = (array11[num12 + num15 + 1] = num + num16 + 1);
				array11[num12 + num15 + 5] = num + num16 + info.Width + 2;
				num18++;
				num15 += 6;
				num16++;
			}
			num17++;
			num16++;
		}
		int num19 = 0;
		if (info.IsQuad)
		{
			for (int i = 0; i < info.Width; i++)
			{
				int num20 = num13 + num19;
				array11[num20] = num3;
				array11[num20 + 1] = num3 + 2;
				array11[num20 + 2] = num3 + 1;
				array11[num20 + 3] = num3 + 1;
				array11[num20 + 4] = num3 + 2;
				array11[num20 + 5] = num3 + 3;
				num19 += 6;
				num3 += 2;
			}
			num3 += 2;
		}
		for (int i = 0; i < info.Height; i++)
		{
			int num21 = num13 + num19;
			array11[num21] = num3;
			array11[num21 + 1] = num3 + 2;
			array11[num21 + 2] = num3 + 1;
			array11[num21 + 3] = num3 + 1;
			array11[num21 + 4] = num3 + 2;
			array11[num21 + 5] = num3 + 3;
			num19 += 6;
			num3 += 2;
		}
		num3 += 2;
		for (int i = 0; i < info.Width; i++)
		{
			int num22 = num13 + num19;
			array11[num22] = num3;
			array11[num22 + 1] = num3 + 2;
			array11[num22 + 2] = num3 + 1;
			array11[num22 + 3] = num3 + 1;
			array11[num22 + 4] = num3 + 2;
			array11[num22 + 5] = num3 + 3;
			num19 += 6;
			num3 += 2;
		}
		num3 += 2;
		for (int i = 0; i < info.Height; i++)
		{
			int num23 = num13 + num19;
			array11[num23] = num3;
			array11[num23 + 1] = num3 + 2;
			array11[num23 + 2] = num3 + 1;
			array11[num23 + 3] = num3 + 1;
			array11[num23 + 4] = num3 + 2;
			array11[num23 + 5] = num3 + 3;
			num19 += 6;
			num3 += 2;
		}
		mesh.vertices = array;
		mesh.uv = array2;
		mesh.triangles = array11;
		mesh.RecalculateNormals();
		mesh.Optimize();
		AssignMesh(mesh);
	}

	protected void AssignMesh(Mesh m)
	{
		if (VisualController.MeshFilter.sharedMesh != null)
		{
			VisualController.MeshFilter.sharedMesh.Clear();
			UnityEngine.Object.Destroy(VisualController.MeshFilter.sharedMesh);
		}
		VisualController.MeshFilter.sharedMesh = m;
		meshGenerated = true;
		fragGenerated = false;
	}

	public Vector3 CornersCenter()
	{
		if (!isValid)
		{
			return base.transform.position;
		}
		Vector3 zero = Vector3.zero;
		if (edges.Length > 0)
		{
			int num = 0;
			for (int i = 0; i < edges.Length; i++)
			{
				if (edges[i].isValid)
				{
					zero += (edges[i].startNode.transform.position + edges[i].endNode.transform.position) / 2f;
					num++;
				}
			}
			if (num > 0)
			{
				zero /= 1f * (float)num;
			}
		}
		return zero;
	}

	public override Vector3 GetTarget()
	{
		return GetCenter();
	}

	public override Vector3 GetCenter()
	{
		return base.transform.TransformPoint(localCenter);
	}

	internal void GenerateFractureFragments()
	{
		if (fragGenerated)
		{
			return;
		}
		FragmentController = GetComponent<SurfaceFragmentController>();
		if (FragmentController != null)
		{
			for (int i = 0; i < FragmentController.fragments.Length; i++)
			{
				if (FragmentController.fragments[i] != null)
				{
					UnityEngine.Object.Destroy(FragmentController.fragments[i].Object);
				}
			}
		}
		if (!currentType.breakable || fauxVolume < 0.5f)
		{
			surfaceVisController.brokenVis = new FilterRendererPair[0];
			surfaceVisController.breakIntoPieces = false;
			if (FragmentController != null)
			{
				UnityEngine.Object.Destroy(FragmentController);
			}
			return;
		}
		if (hasNoThickness)
		{
			surfaceVisController.brokenVis = new FilterRendererPair[0];
		}
		if (FragmentController == null)
		{
			FragmentController = base.gameObject.AddComponent<SurfaceFragmentController>();
			FragmentController.mySurface = this;
		}
		currentFracturePattern = currentType.fracturePatterns[UnityEngine.Random.Range(0, currentType.fracturePatterns.Length)];
		int count = currentFracturePattern.GetCount(info.IsQuad);
		List<FilterRendererPair> list = new List<FilterRendererPair>(count);
		surfaceVisController.breakIntoPieces = true;
		FragmentController.fragments = new Fragment[count];
		if (materialIndex == -1)
		{
			materialIndex = UnityEngine.Random.Range(0, currentType.materials[materialSizeIndex].materials.Length);
		}
		Material sharedMaterial = currentType.materials[materialSizeIndex].fractureMaterials[materialIndex];
		ShadowCastingMode shadowCastingMode = (currentType.shadows ? ShadowCastingMode.On : ShadowCastingMode.Off);
		for (int j = 0; j < count; j++)
		{
			Fragment fragment = new Fragment();
			fragment.Object = UnityEngine.Object.Instantiate(ReferenceMaster.Instance.BuildSurfaceGo, base.transform, false) as GameObject;
			fragment.Object.name = "Surface Fragment  (s" + BuildIndex + ")";
			if (!noRigidbody)
			{
				fragment.Mass = Rigidbody.mass / (float)count;
			}
			FracturePiece fracturePiece = currentFracturePattern.Get(j, info.IsQuad);
			if (fracturePiece.MainPiece == null)
			{
				Debug.LogWarning("Does not have mesh for fragment " + j + "!");
				continue;
			}
			FilterRendererPair filterRendererPair = new FilterRendererPair(fragment.Object.GetComponent<MeshFilter>(), fragment.Object.GetComponent<MeshRenderer>());
			filterRendererPair.renderer.sharedMaterial = sharedMaterial;
			filterRendererPair.renderer.shadowCastingMode = shadowCastingMode;
			filterRendererPair.renderer.enabled = !hasNoThickness;
			fragment.Renderer = filterRendererPair.renderer;
			filterRendererPair.filter.sharedMesh = ((!hasNoThickness) ? FitFractureMeshOntoSurface(fracturePiece.MainPiece) : BlockSkinnedVisualController.empty);
			list.Add(filterRendererPair);
			if (fracturePiece.FractureParticles != null && fauxVolume > 2f && !hasNoThickness)
			{
				fragment.Particles = new Particle[fracturePiece.FractureParticles.Length];
				for (int k = 0; k < fracturePiece.FractureParticles.Length; k++)
				{
					if (fracturePiece.FractureParticles[k] == null || fracturePiece.FractureParticles[k].Mesh == null)
					{
						Debug.LogWarning("Does not have mesh for fragment " + j + ", particle " + k + "!");
						continue;
					}
					Particle particle = new Particle();
					Mesh mesh = FitFractureMeshOntoSurface(fracturePiece.FractureParticles[k].Mesh);
					Bounds bounds = mesh.bounds;
					particle.Object = UnityEngine.Object.Instantiate(ReferenceMaster.Instance.BuildSurfaceParticle, fragment.Object.transform, false) as GameObject;
					particle.Object.name = "Fragment Particle #" + k + " (s" + BuildIndex + ")";
					particle.Object.transform.localPosition = bounds.center;
					GameObject gameObject = UnityEngine.Object.Instantiate(ReferenceMaster.Instance.BuildSurfaceVis, particle.Object.transform, false) as GameObject;
					gameObject.transform.localPosition = -bounds.center;
					MeshFilter component = gameObject.GetComponent<MeshFilter>();
					component.sharedMesh = mesh;
					filterRendererPair = new FilterRendererPair(component, gameObject.GetComponent<MeshRenderer>());
					filterRendererPair.renderer.sharedMaterial = sharedMaterial;
					filterRendererPair.renderer.shadowCastingMode = shadowCastingMode;
					filterRendererPair.filter.sharedMesh = mesh;
					list.Add(filterRendererPair);
					BoxCollider component2 = particle.Object.GetComponent<BoxCollider>();
					component2.center = Vector3.zero;
					component2.size = bounds.size * 0.2f;
					particle.Mass = fragment.Mass / (float)fracturePiece.FractureParticles.Length;
					particle.IsStickySide = fracturePiece.FractureParticles[k].Sticky;
					particle.Side = fracturePiece.FractureParticles[k].Side;
					if (!particle.IsStickySide)
					{
						particle.CreateRigidbody();
					}
					fragment.Particles[k] = particle;
				}
			}
			FragmentController.fragments[j] = fragment;
		}
		surfaceVisController.brokenVis = list.ToArray();
		CreateFragmentColliders();
		fragGenerated = true;
	}

	public void OnRemoteFragmentBreak(int index)
	{
		if (FragmentController != null)
		{
			FragmentController.OnRemoteFragmentBreak(index);
		}
	}

	public void OnRemoteBreak()
	{
		(VisualController as SurfaceVisualController).OnRemoteBreak();
		if (FragmentController != null)
		{
			FragmentController.OnRemoteBreak();
		}
		OnBreakComplete();
	}

	private Mesh FitFractureMeshOntoSurface(Mesh template)
	{
		Vector3[] vertices = template.vertices;
		Vector2[] uv = template.uv;
		int num = uv.Length;
		Vector3[] array = new Vector3[num];
		Vector3 vector = ((!info.IsQuad) ? (-1f) : 1f) * localCenterNormal * Thickness();
		float max = ((info.IsQuad || !currentType.adjustedTriangleUVs) ? 1f : 0.5f);
		for (int i = 0; i < num; i++)
		{
			Vector2 meshUV = uv[i].Clamp(0f, max);
			Vector2 vector2 = MeshUVToSurfaceUV(meshUV);
			array[i] = GetPointOnSurface(vector2.x, vector2.y) + Mathf.Sign(vertices[i].y) * vector;
		}
		Mesh mesh = new Mesh();
		mesh.name = "BuildSurface Fragment";
		mesh.vertices = array;
		mesh.triangles = template.triangles;
		Vector2[] uv2 = template.uv2;
		mesh.uv = ((uv2.Length == 0) ? uv : uv2);
		mesh.RecalculateNormals();
		return mesh;
	}

	public void PreBreakJoints()
	{
		CreateSimLists();
		foreach (Joint item in jointsToMe)
		{
			if ((bool)item)
			{
				float breakForce = (item.breakTorque = 0f);
				item.breakForce = breakForce;
			}
		}
		jointsToMe.Clear();
	}

	public void OnBreakComplete()
	{
		if (!noRigidbody)
		{
			UnityEngine.Object.Destroy(Rigidbody);
			noRigidbody = true;
		}
		if ((bool)fireTag && (!SimPhysics || fireTag.burning))
		{
			fireTag.fireControllerCode.ImmediateStop();
		}
		for (int i = 0; i < base.transform.childCount; i++)
		{
			Transform child = base.transform.GetChild(i);
			if (child.gameObject.CompareTag("Projectile"))
			{
				child.gameObject.SetActive(false);
			}
		}
		for (int j = 0; j < visAddedToMe.Count; j++)
		{
			if (visAddedToMe[j] != null)
			{
				visAddedToMe[j].gameObject.SetActive(false);
			}
		}
	}

	private Vector2 MeshUVToSurfaceUV(Vector2 meshUV)
	{
		if (info.IsQuad)
		{
			return meshUV;
		}
		float y;
		float a;
		float b;
		if (currentType.adjustedTriangleUVs)
		{
			y = 1f - 2f * meshUV.y;
			a = meshUV.y;
			b = 1f - meshUV.y;
		}
		else
		{
			y = 1f - meshUV.y;
			a = 0.5f * meshUV.y;
			b = 1f - 0.5f * meshUV.y;
		}
		float x = Mathf.Lerp(0f, 1f, Mathf.InverseLerp(a, b, meshUV.x));
		return new Vector2(x, y);
	}

	private Vector2 MeshUVToSurfaceUVNoAdjustedTris(Vector2 meshUV)
	{
		if (info.IsQuad)
		{
			return meshUV;
		}
		float y = 1f - meshUV.y;
		float a = 0.5f * meshUV.y;
		float b = 1f - 0.5f * meshUV.y;
		float x = Mathf.Lerp(0f, 1f, Mathf.InverseLerp(a, b, meshUV.x));
		return new Vector2(x, y);
	}

	private Vector2 SurfaceUVToMeshUV(Vector2 surfaceUV)
	{
		if (info.IsQuad)
		{
			return surfaceUV;
		}
		float num;
		float a;
		float b;
		if (currentType.adjustedTriangleUVs)
		{
			num = 0.5f - surfaceUV.y * 0.5f;
			a = num;
			b = 1f - num;
		}
		else
		{
			num = 1f - surfaceUV.y;
			a = 0.5f * num;
			b = 1f - 0.5f * num;
		}
		float x = Mathf.Lerp(a, b, Mathf.InverseLerp(0f, 1f, surfaceUV.x));
		return new Vector2(x, num);
	}

	private bool CombineQuad(SurfaceInfo.Quad q1, SurfaceInfo.Quad q2, int spanX, int spanY, bool testCombine, bool testRect)
	{
		if (spanX > 1 && q1.X != q2.X)
		{
			int num = Mathf.FloorToInt((float)q1.X / (float)spanX);
			int num2 = Mathf.FloorToInt((float)q2.X / (float)spanX);
			if (num == num2)
			{
				return true;
			}
		}
		if (spanY > 1 && q1.Y != q2.Y)
		{
			int num3 = Mathf.FloorToInt((float)q1.Y / (float)spanY);
			int num4 = Mathf.FloorToInt((float)q2.Y / (float)spanY);
			if (num3 == num4)
			{
				return true;
			}
		}
		if (!testCombine)
		{
			return false;
		}
		if (testRect && (!q1.IsRect || !q2.IsRect))
		{
			return false;
		}
		float num5 = 7f;
		float f = Mathf.Clamp(Vector3.Dot(q1.Normal, q2.Normal), -1f, 1f);
		return Mathf.Abs(57.29578f * Mathf.Acos(f)) < num5;
	}

	private void CreateSurfaceInfo()
	{
		info = new SurfaceInfo(this, nodes, edges);
		int width = info.Width;
		int height = info.Height;
		float num = 1f / (float)width;
		float num2 = 1f / (float)height;
		float num3 = 0.06f;
		int num4 = 0;
		localCenter = GetPointOnSurface(0.5f, 0.5f);
		localCenterNormal = GetSurfaceNormal(localCenter, 0.5f, 0.5f);
		float num5 = 0.5f;
		float num6 = 1f;
		float num7 = 2f;
		float maxEdgeWidth = info.MaxEdgeWidth;
		int spanX = ((maxEdgeWidth < num5) ? 5 : ((maxEdgeWidth < num6) ? 4 : ((maxEdgeWidth < num7) ? 3 : ((!info.CombineQuadsH) ? 2 : 0))));
		float maxEdgeHeight = info.MaxEdgeHeight;
		int spanY = ((maxEdgeHeight < num5) ? 5 : ((maxEdgeHeight < num6) ? 4 : ((maxEdgeHeight < num7) ? 3 : ((!info.CombineQuadsV) ? 2 : 0))));
		for (int i = 0; i <= height; i++)
		{
			float num8 = num2 * (float)i;
			int num9 = 0;
			while (num9 <= width)
			{
				float num10 = num * (float)num9;
				Vector3 pointOnSurface = GetPointOnSurface(num10, num8);
				Vector2 surfaceUV = new Vector2(num10, num8);
				Vector3 surfaceNormal;
				if (!info.IsQuad && i == 0)
				{
					float u = ((num9 != width) ? num10 : (num10 - num3));
					float v = num3;
					surfaceNormal = GetSurfaceNormal(GetPointOnSurface(u, v), u, v);
				}
				else if (i == 0 && num9 == 0)
				{
					float u = num3;
					float v = num3;
					surfaceNormal = GetSurfaceNormal(GetPointOnSurface(u, v), u, v);
				}
				else if (num9 == width || i == height)
				{
					float u;
					float v;
					if (info.IsQuad)
					{
						if (i == 0 && num9 == width)
						{
							u = 1f - num3;
							v = num3;
						}
						else if (i == height && num9 == 0)
						{
							u = num3;
							v = 1f - num3;
						}
						else if (i == height && num9 == width)
						{
							u = 1f - num3;
							v = 1f - num3;
						}
						else
						{
							u = ((num9 != width) ? num10 : (num10 - num3));
							v = ((i != height) ? num8 : (num8 - num3));
						}
					}
					else
					{
						u = ((num9 != width) ? num10 : (num10 - num3));
						v = ((i != height) ? num8 : (num8 - num3));
					}
					surfaceNormal = GetSurfaceNormal(GetPointOnSurface(u, v), u, v);
				}
				else
				{
					surfaceNormal = GetSurfaceNormal(pointOnSurface, num10, num8);
				}
				SurfaceInfo.Vertex vertex = new SurfaceInfo.Vertex();
				vertex.X = num9;
				vertex.Y = i;
				vertex.Position = pointOnSurface;
				vertex.Normal = surfaceNormal;
				vertex.SurfaceUV = surfaceUV;
				vertex.TextureUV = SurfaceUVToMeshUV(surfaceUV);
				SurfaceInfo.Vertex vertex2 = vertex;
				if (num9 > 0)
				{
					SurfaceInfo.Quad quad = new SurfaceInfo.Quad();
					SurfaceInfo.Vertex vertex3 = info.Vertices[num4 - 1];
					Vector3 vector = pointOnSurface - vertex3.Position;
					float magnitude = vector.magnitude;
					if (i < height)
					{
						quad.X = num9 - 1;
						quad.Y = i;
						info.Quads[quad.Y * width + quad.X] = quad;
						quad.TopLeft = vertex3.Position;
						quad.TopRight = pointOnSurface;
						quad.TopDelta = vector;
						quad.TopWidth = magnitude;
					}
					if (i > 0)
					{
						quad = info.Quads[(i - 1) * width + (num9 - 1)];
						quad.BottomLeft = vertex3.Position;
						quad.BottomRight = pointOnSurface;
						quad.BottomDelta = vector;
						quad.BottomWidth = magnitude;
					}
				}
				if (i > 0)
				{
					SurfaceInfo.Vertex vertex4 = info.Vertices[num4 - width - 1];
					Vector3 vector2 = pointOnSurface - vertex4.Position;
					float magnitude2 = vector2.magnitude;
					if (num9 < width)
					{
						SurfaceInfo.Quad quad = info.Quads[(i - 1) * width + num9];
						quad.LeftDelta = vector2;
						quad.LeftHeight = magnitude2;
					}
					if (num9 > 0)
					{
						int num11 = num9 - 1;
						int num12 = i - 1;
						SurfaceInfo.Quad quad = info.Quads[num12 * width + num11];
						quad.RightDelta = vector2;
						quad.RightHeight = magnitude2;
						SurfaceInfo.Vertex vertex5 = info.Vertices[num4 - width - 2];
						Vector3 vector3 = pointOnSurface - vertex5.Position;
						quad.Position = vertex5.Position + vector3 * 0.5f;
						Vector3 a = Vector3.Cross(quad.TopDelta, quad.LeftDelta);
						Vector3 b = Vector3.Cross(quad.BottomDelta, quad.RightDelta);
						quad.Normal = Vector3.Lerp(a, b, 0.5f).normalized;
						quad.SurfaceUV = new Vector2(num10 - num * 0.5f, num8 - num2 * 0.5f);
						bool flag = !info.EdgesStraight;
						if (flag && (info.CombineQuadsH || info.CombineQuadsV))
						{
							float num13 = 10f;
							Vector3 lhs = quad.TopDelta / quad.TopWidth;
							Vector3 rhs = quad.LeftDelta / quad.LeftHeight;
							float num14 = 57.29578f * Mathf.Acos(Vector3.Dot(lhs, rhs));
							if (Mathf.Abs(num14 - 90f) < num13)
							{
								Vector3 lhs2 = -quad.RightDelta / quad.RightHeight;
								Vector3 rhs2 = -quad.BottomDelta / quad.BottomWidth;
								float num15 = 57.29578f * Mathf.Acos(Vector3.Dot(lhs2, rhs2));
								quad.IsRect = Mathf.Abs(num15 - 90f) < num13;
							}
						}
						if (num12 == 0)
						{
							if (num11 == 0)
							{
								SurfaceInfo.ColliderArea colliderArea = new SurfaceInfo.ColliderArea();
								colliderArea.quads.Add(quad);
								quad.Collider = colliderArea;
								info.Colliders.Add(colliderArea);
							}
							else
							{
								SurfaceInfo.Quad quad2 = info.Quads[num12 * width + (num11 - 1)];
								if (CombineQuad(quad2, quad, spanX, spanY, info.CombineQuadsH, flag))
								{
									(quad.Collider = quad2.Collider).quads.Add(quad);
								}
								else
								{
									SurfaceInfo.ColliderArea colliderArea = new SurfaceInfo.ColliderArea();
									colliderArea.quads.Add(quad);
									quad.Collider = colliderArea;
									info.Colliders.Add(colliderArea);
								}
							}
						}
						else if (num11 == 0)
						{
							SurfaceInfo.Quad quad2 = info.Quads[(num12 - 1) * width + num11];
							if (CombineQuad(quad2, quad, spanX, spanY, info.CombineQuadsV, flag))
							{
								SurfaceInfo.ColliderArea colliderArea = quad2.Collider;
								quad.Collider = colliderArea;
								quad2.Collider.quads.Add(quad);
							}
							else
							{
								SurfaceInfo.ColliderArea colliderArea = new SurfaceInfo.ColliderArea();
								colliderArea.quads.Add(quad);
								quad.Collider = colliderArea;
								info.Colliders.Add(colliderArea);
							}
						}
						else
						{
							SurfaceInfo.Quad quad2 = info.Quads[num12 * width + num11 - 1];
							if (CombineQuad(quad2, quad, spanX, spanY, info.CombineQuadsH, flag))
							{
								(quad.Collider = quad2.Collider).quads.Add(quad);
							}
							else
							{
								SurfaceInfo.Quad quad3 = info.Quads[(num12 - 1) * width + num11];
								if (CombineQuad(quad3, quad, spanX, spanY, info.CombineQuadsV, flag))
								{
									(quad.Collider = quad3.Collider).quads.Add(quad);
								}
								else
								{
									if (quad2.Collider == quad3.Collider)
									{
										SurfaceInfo.ColliderArea colliderArea2 = new SurfaceInfo.ColliderArea();
										for (int j = 0; j < num11; j++)
										{
											SurfaceInfo.Quad quad4 = info.Quads[num12 * width + j];
											if (quad3.Collider == quad4.Collider)
											{
												quad3.Collider.quads.Remove(quad4);
												quad4.Collider = colliderArea2;
												colliderArea2.quads.Add(quad4);
											}
										}
										if (colliderArea2.quads.Count > 0)
										{
											info.Colliders.Add(colliderArea2);
										}
									}
									SurfaceInfo.ColliderArea colliderArea = (quad.Collider = new SurfaceInfo.ColliderArea());
									colliderArea.quads.Add(quad);
									info.Colliders.Add(colliderArea);
								}
							}
						}
					}
				}
				info.Vertices[num4] = vertex2;
				num9++;
				num4++;
			}
		}
		float num16 = 0.1f;
		float num17 = 0.1f;
		for (int j = 0; j < info.Colliders.Count; j++)
		{
			SurfaceInfo.ColliderArea colliderArea3 = info.Colliders[j];
			SurfaceInfo.Quad quad5 = colliderArea3.quads[0];
			Vector3 vector4 = ((quad5.X != 0 || !(quad5.TopWidth > 0f)) ? Vector3.zero : (quad5.TopDelta * (num16 / quad5.TopWidth)));
			Vector3 vector5 = ((quad5.Y != 0 || !(quad5.LeftHeight > 0f)) ? Vector3.zero : (quad5.LeftDelta * (num17 / quad5.LeftHeight)));
			colliderArea3.TopLeft = quad5.TopLeft + vector4 + vector5;
			SurfaceInfo.Quad quad6 = colliderArea3.quads[colliderArea3.quads.Count - 1];
			vector4 = ((quad5.X != width - 1 || !(quad6.BottomWidth > 0f)) ? Vector3.zero : (-quad6.BottomDelta * (num16 / quad6.BottomWidth)));
			vector5 = ((quad5.Y != height - 1 || !(quad6.RightHeight > 0f)) ? Vector3.zero : (-quad6.RightDelta * (num17 / quad6.RightHeight)));
			colliderArea3.BottomRight = quad6.BottomRight + vector4 + vector5;
			if (colliderArea3.quads.Count > 1)
			{
				SurfaceInfo.Quad quad7 = info.Quads[quad5.Y * width + quad6.X];
				vector4 = ((quad7.X != width - 1 || !(quad7.TopWidth > 0f)) ? Vector3.zero : (-quad7.TopDelta * (num16 / quad7.TopWidth)));
				vector5 = ((quad7.Y != 0 || !(quad7.RightHeight > 0f)) ? Vector3.zero : (quad7.RightDelta * (num17 / quad7.RightHeight)));
				colliderArea3.TopRight = quad7.TopRight + vector4 + vector5;
				SurfaceInfo.Quad quad8 = info.Quads[quad6.Y * width + quad5.X];
				vector4 = ((quad8.X != 0 || !(quad8.BottomWidth > 0f)) ? Vector3.zero : (quad8.BottomDelta * (num16 / quad8.BottomWidth)));
				vector5 = ((quad8.Y != height - 1 || !(quad8.LeftHeight > 0f)) ? Vector3.zero : (-quad8.LeftDelta * (num17 / quad8.LeftHeight)));
				colliderArea3.BottomLeft = quad8.BottomLeft + vector4 + vector5;
			}
			else
			{
				vector4 = ((quad5.X != width - 1 || !(quad5.TopWidth > 0f)) ? Vector3.zero : (-quad5.TopDelta * (num16 / quad5.TopWidth)));
				vector5 = ((quad5.Y != 0 || !(quad5.RightHeight > 0f)) ? Vector3.zero : (quad5.RightDelta * (num17 / quad5.RightHeight)));
				colliderArea3.TopRight = quad5.TopRight + vector4 + vector5;
				vector4 = ((quad5.X != 0 || !(quad5.BottomWidth > 0f)) ? Vector3.zero : (quad5.BottomDelta * (num16 / quad5.BottomWidth)));
				vector5 = ((quad5.Y != height - 1 || !(quad5.LeftHeight > 0f)) ? Vector3.zero : (-quad5.LeftDelta * (num17 / quad5.LeftHeight)));
				colliderArea3.BottomLeft = quad5.BottomLeft + vector4 + vector5;
			}
		}
		for (int j = 0; j < info.Quads.Length; j++)
		{
			info.DragNormal += info.Quads[j].Normal / info.Quads.Length;
			float num18 = 0.5f * Vector3.Cross(info.Quads[j].TopRight - info.Quads[j].BottomLeft, info.Quads[j].TopLeft - info.Quads[j].BottomRight).magnitude;
			info.SurfaceArea += num18;
		}
		info.DragNormal = info.DragNormal.normalized;
		UpdateDragDisplay();
	}

	private SurfaceInfo.Quad FindCollideCorner(List<SurfaceInfo.Quad> quads, Quaternion boxPlaneRotation, int cornerPosition)
	{
		SurfaceInfo.Quad result = null;
		float num = float.MinValue;
		float num2 = float.MaxValue;
		for (int i = 0; i < quads.Count; i++)
		{
			Vector3 vector = boxPlaneRotation * quads[i].Position;
			switch (cornerPosition)
			{
			case 0:
				if (vector.y + vector.x > num)
				{
					num = vector.y + vector.x;
					result = quads[i];
				}
				break;
			case 1:
				if (vector.y + (0f - vector.x) > num)
				{
					num = vector.y + (0f - vector.x);
					result = quads[i];
				}
				break;
			case 2:
				if (0f - vector.y + vector.x > num)
				{
					num = 0f - vector.y + vector.x;
					result = quads[i];
				}
				break;
			case 3:
				if (vector.y + vector.x < num2)
				{
					num2 = vector.y + vector.x;
					result = quads[i];
				}
				break;
			}
		}
		return result;
	}

	private bool IsQuadInArea(SurfaceInfo.Quad quad, Quaternion boxPlaneRotation, float minY, float maxY, float minX, float maxX)
	{
		if (quad == null)
		{
			return false;
		}
		Vector3 vector = boxPlaneRotation * quad.Position;
		maxY -= quad.LeftHeight / 3f;
		minY += quad.RightHeight / 3f;
		maxX -= quad.TopWidth / 3f;
		minX += quad.BottomWidth / 3f;
		return vector.y >= minY && vector.y <= maxY && vector.x >= minX && vector.x <= maxX;
	}

	public override void SetRotation(Quaternion rot)
	{
		base.SetRotation(rot);
		UpdateDragDisplay();
	}

	private void OnHudHide()
	{
		if (StatMaster.Mode.displayDrag && !isSimulating && currentType.hasAerodynamics && aero.IsActive && base.ParentMachine.isLocalMachine)
		{
			for (int i = 0; i < dragVisualisers.Length; i++)
			{
				LineRenderer lineRenderer = dragVisualisers[i];
				lineRenderer.gameObject.SetActive(!StatMaster.hudHidden && lineRenderer.enabled);
			}
		}
	}

	private void UpdateAerodynamic(bool b)
	{
		UpdateDragDisplay();
	}

	public void UpdateDragDisplay()
	{
		if (isSimulating || info == null || !base.ParentMachine.isLocalMachine || base.InWater)
		{
			return;
		}
		if (!StatMaster.Mode.displayDrag || !currentType.hasAerodynamics || !aero.IsActive)
		{
			for (int i = 0; i < dragVisualisers.Length; i++)
			{
				dragVisualisers[i].gameObject.SetActive(false);
			}
			return;
		}
		int[] array = new int[4]
		{
			0,
			(info.Width + 1) * info.Height,
			(info.Width + 1) * (info.Height + 1) - 1,
			info.Width
		};
		Vector3[] array2 = CalculatePredictedDragForces(AeroDynamicDisplay.MovementDirection * 30f);
		for (int j = 0; j < dragVisualisers.Length; j++)
		{
			LineRenderer lineRenderer = dragVisualisers[j];
			if (j < nodes.Length)
			{
				SurfaceInfo.Vertex vertex = info.Vertices[array[j]];
				float magnitude = array2[j].magnitude;
				if (magnitude > 0.07f)
				{
					Vector3 vector = Vector3.ClampMagnitude(array2[j], dragVisScale.Evaluate(magnitude));
					Vector3 vector2 = vertex.Position + vector;
					Vector3 position = vertex.Position;
					Vector3 direction = vector2 - position;
					lineRenderer.transform.up = base.transform.TransformDirection(direction);
					lineRenderer.transform.localPosition = vector2;
					float t = Mathf.InverseLerp(30f, 40f, magnitude);
					t = Mathf.Lerp(0.05f, 0.1f, t);
					lineRenderer.SetWidth(t, t);
					lineRenderer.enabled = true;
					lineRenderer.gameObject.SetActive(!StatMaster.hudHidden);
					lineRenderer.SetPositions(new Vector3[2]
					{
						new Vector3(0f, 0.2f, 0f),
						lineRenderer.transform.InverseTransformPoint(base.transform.TransformPoint(position))
					});
				}
				else
				{
					lineRenderer.gameObject.SetActive(false);
					lineRenderer.enabled = false;
				}
			}
			else
			{
				lineRenderer.gameObject.SetActive(false);
				lineRenderer.enabled = false;
			}
		}
	}

	public void SetOutlineForMapper(bool select)
	{
		int state = ((!select) ? (IsSelected ? 1 : 0) : (IsSelected ? 1 : 2));
		VisualController.UpdateOutline(state);
		VisualController.freezeOutline = select;
	}

	private Vector3[] CalculatePredictedDragForces(Vector3 worldMovementDirection)
	{
		if (!currentType.hasAerodynamics || !aero.IsActive)
		{
			return null;
		}
		Vector3[] array = new Vector3[nodes.Length];
		float surfaceArea = info.SurfaceArea;
		int[] array2 = new int[4]
		{
			0,
			(info.Width + 1) * info.Height,
			(info.Width + 1) * (info.Height + 1) - 1,
			info.Width
		};
		for (int i = 0; i < nodes.Length; i++)
		{
			SurfaceInfo.Vertex vertex = info.Vertices[array2[i]];
			Vector3 normal = vertex.Normal;
			Vector3 direction = worldMovementDirection;
			transformedDirection = base.transform.InverseTransformDirection(direction);
			float num = Vector3.Dot(normal, -transformedDirection);
			float num2 = Mathf.Min(direction.sqrMagnitude, currentType.dragVelocityCap);
			float num3 = num * num2 * currentType.dragMultiplier * surfaceArea;
			num3 /= (float)nodes.Length;
			array[i] = num3 * normal;
		}
		return array;
	}

	public bool UpdateSurface(bool forceUpdate = false)
	{
		if (!isValid)
		{
			return false;
		}
		UpdateNodes();
		if (!isValid)
		{
			return false;
		}
		if (info != null && !forceUpdate && !IsDirty())
		{
			info.UpdateTransformData(nodes, edges);
			return false;
		}
		CreateSurfaceInfo();
		CalculateMaterialSizeIndex();
		UpdateSurfaceMaterial();
		GenerateMesh();
		UpdateTiling();
		GenerateColliders();
		GenerateFractureFragments();
		SetupJointTriggersAndAddingPoints();
		if (StatMaster.isMP && base.ParentMachine.isLocalMachine)
		{
			ToggleAddpoints(StatMaster.Mode.selectedTool);
		}
		else
		{
			ToggleAddpoints(StatMaster.Tool.None);
		}
		SetupFire();
		SetupBreakParticles();
		UpdateHealth();
		_isDirty = false;
		FireParticles.transform.position = GetCenter();
		return true;
	}

	public void UpdateMesh()
	{
		if (!isValid)
		{
			return;
		}
		UpdateNodes();
		if (isValid && IsDirty(true))
		{
			bool flag = StatMaster.Mode.isTranslating || StatMaster.Mode.isRotating || StatMaster.Mode.isScaling;
			CreateSurfaceInfo();
			GenerateMesh();
			if (!flag)
			{
				UpdateMeshCollider();
			}
			UpdateTiling();
			_isDirty = true;
		}
	}

	public bool IsDirty(bool meshOnly = false)
	{
		info.localMatrix = Matrix4x4.TRS(Position, Rotation, Scale).inverse;
		if (!meshOnly && _isDirty)
		{
			return true;
		}
		if (info.Nodes.Length != nodes.Length)
		{
			return true;
		}
		for (int i = 0; i < nodes.Length; i++)
		{
			if (info.Nodes[i] != info.localMatrix.MultiplyPoint3x4(nodes[i].Position) || info.Edges[i].Position != info.localMatrix.MultiplyPoint3x4(edges[i].Position))
			{
				return true;
			}
		}
		return false;
	}

	public void UpdateMass()
	{
		volume = CompoundColliderSize();
		if (!noRigidbody)
		{
			if (massSlider.Value > 0f)
			{
				Rigidbody.mass = massSlider.Value;
				originalMassDensity = originalMassDensityOverrideWithCustomMass;
			}
			else
			{
				float num = Mathf.Clamp(volume * currentType.density, currentType.minimumMass, currentType.maximumMass);
				float mass = num;
				Rigidbody.mass = mass;
				originalMassDensity = mass;
			}
			UpdateJointBreakForces();
			if (FragmentController != null)
			{
				FragmentController.UpdateMass(Rigidbody.mass);
			}
		}
	}

	private void UpdateJointBreakForces()
	{
		float breakForce = currentType.jointBaseBreakForce + Rigidbody.mass * currentType.jointBreakForceScaler;
		float breakTorque = currentType.jointBaseBreakForce + Rigidbody.mass * currentType.jointBreakTorqueScaler;
		for (int i = 0; i < Joints.Length; i++)
		{
			if (Joints[i] != null)
			{
				Joints[i].breakForce = breakForce;
				Joints[i].breakTorque = breakTorque;
			}
		}
		startBreakForces = breakForce;
		startBreakTorques = breakTorque;
	}

	private void UpdateHealth()
	{
		if (currentType.hasHealth)
		{
			float surfaceArea = info.SurfaceArea;
			surfaceArea = Mathf.Clamp(surfaceArea, 1f, 25f);
			float t = (surfaceArea - 1f) / 24f;
			BlockHealth.health = Mathf.Lerp(2f, 8f, t);
		}
	}

	public void UpdateMeshCollider()
	{
		Mesh sharedMesh = VisualController.MeshFilter.sharedMesh;
		if (UseMeshColliderInSim && isSimulating && version > 0 && colliderMesh != null)
		{
			sharedMesh = colliderMesh;
		}
		meshCollider.sharedMesh = ((!meshCollider.enabled) ? null : sharedMesh);
	}

	private float GetColliderThickness()
	{
		return (!(currentThickness > 0.12f)) ? 0.036f : (currentThickness - 0.08f);
	}

	private Mesh CreatePlanarCollider()
	{
		Mesh mesh = new Mesh();
		mesh.name = "Planar Mesh Collider";
		Mesh mesh2 = mesh;
		Plane plane = new Plane(info.Nodes[0], info.Nodes[1], info.Nodes[2]);
		float num = 0.1f;
		Vector3 normal = plane.normal;
		Vector3 vector = normal * GetColliderThickness();
		Vector3 vector2 = info.Nodes[1] - info.Nodes[0];
		Vector3 vector3 = info.Nodes[2] - info.Nodes[1];
		vector2 = Vector3.ProjectOnPlane(vector2, plane.normal).normalized * num;
		vector3 = Vector3.ProjectOnPlane(vector3, plane.normal).normalized * num;
		Vector3[] vertices;
		int[] triangles;
		if (info.IsQuad)
		{
			Vector3 vector4 = info.Nodes[3] - info.Nodes[0];
			Vector3 vector5 = info.Nodes[3] - info.Nodes[2];
			vector4 = Vector3.ProjectOnPlane(vector4, plane.normal).normalized * num;
			vector5 = Vector3.ProjectOnPlane(vector5, plane.normal).normalized * num;
			Vector3 vector6 = vector2 + vector4;
			Vector3 vector7 = -vector2 + vector3;
			Vector3 vector8 = -vector3 + vector5;
			Vector3 vector9 = -vector5 - vector4;
			vertices = new Vector3[16]
			{
				info.Nodes[0] + vector6 + vector,
				info.Nodes[1] + vector7 + vector,
				info.Nodes[2] + vector8 + vector,
				info.Nodes[3] + vector9 + vector,
				info.Nodes[0] + vector6 - vector,
				info.Nodes[1] + vector7 - vector,
				info.Nodes[2] + vector8 - vector,
				info.Nodes[3] + vector9 - vector,
				info.Edges[0].Position + (vector6 + vector7) * 0.5f + vector,
				info.Edges[1].Position + (vector7 + vector8) * 0.5f + vector,
				info.Edges[2].Position + (vector8 + vector9) * 0.5f + vector,
				info.Edges[3].Position + (vector9 + vector6) * 0.5f + vector,
				info.Edges[0].Position + (vector6 + vector7) * 0.5f - vector,
				info.Edges[1].Position + (vector7 + vector8) * 0.5f - vector,
				info.Edges[2].Position + (vector8 + vector9) * 0.5f - vector,
				info.Edges[3].Position + (vector9 + vector6) * 0.5f - vector
			};
			triangles = new int[84]
			{
				0, 2, 1, 0, 3, 2, 0, 1, 8, 1,
				2, 9, 2, 3, 10, 3, 0, 11, 4, 6,
				5, 4, 7, 6, 4, 5, 12, 5, 6, 13,
				6, 7, 14, 7, 4, 15, 0, 8, 12, 12,
				4, 0, 1, 8, 12, 12, 5, 1, 1, 9,
				13, 13, 5, 1, 2, 9, 13, 13, 6, 2,
				2, 10, 14, 14, 6, 2, 3, 10, 14, 14,
				7, 3, 3, 11, 15, 15, 7, 3, 0, 11,
				15, 15, 4, 0
			};
		}
		else
		{
			Vector3 vector10 = info.Nodes[2] - info.Nodes[0];
			vector10 = Vector3.ProjectOnPlane(vector10, plane.normal).normalized * num;
			Vector3 vector11 = vector2 + vector10;
			Vector3 vector12 = -vector2 + vector3;
			Vector3 vector13 = -vector3 - vector10;
			vertices = new Vector3[12]
			{
				info.Nodes[0] + vector11 + vector,
				info.Nodes[1] + vector12 + vector,
				info.Nodes[2] + vector13 + vector,
				info.Nodes[0] + vector11 - vector,
				info.Nodes[1] + vector12 - vector,
				info.Nodes[2] + vector13 - vector,
				info.Edges[0].Position + (vector11 + vector12) * 0.5f + vector,
				info.Edges[1].Position + (vector12 + vector13) * 0.5f + vector,
				info.Edges[2].Position + (vector13 + vector11) * 0.5f + vector,
				info.Edges[0].Position + (vector11 + vector12) * 0.5f - vector,
				info.Edges[1].Position + (vector12 + vector13) * 0.5f - vector,
				info.Edges[2].Position + (vector13 + vector11) * 0.5f - vector
			};
			triangles = new int[60]
			{
				0, 2, 1, 0, 1, 6, 1, 2, 7, 2,
				0, 8, 3, 5, 4, 3, 4, 9, 4, 5,
				10, 5, 3, 11, 0, 6, 9, 9, 3, 0,
				1, 6, 9, 9, 4, 1, 1, 7, 10, 10,
				4, 1, 2, 7, 10, 10, 5, 2, 2, 8,
				11, 11, 5, 2, 0, 8, 11, 11, 3, 0
			};
		}
		mesh2.vertices = vertices;
		mesh2.triangles = triangles;
		mesh2.Optimize();
		return mesh2;
	}

	private float CompoundColliderSize()
	{
		if (IsZeroVolume())
		{
			return 0f;
		}
		float num = 0f;
		for (int i = 0; i < colliders.Count; i++)
		{
			Vector3 size = colliders[i].size;
			num += size.x * size.y * size.z;
		}
		return num;
	}

	private void ToggledCollision(bool hasCollision)
	{
		collisionActive = hasCollision;
		if (colliders == null || colliders.Count == 0)
		{
			if (hasCollision && info != null)
			{
				GenerateColliders();
			}
		}
		else if (!hasCollision)
		{
			foreach (BoxCollider collider in colliders)
			{
				UnityEngine.Object.Destroy(collider.gameObject);
			}
			colliders.Clear();
		}
		if (!hasCollision)
		{
			UpdateMass();
		}
	}

	private bool VectorAproximately(Vector3 a, Vector3 b)
	{
		return (b - a).sqrMagnitude < 0.001f;
	}

	private void GenerateColliders()
	{
		bool flag = IsZeroVolume();
		if (colliders == null)
		{
			colliders = new List<BoxCollider>();
		}
		else
		{
			foreach (BoxCollider collider in colliders)
			{
				UnityEngine.Object.Destroy(collider.gameObject);
			}
			colliders.Clear();
		}
		if (!flag && collisionActive)
		{
			for (int i = 0; i < info.Colliders.Count; i++)
			{
				BoxCollider boxCollider = FindAndCreateQuadCollider(info.Colliders[i], simColliderParent.transform);
				if (!boxCollider)
				{
					continue;
				}
				bool flag2 = false;
				for (int j = 0; j < colliders.Count; j++)
				{
					BoxCollider boxCollider2 = colliders[j];
					if (VectorAproximately(boxCollider.size, boxCollider2.size) && VectorAproximately(boxCollider.transform.localPosition, boxCollider2.transform.localPosition))
					{
						UnityEngine.Object.Destroy(boxCollider.gameObject);
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					colliders.Add(boxCollider);
				}
			}
		}
		meshCollider.enabled = !flag && !hasNoThickness;
		if (info.EdgesPlanar)
		{
			UnityEngine.Object.Destroy(colliderMesh);
			colliderMesh = CreatePlanarCollider();
		}
		UpdateMeshCollider();
		UpdateMass();
		SingleInstance<Events>.Instance.SurfaceCollidersGenated(this);
	}

	private bool IsZeroVolume()
	{
		if (nodes.Length < 2)
		{
			return true;
		}
		if (base.transform.localScale.x == 0f && base.transform.localScale.y == 0f && base.transform.localScale.z == 0f)
		{
			return true;
		}
		float num = 0.0025f;
		float num2 = 0.9999f;
		Vector3 lhs = nodes[1].transform.position - nodes[0].transform.position;
		Vector3 vector = edges[1].transform.position - edges[0].transform.position;
		float num3 = Mathf.Abs(Vector3.Dot(lhs, vector.normalized));
		if (num3 < num2)
		{
			return false;
		}
		if (lhs.sqrMagnitude < num)
		{
			return true;
		}
		lhs = lhs.normalized;
		for (int i = 2; i < nodes.Length; i++)
		{
			int num4 = i - 1;
			vector = nodes[i].transform.position - nodes[num4].transform.position;
			if (vector.sqrMagnitude < num)
			{
				return true;
			}
			num3 = Mathf.Abs(Vector3.Dot(lhs, vector.normalized));
			if (num3 < num2)
			{
				return false;
			}
		}
		lhs = (edges[1].transform.position - edges[0].transform.position).normalized;
		for (int j = 2; j < edges.Length; j++)
		{
			int num5 = j - 1;
			num3 = Mathf.Abs(Vector3.Dot(lhs, (edges[j].transform.position - edges[num5].transform.position).normalized));
			if (num3 < num2)
			{
				return false;
			}
		}
		return true;
	}

	private void CreateFragmentColliders()
	{
		if (info == null)
		{
			Debug.LogError("Missing SurfaceInfo on CreateFragmentColliders");
			return;
		}
		int num = 9;
		float num2 = 1f / 3f;
		float num3 = 1f / 3f;
		int num4 = ((!info.IsQuad) ? num : 0);
		for (int i = 0; i < 3; i++)
		{
			float num5 = (float)i / 3f;
			for (int j = 0; j < 3; j++)
			{
				float num6 = (float)j / 3f;
				int num7 = i * 3 + j + num4;
				int num8 = currentFracturePattern.ColliderFragmentMapping[num7];
				if (num8 != -1)
				{
					Vector2 vector = MeshUVToSurfaceUVNoAdjustedTris(new Vector2(num6, num5));
					Vector2 vector2 = MeshUVToSurfaceUVNoAdjustedTris(new Vector2(num6 + num2, num5));
					Vector2 vector3 = MeshUVToSurfaceUVNoAdjustedTris(new Vector2(num6, num5 + num3));
					Vector2 vector4 = MeshUVToSurfaceUVNoAdjustedTris(new Vector2(num6 + num2, num5 + num3));
					SurfaceInfo.ColliderArea colliderArea = new SurfaceInfo.ColliderArea();
					colliderArea.BottomLeft = GetPointOnSurface(vector.x, vector.y);
					colliderArea.BottomRight = GetPointOnSurface(vector2.x, vector2.y);
					colliderArea.TopLeft = GetPointOnSurface(vector3.x, vector3.y);
					colliderArea.TopRight = GetPointOnSurface(vector4.x, vector4.y);
					SurfaceInfo.ColliderArea area = colliderArea;
					FindAndCreateQuadCollider(area, FragmentController.fragments[num8].Renderer.transform, 0.9f);
				}
			}
		}
	}

	private BoxCollider FindAndCreateQuadCollider(SurfaceInfo.ColliderArea area, Transform parent, float sizeMult = 1f)
	{
		Vector3 topLeft = area.TopLeft;
		Vector3 topRight = area.TopRight;
		Vector3 bottomRight = area.BottomRight;
		Vector3 bottomLeft = area.BottomLeft;
		Vector3 a = topRight - topLeft;
		Vector3 b = bottomRight - bottomLeft;
		Vector3 a2 = bottomRight - topRight;
		Vector3 b2 = bottomLeft - topLeft;
		Vector3 vector = Vector3.Lerp(a, b, 0.5f);
		Vector3 vector2 = Vector3.Lerp(a2, b2, 0.5f);
		bool flag = vector.sqrMagnitude > vector2.sqrMagnitude;
		Vector3 normalized;
		Vector3 vector3;
		if (flag)
		{
			normalized = vector.normalized;
			vector3 = Vector3.Lerp(topLeft, bottomLeft, 0.5f);
		}
		else
		{
			normalized = vector2.normalized;
			vector3 = Vector3.Lerp(topLeft, topRight, 0.5f);
		}
		Vector3 vector4 = topLeft - vector3;
		Vector3 vector5 = topRight - vector3;
		Vector3 vector6 = bottomRight - vector3;
		Vector3 vector7 = bottomLeft - vector3;
		Vector3 vector8 = Vector3.Project(vector4, normalized);
		Vector3 vector9 = Vector3.Project(vector5, normalized);
		Vector3 vector10 = Vector3.Project(vector6, normalized);
		Vector3 vector11 = Vector3.Project(vector7, normalized);
		Vector3 vector14;
		float num;
		Vector3 normal;
		if (flag)
		{
			Vector3 vector12 = Vector3.Lerp(vector4, vector7, 0.5f);
			Vector3 vector13 = Vector3.Lerp(vector5, vector6, 0.5f);
			float sqrMagnitude = (vector13 - vector8).sqrMagnitude;
			float sqrMagnitude2 = (vector13 - vector11).sqrMagnitude;
			float sqrMagnitude3 = (vector12 - vector9).sqrMagnitude;
			float sqrMagnitude4 = (vector12 - vector10).sqrMagnitude;
			vector14 = ((!(sqrMagnitude < sqrMagnitude2)) ? vector11 : vector8);
			num = Vector3.Distance(vector14, (!(sqrMagnitude3 < sqrMagnitude4)) ? vector10 : vector9);
			normal = Vector3.Cross(normalized, (!(sqrMagnitude < sqrMagnitude2)) ? (Vector3.Lerp(vector7, -vector5, 0.5f) - vector11) : (Vector3.Lerp(vector4, -vector6, 0.5f) - vector8));
		}
		else
		{
			Vector3 vector15 = Vector3.Lerp(vector4, vector5, 0.5f);
			Vector3 vector16 = Vector3.Lerp(vector6, vector7, 0.5f);
			float sqrMagnitude5 = (vector16 - vector8).sqrMagnitude;
			float sqrMagnitude6 = (vector16 - vector9).sqrMagnitude;
			float sqrMagnitude7 = (vector15 - vector10).sqrMagnitude;
			float sqrMagnitude8 = (vector15 - vector11).sqrMagnitude;
			vector14 = ((!(sqrMagnitude5 < sqrMagnitude6)) ? vector9 : vector8);
			num = Vector3.Distance(vector14, (!(sqrMagnitude7 < sqrMagnitude8)) ? vector11 : vector10);
			normal = Vector3.Cross(normalized, (!(sqrMagnitude5 < sqrMagnitude6)) ? (Vector3.Lerp(vector5, -vector7, 0.5f) - vector9) : (Vector3.Lerp(vector4, -vector6, 0.5f) - vector8));
		}
		float num2 = Mathf.Sqrt(Mathf.Min((vector8 - vector4).sqrMagnitude, (vector9 - vector5).sqrMagnitude, (vector10 - vector6).sqrMagnitude, (vector11 - vector7).sqrMagnitude)) * 2f;
		if (num2 < 0.01f || num < 0.01f)
		{
			return null;
		}
		return CreateQuadCollider(parent, vector3 + vector14, normalized, normal, num2, num, sizeMult);
	}

	private BoxCollider CreateQuadCollider(Transform parent, Vector3 bottomCenter, Vector3 dir, Vector3 normal, float width, float height, float sizeMult = 1f)
	{
		Vector3 pos = bottomCenter + dir * (height * 0.5f);
		Quaternion rot = Quaternion.LookRotation(dir, normal);
		Vector3 scale = new Vector3(width * sizeMult, GetColliderThickness() * 2f, height * sizeMult);
		return AddCollider(parent, pos, rot, scale);
	}

	private BoxCollider CreateQuadCollider(Transform parent, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
	{
		Vector3 vector = d - a;
		Vector3 b2 = c - b;
		Vector3 vector2 = b - a;
		Vector3 b3 = c - d;
		Vector3 pos = (a + b + c + d) / 4f;
		Vector3 scale = new Vector3(GetColliderThickness() * 2f, Vector3.Lerp(vector, b2, 0.5f).magnitude, Vector3.Lerp(vector2, b3, 0.5f).magnitude);
		Quaternion rot = Quaternion.LookRotation(vector2, vector);
		return AddCollider(parent, pos, rot, scale);
	}

	private BoxCollider AddCollider(Transform parent, Vector3 pos, Quaternion rot, Vector3 scale)
	{
		GameObject gameObject = new GameObject("coll" + parent.childCount, typeof(BoxCollider));
		Transform transform = gameObject.transform;
		transform.SetParent(parent, false);
		transform.localPosition = pos;
		transform.localRotation = rot;
		transform.localScale = Vector3.one;
		BoxCollider component = gameObject.GetComponent<BoxCollider>();
		component.center = Vector3.zero;
		component.size = scale;
		return component;
	}

	public Joint GetJointForTrigger(TriggerSetJointSurface trigger)
	{
		return Joints[Array.IndexOf(JointTriggers, trigger)];
	}

	public void CheckJoint(TriggerSetJointSurface trigger, List<Collider> consideredColliders)
	{
		int index = Array.IndexOf(JointTriggers, trigger);
		Joint jointForTrigger = GetJointForTrigger(trigger);
		if (jointForTrigger.connectedBody == null)
		{
			UnityEngine.Object.Destroy(jointForTrigger);
			for (int i = 0; i < consideredColliders.Count; i++)
			{
				if (consideredColliders[i].isTrigger)
				{
					continue;
				}
				if (UseMeshColliderInSim)
				{
					Physics.IgnoreCollision(consideredColliders[i], meshCollider);
					continue;
				}
				for (int j = 0; j < colliders.Count; j++)
				{
					Physics.IgnoreCollision(consideredColliders[i], colliders[j]);
				}
			}
		}
		else
		{
			BuildSurface component = jointForTrigger.connectedBody.GetComponent<BuildSurface>();
			if (component != null)
			{
				float breakForce = Mathf.Max(startBreakForces, component.startBreakForces);
				float breakTorque = Mathf.Max(startBreakTorques, component.startBreakTorques);
				jointForTrigger.breakForce = breakForce;
				jointForTrigger.breakTorque = breakTorque;
			}
		}
		List<BlockBehaviour> list = new List<BlockBehaviour>();
		for (int k = 0; k < consideredColliders.Count; k++)
		{
			if (!(consideredColliders[k].attachedRigidbody == null))
			{
				BlockBehaviour component2 = consideredColliders[k].attachedRigidbody.GetComponent<BlockBehaviour>();
				if (!(component2 == null) && !list.Contains(component2))
				{
					list.Add(component2);
				}
			}
		}
		if (FragmentController != null)
		{
			FragmentController.OnConnectionEstablished(index, list);
		}
	}

	public void OnSetJoint(Joint joint, Collider addingPoint, List<Collider> consideredColliders)
	{
		if (connectedBlocks == null)
		{
			connectedBlocks = new GameObject[nodes.Length];
		}
		int num = Array.IndexOf(Joints, (ConfigurableJoint)joint);
		connectedBlocks[num] = joint.connectedBody.gameObject;
		FragmentVisualController component = joint.connectedBody.gameObject.GetComponent<FragmentVisualController>();
		if (component != null)
		{
			component.onVisualBreak = (Action)System.Delegate.Combine(component.onVisualBreak, (Action)delegate
			{
				UnityEngine.Object.Destroy(joint);
			});
		}
		BuildSurface component2 = joint.connectedBody.GetComponent<BuildSurface>();
		if ((bool)component2)
		{
			if (component2.isValid)
			{
				component2.OnSetJointFrom(joint, addingPoint, this);
			}
			else
			{
				UnityEngine.Object.Destroy(joint);
			}
		}
		for (int num2 = 0; num2 < consideredColliders.Count; num2++)
		{
			if (consideredColliders[num2].isTrigger || !(consideredColliders[num2] != addingPoint))
			{
				continue;
			}
			if (UseMeshColliderInSim)
			{
				Physics.IgnoreCollision(consideredColliders[num2], meshCollider);
				continue;
			}
			for (int num3 = 0; num3 < colliders.Count; num3++)
			{
				Physics.IgnoreCollision(consideredColliders[num2], colliders[num3]);
			}
		}
	}

	public void OnSetJointFrom(Joint joint, Collider myAddingPoint, BuildSurface other)
	{
		if (connectedBlocks == null)
		{
			connectedBlocks = new GameObject[nodes.Length];
		}
		int num = Array.IndexOf(AddingPoints, myAddingPoint);
		connectedBlocks[num] = other.gameObject;
	}

	public GameObject GetConnectedBlock(ConfigurableJoint j)
	{
		return connectedBlocks[Array.IndexOf(Joints, j)];
	}

	private void SetupJointTriggersAndAddingPoints()
	{
		if (!noRigidbody)
		{
			if (info.IsQuad && Joints[3] == null)
			{
				Joints[3] = Joints[0].gameObject.AddComponent<ConfigurableJoint>();
				JointTriggers[3].gameObject.SetActive(true);
				AddingPoints[3].enabled = true;
			}
			for (int i = 0; i < nodes.Length; i++)
			{
				JointTriggers[i].transform.localPosition = info.Nodes[i];
				JointTriggers[i].loadedPos = true;
				AddingPoints[i].transform.localPosition = info.Nodes[i];
				Joints[i].anchor = info.Nodes[i];
			}
			if (!info.IsQuad)
			{
				JointTriggers[3].gameObject.SetActive(false);
				AddingPoints[3].enabled = false;
				UnityEngine.Object.Destroy(Joints[3]);
			}
		}
	}

	public static void WriteData(XDataHolder data, BuildEdgeBlock[] edges)
	{
		if (edges == null)
		{
			return;
		}
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < edges.Length; i++)
		{
			stringBuilder.Append(edges[i].Guid.ToString());
			if (i < edges.Length - 1)
			{
				stringBuilder.Append("|");
			}
		}
		data.Write("edges", stringBuilder.ToString());
	}

	private bool GetEdge(Machine m, BuildNodeBlock startNode, BuildNodeBlock endNode, out BuildEdgeBlock edge)
	{
		for (int i = 0; i < m.BlockCount; i++)
		{
			BlockBehaviour block;
			if (m.GetBlockFromIndex(i, out block) && block.Prefab.Type == BlockType.BuildEdge)
			{
				BuildEdgeBlock buildEdgeBlock = block as BuildEdgeBlock;
				if (buildEdgeBlock.isValid && ((buildEdgeBlock.startNode == startNode && buildEdgeBlock.endNode == endNode) || (buildEdgeBlock.endNode == startNode && buildEdgeBlock.startNode == endNode)))
				{
					edge = buildEdgeBlock;
					return true;
				}
			}
		}
		Debug.LogError(string.Concat("Couldn't find edge from ", startNode.Guid, " to ", endNode.Guid));
		edge = null;
		return false;
	}

	private void UpdateNodes()
	{
		nodes = new BuildNodeBlock[edges.Length];
		for (int i = 0; i < edges.Length; i++)
		{
			BuildEdgeBlock buildEdgeBlock = edges[i];
			if (!buildEdgeBlock.isValid)
			{
				isValid = false;
				break;
			}
			BuildEdgeBlock buildEdgeBlock2 = edges[(i < edges.Length - 1) ? (i + 1) : 0];
			nodes[i] = ((!(buildEdgeBlock.endNode == buildEdgeBlock2.startNode) && !(buildEdgeBlock.endNode == buildEdgeBlock2.endNode)) ? buildEdgeBlock.endNode : buildEdgeBlock.startNode);
		}
	}

	public override void OnSave(XDataHolder data)
	{
		data.Write("bmt-version", version);
		base.OnSave(data);
		if (isValid)
		{
			WriteData(data, edges);
			data.Write("materialIndex", materialIndex);
			if (data.HasKey("bmt-surfMat"))
			{
				int num = data.ReadInt("bmt-surfMat");
				data.Write("bmt-surfMat", (num > 0) ? 2 : 0);
			}
		}
	}

	public override void OnLoad(XDataHolder data)
	{
		bool wasLoadedFromFile = data.WasLoadedFromFile;
		bool wasCreated = data.WasCreated;
		if (data.HasKey("bmt-surfMat"))
		{
			data = data.Clone();
			data.WasLoadedFromFile = wasLoadedFromFile;
			data.WasCreated = wasCreated;
			int num = data.ReadInt("bmt-surfMat");
			data.Write("bmt-surfMat", (num > 0) ? 1 : 0);
		}
		if (data.HasKey("materialIndex"))
		{
			materialIndex = data.ReadInt("materialIndex");
		}
		else if (wasCreated)
		{
			materialIndex = -1;
		}
		isLoading = true;
		if (!isSimulating)
		{
			if (!data.HasKey("bmt-version"))
			{
				if (data.WasLoadedFromFile)
				{
					version = 0;
					data.Write("bmt-version", version);
				}
			}
			else
			{
				version = data.ReadInt("bmt-version");
			}
		}
		base.OnLoad(data);
		isLoading = false;
		if (isSimulating)
		{
			return;
		}
		if (isBMAction)
		{
			WriteData(data, edges);
		}
		isValid = true;
		Machine parentMachine = base.ParentMachine;
		if (data.HasKey("nodes"))
		{
			string text = data.ReadString("nodes");
			string[] array = text.Split('|');
			nodes = new BuildNodeBlock[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				BlockBehaviour block;
				if (!parentMachine.GetBlock(new Guid(array[i]), out block))
				{
					Debug.LogError("Couldn't load node " + i + " for surface!");
					return;
				}
				nodes[i] = block as BuildNodeBlock;
			}
			edges = new BuildEdgeBlock[nodes.Length];
			for (int i = 0; i < nodes.Length; i++)
			{
				if (!GetEdge(parentMachine, nodes[i], (i >= nodes.Length - 1) ? nodes[0] : nodes[i + 1], out edges[i]))
				{
					Debug.LogError("Couldn't find node " + i + "!");
					return;
				}
			}
		}
		else
		{
			if (!data.HasKey("edges"))
			{
				Debug.LogError(string.Concat("Surface ", Guid, " doesn't contain edges!"));
				isValid = false;
				return;
			}
			string text2 = data.ReadString("edges");
			string[] array2 = text2.Split('|');
			edges = new BuildEdgeBlock[array2.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				string g = array2[i];
				BlockBehaviour block2;
				if (!parentMachine.GetBlock(new Guid(g), out block2))
				{
					isValid = false;
					return;
				}
				BuildEdgeBlock buildEdgeBlock = block2 as BuildEdgeBlock;
				if (!buildEdgeBlock.isValid)
				{
					isValid = false;
				}
				edges[i] = buildEdgeBlock;
			}
		}
		parentMachine.nodeController.Refresh(this);
	}

	public override void OnPostEdit()
	{
		base.OnPostEdit();
		WriteData(InitialState, edges);
		needsSort = true;
		base.ParentMachine.nodeController.Refresh(this);
	}

	private void SetupFire()
	{
		if (!currentType.burnable)
		{
			if ((bool)fireTag)
			{
				UnityEngine.Object.Destroy(fireTag.fireControllerCode);
				UnityEngine.Object.Destroy(fireTag);
			}
			return;
		}
		FireController fireController;
		if (!fireTag)
		{
			fireController = base.gameObject.AddComponent<FireController>();
			fireController.enabled = false;
			fireController.basicInfo = this;
			fireController.HasBasicInfo = true;
			fireController.fireParticles = FireParticles;
			fireController.earlyBurnDuration = currentType.destroyTimer;
			fireController.lateBurnDuration = currentType.onFireDuration;
			fireController.randomAmount = 2f;
			fireController.igniteDelay = 0.2f;
			fireController.displayBurnOnBlock = true;
			fireTag = base.gameObject.AddComponent<FireTag>();
			fireTag.basicInfo = this;
			fireTag.HasBasicInfo = true;
			fireTag.canBeDoused = true;
			fireTag.igniteOnce = false;
			fireTag.fireControllerCode = fireController;
			fireTag.hasController = true;
			fireController.fireTagCode = fireTag;
			fireController.hasFireTag = true;
		}
		else
		{
			fireController = fireTag.fireControllerCode;
		}
		Bounds bounds = default(Bounds);
		if (nodes.Length > 0)
		{
			bounds.center = base.transform.InverseTransformPoint(nodes[0].transform.position);
		}
		for (int i = 0; i < nodes.Length; i++)
		{
			Vector3 point = base.transform.InverseTransformPoint(nodes[i].transform.position);
			bounds.Encapsulate(point);
		}
		for (int j = 0; j < edges.Length; j++)
		{
			Vector3 point2 = base.transform.InverseTransformPoint(edges[j].transform.position);
			bounds.Encapsulate(point2);
		}
		fireController.overlapType = FireController.OverlapType.Box;
		fireController.overlapCenter = bounds.center;
		fireController.overlapSize = bounds.size * 1.5f;
		if (info != null)
		{
			ParticleSystem.EmissionModule emission = FireParticles.emission;
			emission.rate = new ParticleSystem.MinMaxCurve(info.SurfaceArea * currentType.fireParticleDensity);
		}
	}

	private void SetupBreakParticles()
	{
		if (isSimulating)
		{
			return;
		}
		if (breakParticleSystems != null)
		{
			for (int i = 0; i < breakParticleSystems.Length; i++)
			{
				UnityEngine.Object.Destroy(breakParticleSystems[i].gameObject);
			}
		}
		if (currentType.breakParticleSystems == null || currentType.breakParticleSystems.Length == 0)
		{
			breakParticleSystems = null;
			return;
		}
		breakParticleSystems = new ParticleSystem[currentType.breakParticleSystems.Length];
		for (int j = 0; j < breakParticleSystems.Length; j++)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(currentType.breakParticleSystems[j], base.transform);
			gameObject.gameObject.SetActive(false);
			ParticleSystem component = gameObject.GetComponent<ParticleSystem>();
			ParticleSystem.ShapeModule shape = component.shape;
			shape.shapeType = ParticleSystemShapeType.MeshRenderer;
			shape.meshShapeType = ParticleSystemMeshShapeType.Triangle;
			shape.meshRenderer = VisualController.renderers[0];
			breakParticleSystems[j] = component;
		}
	}

	public void PlayBreakParticles()
	{
		if (collisionActive)
		{
			PlaySound(currentType.breakSfx, 0.95f, 1.2f);
		}
		if (breakParticleSystems != null)
		{
			for (int i = 0; i < breakParticleSystems.Length; i++)
			{
				breakParticleSystems[i].gameObject.SetActive(true);
				breakParticleSystems[i].Play();
			}
		}
	}

	protected override void AddToAreodynamicsList()
	{
		if (WaterController.Exist)
		{
			WaterController.aerodynamicCount += 4;
		}
	}

	protected override void RetrieveDefaultBounds()
	{
		base.RetrieveDefaultBounds();
		hasMultipleBounds = true;
		_defaultBoundsArray = new Bounds[nodes.Length];
		_defaultBoundsRotation = new Quaternion[nodes.Length];
		Transform transform = ((!(Rigidbody != null)) ? base.transform : Rigidbody.transform);
		Vector3 vector = GetCenter();
		Plane plane = default(Plane);
		Vector3[] array = new Vector3[nodes.Length];
		Quaternion quaternion = Quaternion.Inverse(transform.rotation);
		for (int i = 0; i < nodes.Length; i++)
		{
			lowestDot = 10f;
			_defaultBoundsArray[i] = new Bounds(Vector3.zero, Vector3.zero);
			Color color = Color.Lerp(Color.red, Color.blue, (float)i / (float)nodes.Length);
			color.a = 1f;
			array[0] = quaternion * (vector - nodes[i].GetCenter()) + vector;
			array[1] = quaternion * (vector - edges[i].GetCenter()) + vector;
			array[2] = quaternion * (vector - edges[(i != 0) ? (i - 1) : (edges.Length - 1)].GetCenter()) + vector;
			if (nodes.Length == 4)
			{
				array[3] = vector;
			}
			nodes[i].boundsCenter = (array[0] + array[1] + vector + array[2]) * 0.25f;
			plane.Set3Points(array[0], array[1], array[2]);
			Vector3 normal = plane.normal;
			plane.Set3Points(vector, array[1], array[2]);
			if (Vector3.Dot(normal, plane.normal) < 0f)
			{
				normal *= -1f;
			}
			normal = (normal + plane.normal) / 2f;
			Quaternion quaternion2 = Quaternion.FromToRotation(normal, Vector3.up);
			array[0] = quaternion2 * (nodes[i].boundsCenter - array[0]);
			array[1] = quaternion2 * (nodes[i].boundsCenter - array[1]);
			array[2] = quaternion2 * (nodes[i].boundsCenter - array[2]);
			if (nodes.Length == 4)
			{
				array[3] = quaternion2 * (nodes[i].boundsCenter - array[3]);
			}
			if (nodes.Length == 4)
			{
				CompareEdges(array[0] - array[2], array[2] - array[3]);
				CompareEdges(array[2] - array[3], array[3] - array[1]);
				CompareEdges(array[3] - array[1], array[1] - array[0]);
				CompareEdges(array[1] - array[0], array[0] - array[2]);
			}
			else
			{
				CompareEdges(array[0] - array[2], array[2] - array[1]);
				CompareEdges(array[2] - array[1], array[1] - array[0]);
				CompareEdges(array[1] - array[0], array[0] - array[2]);
			}
			bestEdge.y = 0f;
			Quaternion quaternion3 = Quaternion.FromToRotation(bestEdge.normalized, Vector3.right);
			_defaultBoundsRotation[i] = Quaternion.Inverse(quaternion3 * quaternion2);
			array[0] = quaternion3 * array[0];
			array[1] = quaternion3 * array[1];
			array[2] = quaternion3 * array[2];
			if (nodes.Length == 4)
			{
				array[3] = quaternion3 * array[3];
			}
			_defaultBoundsArray[i].Encapsulate(array[0]);
			_defaultBoundsArray[i].Encapsulate(array[1]);
			_defaultBoundsArray[i].Encapsulate(array[2]);
			if (nodes.Length == 4)
			{
				_defaultBoundsArray[i].Encapsulate(array[3]);
			}
			_defaultBoundsArray[i].center = vector - nodes[i].boundsCenter;
			DebugExtension.DebugCube(transform.rotation * _defaultBoundsArray[i].center + vector, _defaultBoundsArray[i].extents * 2f, transform.rotation * _defaultBoundsRotation[i], color, 2f, false);
			_defaultBoundsArray[i].extents = _defaultBoundsRotation[i] * _defaultBoundsArray[i].extents;
		}
	}

	private void CompareEdges(Vector3 edge1, Vector3 edge2)
	{
		edge1.y = (edge2.y = 0f);
		float num = Mathf.Abs(Vector3.Dot(edge1.normalized, edge2.normalized));
		if (lowestDot > num)
		{
			bestEdge = edge1;
			lowestDot = num;
		}
	}

	public void AssignStressCorners()
	{
		if (info != null)
		{
			VisualController.AssignMaterialProperty("_Pos1", info.Nodes[0], false);
			VisualController.AssignMaterialProperty("_Pos2", info.Nodes[1], false);
			VisualController.AssignMaterialProperty("_Pos3", info.Nodes[2], false);
			if (info.IsQuad)
			{
				VisualController.AssignMaterialProperty("_Pos4", info.Nodes[3]);
			}
			else
			{
				VisualController.AssignMaterialProperty("_Pos4", Vector3.one * float.MaxValue);
			}
		}
	}

	public float[] GetStresses()
	{
		return currentStresses;
	}

	public override void SetStress(float s, Vector3 pos)
	{
		if (!isSimulating)
		{
			return;
		}
		float num = float.MaxValue;
		int num2 = 0;
		int num3 = ((!info.IsQuad) ? 3 : 4);
		for (int i = 0; i < num3; i++)
		{
			float sqrMagnitude = (info.Nodes[i] - pos).sqrMagnitude;
			if (sqrMagnitude < num)
			{
				num = sqrMagnitude;
				num2 = i;
			}
		}
		currentStresses[num2] = Mathf.Max(currentStresses[num2], s);
		stressFrame = Time.frameCount;
	}

	protected override void FadeStress()
	{
		for (int i = 0; i < info.Nodes.Length; i++)
		{
			currentStresses[i] = Mathf.Lerp(currentStresses[i], 0f, Time.deltaTime * 2f);
		}
		if (Time.frameCount > stressFrame + 5 && stressFrame > 0 && currentStress < 0.01f)
		{
			stressFrame = int.MaxValue;
			for (int j = 0; j < info.Nodes.Length; j++)
			{
				currentStresses[j] = 0f;
			}
		}
	}
}
