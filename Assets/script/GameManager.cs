using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private textcontroller textcontroller;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private GameObject onnnanoko;
    public static bool nowMessage;
    public void TestClick2(ItemClickEvent clickEvent){
        StartCoroutine("TestClick",clickEvent);
    }
    public IEnumerator TestClick(ItemClickEvent itemClickEvent){
        messagePanel.SetActive(true);
        nowMessage = true;
        switch(itemClickEvent.name){
            case "Drill Bits.001":
                yield return StartCoroutine(textcontroller.NormalChat("テスト","とてもねむいです。"));
                break;

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
            case "2 sockets":
            //キャラクター表示
            onnnanoko.SetActive(true);
                yield return StartCoroutine(textcontroller.NormalChat("女の子","電気は通っているようだけどあなたと心は通わないわね。"));
            //キャラクター消去
            onnnanoko.SetActive(false);
                break;            
            default:
                yield return StartCoroutine(textcontroller.NormalChat("テスト","これは取れないようだ。"));
                break;
        }
        messagePanel.SetActive(false);
        nowMessage = false;
    }
}
