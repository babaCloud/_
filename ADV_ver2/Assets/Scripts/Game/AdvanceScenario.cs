using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AdvanceScenario : MonoBehaviour
{
    [SerializeField, Header("画面内の変更するもの")]
    private Image[] characterImage;
    [SerializeField]
    private Image subCharacterImage;
    [SerializeField]
    private Image subCharacter2Image;
    [SerializeField]
    private Text talkingCharacterNameText;
    [SerializeField]
    private Text scenarioText;
    [SerializeField]
    private Image backImage;

    [SerializeField, Header("待ち時間")]
    private float waitTime;

    [SerializeField, Header("選択肢")]
    private GameObject choiesCanvas;
    [SerializeField]
    private Text[] choiesText;

    [SerializeField, Header("データ")]
    private ScenarioData scenarioData;

    [SerializeField, Header("参照先クラス")]
    private ScenarioManager scenarioManager;
    [SerializeField]
    private ChoiesManager choiesManager;
    [SerializeField]
    private CharacterManager characterManager;
    [SerializeField]
    private GameDirecter gameDirecter;


    private bool isCanClick = false;//クリックして次に進めることが可能か
    private bool isChoies = false;//次クリックしたら選択肢か
    public bool isChoiesScenario { set; private get; } = false;//分岐ストリー中か
    private int scenarioCount = 0;
    private string[] choies = new string[3];//選択肢
    public int choiesNum { set; private get; } = -1;//どの選択肢を選んだか
    private int choiesScenarioCount = 0;

    void Start()
    {
        scenarioText.text = "";
        choiesCanvas.SetActive(false);
        GoScenario(true);
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !choiesCanvas.activeSelf)
        {
            GoScenario(false);
        }

        if (gameDirecter.JudgeCanPlay())
        {
            gameDirecter.ClearIsPlayingList();
            StartCoroutine("JudgeNext");
        }
    }

    public void GoScenario(bool isFirst)
    {
        if (!isFirst)
        {
            if (!isCanClick) return;
            isCanClick = false;
        }
        

        //選択肢
        choiesManager.ActiveChoies(isChoies && !isChoiesScenario, choiesCanvas, choiesText, choies);
        if (isChoiesScenario)
        {
            //分岐ストーリー終了
            if (choiesScenarioCount == scenarioData.choies[0].choiesSet[choiesNum].choiesScenario.Count)
            {
                isChoies = false;
                isChoiesScenario = false;
                scenarioCount++;
                choiesScenarioCount = 0;
            }
            else
            {
                scenarioManager.ChangeScenario
                    (scenarioText, scenarioData.choies[0].choiesSet[choiesNum].choiesScenario[choiesScenarioCount].scenario);
                scenarioManager.ChangeCharacterName
                    (talkingCharacterNameText, scenarioData.choies[0].choiesSet[choiesNum].choiesScenario[choiesScenarioCount].talkingCharacterName);

                characterManager.ChangeCharacterImage(characterImage[0], scenarioData.choies[0].choiesSet[choiesNum].choiesScenario[choiesScenarioCount].mainCharacterImage);
                characterManager.MoveCharacter(characterImage[0], scenarioData.choies[0].choiesSet[choiesNum].choiesScenario[choiesScenarioCount].mainCharacterPos);
                characterManager.ChangeCharacterImage(characterImage[1], scenarioData.choies[0].choiesSet[choiesNum].choiesScenario[choiesScenarioCount].subCharacter01Image);
                characterManager.MoveCharacter(characterImage[1], scenarioData.choies[0].choiesSet[choiesNum].choiesScenario[choiesScenarioCount].subCharacter01Pos);
                characterManager.ChangeCharacterImage(characterImage[2], scenarioData.choies[0].choiesSet[choiesNum].choiesScenario[choiesScenarioCount].subCharacter02Image);
                characterManager.MoveCharacter(characterImage[2], scenarioData.choies[0].choiesSet[choiesNum].choiesScenario[choiesScenarioCount].subCharacter02Pos);

                choiesScenarioCount++;
                return;
            }

        }

        //シナリオテキスト
        scenarioManager.ChangeScenario
            (scenarioText, scenarioData.scenarios[scenarioCount].scenario);
        scenarioManager.ChangeCharacterName
            (talkingCharacterNameText, scenarioData.scenarios[scenarioCount].talkingCharacterName);

        //キャラクターの画像変更と移動
        characterManager.ChangeCharacterImage(characterImage[0], scenarioData.scenarios[scenarioCount].mainCharacterImage);
        characterManager.MoveCharacter(characterImage[0], scenarioData.scenarios[scenarioCount].mainCharacterPos);
        characterManager.ChangeCharacterImage(characterImage[1], scenarioData.scenarios[scenarioCount].subCharacter01Image);
        characterManager.MoveCharacter(characterImage[1], scenarioData.scenarios[scenarioCount].subCharacter01Pos);
        characterManager.ChangeCharacterImage(characterImage[2], scenarioData.scenarios[scenarioCount].subCharacter02Image);
        characterManager.MoveCharacter(characterImage[2], scenarioData.scenarios[scenarioCount].subCharacter02Pos);

        //この次が選択肢かどうか
        isChoies = (scenarioData.scenarios[scenarioCount].choies != "") ? true : false;
        choies = scenarioData.scenarios[scenarioCount].choies.Split(' ');

        if (!isChoies) scenarioCount++;
    }

    IEnumerator JudgeNext()
    {
        yield return new WaitForSeconds(waitTime);
        isCanClick = true;
    }

}
