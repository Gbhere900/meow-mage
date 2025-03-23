using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
public class TMP_Pool_damage : BasicPoolClass<TextMeshPro>
{
    public TMP_Pool_damage() { }
    override protected string GetPrefabsPath()
    {
        return "Prefabs/DamageText.prefab";
    }
}

