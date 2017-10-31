using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TitleSceneManeger : MonoBehaviour {


    public static float pagebookmark;
    private float pageWidth = 700;


    // 開始
    void Start () {

        //座標を計算ページブックマークから1850以上だったら、もう先はないので、1850にしてるけど、メニューの数が増えたらここを変えないとだよ。
        float destY = pagebookmark * pageWidth;
        if (destY > 1850)
        {
            destY = 1850;
        }
        Debug.Log("destY="+destY);

        RectTransform MenuPos = GameObject.Find("Content").GetComponent<RectTransform>();
        MenuPos.anchoredPosition = new Vector2(0, destY);


        //BGM再生
        SoundManager.Instance.PlayBGM(0);

        //シーン開始前に少し操作できない期間をおいて。
        StartCoroutine("SceneStartWait");

    }

    // 更新
    void Update () {
        // プラットフォームがアンドロイドかチェック
        if (Application.platform == RuntimePlatform.Android)
        {
            // エスケープキー取得
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // アプリケーション終了
                Application.Quit();
                return;
            }
        }

    }

    IEnumerator SceneStartWait()
    {
        //イベントシステムを無効にしてそれ以上ボタンが押せないように。
        var eventSystem = GameObject.FindObjectOfType<EventSystem>();
        eventSystem.enabled = false;

        yield return new WaitForSeconds(1.0f);

        eventSystem.enabled = true;

    }


}
