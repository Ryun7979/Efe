using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeScript : MonoBehaviour {


    public ballScript BallScript;

    public void Exchange()
    {
        //配列に「Respawn」タグの付いているオブジェクトをすべて格納
        GameObject[] piyos = GameObject.FindGameObjectsWithTag("Respawn");
        //すべて取り出して消す
        foreach(GameObject obs in piyos)
        {
            Destroy(obs);
        }
        //ballScriptのDropBallを使って50個作成
        BallScript.SendMessage("DropBall", 40);
    }
}
