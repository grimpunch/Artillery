var timeOut = 1.0;
var detachChildren = false;

function Awake ()
{
	Invoke ("DestroyNow", timeOut);


}



function DestroyNow ()
{
	if (detachChildren) {
		transform.DetachChildren ();
	}
	
	
	GameObject.FindGameObjectWithTag("GameLogic").SendMessage("ExplosionEndedSet");
	DestroyObject (gameObject);
}