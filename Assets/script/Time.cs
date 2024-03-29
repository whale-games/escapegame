using UnityEngine;
using UnityEngine.Events;

public class Time : MonoBehaviour
{
    [SerializeField] GameObject longBar,shortBar;
    [SerializeField] int longTarget,shortTarget;
    [SerializeField] UnityEvent<ItemClickEvent> Event;
    private int longtime=0,shorttime=0;
    private void OnEnable() {
        GameManager.nowPuzzle = true;
    }
    public void click(int number){
        switch(number){
            case 0:
                longBar.transform.Rotate(0,0,-30);
                longtime++;
                break;
            case 1:
                shortBar.transform.Rotate(0,0,-30);
                shorttime++;
                break;
        }

        if(longtime == 12) longtime = 0;
        if(shorttime == 12) shorttime = 0;

        if(longtime == longTarget && shorttime == shortTarget){
            Debug.Log("完了");
            //正解時
            GameManager.flag2end = true;
            GameManager.flag2enda = true;
            Event.Invoke(new ItemClickEvent{tag = "Wrongitem",name = "Small stool",errorMessage=null});
            GameManager.nowPuzzle = false; 
        }
    }

    public void Back(){
        Debug.Log("時計戻る");
        GameManager.nowPuzzle= false;
        transform.parent.gameObject.SetActive(false);        
    }
}
