using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreOfGamer : MonoBehaviour {

	public int Score=0; //в начале счет всегда равен 0
	public int Level=1; // начинаем всегда с первого уровня
	public float MaxTime=30f; // начальное количество милисекунд (30 секунд)
	public float CurTime;


	public Canvas JobField; //рабочая площадь
	public Canvas ScoreField; //результат
	public Button  started; // кнопка старта игры
	public Button exited; // кнопка выхода

	public Text scoreData;
	public Text levelData;
	// Use this for initialization
	void Start () {
		CurTime = MaxTime; //устанавливаем время на максимум, задуманный в игре
		// захватить необходимые компоненты у объектов и получить сылки на них
		JobField = JobField.GetComponent<Canvas> ();
		ScoreField = ScoreField.GetComponent<Canvas> ();
		started = started.GetComponent<Button> ();
		exited = exited.GetComponent<Button> ();

		scoreData = scoreData.GetComponent<Text> ();
		levelData= levelData.GetComponent<Text> ();

		ScoreField.enabled = false; // изначально меню выхода скрыто
	}
	
	// Update is called once per frame
	void Update () {
		
		CurTime = Mathf.MoveTowards(CurTime, 0f, Time.deltaTime); //уменьшать на время, равное секундам между отрисовкой кадров, пока не станет равно 0
		if (CurTime == 0) {
			//высветить таблицу результатов
			JobField.enabled=false;
			ScoreField.enabled = true;
			scoreData.text = Score.ToString();
			levelData.text = Level.ToString();
		}	
	}
	//нажали на старт
	public void StartGame()
	{
		Application.LoadLevel("NewGame");// грузим основную игру (здесь номер сцены)
	}
	// если нажали на да, хотим выйти
	public void ExitGame()
	{
		Application.LoadLevel("Start menu"); //выходим в главное меню
	}

}
