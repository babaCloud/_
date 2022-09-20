using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEditor;

[CreateAssetMenu(menuName = "ScenarioData")]

[System.Serializable]
public class ChoiseScenario 
{
    public string choies;
    public string talkingCharacterName = "";
    public int talkingCharacterCount = 0;
    public Sprite mainCharacterImage = null;
    public Sprite subCharacter01Image = null;
    public Sprite subCharacter02Image = null;
    public Sprite backImage = null;
    public string scenario = "";
}
[System.Serializable]
public class Choies
{
    public string answer;
    public ChoiseScenario[] choiesScenario = new ChoiseScenario[3];
}
[System.Serializable]
public class ChoiesSet
{
    public string question;
    public List<Choies> choiesSet = new List<Choies>();
}


[System.Serializable]
public class ScenarioSet
{
    public string talkingCharacterName = "";
    public int talkingCharacterCount = 0;
    public Sprite mainCharacterImage = null;
    public Sprite subCharacter01Image = null;
    public Sprite subCharacter02Image = null;
    public Sprite backImage = null;
    public string scenario = "";
    public string choies = "";
}

public class ScenarioData : ScriptableObject
{
    public TextAsset mainCsvFile;
    public TextAsset[] choiesCsvFile;
  
    public List<ScenarioSet> scenarios = new List<ScenarioSet>(); 
    public List<ChoiesSet> choies = new List<ChoiesSet>();

    private List<string[]> mainCsvData = new List<string[]>();
    private List<string[]> choiesCsvData = new List<string[]>();

    private void OnEnable()
    {       
        ReadMainCSV();
        ReadChoiesCSV();
    }

    void ReadMainCSV()
    {
        
        StringReader reader = new StringReader(ChangeMojiCode(mainCsvFile));
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            mainCsvData.Add(line.Split(','));
        }

        scenarios.Clear();
        for (int i = 0; i < mainCsvData.Count - 1; i++)
        {            
            scenarios.Add(new ScenarioSet());
        }

        for (int i = 1; i < mainCsvData.Count; i++)
        {
            
            scenarios[i - 1].talkingCharacterName = mainCsvData[i][0];
            scenarios[i - 1].talkingCharacterCount = int.Parse(mainCsvData[i][1]);
            scenarios[i - 1].mainCharacterImage = Resources.Load<Sprite>(mainCsvData[i][2]);
            scenarios[i - 1].subCharacter01Image = Resources.Load<Sprite>(mainCsvData[i][3]);
            scenarios[i - 1].subCharacter02Image = Resources.Load<Sprite>(mainCsvData[i][4]);
            scenarios[i - 1].backImage = Resources.Load<Sprite>(mainCsvData[i][5]);
            scenarios[i - 1].scenario = mainCsvData[i][6];
            scenarios[i - 1].choies = mainCsvData[i][7];

        }
    }

    void ReadChoiesCSV()
    {
        choies.Clear();
        Debug.Log(choiesCsvFile.Length);
        for (int j = 0; j < choiesCsvFile.Length; j++)
        {
            
            choies.Add(new ChoiesSet());
        }

        for (int i = 0; i < choiesCsvFile.Length; i++)
        {
            StringReader reader = new StringReader(ChangeMojiCode(choiesCsvFile[i]));
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                choiesCsvData.Add(line.Split(','));
            }

            int j = 0;
            while(j < 3)
            {
                for (int I = 1; I < choiesCsvData.Count; I++)
                {
                    choies[i].choiesSet[j].choiesScenario[I].choies = choiesCsvData[I][0];
                    choies[i].choiesSet[j].choiesScenario[I].talkingCharacterName = choiesCsvData[I][1];
                    choies[i].choiesSet[j].choiesScenario[I].talkingCharacterCount = int.Parse(choiesCsvData[I][2]);
                    choies[i].choiesSet[j].choiesScenario[I].mainCharacterImage = Resources.Load<Sprite>(choiesCsvData[I][3]);
                    choies[i].choiesSet[j].choiesScenario[I].subCharacter01Image = Resources.Load<Sprite>(choiesCsvData[I][4]);
                    choies[i].choiesSet[j].choiesScenario[I].subCharacter02Image = Resources.Load<Sprite>(choiesCsvData[I][5]);
                    choies[i].choiesSet[j].choiesScenario[I].backImage = Resources.Load<Sprite>(choiesCsvData[I][6]);
                    choies[i].choiesSet[j].choiesScenario[I].scenario = choiesCsvData[I][7];

                    if (choiesCsvData[I + 1][0] != "")
                    {
                        j++;
                        break;
                    }
                }

                
            }
            
        }
    }

    string ChangeMojiCode(TextAsset text)
    {
        byte[] loadData;

        using (FileStream fileStream = new FileStream(AssetDatabase.GetAssetPath(text),
            FileMode.Open, FileAccess.Read))
        {
            loadData = new byte[fileStream.Length];
            fileStream.Read(loadData, 0, loadData.Length);
        }

        Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
        string sjisstr = sjisEnc.GetString(loadData);
        byte[] bytesData = System.Text.Encoding.UTF8.GetBytes(sjisstr);
        Encoding utf8Enc = Encoding.GetEncoding("UTF-8");
        return utf8Enc.GetString(bytesData);

    }
}





