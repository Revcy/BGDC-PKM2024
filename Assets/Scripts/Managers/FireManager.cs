using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    private static Dictionary<string, List<string>> fireExtinguisherMapping;

    private void Awake()
    {
        InitializeFireExtinguisherMapping();
    }

    private void InitializeFireExtinguisherMapping()
    {
        fireExtinguisherMapping = new Dictionary<string, List<string>>
        {
            { "FireA", new List<string> { "WBucket", "Foam", "DryChem" } },
            { "FireB", new List<string> { "Foam", "CO2", "DryChem" } },
            { "FireC", new List<string> { "CO2", "DryChem" } },
            { "FireD", new List<string> { "DryPow" } },
            { "FireK", new List<string> { "WetChem" } }
        };
    }

    public static bool CanExtinguish(string fireTag, string extinguisherName)
    {
        if (fireExtinguisherMapping.ContainsKey(fireTag))
        {
            return fireExtinguisherMapping[fireTag].Contains(extinguisherName);
        }
        return false;
    }
}
