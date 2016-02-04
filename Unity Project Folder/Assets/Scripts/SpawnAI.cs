using UnityEngine;
using System.Collections;

public class SpawnAI : MonoBehaviour {

    public GameObject[] waypts;
    public float coolDown;
    public bool roundStarted;
    private float lastSpawn;
    public Transform[] spawnPosition;

    public int percentScorp;
    public int percentMummy;
    private int total;

    public float roundTime;

    private float timeStarted;

    public Rigidbody2D[] monsters;
    private bool firstRound;

	// Use this for initialization
	void Start () {
        lastSpawn = Time.time;
        total = percentMummy + percentScorp;
        timeStarted = Time.time;
        firstRound = true;
        StartCoroutine(ResetRound());
        firstRound = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (roundStarted && Time.time - lastSpawn > coolDown)
        {
            Vector2 spawn = spawnPosition[Random.Range(0, spawnPosition.Length)].position;
            Rigidbody2D monster;
            int i = Random.Range(0, total + 1);
            if (i > percentScorp)
                monster = monsters[1];
            else
                monster = monsters[0];
            
            Rigidbody2D monsterR = (Rigidbody2D)Instantiate(monster, spawn, Quaternion.identity);
            monsterR.GetComponent<EnemyAI>().spawn = spawn;
            lastSpawn = Time.time;
        }
        else if(roundStarted == false)
        {
            timeStarted = Time.time;
        }

        if (roundStarted && Time.time - timeStarted > roundTime)
        {
            roundStarted = false;
            StartCoroutine(ResetRound());
        }


	}

    IEnumerator ResetRound()
    {   
        AudioSource audio = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioSource>();
        if (audio)
        {
            audio.Stop();
        }
        if (!(firstRound))
            gameObject.GetComponent<AudioSource>().Play();

        coolDown -= 0.1f;

        int towerCount = Random.Range(0, 5);
        int towers = 0;
        Debug.Log("Tower count: " + towerCount.ToString());
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            //Debug.Log("HERP");
            TowerClass t = g.GetComponent<TowerClass>();
            if (t)
            {
                if (towers <= towerCount)
                {
                    t.enableTower();
                    towers++;
                }
                else
                {
                    t.disableTower();
                }
            }
        }

        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(g);
        }
        yield return new WaitForSeconds(10);

        roundStarted = true;
        timeStarted = Time.time;
        if (audio)
            audio.Play();
    }    
}
