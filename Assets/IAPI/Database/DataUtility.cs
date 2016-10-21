using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace IAPI.Database {
	public class DataUtility {

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

		public static bool AddPart (string partName,string partType,string spriteName,MainDatabase mDB)
		{
			PartData newPart = new PartData ();
			newPart.Name = partName;
			newPart.Type = partType;
			newPart.Sprite = spriteName;
			foreach (PartType pType in mDB.Parts) 
			{
				if (pType.TypeName == partType)
				{
					pType.Parts.Add (newPart);
					return true;
				}
			}
			return false;
		}

		public static bool AddPartType (string typeName,MainDatabase mDB)
		{
			PartType pType = new PartType ();
			pType.TypeName = typeName;
			mDB.Parts.Add (pType);
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

		public static Vector3[] ConvertTransformData (TransformData tData)
		{
			Vector3[] VectorData = new Vector3[2];
			Vector3 newPos = new Vector3 (tData.PositionX,tData.PositionY,tData.PositionZ);
			Vector3 newRot = new Vector3 (tData.RotationX,tData.RotationY,tData.RotationZ);
			VectorData [0] = newPos;
			VectorData [1] = newRot;
			return VectorData;

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