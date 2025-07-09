using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private float _groundDistance = 2.1f;
    [SerializeField] private LayerMask _groundLayer;



    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public bool IsGrounded()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, _groundDistance, _groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, Vector3.down * _groundDistance);

    }

}
