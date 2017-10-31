using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;



public class ballScript : MonoBehaviour {

    public GameObject ballPrefab;
    public Sprite[] ballSprites;
    private GameObject firstBall;   //最初のタッチ
    private GameObject lastBall;    //最後のタッチ
    private string currentName;     //名前判定用のstring変数
    List<GameObject> removableBallList = new List<GameObject>();    //削除ボールのリスト

    public GameObject scoreGUI;
    public GameObject playcounterGUI;
    private int point = 10;
    private int BonusPoint;

    public GameObject exchangeButton;

    public static int PlayCounter; //プレイ回数をカウントする。残りプレイ回数にも表示する。
    public static int BeneScoreCount; //結果用：スコア。
    public static int vanishBallCount; //結果用：消したボールの数をカウントする。

    float ballSize;

    public int remove_cnt;


    //スタート
    void Start () {

        //BGMスタート
        SoundManager.Instance.PlayBGM(0);

        //プレイカウンターの初期化
        PlayCounter = 10;
        playcounterGUI.SendMessage("NumPlayCounter", PlayCounter);

        StartCoroutine(DropBall(40));
	}

    //アップデート
    private void Update()
    {

        if (PlayCounter > 0) { 
            //画面をクリックし、firstBallがnullなら実行
            if (Input.GetMouseButtonDown(0)&&firstBall == null)
            {
                OnDragStart();
            }else if (Input.GetMouseButtonUp(0))
            {
                //クリック終了
                OnDragEnd();
            }else if (firstBall != null)
            {
                //クリック中
                OnDragging();
            }
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            // エスケープキー取得
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                FadeManager.Instance.LoadScene("title", 0.1f);
            }
        }
    }

    //---------------------------------------------------------

    //ドラッグが開始時の処理

    //---------------------------------------------------------
    private void OnDragStart()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if(hit.collider != null)
        {
            //タップONでものがあればSEを鳴らす。
            SoundManager.Instance.PlaySE(0);

            GameObject hitObj = hit.collider.gameObject;
            //オブジェクトの名前を前方一致で判定
            string ballName = hitObj.name;

            if (ballName.StartsWith("Piyo"))
            {
                firstBall = hitObj;
                lastBall = hitObj;
                currentName = hitObj.name;
                //削除対象リストの初期化
//                removableBallList = new List<GameObject>();
                //削除対象オブジェクトを代入
                PushToList(hitObj);
            }
        }
    }

    //---------------------------------------------------------

    //ドラッグが終わった時の処理

    //---------------------------------------------------------
    private void OnDragEnd()
    {


        remove_cnt = removableBallList.Count;

        //        Debug.Log(remove_cnt);

        //削除対象がxxつ以上なら
        if (remove_cnt >= 1)
        {
            //ドラッグ終了SEを鳴らす
            SoundManager.Instance.PlaySE(1);

            //いったん前のボイスを消してから。消えた数によってボイスが変わる。
            SoundManager.Instance.StopVoice();
            if (remove_cnt == 2)
            {
                SoundManager.Instance.PlayVoice(0);
            }
            else if (remove_cnt == 3)
            {
                SoundManager.Instance.PlayVoice(1);
                BonusPoint = 300;
            }
            else if (remove_cnt == 4)
            {
                SoundManager.Instance.PlayVoice(2);
                BonusPoint = 500;
            }
            else if (remove_cnt == 5)
            {
                SoundManager.Instance.PlayVoice(3);
                BonusPoint = 1000;
            }
            else if (remove_cnt >= 6)
            {
                SoundManager.Instance.PlayVoice(4);
                BonusPoint = 3000;
            }


            //まず、当たり判定と、重力の影響を無効化する。
            for (int i = 0; i < remove_cnt; i++)
            {
                removableBallList[i].GetComponent<CircleCollider2D>().enabled = false;
                removableBallList[i].GetComponent<Rigidbody2D>().simulated = false;
            }

            //そのあとで、消えるときのアニメーションを行う
            for (int i = 0; i < remove_cnt; i++)
            {
                //フェードしながら
                iTween.FadeTo(removableBallList[i], iTween.Hash(
                    "alpha", 0,
                    "time", 0.3f)
                );

                //拡大して、アニメが終わったら続きの処理へ飛ぶ
                iTween.ScaleTo(removableBallList[i], iTween.Hash(
                    "x", 2f,
                    "y", 2f,
                    "time", 0.3f,
                    "delay",0.0f,
                    "oncomplete", "OnCompleteHandler",
                    "oncompletetarget", this.gameObject)
                );

            }

        }
    }

    //
    //iTweenのアニメーションが終あとの続きの処理
    //
    void OnCompleteHandler()
    {
        Debug.Log("終わりを通過");

        for (int i = 0; i < remove_cnt; i++)
        {
            Destroy(removableBallList[i]);
        }

        //結果用に消した数をcount
        vanishBallCount = vanishBallCount + remove_cnt;

        Debug.Log("消えた玉：" + vanishBallCount);

        //スコアをカウント point * 消した数 + ボーナス
        scoreGUI.SendMessage("AddPoint", point * remove_cnt + BonusPoint);
        //新しく生成
        StartCoroutine(DropBall(remove_cnt));


        //プレイカウンターで１０回をcountして終わりを判定する。
        PlayCounter--; //プレイカウンターを１つ減らす

        playcounterGUI.SendMessage("NumPlayCounter", PlayCounter);

        //            Debug.Log(PlayCounter);

        //10回消したら終わり
        if (PlayCounter <= 0)
        {
            SoundManager.Instance.StopBGM();

            StartCoroutine("GameFinish");
        }
        else
        {
            //透明度を100%にもどす。
            for(int i = 0; i<remove_cnt; i++)
            {
                ChangeColor(removableBallList[i], 1.0f);
            }
        }

        firstBall = null;
        lastBall = null;
        removableBallList = new List<GameObject>();


    }



    //---------------------------------------------------------

    //ドラッグ中

    //---------------------------------------------------------

    private void OnDragging()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            GameObject hitObj = hit.collider.gameObject;

            //同じ名前のブロックをクリック、かつ、lastBallとは別のオブジェクトである。
            if (hitObj.name == currentName && lastBall != hitObj)
            {
                //2つのブロックの距離を取得
                float distance = Vector2.Distance(hitObj.transform.position, lastBall.transform.position);
                if (distance < 1.35f)
                {
                    //タップONでSEを鳴らす。
                    SoundManager.Instance.PlaySE(0);

                    //削除対象のオブジェクトを格納
                    lastBall = hitObj;
                    PushToList(hitObj);
                }
            }
        }
    }


    //---------------------------------------------------------

    //ボール生成用のコルーチン

    //---------------------------------------------------------
    IEnumerator DropBall(int count)
    {
        if(count == 40)
        {
            StartCoroutine("RestrictPush");
        }

        for(int i = 0; i < count; i++)
        {
            Vector2 pos = new Vector2(Random.Range(-1.8f, 1.8f), 7f);
            GameObject ball = Instantiate(ballPrefab, pos, Quaternion.AngleAxis(Random.Range(-40, 40), Vector3.forward)) as GameObject;

            //ボールのサイズを変更する。
            int sizeSet = Random.Range(0, 3);
            if (sizeSet == 0)
            {
                ballSize = 0.6f;
            }else if(sizeSet == 1)
            {
                ballSize = 0.8f;
            }else if (sizeSet == 2)
            {
                ballSize = 1.1f;
            }
            ball.transform.localScale = new Vector2(ballSize, ballSize);
            
            int spriteId = Random.Range(0, 5);
            ball.name = "Piyo" + spriteId;
            SpriteRenderer spriteObj = ball.GetComponent<SpriteRenderer>();
            spriteObj.sprite = ballSprites[spriteId];
            yield return new WaitForSeconds(0.05f);
        }
    }

    //---------------------------------------------------------

    //リセットボタンの制御

    //---------------------------------------------------------
    IEnumerator RestrictPush()
    {
        exchangeButton.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(5.0f);
        exchangeButton.GetComponent<Button>().interactable = true;
    }


    void PushToList(GameObject obj)
    {
        removableBallList.Add(obj);
        ChangeColor(obj, 0.5f);
    }

    void ChangeColor(GameObject obj,float transpearency)
    {
        //SpriteRenderコンポーネントを取得
        SpriteRenderer ballTexture = obj.GetComponent<SpriteRenderer>();
        //coloreプロパティの透明度を変更する。
        ballTexture.color = new Color(ballTexture.color.r, ballTexture.color.g, ballTexture.color.b, transpearency);
    }

    IEnumerator GameFinish()
    {

        //イベントシステムを無効にしてそれ以上ボタンが押せないように。
        var eventSystem = GameObject.FindObjectOfType<EventSystem>();
        eventSystem.enabled = false;

        // 5秒間待つ
        yield return new WaitForSeconds(0.5f);
        FadeManager.Instance.LoadScene("BeneScore", 0.5f);
    }

}
