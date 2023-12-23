using System;
using System.Collections;
using System.Collections.Generic;
using AssemblySystem.Command;
using AssemblySystem.Manager;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

[RequireComponent(typeof(Collider), typeof(MeshFilter))]
public class SelectObject : MonoBehaviour
{
    private Collider _collider;
    private Mesh _mesh;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        _mesh = GetComponent<Mesh>();
    }

    public void Select()
    {
        Debug.Log("selected(from select object)");
    }

    public Collider Collider => _collider;
    public Mesh Mesh => _mesh;
}
