using UnityEngine;

public class EditFieldHandler : MonoBehaviour
{
	public static EditFieldHandler Instance;

	public virtual void OnCloseMapper()
	{
	}

	public virtual void OnCloseOverviewMapper()
	{
	}

	public virtual void OnEditField(SaveableDataHolder dataHolder, MapperType mapperType)
	{
	}

	public virtual void OnReset()
	{
	}

	public virtual void OnPaste(BlockBehaviour block, CopyMode mode)
	{
	}

	public virtual void OnPaste(GenericEntity entity, CopyMode mode)
	{
	}
}
