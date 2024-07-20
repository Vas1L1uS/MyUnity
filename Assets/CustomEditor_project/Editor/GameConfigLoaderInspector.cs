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
        private SerializedProperty _currentGameConfigProperty;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();
            
            if (_loadedGameConfigs != null && _loadedGameConfigs.Count > 0)
            {
                EditorGUILayout.LabelField("Select game config:", EditorStyles.boldLabel);
                for (int i = 0; i < _loadedGameConfigs.Count; i++)
                {
                    if (_loadedGameConfigs[i] != null)
                    {
                        EditorGUI.BeginChangeCheck();
                        bool isSelected = EditorGUILayout.ToggleLeft(_loadedGameConfigs[i].name, _loadedGameConfigs[i] == (GameConfig)_currentGameConfigProperty.objectReferenceValue);
                        if (EditorGUI.EndChangeCheck())
                        {
                            if (isSelected)
                            {
                                _currentGameConfigProperty.objectReferenceValue = _loadedGameConfigs[i];
                            }
                            else if (_loadedGameConfigs[i] == (GameConfig)_currentGameConfigProperty.objectReferenceValue)
                            {
                                _currentGameConfigProperty.objectReferenceValue = null;
                            }
                        }
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
            _currentGameConfigProperty = serializedObject.FindProperty("_currentGameConfig");
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