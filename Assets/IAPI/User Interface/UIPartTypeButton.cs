using UnityEngine;
using System.Collections;

public class UIPartTypeButton : MonoBehaviour {

	public ConstructionManager CManager;
	public string partType;

	public void OnClick ()
	{
		CManager.DisplayParts(IAPI.Database.DataUtility.GetPartType(partType,CManager.GManager.PManager.ActiveProfile));
	}

}
