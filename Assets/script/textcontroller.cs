using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textcontroller : MonoBehaviour
{
    public Text ChatText;
    public Text CharacterName;

    public string writerText = "";

    void Start()
    {
        //StartCoroutine(Textout());
    }

    public IEnumerator NormalChat(string narrator, string narration)
    {
        int count = 0;
        CharacterName.text = narrator;
        writerText = "";

        for (count = 0; count < narration.Length; count++)
        {
            while(true){
                if(narration[count] == '<'){
                    if(narration[count+1] == '/')
                        narration = narration.Remove(count, narration.IndexOf(">") - count + 1);
                    else{
                        writerText += narration.Substring(count, narration.IndexOf(">") - count + 1);
                        narration = narration.Remove(count, narration.IndexOf(">") - count + 1);
                        writerText += narration.Substring(narration.IndexOf("<"), narration.IndexOf(">") - narration.IndexOf("<") + 1);
                    }
                }
                if(count >= narration.Length) break;
                if(narration[count] != '<') break;
            }
            if(count < narration.Length){
                if(count < narration.IndexOf("</"))
                    writerText = writerText.Insert(writerText.Length-8,narration[count].ToString());
                else
                    writerText += narration[count];
            }
            ChatText.text = writerText;
            yield return null;
        }
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                break;
            }
            yield return null;
        }

    }

    IEnumerator Textout()
    {
        yield return StartCoroutine(NormalChat("�e�X�g","�ƂĂ��˂ނ��ł��B"));
        yield return StartCoroutine(NormalChat("�e�X�g", "�m�F���Ă�������"));
        yield return StartCoroutine(NormalChat("�e�X�g", "���v�ł���"));
        yield return StartCoroutine(NormalChat("�e�X�g", "�ǂ��ł���"));
    }
}
