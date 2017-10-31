using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class Judge : MonoBehaviour {

    //サウンド用変数
    public AudioClip buttonSE;


    public void JudgeAnswer()
    {

        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot (buttonSE);

        string answerText = QuizMgr.AnswerNum.ToString();


        Text selectedBtn = this.GetComponentInChildren<Text>();

        if (selectedBtn.text == answerText)
        {
            JudgeMgr.SetJudgeData("せいかい");
            StartCoroutine("ResultSceneChange");
            //            Application.LoadLevel("Result");
        }
        else
        {
            JudgeMgr.SetJudgeData("まちがい");
            StartCoroutine("ResultSceneChange");
            //            Application.LoadLevel("Result");
        }



    }


    IEnumerator ResultSceneChange()
    {
        //イベントシステムを無効にしてそれ以上ボタンが押せないように。
        var eventSystem = GameObject.FindObjectOfType<EventSystem>();
        eventSystem.enabled = false;

        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("Result");
    }



}
