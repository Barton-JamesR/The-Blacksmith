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
	//string State = ""; //This is for determining what's going on with the player presently; being in different stations affects your state.
	PlayerStates myPlayerState; // used to access and change the players state


	void Start () {
		myPlayerState = GetComponent <PlayerStates>(); // get the Player State script and attach the reference
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
				myPlayerState.LeftHand = "Empty";
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
						myPlayerState.LeftHand = leftHand.tag;
					}
				}
			}
			Debug.Log("The left hand is holding " + myPlayerState.LeftHand);
		}
		if(Input.GetMouseButtonDown(1)){
			ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
			if(rightHand != null){
				rightHand.transform.parent = null;
				rightHand.GetComponent<Rigidbody>().isKinematic=false;
				rightHand = null;
				myPlayerState.RightHand = "Empty";
			}
			else if(Physics.Raycast(ray,out hit,3)){
				if(rightHand == null){
					if(hit.collider.tag=="Iron"){//Right now we only pick up barrels, but we can do SO MUCH MORE!
						Debug.Log("Got something in my right hand!  It's a "+hit.collider.tag);
						rightHand = hit.collider.gameObject;
						rightHand.transform.parent = parent.transform;
						rightHand.GetComponent<Rigidbody>().isKinematic=true;
						rightHand.transform.localPosition = new Vector3(1,0,1);//One to the right, zero up, one forward.  Right hand position.
						myPlayerState.RightHand = rightHand.tag;
					}
					if(hit.collider.tag=="Weapon"){
						Debug.Log("Got something in my right hand!  It's a "+hit.collider.tag);
						rightHand = hit.collider.gameObject;
						rightHand.transform.parent = parent.transform;
						rightHand.GetComponent<Rigidbody>().isKinematic=true;
						rightHand.transform.localPosition = new Vector3(1,0,1);//One to the right, zero up, one forward.  Right hand position.
						rightHand.transform.localRotation = new Quaternion(0,0,0, 0);//The hammer we have needs to be rotated properly to face forward...
						myPlayerState.RightHand = rightHand.tag;
					}
				}
			}
			Debug.Log("The right hand is holding " + myPlayerState.RightHand);;
		}


		//GameObject hitMe = null;
		string theTag = "";
		if(Input.GetKeyDown ("e")){
			ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);

			if(!locked){
				if(Physics.Raycast(ray,out hit,3)){
					//hitMe = hit.collider.gameObject;
					theTag = hit.collider.tag;
					if(hit.collider.tag=="Anvil"){
						SnapToObject();
						hit.collider.GetComponent<AnvilStates>().Player = parent; // assign the player reference to the Player property in the AnvilStates script
						hit.collider.GetComponent<AnvilStates>().SetWeapon(rightHand);
						hit.collider.GetComponent<AnvilStates>().SetTool(leftHand);
					}
					if(hit.collider.tag=="Workbench"){
						SnapToObject();
						//hit.collider.GetComponent<WorkbenchStates>().Player = transform.parent.gameObject;
					}
					if(hit.collider.tag=="Forge"){
						SnapToObject();
						hit.collider.GetComponent<ForgeStates>().Player = parent;
						hit.collider.GetComponent<ForgeStates>().SetWeapon(rightHand);
					}
					if(hit.collider.tag=="Barrel"){
						SnapToObject();
						hit.collider.GetComponent<BarrelStates>().Player = parent;
						hit.collider.GetComponent<BarrelStates>().SetWeapon(rightHand);
					}
					if(hit.collider.tag=="Grindstone"){
						SnapToObject();
						hit.collider.GetComponent<GrindstoneStates>().Player = parent;
						hit.collider.GetComponent<GrindstoneStates>().SetWeapon(rightHand);
					}
				}
			}
			else{
				locked = false;
				lockPlayer(true);
				myPlayerState.Place = "Free"; 
				Debug.Log("We are unlocked and theTag = " + theTag + " or " + hit.collider.tag);

				if(hit.collider.tag=="Anvil"){
					hit.collider.GetComponent<AnvilStates>().Player = null; 
				}
				if(hit.collider.tag=="Workbench"){
					//hit.collider.GetComponent<WorkbenchStates>().Player = null;
				}
				if(hit.collider.tag=="Forge"){
					hit.collider.GetComponent<ForgeStates>().Player = null;
				}
				if(hit.collider.tag=="Barrel"){
					hit.collider.GetComponent<BarrelStates>().Player = null;
				}
				if(hit.collider.tag=="Grindstone"){
					hit.collider.GetComponent<GrindstoneStates>().Player = null;
				}
			}
			Debug.Log("The player is at " + myPlayerState.Place);
		}
	}

	void lockPlayer(bool islocked){
		parent.GetComponent<CharacterMotor>().canControl = islocked;
	}

	void SnapToObject()
	{
		parent.transform.position = new Vector3(hit.transform.position.x+1,parent.transform.position.y,hit.transform.position.z);
		parent.transform.rotation = (parent.transform.rotation*(Quaternion.FromToRotation(parent.transform.forward,hit.transform.forward)));
		parent.transform.Rotate (new Vector3(0,-90,0));
		locked=true;
		lockPlayer (false);
		myPlayerState.Place = hit.collider.tag; // Set the property Place to the tag of whatever thing we get locked to.
	}
}
