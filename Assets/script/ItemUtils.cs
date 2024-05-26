using System.Collections.Generic;
using UnityEngine;

public class ItemUtils : MonoBehaviour{
    public List<string> itemList = new List<string>(5);
    public GameObject choosingGameObject = null;

        //アイテムを持ち物に追加
    public string AddItem(GameObject item, List<GameObject> itemPanelList)
    {
        //要素数が最大の場合に戻す
        if(itemList.Count == 5){
            return "配列がいっぱいです。";
        }
        //
        itemList.Add(item.name);
        item.transform.parent = itemPanelList[itemList.IndexOf(item.name)].transform;
        item.transform.localPosition = new Vector3(0,0,-11);
        item.transform.localScale = new Vector3(500,500,500);
        item.transform.rotation = Quaternion.identity;
        //サイズの指定↓
        //ItemController.csの「3面素材作成ギミック（タグ書き換え→Item）」にあるオブジェクト名の物の大きさを調整する。↓
        switch(item.name){
            case "Hooks":
        item.transform.localPosition = new Vector3(0,0,-11);
        item.transform.localScale = new Vector3(100,100,100);
        item.transform.rotation = Quaternion.identity;
            break;
            case "Bench Grinder":            
        item.transform.localPosition = new Vector3(25,0,-11);
        item.transform.localScale = new Vector3(250,250,250);
        item.transform.rotation = Quaternion.identity;
            break;
            }

        //
        item.layer = 6;
        return "0";
    }

    //アイテムを削除
    public int RemoveItem(GameObject item,List<GameObject> itemPanelList)
    {
        for(int i = itemList.IndexOf(item.name);i <= 3;i++){
            if (itemPanelList[i].transform.childCount > 1){
                Transform transform = itemPanelList[i+1].transform.GetChild(0).gameObject.transform;
                transform.parent = itemPanelList[i].transform;
                transform.localPosition = new Vector3(0,0,0);
            }
        }
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

}
