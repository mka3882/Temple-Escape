using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Base : ApplicationElement
{
    protected Vector3 BasePosition;
    protected GameObject controlObject;
    protected TextMesh nameTag;

    private void Start()
    {
        nameTag = gameObject.GetComponentInChildren<TextMesh>();
    }

    public virtual void InitObject()
    {
        Destroy(nameTag);
        BasePosition = transform.position;
    }
}
