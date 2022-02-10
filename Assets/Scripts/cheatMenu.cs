using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cheatMenu : MonoBehaviour
{
    [SerializeField] public Toggle noMonsterToggle;
    [SerializeField] public Toggle infiniteAmmoToggle;


    public static cheatMenu instance;
    private void Awake()
    {
        instance = this;
        infiniteAmmoToggle.isOn = false;
        noMonsterToggle.isOn = false;
    }

    public void ChangeColorOnNoMonsterToggle()
    {
        if (noMonsterToggle.isOn)
        {
            
        }
    }
}
