using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField] private List<GameObject> panels;
    [SerializeField] private KeyPanel keyPanel;
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
                if(hit.collider.tag == "Item"){
                    int returncode = itemUtils.AddItem(hit.collider.gameObject, panels);
                    //エラー処理
                    //-1 配列がいっぱい
                    switch(returncode){
                        case -1:
                            Debug.Log("配列がいっぱいです。");
                            break;
                    }
                }

                if(hit.collider.gameObject.name == "Table"){
                    keyPanel.ActiveKeyPad();
                }

                //アイテムを使用する場所をクリックした時の処理
                if(hit.collider.tag == "ItemUse" && itemUtils.choosingGameObject != null){
                    itemUtils.RemoveItem(itemUtils.choosingGameObject.transform.GetChild(0).gameObject);
                    Debug.Log("アイテムを使用した！");
                }else if (itemUtils.choosingGameObject == null){
                    Debug.Log("アイテムを指定していません");
                }
            }

            //アイテムを選択時しているときにパネルの色を戻す
            if(itemUtils.choosingGameObject != null) {
                Image image = itemUtils.choosingGameObject.GetComponent<Image>();
                image.color = itemUtils.HexToRGB("#808080");
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
            image.color = itemUtils.HexToRGB("#808080");
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