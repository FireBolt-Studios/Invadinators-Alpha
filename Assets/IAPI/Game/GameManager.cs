using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public MainDatabase mDB;

	public ProfileManager PManager;

	void Awake ()
	{
		DontDestroyOnLoad (this);
		FillCargo();
	}

	public void FillCargo ()
	{
		for (int i = 0; i < PManager.ActiveProfile.Cargo.Count; i++)
		{
			foreach (PartData pData in mDB.Parts[i].Parts)
			{
				PartData copiedPartData = IAPI.Database.DataUtility.DeepCopy(pData);
				copiedPartData.Quantity = 10;
				PManager.ActiveProfile.Cargo[i].Parts.Add(copiedPartData);
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
