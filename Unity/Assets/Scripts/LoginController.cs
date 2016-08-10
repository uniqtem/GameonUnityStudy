using UnityEngine;
using System.Collections;
using Hellgate;

public class LoginController : SceneController
{
    private AssetBundleInitialDownloader downloader;

    public override void OnSet (object data)
    {
        base.OnSet (data);

        downloader = new AssetBundleInitialDownloader (
            "https://dl.dropboxusercontent.com/u/95277951/gameon/resource.json",
            "https://dl.dropboxusercontent.com/u/95277951/gameon/" + Util.GetDevice ());

        downloader.aEvent = Callback;
        downloader.Download ();
    }

    private void Callback (AssetBundleInitalStatus status)
    {
        switch (status) {
        case AssetBundleInitalStatus.Start:
            Debug.Log ("Start");
        break;
//        case AssetBundleInitalStatus.HttpError:
        case AssetBundleInitalStatus.HttpTimeover:
            Debug.Log ("Error");
        break;
        case AssetBundleInitalStatus.DownloadOver:
        case AssetBundleInitalStatus.Over:
            Debug.Log ("Over");
        break;
        default:
        break;
        }
    }
}
