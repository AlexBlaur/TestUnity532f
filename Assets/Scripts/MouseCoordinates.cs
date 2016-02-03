using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MouseCoordinates : MonoBehaviour {

	public float delta=0.1f; // погрешность при проверке
	int index; // индекс выбираемого элемента
	public int result=0; //результат игры
	public ArrayList MouseCoord=new ArrayList(); // инициализируем массив координат
	public TrailRenderer MyWay; //объект, который организует путь за мышью

	public ArrayList Array=new ArrayList(); //массив шаблонов для использования
	// картинки для шаблонов
	public Sprite[] ArraySprite;

	// куда вывести картинку шаблона
	public Image source; 
	// информация об игроке
	public ScoreOfGamer score;

	//отладочная информация
	public Text t1;
	public Text t2;
	// Use this for initialization
	void Start () {
		MyWay = MyWay.GetComponent<TrailRenderer> (); //даем скрипту управление над объектом пути
		MyWay.enabled=false;

		score = score.GetComponent<ScoreOfGamer> ();//получаем ссылку на счет игрока

		//создаем массив шаблонов
		CreateShablon (); 
		index = Random.Range (0, Array.Count); //рандомно выбираем шаблон от минимального до количества в массиве
		//index=1;
		Shablon t= (Shablon)Array[index]; 
		source.sprite=t.Shab; //назначаем шаблон

		/*t1.text = "";
		t2.text = "";*/


	}

	// Update is called once per frame
	void Update () {
		// если мышь нажата и зажата
		if (Input.GetKey (KeyCode.Mouse0)) {
			MouseCoord.Add (Input.mousePosition); // записываем в массив координаты
			MyWay.enabled=true; //включаем путь
		}

		if (Input.GetKeyUp (KeyCode.Mouse0))
		{
			MyWay.enabled=false; //отключаем путь
			// нормализация массива
			Normalize();
			//проверка его соответствия шаблону
			result=Simile();
			if (result == 1) {
				score.Level=score.Level+1;
				score.Score = score.Score + 1;
				score.MaxTime = score.MaxTime - 5; // каждый раз времени меньше на 1 секунду
				score.CurTime = score.MaxTime; //перезапускаем время
				result=0;
				// выбрать новый шаблон
				index = Random.Range (0, Array.Count); //рандомно выбираем шаблон от минимального до количества в массиве
				//index=1;
				Shablon t= (Shablon)Array[index]; 
				source.sprite=t.Shab;
			}
			// код проверки. что у нас в массиве. работает по отпуску мыши
			/*for (int i = 0; i < MouseCoord.Count; i++) {
				Vector3 pos = (Vector3)MouseCoord [i];
				print ("x="+pos.x+" y="+pos.y);
			}*/
			// очистка массива
			MouseCoord.Clear();
		}
	
	}

	//нормализация полученных координат (приведение их к отрезку [0,1])
	void Normalize()
	{
		//удалим одинаковые (ибо зачем?)
		int j=1;
		while (j<MouseCoord.Count){
			Vector3 t1=(Vector3)MouseCoord [j];
			Vector3 t2=(Vector3)MouseCoord [j - 1];
			if (t1.x == t2.x && t1.y == t2.y) {
				MouseCoord.RemoveAt (j);		
			} else {
				j++;
			}
		}
		//проверочная печать
		/*for (int i = 0; i < MouseCoord.Count; i++) {
			Vector3 pos = (Vector3)MouseCoord [i];
			t2.text+=("x="+pos.x+" y="+pos.y+"\n").ToString();
		}*/

		// суть нормализации в привидении к диапазону от 0 до 1. Минимальное значение =0, максимальное значение =1 в нормализованном массиве
		float minX, minY, maxX, maxY;
		Vector3 cord = (Vector3)MouseCoord [0];
		//классика - приводим максимальное и минимальное значение к первому элементу массива. Так убеждаемся, что минимальноеи максимальное значения точно попадут в диапазон массива
		minX=cord.x;
		minY=cord.y;
		maxX=cord.x;
		maxY=cord.y;

		//вычисляем реальные минимумы и максимумы массива
		foreach(Vector3 t in MouseCoord)
		{
			if (t.x <= minX)
				minX = t.x;
			if (t.x >= maxX)
				maxX = t.x;
			if (t.y <= minY)
				minY = t.y;
			if (t.y >= maxY)
				maxY = t.y;
		}
		// но ведь самая малая координата - не всегда 0. Чтобы получить нули - вычитаем самую малую координатуиз всех значений.
		maxX=maxX-minX;
		maxY = maxY - minY;
		//заодно приведем к единице
		for (int i = 0; i < MouseCoord.Count; i++)
		{
			Vector3 pos = (Vector3)MouseCoord [i];
			pos.x = (float)System.Math.Round(((pos.x - minX)/maxX),2);
			pos.y = (float)System.Math.Round(((pos.y - minY)/maxY),2);
			MouseCoord [i] = new Vector3 (pos.x, pos.y,0);
		}
		//проверка

		/*for (int i = 0; i < MouseCoord.Count; i++) {
			Vector3 pos = (Vector3)MouseCoord [i];
			t2.text+=("x="+pos.x+" y="+pos.y+"\n").ToString();
		}*/	
	}

	//проверка (сравнение шаблрна со значениями)
	public int Simile() 
	{
		t1.text = "";
		ArrayList flags=new ArrayList();

		Shablon t= (Shablon)Array[index]; 
		int tempf = 0; // соответствие координате найдено
		foreach (Vector3 key in t.Keys) {
			foreach (Vector3 pos in MouseCoord) {
				if ((key.x <= (pos.x + delta)) && (key.x >= (pos.x - delta))) { //ключ входит в допуск координат
					if ((key.y <= (pos.y + delta)) && (key.y >= (pos.y - delta))) {
						flags.Add (1); //добавляем флаг соответствия
						//t1.text+=(key.x+" "+key.y+"\n").ToString();
						tempf = 1; //знак найден
						break; //проверку координат можно прервать
					}
				}
			}
			if (tempf == 1) { //сбрасывание маркера
				tempf = 0;
				continue;
			}
		}
				
		/*for (int i = 0; i < flags.Count; i++) {
			t1.text+=(flags[i]+"\n").ToString();
		}*/

		if (flags.Count != t.Keys.Count)
			return 0;//координата не подошла к шаблону 
		else
			return 1; //кол-во флагов совпадает с количеством ключей
				
	}

	// создание массива шаблонов
	public void CreateShablon()
	{
		Shablon temp=new Shablon();
		//1 шаблон - треугольник
		temp.Shab = ArraySprite[0]; //задать картинку
		temp.Keys.Add (new Vector3 (0.5f, 1f, 0)); //задать точки (y по низу!! х по высоте)
		temp.Keys.Add (new Vector3 (0, 0, 0));
		temp.Keys.Add (new Vector3 (1, 0, 0));
		temp.Keys.Add (new Vector3 (0.3f, 0.5f, 0));
		temp.Keys.Add (new Vector3 (0.8f, 0.5f, 0));
		Array.Add (temp);//добавим в массив

		//2 шаблон - следующая загогулина
		temp=new Shablon();
		temp.Shab = ArraySprite[1]; //задать картинку
		temp.Keys.Add (new Vector3 (0, 1, 0)); //задать точки
		temp.Keys.Add (new Vector3 (1, 1, 0)); //задать точки
		temp.Keys.Add (new Vector3 (0.5f, 0, 0)); //задать точки
		Array.Add (temp);//добавим в массив

		//3 шаблон
		temp=new Shablon();
		temp.Shab = ArraySprite[2]; //задать картинку
		temp.Keys.Add (new Vector3 (0.5f, 1, 0)); //задать точки
		temp.Keys.Add (new Vector3 (0.5f, 0, 0)); //задать точки
		temp.Keys.Add (new Vector3 (0, 0.5f, 0)); //задать точки
		temp.Keys.Add (new Vector3 (1, 0.5f, 0)); //задать точки
		Array.Add (temp);//добавим в массив

				
	}

}
