using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirecter : MonoBehaviour
{
    private List<bool> isPlaying =  new List<bool>();

    public void AddIsPlayingList()
    {
        isPlaying.Add(false);
    }

    public void SwitchIsPlayingList()
    {
        for (int i = 0; i < isPlaying.Count; i++)
        {
            if (!isPlaying[i])
            {
                isPlaying[i] = true;
                break;
            }
        }
    }

    public bool JudgeCanPlay()
    {
        if (isPlaying.Count == 0) return false;
        return isPlaying[isPlaying.Count - 1];
    }

    public void ClearIsPlayingList()
    {
        isPlaying.Clear();
    }
}
