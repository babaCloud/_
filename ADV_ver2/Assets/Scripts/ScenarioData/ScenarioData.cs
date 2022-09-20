using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEditor;

[CreateAssetMenu(menuName ="ScenarioData")]

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
    public TextAsset csvFile;
    public List<ScenarioSet> scenarios = new List<ScenarioSet>();
    private List<string[]> csvData = new List<string[]>();
    private string data;

    private void OnEnable()
    {
        ChangeMojiCode();
        ReadCSV();
    }

    void ReadCSV()
    {

        StringReader reader = new StringReader(data);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvData.Add(line.Split(','));
        }

        scenarios.Clear();
        for (int i = 0; i < csvData.Count - 1; i++)
        {
            scenarios.Add(new ScenarioSet());
        }

        for (int i = 1; i < csvData.Count; i++)
        {
            
            scenarios[i - 1].talkingCharacterName = csvData[i][0];
            scenarios[i - 1].talkingCharacterCount = int.Parse(csvData[i][1]);
            scenarios[i - 1].mainCharacterImage = Resources.Load<Sprite>(csvData[i][2]);
            scenarios[i - 1].subCharacter01Image = Resources.Load<Sprite>(csvData[i][3]);
            scenarios[i - 1].subCharacter02Image = Resources.Load<Sprite>(csvData[i][4]);
            scenarios[i - 1].backImage = Resources.Load<Sprite>(csvData[i][5]);
            scenarios[i - 1].scenario = csvData[i][6];
            scenarios[i - 1].choies = csvData[i][7];

        }
    }

    void ChangeMojiCode()
    {
        byte[] loadData;

        using (FileStream fileStream = new FileStream(AssetDatabase.GetAssetPath(csvFile), 
            FileMode.Open, FileAccess.Read))
        {
            loadData = new byte[fileStream.Length];
            fileStream.Read(loadData, 0, loadData.Length);
        }

        Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
        string sjisstr = sjisEnc.GetString(loadData);
        byte[] bytesData = System.Text.Encoding.UTF8.GetBytes(sjisstr);
        Encoding utf8Enc = Encoding.GetEncoding("UTF-8");
        data = utf8Enc.GetString(bytesData);

    }
}





