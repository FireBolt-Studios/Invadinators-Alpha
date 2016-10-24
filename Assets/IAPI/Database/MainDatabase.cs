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
				GameObject newPart = IAPI.Database.DataUtility.MakePart(pData,this);
				GameObject newDetail = new GameObject("Detail",typeof(SpriteRenderer));
				newDetail.transform.parent = newPart.transform;

				SpriteData spriteData = DataUtility.GetSpriteData(pData.Sprite,GManager.mDB);
				newPart.GetComponent<SpriteRenderer>().sprite = spriteData.Base;
				newDetail.GetComponent<SpriteRenderer>().sprite = spriteData.Detail;

				newPart.transform.parent = newShip.transform;
			}
			IAPI.Game.GameUtility.CenterParts (newShip.transform);
			return true;
		}
		return false;
	}
}

