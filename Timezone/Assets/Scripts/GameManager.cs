using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Net;
using SimpleJSON;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public GameObject canvas;
	public GameObject canvasFuel;
	Text myText;
	Text fuelText;

	float hrs;
	float mins;

	int num;

	const int HOURS_MAX = 24;
	const int MINS_MAX = 60;


	const int FUEL_MIN = 0;
	public const int FUEL_MAX = 1500;

	public int baseFuelCost;

	private int fuel;

	public int Fuel{
		get{
			return fuel;
		}

		set{
			fuel = value;

			if(fuel > FUEL_MAX){
				fuel = FUEL_MAX;
			}

			if(fuel < FUEL_MIN){
				fuel = FUEL_MIN;
			}
		}
	}


	// Use this for initialization
	void Start () {

		if(instance == null){
			instance = this;
			DontDestroyOnLoad(this);
		} else {
			Destroy(gameObject);
		}
	
		myText = canvas.GetComponent<Text> ();
		fuelText = canvasFuel.GetComponent<Text> ();

		hrs = 0;
		mins = Time.time;

		Fuel = FUEL_MAX;

		///////////////////////////////////////
		//JSON TESTING BEGIN

		JSONClass subClass = new JSONClass ();

		subClass ["test"] = "value";

		JSONClass json = new JSONClass();

		json["x"].AsFloat = 7;
		json["y"].AsFloat = 0;
		json["z"].AsFloat = 2;
		json ["name"] = "Hosni";
		json ["sub"] = subClass;


		UtilScript.WriteStringToFile (Application.dataPath, "file.json", json.ToString ());
		Debug.Log(json);

		string result = UtilScript.ReadStringFromFile (Application.dataPath, "file.json");

		JSONNode readJSON = JSON.Parse (result);

		Debug.Log (readJSON ["z"].AsFloat);

		WebClient client = new WebClient ();
		string content = client.DownloadString ("https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22nome%2C%20ak%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys ");

		JSONNode miami = JSON.Parse (content);

		string weather = miami ["query"] ["results"] ["channel"] ["item"] ["condition"]["text"];
		print (weather);

		//JSON TESTING END
		///////////////////////////////////////



//		hourVar = System.DateTime.Now.ToString("HH:");
//		string t = System.DateTime.Now.AddHours (2).ToString ("HH:mm");
//		string m = System.DateTime.Now.AddHours (10).ToString ("HH:mm");

	}

	// Update is called once per frame
	void Update () {
//		hrs = (hrs + Time.deltaTime/100);
		mins = (mins + Time.deltaTime * 20);
//		Debug.Log (hrs);

		if (hrs >= HOURS_MAX) {
			Reset (hrs, 1);
		}

		if (mins >= MINS_MAX) {
			hrs++;
			Reset (mins, 0);
		}

		// for Testing, remove when Timezones implemented
		if (Input.GetKeyDown(KeyCode.B)){
			hrs++;
		}
//
//		CurrentTime = Time.time;
		myText.text = hrs.ToString("00") + ":" + mins.ToString("00");

		fuelText.text = fuel.ToString () + "F";
	}

	public void Reset(float time, int i){
		if (time >= MINS_MAX && i == 0) {
			mins = 0;
		}
		if (time >= HOURS_MAX && i > 0) {
			hrs = 0;
		}

	}
		
}
