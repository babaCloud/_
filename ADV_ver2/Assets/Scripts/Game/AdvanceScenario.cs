using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AdvanceScenario : MonoBehaviour
{
    [SerializeField, Header("��ʓ��̕ύX�������")]
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

    [SerializeField, Header("�҂�����")]
    private float waitTime;

    [SerializeField, Header("�I����")]
    private GameObject choiesCanvas;
    [SerializeField]
    private Text[] choiesText;

    [SerializeField, Header("�f�[�^")]
    private ScenarioData scenarioData;

    [SerializeField, Header("�Q�Ɛ�N���X")]
    private ScenarioManager scenarioManager;
    [SerializeField]
    private ChoiesManager choiesManager;
    [SerializeField]
    private CharacterManager characterManager;
    [SerializeField]
    private GameDirecter gameDirecter;


    private bool isCanClick = false;//�N���b�N���Ď��ɐi�߂邱�Ƃ��\��
    private bool isChoies = false;//���N���b�N������I������
    public bool isChoiesScenario { set; private get; } = false;//����X�g���[����
    private int scenarioCount = 0;
    private string[] choies = new string[3];//�I����
    public int choiesNum { set; private get; } = -1;//�ǂ̑I������I�񂾂�
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
        

        //�I����
        choiesManager.ActiveChoies(isChoies && !isChoiesScenario, choiesCanvas, choiesText, choies);
        if (isChoiesScenario)
        {
            //����X�g�[���[�I��
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

        //�V�i���I�e�L�X�g
        scenarioManager.ChangeScenario
            (scenarioText, scenarioData.scenarios[scenarioCount].scenario);
        scenarioManager.ChangeCharacterName
            (talkingCharacterNameText, scenarioData.scenarios[scenarioCount].talkingCharacterName);

        //�L�����N�^�[�̉摜�ύX�ƈړ�
        characterManager.ChangeCharacterImage(characterImage[0], scenarioData.scenarios[scenarioCount].mainCharacterImage);
        characterManager.MoveCharacter(characterImage[0], scenarioData.scenarios[scenarioCount].mainCharacterPos);
        characterManager.ChangeCharacterImage(characterImage[1], scenarioData.scenarios[scenarioCount].subCharacter01Image);
        characterManager.MoveCharacter(characterImage[1], scenarioData.scenarios[scenarioCount].subCharacter01Pos);
        characterManager.ChangeCharacterImage(characterImage[2], scenarioData.scenarios[scenarioCount].subCharacter02Image);
        characterManager.MoveCharacter(characterImage[2], scenarioData.scenarios[scenarioCount].subCharacter02Pos);

        //���̎����I�������ǂ���
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
