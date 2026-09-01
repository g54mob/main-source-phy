using System;
using UnityEngine;
using UnityEngine.Audio;

[AddComponentMenu("Blocks/FragmentVisualController")]
public class FragmentVisualController : BlockVisualController
{
	protected static ParticleSystem.EmitParams emitter = default(ParticleSystem.EmitParams);

	public Action onVisualBreak;

	public bool breakIntoPieces = true;

	public FilterRendererPair shortVis;

	public FilterRendererPair[] brokenVis;

	public Transform[] disableOnBreak;

	protected Renderer mainRen;

	private bool hasAudio;

	public AudioSource audio;

	public AudioClip[] breakSfx;

	protected AudioMixerGroup mixer;

	protected AudioMixerGroup underwaterMixer;

	protected bool broken;

	public Vector3 sjOffset = Vector3.forward;

	public static void EmitJointBreakMarker(Vector3 pos)
	{
		if (StatMaster.stressCoded)
		{
			emitter.applyShapeToPosition = true;
			emitter.position = pos;
			emitter.startSize = 0.25f;
			GlobalParticles.EmitParticle(20, emitter, 1);
			emitter.startSize = 0.08f;
			GlobalParticles.EmitParticle(18, emitter, 10);
		}
	}

	public virtual void StartController()
	{
		ConfigurableJoint[] components = base.gameObject.GetComponents<ConfigurableJoint>();
		if (brokenVis.Length > 0)
		{
			SetBrokenParent(components, 0);
			if (brokenVis.Length > 2)
			{
				SetBrokenParent(components, 2);
			}
		}
		if (Block.Prefab.hasBVC)
		{
			if (renderers.Length > 0)
			{
				mainRen = renderers[0];
			}
			else
			{
				Debug.LogWarning("No renderers found on '" + Machine.GetObjectPath(base.gameObject) + "'!");
			}
		}
		hasAudio = audio != null;
		if (hasAudio)
		{
			mixer = audio.outputAudioMixerGroup;
			underwaterMixer = ReferenceMaster.GetWaterMixerFrom(mixer);
		}
	}

	protected void SetBrokenParent(ConfigurableJoint[] js, int index)
	{
		int num = ((index <= 1) ? index : (index - 1));
		if (js.Length <= num)
		{
			return;
		}
		ConfigurableJoint configurableJoint = js[num];
		if (!(configurableJoint != null))
		{
			return;
		}
		Rigidbody connectedBody = configurableJoint.connectedBody;
		if (!(connectedBody != null))
		{
			return;
		}
		FilterRendererPair filterRendererPair = brokenVis[index];
		if (filterRendererPair.renderer != null)
		{
			filterRendererPair.renderer.transform.parent = connectedBody.transform;
			BlockBehaviour componentInParent = connectedBody.GetComponentInParent<BlockBehaviour>();
			if (componentInParent != null)
			{
				componentInParent.visAddedToMe.Add(filterRendererPair.renderer);
				brokenVis[index].active = false;
			}
		}
		else
		{
			Debug.LogWarning("pair " + index + " renderer is null (" + Machine.GetObjectPath(base.gameObject) + ")!");
		}
	}

	protected override void SetShortMesh(BlockSkinLoader.SkinPack.Skin selectedSkin)
	{
		Renderer shortRenderer;
		if (selectedSkin.shortSkin != null && GetShortRenderer(out shortRenderer))
		{
			MeshFilter component = shortRenderer.GetComponent<MeshFilter>();
			component.sharedMesh = selectedSkin.shortSkin.mesh;
		}
	}

	public override void SetBrokenFragmentMaterial(Material mat)
	{
		if (selectedSkin == null || !selectedSkin.isDefault)
		{
			return;
		}
		for (int i = 0; i < brokenVis.Length; i++)
		{
			MeshRenderer renderer = brokenVis[i].renderer;
			if (renderer != null)
			{
				if (StatMaster.clusterCoded)
				{
					renderer.material = GetClusterMaterial();
				}
				else if (StatMaster.aeroCoded || StatMaster.stressCoded)
				{
					renderer.material = GetIntensityMaterial();
				}
				else
				{
					brokenVis[i].renderer.sharedMaterial = mat;
				}
			}
		}
	}

