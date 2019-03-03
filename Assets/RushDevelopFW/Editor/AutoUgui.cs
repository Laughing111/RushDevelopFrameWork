using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using RDFW;

namespace RDFW
{
    [System.Serializable]
    public class JsonRead
    {
        public List<ImageJson> data;
    }
    [System.Serializable]
    public class ImageJson
    {
        public string name;
        public float x;
        public float y;
        public float width;
        public float height;
        public string father;
    }


    public class AutoUgui : EditorWindow
    {

        [MenuItem("R.D.Tools/UIPageLayOut")]
        private static void Go2UI()
        {
            //Transform canvas=GameObject.Find("Canvas").transform;
            //GameObject UI = new GameObject("UI");
            //UI.AddComponent<RawImage>();
            //UI.transform.SetParent(canvas, false);
            PopWindow pw = GetWindow(typeof(PopWindow), true, "UI自动布局") as PopWindow;
            pw.minSize = PopWindow.minResolution;
            pw.maxSize = PopWindow.minResolution;
            pw.Init();
            pw.Show();
        }
    }

    public class PopWindow : EditorWindow
    {
        public static PopWindow window;
        public static Vector2 minResolution = new Vector2(500, 200);
        public static Rect middleCenterRect = new Rect(50, 10, 300, 200);
        public GUIStyle labelStyle;
        public static Transform uiroot;
        public static string uiAssetsUrl = "/UIAssets";
        private JsonRead uiData;

        public void Init()
        {
            labelStyle = new GUIStyle();
            labelStyle.normal.textColor = Color.white;
            labelStyle.alignment = TextAnchor.UpperLeft;
            labelStyle.fontSize = 14;
            labelStyle.border = new RectOffset(10, 10, 20, 20);
        }
        private void OnGUI()
        {
            GUILayout.BeginArea(middleCenterRect);
            GUILayout.BeginVertical();
            EditorGUILayout.LabelField("根据psd文件自动布局UI", labelStyle, GUILayout.Width(220));
            GUILayout.Space(20);
            uiroot = EditorGUILayout.ObjectField(new GUIContent("UIRoot", "选择UI根节点"), uiroot, typeof(Transform), true) as Transform;
            uiAssetsUrl = EditorGUILayout.TextField(new GUIContent("UIAssetsUrl", "UI资源路径"), uiAssetsUrl);
            GUILayout.Space(20);
            EditorGUILayout.LabelField("点击下面的按钮进行自动布局", labelStyle, GUILayout.Width(220));
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("加载UI信息", GUILayout.Width(80)))
            {
                ReadJson();
            }
            GUILayout.Space(20);
            if (GUILayout.Button("自动布局", GUILayout.Width(80)))
            {
                SetLayout();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        private void ReadJson()
        {
            string jsonTxt = System.IO.File.ReadAllText(Application.dataPath + uiAssetsUrl + "/JsonData.txt");
            uiData = JsonUtility.FromJson<JsonRead>(jsonTxt);
            EditorUtility.DisplayDialog("R.D.UI自动布局", "读取UI配置信息完毕！", "好的");
        }

        private void SetLayout()
        {
            List<ImageJson> uiList = uiData.data;
            if (uiroot != null)
            {
                for (int i = uiList.Count - 1; i >= 0; i--)
                {
                    Transform father;
                    if (uiList[i].father.Contains(":"))
                    {
                        //有祖父物体
                        string[] fatherGroup = uiList[i].father.Split(':');
                        Transform GFather = uiroot.SearchforChild(fatherGroup[1].StringModify("#", ""));
                        if (GFather == null)
                        {
                            GFather = new GameObject(fatherGroup[1].StringModify("#", "")).AddComponent<RectTransform>().transform;
                            GFather.transform.SetParent(uiroot, false);
                            GFather.transform.localPosition = Vector3.zero;
                        }

                        father = uiroot.SearchforChild(fatherGroup[0].StringModify("#", ""));
                        if(father==null)
                        {
                            father = new GameObject(fatherGroup[0].StringModify("#", "")).AddComponent<RectTransform>().transform;
                            father.transform.SetParent(GFather, false);
                            father.transform.localPosition = Vector3.zero;
                        }
                    }
                    else
                    {
                        //只有父物体
                         father = uiroot.SearchforChild(uiList[i].father.StringModify("#", ""));
                        if (father == null)
                        {
                            father = new GameObject(uiList[i].father.StringModify("#","")).AddComponent<RectTransform>().transform;
                            father.transform.SetParent(uiroot, false);
                            father.transform.localPosition = Vector3.zero;
                        }

                    }                                                  
                    string name = uiList[i].name;
                    GameObject UI = new GameObject(name.StringModify("#",""));
                    RawImage uiRawImg = UI.AddComponent<RawImage>();
                    UI.transform.SetParent(father, false);
                    //UI.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
                    //UI.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 0);
                    UI.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(uiList[i].x, -uiList[i].y, 0);
                    UI.GetComponent<RectTransform>().sizeDelta = new Vector2(uiList[i].width, uiList[i].height);
                    Texture tex = AssetDatabase.LoadAssetAtPath<Texture>("Assets" + uiAssetsUrl + "/" + name + ".png");
                    uiRawImg.texture = tex;

                    
                }
                EditorUtility.DisplayDialog("R.D.UI自动布局", "布局完毕！", "知道了！");
            }
            else
            {
               EditorUtility.DisplayDialog("R.D.UI自动布局","Error:请指定UI根节点","知道了！");
            }


            

        }
    }
}


