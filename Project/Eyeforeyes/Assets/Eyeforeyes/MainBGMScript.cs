using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBGMScript : MonoBehaviour
{

    public bool DontDestroyEnabled = true;
 
    // Use this for initialization
    void Start()
    {
        AudioSource mainBGM1 = gameObject.GetComponent<AudioSource>();


        if (!mainBGM1.isPlaying)
        {
            mainBGM1.Play();
        }

/*
        if (DontDestroyEnabled)
        {
            // Sceneを遷移してもオブジェクトが消えないようにする
            DontDestroyOnLoad(this);

        }*/
    }

}
	
