using UnityEngine;
using System.Collections;
using System;

public class EnemyAI : MonoBehaviour, IAttackable {
    [HideInInspector]
    public GameObject[] waypoints;
    [HideInInspector]
    public Vector2 spawn;

    private GameObject currentTarget;

    private Vector2 currentWayPoint;
    private Vector2 lastWayPoint;

    private float lastWaypointSwitchTime;
    public float speed = 1f;

    public bool isLeft;

    public float damage;
    public float hp;
    public float attackCoolDown;
    public float pri;
    private float lastAttack;

    #region "Properties"
    public float priority
    {
        get
        {
            return pri;
        }
    }

    bool IAttackable.isLeft
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public bool available
    {
        get
        {
            return hp > 0;
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
    void Start () {
        lastWaypointSwitchTime = Time.time;
        lastAttack = Time.time;

        lastWayPoint = spawn;
        //Debug.Log(lastWayPoint);
        
    }
	
	// Update is called once per frame
	void Update () {
        getTarget();

        Vector3 startPos = lastWayPoint;
        Vector3 endPos = currentWayPoint;

        float pathLength = Vector3.Distance(startPos, endPos);
        float totalTimeForPath = pathLength / speed;
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;

        if (startPos != new Vector3(0, 0, 0) | endPos != new Vector3(0, 0, 0))
            gameObject.transform.position = Vector3.Lerp(startPos, endPos, currentTimeOnPath / totalTimeForPath);
        else
        {
            //Debug.Log("DERP");
            //Debug.Log(gameObject.transform.position);
            //Debug.Log(lastWayPoint);
            //Debug.Log(startPos);
            //Debug.Log(endPos);
        }

        if (gameObject.transform.position.Equals(endPos))
        {
            IAttackable i = currentTarget.GetComponent<IAttackable>();
            if (i != null)
                i.decrease(1);

        }

	}

    void getTarget()
    {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Waypoint"))
        {

            if (currentTarget == null)
            {
                currentTarget = g;
                lastWayPoint = gameObject.transform.position;
                currentWayPoint = g.transform.position;
                lastWaypointSwitchTime = Time.time;
                return;
            }
            else if ((currentTarget.GetComponent<IAttackable>().priority < g.GetComponent<IAttackable>().priority && g.GetComponent<IAttackable>().available) || (!(currentTarget.GetComponent<IAttackable>().available)))
            {
                currentTarget = g;
                lastWayPoint = gameObject.transform.position;
                currentWayPoint = g.transform.position;
                lastWaypointSwitchTime = Time.time;
            }
        }
    }

    void attack(IAttackable i)
    {
        if (i != null)
        {
            if (Time.time - lastAttack > attackCoolDown)
                i.decrease(damage);
        }
    }

    public void decrease(float i)
    {
        hp -= i;
        if (hp <= 0)
            Destroy(gameObject);
    }

    public void increase(float i)
    {
        hp += i;
    }
}
