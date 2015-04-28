using UnityEngine;
using System.Collections;

public class EndingGame : MonoBehaviour {

	public void OnMouseDown () {
		if (this.name == "door") {
			// load the stats panel
			// Debug.Log("door was clicked");
			Application.LoadLevel("EndStatistics");
		}
	}

	public void MenuClicked() {
		Application.LoadLevel("MainMenu");
	}

	public void ExitClicked() {
		Application.Quit();
	}
}
