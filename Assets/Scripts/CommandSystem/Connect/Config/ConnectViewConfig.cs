
using UnityEngine;

namespace AssemblySystem.Command.CommandsSO
{
    [CreateAssetMenu(menuName = "AssemblySystem/AssemblyCommandSO/ConnectAssemblyCommandSO", fileName = "DefaultConnectACSO")]
    public class ConnectViewConfig : ScriptableObject
    {
        [SerializeField] private bool isJack;

        [SerializeField] private JackType jackType;

        public JackType JackType => jackType;

        public bool IsJack => isJack;
    }
    
    public enum JackType
    {
        CharacterJoint,
        HingeJoint
    }
}