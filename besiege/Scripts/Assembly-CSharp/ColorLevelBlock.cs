using System.Collections.Generic;
using Localisation;
using UnityEngine;

public class ColorLevelBlock : GenericEntity, ILocalisationAware
{
	public bool defaultToMaterialColour;

	public bool correctColour = true;

	public bool showColour = true;

	public bool disableTexture;

	public bool allowNoTexture;

	public Texture defaultTexture;

	public Texture NoGridTexture;

	public Collider[] colliders;

	public Renderer blockRenderer;

	protected MColourSlider colourSlider;

	private MToggle textureToggle;

	[Range(0f, 1f)]
	public float brightnessModifier = 1f;

	protected Vector3 def;

	private MSlider dragModifier;

	private MSlider angularDragModifier;

	private MSlider bouncinessModifier;

	private MSlider frictionModifier;

	private MMenu frictionMode;

	private MMenu bounceMode;

	private Rigidbody body;

	private PhysicMaterial physMaterial;

	private MaterialPropertyBlock prop;

	public override void Init()
	{
		if (!isInitialized)
		{
			prop = new MaterialPropertyBlock();
			colourSlider = AddColourSlider(2503, "colour", new Color(1f, 1f, 1f), false);
			colourSlider.DisplayInMapper = showColour;
			ResetToDefaultColor();
			physMaterial = new PhysicMaterial();
			for (int i = 0; i < colliders.Length; i++)
			{
				colliders[i].material = physMaterial;
			}
			if (allowNoTexture)
			{
				textureToggle = AddToggle(3395, "Disable Texture", disableTexture);
				textureToggle.Toggled += OnTextureToggle;
				OnTextureToggle(disableTexture);
			}
			body = GetComponent<Rigidbody>();
			if (!noRigidbody)
			{
				dragModifier = AddSliderUnclamped(2929, "drag", body.drag, 0f, 100f, string.Empty);
				dragModifier.ValueChanged += OnDragChanged;
				angularDragModifier = AddSliderUnclamped(2930, "angular-drag", body.angularDrag, 0f, 100f, string.Empty);
				angularDragModifier.ValueChanged += OnAngularDragChanged;
			}
			bouncinessModifier = AddSlider(3191, "bounciness", physMaterial.bounciness, 0f, 1f, string.Empty);
			bouncinessModifier.ValueChanged += OnBouncinessChanged;
			bounceMode = AddMenu("bounce-mode", 0, new List<string>
			{
				LocalisationManager.GetTranslation(3193),
				LocalisationManager.GetTranslation(3194),
				LocalisationManager.GetTranslation(3195),
				LocalisationManager.GetTranslation(3196)
			});
			bounceMode.ValueChanged += OnBounceModeChanged;
			frictionModifier = AddSlider(3192, "friction", physMaterial.dynamicFriction, 0f, 1f, string.Empty);
			frictionModifier.ValueChanged += OnFrictionChanged;
			frictionMode = AddMenu("friction-mode", 0, new List<string>
			{
				LocalisationManager.GetTranslation(3197),
				LocalisationManager.GetTranslation(3198),
				LocalisationManager.GetTranslation(3199),
				LocalisationManager.GetTranslation(3200)
			});
			frictionMode.ValueChanged += OnFrictionModeChanged;
			physMaterial.bounceCombine = PhysicMaterialCombine.Average;
			physMaterial.frictionCombine = PhysicMaterialCombine.Average;
			maxMassScale = 100f;
			base.Init();
			FinalizeInit();
		}
	}

	protected virtual void ResetToDefaultColor()
	{
		if (defaultToMaterialColour)
		{
			colourSlider.Value = blockRenderer.material.GetColor("_Color");
			def = ColorToVector3(colourSlider.Value);
		}
	}

	protected virtual void FinalizeInit()
	{
		SetBlockColor(colourSlider.Value);
		colourSlider.ValueChanged += ColourChanged;
		correctColour = true;
	}

	protected virtual void ColourChanged(Color value)
	{
		SetBlockColor(value);
	}

	private void OnFrictionModeChanged(int value)
	{
		physMaterial.frictionCombine = (PhysicMaterialCombine)value;
	}

	private void OnBounceModeChanged(int value)
	{
		physMaterial.bounceCombine = (PhysicMaterialCombine)value;
	}

	private void OnFrictionChanged(float value)
	{
		PhysicMaterial physicMaterial = physMaterial;
		float num = Mathf.Clamp01(value);
		physMaterial.staticFriction = num;
		physicMaterial.dynamicFriction = num;
	}

	private void OnBouncinessChanged(float value)
	{
		physMaterial.bounciness = Mathf.Clamp01(value);
	}

	private void OnDragChanged(float newValue)
	{
		if (body != null)
		{
			body.drag = ((!(newValue < 0f)) ? newValue : 0f);
		}
	}

	private void OnAngularDragChanged(float newValue)
	{
		if (body != null)
		{
			body.angularDrag = ((!(newValue < 0f)) ? newValue : 0f);
		}
	}

	protected override void OnPhysicsToggled(bool toggle)
	{
		if (!startingSim)
		{
			base.OnPhysicsToggled(toggle);
			dragModifier.DisplayInMapper = toggle;
			angularDragModifier.DisplayInMapper = toggle;
		}
	}

	public override void SetupDefault()
	{
		base.SetupDefault();
		if (!noRigidbody && physicsToggle != null)
		{
			physicsToggle.IsActive = false;
			physicsToggle.ApplyValue();
		}
	}

	protected virtual void SetBlockColor(Color value)
	{
		if (correctColour && (!defaultToMaterialColour || !(Vector3.Angle(ColorToVector3(value), def) < 0.5f)))
		{
			Color color = value + Mathf.Clamp(Mathf.Abs(value.r - value.g * 2f) * value.g * 0.5f + value.b, 0f, 1f) * Color.white * 0.3f;
			color = (color * 3f + Color.white * 2f) / 5f - Color.white * 0.2f;
			color *= GetBrightness();
			color.a = 1f;
			SetCurrentColor(color);
		}
		else
		{
			SetCurrentColor(value);
		}
	}

	protected virtual float GetBrightness()
	{
		return brightnessModifier;
	}

	protected virtual void SetCurrentColor(Color value)
	{
		prop.SetColor("_Color", value);
		blockRenderer.SetPropertyBlock(prop);
	}

	protected Vector3 ColorToVector3(Color color)
	{
		return new Vector3(color.r, color.g, color.b);
	}

	private void OnTextureToggle(bool newValue)
	{
		prop.SetTexture("_MainTex", (!newValue) ? defaultTexture : NoGridTexture);
		blockRenderer.SetPropertyBlock(prop);
		disableTexture = newValue;
	}

	public override void OnLocalisationChange()
	{
		base.OnLocalisationChange();
		if (bounceMode != null)
		{
			bounceMode.Items = new List<string>
			{
				LocalisationManager.GetTranslation(3193),
				LocalisationManager.GetTranslation(3194),
				LocalisationManager.GetTranslation(3195),
				LocalisationManager.GetTranslation(3196)
			};
		}
		if (frictionMode != null)
		{
			frictionMode.Items = new List<string>
			{
				LocalisationManager.GetTranslation(3197),
				LocalisationManager.GetTranslation(3198),
				LocalisationManager.GetTranslation(3199),
				LocalisationManager.GetTranslation(3200)
			};
		}
	}
}
