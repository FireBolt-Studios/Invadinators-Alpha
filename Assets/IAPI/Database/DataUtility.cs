using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace IAPI.Database {
	public class DataUtility {

		public static GameObject GetProjectile (string projectileName,MainDatabase mDB)
		{
			foreach (GameObject projectile in mDB.Projectiles)
			{
				if (projectileName == projectile.name)
				{
					return projectile;
				}
			}
			return null;
		}

		public static SpriteData GetRandomSpriteData (string type,int size,MainDatabase mDB)
		{
			string textSize = "";
			Debug.Log(size);
			switch(size)
			{
			case 1:
				textSize = "Small";
				break;
			case 2:
				textSize = "Large";
				break;
			}

			List<SpriteData> sData = new List<SpriteData>();

			foreach (SpriteData spData in mDB.Sprites)
			{
				if (spData.Name.Contains(type))
				{
					if (spData.Name.Contains(textSize))
					{
						sData.Add(spData);
					}
				}
			}

			if (sData.Count == 0)
			{
				foreach (SpriteData spData in mDB.Sprites)
				{
					if (!spData.Name.Contains("Reactor") 
						&& !spData.Name.Contains("Shield") 
						&& !spData.Name.Contains("Weapon") 
						&& !spData.Name.Contains("Thruster") 
						&& !spData.Name.Contains("Cockpit"))
					{
						if (spData.Name.Contains(textSize))
						{
							sData.Add(spData);
						}
					}
				}
			}

			SpriteData randomSData = sData[UnityEngine.Random.Range(0,sData.Count-1)];
			return randomSData;
		}

		public static T DeepCopy<T>(T other)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(ms, other);
				ms.Position = 0;
				return (T)formatter.Deserialize(ms);
			}
		}

		public static T Load<T>(string filename) where T : class
		{
			if (File.Exists(filename))
			{
				using (Stream stream = File.OpenRead(filename))
				{
					BinaryFormatter formatter = new BinaryFormatter();
					return formatter.Deserialize(stream) as T;
				}
			}
			return null;
		}

		public static void Save<T>(string filename, T data) where T : class
		{
			using (Stream stream = File.Open(filename, FileMode.OpenOrCreate))
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, data);
			}
		}

		public static T LoadXML<T>(string filename) where T : class
		{
			if (File.Exists(filename))
			{
				using (Stream stream = File.OpenRead(filename))
				{
					XmlSerializer serial = new XmlSerializer(typeof(T));
					return serial.Deserialize(stream) as T;
				}
			}

			return null;
		}

		public static void SaveXML<T>(string filename, T data) where T : class
		{
			using (Stream stream = File.Open(filename, FileMode.OpenOrCreate))
			{
				XmlSerializer serial = new XmlSerializer(typeof (T));
				serial.Serialize(stream, data);
			}
		}

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

		public static PartType GetPartType (string typeName,ProfileData profileData)
		{
			foreach (PartType pType in profileData.Cargo)
			{
				if (pType.TypeName == typeName)
				{
					return pType;
				}
			}
			return null;
		}

		public static PartData GetPartData (string partName,MainDatabase mDB)
		{
			foreach (PartType pType in mDB.GManager.PManager.ActiveProfile.Cargo)
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

		public static ShipData GetShip (string shipName,ProfileData profileData)
		{
			foreach (ShipData ship in profileData.Ships) 
			{
				if (ship.Name == shipName) 
				{
					return ship;
				}
			}
			return null;
		}

		public static TransformData ConvertTransform (Vector3 position,Vector3 euler)
		{
			TransformData tData = new TransformData();
			tData.PositionX = position.x;
			tData.PositionY = position.y;
			tData.PositionZ = position.z;
			tData.RotationX = euler.x;
			tData.RotationY = euler.y;
			tData.RotationZ = euler.z;
			return tData;
		}

		public static Vector3[] ConvertTransformData (TransformData tData)
		{
			Vector3[] VectorData = new Vector3[2];
			Vector3 newPos = new Vector3 (tData.PositionX,tData.PositionY,tData.PositionZ);
			Vector3 newRot = new Vector3 (tData.RotationX,tData.RotationY,tData.RotationZ);
			VectorData [0] = newPos;
			VectorData [1] = newRot;
			return VectorData;
		}

		public static ColorData ConvertColor (Color color)
		{
			ColorData newCData = new ColorData();
			newCData.Red = color.r;
			newCData.Green = color.g;
			newCData.Blue = color.b;
			newCData.Alpha = 1;
			return newCData;
		}

		public static Color ConvertColorData (ColorData cData)
		{
			Color newColor = new Color(cData.Red,cData.Green,cData.Blue,cData.Alpha);
			return newColor;
		}

		public static bool CheckDirectories ()
		{
			if (!Directory.Exists(Application.persistentDataPath+"/Profiles/"))
			{
				Directory.CreateDirectory(Application.persistentDataPath+"/Profiles/");
				return true;
			}
			return true;
		}

		public static bool CheckForProfile ()
		{
			if (!File.Exists(Application.persistentDataPath+"/Profiles/Profile.pro"))
			{
				return false;
			}
			return true;
		}

		public static ProfileData CreateLocalProfile (string profileName,MainDatabase mDB)
		{
			ProfileData newProfileData = new ProfileData();
			newProfileData.Name = profileName;
			newProfileData.Credits = 1000;
			newProfileData.Level = 1;
			newProfileData.Rank = "Private";
			newProfileData.Learning = DeepCopy<Learning>(mDB.Learning);
			foreach (string partType in mDB.PartTypes)
			{
				PartType pType = new PartType();
				pType.TypeName = partType+"s";
				newProfileData.Cargo.Add(pType);
			}
			return newProfileData;
		}

		public static GameObject SpawnPart (PartData partData,MainDatabase mDB,Transform ship)
		{
			// Create Gameobjects
			GameObject newPart = new GameObject(partData.Name,typeof(SpriteRenderer),typeof(BoxCollider2D),typeof(Part));
			GameObject newDetail = new GameObject("Detail",typeof(SpriteRenderer));

			// Setup Transforms
			newDetail.transform.parent = newPart.transform;
			newPart.transform.parent = ship;
			Vector3[] vectorData = ConvertTransformData(partData.Transform);
			newPart.transform.localPosition = vectorData[0];
			newPart.transform.localEulerAngles = vectorData[1];
			if (partData.size == "Small")
			{
				newPart.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
			}
			else
			{
				newPart.transform.localScale = new Vector3(1,1,1);
			}

			//Setup Sprite Renderers
			SpriteRenderer Base = newPart.GetComponent<SpriteRenderer>();
			SpriteRenderer Detail = newDetail.GetComponent<SpriteRenderer>();
			Base.sprite = GetSpriteData(partData.Sprite,mDB).Base;
			Base.color = ConvertColorData(partData.Colors[0]);
			Base.flipX = partData.FlipX;
			Detail.sprite = GetSpriteData(partData.Sprite,mDB).Detail;
			Detail.color = ConvertColorData(partData.Colors[1]);
			Detail.flipX = partData.FlipX;
			Detail.sortingOrder = 1;

			//Setup Part Data
			newPart.GetComponent<Part>().PartData = partData;
			return newPart;
		}
	}
}

