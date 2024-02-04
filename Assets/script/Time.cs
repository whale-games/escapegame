using UnityEngine;

public class Time : MonoBehaviour
{
    [SerializeField] GameObject longBar,shortBar;
    [SerializeField] int longTarget,shortTarget;
    private int longtime=0,shorttime=0;

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
        }
    }
}
