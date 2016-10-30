using IAPI.Game;
using UnityEngine;
using System.Collections;

namespace IAPI.Database {
	public class GenerateUtility : MonoBehaviour {

		public static PartData GeneratePart (string partType,int rarity,int tier,int size,MainDatabase mDB)
		{
			PartData newPart = new PartData();
			newPart.Quantity = 10;

			Tier cTier = mDB.tiers[tier];
			Rarity cRarity = mDB.rarities[rarity];
			newPart.Tier = tier;
			newPart.Rarity = rarity;
			switch(size)
			{
			case 1:
				newPart.size = "Small";
				break;
			case 2:
				newPart.size = "Large";
				break;
			}

			tier += 1;
			rarity += 1;
			int minRange = cTier.minRange*rarity;
			int maxRange = cTier.maxRange*rarity;
			float minSpeed = cTier.minSpeed/rarity;
			float maxSpeed = cTier.maxSpeed/rarity;

			switch (partType)
			{
			case "Armors":
				newPart.Name = cRarity.RarityNames[Random.Range(0,3)] + " Armor Segment " + "MK" + tier;
				newPart.Sprite = DataUtility.GetRandomSpriteData("Armor",size,mDB).Name;
				newPart.MaxDurability = GameUtility.RoundToHundred(Random.Range(minRange,maxRange)*size);
				//print(size + " " + rarity + " " + tier + " " + newPart.MaxDurability);
				newPart.Worth = GameUtility.RoundToHundred((((tier)*10)+((rarity)*10))*(newPart.MaxDurability/2)/10);

				newPart.Type = "Armor";
				break;
			case "Reactors":
				if (newPart.size == "Large")
				{
					return null;
				}
				newPart.Name = cRarity.RarityNames[Random.Range(0,3)] + " Reactor " + "MK" + tier;
				newPart.Sprite = DataUtility.GetRandomSpriteData("Reactor",size,mDB).Name;
				newPart.MaxDurability = GameUtility.RoundToHundred((Random.Range(minRange,maxRange)*size)/2);
				newPart.MaxCapacity = GameUtility.RoundToHundred((Random.Range(minRange,maxRange)*size));
				newPart.RechargeRate = Random.Range(minSpeed,maxSpeed);
				int rdMod = Mathf.RoundToInt(5*newPart.RechargeRate);
				if (rdMod == 0)
				{
					rdMod = 1;
				}
				newPart.Worth = GameUtility.RoundToHundred((((tier)*10)+((rarity)*10))*((newPart.MaxDurability/2)+(newPart.MaxCapacity/4))/rdMod);
				newPart.Type = "Reactor";
				break;
			case "Shields":
				if (newPart.size == "Large")
				{
					return null;
				}
				newPart.Name = cRarity.RarityNames[Random.Range(0,3)] + " Shield " + "MK" + tier;
				newPart.Sprite = DataUtility.GetRandomSpriteData("Shield",size,mDB).Name;
				newPart.MaxDurability = GameUtility.RoundToHundred((Random.Range(minRange,maxRange)*size)/2);
				newPart.MaxCapacity = GameUtility.RoundToHundred((Random.Range(minRange,maxRange)*size));
				newPart.RechargeRate = Random.Range(minSpeed,maxSpeed);
				newPart.Drain = Random.Range((minRange/10),(maxRange/10))*(newPart.MaxCapacity/100)/2;
				int sdMod = Mathf.RoundToInt(5*newPart.RechargeRate);
				if (sdMod == 0)
				{
					sdMod = 1;
				}
				newPart.Worth = GameUtility.RoundToHundred(((((tier)*10)+((rarity)*10))*((newPart.MaxDurability/2)+(newPart.MaxCapacity/4))/sdMod)+(newPart.Drain*10));
				newPart.Type = "Shield";
				break;
			case "Cockpits":
				if (newPart.size == "Small")
				{
					return null;
				}
				newPart.Name = cRarity.RarityNames[Random.Range(0,3)] + " Cockpit " + "MK" + tier;
				newPart.Sprite = DataUtility.GetRandomSpriteData("Cockpit",size,mDB).Name;
				newPart.MaxDurability = GameUtility.RoundToHundred(Mathf.RoundToInt((Random.Range(minRange,maxRange)*size)/1.2f));
				newPart.Worth = GameUtility.RoundToHundred((((tier)*10)+((rarity)*10))*(newPart.MaxDurability/2)/8);
				newPart.Type = "Cockpit";
				break;
			case "Thrusters":
				if (newPart.size == "Small")
				{
					return null;
				}
				newPart.Name = cRarity.RarityNames[Random.Range(0,3)] + " Thruster " + "MK" + tier;
				newPart.Sprite = DataUtility.GetRandomSpriteData("Thruster",size,mDB).Name;
				newPart.MaxDurability = GameUtility.RoundToHundred((int)(Random.Range(minRange,maxRange)*size)/2);
				newPart.Thrust = (Random.Range((minRange/25),(maxRange/25))*size)/5;
				newPart.Torque = (Random.Range((minRange/25),(maxRange/25))*size)/10;
				int tdMod = Mathf.RoundToInt((2.5f*newPart.Thrust)+(5*newPart.Torque));
				if (tdMod == 0)
				{
					tdMod = 1;
				}
				newPart.Worth = GameUtility.RoundToHundred(((((tier)*10)+((rarity)*10))*((newPart.MaxDurability/2)+(newPart.MaxCapacity/4))/tdMod));
				newPart.Type = "Thruster";
				break;
			case "Weapons":
				if (newPart.size == "Small")
				{
					return null;
				}
				newPart.Name = cRarity.RarityNames[Random.Range(0,3)] + " Weapon " + "MK" + tier;
				newPart.Sprite = DataUtility.GetRandomSpriteData("Weapon",size,mDB).Name;
				newPart.MaxDurability = GameUtility.RoundToHundred((int)(Random.Range(minRange,maxRange)*size)/2);
				newPart.FireRate = Random.Range(minSpeed,maxSpeed);
				newPart.Damage = Random.Range((minRange/10),(maxRange/10))*size;
				newPart.Drain = (Random.Range((minRange/10),(maxRange/10))*newPart.Damage)/2;
				newPart.Projectile = mDB.Projectiles[Random.Range(0,mDB.Projectiles.Length)].name;
				int wdMod = Mathf.RoundToInt(5*newPart.FireRate);
				if (wdMod == 0)
				{
					wdMod = 1;
				}
				newPart.Worth = GameUtility.RoundToHundred(((((tier)*10)+((rarity)*10))*((newPart.MaxDurability/2)+(newPart.Damage))/wdMod));
				newPart.Type = "Weapon";
				break;
			}
			return newPart;
		}

	}
}

[System.Serializable]
public class Tier {

	public int minRange;
	public int maxRange;

	public float minSpeed;
	public float maxSpeed;

	public string[] PartNames;

}

[System.Serializable]
public class Rarity {

	public string[] RarityNames;
	public Color color;
}