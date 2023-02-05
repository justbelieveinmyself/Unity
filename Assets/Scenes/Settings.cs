using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Settings : MonoBehaviour
{
    public TextMeshProUGUI currentDif;
    int numberDif = 0;
    public void Difficult(){
        string[] difficults = {"Легкая","Средняя","Тяжелая"};
        numberDif ++;
        currentDif.text = difficults[numberDif];
        if (numberDif == 2) numberDif = -1;
    }
}
