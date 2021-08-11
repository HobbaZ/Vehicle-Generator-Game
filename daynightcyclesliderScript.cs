using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class daynightcyclesliderScript : MonoBehaviour
{
    public Slider daynightSlider;
    public GameObject daynightCycle;
    public Gradient daynightGradient;
    private Color newColour;
    public Camera cam;

    public float sliderValue;

    void Start() {
        //cam.clearFlags = CameraClearFlags.SolidColor;
    }

    // Update is called once per frame
    void Update()
    {
        sliderValue = daynightSlider.value;
        daynightCycle.transform.rotation = (Quaternion.Euler(sliderValue *180,90,0));

        newColour = daynightGradient.Evaluate(daynightSlider.value);
        //cam.backgroundColor = newColour;
    }
}
