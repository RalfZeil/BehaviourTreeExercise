using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThrowable
{
    public void Detonate();
    public void Throw(Vector3 startPoint, Vector3 endPoint);
}
