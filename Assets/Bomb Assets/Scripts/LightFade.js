// Fades light over time;

var lightfade : float = 2;

function Update () {
	light.color = Color(lightfade, lightfade, lightfade, 1);
	lightfade -= Time.deltaTime;
	lightfade = Mathf.Clamp(lightfade, 0, 2);
}