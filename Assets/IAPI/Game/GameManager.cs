using UnityEngine;
using IAPI.Database;
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
		PManager.ActiveProfile.Cargo[0].Parts.Add(GenerateUtility.GenerateCockpitPart(1,1,2,mDB));
		PManager.ActiveProfile.Cargo[1].Parts.Add(GenerateUtility.GenerateReactorPart(1,1,1,mDB));
		PManager.ActiveProfile.Cargo[2].Parts.Add(GenerateUtility.GenerateShieldPart(1,1,1,mDB));
		PManager.ActiveProfile.Cargo[3].Parts.Add(GenerateUtility.GenerateWeaponPart(1,1,1,mDB));
		PManager.ActiveProfile.Cargo[4].Parts.Add(GenerateUtility.GenerateThrusterPart(1,1,2,mDB));

		for (int i = 0; i < 20; i++)
		{
			PManager.ActiveProfile.Cargo[5].Parts.Add(GenerateUtility.GenerateArmorPart(1,1,2,mDB));
		}

		for (int i = 0; i < 20; i++)
		{
			PManager.ActiveProfile.Cargo[5].Parts.Add(GenerateUtility.GenerateArmorPart(1,1,1,mDB));
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
