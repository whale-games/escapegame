using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private textcontroller textcontroller;
    [SerializeField] private GameObject messagePanel;
    public static bool flag0,flag1,flag2,flag3,flag20,flag21,flag22,flag23,flag30,flag301,flag31,flag32,flag33;
    private AudioSource audioSource;
 	[SerializeField] private AudioClip[] audioClip;//配列使用参考
    [SerializeField] private AudioClip[] koukaonClip;//配列使用参考
    [SerializeField] private GameObject onnnanoko;
    [SerializeField] private GameObject Clock;
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
                yield return StartCoroutine(textcontroller.NormalChat("Player","ここは…？"));
                yield return StartCoroutine(textcontroller.NormalChat("　　　","目が覚めると僕は見知らぬ場所にいた。"));                
                onnnanoko.SetActive(true);
                //女性ボイス
                audioSource.Stop();
                audioSource.volume = 0.9f;
                audioSource.clip = audioClip[0];
                audioSource.Play();
                yield return StartCoroutine(textcontroller.NormalChat("女の子","あら、ようやくお目覚め？"));
                audioSource.Stop(); 
                yield return StartCoroutine(textcontroller.NormalChat("Player","君は…？"));
                //女性ボイス
                audioSource.volume = 0.9f;
                audioSource.clip = audioClip[1];
                audioSource.Play();
                yield return StartCoroutine(textcontroller.NormalChat("女の子","もしかして、覚えてないの？　昨日の事"));
                audioSource.Stop();
                yield return StartCoroutine(textcontroller.NormalChat("　　　","…なんの事だろう？"));
                yield return StartCoroutine(textcontroller.NormalChat("　　　","なんにも覚えていないなあ。ああ、覚えてない覚えてない。"));
                yield return StartCoroutine(textcontroller.NormalChat("Player","とにかく僕はもう帰りたいんだ。ここは僕のいるべき場所じゃない。"));
                //女性ボイス
                audioSource.Stop();
                audioSource.volume = 0.9f;
                audioSource.clip = audioClip[2];
                audioSource.Play(); 
                yield return StartCoroutine(textcontroller.NormalChat("女の子","…そんなに簡単にここから出られるとは思えないけど。"));
                audioSource.Stop();
                yield return StartCoroutine(textcontroller.NormalChat("　　　","なんなんだこの女は。"));
                yield return StartCoroutine(textcontroller.NormalChat("　　　","部屋を見渡せばすぐに出ていく事ができるはずだ。"));
                onnnanoko.SetActive(false);
                //※赤文字に変更？
                yield return StartCoroutine(textcontroller.NormalChat("　　　","画面上にある左右の矢印ボタンを押すことで視点を変えることができます。"));
                //※シャッター
                flag0 = true;
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
                yield return StartCoroutine(textcontroller.NormalChat("女の子","どこで使うのかしら。"));
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
            //シナリオ１部分
            case "Table":
                if (flag1){
                    yield return StartCoroutine(textcontroller.NormalChat("Player","鍵が開いた。"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","紙だ。「1234」と書いてある"));}
                else if (flag1 && flag2)
                    yield return StartCoroutine(textcontroller.NormalChat("Player","机の引き出しにあった数字は1234…。スーツケースの中のメモに「机の引き出しの数字は逆」これらを組み合わせると…。"));
                else
                    yield return StartCoroutine(textcontroller.NormalChat("Player","鍵穴が空いている。中を見るには鍵が必要だ。"));               
                break;
            case "Camouflage suitcase with relief":
                if (flag3)
                yield return StartCoroutine(textcontroller.NormalChat("Player","紙だ。「机の引き出しの数字は逆」と書いてある"));
                break;
            case "Locker":
                if (!flag1 && !flag2)
                    yield return StartCoroutine(textcontroller.NormalChat("Player","キーパネルだ。何を入力していいのか分からない。…とにかく部屋を探そう！"));
                else if (flag1 && !flag2)
                    yield return StartCoroutine(textcontroller.NormalChat("Player","1234と入力してみたが反応がない。この数字は関係ないのだろうか？　もしくは他になにか情報が必要なのか？"));
                else if (!flag1 && flag2)
                    yield return StartCoroutine(textcontroller.NormalChat("Player","「机の引き出しの数字は逆」というメモが気になる。まずは机を探してみよう！"));
                //else if (flag1 && flag2)
                //    yield return StartCoroutine(textcontroller.NormalChat("Player","机の引き出しにあった数字は1234…。スーツケースの中のメモに「机の引き出しの数字は逆」これらを組み合わせると…。"));
                break;
                //シナリオ２部分
        　　　　//シナリオ３部分
        　　　　//ハズレアイテムタッチ時
                //それ以外   
            case "Drill Bits.002":
                //キャラクター表示
                onnnanoko.SetActive(true);
                yield return StartCoroutine(textcontroller.NormalChat("女の子","とてもねむいわね。"));
                //キャラクター消去
                onnnanoko.SetActive(false);   
                break;
            case "Cone Drill Bits":
                //キャラクター表示
                onnnanoko.SetActive(true);
                yield return StartCoroutine(textcontroller.NormalChat("女の子","こんなものは何の役にも立たないわ。"));
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
              yield return StartCoroutine(textcontroller.NormalChat("女の子","電気は通っているようだけどあなたと心は通わないわね。"));
              onnnanoko.SetActive(false);
              break;
            case "Hose":
                if (flag20)
                    yield return StartCoroutine(textcontroller.NormalChat("Player","ただのホースのようだ。これも脱出に何か関係あるのだろうか。"));
                else
                    yield return StartCoroutine(textcontroller.NormalChat("Player","ただのホースのようだ。"));
                break;
            case "Emissive window":
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","開かない。強化ガラスなのかとても頑丈そうでここからは出られそうにない…。"));
                    //キャラクター表示
                    onnnanoko.SetActive(true);
                    yield return StartCoroutine(textcontroller.NormalChat("Player","ここからは出れないか…"));
                    yield return StartCoroutine(textcontroller.NormalChat("女の子","まーどーでもいいじゃない、窓だけに。"));
                    //キャラクター消去
                    onnnanoko.SetActive(false);
                    yield return StartCoroutine(textcontroller.NormalChat("Player","！？"));
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
                    yield return StartCoroutine(textcontroller.NormalChat("女の子","出られそうにないわね。"));
                    audioSource.Stop();
                    yield return StartCoroutine(textcontroller.NormalChat("Player","…何か知っているの？"));   
                    //女性ボイス
                    audioSource.volume = 0.9f;
                    audioSource.clip = audioClip[4];
                    audioSource.Play(); 
                    yield return StartCoroutine(textcontroller.NormalChat("女の子","別に。あなたより少し早く起きたから、それくらいは先に調べていたってだけよ。"));
                    audioSource.Stop();
                    yield return StartCoroutine(textcontroller.NormalChat("Player","じゃあ何も知らないのか。どうやってここから出ればいいんだ？"));
                    //女性ボイス
                    audioSource.volume = 0.9f;
                    audioSource.clip = audioClip[5];
                    audioSource.Play(); 
                    yield return StartCoroutine(textcontroller.NormalChat("女の子","部屋の中を色々と調べてみましょう。"));
                    audioSource.Stop();
                    //女性ボイス
                    audioSource.volume = 0.9f;
                    audioSource.clip = audioClip[6];
                    audioSource.Play(); 
                    yield return StartCoroutine(textcontroller.NormalChat("女の子","何か脱出の手がかりがあるかもしれないから。"));
                    audioSource.Stop();
                    yield return StartCoroutine(textcontroller.NormalChat("Player","そうするしかなさそうだな…。"));
                    //女性ボイス
                    audioSource.volume = 0.9f;
                    audioSource.clip = audioClip[7];
                    audioSource.Play();
                    yield return StartCoroutine(textcontroller.NormalChat("女の子","素直ね。"));
                    audioSource.Stop();
                    yield return StartCoroutine(textcontroller.NormalChat("Player","そうかな。そうかもしれない。"));
                    //女性ボイス
                    audioSource.volume = 0.9f;
                    audioSource.clip = audioClip[8];
                    audioSource.Play();
                    yield return StartCoroutine(textcontroller.NormalChat("女の子","あなたって主義主張がなさそうな顔してるものね"));
                    audioSource.Stop();
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","そうだろうか？　言われてみたらそうかもしれない。僕には大事にしているものなんて何もないんだ。"));
                    yield return StartCoroutine(textcontroller.NormalChat("Player","とにかく部屋の中を探そう。"));
                    onnnanoko.SetActive(false);
                break;
            case "Small stool":
                //クリア前
                if (!flag30){
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","裏側に時計がある。"));
                    Clock.SetActive(true);}
                //クリア後
                else if (flag30 && !flag301){
                    Clock.SetActive(false);
                    audioSource.Stop(); 
                    audioSource.volume = 0.5f;
                    audioSource.clip = koukaonClip[0];
                    audioSource.Play();
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","仕掛けが解除された。"));
                    flag301 = true;}

                else if (flag30 && flag301)
                    yield return StartCoroutine(textcontroller.NormalChat("　　　","時計の仕掛けは解除済だ。"));
                break;    
            default:
                break;
        }
        messagePanel.SetActive(false);
        nowMessage = false;
    }
}
