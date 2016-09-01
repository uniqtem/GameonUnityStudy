using UnityEngine;
using System.Collections;
using Hellgate;

public enum BattleStatus
{
    Play,
    Clear,
    Die
}

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
    public BattleStatus status;
    private QuestData data;

    public override void OnSet (object data)
    {
        base.OnSet (data);

        this.data = data as QuestData;

        spawns = spawn.GetComponentsInChildren<Transform> ();

        currentTime = 0;
        currentCount = 0;

        time = 1f;
        count = this.data.Count;

        status = BattleStatus.Play;
    }

    void Update ()
    {
        if (status != BattleStatus.Play) {
            return;
        }

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
                script.speed = (float)data.Speed;

                // last
                if (currentCount == count) {
                    SceneManager.Instance.Wait (10f, delegate() {
                        Clear ();
                    });
                }
            }
        }
    }

    public void Clear ()
    {
        status = BattleStatus.Clear;
        SceneManager.Instance.PopUp ("Clear !!", PopUpType.Ok, delegate(PopUpYNType type) {
            Register.SetInt ("Quest" + data.Idx, data.Idx);
            QuestController.Main ();
        });
    }

    public void Die ()
    {
        status = BattleStatus.Die;
        SceneManager.Instance.PopUp ("Die !!", PopUpType.Ok, delegate(PopUpYNType type) {
            QuestController.Main ();
        });
    }
}
