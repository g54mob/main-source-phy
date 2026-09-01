using System;
using System.Collections.Generic;
using Landfall.TABS;
using UnityEngine;

[Serializable]
public class SongInstance
{
	public List<MapAsset> MapsAssociated;

	public string songRef;

	public AudioClip clip;

	public int positionInSong;

	public SoundEffectInstance soundEffectInstance;
}
