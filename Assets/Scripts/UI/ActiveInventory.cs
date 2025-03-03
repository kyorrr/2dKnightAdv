using UnityEngine;

public class ActiveInventory : MonoBehaviour {

	private int _activeSlotIndexNum = 0;

	private PlayerControls _playerControls;

	private void Awake() {
		_playerControls = new PlayerControls();
	}

	private void Start() {
		_playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
		ToggleActiveHighlight(0);
	}

	private void OnEnable() {
		_playerControls.Enable();
	}

	private void ToggleActiveSlot(int numValue) {
		ToggleActiveHighlight(numValue - 1);
	}

	private void ToggleActiveHighlight(int indexNum) {
		_activeSlotIndexNum = indexNum;

		foreach (Transform inventorySlot in this.transform) {
			inventorySlot.GetChild(0).gameObject.SetActive(false);
		}
		this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

		ChangeActiveWeapon();
	}

	private void ChangeActiveWeapon() {
		if (ActiveWeapon.Instance.CurrentActiveWeapon != null) {
			Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
		}

		if (!transform.GetChild(_activeSlotIndexNum).GetComponentInChildren<InventorySlot>()) {
			ActiveWeapon.Instance.WeaponNull();
			return;
		}

		GameObject weaponToSpawn = transform.GetChild(_activeSlotIndexNum).GetComponentInChildren<InventorySlot>().GetWeaponInfo()._weaponPrefab;

		GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);

		ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
		newWeapon.transform.parent = ActiveWeapon.Instance.transform;

		ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
	}
}
