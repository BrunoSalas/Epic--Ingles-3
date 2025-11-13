using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SpatialSys.UnitySDK;

public class SelectSeed : MonoBehaviour
{
    public static SelectSeed instance;
    public Seed actualSeed;
    public TMP_Dropdown seedDropdown;

    private void Start()
    {
        instance = this;
    }
    public void DropdownValueChanged()
    {
        actualSeed = (Seed)seedDropdown.value;
    }
}
