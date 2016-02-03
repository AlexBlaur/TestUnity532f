using UnityEngine;
using System.Collections;

public class TrailSystemForMouse : MonoBehaviour {
	// система частиц для мыши
	public float distance=10;

	// Update is called once per frame
	void Update () {
		// объект следит за мышью и перемещается за ним. Он же делает милый шлейф при помощи Trail Renderer
		Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
		Vector2 pos = r.GetPoint (distance);
		transform.position = pos;	
	}
}
