using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuizMgr : MonoBehaviour
{
    public static int AnswerNum;
    public static float Qwidth = 220.0f;
    public static float Qheight = 220.0f;
    public GameObject AnsButton1Object;
    public GameObject AnsButton2Object;
    public GameObject AnsButton3Object;
    public GameObject AnsButton4Object;
    public GameObject AnsButton5Object;
    public GameObject AnsButton6Object;
    int[] ImageCount = new int[8];


    //レベル選択用の変数
    int LevelNum;
    public static string LevelImageFolder;



    // Use this for initialization
    void Start () {


        StartCoroutine("SceneStartWait");

        if (Gamestart.Efe_Level == 0)
        {
            LevelNum = 4;
            LevelImageFolder = "panelImage0/panel_img0";
            //レベルに合わせたボタンを表示有効に
            AnsButton5Object.SetActive(true);
            AnsButton6Object.SetActive(true);
            AnswerButtonImageSet2();
            QuizImageSet2();
        }
        else if(Gamestart.Efe_Level == 1)
        {
            LevelNum = 8;
            LevelImageFolder = "panelImage1/panel_img0";
            //レベルに合わせたボタンを表示有効に
            AnsButton1Object.SetActive(true);
            AnsButton2Object.SetActive(true);
            AnsButton3Object.SetActive(true);
            AnsButton4Object.SetActive(true);
            AnswerButtonImageSet();
            QuizImageSet();
        }
        else
        {
            LevelNum = 8;
            LevelImageFolder = "panelImage2/panel_img0";
            //レベルに合わせたボタンを表示有効に
            AnsButton1Object.SetActive(true);
            AnsButton2Object.SetActive(true);
            AnsButton3Object.SetActive(true);
            AnsButton4Object.SetActive(true);
            AnswerButtonImageSet();
            QuizImageSet();
        }

        //おなじのどれだの文字をセット
        QuestionLabelSet();
    }


    private void QuestionLabelSet()
    {
        Text qLabel = GameObject.Find("QLabel").GetComponentInChildren<Text>();
     　 qLabel.text = "おなじのど～れだ？";

    }

    //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
    //選択ボタンが4つの場合
    //_________________________________________________________

    private void AnswerButtonImageSet()
    {

        //フォルダパスを配列に入れて、forで指定するように。
        string[] Folder = new string[4] { "AnsButton1/btImage", "AnsButton2/btImage", "AnsButton3/btImage", "AnsButton4/btImage" };

//        string[] array = new string[LevelNum];

        int SelectImage = Random.Range(0, LevelNum);

        //ImageCountの初期化

        for (int i = 0; i< LevelNum; i++)
        {
            ImageCount[i] = i;

//            Debug.Log(ImageCount[i]);

        }

        //ImageCountを並び変え。重複しないランダムな数字の配列を作成。

        for (int i = 0; i < LevelNum; i++)
        {
            SelectImage = Random.Range(0, 4);

            int temp = ImageCount[i];
            ImageCount[i] = ImageCount[SelectImage];
            ImageCount[SelectImage] = temp;

//            Debug.Log(ImageCount[i]);


        }

        for (int i = 0; i < 4; i++)
        {


            SpriteRenderer ansButtonImage = GameObject.Find(Folder[i]).GetComponent<SpriteRenderer>();
            
            Sprite loadingbuttonImage = Resources.Load<Sprite>(LevelImageFolder + ImageCount[i]);
            ansButtonImage.sprite = loadingbuttonImage;


            Text ansLabel = GameObject.Find("AnsButton" + (i + 1)).GetComponentInChildren<Text>();

            //重複しない数字をつかって、テキストに変換後。テキスト設定。
            ansLabel.text = ImageCount[i].ToString();


        }

    }

    private void QuizImageSet()
    {

        //レイアウト側で、オブジェクトにRectTransformのコンポーネントをaddしてから。いじれる。
        RectTransform sizeChange = GameObject.Find("panel_img00").GetComponent<RectTransform>();
        SpriteRenderer QuizImage = GameObject.Find("panel_img00").GetComponent<SpriteRenderer>();



        /*
                //画像の縦横サイズを取得してみるテスト
        Qwidth = QuizImage.bounds.size.x;
        Qheight = QuizImage.bounds.size.y;

                Debug.Log("Width/ " + width + "  ::  Height/ " + height );
        */

        //Vector2の値が、RectTransformのscaleの値になるよ。

        sizeChange.localScale = new Vector2(Qwidth, Qheight);


        int choice = Random.Range(0, 4);
        AnswerNum = ImageCount[choice];


        Sprite loadingImage = Resources.Load<Sprite>(LevelImageFolder + AnswerNum);
        QuizImage.sprite = loadingImage;
    }


    //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
    //選択ボタンが2つの場合
    //_________________________________________________________

    private void AnswerButtonImageSet2()
    {

        //フォルダパスを配列に入れて、forで指定するように。
        string[] Folder = new string[2] { "AnsButton5/btImage", "AnsButton6/btImage" };

//        string[] array = new string[LevelNum];

        int SelectImage = Random.Range(0, LevelNum);

        //ImageCountの初期化

        for (int i = 0; i < LevelNum; i++)
        {
            ImageCount[i] = i;

            //            Debug.Log(ImageCount[i]);

        }

        //ImageCountを並び変え。重複しないランダムな数字の配列を作成。

        for (int i = 0; i < LevelNum; i++)
        {
            SelectImage = Random.Range(0, 4);

            int temp = ImageCount[i];
            ImageCount[i] = ImageCount[SelectImage];
            ImageCount[SelectImage] = temp;

            //            Debug.Log(ImageCount[i]);


        }

        for (int i = 0; i < 2; i++)
        {


            SpriteRenderer ansButtonImage = GameObject.Find(Folder[i]).GetComponent<SpriteRenderer>();

            Sprite loadingbuttonImage = Resources.Load<Sprite>(LevelImageFolder + ImageCount[i]);
            ansButtonImage.sprite = loadingbuttonImage;

            //ボタンの番号指定を注意
            Text ansLabel = GameObject.Find("AnsButton" + (i + 5)).GetComponentInChildren<Text>();

            //重複しない数字をつかって、テキストに変換後。テキスト設定。
            ansLabel.text = ImageCount[i].ToString();

        }

    }

    private void QuizImageSet2()
    {

        //レイアウト側で、オブジェクトにRectTransformのコンポーネントをaddしてから。いじれる。
        RectTransform sizeChange = GameObject.Find("panel_img00").GetComponent<RectTransform>();
        SpriteRenderer QuizImage = GameObject.Find("panel_img00").GetComponent<SpriteRenderer>();

        //Vector2の値が、RectTransformのscaleの値になるよ。
        sizeChange.localScale = new Vector2(Qwidth, Qheight);

        int choice = Random.Range(0, 2);
        AnswerNum = ImageCount[choice];

        Sprite loadingImage = Resources.Load<Sprite>(LevelImageFolder + AnswerNum);
        QuizImage.sprite = loadingImage;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            // エスケープキー取得
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                FadeManager.Instance.LoadScene("title", 0.1f);
            }
        }
    }

    IEnumerator SceneStartWait()
    {
        //イベントシステムを無効にしてそれ以上ボタンが押せないように。
        var eventSystem = GameObject.FindObjectOfType<EventSystem>();
        eventSystem.enabled = false;

        yield return new WaitForSeconds(0.2f);

        eventSystem.enabled = true;

    }




}


