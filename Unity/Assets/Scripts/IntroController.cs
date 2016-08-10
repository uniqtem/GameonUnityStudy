using UnityEngine;
using System.Collections;
using Hellgate;

public class IntroController : SceneController
{
    public override void OnSet (object data)
    {
        base.OnSet (data);

        SceneManager.Instance.Wait (3f, delegate() {
            SceneManager.Instance.Screen ("Login");
        });
    }
}
