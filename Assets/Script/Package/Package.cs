using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Package", fileName = "PackageLib")]
public class PackageLib : ScriptableObject
{
  

    public List<MagicSO> magicSOs;
    public void AddToList(MagicSO magicSO)
    {
        magicSOs.Add(magicSO);
    }

    public void CLearPackage()
    {
        magicSOs.Clear();
    }
}
