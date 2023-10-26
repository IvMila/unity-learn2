using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashSetScript : MonoBehaviour
{
    private HashSet<string> _activePlayers = new HashSet<string>() { "Joe", "Bill" };
    private HashSet<string> _inactivePlayers = new HashSet<string>() { "Tom", "Jasy" };

    private void Start()
    {
        _activePlayers.UnionWith(_inactivePlayers);
        Debug.Log(_activePlayers);
    }
}
