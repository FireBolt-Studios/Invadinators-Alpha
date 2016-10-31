using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public MainDatabase mDB;

	public ProfileManager PManager;

	void Awake ()
	{
		DontDestroyOnLoad (this);
		Initialization();
	}
		

	public void FillCargo ()
	{
		foreach (PartType pType in PManager.ActiveProfile.Cargo)
		{
			for (int t = 0; t <= 1; t++)
			{
				for (int r = 0; r <= 2; r++)
				{
					for (int s = 1; s <= 2; s++)
					{
						PartData newPartData = IAPI.Database.GenerateUtility.GeneratePart(pType.TypeName,r,t,s,mDB);
						if (newPartData != null)
						{
							pType.Parts.Add(newPartData);
						}
					}
				}
			}
		}
	}

	public void LoadTest ()
	{
		SceneManager.LoadScene("Test");
	}

	void Initialization ()
	{
		if (IAPI.Database.DataUtility.CheckDirectories())
		{
			if (IAPI.Database.DataUtility.CheckForProfile())
			{
				print("Profile Found");
				PManager.ActiveProfile = IAPI.Database.DataUtility.Load<ProfileData>(Application.persistentDataPath+"/Profiles/Profile.pro");
			}
			else
			{
				PManager.ActiveProfile = IAPI.Database.DataUtility.CreateLocalProfile("Cypher",mDB);
				FillCargo();
				IAPI.Database.DataUtility.Save(Application.persistentDataPath+"/Profiles/Profile.pro",PManager.ActiveProfile);
			}
		}
	}

}
