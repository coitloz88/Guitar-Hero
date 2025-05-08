using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    ParticleSystem.EmitParams _emitParams;
    // Start is called before the first frame update
    void Start()
    {
        _emitParams = new ParticleSystem.EmitParams();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) { 
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
            _emitParams.position = pos;
            _particleSystem.Emit(_emitParams, 1);
        }
    }
}
