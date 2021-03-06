﻿using UnityEngine;
using Assets.Scripts.Util;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Bullets
{
    public class Free : Entity, Bullet
    {
        [SerializeField]
        private int damage;
        [SerializeField]
        private float speed;
        [SerializeField]
        private float raycastRadius;
        [SerializeField]
        private string targetTag;
        [SerializeField]
        private float lifeTime;
        [SerializeField]
        private LayerMask[] rayCastLayers;
        private int rayCastLayer;
        [SerializeField]
        private Transform rayCastPoint;
        private float currentLifeTime;
        [SerializeField]
        private Util.SoundPlayer sound;

        public int getDamage()
        {
            return damage;
        }

        public override void InitData()
        {
            currentLifeTime = 0;
            foreach (LayerMask layerMask in rayCastLayers)
            {
                rayCastLayer = rayCastLayer | layerMask.value;
            }
            sound.PlaySong(0);
        }

        public override void RunEntity()
        {
            switch (direction)
            {
                case Enums.Direction.Up:
                    transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
                    break;
                case Enums.Direction.Down:
                    Vector3 directionDown = transform.up;
                    directionDown.y = -directionDown.y;
                    transform.Translate(directionDown * speed * Time.deltaTime, Space.World);
                    break;
                case Enums.Direction.Left:
                    Vector3 directionLeft = transform.right;
                    directionLeft.x = -directionLeft.x;
                    transform.Translate(directionLeft * speed * Time.deltaTime, Space.World);
                    break;
                case Enums.Direction.Right:
                    transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
                    break;
            }
            RaycastHit2D hit = (Physics2D.CircleCast(transform.position, raycastRadius, Vector2.zero, 0, ~rayCastLayer));
            if (hit)
            {
                if (hit.collider.tag == targetTag)
                    hit.collider.gameObject.GetComponent<Managers.Entity>().HitByEntity(this);
            }
            if ((currentLifeTime += Time.deltaTime) > lifeTime)
                Die();
        }

        public override void HitByEntity(Entity col)
        {
        }
    }
}
