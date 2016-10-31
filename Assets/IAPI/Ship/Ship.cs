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

	void CalculateModifiers ()
	{

		foreach (PartType pType in PartTypes)
		{
			foreach (PartData part in pType.Parts)
			{
				part.MaxDurability = Mathf.RoundToInt(part.MaxDurability * (1+(GManager.PManager.ActiveProfile.Learning.StatModifiers[11].TotalMod/100)));
				part.CurrentDurability = part.MaxDurability;
			}
		}

		foreach (PartData Shield in PartTypes[2].Parts)
		{
			Shield.MaxCapacity = Mathf.RoundToInt(Shield.MaxCapacity * (1+(GManager.PManager.ActiveProfile.Learning.StatModifiers[3].TotalMod/1000)));
			Shield.RechargeRate -= GManager.PManager.ActiveProfile.Learning.StatModifiers[4].TotalMod/10000;
			Shield.Drain = Mathf.RoundToInt(Shield.Drain * (1+(GManager.PManager.ActiveProfile.Learning.StatModifiers[5].TotalMod/1000)));
		}

		foreach (PartData Reactor in PartTypes[1].Parts)
		{
			Reactor.MaxCapacity = Mathf.RoundToInt(Reactor.MaxCapacity * (1+(GManager.PManager.ActiveProfile.Learning.StatModifiers[6].TotalMod/1000)));
			Reactor.RechargeRate -= GManager.PManager.ActiveProfile.Learning.StatModifiers[7].TotalMod/10000;
		}

		foreach (PartData Weapon in PartTypes[3].Parts)
		{
			Weapon.Damage = Mathf.RoundToInt(Weapon.MaxCapacity * (1+(GManager.PManager.ActiveProfile.Learning.StatModifiers[0].TotalMod/1000)));
			Weapon.FireRate -= GManager.PManager.ActiveProfile.Learning.StatModifiers[2].TotalMod/10000;
			Weapon.Drain = Mathf.RoundToInt(Weapon.Drain * (1+(GManager.PManager.ActiveProfile.Learning.StatModifiers[8].TotalMod/1000)));
		}

		foreach (PartData Thruster in PartTypes[4].Parts)
		{
			Thruster.Thrust = Mathf.RoundToInt(Thruster.Thrust * (1+(GManager.PManager.ActiveProfile.Learning.StatModifiers[9].TotalMod/1000)));
			Thruster.Torque = Mathf.RoundToInt(Thruster.Torque * (1+(GManager.PManager.ActiveProfile.Learning.StatModifiers[10].TotalMod/1000)));
			Thrust += Thruster.Thrust;
			Torque += Thruster.Torque;
		}
	}

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
		ReactorCharge = reactorCharge;
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

		CalculateModifiers();
		Active = true;
	}
}