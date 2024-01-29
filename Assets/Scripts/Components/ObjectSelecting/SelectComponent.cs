using System;
using System.Collections;
using System.Collections.Generic;
using AssemblySystem.Command;
using AssemblySystem.Manager;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

[RequireComponent(typeof(Collider), typeof(MeshFilter))]
public class SelectComponent : MonoBehaviour
{
    [SerializeField] private bool _isStatic = false;
    private bool _isSelected = false;
    private Collider _collider;
    private Mesh _mesh;

    
    //TODO: create throwing out frow inventory
    public Action OnDeselected;
    
    
    private void Start()
    {
        _collider = GetComponent<Collider>();
        _mesh = GetComponent<Mesh>();
    }

    public void Select()
    {
        _isSelected = true;
    }

    public void Deselect()
    {
        _isSelected = false;
    }

    public bool IsSelected => _isSelected;
    public Collider Collider => _collider;
    public Mesh Mesh => _mesh;

    public bool IsStatic => _isStatic;
}