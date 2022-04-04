using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationsSpeed;

   private void Update()
   {
       HandleTranslation();
       HandleRotation();
   }

   private void HandleTranslation()
   {
       var targetPosition = target.TransformPoint(offset);
       transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed* Time.deltaTime);
   }
    
    private void HandleRotation()
    {
        var direction = target.position - transform.position;
        var rotation= Quaternion.LookRotation(direction,Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation,rotation,rotationsSpeed * Time.deltaTime);
    }

}