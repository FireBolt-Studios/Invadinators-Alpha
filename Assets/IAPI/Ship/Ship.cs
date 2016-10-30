using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {

	public bool Active;

	public GameManager GManager;

	public ShipData ShipData;
	public List<PartType> PartTypes = new List<PartType>();

	public List<Part> Weapons = new List<Part>();

	public int ShieldCharge;
	public int ReactorCharge;

	public int Thrust;
	public int Torque;

	void ShieldCalculation ()
	{
		int shieldCharge = 0;
		foreach (PartData Shield in PartTypes[2].Parts)
		{
			shieldCharge += Shield.Charge;
		}
		ShieldCharge = shieldCharge;
	}

	void ReactorCalculation ()
	{
		int reactorCharge = 0;
		foreach (PartData Reactor in PartTypes[1].Parts)
		{
			reactorCharge += Reactor.Charge;
		}
		ReactorCharge= reactorCharge;
	}

	void Update ()
	{
		if (Active)
		{
			ShieldCalculation();
			ReactorCalculation();
		}
	}

	public void Initialize ()
	{
		print("Init Started");
		PartTypes.Clear();

		Thrust = 0;
		Torque = 0;

		foreach (string typeName in GManager.mDB.PartTypes)
		{
			PartType newPartType = new PartType();
			newPartType.TypeName = typeName;
			PartTypes.Add(newPartType);
		}

		foreach (PartData partData in ShipData.Parts)
		{
			if (partData.Type == "Cockpit")
			{
				PartTypes[0].Parts.Add(partData);
			}
			if (partData.Type == "Reactor")
			{
				PartTypes[1].Parts.Add(partData);
			}
			if (partData.Type == "Shield")
			{
				PartTypes[2].Parts.Add(partData);
			}
			if (partData.Type == "Weapon")
			{
				PartTypes[3].Parts.Add(partData);
			}
			if (partData.Type == "Thruster")
			{
				PartTypes[4].Parts.Add(partData);
				Thrust += partData.Thrust;
				Torque += partData.Torque;
			}
			if (partData.Type == "Armor")
			{
				PartTypes[5].Parts.Add(partData);
			}
			Part[] parts = GetComponentsInChildren<Part>();
			foreach (Part part in parts)
			{
				part.Active = true;
				if (part.PartData.Type == "Weapon")
				{
					GameObject pRef = new GameObject("Ref");
					pRef.transform.parent = part.transform;
					if (part.PartData.FlipX)
					{
						pRef.transform.localPosition = new Vector3(0.388f,-1f);
					}
					else
					{					
						pRef.transform.localPosition = new Vector3(-0.388f,-1f);
					}

					pRef.transform.localRotation = transform.rotation;
					Weapons.Add(part);
				}
			}
		}
		IAPI.Game.GameUtility.CenterParts(transform);
		Active = true;
	}
}