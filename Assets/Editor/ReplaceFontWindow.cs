using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ReplaceFontWindow : EditorWindow
{
#if UNITY_EDITOR
    [MenuItem("Tools/��������")]
    public static void Open()
    {
        GetWindow(typeof(ReplaceFontWindow)); //�����´���
    }

    Font toChange; //�滻���壨��帳ֵ��
    FontStyle toFontStyle;//������ʽ����帳ֵ��
    static Font toChangeFont;
    static FontStyle toChangeFontStyle;

    //

    void OnGUI()
    {
        toChange = (Font)EditorGUILayout.ObjectField(toChange, typeof(Font), true, GUILayout.MinWidth(100f));
        toFontStyle = (FontStyle)EditorGUILayout.EnumPopup(toFontStyle, GUILayout.MinWidth(100f));
        //��ֵ
        toChangeFont = toChange;
        toChangeFontStyle = toFontStyle;

        //��ť
        if (GUILayout.Button("����"))
        {
            Transform canvas = GameObject.Find("Canvas").transform;
            if (!toChangeFont)
            {
                Debug.Log("NO Font");
                return;
            }

            if (!canvas)
            {
                Debug.Log("NO Canvas");
                return;
            }

            SetFonts(canvas);

            Debug.Log("Font replacement succeeded");
        }
    }

    Transform childObj;
    public void SetFonts(Transform obj)
    {
        for (int i = 0; i < obj.childCount; i++)
        {
            childObj = obj.GetChild(i);
            Text t = childObj.GetComponent<Text>();
            if (t)
            {
                //������ŵ�������¼�У����Ӵ˴��� �޷���ԭ�滻ǰ�Ĳ���
                Undo.RecordObject(t, t.name);
                t.font = toChange;
                t.fontStyle = toChangeFontStyle;
                //ˢ����
                EditorUtility.SetDirty(childObj);
            }

            //�ݹ��ѯ
            if (childObj.childCount > 0)
            {
                SetFonts(childObj);
            }
        }
    }
#endif
}