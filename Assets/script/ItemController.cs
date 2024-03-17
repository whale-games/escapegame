using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemController : MonoBehaviour
{
    [SerializeField] private List<GameObject> panels;
    [SerializeField] private KeyPanel keyPanel;
     private AudioSource audioSource;
 	[SerializeField] private AudioClip sound01,sound02;
    [SerializeField] UnityEvent<ItemClickEvent> Event;
    
    private ItemUtils itemUtils;
    bool clickCancel;
    private void Start() {
        itemUtils = GetComponent<ItemUtils>(); //アイテムUtilsの取得
        audioSource = GetComponent<AudioSource>(); //AudioSourceの取得

    }

    //画面クリック時の処理
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (GameManager.flag0 != true) return;
            if (clickCancel) return;
            if (GameManager.nowMessage) return;//アイテム触ってる時に他の反応ブロック
            if (GameManager.nowPuzzle) return;//アイテム触ってる時に他の反応ブロック
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {

                //アイテムをクリックした時の処理
                if(hit.collider.tag == "Item" && !GameManager.nowMessage){
                    Debug.Log(hit.collider.gameObject.name);
                    string errorMessage = itemUtils.AddItem(hit.collider.gameObject, panels);
                    audioSource.volume = 0.5f;
                    audioSource.clip = sound01;
                    audioSource.Play();
                    Event.Invoke(new ItemClickEvent{tag = "Item",name = hit.collider.gameObject.name,errorMessage=errorMessage});
                }

                //アイテムを使用する場所をクリックした時の処理
                if(hit.collider.tag == "ItemUse" && itemUtils.choosingGameObject != null){
                    switch(itemUtils.choosingGameObject.transform.GetChild(0).gameObject.name){
                        case "Simple_02":
                            if(hit.collider.gameObject.name != "Table") return;
                            //一面スタート
                            GameManager.flag1 = true;
                            audioSource.volume = 1;
                            audioSource.clip = sound02;
                            audioSource.Play();
                            Event.Invoke(new ItemClickEvent{tag = "ItemUse",name = hit.collider.gameObject.name,errorMessage=null});
                            itemUtils.RemoveItem(itemUtils.choosingGameObject.transform.GetChild(0).gameObject);
                            break;
                    }
                }else if (hit.collider.tag == "ItemUse" && itemUtils.choosingGameObject == null){
                    switch(hit.collider.gameObject.name){
                        case "Camouflage suitcase with relief":
                            Event.Invoke(new ItemClickEvent{tag = "ItemUse",name = hit.collider.gameObject.name,errorMessage=null});
                            if (GameManager.iflag1 && GameManager.flag1b && !GameManager.flag1c)
                              GameManager.flag1c = true;
                            break;
                        case "Locker":
                            Event.Invoke(new ItemClickEvent{tag = "ItemUse",name = hit.collider.gameObject.name,errorMessage=null});
                            if(GameManager.iflag1 && GameManager.flag1a)
                                GameManager.flag1b = true;
                            if (GameManager.flag1c){
                                GameManager.nowPuzzle = true;                               
                                keyPanel.ActiveKeyPad();}
                            break;
                        case "Table":
                            Event.Invoke(new ItemClickEvent{tag = "ItemUse",name = hit.collider.gameObject.name,errorMessage=null});
                            break;
                        case "Small locker.002":
                            Event.Invoke(new ItemClickEvent{tag = "ItemUse",name = hit.collider.gameObject.name,errorMessage=null});
                            if (GameManager.flag0)
                              Debug.Log("Small locker.002flag=true");
                              GameManager.flag2c = true;
                            break;
                        default:
                            Debug.Log("アイテムを指定していません");
                            break;
                    }
                }
                            //使用不可アイテムをクリックした時の処理
                if(hit.collider.tag == "Wrongitem" && !GameManager.nowMessage){
                    Debug.Log(hit.collider.gameObject.name);
                    Event.Invoke(new ItemClickEvent{tag = "Wrongitem",name = hit.collider.gameObject.name,errorMessage=null});
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
        Debug.Log("アイテム欄クリック");
        clickCancel = false;
        if (GameManager.nowMessage) return;//アイテム触ってる時に他の反応ブロック
        if (GameManager.nowPuzzle) return;//アイテム触ってる時に他の反応ブロック
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