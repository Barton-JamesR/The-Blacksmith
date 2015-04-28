using UnityEngine;
using System.Collections;

public class DepositStates : MonoBehaviour {
	public GameObject box;
	public GameObject Player { get; set; }
	public string WeaponState { get; set; }
	private GameObject weapon;
	public PlayerStates myPlayerState = null; // used to access and change the players state
	public GameObject newWeapon;
	public GameObject newBoard;
	public GameObject spawn;
	public GameObject newSteel;
	public GameObject testdummy;
	public void SetWeapon(GameObject w)
	{
		weapon = w;
	}
	
	// Use this for initialization
	void Start () 
	{
		Player = null;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Player != null)
		{
			if(weapon != null)
			{
				WeaponState = weapon.GetComponent<ItemStates>().State;
			}
		}
		else
		{
			weapon = null;
			WeaponState = "NULL_STATE";
			
		}
		if(Input.GetMouseButton (1))
		{
			Debug.Log ("The weapon (or shield)'s state is..." + WeaponState);
			if(weapon != null)
			{
				if(WeaponState == "Good")
				{
					myPlayerState.points += weapon.GetComponent<ItemStates>().Quality;
					weapon.transform.position = new Vector3(box.transform.position.x, weapon.transform.position.y+.25f, box.transform.position.z);	//sets weapon above box
					weapon.transform.parent = null;
					weapon.GetComponent<Rigidbody>().isKinematic=false;
					weapon.GetComponent<BoxCollider>().isTrigger=false;
					Destroy (weapon, 1f);
					weapon = null;
					myPlayerState.RightHand = "Empty";
				}
			}
		}
		/*if(Input.GetKeyDown ("[1]")){
			if(myPlayerState.points >= 30){
				Instantiate(newWeapon, spawn.transform.position, spawn.transform.rotation);
				myPlayerState.points -= 30;
			}
		}
		if(Input.GetKeyDown ("[3]")){
			if(myPlayerState.points >= 20){
				Instantiate(newBoard, spawn.transform.position, spawn.transform.rotation);
				myPlayerState.points -= 20;
			}
		}
		if(Input.GetKeyDown ("[4]")){
			if(myPlayerState.points >= 80){
				Instantiate(newSteel, spawn.transform.position, spawn.transform.rotation);
				myPlayerState.points -= 80;
			}
		}*/
	}
	public void inst(int i){
		if(i==1){
			Instantiate(newWeapon, spawn.transform.position, spawn.transform.rotation);
		}
		if(i==2){
			Instantiate(newBoard, spawn.transform.position, spawn.transform.rotation);
		}
		if(i==3){
			Instantiate(newSteel, spawn.transform.position, spawn.transform.rotation);
		}
	}
}