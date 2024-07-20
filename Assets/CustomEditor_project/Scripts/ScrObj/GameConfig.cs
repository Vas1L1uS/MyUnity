using UnityEngine;

namespace CustomEditor_project.Scripts.ScrObj
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private string _configData = "default";
    }  
}
