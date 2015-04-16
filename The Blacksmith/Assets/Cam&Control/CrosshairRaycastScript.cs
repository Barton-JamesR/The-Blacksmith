using UnityEngine;
using System.Collections;
//Handles the reticule and tooltip that you get for hovering over things.
public class CrosshairRaycastScript : MonoBehaviour {
	public Texture2D crosshairImage;//The image of the reticule you're putting in; if this isn't the Reticule.png that came with this,
									//You will have to adjust the width and height of said reticule manually below in OnGUI().
	public GUIText text;			//The text where the tooltip will show up.  It's its own object in the scene, and should be present for every scene.
									//It must be part of the main scene too; if you try to attach it to the prefab, it will just break.
	Ray ray;
	RaycastHit hit;
	void Start () {
	}
	void OnGUI(){
		float xMin = (Screen.width/2)-(33/2);//Any 33s here should be replaced with the width or height (w, h, w, h in that order) of the new
		float yMin = (Screen.height/2)-(33/2);//crosshair image on replacement.
		GUI.DrawTexture(new Rect(xMin, yMin, 33, 33), crosshairImage);
	}
	// Update is called once per frame
	void Update () {
		ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
		if(Physics.Raycast(ray,out hit,3)){
			//Debug.Log(hit.collider.tag);
			if(hit.collider.tag != "Untagged"){
				text.text=hit.collider.tag;
			}
			else{
				text.text="";
			}
		}
		else{
			//Debug.Log("No hit");
			text.text="";
		}
	}
}
