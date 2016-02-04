using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
    public float damage;
    
	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void FireProjectile(Vector2 v, Card c)
    {

    }

    void OnTriggerEnter2D(Collider2D co)
    {
        //Debug.Log("BAM");
        IAttackable i = co.gameObject.GetComponent<IAttackable>();
        if (i != null)
        {
            i.decrease(damage);
            Destroy(gameObject);
        }
    }
}
