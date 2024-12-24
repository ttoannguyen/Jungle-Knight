using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulltMoveSpeed;
    [SerializeField] private int burstCount;
    [SerializeField] private int projectilePerBurst;
    [SerializeField][Range(0, 359)] private float angleSpread;
    [SerializeField] private float startingDistance = 0.1f;
    [SerializeField] private float timeBetweenBursts;
    [SerializeField] private float restTime = 1f;

    private bool isShooting = false;

    public void Attack()
    {
        if (!isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
    }
    #region Shooting - old
    // private IEnumerator ShootRoutine()
    // {
    //     isShooting = true;
    //     for (int i = 0; i < burstCount; i++)
    //     {
    //         Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
    //         GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    //         newBullet.transform.right = targetDirection;
    //         if (newBullet.TryGetComponent(out Projectile projectile))
    //         {
    //             projectile.UpdateMoveSpeed(bulltMoveSpeed);
    //         }
    //         yield return new WaitForSeconds(timeBetweenBursts);
    //     }

    //     yield return new WaitForSeconds(restTime);
    //     isShooting = false;
    // }
    #endregion
    private IEnumerator ShootRoutine()
    {
        isShooting = true;
        float startAngle, currentAngle, angleStep;
        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep);
        for (int i = 0; i < burstCount; i++)
        {
            for (int j = 0; j < projectilePerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);
                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;

                if (newBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateMoveSpeed(bulltMoveSpeed);
                }

                currentAngle += angleStep;

            }

            currentAngle = startAngle;

            yield return new WaitForSeconds(timeBetweenBursts);

            TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep);
        }

        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }

    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep)
    {
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        float endAngle = targetAngle;
        currentAngle = targetAngle;
        float haftAngleSpread = 0f;
        angleStep = 0f;

        if (angleSpread != 0)
        {
            angleStep = angleSpread / (projectilePerBurst - 1);
            haftAngleSpread = angleSpread / 2;
            startAngle = targetAngle - haftAngleSpread;
            endAngle = targetAngle + haftAngleSpread;
            currentAngle = startAngle;
        }
    }

    private Vector2 FindBulletSpawnPos(float currentAngle)
    {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        Vector2 pos = new(x, y);

        return pos;
    }
}
