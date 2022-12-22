using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartyGenerator : MonoBehaviour
{

    public List<PartyScript> AllParties
    {
        get
        {
            List<PartyScript> _AllParties = new();
            foreach (Transform child in gameObject.transform)
            {
                _AllParties.Add(child.gameObject.GetComponent<PartyScript>());
            }
            return _AllParties;
        }
    }
    public static PartyGenerator Instance;

    public GameObject PartyPrefab;

    public int AmountOfParties = 3; // Max 4

    void Start()
    {
        Instance = this;
    }


    HashSet<Vector3> RangesUsed = new();
    public void ClearParties()
    {
        RangesUsed = new();
        foreach (Transform child in transform)
            GameObject.Destroy(child.gameObject);
    }
    public static Vector3 GetUnusedVectorPosition(HashSet<Vector3> UsedPositions)
    {
        //Random variables, but no double positions
        var positionAccepted = false;

        Vector3 vector = new();

        while (!positionAccepted)
        {
            float x = Random.Range(-7, 7);
            float z = Random.Range(-7, 7);

            vector = new Vector3(x, 0, z);

            positionAccepted = !UsedPositions.Contains(vector);
            if (positionAccepted) UsedPositions.Add(vector);
        }
        return vector;
    }

    public void GenerateParties()
    {
        ClearParties();
        List<Parties> colourStack = new() { Parties.Blue, Parties.Yellow, Parties.Green, Parties.Yellow };

        //Create voters
        for (var i = 0; i < AmountOfParties; i++)
        {
            //Get color
            var color = colourStack.First();
            colourStack.Remove(color);

            //Get position
            Vector3 newPos = GetUnusedVectorPosition(RangesUsed);

            //Instantiate
            var party = Instantiate(PartyPrefab,
                new Vector3(newPos.x, 0, newPos.z),
                Quaternion.identity,
                gameObject.transform);

            party.GetComponent<PartyScript>()
                .PartyColour = color;
        }
    }
}
