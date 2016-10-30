﻿using UnityEngine;
using System.Collections;

public class Part : MonoBehaviour {

	public bool Active;

	public Ship Ship;
	public PartData PartData;

	public float timer;
	public GameObject Prefab;
	public bool canAction;

	void Update ()
	{
		if (Active)
		{
			if (PartData.Type == "Shield" || PartData.Type == "Reactor")
			{
				if (timer >= PartData.RechargeRate)
				{
					DoAction();
					timer = 0;
				}
				else
				{
					timer += Time.deltaTime;
				}
			}
			else if (PartData.Type == "Weapon")
			{
				if (canAction == false)
				{
					if (timer >= PartData.FireRate)
					{
						canAction = true;
						timer = 0;
					}
					else
					{
						timer += Time.deltaTime;
					}
				}
				else
				{
					if (Input.GetMouseButton(0))
					{
						DoAction();
						canAction = false;
					}
				}
			}
			else
			{

			}
		}
	}
		
	void DoAction ()
	{
		if (PartData.Type == "Reactor")
		{
			if (PartData.Charge < PartData.MaxCapacity)
			{
				PartData.Charge += PartData.MaxCapacity/20;
				if (PartData.Charge > PartData.MaxCapacity)
				{
					PartData.Charge = PartData.MaxCapacity;
				}
			}
		}
		if (PartData.Type == "Shield")
		{
			if (PartData.Charge < PartData.MaxCapacity)
			{
				PartData hasCharge = IAPI.ShipUtility.GetReactorCharge(Ship);
				if (hasCharge != null)
				{
					PartData.Charge += PartData.MaxCapacity/20;
					hasCharge.Charge -= PartData.Drain;
				}
				if (PartData.Charge > PartData.MaxCapacity)
				{
					PartData.Charge = PartData.MaxCapacity;
				}
			}
		}
		if (PartData.Type == "Weapon")
		{
			IAPI.ShipUtility.FireProjectile(transform.GetChild(1),Prefab);
		}
	}

	void DamagePart (int damage)
	{
		if (Ship.ShieldCharge <= 0)
		{
			PartData.CurrentDurability -= damage;
		}
		else
		{
			IAPI.ShipUtility.GetShieldCharge(Ship).Charge -= damage;
		}

		if (PartData.CurrentDurability <= 0)
		{
			DestroyPart();
		}
	}

	void DestroyPart ()
	{
		Ship.ShipData.Parts.Remove(PartData);
		foreach (PartType pType in Ship.PartTypes)
		{
			if (pType.TypeName == PartData.Type+"s")
			{
				if (PartData.Type == "Thruster")
				{
					Ship.Thrust -= PartData.Thrust;
					Ship.Torque -= PartData.Torque;
				}
				pType.Parts.Remove(PartData);
			}
		}

		Destroy(gameObject);
	}

}
