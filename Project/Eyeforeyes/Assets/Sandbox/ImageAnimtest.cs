using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageAnimtest : MonoBehaviour {


    GameObject GMpref;

    // Use this for initialization
    void Start () {

        GMpref = GameObject.Find("Canvas/Image");

        iTween.PunchScale(GMpref, iTween.Hash(
            "x", 3,
            "y", 3,
            "delay", 1,
            "time", 3.0f)
        );
    }

    // Update is called once per frame
    void Update () {
    }
}
