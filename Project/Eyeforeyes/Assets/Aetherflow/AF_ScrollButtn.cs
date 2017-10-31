using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AF_ScrollButtn : MonoBehaviour {

    [SerializeField]
    private GameObject AF_btn;  //ボタンプレハブ


    //ボタン数設定
    const int BUTTON_COUNT = 69;

    public int WinNumber;

    //ボタンのサイズ変更用の変数と、いくつおきにサイズを変えるか。
    float Pho_width = 1.0f;
    float Pho_height = 1.0f;
    public int Size_num = 5; //いくつ消すかは、コンポーネントの所で入力するべき。

    string[] AF_buttonLabel = new string[69] {
        "あー",
        "だーだ",
        "あはは",
        "じゃー",
        "じゃーん",
        "ぴーぽー",
        "ちゅどーん",
        "かしゃっ",
        "ぱしゃ",
        "ぴゅ",
        "にゃーん",
        "ぴゅーっぴゅーっ",
        "ぴんぽんぴんぽん",
        "ぴんぽーん",
        "かぁー",
        "かかっ",
        "ぱぁっ",
        "ぴしゅ",
        "ぴひょ",
        "ぴろりらん",
        "ぴろるん",
        "ぴしゅっ",
        "わん",
        "どぅん",
        "ぷぁーぁ",
        "しゅぴ～ん",
        "ぴゅ～ん",
        "めぇぇ～",
        "ほーほけきょ",
        "ぱからっ",
        "ぜろっ",
        "いちっ",
        "にっ",
        "さん",
        "よんっ",
        "ごっ",
        "ろくっ",
        "ななっ",
        "はちっ",
        "きゅー",
        "じゅぅー",
        "はじまるよー",
        "ぴんぽんぴんぽーん",
        "よくできましたっ",
        "あははは",
        "あれ？",
        "ありがとっ",
        "ばぁ",
        "ちょっとまずいかも",
        "だーれだっ",
        "ごめんなさいっ",
        "はい！",
        "ひらめいた～",
        "ひゃー",
        "いないいない",
        "ばぁ",
        "ぽん",
        "きょーちゃん",
        "がうがう",
        "きらりらりん",
        "ぴひょん",
        "ぴゅぽん",
        "けこー",
        "じゃじゃんっ",
        "うぁーっ",
        "じゃばーん",
        "ぴゅぽん",
        "どろどろどろどろ",
        "じゃん" };


    public Sprite[] AF_btnImage = new Sprite[69];

    // Use this for initialization
    void Start () {


        //当たりを設定
        SetWinNumber();


        //Content取得
        RectTransform content = GameObject.Find("Canvas/Scroll View/Viewport/Content").GetComponent<RectTransform>();

        //Contentの高さを設定
        //（ボタンの高さ+ボタン同士の間隔）*ボタンの数

        float btnSpace = content.GetComponent<VerticalLayoutGroup>().spacing;
        float btnHeight = AF_btn.GetComponent<LayoutElement>().preferredHeight;
        content.sizeDelta = new Vector2(0, (btnHeight + btnSpace) * BUTTON_COUNT);



 

        for (int i = 0; i < BUTTON_COUNT; i++)
        {
            int no = i;
            int num_counter = i;

            //ボタンの生成
            GameObject btn = (GameObject)Instantiate(AF_btn);
            //ボタンをContentの子に設定
            btn.transform.SetParent(content, false);

            //〇つに一度ボタンのサイズを変更
            if ((num_counter %= Size_num) == 0)
            {
                Pho_width -= 0.03f;
                Pho_height -= 0.03f;

            }

            btn.transform.localScale = new Vector2(Pho_width, Pho_height);

            //ボタンのテキストを設定
            btn.transform.GetComponentInChildren<Text>().text = AF_buttonLabel[no];
            //ボタンのImageを設定
            btn.transform.GetComponentInChildren<Image>().sprite = AF_btnImage[no];
            //ボタンのクリックイベント登録
            btn.transform.GetComponent<Button>().onClick.AddListener(()=>OnClick(no));

        }

    }

    public void OnClick(int no)
    {

        //ひとまずSEを止めてから
        SoundManager.Instance.StopSE();
        //配列no版のSEを鳴らす。
        SoundManager.Instance.PlaySE(no);


//        SoundManager.Instance.PlayBGM(0);
//        SoundManager.Instance.PlaySE(2);
//        SoundManager.Instance.PlayVoice(3);


        if (WinNumber == no)
        {
            Debug.Log("当たり" + no);
            SetWinNumber();
        }
        else
        {
            Debug.Log("はずれ"+no);


        }
    }


    private void SetWinNumber()
    {
        WinNumber = Random.Range(0, BUTTON_COUNT);
        Debug.Log("WinNumber=" + WinNumber);

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




}
