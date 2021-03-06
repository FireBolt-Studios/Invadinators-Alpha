﻿using IAPI.Database;
using UnityEngine;
using System.Collections;

public class MissionManager : MonoBehaviour {

	public GameManager GManager;
	public string shipToLoad;

	public MissionSpawner[] Spawners;

	public float spawnTimer;
	public float spawnTime;

	void Awake ()
	{
		GManager = GameObject.FindObjectOfType<GameManager>();
		shipToLoad = "G2HAWK";
		LoadShip();
		Spawners = GameObject.FindObjectsOfType<MissionSpawner>();
	}

	void Update ()
	{
		if (spawnTimer >= spawnTime)
		{
			int random = Random.Range(0,Spawners.Length);
			Spawners[random].Spawn();
			spawnTimer = 0;
		}
		else
		{
			spawnTimer += Time.deltaTime;
		}
	}

	public void LoadShip ()
	{
		ShipData shipData = DataUtility.GetShip(shipToLoad,GManager.PManager.ActiveProfile);
		GameObject newShip = new GameObject(shipToLoad,typeof(Ship),typeof(ShipControl));
		GameObject newShipDisplay = new GameObject("Ship Display",typeof(PartDisplay));
		Ship ship = newShip.GetComponent<Ship>();
		ship.GManager = GManager;
		foreach (PartData partData in shipData.Parts)
		{
			GameObject part = DataUtility.SpawnPart(partData,GManager.mDB,newShip.transform,false);
			GameObject partDisplay = DataUtility.SpawnPart(partData,GManager.mDB,newShipDisplay.transform,true);
			Destroy(partDisplay.transform.GetChild(0).gameObject);
			partDisplay.GetComponent<PartDisplay>().part = part.GetComponent<Part>();
			part.GetComponent<Part>().Ship = ship.GetComponent<Ship>();
			if (partData.Type == "Weapon")
			{
				part.GetComponent<Part>().Prefab = DataUtility.GetProjectile(partData.Projectile,GManager.mDB);
			}
		}

		newShipDisplay.transform.position = new Vector3(-25,-10,0);
		newShipDisplay.transform.localScale = new Vector3(2,2,2);
		ship.GetComponent<Ship>().ShipData = shipData;
		ship.GetComponent<Ship>().Initialize();
		ship.GetComponent<ShipControl>().ship = ship;
	}
}
