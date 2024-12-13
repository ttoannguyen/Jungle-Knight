using UnityEngine;

public class Sword : MonoBehaviour
{
    private Animator myAnimator;
    private ActiveWeapon activeWeapon;
    private PlayerControls playerControls;
    private PlayerController playerController;

    [SerializeField]
    private GameObject slashAnimPrefab;

    [SerializeField]
    private Transform slashAnimSpawnPoint;

    private GameObject slashAnim;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();

        playerController = GetComponentInParent<PlayerController>();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    private void Attack()
    {
        myAnimator.SetTrigger("Attack");
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }

    private void SwingUpFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180f, 0f, 0f);
        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void SwingDownFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }


    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0f, -180f, angle);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
