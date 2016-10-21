using IAPI.Database;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainDatabase : MonoBehaviour {

	public GameManager GManager;

	public List<PartType> Parts = new List<PartType>();
	public List<SpriteData> Sprites = new List<SpriteData>();

	public bool SpawnShip (string shipName)
	{
		ShipData sData = DataUtility.GetShip (shipName,GManager.PManager.ActiveProfile);
		if (sData != null)
		{
			GameObject newShip = new GameObject (shipName);
			foreach (PartData pData in sData.Parts)
			{
				GameObject newPart = new GameObject 
				(
					pData.Name,
					typeof(SpriteRenderer),
					typeof(BoxCollider2D),
					typeof(Part)
				);
				newPart.transform.parent = newShip.transform;
				Vector3[] VectorData = DataUtility.ConvertTransformData (pData.Transform);
				newPart.transform.localPosition = VectorData [0];
				newPart.transform.localEulerAngles = VectorData [1];
				SpriteData spriteData = DataUtility.GetSpriteData (pData.Sprite,this);
				newPart.GetComponent<SpriteRenderer> ().sprite = spriteData.Base;
				newPart.GetComponent<SpriteRenderer> ().sprite = spriteData.Detail;
				newPart.GetComponent<Part> ().PartData = DataUtility.DeepCopy (DataUtility.GetPartData(pData.Name,this));
			}
			IAPI.Game.GameUtility.CenterParts (newShip.transform);
			return true;
		}
		return false;
	}
}

