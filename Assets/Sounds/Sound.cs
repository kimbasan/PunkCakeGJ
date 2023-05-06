using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioSource Audio;
    [SerializeField] private AudioClip[] Clip;

    public void PlaySound(int Index)
    {
        Audio.PlayOneShot(Clip[Index]);
    }
}
