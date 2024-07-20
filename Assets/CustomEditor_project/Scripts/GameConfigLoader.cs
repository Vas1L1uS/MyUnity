using CustomEditor_project.Scripts.ScrObj;
using UnityEngine;

namespace CustomEditor_project.Scripts
{
    public class GameConfigLoader : MonoBehaviour
    {
        [HideInInspector] [SerializeField] private GameConfig _currentConfig;
    }
}