using UnityEngine;
using System.Collections.Generic;

public class EjemploSonido : MonoBehaviour
{
    public AudioSource AudioSourcegolpecito;
    public AudioSource AudioSourcemuerte;
    public AudioSource AudioSourcepatada;

    public AudioClip golpecitoclip;
    public AudioClip Muriciendoclip;
    public AudioClip patadaclip;
    public void golpecito()
    {
        AudioSourcegolpecito.pitch = Random.Range(0.9f, 1.1f);
        AudioSourcegolpecito.PlayOneShot(golpecitoclip);
    }
    public void Muriciendo()
    {
        AudioSourcemuerte.pitch = Random.Range(0.9f, 1.1f);
        AudioSourcemuerte.PlayOneShot(Muriciendoclip);
    }
    public void Patada()
    {
        AudioSourcepatada.pitch = Random.Range(0.9f, 1.1f);
        AudioSourcepatada.PlayOneShot(patadaclip);
    }
}
