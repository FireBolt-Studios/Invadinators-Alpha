using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class UIStatModifier : MonoBehaviour {

	public ProfileManager PManager;

	public int StatIndex;

	public Text Points;
	public Text Modifier;

	public Button addButton;
	public Button minusButton;

	void Start ()
	{
		
	}

	public void SetValues (Learning learning)
	{
		Points.text = "POINTS: "+learning.StatModifiers[StatIndex].CurrentPoints.ToString();
		Modifier.text = "MOD: "+(learning.StatModifiers[StatIndex].TotalMod/100).ToString()+"%";
	}

	public void DecreaseValue ()
	{
		if (PManager.newLearning.StatModifiers[StatIndex].CurrentPoints > PManager.ActiveProfile.Learning.StatModifiers[StatIndex].CurrentPoints)
		{
			PManager.newLearning.StatModifiers[StatIndex].CurrentPoints -= 1;
			PManager.newLearning.StatModifiers[StatIndex].TotalMod -= PManager.newLearning.StatModifiers[StatIndex].ModPerPoint;
			PManager.pointsUsed -= 1;
			PManager.PointsUsed.text = "POINTS SPENT: "+PManager.pointsUsed.ToString();
			SetValues(PManager.newLearning);
			if (PManager.newLearning.StatModifiers[StatIndex].CurrentPoints <= PManager.ActiveProfile.Learning.StatModifiers[StatIndex].CurrentPoints)
			{
				minusButton.interactable = false;
			}
		}
	}

	public void IncreaseValue ()
	{
		if (PManager.ActiveProfile.LP > 0)
		{
			if (PManager.pointsUsed == 0)
			{
				PManager.newLearning = IAPI.Database.DataUtility.DeepCopy<Learning>(PManager.ActiveProfile.Learning);
			}

			PManager.newLearning.StatModifiers[StatIndex].CurrentPoints += 1;
			PManager.newLearning.StatModifiers[StatIndex].TotalMod += PManager.newLearning.StatModifiers[StatIndex].ModPerPoint;
			PManager.pointsUsed += 1;
			PManager.PointsUsed.text = "POINTS SPENT: "+PManager.pointsUsed.ToString();
			SetValues(PManager.newLearning);
			minusButton.interactable = true;
		}
	}

}
