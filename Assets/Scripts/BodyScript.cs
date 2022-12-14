using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyScript : MonoBehaviour
{
    public GameObject Sphere;

    public GameObject Capsule;

    public Colours CurrentColour = Colours.Gray;
    public void UpdateColour(Colours color)
    {
        //Return if updated
        if (CurrentColour == color) return;

        Sphere.GetComponent<Renderer>()
            .material = ColourHelper.Load(color);

        Capsule.GetComponent<Renderer>()
            .material = ColourHelper.Load(color);
    }
}
