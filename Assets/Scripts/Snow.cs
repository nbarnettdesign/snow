using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Snow : MonoBehaviour {

	// int of current bodyheat
	public int bodyHeat = 100;

	//int of max body heat
	public int bodyHeatMax = 100;

	//float of timer as bodyheat decreases
	public float decreaseTimer = 2;

	//float of timer as bodyheat increases
	public float increaseTimer = 5;

	//float of which timer is active
	public float rateTimer;

	//float of timer
	public float timer;

	//int of amount of body heat lost while away from heat
	public int decreaseAmount = -2;

	//int of amount of body heat gained while at heat source
	public int increaseAmount = 5;

	//int of which rate above is active
	public int bodyHeatRate;

	//slider of body heat
	public Slider heatSlider;

	//warm UI
	public GameObject warm;

	//cold UI
	public GameObject cold;

	//bool for when player freezes to death
	private bool frozenToDeath = false;
	public GameObject home;

	//bool for when player gets home
	private bool homeSafe = false;
	public GameObject frozen;

	// Use this for initialization
	void Start () {
		bodyHeatRate = decreaseAmount;
		rateTimer = decreaseTimer;
		timer = Time.time;
		heatSlider.maxValue = bodyHeatMax;
	}
	
	// Update is called once per frame
	void Update () {
		if (frozenToDeath || homeSafe){
			bodyHeatRate = 0;
			increaseAmount = 0;
			decreaseAmount = 0;
			if (Input.GetKeyDown (KeyCode.Return)) {
				SceneManager.LoadScene (0);
			}
		}
		heatSlider.value = bodyHeat;
		if (Time.time - timer > rateTimer) {
			timer = Time.time;
			BodyHeatRate ();
			if (bodyHeat >= bodyHeatMax){
				bodyHeat = bodyHeatMax;
				bodyHeatRate = 0;
			}else if (bodyHeat <=0){
				frozenToDeath = true;
				frozen.gameObject.SetActive (true);
			}
		
		}
	}
	//When Something enters the ParticleSystemTriggerEventType
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Fire") {
			bodyHeatRate = increaseAmount;
			rateTimer = increaseTimer;
			warm.SetActive (true);
			cold.SetActive (false);
		} else if (other.tag == "Home") {
			bodyHeatRate = increaseAmount;
			rateTimer = increaseTimer;
			home.SetActive (true);
			homeSafe = true;
		}

	}


	//When someone Exits the Trigger
		void OnTriggerExit () {
		bodyHeatRate = decreaseAmount;
		rateTimer = decreaseTimer;
		warm.SetActive (false);
		cold.SetActive (true);
		}
	public void BodyHeat(){
		
	}
	void BodyHeatRate(){
		bodyHeat = bodyHeat + bodyHeatRate;
	}
	void Death (){

	}
}

