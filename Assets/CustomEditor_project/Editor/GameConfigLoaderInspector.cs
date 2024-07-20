using System.Collections.Generic;
using CustomEditor_project.Scripts;
using CustomEditor_project.Scripts.ScrObj;
using UnityEditor;

namespace CustomEditor_project.Editor
{
    [CustomEditor(typeof(GameConfigLoader))]
    public class GameConfigLoaderInspector : UnityEditor.Editor
    {
        private readonly List<GameConfig> _loadedGameConfigs = new();

        private SerializedProperty _gameConfigsProperty;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();
            
            if (_loadedGameConfigs != null && _loadedGameConfigs.Count > 0)
            {
                EditorGUILayout.LabelField("Loaded Game Configs:", EditorStyles.boldLabel);
                foreach (GameConfig config in _loadedGameConfigs)
                {
                    if (config != null)
                    {
                        EditorGUILayout.ObjectField(config.name, config, typeof(GameConfig), false);
                    }
                }
            }
            else
            {
                EditorGUILayout.LabelField("No game configs found.");
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            LoadGameConfigs();
        }

        private void LoadGameConfigs()
        {
            string[] configsPaths = AssetDatabase.GetAssetPathsFromAssetBundle("config");
            _loadedGameConfigs.Clear();

            foreach (string path in configsPaths)
            {
                _loadedGameConfigs.Add(AssetDatabase.LoadAssetAtPath<GameConfig>(path));
            }
        }
    }
}