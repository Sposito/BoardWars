using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class MenuController : MonoBehaviour {
	public GameObject infoPannel;
	public GameObject rulesPannel;
	public void LoadGame(){
		#if UNITY_IOS || UNITY_EDITOR
		Advertisement.Show ();
		#endif

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

	public void ToogleRulesPannel(){
		if (rulesPannel.activeInHierarchy)
			rulesPannel.SetActive (false);
		else
			rulesPannel.SetActive (true);
	}
}
