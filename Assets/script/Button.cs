using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] List<Counts> answer,counts;

    public void clickButton(string name){

        if(name == "reset")
            counts.Clear();

        if(counts.Count == 0){
            counts.Add(new Counts(){name = name,count = 1});
            return;
        }

        if(counts.Last().name == name){
            counts.Last().count++;
        }else{
            counts.Add(new Counts(){name = name,count = 1});
        }

        if(answerCheck())
            Debug.Log("ok");

    }

    bool answerCheck(){

        if(counts.Count < answer.Count) return false;

        for(int i = 0;i<answer.Count;i++){
            if(counts[i].name != answer[i].name || counts[i].count != answer[i].count)
                return false;
        }

        return true;
    }

    [System.Serializable]
    class Counts{
        public string name;
        public int count = 0;
    }
}
