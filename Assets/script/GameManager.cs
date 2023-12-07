using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private textcontroller textcontroller;
    [SerializeField] private GameObject messagePanel;
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
            default:
                yield return StartCoroutine(textcontroller.NormalChat("テスト","これは取れないようだ。"));
                break;
        }
        messagePanel.SetActive(false);
        nowMessage = false;
    }
}
