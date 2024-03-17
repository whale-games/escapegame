using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBinder : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] musicClip;//配列使用参考
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); //AudioSourceの取得
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Musicplay(){
    if (!GameManager.flag0end && !GameManager.flag1end && !GameManager.flag2end && !GameManager.flag3end && !GameManager.flag4){
    Debug.Log("musicClip[0]play到達");
    audioSource.Stop();
    audioSource.volume = 0.3f;
    audioSource.clip = musicClip[0];
    audioSource.Play();}
    else if(GameManager.flag0end && !GameManager.flag1end && !GameManager.flag2end && !GameManager.flag3end && !GameManager.flag4end){
    Debug.Log("musicClip[1]");
    audioSource.Stop();
    audioSource.volume = 0.1f;
    audioSource.clip = musicClip[1];
    audioSource.Play();}
    else if(GameManager.flag0end && GameManager.flag1end && !GameManager.flag2end && !GameManager.flag3end && !GameManager.flag4end){
    Debug.Log("musicClip[2]");
    audioSource.Stop();
    audioSource.volume = 0.1f;
    audioSource.clip = musicClip[2];
    audioSource.Play();}
    else if(GameManager.flag0end && GameManager.flag1end && GameManager.flag2end && !GameManager.flag3end && !GameManager.flag4end){
    Debug.Log("musicClip[3]");
    audioSource.Stop();
    audioSource.volume = 0.3f;
    audioSource.clip = musicClip[3];
    audioSource.Play();}
    else if(GameManager.flag0end && GameManager.flag1end && GameManager.flag2end && GameManager.flag3end && !GameManager.flag4end){
    Debug.Log("musicClip[4]");
    audioSource.Stop();
    audioSource.volume = 0.3f;
    audioSource.clip = musicClip[4];
    audioSource.Play();}
    else 
    return;
    }  
    public void Musicstop(){
    Debug.Log("Musicstop到達");
    audioSource.Stop();}
  
}
