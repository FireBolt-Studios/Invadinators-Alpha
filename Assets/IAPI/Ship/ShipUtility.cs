﻿using UnityEngine;
using System.Collections;

namespace IAPI {
	public class ShipUtility : MonoBehaviour {

		public static void FireProjectile (Transform origin,GameObject projectile,int damage)
		{
			ShipProjectile newProjectile = Instantiate(projectile,origin.position,origin.rotation) as ShipProjectile;
			newProjectile.damage = damage;
		}

		public static PartData GetReactorCharge (Ship ship)
		{
			foreach (PartData partData in ship.PartTypes[1].Parts)
			{
				if (partData.Charge > 0)
				{
					return partData;
				}
			}
			return null;
		}

		public static PartData GetShieldCharge (Ship ship)
		{
			foreach (PartData partData in ship.PartTypes[2].Parts)
			{
				if (partData.Charge > 0)
				{
					return partData;
				}
			}
			return null;
		}
	}
}
