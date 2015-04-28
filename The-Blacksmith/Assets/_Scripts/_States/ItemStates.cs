using UnityEngine;
using System.Collections;

public class ItemStates : MonoBehaviour 
{
	public int baseState = 100;
	// Properties that can store the State of the GameObject and the index in the array of STATES
	public string State { get; set; }
	public int Quality { get; set; }	// To hold a numerical representation of the quality of the item
	public int Durability { get; set; }	// To hold a numerical representation of the durability of the item
	public int Sharpness { get; set; }
	//------------James' brief section-----------//
	// Note that we can use the weapons Durability as way of checking progress on the creation of the item ... or maybe just another property
	// public int Progress { get; set; }
	public int Form { get; set; }  //Holds the form of the item; after being struck with a hammer at an anvil, this increases.
	public int Heat { get; set; }  //Holds the heat of the item; being held in the forge increases heat, striking at an anvil, using the bucket,
								   //Or the passing of time changes this for the +/-/-.
	private double cooldownH = 1.0;//Tracks how long it's been since the weapon cooled a bit, cools it 1heat/sec
	private bool isFormed = false; //tracks the first time the object reaches 100 form; once it does the mesh should swap for a swordy-one.
	//-----------End James' Section-----------------//

	// A constant string that contains a set list of all the possible states
	string[] STATES = {"Good", "Bad"};
	
	//public enum STATES{GOOD, BAD}
	//enum myState = GOOD;

	// Reference to this script
	private ItemStates myItemState;

	// Use this for initialization
	void Start () 
	{
		State = STATES[1]; // Items start at bad, and get 'Good' once they're formed (even if still hot or not sharp); I'll use this to 
		Quality = baseState;     //Gauge whether or not the shop should accept the item eventually.
		Durability = 100; // 100 sounds good for these ... 
		Sharpness = 0;
		Heat = 0;//James:  Added a tracker for object heat
		Form = 0;//James:  Added a tracker for the Form of the object; how much smithing was added.
	} // End Start()

	void Update()
	{
		cooldownH -= Time.deltaTime;
		if(cooldownH <= 0){
			cooldownH = 1.0;
			addHeat(-1);
		}

		if(Input.GetKeyDown("u")){
			Debug.Log (gameObject.tag + ":" + State);
		}
	}


//----------------James' "too much of a wuss to make unity play with the properties directly" section -------------//
	public void addHeat(int f){
		Heat += f;
		if(Heat >= 100){
			Heat = 100;
		}
		if(Heat <= 0){
			Heat = 0;
		}
		//Debug.Log("Added " + f + " heat; heat= " + Heat);
	}
	public int getHeat(){
		return Heat;
	}
	public void setstate(int f){
		State = STATES[f];
		Debug.Log (State);
	}
	public void addForm(int f){
		Form += f;
		if(Form > 100){
			Form = 100;}
		if(Form == 100){
			State = STATES[0];
			if(gameObject.tag=="Iron" || gameObject.tag=="Steel")
				gameObject.tag="Weapon";
		}
		if(Form < 0){
			Form = 0;}
		//Debug.Log("Added " + f + "Form:  Form=" + Form);
	}
	public void addQuality(int f){
		Quality += f;
		if(Quality < 0){
			Quality = 0;
		}
		if(Quality > 200){
			Quality = 200;
		}
		if(Quality > 125){
			Debug.Log ("State set!");
			State = STATES[0];
		}
	}
	public void addDurability(int f){
		Durability += f;
		if(Durability < 0){
			//IT BREAKS!  ...just kidding...
			Durability = 0;
		}
		if(Durability > 100){
			Durability = 100;
		}
	}
	public void addSharpness(int f){
		Sharpness += f;
		if(Sharpness > 100){
			Sharpness = 100;
		}
		if(Sharpness < 0){
			Sharpness = 0;
		}
		//Debug.Log("Added " + f + "Sharpness:  Sharpness=" + Form);
	}
} // End ItemStates


//-------------James' Notes on the compatibility between workstations and objects-----------//
//Each station (except workbench) changes the state of these:
//Forge increases heat at .2/tick, up to 100%
//Air reduces heat by 1/second. 
//Anvil with hammer, if heat >= 50 (call it 50 for now) reduces heat by seven, increases form by 5, quality by 2.  (change this later based on level)
//Anvil with hammer, if heat < 50 reduces quality by 2.5, durability by 5.  Don't beat the cold iron.
//Bucket reduces heat by 15/tick, and creates steam particles if heat was > 0
//Grindstone with heat below 25 increases sharpness by 5, and quality by 2.5, and adds sparks.
//Grindstone with heat above 25 decreases sharpness, durability by 5and quality by 2.5.  Don't try to grind the hot metal; I should probably change form
//But I really don't want to >.>