var splat : Texture2D;
var blank : Texture2D;
private var tex : Texture2D;
private var splatTexArray : Color[];
private var projectorSize : float;

// Work off a copy of the texture, since we'll be writing to it
function Start () {
	tex = Instantiate(blank);
	GetComponent(Projector).material.SetTexture("_ShadowTex", tex);
	splatTexArray = splat.GetPixels();
	projectorSize = GetComponent(Projector).orthoGraphicSize * 2;
}

function OnApplicationQuit() {
	GetComponent(Projector).material.SetTexture("_ShadowTex", blank);
	Destroy(tex);
}

function ScorchMark (hitX : float, hitZ : float) {
	// Limit hit area to size of projector
	hitX = Mathf.Clamp(hitX, 0, projectorSize);
	hitZ = Mathf.Clamp(hitZ, 0, projectorSize);
	// Get texture drawing coordinates from hit point
	var uvX : int = Mathf.InverseLerp(0, projectorSize, hitX) * tex.width;
	var uvY : int = Mathf.InverseLerp(0, projectorSize, hitZ) * tex.height;	
	// Make sure coordinates will cause GetPixels array to be within bounds
	uvX = Mathf.Clamp(uvX-splat.width/2, 0, tex.width-splat.width);
	uvY = Mathf.Clamp(uvY-splat.height/2, 0, tex.height-splat.height);
	// Get pixels from background area that matches with splat area, and multiply them with splat pixels
	var bgpix = tex.GetPixels(uvX, uvY, splat.width, splat.height);
	if (Random.value < .5) {
		for (var i = 0; i < bgpix.Length; i++) {
			bgpix[i] *= splatTexArray[i];
		}
	}
	// Half the time, put the splat texture in upside-down & backwards just for more variety
	else {
		var count = splatTexArray.Length;
		for (var e = 0; e < bgpix.Length; e++) {
			bgpix[e] *= splatTexArray[--count];
		}
	}
	// Write changed array back to main texture
	tex.SetPixels(uvX, uvY, splat.width, splat.height, bgpix);
	tex.Apply();
}