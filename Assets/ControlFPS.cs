using UnityEngine;

public class ControlFPS : MonoBehaviour
{
    public int limitaFPS = 60;

    void Start()
    {
        Application.targetFrameRate = limitaFPS;
    }
}