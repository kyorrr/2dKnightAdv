using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon {

	[SerializeField] private GameObject _slashAnimPrefab;
	[SerializeField] private Transform _slashAnimSpawnPoint;
	[SerializeField] private WeaponInfo _weaponInfo;

	private Transform _weaponCollider;
	private Animator _animator;

	private GameObject _slashAnim;

	private void Awake() {
		_animator = GetComponent<Animator>();
	}

	private void Start() {
		_weaponCollider = PlayerController.Instance.GetWeaponCollider();
		_slashAnimSpawnPoint = GameObject.Find("SlashSpawnPoint").transform;
	}

	private void Update() {
		MouseFollowWithOffset();
	}

	public WeaponInfo GetWeaponInfo() {
		return _weaponInfo;
	}

	public void Attack() {
		_animator.SetTrigger("Attack");
		_weaponCollider.gameObject.SetActive(true);
		_slashAnim = Instantiate(_slashAnimPrefab, _slashAnimSpawnPoint.position, Quaternion.identity);
		_slashAnim.transform.parent = this.transform.parent;
	}

	public void DoneAttackingAnimEvent() {
		_weaponCollider.gameObject.SetActive(false);
	}

	public void SwingUpFlipAnimEvent() {
		_slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

		if (PlayerController.Instance.FacingLeft) {
			_slashAnim.GetComponent<SpriteRenderer>().flipX = true;
		}
	}
	public void SwingDownFlipAnimEvent() {
		_slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

		if (PlayerController.Instance.FacingLeft) {
			_slashAnim.GetComponent<SpriteRenderer>().flipX = true;
		}
	}

	private void MouseFollowWithOffset() {
		Vector3 mousePos = Input.mousePosition;
		Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

		if (mousePos.x < playerScreenPoint.x) {
			ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
			_weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
		}
		else {
			ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
			_weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
		}
	}
}
