using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
static public class TMP_Pool
{
    static public ObjectPool<DamageUI> TMP_damageTextPool;
    static DamageUI TMP_damageText;
    static TMP_Pool()
    {
        TMP_damageTextPool = new ObjectPool<DamageUI>(CreateFunction_damageText, ActionOnGet_damageText, ActionOnRelease_damageText, ActionOnDestroy_damageText);
        TMP_damageText = Resources.Load<DamageUI>("Prefabs/DamageText");
        if (TMP_damageText == null)
            Debug.Log("damageTxtº”‘ÿ ß∞‹");
    }

    private static DamageUI CreateFunction_damageText()
    {
        return GameObject.Instantiate(TMP_damageText);

    }

    private static void ActionOnGet_damageText(DamageUI TMP_damageText)
    {
        TMP_damageText.gameObject.SetActive(true);
    }

    private static void ActionOnRelease_damageText(DamageUI TMP_damageText)
    {
        TMP_damageText.gameObject.SetActive(false);
    }
    private static void ActionOnDestroy_damageText(DamageUI TMP_damageText)
    {
        GameObject.Destroy(TMP_damageText.gameObject);
    }

}
