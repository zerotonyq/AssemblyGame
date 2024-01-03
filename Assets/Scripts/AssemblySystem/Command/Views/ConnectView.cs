using System;
using System.Collections;
using System.Collections.Generic;
using AssemblySystem.Command;
using AssemblySystem.Manager;
using AssemblySystem.Views;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

[RequireComponent(typeof(SelectView))]
public class ConnectView : CommandView<ConnectAssemblyCommand>
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ConnectView view))
        {
            if (!gameObject.GetComponent<SelectView>().IsSelected)
                return;
            
            var currentCommand = assemblyCommand as ConnectAssemblyCommand;
            currentCommand.first = this;
            currentCommand.second = view;
            
            TryExecCommand(view);
        }
    }
}
