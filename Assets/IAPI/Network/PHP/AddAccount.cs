using IAPI.Database;
using UnityEngine;
using System.Collections;

public class AddAccount : MonoBehaviour {

	private string secretKey = "mySecretKey"; // Edit this value and make sure it's the same as the one stored on the server
	public string addScoreURL = "http://invadinators.netai.net/AddAccount.php?"; //be sure to add a ? to your url
	//public string highscoreURL = "http://localhost/unity_test/display.php";

	public void AddNewAccount ()
	{
		StartCoroutine(PostScores("Cypher","00j00j","jamiewhite121988@gmail.com"));
	}

	// remember to use StartCoroutine when calling this function!
	IEnumerator PostScores(string name, string password, string email)
	{
		print("Attempting to add to table");
		//This connects to a server side php script that will add the name and score to a MySQL DB.
		// Supply it with a string representing the players name and the players score.
		string hash = DataUtility.Md5Sum(name + password + email + secretKey);
		print(hash);

		string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&password=" + password + "&email=" + email + "&hash=" + hash;
		print(post_url);

		// Post the URL to the site and create a download object to get the result.
		WWW hs_post = new WWW(post_url);
		yield return hs_post; // Wait until the download is done

		print(hs_post.progress);

		if (hs_post.error != null)
		{
			print("There was an error posting the high score: " + hs_post.error);
		}
	}

//	// Get the scores from the MySQL DB to display in a GUIText.
//	// remember to use StartCoroutine when calling this function!
//	IEnumerator GetScores()
//	{
//		gameObject.guiText.text = "Loading Scores";
//		WWW hs_get = new WWW(highscoreURL);
//		yield return hs_get;
//
//		if (hs_get.error != null)
//		{
//			print("There was an error getting the high score: " + hs_get.error);
//		}
//		else
//		{
//			gameObject.guiText.text = hs_get.text; // this is a GUIText that will display the scores in game.
//		}
//	}
}
