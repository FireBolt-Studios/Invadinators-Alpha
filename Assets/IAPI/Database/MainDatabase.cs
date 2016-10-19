using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainDatabase : MonoBehaviour {

	public PartsDatabase Parts;

}

[System.Serializable]
public class PartsDatabase {

	public List<PartData> Cockpits = new List<PartData>();
	public List<PartData> Reactors = new List<PartData>();
	public List<PartData> Shields = new List<PartData>();
	public List<PartData> Weapons = new List<PartData>();
	public List<PartData> Thrusters = new List<PartData>();
	public List<PartData> Armor = new List<PartData>();

}
