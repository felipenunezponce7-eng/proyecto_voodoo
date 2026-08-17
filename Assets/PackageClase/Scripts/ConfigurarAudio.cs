using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI; 
public class ConfigurarAudio : MonoBehaviour 
{
    private bool pause = false;
    public GameObject pauseMenuUI;
    public AudioMixer masterMixer;
    public Slider sliderMaster;  
    private void Start()
	{
        pauseMenuUI.SetActive(false);
        float Volumen_Master = PlayerPrefs.GetFloat("MasterVolume", 1f); 
        sliderMaster.value = Volumen_Master;  
        SetMasterVolume(Volumen_Master); 
    }
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause = !pause;
            pauseMenuUI.SetActive(pause);
            if (pause)
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }
			else
			{
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }
        }
    }
    public void SetMasterVolume( float volume)
    {
        masterMixer.SetFloat("Volumen_Master", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("Volumen_Master", volume); 
    }
    public void SetSFXVolume( float volume )
    {
        masterMixer.SetFloat("Volumen_SFX", Mathf.Log10(volume) * 20f);
    }
    public void SetAmbientalVolume(float volume)
    {
        masterMixer.SetFloat("Volumen_Ambiental", Mathf.Log10(volume) * 20f);
    }
}
