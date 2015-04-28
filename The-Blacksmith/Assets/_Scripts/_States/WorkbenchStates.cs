using UnityEngine;
using System.Collections;

public class WorkbenchStates : MonoBehaviour {
	public GameObject replacementObject;

	public GameObject Player { get; set; }
	public string State { get; set; } // a property for holding a state ... at the moment not sure exactly what it'll be but if we need it it's here
	public string WeaponState { get; set; } // maybe we will need a property that holds the state of the weapon we are working with.
	public string ToolState { get; set; } // maybe another property for the state of the tool we are working with.

	public AudioClip clip;
	public int qual;

	private GameObject weapon;
	private GameObject tool;
	bool isWorking = false;
	double timer;
	double startTime;
	int work;
	public GUIText timeText;
	public GUIText workText;
	public void SetWeapon(GameObject w)
	{
		weapon = w;
	}
	public void SetTool(GameObject t)
	{
		tool = t;
	}

	// Use this for initialization
	void Start () 
	{
		Player = null;
		State = "NULL_STATE";
		WeaponState = "NULL_STATE";
		ToolState = "NULL_STATE";
	}
	// Update is called once per frame
	void Update () 
	{
		if(Player != null){
			// put the code in here for anything that we want to do while at the Workbench.
			if(isWorking){//If you've started working after five seconds...
				timeText.text = (5 - (timer - startTime)).ToString ("0.00");
				workText.text = work.ToString () + " /25";
				if((timer - startTime) > 5){
					if(work>=25){
						GameObject shield = (GameObject)Instantiate (replacementObject);
						shield.GetComponent<ItemStates>().addQuality (work);
						//Debug.Log ("Created the shield");
						Destroy (weapon);
						//Debug.Log ("Destroyed the wood.");
						weapon=shield;
						Player.GetComponent<ItemHoldingScript>().setShield (shield, work);
						qual = work;
						//Debug.Log ("Did ItemHoldingScript.setShield(shield,work)?");
						//Debug.Log (weapon.GetComponent<ItemStates>().State);
					}
					isWorking=false;
					//Debug.Log ("TIME IS UP!\nYou scored... "+work.ToString()+" hits!");
					work = 0;
					//Debug.Log ("ShieldState... "+weapon.GetComponent<ItemStates>().State);
				}
			}
			else{
				timeText.text = "";
				workText.text = "";
			}
			timer = Time.time;
			if (Player != null) 
			{
				//if(tool != null){
				//	if(tool.tag == "Chisel"){
						if(weapon != null){
							if(weapon.tag == "Wood" || weapon.tag == "Hardwood"){
								//Debug.Log ("GOT WOOD~");
								if(Input.GetMouseButton (1)){
									//BEGIN THE MINIGAME!
									StartTime ();
									audio.clip = clip;
									audio.Play ();
									//Try to update a guitext to give the player advice!
									Debug.Log ("STARTING THE MINIGAME!");
								}
							}
						}
					//}
				//}
				if(isWorking){
					if(Input.GetKeyDown("space")){
					   work++;
					   //Debug.Log (work.ToString ()+" HITS");
					}
					if(Input.GetKeyDown("p")){
						work+=30;
					}
				}
			}
			else 
			{
				if(!isWorking){
					weapon = null;
					WeaponState = "NULL_STATE"; // if Player disattaches themselves reset the weapon info to null

					tool = null;
					ToolState = "NULL_STATE";
				}
			}
		}
	}
	void StartTime(){
		isWorking = true;
		startTime = Time.time;
		work = 0;
	}
}
