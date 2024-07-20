using UnityEngine;

namespace CustomEditor_project.Scripts.ScrObj
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public string ConfigData => _configData;
        
        [SerializeField] private string _configData = "default";
    }  
}
