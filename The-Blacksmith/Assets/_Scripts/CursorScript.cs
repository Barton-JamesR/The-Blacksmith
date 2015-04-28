using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour {
	public Texture2D myTex;
	// Use this for initialization
	void Start () {
		Cursor.SetCursor(myTex, new Vector2(16,16), CursorMode.Auto);
		Screen.showCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
