using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestScript : MonoBehaviour {
	//public ScoreOfGamer Data;

	public MouseCoordinates temp;
	//public Text t;

	// Use this for initialization
	void Start () {
		//Data = Data.GetComponent<ScoreOfGamer> ();

		temp = temp.GetComponent<MouseCoordinates> ();
		//t.text = "";
	}
	
	// Update is called once per frame
	void Update () {



		/*if (Input.GetKeyDown(KeyCode.Mouse0)) // если нажата левая кнопка мыши
		{
			Data.Score=Data.Score+1;
		}*/


	
	}

}
