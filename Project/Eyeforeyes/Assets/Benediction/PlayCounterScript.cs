using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCounterScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = "あと " + ballScript.PlayCounter.ToString();
    }

    public void NumPlayCounter(int PlayCounter)
    {
        GetComponent<Text>().text = "あと " + ballScript.PlayCounter.ToString();
    }

}
