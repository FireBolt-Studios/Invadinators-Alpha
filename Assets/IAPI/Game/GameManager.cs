using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public ProfileManager PManager;

	void Awake ()
	{
		DontDestroyOnLoad (this);
	}

}