[System.Serializable]
public class ProfileData {

	public string Name;
	public int Level;
	public string Rank;
	public int Credits;
	public int LP;
	public int XP;
	public Learning Learning;
	public List<ShipData> Ships = new List<ShipData>();
	public List<PartType> Cargo = new List<PartType>();
	public List<PartData> partTest = new List<PartData>();

}

[System.Serializable]
public class Learning {

	public StatModifier[] StatModifiers;

}

[System.Serializable]
public class StatModifier {

	public string StatName;
	public int CurrentPoints;
	public float ModPerPoint;
	public float TotalMod;

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
	public int CurrentDurability;
	public int MaxCapacity;
	public int Charge;
	public float RechargeRate;
	public int Thrust;
	public int Torque;
	public int Damage;
	public float FireRate;
	public int Drain;

	public int Tier;
	public int Rarity;
	public string size;

	public string Projectile;
	public string Sprite;
	public ColorData[] Colors = new ColorData[2];
	public TransformData Transform;
	public bool FlipX;
	public int Quantity;

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

[System.Serializable]
public class Progression {

	public LevelInfo[] levelInfo;

	public float TotalXP;
	public float TotalLearning;
	public int TotalCredit;

}

[System.Serializable]
public class LevelInfo {

	public string Level;
	public int ExpRequired;
	public int LearningGain;
	public int CreditGain;

}