using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class CameraPrioritySwitcher : MonoBehaviour
{
    public Button Button;

    public CinemachineVirtualCamera EditCamera;
    public CinemachineVirtualCamera PlayCamera;
    public CameraTargetDamper Damper;

    private bool _play;

    private void OnEnable()
    {
        Button.onClick.AddListener(Toggle);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(Toggle);
    }

    private void Toggle()
    {
        _play = !_play;

        if (_play)
        {
            PlayCamera.Priority = 10;
            EditCamera.Priority = 0;
            Damper.enabled = true;
        }
        else
        {
            PlayCamera.Priority = 0;
            EditCamera.Priority = 10;
            Damper.enabled = false;
        }
    }
}
