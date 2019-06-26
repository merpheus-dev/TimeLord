using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Subtegral.TimeLord.Core;
public class Movement : MonoBehaviour
{
    public float Speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * TimeLord.deltaTime*Speed;
    }
}
