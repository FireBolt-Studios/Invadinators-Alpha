using UnityEngine;
using System.Collections;

namespace IAPI.Profile {
	public class ProfileUtility {

		public static void GiveXp (int amount,ProfileManager pManager,MainDatabase mDB)
		{
			pManager.ActiveProfile.XP += amount;
			if (pManager.ActiveProfile.XP >= mDB.Progression.levelInfo[pManager.ActiveProfile.Level+1].ExpRequired)
			{
				pManager.LevelUp();
			}
		}

		public static void SaveProfile (ProfileData profile)
		{
			IAPI.Database.DataUtility.Save(Application.persistentDataPath+"/Profiles/Profile.pro",profile);
		}
	}
}
