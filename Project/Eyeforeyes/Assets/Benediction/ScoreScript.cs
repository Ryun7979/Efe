using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    public static int Benescore = 0;

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = "SCORE:" + Benescore.ToString();
	}

	public void AddPoint(int point)
    {
        Benescore = Benescore + point;
        GetComponent<Text>().text = "SCORE:" + Benescore.ToString();
    }

}
