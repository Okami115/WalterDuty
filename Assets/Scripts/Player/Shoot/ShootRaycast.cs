using System;
using UnityEngine;

/// <summary>
/// Contains the logic of the bullets by raycast
/// </summary>
public class ShootRaycast : MonoBehaviour
{
    [SerializeField] private string reloadState = "Reloading";
    [SerializeField] private string reloadAnimation = "Reload";
    [SerializeField] private string shootAnimation = "Shoot";

    [SerializeField] Animator shootingAnimator;
    [SerializeField] private string enemy = "Enemy";
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform raycastController;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float Range;
    [SerializeField] bool isAutomatic;

    private bool isShooting;
    private bool canShoot = true;

    private bool reloading = false;
    [SerializeField] private float timeToReload;
    [SerializeField] private float currentTimeToReload;

    [SerializeField] private int points = 10;

    [SerializeField] private int damage;
    [SerializeField]private float fireRate;
    private float timeToShoot;

    [SerializeField] private int bulletsInMagazine;
    [SerializeField] private int maxBulletsInMagazine;

    public event Action relaod;
    public event Action shoot;

    public int BulletsInMagazine { get => bulletsInMagazine; set => bulletsInMagazine = value; }
    public int MaxBulletsInMagazine { get => maxBulletsInMagazine; set => maxBulletsInMagazine = value; }

    void Start()
    {
        BulletsInMagazine = MaxBulletsInMagazine;
        playerController.Shoot += Shoot;
        playerController.Reload += Reload;
    }

    void Update()
    {
        timeToShoot += Time.deltaTime;

        if(reloading)
        {
            currentTimeToReload -= Time.deltaTime;
        }
        if(currentTimeToReload < 0)
        {
            currentTimeToReload = timeToReload;
            reloading = false;
            //TODO: TP2 - SOLID
            shootingAnimator.SetBool(reloadState, false);
        }


        if(canShoot && isShooting && timeToShoot > fireRate && BulletsInMagazine != 0 && !reloading) 
        {
            shootingAnimator.Play(shootAnimation);
            shoot.Invoke(); 

            RaycastHit hit;

            if (Physics.Raycast(raycastController.position, raycastController.forward,out hit, Range))
            {
                if(hit.transform.CompareTag(enemy))
                {
                    hit.transform.GetComponent<Health>().TakeDamage(damage);
                    gameManager.Credits += points;
                }
            }

            BulletsInMagazine--;
            timeToShoot = 0;
        }

        if(!isAutomatic && isShooting)
        {
            isShooting = false;
        }
    }

    /// <summary>
    /// read the input to execute the firing logic
    /// </summary>
    /// <param name="input"></param>
    public void Shoot(bool input)
    {
        isShooting = input;
    }

    /// <summary>
    /// contains the logic to start the reload
    /// </summary>
    public void Reload()
    {
        reloading = true;
        BulletsInMagazine = MaxBulletsInMagazine;
        shootingAnimator.SetBool(reloadState, true);
        shootingAnimator.Play(reloadAnimation);
        relaod.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(raycastController.position, raycastController.forward.normalized * Range);
    }
}
