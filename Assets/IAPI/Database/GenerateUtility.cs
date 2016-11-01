using IAPI.Game;
using UnityEngine;
using System.Collections;

namespace IAPI.Database {
	public class GenerateUtility : MonoBehaviour {

		public static PartData GenerateBasicPartData (int tier,int rarity,int size)
		{
			PartData NewPart = new PartData();

			NewPart.Tier = tier;
			NewPart.Rarity = rarity;
			NewPart.Quantity = 10;

			switch(size)
			{
			case 1:
				NewPart.size = "Small";
				break;
			case 2:
				NewPart.size = "Large";
				break;
			}
			return NewPart;
		}

		public static PartData GenerateThrusterPart (int tier,int rarity,int size,MainDatabase mDB)
		{
			PartData NewPart = GenerateBasicPartData(tier,rarity,size);
			NewPart.Type = "Thruster";
			Tier currentTier = mDB.tiers[tier-1];
			//Rarity currentRarity = mDB.rarities[rarity-1];

			float dMod = Random.Range(0f,1f);
			float tMod = Random.Range(dMod,1f)-dMod;
			float toMod = 1-(dMod+tMod);

			NewPart.MaxDurability = Mathf.RoundToInt(Random.Range(currentTier.minRange*rarity,currentTier.maxRange*rarity)*(0.8f+dMod)*size);
			NewPart.Thrust = Mathf.RoundToInt((Random.Range(currentTier.minRange*rarity,currentTier.maxRange*rarity)/(100*(tier*tier)))*(0.8f+tMod)*size);
			NewPart.Torque = Mathf.RoundToInt((Random.Range(currentTier.minRange*rarity,currentTier.maxRange*rarity)/(100*(tier*tier)))*(0.8f+toMod)*size);

			string adjective = "";
			float[] floats = new float[3] {dMod,tMod,toMod};
			float number = GameUtility.GetHighestFloat(floats);
			if (number == dMod)
			{
				adjective = mDB.statAdjectives[0].adjectives[Random.Range(0,3)];
			}
			if (number == tMod)
			{
				adjective = mDB.statAdjectives[2].adjectives[Random.Range(0,3)];
			}
			if (number == toMod)
			{
				adjective = mDB.statAdjectives[5].adjectives[Random.Range(0,2)];
			}

			NewPart.Name = adjective + " Thruster " + "MK" + tier;
			NewPart.Worth = Mathf.RoundToInt(NewPart.MaxDurability + (NewPart.Thrust*100) + (NewPart.Torque*100)*rarity);

			string spriteName = DataUtility.GetRandomSpriteData("Thruster",size,mDB).Name;
			if (!spriteName.Contains("Thruster"))
			{
				return null;
			}

			NewPart.Sprite = spriteName;

			print(NewPart.Thrust);
			print(NewPart.Torque);
			print(NewPart.Name + " Worth: " + NewPart.Worth + " Sprite: " + NewPart.Sprite);

			return NewPart;
		}

		public static PartData GenerateReactorPart (int tier,int rarity,int size,MainDatabase mDB)
		{
			PartData NewPart = GenerateBasicPartData(tier,rarity,size);
			NewPart.Type = "Reactor";
			Tier currentTier = mDB.tiers[tier-1];
			//Rarity currentRarity = mDB.rarities[rarity-1];

			float dMod = Random.Range(0f,1f);
			float cMod = Random.Range(dMod,1f)-dMod;
			float rMod = 1-(dMod+cMod);

			NewPart.MaxDurability = Mathf.RoundToInt(Random.Range(currentTier.minRange*rarity,currentTier.maxRange*rarity)*(0.8f+dMod)*size);
			NewPart.MaxCapacity = Mathf.RoundToInt(Random.Range(currentTier.minRange*rarity,currentTier.maxRange*rarity)*(0.8f+cMod)*size);
			NewPart.RechargeRate = Random.Range(currentTier.minSpeed,currentTier.maxSpeed)/(0.8f+rMod);

			string adjective = "";
			float[] floats = new float[3] {dMod,cMod,rMod};
			float number = GameUtility.GetHighestFloat(floats);
			if (number == dMod)
			{
				adjective = mDB.statAdjectives[0].adjectives[Random.Range(0,3)];
			}
			if (number == cMod)
			{
				adjective = mDB.statAdjectives[1].adjectives[Random.Range(0,3)];
			}
			if (number == rMod)
			{
				adjective = mDB.statAdjectives[2].adjectives[Random.Range(0,3)];
			}

			NewPart.Name = adjective + " Reactor " + "MK" + tier;
			NewPart.Worth = Mathf.RoundToInt(NewPart.MaxDurability + NewPart.MaxCapacity - ((NewPart.RechargeRate*currentTier.minRange)*rarity));

			string spriteName = DataUtility.GetRandomSpriteData("Reactor",size,mDB).Name;
			if (!spriteName.Contains("Reactor"))
			{
				return null;
			}

			NewPart.Sprite = spriteName;

			print(NewPart.Name + " Worth: " + NewPart.Worth + " Sprite: " + NewPart.Sprite);

			return NewPart;
		}

