var explosion : GameObject;
private var alreadyHit = false;

function Start () {
	// Ignore collisions with other bombs
	var bombs = FindObjectsOfType(Bomb);
	for (bomb in bombs) {
		if (bomb != this) {Physics.IgnoreCollision(collider, bomb.gameObject.collider);}
	}
}

function SetVelocity(velocity){
    rigidbody.AddForce(velocity,ForceMode.VelocityChange);
}

function OnCollisionEnter () {
	if (alreadyHit) {return;}
	
	alreadyHit = true;	// prevent multiple OnCollisionEnter events
	Instantiate(explosion, transform.position + Vector3.up, Quaternion.identity);
	FindObjectOfType(SplatTexture).ScorchMark(transform.position.x, transform.position.z);
	FindObjectOfType(MakeCrater).Crater(transform.position.x, transform.position.z);
	Destroy(gameObject);
}