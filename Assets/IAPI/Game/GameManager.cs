using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public MainDatabase mDB;

	public ProfileManager PManager;

	void Awake ()
	{
		DontDestroyOnLoad (this);
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
