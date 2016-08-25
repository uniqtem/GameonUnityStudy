using UnityEngine;
using System.Collections;

public class MonsterScript : MonoBehaviour
{
    public Vector3 target;
    public float speed = 10;

    void Update ()
    {
        if (target == Vector3.zero) {
            return;
        }

        transform.forward = target;
        transform.Translate (Vector3.forward * Time.deltaTime * speed);
    }
}
