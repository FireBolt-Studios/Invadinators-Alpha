using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public MainDatabase mDB;

	public ProfileManager PManager;

	void Awake ()
	{
		DontDestroyOnLoad (this);
		//FillCargo();
	}

	public void FillCargo ()
	{
		foreach (PartType pType in PManager.ActiveProfile.Cargo)
		{
			for (int t = 0; t <= 4; t++)
			{
				for (int r = 0; r <= 4; r++)
				{
					for (int s = 0; s <= 1; s++)
					{
						pType.Parts.Add(IAPI.Database.GenerateUtility.GeneratePart(pType.TypeName,r,t,s,mDB));
					}
				}
			}
		}
	}

	void Initialization ()
	{
		if (IAPI.Database.DataUtility.CheckDirectories())
		{
			if (IAPI.Database.DataUtility.CheckForProfile())
			{
				PManager.ActiveProfile = IAPI.Database.DataUtility.Load<ProfileData>(Application.persistentDataPath+"/Invadinators/Profile/Profile.bin");
			}
			else
			{
				IAPI.Database.DataUtility.Save(Application.persistentDataPath+"/Invadinators/Profile/Profile.bin",PManager.ActiveProfile);
			}
		}
	}

}
