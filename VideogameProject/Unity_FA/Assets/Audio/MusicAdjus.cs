using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeControl : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = MusicManager.Instance.GetVolume();
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }

    private void OnVolumeChange(float value)
    {
        MusicManager.Instance.SetVolume(value);
    }
}
