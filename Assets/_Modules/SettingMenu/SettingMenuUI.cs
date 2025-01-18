using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject SettingMenuPanel;
    [SerializeField] private Button SettingBtn;
    [SerializeField] private Button BackBtn;
    public AudioMixer audioMixer;

    private void Start()
    {
        SettingBtn.onClick.AddListener(OnSetting);
        BackBtn.onClick.AddListener(OnBack);
        SettingMenuPanel.SetActive(false);
    }

    private void OnSetting()
    {
        SettingMenuPanel.SetActive(true);
    }

    private void OnBack()
    {
        SettingMenuPanel.SetActive(false);
    }

    public void SetSound (float sound) 
 {
   audioMixer.SetFloat("sound",  sound);  
 }
    public void SetMusic (float music) 
    {
    audioMixer.SetFloat("music",  music);  
    }
}
