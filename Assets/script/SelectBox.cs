using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SelectBox : MonoBehaviour
{
    [SerializeField] Text[] selectTexts;
    [SerializeField] UnityEvent<ItemClickEvent> Event;
    private string selectTag = "";

    /*
        string[] string = {"","",""};選択肢を書く
        selectBox.SetReset(string, string[])リセット関数を実行する(好きな名前と選択肢が入った変数)

        @return 名前と選択された番号1~3の文字列を返される
        
        例
        string[] texts = {"a","b","c"}; 
        selectBox.SetReset("test",texts);

        上記の場合、1つめのボタンを押せば"test1"が返る。二つ目を押した場合は"test2"、三つめは"test3" 
    */

    public void SetReset(string tag,string[] texts){
        selectTag = tag;
        for(int i = 0;i<selectTexts.Length;i++){
            selectTexts[i].text = texts[i];
        }
    }

    public void onClickButton(int value){
        Event.Invoke(new ItemClickEvent{tag = "Wrongitem",name = selectTag+value.ToString(),errorMessage=null});
        gameObject.SetActive(false);
    }
}
