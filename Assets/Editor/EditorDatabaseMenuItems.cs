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

	[MenuItem ("Invadinators/Database/Build Progression")]
	static void buildProgression () 
	{
		GameObject.FindObjectOfType<MainDatabase>().Progression = IAPI.Game.GameUtility.BuildProgression(GameObject.FindObjectOfType<MainDatabase>());
	}
}
