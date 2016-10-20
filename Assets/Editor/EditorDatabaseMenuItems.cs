using UnityEngine;
using UnityEditor;
using System.Collections;

public class EditorDatabaseMenuItems : MonoBehaviour {

	[MenuItem ("Invadinators/Database/Load Sprites")]
	static void LoadSprites () 
	{
		GameObject.FindObjectOfType<MainDatabase> ().Sprites.Clear ();
		IAPI.Database.DataUtility.LoadSprites (GameObject.FindObjectOfType<MainDatabase>());
	}
}
