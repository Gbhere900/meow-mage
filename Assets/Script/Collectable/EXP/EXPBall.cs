using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPBall : Collectable
{
    static public Action<EXPBall> OnRecycled;
    static public Action OnCollected;
    public AudioClip collectedAudio;

    private void Awake()
    {
        collectedAudio = Resources.Load<AudioClip>("Audio/SoundEffect/PickUpEXP");
    }
    public override void Collect()
    {
        AudioManager.Instance().PlaySound(collectedAudio);
        OnCollected.Invoke();
        Recycle();
    }

    public void Recycle()
    {
        OnRecycled?.Invoke(this);
    }

    

}
