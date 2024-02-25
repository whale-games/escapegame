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
                writerText += narration[count];
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
