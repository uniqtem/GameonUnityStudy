using UnityEngine;
using System.Collections;

public class UserScript : MonoBehaviour
{
    public float speed = 10;
    private Vector3 pos;
    private bool inputFlag;
    private BattleController battle;

    void Start ()
    {
        battle = GameObject.FindObjectOfType<BattleController> ();
    }

    void Update ()
    {
        if (battle.status != BattleStatus.Play) {
            return;
        }

        if (Input.GetMouseButtonDown (0)) {
            inputFlag = true;
        }

        if (inputFlag == true) {
            pos = Input.mousePosition;
            pos = Camera.main.ScreenToWorldPoint (pos);
            pos.z = 0;
        }

        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards (transform.position,
                                                  pos,
                                                  step);

        if (Input.GetMouseButtonUp (0)) {
            inputFlag = false;
        }
    }

    void OnTriggerEnter (Collider other)
    {
        Debug.Log (other.name);
        battle.Die ();
    }
}
