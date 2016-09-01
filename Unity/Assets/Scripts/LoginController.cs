using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Hellgate;

public class LoginController : SceneController
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private Scrollbar scrollbar;
    [SerializeField]
    private Button button;

    private AssetBundleInitialDownloader downloader;
    private AssetBundleInitalStatus mStatus;

    public override void OnSet (object data)
    {
        base.OnSet (data);

        text.gameObject.SetActive (false);
        scrollbar.gameObject.SetActive (false);
        button.gameObject.SetActive (false);

        downloader = new AssetBundleInitialDownloader (
            "https://dl.dropboxusercontent.com/u/95277951/gameon/resource.json",
            "https://dl.dropboxusercontent.com/u/95277951/gameon/" + Util.GetDevice ());

        downloader.aEvent = Callback;
        downloader.Download ();

        button.onClick.AddListener (delegate() {
            Debug.Log ("OnClick");
            QuestController.Main ();
        });
    }

    void Update ()
    {
        if (mStatus == AssetBundleInitalStatus.Start) {
            text.text = downloader.CurretIndex + " / " +
            downloader.DownloadCount;
            scrollbar.size = downloader.Progress;
        }
    }

    private void Callback (AssetBundleInitalStatus status)
    {
        mStatus = status;

        switch (status) {
        case AssetBundleInitalStatus.Start:
            Debug.Log ("Start");

            text.gameObject.SetActive (true);
            scrollbar.gameObject.SetActive (true);
        break;
        case AssetBundleInitalStatus.HttpError:
        case AssetBundleInitalStatus.HttpTimeover:
            Debug.Log ("Error");

            SceneManager.Instance.PopUp ("Error " + status,
                                         PopUpType.Ok,
                                         delegate(PopUpYNType type) {
                SceneManager.Instance.Screen ("Intro");
            });
        break;
        case AssetBundleInitalStatus.DownloadOver:
        case AssetBundleInitalStatus.Over:
            Debug.Log ("Over");

            text.gameObject.SetActive (false);
            scrollbar.gameObject.SetActive (false);
            button.gameObject.SetActive (true);
        break;
        default:
        break;
        }
    }
}
