using UnityEngine;
using System.Collections;

public class AIDirection : MonoBehaviour {
	public float posX = 0.0f;
	public float posY = 0.0f;
	public float posZ = 0.0f;

	private int ScreenWidth;
	private Transform target;
	Camera camera;

	void Start() {
		camera = Camera.main;
		ScreenWidth = Screen.width / 2;
		target = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 screenPos = camera.WorldToScreenPoint(target.position);

		if(screenPos.x <= (ScreenWidth))
		{
			transform.localScale = new Vector3(-posX,posY,posZ); //(c# code)
		}
		else
		{
			transform.localScale = new Vector3(posX,posY,posZ); //(c# code)
		}

	}
}
