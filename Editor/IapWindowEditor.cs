using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace VirtueSky.Iap
{
    public class IapWindowEditor : EditorWindow
    {
        [MenuItem("Unity-Common/IapSettings %W", false)]
        public static void MenuOpenIapSettings()
        {
            string path = "Assets/_Root/Resources";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var settings = CreateAndGetScriptableAsset<IapSettings>(path);
            Selection.activeObject = settings;
            EditorGUIUtility.PingObject(settings);
            EditorUtility.FocusProjectWindow();
        }

        private static T CreateAndGetScriptableAsset<T>(string path = "")
            where T : ScriptableObject
        {
            var so = FindAssetAtFolder<T>(new string[] { "Assets" }).FirstOrDefault();
            if (so == null)
            {
                var settings = ScriptableObject.CreateInstance<T>();
                AssetDatabase.CreateAsset(settings, $"{path}/{typeof(T).Name}.asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                so = FindAssetAtFolder<T>(new string[] { "Assets" }).FirstOrDefault();
            }

            return so;
        }

        private static T[] FindAssetAtFolder<T>(string[] paths) where T : Object
        {
            var list = new List<T>();
            var guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", paths);
            foreach (var guid in guids)
            {
                var asset = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid));
                if (asset)
                {
                    list.Add(asset);
                }
            }

            return list.ToArray();
        }
    }
}