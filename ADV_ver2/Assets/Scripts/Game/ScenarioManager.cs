using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScenarioManager : MonoBehaviour
{
    [SerializeField]
    private float scenarioTime;
    [SerializeField]
    private AdvanceScenario advanceScenario;
    [SerializeField]
    private GameDirecter gameDirecter;

    public void ChangeScenario(Text text,string scenario)
    {
        gameDirecter.AddIsPlayingList();
        text.text = " ";        
        text.DOText(scenario,scenarioTime).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                gameDirecter.SwitchIsPlayingList();
            });
    }

    public void ChangeCharacterName(Text text, string characterName)
    {
        text.text = characterName;
    }
}
