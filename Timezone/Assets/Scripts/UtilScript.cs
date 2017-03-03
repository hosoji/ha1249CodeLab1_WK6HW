using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class UtilScript : MonoBehaviour {

	public static void WriteJSONtoFile(string path, string fileName, JSONClass json){
		WriteStringToFile (path, fileName, json.ToString ());
	}

	public static void WriteStringToFile ( string path, string fileName, string content){
		StreamWriter sw = new StreamWriter (path + "/" + fileName);

		sw.Write (content);

		sw.Close ();
	}

	public static JSONNode ReadJSOnfromFile(string path, string fileName){
		return JSON.Parse(ReadStringFromFile (path, fileName));
	}

	public static string ReadStringFromFile(string path, string fileName){
		StreamReader sr = new StreamReader (path + "/" + fileName);

		string result = sr.ReadToEnd ();

		sr.Close ();

		return result;
	}

	public static Vector3 CloneVector3(Vector3 vec){
		return new Vector3 (vec.x, vec.y, vec.z);
	}
}
