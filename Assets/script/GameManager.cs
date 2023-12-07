using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private textcontroller textcontroller;
    [SerializeField] private GameObject messagePanel;
    public static bool flag1,flag2,flag3;
    public static bool nowMessage;
    public void TestClick2(ItemClickEvent clickEvent){
        if(clickEvent.tag == "Item")
            StartCoroutine("TestClick",clickEvent);
        else
            StartCoroutine("ItemUseClick",clickEvent);
    }
    public IEnumerator TestClick(ItemClickEvent itemClickEvent){
        messagePanel.SetActive(true);
        nowMessage = true;
        switch(itemClickEvent.name){
            case "Drill Bits.001":
                yield return StartCoroutine(textcontroller.NormalChat("テスト","とてもねむいです。"));
                break;
            case "Simple_02":
                yield return StartCoroutine(textcontroller.NormalChat("Player","これは何かの鍵の様だ"));
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
            case "Table":
                yield return StartCoroutine(textcontroller.NormalChat("Player","紙だ。「1234」と書いてある"));
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
            default:
                break;
        }
        messagePanel.SetActive(false);
        nowMessage = false;
    }
}
