using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ProfileManager : MonoBehaviour {

	public GameManager GManager;

	public ProfileData ActiveProfile;

	public Text[] LevelInfo;
	public RectTransform xpBar;

	void Start ()
	{
		IAPI.Profile.ProfileUtility.GiveXp(500,this,GManager.mDB);
	}

	public void LevelUp ()
	{
		ActiveProfile.Level += 1;
		ActiveProfile.XP = 0;
		ActiveProfile.LP += GManager.mDB.Progression.levelInfo[ActiveProfile.Level].LearningGain;
		ActiveProfile.Credits = GManager.mDB.Progression.levelInfo[ActiveProfile.Level].CreditGain;
		SetLevelInfo();
	}

	void SetLevelInfo ()
	{
		LevelInfo[0].text = "LEVEL: "+ActiveProfile.Level.ToString();
		LevelInfo[1].text = "RANK: "+ActiveProfile.Rank;
		LevelInfo[2].text = "CREDIT: "+ActiveProfile.Credits.ToString();
		LevelInfo[3].text = "LP: "+ActiveProfile.LP.ToString();
		LevelInfo[4].text = ActiveProfile.XP.ToString();
		LevelInfo[5].text = ActiveProfile.Name;

		float percent = (ActiveProfile.XP/GManager.mDB.Progression.levelInfo[ActiveProfile.Level+1].ExpRequired)*100;
		xpBar.sizeDelta = new Vector2(percent*2.4f,10);
		print(percent);
	}

}

