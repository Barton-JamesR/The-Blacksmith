using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {
	
	// OnMouseDown is called everytime the collider gets clicked by the mouse
	public void OnMouseDown () {
		if (this.name == "door") { // if they click door, load the statistics page.
			// load the stats panel
			// Debug.Log("door was clicked");
			Application.LoadLevel("EndStatistics");
		}
		if (this.tag == "MenuButton" ) { // if they click the menu button, load the main menu screen
			// load the main menu
			Debug.Log("menu button click");
			Application.LoadLevel ("MainMenu");
		}
		if (this.tag == "ExitButton") { // if they choose to exit, quit the game
			//  Exit the game
			Application.Quit();
		}
	}
}
