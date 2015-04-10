using UnityEngine;
using System.Collections;

public class PlayBtn : MonoBehaviour {

	void OnMouseDown () {
		if (this.name == "PlayText") {
			Application.LoadLevel ("beginning building");
		}
	}
}
