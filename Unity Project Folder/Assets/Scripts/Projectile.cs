using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public float damage;

	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update()
    {
	    
	}

    void FixedUpdate()
    {
        if (!(this.gameObject.GetComponent<Renderer>().isVisible))
        {
            Destroy(this.gameObject);
        }
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
