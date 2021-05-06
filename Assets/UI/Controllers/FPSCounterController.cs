using System.Linq;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class FPSCounterController : MonoBehaviour
{
    [SerializeField]
    private Text fpsCounterText;

    private float fps;
    private ArrayList lastTenFPS = new ArrayList();
    private float averageFPS;

    private void Awake()
    {
        if (!fpsCounterText) {
            fpsCounterText = GetComponent<Text>();
        }
    }

    private void Update()
    {
        CalculateCurrentFPS();
        CalculateAverageFPS();
        UpdateFPSCounterText();
    }

    private void CalculateCurrentFPS() {
        fps = 1 / Time.unscaledDeltaTime;
    }

    private void CalculateAverageFPS() {
        lastTenFPS.Add(fps);
        if (lastTenFPS.Count > 10) {
            lastTenFPS.RemoveAt(0);
        }
        averageFPS = lastTenFPS.Cast<float>().Sum() / lastTenFPS.Count;
    }

    public void UpdateFPSCounterText() {
        fpsCounterText.text = "FPS: " + Mathf.FloorToInt(averageFPS);
    }
}
