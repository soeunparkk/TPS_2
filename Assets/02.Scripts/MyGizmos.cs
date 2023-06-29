using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmos : MonoBehaviour
{
    public Color _color = Color.yellow;
    public float _radius = 0.1f;
    void OnDrawGizmos()
    {
        // ����� ���� ����
        Gizmos.color = _color;
        // ��ü ����� ����� ����. ���ڴ� (���� ��ġ, ������)
        Gizmos.DrawSphere(transform.position, _radius);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
