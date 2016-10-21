using UnityEngine;
using System.Collections;

namespace Profile.ProfileUtility {
	public class ProfileUtility {

		public static bool CreateLocalProfile (string profileName, ProfileManager pManager)
		{
			ProfileData newProfile = new ProfileData ();
			newProfile.Name = profileName;
			newProfile.Level = 1;
			newProfile.Rank = "Private";
			newProfile.Credits = 1000;
			pManager.ActiveProfile = newProfile;
			return true;
		}
	}
}
