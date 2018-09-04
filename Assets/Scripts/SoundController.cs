using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Singleton<SoundController> {

    [SerializeField] private AudioClip arrowSfx;
    [SerializeField] private AudioClip deathSfx;
    [SerializeField] private AudioClip fireBallSfx;
    [SerializeField] private AudioClip gameOver;
    [SerializeField] private AudioClip hitSfx;
    [SerializeField] private AudioClip rockSfx;
    [SerializeField] private AudioClip newGameSfx;
    [SerializeField] private AudioClip towerBuildSfx;
    [SerializeField] private AudioClip levelSfx;


    public AudioClip Arrow
    {
        get { return arrowSfx; }
    }

    public AudioClip Death
    {
        get { return deathSfx; }
    }

    public AudioClip Fireball
    {
        get { return fireBallSfx; }
    }

    public AudioClip GameOver
    {
        get { return gameOver; }
    }

    public AudioClip Hit
    {
        get { return hitSfx; }
    }

    public AudioClip Rock
    {
        get { return rockSfx; }
    }

    public AudioClip NewGame
    {
        get { return newGameSfx; }
    }

    public AudioClip TowerBuild
    {
        get { return towerBuildSfx; }
    }

    public AudioClip Level
    {
        get { return levelSfx; }
    }




}
