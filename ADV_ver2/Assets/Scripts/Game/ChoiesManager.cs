using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiesManager : MonoBehaviour
{
    private GameObject canvas;
    [SerializeField]
    private AdvanceScenario advanceScenario;

    public void ActiveChoies(bool flg, GameObject canvas,Text[] text,string[] choies)
    {
        this.canvas = canvas;
        canvas.SetActive(flg);
        if (!flg) return;

        for (int i = 0; i < text.Length; i++)
        {
            text[i].text = choies[i];
        }
    }

    public void SelectChoies(int num)
    {
        canvas.SetActive(false);        
        advanceScenario.choiesNum = num;
        advanceScenario.isChoiesScenario = true;
        advanceScenario.GoScenario(true);
    }
}
