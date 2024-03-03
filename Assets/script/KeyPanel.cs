using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class KeyPanel : MonoBehaviour
{
    [SerializeField] GameObject keypad;
    [SerializeField] Image image;
    [SerializeField] Text text;
    private AudioSource audioSource;
    [SerializeField] private AudioClip sound01,sound02;
    string code;
    public string answer;

    private void Start() {
        audioSource = GetComponent<AudioSource>(); //AudioSourceの取得
    }
    public async void PushKey(int number){
        //確定
        if (number == 200){
            if (code == answer){
                //正解時
                audioSource.volume = 1;
                audioSource.clip = sound01;
                audioSource.Play();
                image.color = Color.blue;
                code = "";
                text.text = code;
                await Task.Delay(1000);
                keypad.SetActive(false);
                GameManager.flag20 = true;
                GameManager.nowPuzzle= false;               
            }else{
                //失敗時
                audioSource.volume = 1;
                audioSource.clip = sound02;
                audioSource.Play();
                image.color = Color.red;
                code = "";
                text.text = code;
                await Task.Delay(1000);
                image.color = Color.black;
            }
            return;
        }

        //戻る処理
        if (number == -1){
            GameManager.nowPuzzle= false;  
            keypad.SetActive(false);
            return;
        }

        code += number.ToString();

        //もし9文字(枠上限)を超えたら、errorを出す。
        if (code.Length > 9){
            image.color = Color.red;
            code = "";
            text.text = code;
            await Task.Delay(1000);
            image.color = Color.black;
            return;
        }

        text.text = code;
    }

    public void ActiveKeyPad(){
        keypad.SetActive(true);
    }


}
