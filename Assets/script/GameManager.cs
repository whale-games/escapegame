using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] private textcontroller textcontroller;
    [SerializeField] private ItemController itemcontroller;
    [SerializeField] private GameObject messagePanel;
    //オープニングフラグ管理
    public static bool flag0,flag0end;
    //一面フラグ管理（,,,Locker）（flag1a=table開けたか,flag1b=ロッカー確認,flag1c=スーツケース確認）
    public static bool flag1,flag1a,flag1b,flag1c,flag1end,flag1enda,flag1endb;
    //二面フラグ管理（flag2a=radio,flag2b=Saw,,Small stool）
    public static bool flag2a,flag2b,flag2c,flag2d,flag2e,flag2end,flag2enda,flag2endb; 
    //三面フラグ管理
    public static bool flag3a,flag3b,flag3c,flag3d,flag3e,flag3f,flag3end,flag3enda,flag3endb;
    //アイテム格納管理
    public static bool itemchange;
    //クリア後フラグ管理
    public static bool flag4,flag4end;
    //アイテムフラグ管理（iflag0=Emissive window,iflag1=Garage door,iflag2=Camouflage suitcase with relief,Small stool）
    public static bool iflag0,iflag1,iflag2;
    //ドリルマシンフラグ管理
    public static bool  Drillingmachineflag;
    private AudioSource audioSource;
 	[SerializeField] private AudioClip[] audioClip;//配列使用参考
    [SerializeField] private AudioClip[] koukaonClip;//配列使用参考
    [SerializeField] private GameObject onnnanoko;
    [SerializeField] private GameObject Clock,KeyPad,Rimokon,RemotoController;
    [SerializeField] private MusicBinder MusicBinder;
    [SerializeField] private GameObject gametitle;
    [SerializeField] private SelectBox SelectBox;
    [SerializeField] private RemoteControler RemoteControler;
    public static bool nowMessage;
    public static bool nowPuzzle;
    [SerializeField] private ItemUtils itemUtils;
    [SerializeField] private GameObject getitem;

    public void title(){
        Debug.Log("title start");
        audioSource = GetComponent<AudioSource>(); //AudioSourceの取得
        StartCoroutine("Corou1");
    }
    public IEnumerator Corou1() {
        yield return new WaitForSeconds(1);//処理を1秒待つ
        //シーン切替
        SceneManager.LoadScene("Main");
        StartCoroutine("Start");
    }
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
                audioSource.volume = 0.9f;
                audioSource.clip = audioClip[12];
                audioSource.Play();
                yield return StartCoroutine(textcontroller.NormalChat("謎の女の子","急なショックで、一時的に記憶に混乱が生じているのかしら…。"));
                audioSource.clip = audioClip[13];
                audioSource.Play();
               //女性ボイス
                yield return StartCoroutine(textcontroller.NormalChat("謎の女の子","私の名前はリン。リンという事にしておきましょう。コードネーム的に。"));
                audioSource.Stop();
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
            case "Drilling machine":
                if (!flag1end){
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","電動ドリルだ。コンセントにさせれば部屋に穴を開ける事ができるかもしれない。"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","さすがに壁を壊すのはまだ早いか…。"));
                    //itemchangeフラグはItemControllerの3面素材作成ギミック（タグ書き換え→Item）で使用する
                    //itemchange = true;
                    }
                else if (flag1end && flag2a && !flag2c && !flag2e){
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","電動ドリルだ。コンセントにさせれば部屋に穴を開ける事ができるかもしれない。"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","移動が大変そうだけど…。"));
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("Player","これで壁を壊して出れないかな。"));
                    yield return StartCoroutine(textcontroller.NormalChat("リン","ちょっと難しいかもしれないわね。"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","なんとかできないかな？"));
                    yield return StartCoroutine(textcontroller.NormalChat("リン","多分主催者側もそういう脱出の方法は望んでいないでしょうし。"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","主催者…"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　　","「主催者」ってなんなんだ？"));
                    onnnanoko.SetActive(false);
                    flag2e = true;}
                else if (flag1end && flag2a && !flag2c  && !flag2d && flag2e){
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","電動ドリルだ。"));}
                else if (flag1end && flag2a && !flag2c && flag2d && flag2e){
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("リン","でも脱出に必要ないのにこんなドリルが置いてあるなんて確かに不自然ね。細かいところまでちゃんと見てみて？"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　　","自分で調べればいいのに…。"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","あ、「４５」ってマジックで横に書いてあるよ？"));
                    yield return StartCoroutine(textcontroller.NormalChat("リン","そんな数字、なんの意味があるのよ？"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","ほら、あの椅子に取り付けられてた時計とかに、何か関係あるんじゃないかな？"));
                    //↓悔しそうに
                    yield return StartCoroutine(textcontroller.NormalChat("リン","……なかなかやるじゃない！"));
                    onnnanoko.SetActive(false);
                    flag2c = true;
                    }
                else if (flag2endb && flag3a && ItemController.drillmachinenumber==0){
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("リン","ちょっと待って、この電動ドリル、動かせるんじゃない？"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","でも、これで脱出しちゃいけないって言ってなかった？"));
                    yield return StartCoroutine(textcontroller.NormalChat("リン","まっとうな出方なら問題ないわ。"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","まっとうな出方の定義がわからないけど…。"));
                    yield return StartCoroutine(textcontroller.NormalChat("リン","何か脱出の手順が用意されているはずなのよ。これは「罰」なんだから…。"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","ごめんね、俺のせいで…。"));
                    yield return StartCoroutine(textcontroller.NormalChat("リン","今は、気にしないで。…あとで泣けばいいだけだし。もう誰かの事を恨みたくもないわ。"));                    
                    yield return StartCoroutine(textcontroller.NormalChat("Player","うん…。"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　　","多分僕たちは疲れていた。少なくとも僕はもう、疲れていたんだ。"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","ここにいくつか素材を入れれば、脱出のための道具が作れるんじゃないかな。"));
                    yield return StartCoroutine(textcontroller.NormalChat("リン","あら、多分それね？　どうしたのよ。昔の頼もしかった時のあなたが戻ってきたみたい。"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　　","だから覚えてないんだって…。"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","全部で何個素材が必要？"));
                    yield return StartCoroutine(textcontroller.NormalChat("リン","多分全部で五個くらいじゃないかしら。")); 
                    yield return StartCoroutine(textcontroller.NormalChat("Player","わかった。とにかく部屋の中をしらみつぶしに探してみよう。"));
                    onnnanoko.SetActive(false);}                
                else if (flag2endb && flag3a && ItemController.drillmachinenumber<5){
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("リン","あと"+(5-ItemController.drillmachinenumber)+"個素材が必要ね。"));
                    onnnanoko.SetActive(false);}
                else if (ItemController.drillmachinenumber==5){
                    //ここでアイテム入手したいけど、アセットストアからアイテムまだ取り込んでないから、取り込んでテストする必要あり。
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("リン","これで素材は十分ね。"));
                    //脱出用素材入手スクリプト起動
                    itemUtils.AddItem(getitem, itemcontroller.panels);
                    onnnanoko.SetActive(false);
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","脱出用のアイテムを手に入れた。"));}
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
                    yield return StartCoroutine(textcontroller.NormalChat("Player","うん。"));
                    yield return StartCoroutine(textcontroller.NormalChat("リン","そういえば…"));
                    yield return StartCoroutine(textcontroller.NormalChat("リン","本当に覚えていないの？　この部屋に閉じ込められる「前」の事。"));
                    flag1endb = true;
                    Debug.Log("flag1endb = true");
                    MusicBinder.Musicplay();
                    //選択肢表示  
                    nowPuzzle= true;
                    SelectBox.gameObject.SetActive(true);
                    string[] texts = {"覚えている","覚えていない"}; 
                    SelectBox.SetReset("sentakusi",texts);
                    onnnanoko.SetActive(false);
                    //選択肢表示終
                    }
                else if (flag1end && flag1endb){ 
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("リン","既に解除された仕掛けを何回も見てもしょうがない気がするけど。"));
                    onnnanoko.SetActive(false);}
                break;
            //シナリオ三面部分
            case "Small locker.002":
                if (flag0end)
                    yield return StartCoroutine(textcontroller.NormalChat("   ","何重にも釘が打たれ開かない。"));                                
                break;
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
                if (flag0end && !flag1end)
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","エアコンだ。電源は入っていない。"));
                else if (flag1end && !flag2end){
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
                    audioSource.Stop();}
                else if (flag0end && flag1end && flag2end){
                    //ちょっと調整必要かも？
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","エアコンのリモコンがある。"));
                    nowPuzzle = true;
                    Rimokon.SetActive(true);
                    RemotoController.SetActive(true);}
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
            //オープニング➁
            case "Garage door":
                if (!iflag1){
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
                    }               
                break;
                //シナリオ二面部分
            case "Saw":
                if (!flag1end){
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","電動ノコギリだ。コンセントにさせれば部屋に穴を開ける事ができるかもしれない。"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","さすがに壁を壊すのはまだ早いか…。"));}
                else if (flag1end && flag2a && !flag2b){
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","電動ノコギリだ。コンセントにさせれば部屋に穴を開ける事ができるかもしれない。"));
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("Player","これで壁を壊して出れないかな。"));
                    yield return StartCoroutine(textcontroller.NormalChat("リン","こっちのノコギリでは難しいかもしれないわね。"));
                    onnnanoko.SetActive(false);}
                break;
            case "Locker.001":
                if (!flag1end)
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","大き目なロッカーだが、開かない…。"));
                else if (flag1end && flag2a && !flag2b){
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","大き目なロッカーだが、開かない…。"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","何かありそうな感じなんだけどなあ。"));
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("リン","ちゃんとすみずみまで確認してみた？"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","そんな事言われてもなあ。"));
                    yield return StartCoroutine(textcontroller.NormalChat("リン","すぐに文句言わないの。"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","ロッカーの下の枠の部分をよく見てみると…。"));                    
                    yield return StartCoroutine(textcontroller.NormalChat("Player","あ、木枠が削られて何か書いてある！"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","8時〇〇分次への扉が開かれる。")); //2面正解の文章入れる。
                    yield return StartCoroutine(textcontroller.NormalChat("Player","分の部分がカスれて読めない…。")); //2面正解の文章入れる。
                    yield return StartCoroutine(textcontroller.NormalChat("リン","困ったわね。他に部屋に何かヒントのようなものとかないのかしら。"));
                    onnnanoko.SetActive(false);
                    flag2b = true;}
                else if (flag1end && flag2a && flag2b)
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","「8時〇〇分次への扉が開かれる。」と削り文字で書かれている。分の部分はカスれていて読めない。")); 
                break;
               //二面パズル            
            case "Small stool":           
                //二面クリア前
                if (flag2a && !flag2d && !flag2end){
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","裏側に時計があるが何をすればいいのかわからない…。"));
                    flag2d = true;
                }
                else if  (flag2a && flag2b && flag2c && flag2d && flag2e && !flag2end){
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","裏側に時計がある。"));
                    Clock.SetActive(true);}
                else if (flag2a && !flag2b && !flag2end){
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","裏側に時計があるが何をすればいいのかわからない…。"));                  
                }
                else if (flag2a && !flag2c && !flag2end){
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","裏側に時計があるが何をすればいいのかわからない…。"));                  
                }
                
                //二面クリア後
                else if (flag2enda && !flag2endb){
                    MusicBinder.Musicstop();
                    Clock.SetActive(false);
                    audioSource.Stop(); 
                    audioSource.volume = 0.5f;
                    audioSource.clip = koukaonClip[0];
                    audioSource.Play();
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","仕掛けが解除された。"));
                    flag2endb = true;
                    flag2end = true;
                    MusicBinder.Musicplay();
                    yield return StartCoroutine(textcontroller.NormalChat("Player","おや、これは…？"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","仕掛けの解除と共に小さな紙が出てきた"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","<color=#ff0000>隣にいる女を信じるな</color>"));
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("リン","なんて書いてあったの？　その紙に。"));
                    //２面選択肢表示  
                    nowPuzzle= true;
                    SelectBox.gameObject.SetActive(true);
                    string[] texts = {"なんでもない","実は…"}; 
                    SelectBox.SetReset("nanntekaiteattano",texts);
                    onnnanoko.SetActive(false);
                    //２面選択肢表示終
                    }
                else if (flag2end)
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
                else if(flag2a && !flag2end){
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","ラジオだ。電源は入っていない。"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","どんな仕掛けになっているんだろう？"));
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("リン","そんなのどうでもいいわ。とにかくこの部屋から出る方法を探すのよ！"));
                    onnnanoko.SetActive(false);}
                else if(flag2end && !flag3a){
                    onnnanoko.SetActive(true);
                    MusicBinder.Musicstop();
                    audioSource.Stop(); 
                    audioSource.volume = 0.1f;
                    audioSource.clip = koukaonClip[3];
                    audioSource.Play();
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","ラジオからノイズが聞こえている。")); 
                    audioSource.Stop(); 
                    yield return StartCoroutine(textcontroller.NormalChat("謎の声","なかなか楽しませてもらったが～…。"));
                    //あとで追加
                    flag3a = true;
                    //※音楽変える
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","そうだ。僕はこのラジオの向こう側の人間が誰なのかも知っている。")); 
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","僕とリンは元々ここではないどこかで閉じ込められていた。"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","そこはここと同じように周囲から遮断された場所で、僕らはそこで奇妙な集団生活をさせられていたのだった。"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","そこでは僕とリンはなぜか「兄」と「妹」という役割を与えられ、家族として生活する事を強制させられた。"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","もちろん最初は抵抗した。"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","昨日までの当たり前があっという間に当たり前でなくなることを知った。"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","「前だって、私さえいなければこんな苦労しなくても逃げきれたはずなのに…」"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","「君たちは本当にできの悪い子だよ。『見送り』が決まったあとも、こうして長い事私の手をわずらわせるだなんて」"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","この声は、僕の「お父さん」だった人の声だ。もっとも、本当のお父さんでは、もちろんないけれど。"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","お父さんの声は、どこかぎこちなく、よく聞けば演技をしているのがバレバレだ。"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","慣れない役を上から仰せつかって、緊張もしているのだろう。"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","「そろそろ余興も終わりだ。時間内に脱出できなかった二人には、『ペナルティ』を与えることにしよう」"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","多分リンは最初から分かっていたんだろう。"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","これは僕達二人の為に特別に用意された罰ゲーム。最初から結末がハッピーエンドなんてありえなかった。"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","そう考えると、最初はあんなに冷たかった僕への態度が変わり、急にうつむき加減になってしまったリンの気持ちもなんとなく僕にはわかるのだった。"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","※効果音：ガスが出る音"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","「ガレージを開けるためのボタンは、押し続けている間だけ開くようになっている。つまりどういうことか、分かるね？」"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","なんのことはない。僕とリン、この部屋から出られるのは一人だけだということだ。"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","だとしたらもう、僕がどうするかは明らかだった。"));
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","「僕は…」"));
                    string[] texts = {"リンを助ける","自分の命を大事にする"}; 
                    SelectBox.SetReset("last_choice",texts);
                    onnnanoko.SetActive(false);}
                else{
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("リン","ラジオね。好きな曲でも聴けたらいいのに。")); 
                    yield return StartCoroutine(textcontroller.NormalChat("Player","電源も入ってないからなあ。")); 
                    onnnanoko.SetActive(false);}
                break;
            case "Small locker.001":
                if (flag0end)
                    yield return StartCoroutine(textcontroller.NormalChat("   ","中には何も入っていない。"));                                
                break;
            case "sentakusi0":
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("リン","やっぱり…。で、今回はどうするつもりなの？"));        
                    yield return StartCoroutine(textcontroller.NormalChat("Player","いや、それは…"));        
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","何も考えないで返事をしてしまった。"));
                    yield return StartCoroutine(textcontroller.NormalChat("リン","あなたの事を信頼してはいるけど…。"));        
                    yield return StartCoroutine(textcontroller.NormalChat("リン","ごめんなさい、もしもの時の事は、まだ考えられてないのよね…。"));
                    onnnanoko.SetActive(false);
                break;
            case "sentakusi1":
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("リン","覚えていないだなんて、そんな事あるかしら"));        
                    yield return StartCoroutine(textcontroller.NormalChat("Player","いや、それは…"));        
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","何も考えないで返事をしてしまった。"));
                    yield return StartCoroutine(textcontroller.NormalChat("リン","あなたのせいでこうなったっていうのに…"));        
                    yield return StartCoroutine(textcontroller.NormalChat("　　","僕のせい…。その言葉だけは、何故か納得がいった。全部僕のせいなんだ。"));
                    onnnanoko.SetActive(false);
                break;
            case "nanntekaiteattano0":
                    yield return StartCoroutine(textcontroller.NormalChat("Player","…なんでもない。"));
                    yield return StartCoroutine(textcontroller.NormalChat("リン","…そう。なら、いいけど…。"));
                    audioSource.clip = koukaonClip[3];
                    yield return StartCoroutine(textcontroller.NormalChat("Player","またこの音だ…。")); 
                    yield return StartCoroutine(textcontroller.NormalChat("リン","ラジオを確認しましょう。"));
                    //radioへ
                break;
            case "nanntekaiteattano1":
                    yield return StartCoroutine(textcontroller.NormalChat("Player","実は…")); 
                    yield return StartCoroutine(textcontroller.NormalChat("Player","…なんでもない。"));
                    yield return StartCoroutine(textcontroller.NormalChat("リン","…そう。なら、いいけど…。"));
                    audioSource.clip = koukaonClip[3];
                    yield return StartCoroutine(textcontroller.NormalChat("Player","またこの音だ…。")); 
                    yield return StartCoroutine(textcontroller.NormalChat("リン","ラジオを確認しましょう。"));
                    //radioへ
                break;
            case "last_choice0":
                    yield return StartCoroutine(textcontroller.NormalChat("Player","実は…")); 
                    yield return StartCoroutine(textcontroller.NormalChat("Player","…なんでもない。"));
                    yield return StartCoroutine(textcontroller.NormalChat("リン","…そう。なら、いいけど…。"));
                    audioSource.clip = koukaonClip[3];
                    yield return StartCoroutine(textcontroller.NormalChat("Player","またこの音だ…。")); 
                    yield return StartCoroutine(textcontroller.NormalChat("リン","ラジオを確認しましょう。"));
                    //radioへ
                break;
            case "last_choice1":
                    yield return StartCoroutine(textcontroller.NormalChat("Player","実は…")); 
                    yield return StartCoroutine(textcontroller.NormalChat("Player","…なんでもない。"));
                    yield return StartCoroutine(textcontroller.NormalChat("リン","…そう。なら、いいけど…。"));
                    audioSource.clip = koukaonClip[3];
                    yield return StartCoroutine(textcontroller.NormalChat("Player","またこの音だ…。")); 
                    yield return StartCoroutine(textcontroller.NormalChat("リン","ラジオを確認しましょう。"));
                    //radioへ
                break;
            default:
                break;
        }
        messagePanel.SetActive(false);
        nowMessage = false;
    }
}