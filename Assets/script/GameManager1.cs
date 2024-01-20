using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// MonoBehaviourを継承することでオブジェクトにコンポーネントとして
// アタッチすることができるようになる
public class GameManager1 : MonoBehaviour
{
   // SerializeFieldと書くとprivateなパラメーターでも
   // インスペクター上で値を変更できる
   [SerializeField]
   private Text mainText;
   [SerializeField]
   private Text nameText;
   private string _text = "Hello,World!";
   
   // MonoBehaviourを継承している場合限定で
   // 最初の更新関数(Updateメソッド)が呼ばれる時に最初に呼ばれる
   private void Start()
   {
       // Main Textに指定したTextコンポーネントの
       // テキストのパラメーターに代入する
       mainText.text = _text;
   }
}