		public static PartData GenerateShieldPart (int tier,int rarity,int size,MainDatabase mDB)
		{
			PartData NewPart = GenerateBasicPartData(tier,rarity,size);
			NewPart.Type = "Shield";
			Tier currentTier = mDB.tiers[tier-1];
			//Rarity currentRarity = mDB.rarities[rarity-1];

			float dMod = Random.Range(0f,1f);
			float cMod = Random.Range(dMod,1f);
			float rMod = Random.Range(cMod,1f);
			cMod -= dMod;
			rMod -= (dMod+cMod);
			float drMod = 1-(dMod+cMod+rMod);

			NewPart.MaxDurability = Mathf.RoundToInt(Random.Range(currentTier.minRange*rarity,currentTier.maxRange*rarity)*(0.8f+dMod)*size);
			NewPart.MaxCapacity = Mathf.RoundToInt(Random.Range(currentTier.minRange*rarity,currentTier.maxRange*rarity)*(0.8f+cMod)*size);
			NewPart.RechargeRate = Random.Range(currentTier.minSpeed,currentTier.maxSpeed)/(0.8f+rMod);
			NewPart.Drain = Mathf.RoundToInt(((NewPart.MaxCapacity/(10*(tier*tier)))*rarity)*(0.8f+drMod));

			print(NewPart.MaxCapacity);
			print(NewPart.Drain);

			string adjective = "";
			float[] floats = new float[4] {dMod,cMod,rMod,drMod};
			float number = GameUtility.GetHighestFloat(floats);
			if (number == dMod)
			{
				adjective = mDB.statAdjectives[0].adjectives[Random.Range(0,3)];
			}
			if (number == cMod)
			{
				adjective = mDB.statAdjectives[1].adjectives[Random.Range(0,3)];
			}
			if (number == rMod)
			{
				adjective = mDB.statAdjectives[2].adjectives[Random.Range(0,3)];
			}
			if (number == drMod)
			{
				adjective = mDB.statAdjectives[3].adjectives[Random.Range(0,3)];
			}

			NewPart.Name = adjective + " Shield " + "MK" + tier;
			NewPart.Worth = Mathf.RoundToInt(NewPart.MaxDurability + NewPart.MaxCapacity - ((NewPart.RechargeRate*currentTier.minRange)*rarity));

			string spriteName = DataUtility.GetRandomSpriteData("Shield",size,mDB).Name;
			if (!spriteName.Contains("Shield"))
			{
				return null;
			}

			NewPart.Sprite = spriteName;

			print(NewPart.Name + " Worth: " + NewPart.Worth + " Sprite: " + NewPart.Sprite);

			return NewPart;
		}

