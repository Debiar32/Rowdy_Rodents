using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Overdrive_Attack : MonoBehaviour
{
    [SerializeField] private float damageMultiplier = 1.5f;
    [SerializeField] private float cooldownMultiplier = 0.5f;
    [SerializeField] private float overdriveDuration = 1f;
    [SerializeField] private float activationWindow = 3f;
    [SerializeField] private float overdriveCooldown = 7.5f;
    [SerializeField] private int requiredAttacks = 6;
    [SerializeField] private InputAction overdriveAction;

    private Player_Attack_Manager attackManager;
    private bool canOverdrive = false;
    private bool isOverdrive = false;
    private bool overdriveOnCooldown = false;

    private float baseDamage;
    private float baseCooldown;

    private InputAction.CallbackContext cachedContext;
    private System.Action<InputAction.CallbackContext> callback;

    private void Awake()
    {
        attackManager = GetComponent<Player_Attack_Manager>();
        callback = ctx => TryActivateOverdrive();
        overdriveAction.Enable();
        overdriveAction.performed += callback;
    }

    private void Update()
    {
        if (!canOverdrive && !overdriveOnCooldown && attackManager.Nbr_Attacks >= requiredAttacks && !isOverdrive)
        {
            StartCoroutine(OverdriveActivationWindow());
        }
    }

    private IEnumerator OverdriveActivationWindow()
    {
        canOverdrive = true;
        Debug.Log("Overdrive ready! Press the key now.");
        yield return new WaitForSeconds(activationWindow);
        canOverdrive = false;
        Debug.Log("Overdrive window closed.");
    }

    private void TryActivateOverdrive()
    {
        if (canOverdrive && !isOverdrive && !overdriveOnCooldown)
        {
            StartCoroutine(OverdriveRoutine());
        }
    }

    private IEnumerator OverdriveRoutine()
    {
        isOverdrive = true;
        canOverdrive = false;
        overdriveOnCooldown = true;

        baseDamage = attackManager.Attack_Damage;
        baseCooldown = attackManager.Attack_Cooldown;

        attackManager.Attack_Damage *= damageMultiplier;
        attackManager.Attack_Cooldown *= cooldownMultiplier;

        Debug.Log("Overdrive activated!");

        yield return new WaitForSeconds(overdriveDuration);

        attackManager.Attack_Damage = baseDamage;
        attackManager.Attack_Cooldown = baseCooldown;

        Debug.Log("Overdrive ended.");

        // Reset hit counter here so you can build it up again
        attackManager.Nbr_Attacks = 0;

        yield return new WaitForSeconds(overdriveCooldown);
        overdriveOnCooldown = false;
        isOverdrive = false;
        Debug.Log("Overdrive cooldown complete.");
    }

    private void OnDestroy()
    {
        overdriveAction.performed -= callback;
    }
}
