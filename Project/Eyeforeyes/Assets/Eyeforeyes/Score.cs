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

        scoreLabel.text = Score.ToString();
        rankLabel.text = Rank.ToString();

    }

}
