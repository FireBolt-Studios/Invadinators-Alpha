using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace IAPI.Database {
	public class DataUtility {

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
	public string SpriteName;
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

