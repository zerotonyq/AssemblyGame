using AssemblySystem.Command;
using AssemblySystem.Command.CommandsSO;
using AssemblySystem.Command.InitData;
using AssemblySystem.Views;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ConnectView : CommandView
{
    private bool _isJack;

    private bool _isConnected;

    private Joint _jack;

    private Rigidbody _rigidbody;

    public void Init(ConnectViewConfig config)
    {
        _command = new ConnectCommand();
        
        _rigidbody = GetComponent<Rigidbody>();
        
        _isJack = config.IsJack;

        if (_isJack)
            AddJackFunctionality(config.JackType);
    }

    private void AddJackFunctionality(JackType type)
    {
        switch (type)
        {
            case JackType.CharacterJoint:
                _jack = gameObject.AddComponent<CharacterJoint>();
                break;
            case JackType.HingeJoint:
                _jack = gameObject.AddComponent<HingeJoint>();
                break;
            default:
                _jack = gameObject.AddComponent<CharacterJoint>();
                break;
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ConnectView otherView))
        {
            if (!otherView._isJack)
                return;

            if(IsPartOfAssembly)
                otherView.Subscribe(AssemblyComponent.AddToAssembly);
            otherView.TryExecCommand();
        }
    }
    
    public bool IsJack => _isJack;
    
}
