using System;
using UnityEngine;

namespace AssemblySystem.Command
{
    public class ConnectCommand : Command
    {
        public Joint _jack;
        public Rigidbody _plug;

        public override void Execute()
        {
            if (!_jack || !_plug)
                throw new Exception("no plug or jack");
            _jack.connectedBody = _plug;
        }
        
        public override void Undo()
        {
            if (!_jack || !_plug)
                throw new Exception("no plug or jack");
            _jack.connectedBody = null;
        }
    }   
}
