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
		//SetLevelInfo();
	}

	void Update ()
	{
		ActiveProfile.XP = 100;
		LevelInfo[0].text = "LEVEL: "+ActiveProfile.Level.ToString();
		LevelInfo[1].text = "RANK: "+ActiveProfile.Rank;
		LevelInfo[2].text = "CREDIT: "+ActiveProfile.Credits.ToString();
		LevelInfo[3].text = "LP: "+ActiveProfile.LP.ToString();
		LevelInfo[4].text = ActiveProfile.XP.ToString();
		LevelInfo[5].text = ActiveProfile.Name;

		float percent = (ActiveProfile.XP/GManager.mDB.Progression.levelInfo[ActiveProfile.Level+1].ExpRequired)*100;
		xpBar.anchorMax = new Vector2(100-percent,10);
		print(percent);
	}

}

