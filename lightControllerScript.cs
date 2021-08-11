using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lightControllerScript : MonoBehaviour
{
    public Slider colourSlider1;
    public Gradient lightGradient;
    public Light[] lightArray;
    public float sliderValue;

    private Color newColour;

    // Start is called before the first frame update
    public void Update()
    {

        sliderValue = colourSlider1.value;
        newColour = lightGradient.Evaluate(colourSlider1.value);

        foreach (Light roomLight in lightArray) {
        roomLight.color = newColour;
    }
}
}
