using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private textcontroller textcontroller;
    [SerializeField] private GameObject messagePanel;
    public static bool flag0,flag1,flag2,flag3;
    [SerializeField] private GameObject onnnanoko;
    public static bool nowMessage;

    public void Start(){
                StartCoroutine("StartTalk"); 
    }
    public IEnumerator StartTalk(){
        messagePanel.SetActive(true);
        nowMessage = true;
                yield return StartCoroutine(textcontroller.NormalChat("Player","〇〇××"));
                onnnanoko.SetActive(true);
                yield return StartCoroutine(textcontroller.NormalChat("女の子","ほいほいほほい"));
                yield return StartCoroutine(textcontroller.NormalChat("Player","なんだかいやな感じの女の子だなあ"));
                yield return StartCoroutine(textcontroller.NormalChat("女の子","時間がないからさっさと脱出方法を探しましょう。"));
                onnnanoko.SetActive(false);
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
                //キャラクター表示
                onnnanoko.SetActive(true);
                yield return StartCoroutine(textcontroller.NormalChat("女の子","何かあるわね。"));
                //キャラクター消去
                onnnanoko.SetActive(false);   
                yield return StartCoroutine(textcontroller.NormalChat("Player","何かの鍵を入手した。"));
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
                if (flag1)
                    yield return StartCoroutine(textcontroller.NormalChat("Player","紙だ。「1234」と書いてある"));
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
                else if (flag1 && !flag2)
                    yield return StartCoroutine(textcontroller.NormalChat("Player","机の引き出しにあった数字は1234…。スーツケースの中のメモに「机の引き出しの数字は逆」これらを組み合わせると…。"));
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

    public IEnumerator WrongItemUseClick(ItemClickEvent itemClickEvent){
        messagePanel.SetActive(true);
        nowMessage = true;
        switch(itemClickEvent.name){
            case "2 sockets":
            onnnanoko.SetActive(true);
            yield return StartCoroutine(textcontroller.NormalChat("女の子","電気は通っているようだけどあなたと心は通わないわね。"));
            onnnanoko.SetActive(false);
            break;
            default:
                break;
        }
        messagePanel.SetActive(false);
        nowMessage = false;
    }
}
