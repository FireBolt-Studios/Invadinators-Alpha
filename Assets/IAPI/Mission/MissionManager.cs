﻿using IAPI.Database;
using UnityEngine;
using System.Collections;

public class MissionManager : MonoBehaviour {

	public GameManager GManager;
	public string shipToLoad;

	void Awake ()
	{
		GManager = GameObject.FindObjectOfType<GameManager>();
		shipToLoad = "G2HAWK";
		LoadShip();
	}

	public void LoadShip ()
	{
		ShipData shipData = DataUtility.GetShip(shipToLoad,GManager.PManager.ActiveProfile);
		GameObject newShip = new GameObject(shipToLoad,typeof(Ship),typeof(ShipControl));
		Ship ship = newShip.GetComponent<Ship>();
		ship.GManager = GManager;
		foreach (PartData partData in shipData.Parts)
		{
			GameObject part = DataUtility.SpawnPart(partData,GManager.mDB,newShip.transform);
			part.GetComponent<Part>().Ship = ship.GetComponent<Ship>();
			if (partData.Type == "Weapon")
			{
				part.GetComponent<Part>().Prefab = DataUtility.GetProjectile(partData.Projectile,GManager.mDB);
			}
		}
		ship.GetComponent<Ship>().ShipData = shipData;
		ship.GetComponent<Ship>().Initialize();
		ship.GetComponent<ShipControl>().ship = ship;
	}
}
