using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class HierarchyDev : MonoBehaviour {
    [InitializeOnLoadMethod]
    static void StartInitializeOnLoadMethod()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
    }

    static void OnHierarchyGUI(int instanceID, Rect selectionRect)
    {
        if (Event.current.shift)
        {
            if (Event.current != null && selectionRect.Contains(Event.current.mousePosition)
             && Event.current.button == 1 && Event.current.type == EventType.MouseUp)
            {
                GameObject selectedGameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
                //这里可以判断selectedGameObject的条件
                if (selectedGameObject != null)
                {
                    //Debug.Log(selectedGameObject.name);
                    Vector2 mousePosition = Event.current.mousePosition;

                    EditorUtility.DisplayPopupMenu(new Rect(mousePosition.x, mousePosition.y, 0, 0), "R.D.Tools/RushScripts", null);
                    Event.current.Use();
                }
            }
        }
    }

    private static string panelName;
    private static string code;
    [MenuItem("R.D.Tools/RushScripts/Serialized2Sys")]
    static void GetAndSerUIPath()
    {    
        Transform PanelTrans = Selection.activeTransform;
        panelName = PanelTrans.name;
        code = "public enum PanelAssets_" + panelName+" {\n"+panelName+",";
        Debug.Log(panelName);
        Search(PanelTrans);
        code = code.Remove(code.Length - 1, 1);
        code += "\n}";
        string url = Application.dataPath + "/RushDevelopFW/Scripts/SysTemDefine/PanelAssets/PanelAssets_"+panelName+".cs";
        Debug.Log(code);
        if(!File.Exists(Application.dataPath + "/RushDevelopFW/Scripts/SysTemDefine/PanelAssets"))
        {
            Directory.CreateDirectory(Application.dataPath + "/RushDevelopFW/Scripts/SysTemDefine/PanelAssets");
        }
        File.WriteAllText(url,code);
        AssetDatabase.Refresh();
    }
    [MenuItem("R.D.Tools/RushScripts/CreatePanelScripts")]
    static void CreatePanelScripts()
    {
        string scripts = "/**\n * Copyright(C) 2019 by #Betech上海赢赞数字科技有限公司#\n * All rights reserved.\n *FileName:     #测试项目#\n *Author:       #Laughing#\n *Version:      #v1.0#\n *UnityVersion：#v2017.4.3#\n *Date:         #20190128#\n *Description:\n    *History:\n*/ " 
            +"\nusing System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine;\nusing UnityEngine.EventSystems;\nusing RDFW;\n\npublic class " +
            Selection.activeGameObject.name + " : UIBase {\n    public void Awake()\n    {\n        PanelInit();\n    }\n    public override void PanelInit()\n    {\n        base.PanelInit();\n    }\n    public override void OnActive()\n    {\n        base.OnActive();\n    }\n    public override void OnInActive()\n    {\n        base.OnInActive();\n    }\n}";

        File.WriteAllText(Application.dataPath + "/Scripts/" + Selection.activeGameObject.name + ".cs",scripts);
        AssetDatabase.Refresh();
    }

    private static void Search(Transform father)
    {
        int length = father.childCount;
        if (length > 0)
        {
            for (int i = 0; i < length; i++)
            {
                Transform child = father.GetChild(i);
                //Debug.Log(child.name);
                code +="\n"+child.name + ",";
                Search(father.GetChild(i));
            }
        }
    }
}
