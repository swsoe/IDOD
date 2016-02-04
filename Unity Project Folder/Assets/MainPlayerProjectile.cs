using UnityEngine;
using System.Collections;

public class MainPlayerProjectile : MonoBehaviour {

    public Rigidbody2D projectile;
    public Transform spawnpoint;
    public float speed;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        Animator anim = GetComponentInChildren<Animator>();
        if (Input.GetMouseButtonUp(0) && !(anim.GetBool("Upgrade")))
        {
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;
            Vector3 v = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
            x = v.x;
            y = v.y;
            fireProjectile(x, y);
        }
    }

    void fireProjectile(float x, float y)
    {
        Vector2 direction = new Vector3(x, y, 0) - this.gameObject.transform.position;
        direction.Normalize();
        Rigidbody2D p = Instantiate(projectile, new Vector3(0, 0, 0), Quaternion.identity) as Rigidbody2D;
        p.velocity = spawnpoint.TransformDirection(direction * speed);
    }
}
