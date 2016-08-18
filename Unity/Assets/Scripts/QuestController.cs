using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using Hellgate;

public class QuestData
{
    private int idx = 0;
    private string name = "";
    private int monster_count;
    private int monster_speed;

    public int Idx {
        get {
            return idx;
        }
    }

    public string Name {
        get {
            return name;
        }
    }
}

public class QuestController : SceneController
{
    [SerializeField]
    private GameObject temp;
    [SerializeField]
    private GameObject content;

    public override void OnSet (object data)
    {
        base.OnSet (data);

        List<object> listObj = data as List<object>;

        TextAsset textAsset = listObj.GetListObject<TextAsset> ();

        IList iList = (IList)Json.Deserialize (textAsset.text);
        QuestData[] questDatas = Reflection.Convert<QuestData> (iList);

        for (int i = 0; i < questDatas.Length; i++) {
            GameObject gObj = Instantiate (temp) as GameObject;
            gObj.transform.SetParent (content.transform);
            gObj.transform.localScale = Vector2.one;
            gObj.transform.localPosition = new Vector2 (0, i);

            gObj.GetComponentInChildren<Text> ().text = 
                questDatas [i].Idx + ". " + questDatas [i].Name;

            gObj.SetActive (true);
        }
    }
}
