using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float yOffset;
    private Vector3 vectorOffset;
    // Start is called before the first frame update
    void Start()
    {
        vectorOffset = new Vector3(0, yOffset, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position + vectorOffset;
    }
}