		public static PartData GenerateWeaponPart (int tier,int rarity,int size,MainDatabase mDB)
		{
			PartData NewPart = GenerateBasicPartData(tier,rarity,size);
			NewPart.Type = "Weapon";
			Tier currentTier = mDB.tiers[tier-1];
			//Rarity currentRarity = mDB.rarities[rarity-1];

			float dMod = Random.Range(0f,1f);
			float daMod = Random.Range(dMod,1f);
			float rMod = Random.Range(daMod,1f);
			daMod -= dMod;
			rMod -= (dMod+daMod);
			float drMod = 1-(dMod+daMod+rMod);

			NewPart.MaxDurability = Mathf.RoundToInt(Random.Range(currentTier.minRange*rarity,currentTier.maxRange*rarity)*(0.8f+dMod)*size);
			NewPart.Damage = Mathf.RoundToInt((Random.Range((currentTier.minRange/10)*rarity,(currentTier.maxRange/10)*rarity)*(0.8f+daMod)*size));
			NewPart.FireRate = Random.Range(currentTier.minSpeed,currentTier.maxSpeed)/(0.8f+rMod);
			NewPart.Drain = Mathf.RoundToInt(((NewPart.MaxCapacity/(10*(tier*tier)))*rarity)*(0.8f+drMod));

			print(NewPart.Damage);

			string adjective = "";
			float[] floats = new float[4] {dMod,daMod,rMod,drMod};
			float number = GameUtility.GetHighestFloat(floats);
			if (number == dMod)
			{
				adjective = mDB.statAdjectives[0].adjectives[Random.Range(0,3)];
			}
			if (number == daMod)
			{
				adjective = mDB.statAdjectives[4].adjectives[Random.Range(0,3)];
			}
			if (number == rMod)
			{
				adjective = mDB.statAdjectives[2].adjectives[Random.Range(0,3)];
			}
			if (number == drMod)
			{
				adjective = mDB.statAdjectives[3].adjectives[Random.Range(0,3)];
			}

			NewPart.Name = adjective + " Weapon " + "MK" + tier;
			NewPart.Worth = Mathf.RoundToInt(NewPart.MaxDurability + (NewPart.Damage*10) - ((NewPart.FireRate*currentTier.minRange)*rarity));

			string spriteName = DataUtility.GetRandomSpriteData("Weapon",size,mDB).Name;
			if (!spriteName.Contains("Weapon"))
			{
				return null;
			}

			NewPart.Sprite = spriteName;

			print(NewPart.Name + " Worth: " + NewPart.Worth + " Sprite: " + NewPart.Sprite);

			return NewPart;
		}

		public static PartData GenerateCockpitPart (int tier,int rarity,int size,MainDatabase mDB)
		{
			PartData NewPart = GenerateBasicPartData(tier,rarity,size);
			NewPart.Type = "Cockpit";
			Tier currentTier = mDB.tiers[tier-1];
			//Rarity currentRarity = mDB.rarities[rarity-1];

			float dMod = Random.Range(0f,1f);
			NewPart.MaxDurability = Mathf.RoundToInt(Random.Range(currentTier.minRange*rarity,currentTier.maxRange*rarity)*(0.8f+dMod)*size);

			if (dMod >= 0.5f)
			{
				NewPart.Name = mDB.statAdjectives[0].adjectives[Random.Range(0,3)] + " Cockpit " + "MK" + tier;
			}
			else
			{
				NewPart.Name = "Cockpit " + "MK" + tier;
			}
			NewPart.Worth = Mathf.RoundToInt(NewPart.MaxDurability)*rarity;

			string spriteName = DataUtility.GetRandomSpriteData("Cockpit",size,mDB).Name;
			if (!spriteName.Contains("Cockpit"))
			{
				return null;
			}

			NewPart.Sprite = spriteName;

			print(NewPart.Name + " Worth: " + NewPart.Worth + " Sprite: " + NewPart.Sprite);

			return NewPart;
		}

		public static PartData GenerateArmorPart (int tier,int rarity,int size,MainDatabase mDB)
		{
			PartData NewPart = GenerateBasicPartData(tier,rarity,size);
			NewPart.Type = "Armor";
			Tier currentTier = mDB.tiers[tier-1];
			//Rarity currentRarity = mDB.rarities[rarity-1];

			float dMod = Random.Range(0f,1f);
			NewPart.MaxDurability = Mathf.RoundToInt(Random.Range(currentTier.minRange*rarity,currentTier.maxRange*rarity)*(0.8f+dMod)*size);

			if (dMod >= 0.5f)
			{
				NewPart.Name = mDB.statAdjectives[0].adjectives[Random.Range(0,3)] + " Armor " + "MK" + tier;
			}
			else
			{
				NewPart.Name = "Armor " + "MK" + tier;
			}
			NewPart.Worth = Mathf.RoundToInt(NewPart.MaxDurability)*rarity;

			string spriteName = DataUtility.GetRandomSpriteData("Armor",size,mDB).Name;
	
			NewPart.Sprite = spriteName;

			print(NewPart.Name + " Worth: " + NewPart.Worth + " Sprite: " + NewPart.Sprite);

			return NewPart;
		}

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

[System.Serializable]
public class StatAdjectives {

	public string typeName;
	public string[] adjectives;
}
