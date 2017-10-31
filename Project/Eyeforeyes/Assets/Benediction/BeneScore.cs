using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeneScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Text BallcountLabel = GameObject.Find("Ballcount").GetComponent<Text>();
        Text BeneScoreLabel = GameObject.Find("BeneScore").GetComponent<Text>();


        BallcountLabel.text = ballScript.vanishBallCount.ToString();
        BeneScoreLabel.text = ScoreScript.Benescore.ToString();

    }

}
