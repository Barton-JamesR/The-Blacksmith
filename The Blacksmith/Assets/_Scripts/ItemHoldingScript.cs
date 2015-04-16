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
	Collider shackle; //This is the workstation you're at!

	public GUIText PointText;
	public GUIText ToolText;
	public GUIText WeaponText;
	public GUIText HeatText;
	public GUIText SharpText;
	public GUIText FormText;


	void Start () {
		myPlayerState = GetComponent <PlayerStates>(); // get the Player State script and attach the reference
	}
	
	// Update is called once per frame
	public void setShield(GameObject RHS, int qual){
		Debug.Log ("Entered setShield...");
		rightHand = RHS;
		rightHand.transform.parent = parent.transform;
		Debug.Log ("Assigned shield...");
		rightHand.GetComponent<Rigidbody>().isKinematic=true;
		rightHand.GetComponent<BoxCollider>().isTrigger=true;
		Debug.Log ("Set variables...");
		rightHand.transform.localPosition = new Vector3(1f,0f,1.2f);
		rightHand.transform.localRotation = new Quaternion(0,0,0,0);
		Debug.Log ("Set position...");
		rightHand.GetComponent<ItemStates>().setstate (0);
		rightHand.GetComponent<ItemStates>().addQuality (qual);
		myPlayerState.RightHand = rightHand.tag;
		Debug.Log ("Cleared setShield()...");
		Debug.Log (rightHand.GetComponent<ItemStates>().State);
	}
	void Update () {
		updateHUD();
		if(Input.GetMouseButtonDown(0)){//NOTE:  0 is LMB, 1 is RMB, 2 is MMB.
			ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
			if(leftHand != null){
				if(!locked){
					if(leftHand.tag=="Hammer")//Right now, this is just for hammer; once everything we pick up can have a rigidbody on it
						leftHand.GetComponent<Rigidbody>().isKinematic=false;//We just remove the if there and make it always change kinematic.
					leftHand.transform.parent = null;
					leftHand = null;
					myPlayerState.LeftHand = "Empty";
				}
			}
			else if(Physics.Raycast(ray,out hit,3)){
				if(leftHand == null){
					if(hit.collider.tag=="Hammer"){//Right now we only pick up barrels, but we can do SO MUCH MORE!
						//DebugLog("Got something in my left hand!  It's a "+hit.collider.tag);
						leftHand = hit.collider.gameObject;
						leftHand.transform.parent = parent.transform;
						leftHand.GetComponent<Rigidbody>().isKinematic=true;
						leftHand.transform.localPosition = new Vector3(-1,0,1);//One to the left, zero up, one forward.  About the 'left hand' position compared to the camera
						leftHand.transform.localRotation = new Quaternion(0,150,0, 0);//The hammer we have needs to be rotated properly to face forward...
						myPlayerState.LeftHand = leftHand.tag;
					}
				}
			}
			//Debug.Log("The left hand is holding " + myPlayerState.LeftHand);
		}
		if(Input.GetMouseButtonDown(1)){
			ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
			if(rightHand != null){
				if(!locked){
					rightHand.transform.parent = null;
					rightHand.GetComponent<Rigidbody>().isKinematic=false;
					rightHand.GetComponent<BoxCollider>().isTrigger=false;
					rightHand = null;
					myPlayerState.RightHand = "Empty";
				}
			}
			else if(Physics.Raycast(ray,out hit,3)){
				if(rightHand == null){
					if(hit.collider.tag=="Iron"){//Right now we only pick up barrels, but we can do SO MUCH MORE!
						//Debug.Log("Got something in my right hand!  It's a "+hit.collider.tag);
						rightHand = hit.collider.gameObject;
						rightHand.transform.parent = parent.transform;
						rightHand.GetComponent<Rigidbody>().isKinematic=true;
						rightHand.transform.localPosition = new Vector3(1,0,1);//One to the right, zero up, one forward.  Right hand position.
						rightHand.transform.localRotation = new Quaternion(0,0,0, 0);//
						myPlayerState.RightHand = rightHand.tag;
					}
					if(hit.collider.tag=="Weapon"){
						//Debug.Log("Got something in my right hand!  It's a "+hit.collider.tag);
						rightHand = hit.collider.gameObject;
						rightHand.transform.parent = parent.transform;
						rightHand.GetComponent<Rigidbody>().isKinematic=true;
						rightHand.transform.localPosition = new Vector3(1,0,1);//One to the right, zero up, one forward.  Right hand position.
						rightHand.transform.localRotation = new Quaternion(0,0,0, 0);//
						myPlayerState.RightHand = rightHand.tag;
					}
					if(hit.collider.tag=="Wood" || hit.collider.tag=="Shield"){
						rightHand = hit.collider.gameObject;
						rightHand.transform.parent = parent.transform;
						rightHand.GetComponent<Rigidbody>().isKinematic=true;
						rightHand.GetComponent<BoxCollider>().isTrigger=true;
						rightHand.transform.localPosition = new Vector3(1f,0f,1.2f);
						rightHand.transform.localRotation = new Quaternion(0,0,0,0);
						myPlayerState.RightHand = rightHand.tag;
					}
				}
			}
			//Debug.Log("The right hand is holding " + myPlayerState.RightHand);;
		}


		//GameObject hitMe = null;
		string theTag = "";
		if(Input.GetKeyDown ("e")){

			if(!locked){
				ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
				if(Physics.Raycast(ray,out hit,3)){
					//hitMe = hit.collider.gameObject;
					theTag = hit.collider.tag;
					if(hit.collider.tag=="Anvil"){
						SnapToObject();
						hit.collider.GetComponent<AnvilStates>().Player = parent; // assign the player reference to the Player property in the AnvilStates script
						hit.collider.GetComponent<AnvilStates>().SetWeapon(rightHand);
						hit.collider.GetComponent<AnvilStates>().SetTool(leftHand);
						GetComponent<MouseLook>().sensitivityX = 0;
						shackle = hit.collider;
					}
					if(hit.collider.tag=="Workbench"){
						SnapToObject();
						hit.collider.GetComponent<WorkbenchStates>().Player = parent;
						hit.collider.GetComponent<WorkbenchStates>().SetWeapon (rightHand);
						//hit.collider.GetComponent<WorkbenchStates>().SetTool (leftHand);
						shackle = hit.collider;
					}
					if(hit.collider.tag=="Forge"){
						SnapToObject();
						hit.collider.GetComponent<ForgeStates>().Player = parent;
						hit.collider.GetComponent<ForgeStates>().SetWeapon(rightHand);
						shackle = hit.collider;
						GetComponent<MouseLook>().sensitivityX = 0;
					}
					if(hit.collider.tag=="Barrel"){
						SnapToObject();
						hit.collider.GetComponent<BarrelStates>().Player = parent;
						hit.collider.GetComponent<BarrelStates>().SetWeapon(rightHand);
						shackle = hit.collider;
						GetComponent<MouseLook>().sensitivityX = 0;
					}
					if(hit.collider.tag=="Grindstone"){
						SnapToObject();
						hit.collider.GetComponent<GrindstoneStates>().Player = parent;
						hit.collider.GetComponent<GrindstoneStates>().SetWeapon(rightHand);
						shackle = hit.collider;
						GetComponent<MouseLook>().sensitivityX = 0;
					}
					if(hit.collider.tag=="Deposit Box"){
						SnapToObject ();
						hit.collider.GetComponent<DepositStates>().Player = parent;
						hit.collider.GetComponent<DepositStates>().SetWeapon(rightHand);
						hit.collider.GetComponent<DepositStates>().myPlayerState = myPlayerState;
						shackle = hit.collider;
						GetComponent<MouseLook>().sensitivityX = 0;
					}
				}
			}
			else{
				myPlayerState.Place = "Free"; 
				////DebugLog("We are unlocked and theTag = " + theTag + " or " + hit.collider.tag);
				GetComponent<MouseLook>().sensitivityX = 15;//The sensitivityX=0 or 15 is to stop the player from looking left/right while undergoing
														//A process; if you don't, they can look left or right and the object rotates with them.
				//DebugLog(shackle);
				if(shackle.tag=="Anvil"){
					shackle.GetComponent<AnvilStates>().Player = null; 
					shackle = null;
				}
				else if(shackle.tag=="Workbench"){

					shackle.GetComponent<WorkbenchStates>().Player = null;
					shackle=null;
					if(rightHand.tag == "Shield")
						rightHand.GetComponent<ItemStates>().setstate(0);
					Debug.Log ("ShieldState... "+rightHand.GetComponent<ItemStates>().State);
					Debug.Log ("Shield's Tag... "+rightHand.tag);
				}
				else if(shackle.tag=="Forge"){
					shackle.GetComponent<ForgeStates>().Player = null;
					shackle = null;
				}
				else if(shackle.tag=="Barrel"){
					shackle.GetComponent<BarrelStates>().Player = null;
					shackle = null;
				}
				else if(shackle.tag=="Grindstone"){
					shackle.GetComponent<GrindstoneStates>().Player = null;
					shackle = null;
				}
				else if(shackle.tag=="Deposit Box"){
					shackle.GetComponent<DepositStates>().Player = null;
					shackle = null;
				}
				locked = false;
				lockPlayer(true);
				if(leftHand != null){//These two ifs in the station-release section are to ensure that the equipment goes right back to where it belongs.
					leftHand.transform.localPosition = new Vector3(-1,0,1);//One to the right, zero up, one forward.  Right hand position.
					leftHand.transform.localRotation = new Quaternion(0,150,0, 0);//The hammer we have needs to be rotated properly to face forward...
				}
				if(rightHand != null){
					rightHand.transform.localPosition = new Vector3(1,0,1);//One to the right, zero up, one forward.  Right hand position.
					rightHand.transform.localRotation = new Quaternion(0,0,0, 0);//The hammer we have needs to be rotated properly to face forward...
				}
			}
			//Debug.Log("The player is at " + myPlayerState.Place);
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

	void updateHUD(){
		if(leftHand != null){
			ToolText.text = leftHand.tag;
		}
		else{
			ToolText.text = "";
		}
		if(rightHand != null){
			WeaponText.text = rightHand.tag;
			if(rightHand.tag == "Weapon" || rightHand.tag == "Iron"){
				SharpText.text = "Sharpness:  " + rightHand.GetComponent<ItemStates>().Sharpness.ToString();
				HeatText.text = "Heat level:  " + rightHand.GetComponent<ItemStates>().Heat.ToString() + "%";
				FormText.text = "Shape rating:  " + rightHand.GetComponent<ItemStates>().Form.ToString() + "%";
			}
			if(rightHand.tag == "Shield"){
				SharpText.text = "Quality:  " + rightHand.GetComponent<ItemStates>().Quality.ToString ();
			}
		}
		else{
			WeaponText.text = "";
			SharpText.text = "";
			HeatText.text = "";
			FormText.text = "";
		}
		PointText.text = "Points: " + myPlayerState.points.ToString ();
	}
}