	public override bool GetShortRenderer(out Renderer shortRenderer)
	{
		shortRenderer = shortVis.renderer;
		return shortVis.renderer != null;
	}

	public virtual void OnJointBreak(float breakForce)
	{
		if (StatMaster.stressCoded)
		{
			BlockJoint j;
			int jointLikyBroken = Block.GetJointLikyBroken(out j);
			Vector3 pos = ((jointLikyBroken < 0) ? base.transform.position : base.transform.TransformPoint(j.anchor));
			EmitJointBreakMarker(pos);
		}
		if (!breakIntoPieces)
		{
			return;
		}
		broken = true;
		if (selectedSkin != null && !selectedSkin.isDefault)
		{
			return;
		}
		InvokeOnVisualBreak();
		for (int i = 0; i < disableOnBreak.Length; i++)
		{
			disableOnBreak[i].gameObject.SetActive(false);
		}
		for (int k = 0; k < brokenVis.Length; k++)
		{
			MeshRenderer renderer = brokenVis[k].renderer;
			if (renderer != null)
			{
				GameObject gameObject = renderer.gameObject;
				if (gameObject != null && !gameObject.activeSelf)
				{
					gameObject.SetActive(true);
				}
			}
		}
		CopyMaterialProperties();
		if (hasAudio)
		{
			PlaySound(breakSfx, 1.1f, 1.3f);
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
		if (Block.GetSubmergedPctMV > 0.9f)
		{
			audio.outputAudioMixerGroup = underwaterMixer;
		}
		else
		{
			audio.outputAudioMixerGroup = mixer;
		}
		audio.pitch = UnityEngine.Random.Range(pitchMin, pitchMax);
		audio.volume = UnityEngine.Random.Range(0.05f, 0.15f) * (1f - base.BurnPct * 0.9f);
		audio.clip = s;
		audio.Play();
	}

	public override void UpdateAeroDragDisplay()
	{
		base.UpdateAeroDragDisplay();
		if (brokenVis.Length > 1 && brokenVis[1].renderer.gameObject != null)
		{
			brokenVis[1].renderer.SetPropertyBlock(props);
		}
	}

	public override void UpdateStressDisplay()
	{
		base.UpdateStressDisplay();
		if (brokenVis.Length > 1 && brokenVis[1].renderer.gameObject != null)
		{
			brokenVis[1].renderer.SetPropertyBlock(props);
		}
	}

	protected void InvokeOnVisualBreak()
	{
		if (onVisualBreak != null)
		{
			onVisualBreak();
		}
	}

	protected override void SetMaterialProperties(MaterialPropertyBlock prop)
	{
		base.SetMaterialProperties(prop);
		CopyMaterialProperties(prop);
	}

	public virtual void CopyMaterialProperties()
	{
		CopyMaterialProperties(props);
	}

	public virtual void CopyMaterialProperties(MaterialPropertyBlock prop)
	{
		bool flag = StatMaster.isMP && (!StatMaster.isHosting || StatMaster.isLocalSim);
		for (int i = 0; i < brokenVis.Length; i++)
		{
			if (brokenVis[i].active)
			{
				MeshRenderer renderer = brokenVis[i].renderer;
				if (flag && renderer == null)
				{
					Debug.LogError("brokenVis ren is null on " + Machine.GetObjectPath(base.gameObject) + "!", base.gameObject);
				}
				else if (renderer.gameObject != null)
				{
					renderer.SetPropertyBlock(prop);
				}
			}
		}
	}

	protected override void OnDestroy()
	{
		if (!quitting)
		{
			base.OnDestroy();
			if (brokenVis != null && brokenVis.Length > 0 && brokenVis[0].renderer != null && brokenVis[0].renderer.gameObject != null)
			{
				UnityEngine.Object.Destroy(brokenVis[0].renderer.gameObject);
			}
		}
	}
}
