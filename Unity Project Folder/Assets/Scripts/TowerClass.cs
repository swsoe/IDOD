using UnityEngine;
using System.Collections;
using System;

public class TowerClass : MonoBehaviour, IAttackable
{

    public bool towerDestroyed;
    [HideInInspector]
    public Card card = null;
    public bool isLeft;
    public float hp;
    public float cooldown;
    private float lastfire;
    public float speed;

    public Rigidbody2D projectile;

    #region "Properties"
    public float priority
    {
        get
        {
            return 1f;
        }
    }

    bool IAttackable.isLeft
    {
        get
        {
            return false;
        }
    }

    public bool available
    {
        get
        {
            return !(towerDestroyed);
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
    #endregion

    // Use this for initialization
    void Start ()
    {
        lastfire = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!(towerDestroyed))
        {
            fire();
            Animator anim = gameObject.GetComponentInChildren<Animator>(true);
            anim.SetBool("isDead", false);
            anim.SetBool("isLeft", false);
            anim.SetBool("isRight", false);
            anim.SetBool("isFront", true);
        }
	}

    public void decrease(float i)
    {
        hp -= i;
        if (hp <= 0)
        {
            towerDestroyed = true;
            //TODO: Switch to destroyed sprite or play animation
            Animator anim = gameObject.GetComponentInChildren<Animator>(true);
            if (anim)
            {
                anim.SetBool("isDead", true);
                anim.SetBool("isLeft", false);
                anim.SetBool("isRight", false);
                anim.SetBool("isFront", false);
            }
            else
            {
                //Debug.Log(gameObject.name);
            }
            
        }
    }

    public void increase(float i)
    {
        hp += i;
    }

    void fire()
    {
        if (Time.time - lastfire > cooldown)
        {
            GameObject target = getTarget();
            if (target)
            {
                Vector2 direction = target.transform.position - this.gameObject.transform.position;
                direction.Normalize();
                Rigidbody2D p = Instantiate(projectile, this.gameObject.transform.position, Quaternion.identity) as Rigidbody2D;
                p.velocity = this.gameObject.transform.TransformDirection(direction * speed);
                lastfire = Time.time;
            }

        }
    }

    GameObject getTarget()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            return g;
        }
        return null;
    }

    public void enableTower()
    {
        towerDestroyed = false;
        hp = 100;
        Animator anim = gameObject.GetComponentInChildren<Animator>(true);
        anim.SetBool("isDead", false);
        anim.SetBool("isLeft", false);
        anim.SetBool("isRight", false);
        anim.SetBool("isFront", true);
    }

    public void disableTower()
    {
        towerDestroyed = true;
        hp = 0;
        Animator anim = gameObject.GetComponentInChildren<Animator>(true);
        anim.SetBool("isDead", true);
        anim.SetBool("isLeft", false);
        anim.SetBool("isRight", false);
        anim.SetBool("isFront", false);
    }
}
