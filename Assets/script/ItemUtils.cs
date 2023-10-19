using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUtils : MonoBehaviour{

    public List<string> itemList = new List<string>(5);
    public GameObject choosingGameObject = null;


        //アイテムを持ち物に追加
    public int AddItem(GameObject item, List<GameObject> itemPanelList)
    {
        //要素数が最大の場合に戻す
        if(itemList.Count == 5){
            return -1;
        }
        itemList.Add(item.name);

        item.transform.parent = itemPanelList[itemList.IndexOf(item.name)].transform;
        item.transform.localPosition = new Vector3(0,0,-11);
        item.transform.localScale = new Vector3(500,500,500);
        item.layer = 6;
        return 0;
    }

    //アイテムを削除
    public int RemoveItem(GameObject item)
    {
        itemList.Remove(item.name);
        Destroy(item);
        return 0;
    }

    //カラーコードからColor型に変換
    public Color HexToRGB(string hex)
    {
        if(ColorUtility.TryParseHtmlString(hex,out Color color)) return color;
        else return Color.black;
    }

    //テキスト取得
    IEnumerator NormalChat(string narrator, string narration)
    {
        int count = 0;
        CharacterName.text = narrator;
        writerText = "";

        for (count = 0; count < narration.Length; count++)
        {
            writerText += narration[count];
            ChatText.text = writerText;
            yield return null;
        }

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                break;
            }
            yield return null;
        }

    }

    IEnumerator Textout()
    {
        yield return StartCoroutine(NormalChat("テスト", "とてもねむいです。"));
    }
}
