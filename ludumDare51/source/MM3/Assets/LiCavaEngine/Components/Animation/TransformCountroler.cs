using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformCountroler : MonoBehaviour
{
    private Transform myTransform;
    private Vector3Countroler position = new Vector3Countroler();
    private Vector3Countroler scale = new Vector3Countroler(new Vector3(1,1,1));
    void Awake()
    {
        myTransform = transform;
    }

    private void LateUpdate()
    {
        transform.localPosition = position.Value;
        transform.localScale = scale.Value;
    }

    public Vector3Countroler RegisterPosition()
    {
        Vector3Countroler res = new Vector3Countroler();
        res.OperationIndex = ValueCountrolerManager.OprationName.num_add;
        position.AddFactor(res);
        return res;
    }

    public Vector3Countroler RegisterScale()
    {
        Vector3Countroler res = new Vector3Countroler();
        res.OperationIndex = ValueCountrolerManager.OprationName.num_mul;
        scale.AddFactor(res);
        return res;
    }
}
