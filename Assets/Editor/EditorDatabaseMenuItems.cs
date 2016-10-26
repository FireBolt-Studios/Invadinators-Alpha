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

	[MenuItem ("Invadinators/Database/Create Profile")]
	static void MakeProfile () 
	{
		GameObject.FindObjectOfType<ProfileManager>().ActiveProfile = IAPI.Database.DataUtility.CreateLocalProfile("Cypher",GameObject.FindObjectOfType<MainDatabase>());
	}

	[MenuItem ("Invadinators/Database/Debug Part Generatrion")]
	static void DebugPart () 
	{
		for (int i = 0; i < 4; i++)
		{
			IAPI.Database.GenerateUtility.GeneratePart("Armor",1,i,2,GameObject.FindObjectOfType<MainDatabase>());
		}
	}
}
