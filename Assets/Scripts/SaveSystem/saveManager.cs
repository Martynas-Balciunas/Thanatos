using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveManager : MonoBehaviour
{
    public static saveManager Instance;
    private bool saveDataExists = false;
    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        
    }


    public bool doesSaveDataExist()
    {
        return saveDataExists;
    }
}
