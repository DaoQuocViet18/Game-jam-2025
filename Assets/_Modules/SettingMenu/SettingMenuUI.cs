using UnityEngine;
using UnityEngine.Audio;


public class SettingMenuUI : MonoBehaviour
{
 public AudioMixer audioMixer;
 public void SetSound (float sound) 
 {
   audioMixer.SetFloat("sound",  sound);  
 }
    public void SetMusic (float music) 
    {
    audioMixer.SetFloat("music",  music);  
    }
}
