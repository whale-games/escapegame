using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private textcontroller textcontroller;
    [SerializeField] private GameObject messagePanel;
    //オープニングフラグ管理
    public static bool flag0,flag0end;
    //一面フラグ管理（,,,Locker）（flag1a=table開けたか,flag1b=ロッカー確認,flag1c=スーツケース確認）
    public static bool flag1,flag1a,flag1b,flag1c,flag3a,flag3b,flag1end,flag1enda,flag1endb;
    //二面フラグ管理（radio,,,Small stool）
    public static bool flag2a,flag2b,flag2c,flag2end,flag2enda,flag2endb;
    //三面フラグ管理
    public static bool flag30,flag301,flag31,flag32,flag33,flag330,flag3end,flag3enda,flag3endb;
    //クリア後フラグ管理
    public static bool flag4,flag4end;
    //アイテムフラグ管理（iflag0=Emissive window,iflag1=Garage door,iflag2=Camouflage suitcase with relief,Small stool）
    public static bool iflag0,iflag1,iflag2;
    private AudioSource audioSource;
 	[SerializeField] private AudioClip[] audioClip;//配列使用参考
    [SerializeField] private AudioClip[] koukaonClip;//配列使用参考
    [SerializeField] private GameObject onnnanoko;
    [SerializeField] private GameObject Clock,KeyPad;
    [SerializeField] private MusicBinder MusicBinder;
    public static bool nowMessage;
    public static bool nowPuzzle;
 
    public void Start(){
                StartCoroutine("StartTalk"); 
                audioSource = GetComponent<AudioSource>(); //AudioSourceの取得
    }
    public IEnumerator StartTalk(){
        //オープニング
        messagePanel.SetActive(true);
        nowMessage = true;
                Debug.Log("音楽再生前");
                yield return StartCoroutine(textcontroller.NormalChat("Player","ここは…？"));
                MusicBinder.Musicplay();
                Debug.Log("音楽再生後");
                yield return StartCoroutine(textcontroller.NormalChat("　　　","目が覚めると僕は見知らぬ場所にいた。"));                
                onnnanoko.SetActive(true);
                //女性ボイス
                audioSource.Stop();
                audioSource.volume = 0.9f;
                audioSource.clip = audioClip[0];
                audioSource.Play();
                yield return StartCoroutine(textcontroller.NormalChat("謎の女の子","あら、ようやくお目覚め？"));
                audioSource.Stop(); 
                yield return StartCoroutine(textcontroller.NormalChat("Player","君は…？"));
                //女性ボイス
                audioSource.volume = 0.9f;
                audioSource.clip = audioClip[1];
                audioSource.Play();
                yield return StartCoroutine(textcontroller.NormalChat("謎の女の子","もしかして、覚えてないの？　昨日の事"));
                audioSource.Stop();
                yield return StartCoroutine(textcontroller.NormalChat("　　　","…なんの事だろう？"));
                yield return StartCoroutine(textcontroller.NormalChat("　　　","なんにも覚えていないなあ。ああ、覚えてない覚えてない。"));
                yield return StartCoroutine(textcontroller.NormalChat("Player","覚えていない…。"));
                yield return StartCoroutine(textcontroller.NormalChat("Player","君の名は…？"));
                //女性ボイス
                yield return StartCoroutine(textcontroller.NormalChat("謎の女の子","急なショックで、一時的に記憶に混乱が生じているのかしら…。"));
               //女性ボイス
                yield return StartCoroutine(textcontroller.NormalChat("謎の女の子","私の名前はリン。リンという事にしておきましょう。コードネーム的に。"));
                yield return StartCoroutine(textcontroller.NormalChat("Player","とにかく僕はここから出る。出なければいけないんだ…。"));
                //女性ボイス
                audioSource.Stop();
                audioSource.volume = 0.9f;
                audioSource.clip = audioClip[2];
                audioSource.Play(); 
                yield return StartCoroutine(textcontroller.NormalChat("リン","…そんなに簡単にここから出られるとは思えないけど。"));
                audioSource.Stop();
                yield return StartCoroutine(textcontroller.NormalChat("　　　","なんなんだこの女は。"));
                yield return StartCoroutine(textcontroller.NormalChat("　　　","部屋を見渡せばすぐに出口から出ていく事ができるはずだ"));
                onnnanoko.SetActive(false);
                //※赤文字に変更？
                MusicBinder.Musicstop();
                yield return StartCoroutine(textcontroller.NormalChat("　　　","<color=#ff0000>※画面上にある左右の矢印ボタンを押すことで視点を変えることができます。</color>"));
                //※シャッター
                flag0 = true;
                Debug.Log("flag0 = true");
                flag0end = true;
                Debug.Log("flag0end = true");
                MusicBinder.Musicplay();
        messagePanel.SetActive(false);
        nowMessage = false;
    }
    public void TestClick2(ItemClickEvent clickEvent){
        if(clickEvent.tag == "Item")
            StartCoroutine("TestClick",clickEvent);        
        else if (clickEvent.tag == "ItemUse")
            StartCoroutine("ItemUseClick",clickEvent); 
        else
            StartCoroutine("WrongItemUseClick",clickEvent); 
    }
    public IEnumerator TestClick(ItemClickEvent itemClickEvent){
        messagePanel.SetActive(true);
        nowMessage = true;
        switch(itemClickEvent.name){
            case "Drill Bits.001":
                yield return StartCoroutine(textcontroller.NormalChat("テスト","とてもねむいです。"));
                break;
            case "Simple_02":
                onnnanoko.SetActive(true);
                yield return StartCoroutine(textcontroller.NormalChat("リン","どこで使うのかしら。"));
                //キャラクター消去
                onnnanoko.SetActive(false);   
                yield return StartCoroutine(textcontroller.NormalChat("　　　","何かの鍵を入手した。"));
                //キャラクター表示
                break;
            default:
                break;
        }
        messagePanel.SetActive(false);
        nowMessage = false;
    }

    public IEnumerator ItemUseClick(ItemClickEvent itemClickEvent){
        messagePanel.SetActive(true);
        nowMessage = true;
        switch(itemClickEvent.name){
            //シナリオ一面部分
            case "Table":
                if (flag1 && !flag1a && !flag1c){
                    yield return StartCoroutine(textcontroller.NormalChat("Player","鍵が開いた。"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","紙だ。「1234」と書いてある"));
                    flag1a=true;
                    Debug.Log("flag1a = true");
                    }
                else if (flag1a && !flag1c)
                    yield return StartCoroutine(textcontroller.NormalChat("Player","引き出しには「1234」と書かれた紙が入っている。"));
                else if (flag1a && flag1c && iflag1)
                    yield return StartCoroutine(textcontroller.NormalChat("Player","机の引き出しにあった数字は1234…。スーツケースの中のメモに「机の引き出しの数字は逆」これらを組み合わせると…。"));
                else
                    yield return StartCoroutine(textcontroller.NormalChat("Player","鍵穴が空いている。中を見るには鍵が必要だ。"));               
                break;
            case "Camouflage suitcase with relief":
                if (flag1b)
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","紙だ。「机の引き出しの数字は逆」と書いてある"));
                break;
            case "Locker":
                if (!iflag1)
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","ロッカーの中にキーパネルがあるが、今はこの部屋の出口を探そう。"));
                else if (iflag1 && !flag1a && !flag1c)
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","中にキーパネルがあるが、何を入力していいかわからない…。"));
                else if (iflag1 && flag1a && !flag1c)
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","1234と入力してみたが反応がない。この数字は関係ないのだろうか？　もしくは他になにか情報が必要なのか？"));
              //else if (!flag1 && flag1c)
              //    yield return StartCoroutine(textcontroller.NormalChat("　　　","「机の引き出しの数字は逆」というメモが気になる。まずは机を探してみよう！"));
                else if (flag1enda && !flag1endb){
                //一面クリア時一回目
                    Debug.Log("一面クリア");
                    KeyPad.SetActive(false);
                    MusicBinder.Musicstop();
                    audioSource.Stop(); 
                    audioSource.volume = 0.5f;
                    audioSource.clip = koukaonClip[0];
                    audioSource.Play();
                    yield return StartCoroutine(textcontroller.NormalChat("   ","何かが解除される音がした。"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","何か音がしたけど…。"));
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("リン","また部屋の中を探してみた方がいいみたいね。"));
                    flag1endb = true;
                    Debug.Log("flag1endb = true");
                    onnnanoko.SetActive(false);
                    MusicBinder.Musicplay();}
                else if (flag1end && flag1endb){ 
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("リン","既に解除された仕掛けを何回も見てもしょうがない気がするけど。"));
                    onnnanoko.SetActive(false);}
                break;

        　　　　//シナリオ３部分
        　　　　//ハズレアイテムタッチ時
                //それ以外   
            case "Drill Bits.002":
                //キャラクター表示
                onnnanoko.SetActive(true);
                yield return StartCoroutine(textcontroller.NormalChat("リン","とてもねむいわね。"));
                //キャラクター消去
                onnnanoko.SetActive(false);   
                break;
            case "Cone Drill Bits":
                //キャラクター表示
                onnnanoko.SetActive(true);
                yield return StartCoroutine(textcontroller.NormalChat("リン","こんなものは何の役にも立たないわ。"));
                //キャラクター消去
                onnnanoko.SetActive(false);
                break;
            default:
                break;
        }
        messagePanel.SetActive(false);
        nowMessage = false;
    }
// Wrongitem
    public IEnumerator WrongItemUseClick(ItemClickEvent itemClickEvent){
        messagePanel.SetActive(true);
        nowMessage = true;
        switch(itemClickEvent.name){
            case "2 sockets":
              onnnanoko.SetActive(true);
              yield return StartCoroutine(textcontroller.NormalChat("リン","電気は通っているようだけどあなたと心は通わないわね。"));
              onnnanoko.SetActive(false);
              break;
            case "Hose":
                if (flag1end)
                    yield return StartCoroutine(textcontroller.NormalChat("Player","ただのホースのようだ。これも脱出に何か関係あるのだろうか。"));
                else
                    yield return StartCoroutine(textcontroller.NormalChat("Player","ただのホースのようだ。"));
                break;
            case "Air conditioner":
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","エアコンだ。電源は入っていない。"));
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("リン","暑いわね。"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","そうかなあ？　むしろ乾燥の方が気になるけど。"));
                    yield return StartCoroutine(textcontroller.NormalChat("リン","それってあなたの感想ですよね？"));
                    audioSource.Stop(); 
                    audioSource.volume = 0.4f;
                    //危険なギャグ時効果音koukaonClip[1]
                    audioSource.clip = koukaonClip[1];
                    audioSource.Play();
                    yield return StartCoroutine(textcontroller.NormalChat("Player","……"));
                    onnnanoko.SetActive(false);
                    audioSource.Stop();
                break;
            case "Emissive window":
                if (!iflag0){
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","開かない。とても頑丈そうでここからは出られそうにない。"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","ここからは出られないか…"));
                    //キャラクター表示
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("リン","まーどーでもいいじゃない、窓だけに。"));            
                    audioSource.Stop(); 
                    audioSource.volume = 0.8f;
                    //危険なギャグ時効果音koukaonClip[2]
                    audioSource.clip = koukaonClip[2];
                    audioSource.Play();
                    yield return StartCoroutine(textcontroller.NormalChat("Player","！？"));
                    audioSource.Stop(); 
                    //キャラクター消去
                    onnnanoko.SetActive(false);
                    iflag0 = true; 
                    }
                else{
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","窓だ。"));
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("リン","二回は言わないわよ？"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","ですよね…。"));
                    onnnanoko.SetActive(false);}
                break;
            case "Garage door":
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","シャッターがある。ここから出られそうだ。"));
                    //※効果音：ガシャガシャ
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","ガシャガシャ。"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","…何らかの方法で閉じられており、開かない。"));
                    onnnanoko.SetActive(true);
                    //女性ボイス
                    audioSource.volume = 0.9f;
                    audioSource.clip = audioClip[3];
                    audioSource.Play(); 
                    yield return StartCoroutine(textcontroller.NormalChat("リン","出られそうにないわね。"));
                    audioSource.Stop();
                    yield return StartCoroutine(textcontroller.NormalChat("Player","…何か知っているの？"));   
                    //女性ボイス
                    audioSource.volume = 0.9f;
                    audioSource.clip = audioClip[4];
                    audioSource.Play(); 
                    yield return StartCoroutine(textcontroller.NormalChat("リン","別に。あなたより少し早く起きたから、それくらいは先に調べていたってだけよ。"));
                    audioSource.Stop();
                    yield return StartCoroutine(textcontroller.NormalChat("Player","じゃあ何も知らないのか。どうやってここから出ればいいんだ？"));
                    //女性ボイス
                    audioSource.volume = 0.9f;
                    audioSource.clip = audioClip[5];
                    audioSource.Play(); 
                    yield return StartCoroutine(textcontroller.NormalChat("リン","部屋の中を色々と調べてみましょう。"));
                    audioSource.Stop();
                    //女性ボイス
                    audioSource.volume = 0.9f;
                    audioSource.clip = audioClip[6];
                    audioSource.Play(); 
                    yield return StartCoroutine(textcontroller.NormalChat("リン","何か脱出の手がかりがあるかもしれないから。"));
                    audioSource.Stop();
                    yield return StartCoroutine(textcontroller.NormalChat("Player","そうするしかなさそうだな…。"));
                    //女性ボイス
                    audioSource.volume = 0.9f;
                    audioSource.clip = audioClip[7];
                    audioSource.Play();
                    yield return StartCoroutine(textcontroller.NormalChat("リン","素直ね。"));
                    audioSource.Stop();
                    yield return StartCoroutine(textcontroller.NormalChat("Player","そうかな。そうかもしれない。"));
                    //女性ボイス
                    audioSource.volume = 0.9f;
                    audioSource.clip = audioClip[8];
                    audioSource.Play();
                    yield return StartCoroutine(textcontroller.NormalChat("リン","あなたって主義主張がなさそうな顔してるものね"));
                    audioSource.Stop();
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","そうだろうか？　言われてみたらそうかもしれない。僕には大事にしているものなんて何もないんだ。"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","とにかく部屋の中を探そう。"));
                    onnnanoko.SetActive(false);
                    iflag1 = true;               
                break;
               //二面 
            case "Small stool":           
                //クリア前
                if (flag2a && flag2b && flag2b && !flag2end){
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","裏側に時計がある。"));
                    Clock.SetActive(true);}
                //クリア後
                else if (flag2enda && !flag2endb){
                    MusicBinder.Musicstop();
                    Clock.SetActive(false);
                    audioSource.Stop(); 
                    audioSource.volume = 0.5f;
                    //開錠時効果音koukaonClip[0]
                    audioSource.clip = koukaonClip[0];
                    audioSource.Play();
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","仕掛けが解除された。"));
                    flag2endb = true;
                    MusicBinder.Musicplay();}
                else if (flag2enda && flag2endb)
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","時計の仕掛けは解除済だ。"));
                break;
            case "Radio":
                if (flag1end && !flag2a){
                    MusicBinder.Musicstop();
                    audioSource.Stop(); 
                    audioSource.volume = 0.1f;
                    audioSource.clip = koukaonClip[3];
                    audioSource.Play();
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","ラジオからノイズが聞こえている。")); 
                    audioSource.Stop(); 
                    yield return StartCoroutine(textcontroller.NormalChat("Player","さっきまで電源も入っていなかったのに…")); 
                    yield return StartCoroutine(textcontroller.NormalChat("謎の声","なかなか理解の早い子供達だな…"));
                    yield return StartCoroutine(textcontroller.NormalChat("謎の声","だがどのみち君たち二人がそこから出られることはない…。"));
                    yield return StartCoroutine(textcontroller.NormalChat("謎の声","せいぜいあがく姿を楽しませてくれ…。フ、フッ、フフフ…。"));
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("リン","くっ、絶対脱出の方法を見つけてみせるんだから！"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","リン…そんなキャラだったっけ？"));
                    onnnanoko.SetActive(false);
                    MusicBinder.Musicplay();
                    flag2a = true;}
                else if(flag2a){
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","ラジオだ。電源は入っていない。"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","どんな仕掛けになっているんだろう？"));
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("リン","そんなのどうでもいいわ。とにかくこの部屋から出る方法を探すのよ！"));
                    onnnanoko.SetActive(false);}
                else{
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("リン","ラジオね。好きな曲でも聴けたらいいのに。")); 
                    yield return StartCoroutine(textcontroller.NormalChat("Player","電源も入ってないからなあ。")); 
                    onnnanoko.SetActive(false);}
                break;
            default:
                break;
        }
        messagePanel.SetActive(false);
        nowMessage = false;
    }
}

