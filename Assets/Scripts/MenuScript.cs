using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public Canvas quitmen; // меню выхода
	public Button  started; // кнопка старта игры
	public Button exited; // кнопка выхода
	public Canvas StartMenu; //главное меню (отключать, дабы не плодить множество включений)

	// Use this for initialization
	void Start () {
		// захватить необходимые компоненты у объектов и получить сылки на них
		quitmen = quitmen.GetComponent<Canvas> ();
		StartMenu = StartMenu.GetComponent<Canvas> ();
		started = started.GetComponent<Button> ();
		exited = exited.GetComponent<Button> ();

		quitmen.enabled = false; // изначально меню выхода скрыто
	}
	// хотим выйти?
	public void ExitPress()
	{
		quitmen.enabled = true; // нажали на выход - появилось меню выхода
		StartMenu.enabled=false; //мы не взаимодействуем с основным меню
	}
	// нет, не хотим выйти
	public void NoPress(){
		quitmen.enabled = false; // убираем меню выхода
		StartMenu.enabled=true; //высвечиваем основноеменю и делаем его доступным
	}
	//нажали на старт
	public void StartGame()
	{
		Application.LoadLevel("NewGame");// грузим основную игру (здесь номер сцены)
	}
	// если нажали на да, хотим выйти
	public void ExitGame()
	{
		Application.Quit (); //выходим
	}

	// Update is called once per frame
	void Update () {
	
	}
}
