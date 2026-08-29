using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptables/EditorAssetsMeta")]
public class EditorAssetsMeta : ScriptableObject
{
	public List<Prop> props;

	public List<SkyboxMeta> skyboxes;

	[Space(20f)]
	public List<TagMeta> specialTags;

	public List<TagMeta> themeTags;

	public List<TagMeta> categoryTags;

	public List<TagMeta> buildingTags;

	public List<PropID> hiddenProps;

	private HashSet<PropTag> _usedPropTags;

	private HashSet<PropID> _hiddenPropIDs;

	private Dictionary<PropID, Prop> _propsByID;

	public HashSet<PropTag> usedPropTags
	{
		get
		{
			if (_usedPropTags != null)
			{
				return _usedPropTags;
			}
			_usedPropTags = new HashSet<PropTag>();
			foreach (Prop item in getPropsAll())
			{
				_usedPropTags.UnionWith(item.tags);
			}
			return _usedPropTags;
		}
	}

	public HashSet<PropID> hiddenPropIDs
	{
		get
		{
			if (_hiddenPropIDs != null)
			{
				return _hiddenPropIDs;
			}
			_hiddenPropIDs = new HashSet<PropID>();
			foreach (PropID hiddenProp in hiddenProps)
			{
				_hiddenPropIDs.Add(hiddenProp);
			}
			return _hiddenPropIDs;
		}
	}

	private Dictionary<PropID, Prop> propsByID
	{
		get
		{
			if (_propsByID != null && _propsByID.Count == props.Count)
			{
				return _propsByID;
			}
			_propsByID = new Dictionary<PropID, Prop>();
			foreach (Prop prop in props)
			{
				_propsByID[prop.ID] = prop;
			}
			return _propsByID;
		}
	}

	public Prop getPropByID(PropID propID)
	{
		return propsByID.GetValueOrDefault(propID);
	}

	public List<Prop> getPropsAll()
	{
		return getProps((Prop _) => true);
	}

	public List<Prop> getPropsWithTag(PropTag tag)
	{
		return getProps((Prop prop) => prop.tags.Contains(tag));
	}

	public List<Prop> getPropsWithAnyOfTags(HashSet<PropTag> tags)
	{
		return getProps((Prop prop) => prop.tags.Exists(tags.Contains));
	}

	public List<Prop> getProps(Predicate<Prop> filter)
	{
		List<Prop> list = new List<Prop>();
		foreach (Prop prop in props)
		{
			if (prop.roots.Count <= 0 && !hiddenPropIDs.Contains(prop.ID) && filter(prop))
			{
				list.Add(prop);
			}
		}
		return list;
	}
}
