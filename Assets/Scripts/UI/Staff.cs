using UnityEngine;

public class Staff : MonoBehaviour, IWeapon {

	[SerializeField] private WeaponInfo _weaponInfo;

	private void Update() {
		MouseFollowWithOffset();
	}

	public void Attack() {
		Debug.Log("Staff Attack");
	}

	public WeaponInfo GetWeaponInfo() {
		return _weaponInfo;
	}

	private void MouseFollowWithOffset() {
		Vector3 mousePos = Input.mousePosition;
		Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

		if (mousePos.x < playerScreenPoint.x) {
			ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
		}
		else {
			ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
		}
	}
}
