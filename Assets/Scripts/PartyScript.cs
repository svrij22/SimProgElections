using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BodyScript;

public class PartyScript : MonoBehaviour
{
    /// <summary>
    /// Bindings
    /// </summary>
    public Parties PartyColour = Parties.Green;

    public GameObject Body;

    /// <summary>
    /// Update event
    /// </summary>
    private void Update()
    {
        //Set party colour
        Body.GetComponent<BodyScript>()
            .UpdateColour(PartyColour);
    }

}
