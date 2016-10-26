using UnityEngine;
using System.Collections;

namespace IAPI.Database {
	public class GenerateUtility : MonoBehaviour {

		public static PartData GeneratePart (string partType,int rarity,int tier,int size,MainDatabase mDB)
		{
			PartData newPart = new PartData();

			Tier cTier = mDB.tiers[tier];
			Rarity cRarity = mDB.rarities[rarity];
			newPart.Tier = tier;
			newPart.Rarity = rarity;

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
				newPart.MaxDurability = Random.Range(minRange,maxRange)*size;
				newPart.Worth = (((tier)*10)+((rarity)*10))*(newPart.MaxDurability/2)/10;
				break;
			case "Reactors":
				newPart.Name = cRarity.RarityNames[Random.Range(0,3)] + " Reactor " + "MK" + tier;
				newPart.Sprite = DataUtility.GetRandomSpriteData("Reactor",size,mDB).Name;
				newPart.MaxDurability = (Random.Range(minRange,maxRange)*size)/2;
				newPart.MaxCapacity = (Random.Range(minRange,maxRange)*size);
				newPart.RechargeRate = Random.Range(minSpeed,maxSpeed);
				int rdMod = Mathf.RoundToInt(5*newPart.RechargeRate);
				if (rdMod == 0)
				{
					rdMod = 1;
				}
				newPart.Worth = (((tier)*10)+((rarity)*10))*((newPart.MaxDurability/2)+(newPart.MaxCapacity/4))/rdMod;
				break;
			case "Shields":
				newPart.Name = cRarity.RarityNames[Random.Range(0,3)] + " Shield " + "MK" + tier;
				newPart.Sprite = DataUtility.GetRandomSpriteData("Shield",size,mDB).Name;
				newPart.MaxDurability = (Random.Range(minRange,maxRange)*size)/2;
				newPart.MaxCapacity = (Random.Range(minRange,maxRange)*size);
				newPart.RechargeRate = Random.Range(minSpeed,maxSpeed);
				newPart.Drain = Random.Range((minRange/10),(maxRange/10))*(newPart.MaxCapacity/100)/2;
				int sdMod = Mathf.RoundToInt(5*newPart.RechargeRate);
				if (sdMod == 0)
				{
					sdMod = 1;
				}
				newPart.Worth = ((((tier)*10)+((rarity)*10))*((newPart.MaxDurability/2)+(newPart.MaxCapacity/4))/sdMod)+(newPart.Drain*10);
				break;
			case "Cockpits":
				newPart.Name = cRarity.RarityNames[Random.Range(0,3)] + " Cockpit " + "MK" + tier;
				newPart.Sprite = DataUtility.GetRandomSpriteData("Cockpit",size,mDB).Name;
				newPart.MaxDurability = Mathf.RoundToInt((Random.Range(minRange,maxRange)*size)/1.2f);
				newPart.Worth = (((tier)*10)+((rarity)*10))*(newPart.MaxDurability/2)/8;
				break;
			case "Thrusters":
				newPart.Name = cRarity.RarityNames[Random.Range(0,3)] + " Thruster " + "MK" + tier;
				newPart.Sprite = DataUtility.GetRandomSpriteData("Thruster",size,mDB).Name;
				newPart.MaxDurability = (int)(Random.Range(minRange,maxRange)*size)/2;
				newPart.Thrust = Random.Range((minRange/10),(maxRange/10))*size;
				newPart.Torque = Random.Range((minRange/10),(maxRange/10))*size;
				int tdMod = Mathf.RoundToInt((2.5f*newPart.Thrust)+(5*newPart.Torque));
				if (tdMod == 0)
				{
					tdMod = 1;
				}
				newPart.Worth = ((((tier)*10)+((rarity)*10))*((newPart.MaxDurability/2)+(newPart.MaxCapacity/4))/tdMod);
				break;
			case "Weapons":
				newPart.Name = cRarity.RarityNames[Random.Range(0,3)] + " Weapon " + "MK" + tier;
				newPart.Sprite = DataUtility.GetRandomSpriteData("Weapon",size,mDB).Name;
				newPart.MaxDurability = (int)(Random.Range(minRange,maxRange)*size)/2;
				newPart.FireRate = Random.Range(minSpeed,maxSpeed);
				newPart.Damage = Random.Range((minRange/10),(maxRange/10))*size;
				newPart.Drain = (Random.Range((minRange/10),(maxRange/10))*newPart.Damage)/2;
				int wdMod = Mathf.RoundToInt(5*newPart.FireRate);
				if (wdMod == 0)
				{
					wdMod = 1;
				}
				newPart.Worth = ((((tier)*10)+((rarity)*10))*((newPart.MaxDurability/2)+(newPart.Damage))/wdMod);
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