using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class JudgeMgr : MonoBehaviour {
    public static string g_JudgeData;
    public static int g_scoreData;
    public static int g_RankData;
    public static int Ranktmp;


    //サウンド用の入れ物を○と×つくる。コンポーネント側で、サウンドを当てはめる。
    public AudioClip Audiomaru;
    public AudioClip Audiobatu;
    public AudioClip TaptoNext;


    private void Start()
    {

 //       Debug.Log(g_JudgeData);

        BeforImageSet();

        //audioを初期化
        AudioSource audio = GetComponent<AudioSource>();


        if (g_JudgeData == "まちがい")
        {
            //いま置いてある画像を取得
            SpriteRenderer judgeImage = GameObject.Find("JudgeImage").GetComponent<SpriteRenderer>();

            Sprite loadingImage = Resources.Load<Sprite>("batu");

            judgeImage.sprite = loadingImage;

            Text judgeLabel = GameObject.Find("JudgeLabel").GetComponent<Text>();
            judgeLabel.text = "まちがい";

            //audioを使ってワンショット再生
            audio.PlayOneShot(Audiobatu);

            if (Ranktmp > 0)
            {

                //不正解なら出題画像が少し大きくなる。
            QuizMgr.Qwidth = QuizMgr.Qwidth + 20f;
            QuizMgr.Qheight = QuizMgr.Qheight + 20f;

            //ランクデータは間違えたらマイナス。
                Ranktmp--;
            }

//            Debug.Log("ランクテンポラリ："+Ranktmp);


        }
        else if(g_JudgeData =="せいかい"){

            //audioを使ってワンショット再生
            audio.PlayOneShot(Audiomaru);


            //正解したら、出題画像のサイズが小さくなる。
            QuizMgr.Qwidth = QuizMgr.Qwidth - 20f;
            QuizMgr.Qheight = QuizMgr.Qheight - 20f;

            //スコアとランクをプラス。
            g_scoreData++;
            Ranktmp++;

//            Debug.Log("ランクテンポラリ：" + Ranktmp);


            if (Ranktmp > g_RankData)
            {
                g_RankData = Ranktmp;
//                Debug.Log("ランク：" + g_RankData);
            }
        }
    }

    public static void SetJudgeData(string judgeData)
    {
        g_JudgeData = judgeData;
    }

    public static int GetScoreData()
    {
        return g_scoreData;
    }


    public static int GetRankData()
    {
        return g_RankData;
    }

    private void BeforImageSet()
    {

        SpriteRenderer QuizImage = GameObject.Find("panel_img00").GetComponent<SpriteRenderer>();

        Sprite loadingImage = Resources.Load<Sprite>(QuizMgr.LevelImageFolder + QuizMgr.AnswerNum);
        QuizImage.sprite = loadingImage;
    }

    void Update()
    {

/*
        if (Input.GetMouseButtonDown(0))
        {
            //イベントシステムを無効にしてそれ以上ボタンが押せないように。
            var eventSystem = GameObject.FindObjectOfType<EventSystem>();
            eventSystem.enabled = false;

            AudioSource audio = GetComponent<AudioSource>();

            //audioを使ってワンショット再生
            audio.PlayOneShot(TaptoNext);

            StartCoroutine("NextGameChange");

        }
*/

    }
    /*
    IEnumerator NextGameChange()
    {
        // 5秒間待つ
        yield return new WaitForSeconds(0.5f);


        if (Gamestart.qCount < 9)
        {
            Gamestart.qCount++;
            FadeManager.Instance.LoadScene("main", 0.1f);
        }
        else
        {
            Gamestart.qCount = 0;
            FadeManager.Instance.LoadScene("Score", 0.5f);
        }

    }
*/


}
