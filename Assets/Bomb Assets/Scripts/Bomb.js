var explosion : GameObject;
private var alreadyHit = false;
public var Velocity : Vector3;
public var TTL:float;
    public var BigBomb = false;
    public var isBallistic:boolean;
    public var Drag : float; // in metres/s lost per second.
    public var Arty :GameObject;
    public var ExplosiveForceonObject:float;
    public var GameLogic : GameObject;
    
function Start () {
	// Ignore collisions with other bombs
	Arty = GameObject.FindGameObjectWithTag("ArtyBarrel");
	GameLogic = GameObject.FindGameObjectWithTag("GameLogic");
        if (TTL == 0)
            TTL = 20;
       // print(TTL);
        Invoke("projectileTimeout", TTL);
   
    ExplosiveForceonObject = 200f;
	var bombs = FindObjectsOfType(Bomb);
	for (bomb in bombs) {
		if (bomb != this)
		 {
		 Physics.IgnoreCollision(collider, bomb.gameObject.collider);
		// Physics.IgnoreCollision(collider, Arty.gameObject.collider);
		 }
	}
	
}

public function SetVelocity(VelocityIn:Vector3)
{
Velocity = VelocityIn;
}
    
    
    // Use this for initialization
  
    // Update is called once per frame
    function Update () 
    {
        if (Drag != 0)
            Velocity += Velocity * (-Drag * Time.deltaTime);
        
        if (isBallistic)
            Velocity += Physics.gravity * Time.deltaTime;
        
        if (Velocity == Vector3.zero)
            return;
        else
            transform.position += Velocity * Time.deltaTime;
        transform.LookAt(transform.position + Velocity.normalized);
        Debug.DrawLine(transform.position, transform.position + Velocity.normalized, Color.red);
        //transform.LookAt(transform.forward);
    }
    
    function projectileTimeout()
    {
    	GameObject.FindGameObjectWithTag("GameLogic").SendMessage("ExplosionEndedSet");
        DestroyObject(gameObject);
    }
    

function OnCollisionEnter (collision : Collision) {
	if (alreadyHit) {return;}
	
	if(collision.gameObject.tag == "powerup")
	{
	GameLogic.SendMessage("upgrade");
	GameLogic.SendMessage("upgrade");
	alreadyHit = true;	// prevent multiple OnCollisionEnter events
	Instantiate(explosion, transform.position + Vector3.up, Quaternion.identity);
	FindObjectOfType(SplatTexture).ScorchMark(transform.position.x, transform.position.z);
	FindObjectOfType(MakeCrater).BigCrater(transform.position.x, transform.position.z);
	Destroy(collision.gameObject);
	Destroy(gameObject);
	}
	
	if(collision.gameObject.tag == "World")
	{
	alreadyHit = true;	// prevent multiple OnCollisionEnter events
	Instantiate(explosion, transform.position + Vector3.up, Quaternion.identity);
	FindObjectOfType(SplatTexture).ScorchMark(transform.position.x, transform.position.z);
	if(BigBomb == false){FindObjectOfType(MakeCrater).Crater(transform.position.x, transform.position.z);}
	else{FindObjectOfType(MakeCrater).BigCrater(transform.position.x, transform.position.z);}
	Destroy(gameObject);
	}
	/*else if(collision.gameObject.tag == "ArtyLegBR"||collision.gameObject.tag == "ArtyLegBL"||collision.gameObject.tag == "ArtyLegFR"||collision.gameObject.tag == "ArtyLegFL")
	{
	alreadyHit = true;	// prevent multiple OnCollisionEnter events
	Instantiate(explosion, transform.position + Vector3.up, Quaternion.identity);
	Debug.Log("Hit leg");
	collision.rigidbody.AddExplosionForce(ExplosiveForceonObject,collision.gameObject.transform.position,200f);
	//Destroy(collision.gameObject);
	collision.gameObject.renderer.enabled = false;
	collision.gameObject.collider.enabled = false;
	Destroy(gameObject);
	
	}
	*/
	else if(collision.gameObject.tag == "Brick")
	{
	alreadyHit = true;	// prevent multiple OnCollisionEnter events
	Instantiate(explosion, transform.position + Vector3.up, Quaternion.identity);
	collision.rigidbody.AddExplosionForce(ExplosiveForceonObject,collision.gameObject.transform.position,150f);
	/*FindObjectOfType(SplatTexture).ScorchMark(transform.position.x, transform.position.z);
	FindObjectOfType(MakeCrater).Crater(transform.position.x, transform.position.z);*/
	Destroy(gameObject);
	}
	else
	{
	Instantiate(explosion, transform.position + Vector3.up, Quaternion.identity);
	collision.rigidbody.AddExplosionForce(ExplosiveForceonObject,collision.gameObject.transform.position,150f);
	FindObjectOfType(SplatTexture).ScorchMark(transform.position.x, transform.position.z);
	if(BigBomb == false){FindObjectOfType(MakeCrater).Crater(transform.position.x, transform.position.z);}
	else{FindObjectOfType(MakeCrater).BigCrater(transform.position.x, transform.position.z);}
	Destroy(gameObject);
	}
}