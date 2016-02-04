using UnityEngine;
using System.Collections;

public class AnimationControl : MonoBehaviour {
    public float scale;

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

		if(Input.mousePosition.x <= (Screen.width / 2))
		{
			anim.SetBool("OnLeft",true);
			transform.localScale = new Vector3(-1f * scale,scale,1f); //(c# code)
		}
		else
		{
			anim.SetBool("OnLeft",false);
			transform.localScale = new Vector3(1f * scale,scale,1f); //(c# code)
		}

		//Firing Animation
		if(Input.GetMouseButtonDown(0))
		{
			anim.SetBool("Fire", true);
		}
		if(Input.GetMouseButton(0))
		{
			anim.SetBool("Fire", true);
			anim.SetBool("Held Down", true);
		}
		if(Input.GetMouseButtonUp(0))
		{
			anim.SetBool("Fire", false);
			anim.SetBool("Held Down", false);
		}

		//Round Wait Animation

		GameObject[] a = GameObject.FindGameObjectsWithTag("Enemy");
		if( a.Length == 0)
			{
				anim.SetBool("Upgrade", true);
			}
			else
			{
				anim.SetBool("Upgrade", false);
			}
		}

}
