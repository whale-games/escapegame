using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RemoteConroler : MonoBehaviour
{
    [SerializeField] int[] counts = new int[7],answers = new int[7];

    public void pushButton(int number){
        if(number == -1){
            counts = new int[7];
            return;
        }
        counts[number]++;
        if(counts.SequenceEqual(answers)){
            Debug.Log("ok");
        }
    }
}