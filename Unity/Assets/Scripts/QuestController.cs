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

    public int Count {
        get {
            return monster_count;
        }
    }

    public int Speed {
        get {
            return monster_speed;
        }
    }
}

public class QuestController : SceneController
{
    [SerializeField]
    private GameObject temp;
    [SerializeField]
    private GameObject content;
    private QuestData[] questDatas;

    public static void Main ()
    {
        AssetBundleData assetBundle = new AssetBundleData ("master");
        assetBundle.type = typeof(TextAsset);
        assetBundle.objName = "quest";

        LoadingJobData jobData = new LoadingJobData ("Quest");
        jobData.assetBundles.Add (assetBundle);

        SceneManager.Instance.LoadingJob (jobData);
    }

    public override void OnSet (object data)
    {
        base.OnSet (data);

        List<object> listObj = data as List<object>;

        TextAsset textAsset = listObj.GetListObject<TextAsset> ();

        IList iList = (IList)Json.Deserialize (textAsset.text);
        questDatas = Reflection.Convert<QuestData> (iList);



        for (int i = 0; i < questDatas.Length; i++) {
            int idx = Register.GetInt ("Quest" + questDatas [i].Idx, 0);
            if (questDatas [i].Idx != idx) {
                continue;
            }

            GameObject gObj = Instantiate (temp) as GameObject;
            gObj.transform.SetParent (content.transform);
            gObj.transform.localScale = Vector2.one;
            gObj.transform.localPosition = new Vector2 (0, i);

            gObj.name = questDatas [i].Idx.ToString ();
            gObj.GetComponentInChildren<Text> ().text = 
                questDatas [i].Idx + ". " + questDatas [i].Name;

            gObj.SetActive (true);
        }
    }

    public void OnClick (GameObject gObj)
    {
        int idx = int.Parse (gObj.name);
        for (int i = 0; i < questDatas.Length; i++) {
            if (questDatas [i].Idx == idx) {
                SceneManager.Instance.PopUp ("Really ??", PopUpType.YesAndNo, delegate(PopUpYNType type) {
                    if (type == PopUpYNType.Yes) {
                        SceneManager.Instance.Screen ("Battle", questDatas [i]);
                    }
                });

                return;
            }
        }
    }
}
