using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour, IAttackable {
    public float hp;

    public bool available
    {
        get
        {
            return true;
        }
    }

    public float health
    {
        get
        {
            return hp;
        }

        set
        {
            hp = health;
        }
    }

    public bool isLeft
    {
        get
        {
            return false;
        }
    }

    public float priority
    {
        get
        {
            return 0f;
        }
    }

    public void decrease(float i)
    {
        hp -= i;
        if (hp <= 0)
        {
            GameObject[] g = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject go in g)
            {
                go.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
            Destroy(gameObject);
            Application.Quit();
        }
    }

    public void increase(float i)
    {
        hp += i;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
