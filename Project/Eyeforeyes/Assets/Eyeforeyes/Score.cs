using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Text scoreLabel = GameObject.Find("Score").GetComponent<Text>();
        Text rankLabel = GameObject.Find("Rank").GetComponent<Text>();

        int Score = JudgeMgr.GetScoreData();
        int Rank = JudgeMgr.GetRankData();

        //firebaseに結果を送ってみるテスト
        Firebase.Analytics.FirebaseAnalytics.LogEvent(
        "プレイ結果",
            new Firebase.Analytics.Parameter[] {
                new Firebase.Analytics.Parameter(
                "スコア", Score),
                new Firebase.Analytics.Parameter(
                "rank", Rank),
            }
        );


        scoreLabel.text = Score.ToString();
        rankLabel.text = Rank.ToString();

    }

}
