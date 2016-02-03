using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowData : MonoBehaviour {

	public ScoreOfGamer Data; // получаем ссылку на данные
	public Text ScoreText; // куда выводить счет
	public Text LevelText; // куда выодить номер уровня
	public Text TimeText; //счетчик времени
	// Use this for initialization
	void Start () {
		// получаем компоненты
		Data = Data.GetComponent<ScoreOfGamer> ();
		ScoreText = ScoreText.GetComponent<Text> ();
		LevelText = LevelText.GetComponent<Text> ();
		TimeText = TimeText.GetComponent<Text> ();
		// связываем ссылки с данными
		LevelText.text=Data.Level.ToString();
		ScoreText.text=Data.Score.ToString();
		TimeText.text =Data.CurTime.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		// обновляем данные (это не двухсторонний биндинг. увы)
		LevelText.text=Data.Level.ToString();
		ScoreText.text=Data.Score.ToString();
		TimeText.text =Data.CurTime.ToString ("f2");
	}
}
