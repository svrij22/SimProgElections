using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyScript : MonoBehaviour
{
    public GameObject Sphere;

    public GameObject Capsule;

    public Parties CurrentColour = Parties.Gray;
    public void UpdateColour(Parties color)
    {
        //Return if updated
        if (CurrentColour == color) return;

        Sphere.GetComponent<Renderer>()
            .material = ColourHelper.Load(color);

        Capsule.GetComponent<Renderer>()
            .material = ColourHelper.Load(color);
    }
}
