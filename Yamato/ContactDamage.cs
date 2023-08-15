﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VesselMayCry.Yamato
{
    internal class ContactDamage : MonoBehaviour
    {
        private int damagenumber = 40;
        private float magnitude = 0f;
        private int direction = (int)AttackDirection.normal;


        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.GetComponent<HealthManager>() != null || collider.gameObject.GetComponentInChildren<HealthManager>() != null || collider.GetComponentInParent<HealthManager>() != null)
            {
                if (collider.gameObject.layer == (int)PhysLayers.ENEMIES)
                {
                    Hit(collider.gameObject);
                }
            }
        }

        private void Hit(GameObject obj)
        {
            HitInstance hitInstance = new HitInstance();
            hitInstance.AttackType = AttackTypes.Nail;
            hitInstance.IgnoreInvulnerable = true;
            hitInstance.Multiplier = 1;
            hitInstance.MagnitudeMultiplier = magnitude;
            hitInstance.MoveDirection = true;
            hitInstance.CircleDirection = false;
            hitInstance.Direction = direction;
            hitInstance.Source = this.gameObject;
            if (magnitude == 2f)
            {
                hitInstance.MoveAngle = 0;
            }

            hitInstance.DamageDealt = damagenumber;
            HitTaker.Hit(obj, hitInstance);
        }

        public void HitAgain()
        {
            Collider2D thiscollider = gameObject.GetComponent<Collider2D>();
            //Is this a problem? probably not?
            Collider2D[] results = new Collider2D[1000];
            ContactFilter2D nofilter = new ContactFilter2D().NoFilter();
            thiscollider.OverlapCollider(nofilter, results);

            List<Collider2D> list = results.ToList<Collider2D>();
            foreach (Collider2D collider in list)
            {
                if (collider != null) { 
                    if (collider.gameObject.GetComponent<HealthManager>() != null || collider.gameObject.GetComponentInChildren<HealthManager>() != null || collider.GetComponentInParent<HealthManager>() != null)
                    {
                        if (collider.gameObject.layer == (int)PhysLayers.ENEMIES)
                        {
                            Hit(collider.gameObject);
                        }
                    }
                }
            }
        }

        public void SetLauncher(bool val)
        {
            if (val)
            {
                direction = (int)AttackDirection.upward;
                magnitude = 2f;
 
            }
        }

        public void SetDamage(int damage)
        {
            damagenumber = damage;
        }
    }
}
