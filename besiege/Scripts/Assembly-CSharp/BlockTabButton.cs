using UnityEngine;

public class BlockTabButton : ClickBehaviour
{
	public BlockTabController controller;

	public int myIndex;

	public Renderer iconRenderer;

	public Material onMaterial;

	public Material offMaterial;

	public Renderer bgRendy;

	protected AudioSource aSource;

	public virtual void Start()
	{
		aSource = GetComponent<AudioSource>();
	}

	public override void OnClicked()
	{
		Press();
	}

	public virtual void Press()
	{
		controller.OpenTab(myIndex);
		aSource.Play();
		LevelEditor instance = LevelEditor.Instance;
		if (instance != null)
		{
			instance.ResetWindow();
		}
		StatMaster.ChangeSelectedBlock(StatMaster.SelectedBlockId);
	}

	public virtual void SetVis(bool state)
	{
		bgRendy.enabled = state;
	}
}
