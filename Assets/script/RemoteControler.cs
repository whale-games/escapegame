using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RemoteControler : MonoBehaviour
{
    [SerializeField] GameObject upBtn,downBtn;
    [SerializeField] int[] counts = new int[7],answers = new int[7];
    [SerializeField] UnityEvent<ItemClickEvent> Event;
    Text buttonText;    //Textコンポーネント
    int counter = 25;	//数を数えるカウンター

    void Start(){
	//Buttonの子要素からTextコンポーネントを探してbuttonTextに代入
	buttonText = transform.Find("Button").GetComponentInChildren<Text>();
	buttonText.text = counter.ToString(); //buttonTextのtextプロパティを更新
    }

    public void pushButton(int number){
        // if(number == -1){
        //     counts = new int[7];
        //     return;
        // }
        // counts[number]++;
        // if(counts.SequenceEqual(answers)){
        //     Debug.Log("ok");        }
        // foreach(int count in counts){
        //     if(count >=10){
        //         Debug.Log("10以上。");
        //     //GameManager.rimokonflag1 = true;
        //     //new～Airconditionar

        //     }
        // }
        if(number == -1){
            counter = 25;
            buttonText.text = counter.ToString();  //buttonText を更新
        }
        else if(number == 5){
            if(counter >= 40){
                return;
            }
            counter++;                             //counter を1ずつ加算
            Debug.Log(counter);
            buttonText.text = counter.ToString();  //buttonText を更新
        }
        else if(number == 6){
            if(counter <= 20){
                return;
            }
            counter--;                             //counter を1ずつ加算
            Debug.Log(counter);
            buttonText.text = counter.ToString();  //buttonText を更新
        }
    }
    public void Back(){
    Debug.Log("リモコン戻る");
    GameManager.nowPuzzle= false;
    transform.parent.gameObject.SetActive(false);
    // return;       
    }
}