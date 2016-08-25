using UnityEngine;
using System.Collections;
using Hellgate;

public class BattleController : SceneController
{
    [SerializeField]
    private GameObject user;
    [SerializeField]
    private GameObject monster;
    [SerializeField]
    private GameObject spawn;
    private Transform[] spawns;
    private float time;
    private float currentTime;
    private int count;
    private int currentCount;

    public override void OnSet (object data)
    {
        base.OnSet (data);
    }

    public override void Start ()
    {
        base.Start ();

        spawns = spawn.GetComponentsInChildren<Transform> ();

        currentTime = 0;
        currentCount = 0;

        time = 1f;
        count = 10;
    }

    void Update ()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= time) {
            currentTime = 0;

            if (currentCount < count) {
                currentCount++;

                int random = Random.Range (1, spawns.Length);

                GameObject gObj = Instantiate (monster) as GameObject;
                gObj.transform.position = spawns [random].position;

                MonsterScript script = gObj.GetComponent<MonsterScript> ();
                script.target = 
                    user.transform.position - gObj.transform.position;
//                script.speed = 10f;
            }
        }
    }
}
