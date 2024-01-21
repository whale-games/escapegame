using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemController : MonoBehaviour
{
    [SerializeField] private List<GameObject> panels;
    [SerializeField] private KeyPanel keyPanel;
    [SerializeField] UnityEvent<ItemClickEvent> Event;
    private ItemUtils itemUtils;
    bool clickCancel;

    private void Start() {
        itemUtils = GetComponent<ItemUtils>(); //アイテムUtilsの取得
    }

    //画面クリック時の処理
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {

            if (clickCancel) return;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {

                //アイテムをクリックした時の処理
                if(hit.collider.tag == "Item" && !GameManager.nowMessage){
                    Debug.Log(hit.collider.gameObject.name);
                    string errorMessage = itemUtils.AddItem(hit.collider.gameObject, panels);
                    Event.Invoke(new ItemClickEvent{tag = "Item",name = hit.collider.gameObject.name,errorMessage=errorMessage});
                }

                //アイテムを使用する場所をクリックした時の処理
                if(hit.collider.tag == "ItemUse" && itemUtils.choosingGameObject != null){
                    switch(itemUtils.choosingGameObject.transform.GetChild(0).gameObject.name){
                        case "Simple_02":
                            if(hit.collider.gameObject.name != "Table") return;

                            Event.Invoke(new ItemClickEvent{tag = "ItemUse",name = hit.collider.gameObject.name,errorMessage=null});
                            itemUtils.RemoveItem(itemUtils.choosingGameObject.transform.GetChild(0).gameObject);
                            GameManager.flag1 = true;
                            break;
                    }
                }else if (hit.collider.tag == "ItemUse" && itemUtils.choosingGameObject == null){
                    switch(hit.collider.gameObject.name){
                        case "Camouflage suitcase with relief":
                            Event.Invoke(new ItemClickEvent{tag = "ItemUse",name = hit.collider.gameObject.name,errorMessage=null});
                            if (GameManager.flag3)
                            GameManager.flag2 = true;
                            break;
                        case "Locker":
                            Event.Invoke(new ItemClickEvent{tag = "ItemUse",name = hit.collider.gameObject.name,errorMessage=null});

                            if(GameManager.flag1)
                            GameManager.flag3 = true;

                            if (GameManager.flag2)
                                keyPanel.ActiveKeyPad();
                            break;
                        default:
                            Debug.Log("アイテムを指定していません");
                            break;
                    }
                }
            }

            //アイテムを選択時しているときにパネルの色を戻す
            if(itemUtils.choosingGameObject != null) {
                Image image = itemUtils.choosingGameObject.GetComponent<Image>();
                Color color = itemUtils.HexToRGB("#808080");
                color.a = 0.705f;
                image.color = color;
                itemUtils.choosingGameObject = null;
            }
        }
    }

    //アイテム欄をクリックしたときの処理　ﾖｼｯ
    public void buttonClick(GameObject gameObject)
    {

        clickCancel = false;

        //パネルがリストに入っているか確認
        if(!panels.Contains(gameObject))
        {
            Debug.Log("そのパネルはリストに入っていません");
            return;
        }

        //選択したパネルにアイテムが入っているか確認
        if(gameObject.transform.childCount == 0){
            Debug.Log("そのパネルにはアイテムが入っていません");
            return;
        }


        Image image;

        //選択されていたパネルの色を薄くする
        if(itemUtils.choosingGameObject != null) {
            image = itemUtils.choosingGameObject.GetComponent<Image>();
            Color color = itemUtils.HexToRGB("#808080");
            color.a = 0.705f;
            image.color = color;
        }

        //選択したパネルの色を濃くする
        image = gameObject.GetComponent<Image>();
        image.color = itemUtils.HexToRGB("#FFFFFF");
        itemUtils.choosingGameObject = gameObject;
    }

    public void PointEnter(GameObject gameObject){
        //パネルがリストに入っているか確認
        if(!panels.Contains(gameObject))
        {
            Debug.Log("そのパネルはリストに入っていません");
            return;
        }

        //選択したパネルにアイテムが入っているか確認
        if(gameObject.transform.childCount == 0){
            Debug.Log("そのパネルにはアイテムが入っていません");
            return;
        }

        Debug.Log("アイテム詳細表示中："+gameObject.transform.GetChild(0).gameObject.name);
    }

    public void PointExit(){
        Debug.Log("アイテム詳細非表示");
    }

    public void PointDown(){
        clickCancel = true;
    }
}

public class ItemClickEvent{
    public string tag,name,errorMessage;

}