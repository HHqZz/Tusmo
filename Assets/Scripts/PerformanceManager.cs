using UnityEngine;

public class PerformanceManager : MonoBehaviour
{
    public int targetFrameRate = 60;
    public bool enableLOD = true;

    void Start()
    {
        Application.targetFrameRate = targetFrameRate;
        QualitySettings.vSyncCount = 0; // Désactiver VSync pour mobile

        // Optimisations Android
        if (Application.platform == RuntimePlatform.Android)
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            // Réduire résolution si nécessaire
        }
    }

    void Update()
    {
        // Monitor FPS
        if (Time.frameCount % 60 == 0)
        {
            float fps = 1.0f / Time.deltaTime;
            Debug.Log("FPS: " + fps);
            if (fps < 30)
            {
                // Réduire qualité
                QualitySettings.SetQualityLevel(QualitySettings.GetQualityLevel() - 1, true);
            }
        }
    }
}