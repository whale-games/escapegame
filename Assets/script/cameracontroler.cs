using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  Transform cameraTransform;
  void Start()
  {
    cameraTransform = Camera.main.transform;
  }
  // カメラをY軸方向に回転させるメソッド
  // ボタンを通じて呼び出すので、このスクリプト内では定義しておくだけ
  public void RotateCamera(float angle)
  {
    if (GameManager.flag0 != true) return;
    if (GameManager.nowMessage) return;//メッセージが表示されてる時にカメラが回転しないようにする。
    if (GameManager.nowPuzzle) return;//パズルを解いている間はカメラが回転しないようにする。
    if(angle == 0)
    {
      return;
    }
    cameraTransform.localRotation = Quaternion.Euler(Vector3.up * angle) * cameraTransform.localRotation;
  }
}
