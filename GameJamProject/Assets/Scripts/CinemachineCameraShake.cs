using Cinemachine;
using UnityEngine;

public class CinemachineCameraShake : MonoBehaviour
{
  [SerializeField]  CinemachineVirtualCamera CinemachineVirtualCamera;
    float shakeTimer;
    // Start is called before the first frame update
    void Start()
    {
        CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CameraShake(5, .2f);
        }
    }
    void CameraShake(float intensity, float timer)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = timer;
    }
}
