using UnityEngine;
using System.Collections;

namespace IAPI.Game {
	public class GameUtility {

		public static Progression BuildProgression (MainDatabase mDB)
		{
			Progression progression = new Progression();
			progression.levelInfo = new LevelInfo[201];

			for (int lvl = 0; lvl < 201; lvl++)
			{
				LevelInfo newInfo = new LevelInfo();
				newInfo.Level = lvl.ToString();
				progression.levelInfo[lvl] = newInfo;

				float nx = (lvl*((lvl*2)+((lvl*2)+15)))*10;
				progression.levelInfo[lvl].ExpRequired = nx;
				if (lvl > 0 && lvl <= 30)
				{
					progression.levelInfo[lvl].LearningGain = 500;
				}
				if (lvl > 30 && lvl <= 70)
				{
					progression.levelInfo[lvl].LearningGain = 400;
				}
				if (lvl > 70 && lvl <= 120)
				{
					progression.levelInfo[lvl].LearningGain = 200;
				}
				if (lvl > 120 && lvl <= 201)
				{
					progression.levelInfo[lvl].LearningGain = 62.5f;
				}
			}

			foreach (LevelInfo lvl in progression.levelInfo)
			{
				progression.TotalLearning += lvl.LearningGain;
			}
			progression.TotalXP += progression.levelInfo[200].ExpRequired;

			return progression;
		}

		public static int RoundToHundred (int number)
		{
			float divide = number/50;
			int roundedNumber = Mathf.RoundToInt(divide*50);
			return roundedNumber;
		}

		public static bool CenterParts (Transform Ship)
		{
			Vector3 center = new Vector3(0,0,0);
			float count = 0;

			Part[] Parts = Ship.GetComponentsInChildren<Part>();

			foreach (Part part in Parts)
			{
				center += part.transform.position;
				count++;
			}
			Vector3 theCenter = center / count;

			GameObject pivot = new GameObject("Pivot");
			pivot.transform.position = theCenter;
			foreach (Part part in Parts)
			{
				part.transform.parent = pivot.transform;
			}
			pivot.transform.parent = Ship;
			pivot.transform.localPosition = Vector3.zero;
			return true;
		}

	}
}
