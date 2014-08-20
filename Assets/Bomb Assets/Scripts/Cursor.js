var bomb : GameObject;
var bombHeight = 40;

function Start() {
	// Can uncomment the next line when projectors work on terrains
//	Screen.showCursor = false;
}

function Update () {
	var hit : RaycastHit;
	var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	if (Physics.Raycast (ray, hit)) {
		transform.position = hit.point + Vector3.up*20;
	}
	if (Input.GetButtonDown("Fire1")) {
		Instantiate(bomb, Vector3(hit.point.x, bombHeight, hit.point.z), Quaternion.identity);
	}
}