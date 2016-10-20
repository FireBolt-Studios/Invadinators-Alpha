using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace IAPI.Database {
	public class DataUtility {

		public static bool LoadSprites (MainDatabase mDB)
		{
			Sprite[] sprites = Resources.LoadAll<Sprite>("");

			for (int i = 0; i < sprites.Length; i+=2)
			{
				SpriteData newSData = new SpriteData ();
				newSData.Name = sprites [i].name;
				newSData.Base = sprites [i];
				newSData.Detail = sprites [i + 1];
				mDB.Sprites.Add (newSData);
			}

			return true;
		}

		public static PartData GetPartData (string partName,MainDatabase mDB)
		{
			foreach (PartType pType in mDB.Parts)
			{
				foreach (PartData pData in pType.Parts)
				{
					if (pData.Name == partName)
					{
						return pData;
					}
				}
			}
			return null;
		}

		public static SpriteData GetSpriteData (string spriteName,MainDatabase mDB)
		{
			foreach (SpriteData sData in mDB.Sprites)
			{
				if (sData.Name == spriteName)
				{
					return sData;
				}
			}
			return null;
		}

	}
}

[System.Serializable]
public class ProfileData {

	public string Name;
	public int Level;
	public string Rank;
	public int Credits;
	public List<ShipData> Ships = new List<ShipData>();
	public List<PartData> Cargo = new List<PartData>();

}

[System.Serializable]
public class ShipData {

	public string Name;
	public List<PartData> Parts = new List<PartData>();

}

[System.Serializable]
public class PartData {

	public string Name;
	public string Type;
	public int Worth;
	public int Weight;
	public int MaxDurability;
	public int MaxCapacity;
	public float RechargeRate;
	public int Thrust;
	public int Torque;
	public int Damage;
	public float FireRate;
	public int Drain;

	public string Projectile;
	public string Sprite;
	public ColorData Colors;
	public TransformData Transform;

}

[System.Serializable]
public class TransformData {

	public float PositionX;
	public float PositionY;
	public float PositionZ;

	public float RotationX;
	public float RotationY;
	public float RotationZ;

}

[System.Serializable]
public class ColorData {

	public float Red;
	public float Green;
	public float Blue;
	public float Alpha;

}


[System.Serializable]
public class SpriteData {

	public string Name;
	public Sprite Base;
	public Sprite Detail;

}

[System.Serializable]
public class PartType {

	public string TypeName;
	public List<PartData> Parts = new List<PartData>();

}