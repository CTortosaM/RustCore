using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    [SerializeField] private float SwayAmmount = 0.02f;
    [SerializeField] private float MaxSwayAmmount = 0.09f;
    [SerializeField] private float SwaySmooth = 3f;
    Vector3 def;
    Vector3 defAth;
    Vector3 euler;
    // Start is called before the first frame update
    void Start()
    {
        def = transform.localPosition;
        euler = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        createSway();
    }

    float _smooth;

    private void createSway()
    {
        _smooth = SwaySmooth;
        float factorX = -Input.GetAxis("Mouse X") * SwayAmmount;
        float factorY = -Input.GetAxis("Mouse Y") * SwayAmmount;

        if (factorX > MaxSwayAmmount || factorX < -MaxSwayAmmount) factorX = MaxSwayAmmount;
        if (factorY > MaxSwayAmmount || factorY < -MaxSwayAmmount) factorY = MaxSwayAmmount;

        Vector3 final = new Vector3(def.x + factorX, def.y + factorY, def.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, final, Time.deltaTime * _smooth);
    }
}
