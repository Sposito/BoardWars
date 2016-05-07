using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
	public GameObject infoPannel;
	public void LoadGame(){
		UnityEngine.SceneManagement.SceneManager.LoadScene (1, UnityEngine.SceneManagement.LoadSceneMode.Single);
	}

	public void QuitGame(){
		Application.Quit ();
	}

	public void ToogleInfoPannel(){
		if (infoPannel.activeInHierarchy)
			infoPannel.SetActive (false);
		else
			infoPannel.SetActive (true);
	}
}
