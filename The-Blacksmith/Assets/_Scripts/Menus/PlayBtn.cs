using UnityEngine;
using System.Collections;

public class PlayBtn : MonoBehaviour {

	public void OnMouseDown () {
		if (this.name == "PlayText") {
			Debug.Log("play button pressed");
			Application.LoadLevel ("beginning building");
		}
	}
}
