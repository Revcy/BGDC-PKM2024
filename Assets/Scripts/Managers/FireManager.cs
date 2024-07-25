using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    [System.Serializable]
    public class FireExtinguisherPair
    {
        public string fireType;
        public List<string> extinguishers;
    }

    public static List<FireExtinguisherPair> fireExtinguisherPairs = new List<FireExtinguisherPair>();

    void Start()
    {
        // Define different types of fires and their corresponding extinguishers
        fireExtinguisherPairs.Add(new FireExtinguisherPair { fireType = "FireA", extinguishers = new List<string> { "WBucket", "Foam", "DryChem" } });
        fireExtinguisherPairs.Add(new FireExtinguisherPair { fireType = "FireB", extinguishers = new List<string> { "Foam", "CO2", "DryChem" } });
        fireExtinguisherPairs.Add(new FireExtinguisherPair { fireType = "FireC", extinguishers = new List<string> { "CO2", "DryChem" } });
        fireExtinguisherPairs.Add(new FireExtinguisherPair { fireType = "FireD", extinguishers = new List<string> { "DryPow" } });
        fireExtinguisherPairs.Add(new FireExtinguisherPair { fireType = "FireK", extinguishers = new List<string> { "WetChem" } });
    }

    public static bool CanExtinguish(string fireType, string extinguisherType)
    {
        FireExtinguisherPair pair = fireExtinguisherPairs.Find(p => p.fireType == fireType);
        if (pair != null)
        {
            return pair.extinguishers.Contains(extinguisherType);
        }
        return false;
    }
}
