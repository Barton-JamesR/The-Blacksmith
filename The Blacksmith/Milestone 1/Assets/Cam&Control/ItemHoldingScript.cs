using UnityEngine;
using System.Collections;
//This script is directly attached to the First Person Controller object, and handles the mouse-clicks and e-button use.
//It will send messages to the objects held if need be.
public class ItemHoldingScript : MonoBehaviour {
	public GameObject parent;//Drag the 'First Person Controller' (AND NOT THE GRAPHICS SEGMENT) onto this in the inspector!
	GameObject leftHand = null; //These are private for a reason; only this script may tell you what you may and may be holding.
	GameObject rightHand = null;
	bool locked = false; //Determines if you're allowed to move away from your station.
	Ray ray; //Part of the raycasting, see below.
	RaycastHit hit; //Part of the raycasting; don't want to re-create the var
	string State = ""; //This is for determining what's going on with the player presently; being in different stations affects your state.

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){//NOTE:  0 is LMB, 1 is RMB, 2 is MMB.
			ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
			if(leftHand != null){
				if(leftHand.tag=="Hammer")//Right now, this is just for hammer; once everything we pick up can have a rigidbody on it
					leftHand.GetComponent<Rigidbody>().isKinematic=false;//We just remove the if there and make it always change kinematic.
				leftHand.transform.parent = null;
				leftHand = null;
			}
			else if(Physics.Raycast(ray,out hit,3)){
				if(leftHand == null){

					if(hit.collider.tag=="Hammer"){//Right now we only pick up barrels, but we can do SO MUCH MORE!
						Debug.Log("Got something in my left hand!  It's a "+hit.collider.tag);
						leftHand = hit.collider.gameObject;
						leftHand.transform.parent = parent.transform;
						leftHand.GetComponent<Rigidbody>().isKinematic=true;
						leftHand.transform.localPosition = new Vector3(-1,0,1);//One to the left, zero up, one forward.  About the 'left hand' position compared to the camera
						leftHand.transform.localRotation = new Quaternion(0,150,0, 0);//The hammer we have needs to be rotated properly to face forward...
					}
				}
			}
		}
		if(Input.GetMouseButtonDown(1)){
			ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
			if(rightHand != null){
				rightHand.transform.parent = null;
				rightHand = null;
			}
			else if(Physics.Raycast(ray,out hit,3)){
				if(rightHand == null){
					if(hit.collider.tag=="Iron"){//Right now we only pick up barrels, but we can do SO MUCH MORE!
						Debug.Log("Got something in my right hand!  It's a "+hit.collider.tag);
						rightHand = hit.collider.gameObject;
						rightHand.transform.parent = parent.transform;
						rightHand.transform.localPosition = new Vector3(1,0,1);//One to the right, zero up, one forward.  Right hand position.
					}
				}
			}
		}
		if(Input.GetKeyDown ("e")){
			ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
			if(!locked){
				if(Physics.Raycast(ray,out hit,3)){
					if(hit.collider.tag=="Anvil"){
						parent.transform.position = new Vector3(hit.transform.position.x+1,parent.transform.position.y,hit.transform.position.z);
						parent.transform.rotation = (parent.transform.rotation*(Quaternion.FromToRotation(parent.transform.forward,hit.transform.forward)));
						parent.transform.Rotate (new Vector3(0,-90,0));
						locked=true;
						lockPlayer (false);

					}
					if(hit.collider.tag=="Workbench"){
						parent.transform.position = new Vector3(hit.transform.position.x+1,parent.transform.position.y,hit.transform.position.z);
						parent.transform.rotation = (parent.transform.rotation*(Quaternion.FromToRotation(parent.transform.forward,hit.transform.forward)));
						parent.transform.Rotate (new Vector3(0,-90,0));
						locked=true;
						lockPlayer (false);

					}
					if(hit.collider.tag=="Forge"){
						parent.transform.position = new Vector3(hit.transform.position.x+1,parent.transform.position.y,hit.transform.position.z);
						parent.transform.rotation = (parent.transform.rotation*(Quaternion.FromToRotation(parent.transform.forward,hit.transform.forward)));
						parent.transform.Rotate (new Vector3(0,-90,0));
						locked=true;
						lockPlayer (false);

					}
					if(hit.collider.tag=="Barrel"){
						parent.transform.position = new Vector3(hit.transform.position.x+1,parent.transform.position.y,hit.transform.position.z);
						parent.transform.rotation = (parent.transform.rotation*(Quaternion.FromToRotation(parent.transform.forward,hit.transform.forward)));
						parent.transform.Rotate (new Vector3(0,-90,0));
						locked=true;
						lockPlayer (false);

					}
					if(hit.collider.tag=="Grindstone"){
						parent.transform.position = new Vector3(hit.transform.position.x+1,parent.transform.position.y,hit.transform.position.z);
						parent.transform.rotation = (parent.transform.rotation*(Quaternion.FromToRotation(parent.transform.forward,hit.transform.forward)));
						parent.transform.Rotate (new Vector3(0,-90,0));
						locked=true;
						lockPlayer (false);

					}
				}
			}
			else{
				locked = false;
				lockPlayer(true);
			}
		}
	}

	void lockPlayer(bool islocked){
		parent.GetComponent<CharacterMotor>().canControl = islocked;
	}
}
