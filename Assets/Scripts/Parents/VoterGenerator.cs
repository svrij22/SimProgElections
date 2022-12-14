using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VoterGenerator : MonoBehaviour
{
    public List<VoterScript> AllVoters
    {
        get
        {
            List<VoterScript> _AllVoters = new();
            foreach (Transform child in gameObject.transform)
            {
                _AllVoters.Add(child.gameObject.GetComponent<VoterScript>());
            }
            return _AllVoters;
        }
    }

    public static VoterGenerator Instance;

    public GameObject VoterPrefab;

    public int AmountOfVoters = 50;

    HashSet<Vector3> RangesUsed = new();

    void Start()
    {
        Instance = this;
        GenerateVoters();
    }

    public void ClearVoters()
    {
        RangesUsed = new();
        foreach (Transform child in transform)
            GameObject.Destroy(child.gameObject);
    }
    public void GenerateVoters()
    {
        ClearVoters();

        //Create voters
        for (var i = 0; i < AmountOfVoters; i++)
        {
            Vector3 newPos = PartyGenerator.GetUnusedVectorPosition(RangesUsed);

            //Instantiate
            Instantiate(VoterPrefab,
                new Vector3(newPos.x, 0, newPos.z),
                Quaternion.identity,
                gameObject.transform);
        }
    }
}