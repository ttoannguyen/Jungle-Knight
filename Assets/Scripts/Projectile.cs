using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   [SerializeField] private float moveSpeed = 22f;

   private void Update() {
    MovePorjectile();
   }

   public void MovePorjectile(){
    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
   }
}
