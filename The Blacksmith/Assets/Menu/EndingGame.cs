using UnityEngine;
using System.Collections;

public class EndingGame : MonoBehaviour {

	public void OnMouseDown () {
		if (this.name == "door") {
			// load the stats panel
			Debug.Log("door was clicked");
			Application.LoadLevel("EndStatistics");
		}
		if (this.tag == "MenuButton" ) {
			// load the main menu
			Application.LoadLevel ("MainMenu");
		}
		if (this.tag == "ExitButton") {
			//  Exit the game
			Application.Quit();
		}
	}
}
