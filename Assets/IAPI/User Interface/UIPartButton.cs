using IAPI.Database;
using UnityEngine;
using System.Collections;

public class UIPartButton : MonoBehaviour {

	public ConstructionManager CManager;
	public PartData partData;

	public void SetProperties ()
	{
		SpriteData spriteData = DataUtility.GetSpriteData(partData,CManager.mDB);
		GetComponent<SpriteRenderer>().sprite = spriteData.Base;
		transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteData.Detail;
	}

	public void OnClick ()
	{
		CManager.SpawnPlacer(partData);
	}

}
