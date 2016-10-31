using IAPI.Database;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainDatabase : MonoBehaviour {

	public GameManager GManager;

	public List<string> PartTypes = new List<string>();
	public List<SpriteData> Sprites = new List<SpriteData>();
	public Tier[] tiers;
	public Rarity[] rarities;
	public GameObject[] Projectiles;
	public Progression Progression;
	public Learning Learning;

	public GameObject[] Asteroids;
}

