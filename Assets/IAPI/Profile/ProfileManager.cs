using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ProfileManager : MonoBehaviour {

	public GameManager GManager;

	public ProfileData ActiveProfile;

	public Text[] LevelInfo;
	public RectTransform xpBar;

	public Learning newLearning;
	public int pointsUsed;
	public Text PointsUsed;
	public UIStatModifier[] StatModifiers;

	void Start ()
	{
		IAPI.Profile.ProfileUtility.GiveXp(500,this,GManager.mDB);
		foreach(UIStatModifier mod in StatModifiers)
		{
			mod.SetValues(ActiveProfile.Learning);
			mod.minusButton.interactable = false;
		}
		PointsUsed.text = "POINTS SPENT: "+pointsUsed.ToString();
		SetLevelInfo();
	}


	public void CommitValues ()
	{
		if (pointsUsed > 0)
		{
			if (pointsUsed <= ActiveProfile.LP)
			{
				ActiveProfile.Learning = newLearning;
				ActiveProfile.LP -= pointsUsed;
				pointsUsed = 0;
				newLearning = null;
				PointsUsed.text = "POINTS SPENT: "+pointsUsed.ToString();
				foreach(UIStatModifier mod in StatModifiers)
				{
					mod.SetValues(ActiveProfile.Learning);
					mod.minusButton.interactable = false;
				}
				IAPI.Profile.ProfileUtility.SaveProfile(ActiveProfile);
				SetLevelInfo();
			}
		}
	}

	public void LevelUp ()
	{
		ActiveProfile.Level += 1;
		ActiveProfile.XP = 0;
		ActiveProfile.LP += GManager.mDB.Progression.levelInfo[ActiveProfile.Level].LearningGain;
		ActiveProfile.Credits = GManager.mDB.Progression.levelInfo[ActiveProfile.Level].CreditGain;
		SetLevelInfo();
	}

	public void SetLevelInfo ()
	{
		LevelInfo[0].text = "LEVEL: "+ActiveProfile.Level.ToString();
		LevelInfo[1].text = "RANK: "+ActiveProfile.Rank;
		LevelInfo[2].text = "CREDIT: "+ActiveProfile.Credits.ToString();
		LevelInfo[3].text = "LP: "+ActiveProfile.LP.ToString();
		LevelInfo[4].text = ActiveProfile.XP.ToString();
		LevelInfo[5].text = ActiveProfile.Name;

		float percent = (ActiveProfile.XP/GManager.mDB.Progression.levelInfo[ActiveProfile.Level+1].ExpRequired)*100;
		xpBar.sizeDelta = new Vector2(percent*2.4f,10);
	}

}

