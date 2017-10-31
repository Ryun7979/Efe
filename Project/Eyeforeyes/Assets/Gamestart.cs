using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Gamestart : MonoBehaviour {

    public static int qCount;
    public static int Efe_Level;


    //パネルあわせ側のスタート
    public void Efe_Easy_NextScene()
    {
        if(SceneManager.GetActiveScene().name == "title")
        {
            //かんたんを選択されたことを設定
            Efe_Level = 0;
            //遷移時のメニューページをおぼえておく。
            TitleSceneManeger.pagebookmark = 0;
            StartCoroutine("TitleSceneChange");
        }
    }

    public void Efe_Nomal_NextScene()
    {
        if (SceneManager.GetActiveScene().name == "title")
        {
            //ふつうを選択されたことを設定
            Efe_Level = 1;
            //遷移時のメニューページをおぼえておく。
            TitleSceneManeger.pagebookmark = 1;
            StartCoroutine("TitleSceneChange");
        }
    }


    public void Efe_Hard_NextScene()
    {
        if (SceneManager.GetActiveScene().name == "title")
        {
            //むづかしいを選択されたことを設定
            Efe_Level = 2;
            //遷移時のメニューページをおぼえておく。
            TitleSceneManeger.pagebookmark = 2;
            StartCoroutine("TitleSceneChange");
        }
    }


    //ドロップパズル側のスタート
    public void Bene_NextScene()
    {

        if (SceneManager.GetActiveScene().name == "title")
        {
            //遷移時のメニューページをおぼえておく。
            TitleSceneManeger.pagebookmark = 3;
            StartCoroutine("BenedictionStartButton");
        }

    }


    //サウンドボタンのスタート
    public void Aether_NextScene()
    {

        if (SceneManager.GetActiveScene().name == "title")
        {
            //遷移時のメニューページをおぼえておく。
            TitleSceneManeger.pagebookmark = 4;
            StartCoroutine("AetherflowStartButton");
        }

    }




    public void Quit()
    {
        if (SceneManager.GetActiveScene().name == "Score")
        {
            JudgeMgr.g_scoreData = 0;
            JudgeMgr.g_RankData = 0;
            JudgeMgr.Ranktmp = 0;


            QuizMgr.Qwidth = 220.0f;
            QuizMgr.Qheight = 220.0f;

            //イベントシステムを無効にしてそれ以上ボタンが押せないように。
            var eventSystem = GameObject.FindObjectOfType<EventSystem>();
            eventSystem.enabled = false;

            FadeManager.Instance.LoadScene("title", 1.0f);
        }

        if (SceneManager.GetActiveScene().name == "BeneScore")
        {
            ballScript.vanishBallCount = 0;
            ScoreScript.Benescore = 0;


            //イベントシステムを無効にしてそれ以上ボタンが押せないように。
            var eventSystem = GameObject.FindObjectOfType<EventSystem>();
            eventSystem.enabled = false;

            FadeManager.Instance.LoadScene("title", 1.0f);
        }

        if (SceneManager.GetActiveScene().name == "Afmain")
        {
            //イベントシステムを無効にしてそれ以上ボタンが押せないように。
            var eventSystem = GameObject.FindObjectOfType<EventSystem>();
            eventSystem.enabled = false;

            FadeManager.Instance.LoadScene("title", 1.0f);
        }


    }

    public void Continue()
    {
        if (SceneManager.GetActiveScene().name == "Score")
        {
            JudgeMgr.g_scoreData = 0;
            JudgeMgr.g_RankData = 0;
            JudgeMgr.Ranktmp = 0;

            QuizMgr.Qwidth = 220.0f;
            QuizMgr.Qheight = 220.0f;

            //イベントシステムを無効にしてそれ以上ボタンが押せないように。
            var eventSystem = GameObject.FindObjectOfType<EventSystem>();
            eventSystem.enabled = false;

            FadeManager.Instance.LoadScene("main", 1.0f);
        }

        if (SceneManager.GetActiveScene().name == "BeneScore")
        {
            ballScript.vanishBallCount = 0;
            ScoreScript.Benescore = 0;

            //イベントシステムを無効にしてそれ以上ボタンが押せないように。
            var eventSystem = GameObject.FindObjectOfType<EventSystem>();
            eventSystem.enabled = false;

            FadeManager.Instance.LoadScene("droppuzzle", 1.0f);
        }

    }



    IEnumerator TitleSceneChange()
    {
        //イベントシステムを無効にしてそれ以上ボタンが押せないように。
        var eventSystem = GameObject.FindObjectOfType<EventSystem>();
        eventSystem.enabled = false;

        SoundManager.Instance.PlaySE(0);
        // 5秒間待つ
        yield return new WaitForSeconds(0.5f);

        FadeManager.Instance.LoadScene("main", 0.1f);
    }

    IEnumerator BenedictionStartButton()
    {
        //イベントシステムを無効にしてそれ以上ボタンが押せないように。
        var eventSystem = GameObject.FindObjectOfType<EventSystem>();
        eventSystem.enabled = false;

        SoundManager.Instance.PlaySE(0);
        // 5秒間待つ
        yield return new WaitForSeconds(0.5f);

        FadeManager.Instance.LoadScene("droppuzzle", 0.1f);
    }


    IEnumerator AetherflowStartButton()
    {
        //イベントシステムを無効にしてそれ以上ボタンが押せないように。
        var eventSystem = GameObject.FindObjectOfType<EventSystem>();
        eventSystem.enabled = false;

        SoundManager.Instance.PlaySE(0);
        // 5秒間待つ
        yield return new WaitForSeconds(0.5f);

        FadeManager.Instance.LoadScene("Afmain", 0.1f);
    }



    //Eyeforeyesで、結果画面からの判定処理
    public void NextQuiz()
    {
        if (SceneManager.GetActiveScene().name == "Result")
        {
            StartCoroutine("NextGameChange");

        }

    }


    IEnumerator NextGameChange()
    {

        //イベントシステムを無効にしてそれ以上ボタンが押せないように。
        var eventSystem = GameObject.FindObjectOfType<EventSystem>();
        eventSystem.enabled = false;


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





}


