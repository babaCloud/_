using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterManager : MonoBehaviour
{

    [SerializeField]
    private float moveTime;
    [SerializeField]
    private float fadeTime;
    [SerializeField]
    private Vector3[] characterPos;
    [SerializeField]
    private GameDirecter gameDirecter;

    private enum FADE 
    {
        fadeIn,
        fadeOut,
        crossFade
    }
    private FADE fade;


    public void MoveCharacter(Image character,int pos)
    {
        if (pos != -1) 
        {
            gameDirecter.AddIsPlayingList();

            character.transform.DOMove(characterPos[pos], moveTime)
                .OnComplete(() =>
                {
                    gameDirecter.SwitchIsPlayingList();
                });
        }
    }

    public void ChangeCharacterImage(Image character,Sprite image)
    {

        if (image != null)
        {
            if (character.color.a == 0f) Fade(character, FADE.fadeIn);
            character.sprite = image;
        }
        else
        {
            if (character.color.a == 1f) Fade(character, FADE.fadeOut);
        }
        
    }

    private void Fade(Image image, FADE fade)
    {
        gameDirecter.AddIsPlayingList();

        switch (fade)
        {
            case FADE.fadeIn:
                DOTween.ToAlpha(
                    ()=>image.color,
                    color=>image.color=color,
                    1f,
                    fadeTime)
                    .SetEase(Ease.InQuart)
                    .OnComplete(() =>
                    {
                        gameDirecter.SwitchIsPlayingList();
                    });
                break;

            case FADE.fadeOut:
                DOTween.ToAlpha(
                   () => image.color,
                   color => image.color = color,
                   0f,
                   fadeTime)
                     .OnComplete(() =>
                     {
                         gameDirecter.SwitchIsPlayingList();
                     });
                break;

            case FADE.crossFade:

                break;
            default:
                break;
        }
    }
}
