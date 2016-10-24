using IAPI.Database;
using UnityEngine;
using System.Collections;

public class ConstructionManager : MonoBehaviour {

	public MainDatabase mDB;

	public Color BaseColor;
	public Color DetailColor;

	public void SpawnPlacer (PartData partData)
	{
		GameObject newPlacer = new GameObject("Placer",typeof(SpriteRenderer),typeof(BoxCollider2D));
		GameObject newDetail = new GameObject("Detail",typeof(SpriteRenderer));
		newDetail.transform.parent = newPlacer.transform;

		SpriteData spriteData = DataUtility.GetSpriteData(partData.Sprite,mDB);
		newPlacer.GetComponent<SpriteRenderer>().sprite = spriteData.Base;
		newDetail.GetComponent<SpriteRenderer>().sprite = spriteData.Detail;

		newPlacer.GetComponent<SpriteRenderer>().color = DataUtility.ConvertColorData(partData.Colors[0]);
		newDetail.GetComponent<SpriteRenderer>().color = DataUtility.ConvertColorData(partData.Colors[1]);

		newPlacer.GetComponent<ConstructionPlacer>().partData = partData;
	}

}
