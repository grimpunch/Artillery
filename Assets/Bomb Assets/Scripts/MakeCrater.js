var crater : Texture2D;	// Alpha channel is used for the shape of the crater
var craterDepth : float;	// Values of less than 1 make crater shallower, with 0 having no effect on the terrain
var Bigcrater: Texture2D;
var BigcraterDepth : float;
private var terrain : TerrainData;
private var craterData : Color[];
private var BigcraterData : Color[];
private var heightmap : float[];

function Start () {
	terrain = Terrain.activeTerrain.terrainData;
	// Set array of crater data based on the crater depth value
	craterData = crater.GetPixels();
	BigcraterData = Bigcrater.GetPixels();
	for (cData in craterData) {
		cData.a *= craterDepth;
	}
	
	for (cData in BigcraterData) {
		cData.a *= BigcraterDepth;
	}
	
	// Store original terrain heightmap...can't seem to create an arbitary built-in 2D array in Javascript, so do it this way instead
	heightmap = new float[terrain.heightmapWidth * terrain.heightmapHeight];
	var tData = terrain.GetHeights(0, 0, terrain.heightmapWidth, terrain.heightmapHeight);
	for (var i = 0; i < terrain.heightmapHeight; i++) {
		for (var j = 0; j < terrain.heightmapWidth; j++) {
			heightmap[i*terrain.heightmapWidth + j] = tData[i,j];
		}
	}
}

function OnApplicationQuit () {
	// Restore original terrain heightmap...again, this is a lot more annoying than it should be because of the built-in 2D array problem
	var tData = terrain.GetHeights(0, 0, terrain.heightmapWidth, terrain.heightmapHeight);
	for (var i = 0; i < terrain.heightmapHeight; i++) {
		for (var j = 0; j < terrain.heightmapWidth; j++) {
			tData[i,j] = heightmap[i*terrain.heightmapWidth + j];
		}
	}
	terrain.SetHeights(0, 0, tData);
}
function Reset()
{
	// Restore original terrain heightmap...again, this is a lot more annoying than it should be because of the built-in 2D array problem
	var tData = terrain.GetHeights(0, 0, terrain.heightmapWidth, terrain.heightmapHeight);
	for (var i = 0; i < terrain.heightmapHeight; i++) {
		for (var j = 0; j < terrain.heightmapWidth; j++) {
			tData[i,j] = heightmap[i*terrain.heightmapWidth + j];
		}
	}
	terrain.SetHeights(0, 0, tData);
}
function BigCrater (hitX : float, hitZ : float) {
	// Get heightmap coordinates from hit point
	var x : int = Mathf.Lerp(0, terrain.heightmapWidth, Mathf.InverseLerp(0, terrain.size.x, hitX));
	var z : int = Mathf.Lerp(0, terrain.heightmapHeight, Mathf.InverseLerp(0, terrain.size.z, hitZ));
	// Make sure crater area stays within bounds of heightmap
	x = Mathf.Clamp(x, Bigcrater.width/2, terrain.heightmapWidth-Bigcrater.width/2);
	z = Mathf.Clamp(z, Bigcrater.height/2, terrain.heightmapHeight-Bigcrater.height/2);
	// Get terrain heightmap data from area surrounding hit point
	var tData = terrain.GetHeights(x-Bigcrater.width/2, z-Bigcrater.height/2, Bigcrater.width, Bigcrater.height);
	// Subtract crater from heightmap data
	for (var i = 0; i < Bigcrater.height; i++) {
		for (var j = 0; j < Bigcrater.width; j++) {
			tData[i,j] = tData[i,j] - BigcraterData[i*Bigcrater.width + j].a;	// Can't do "tData -= craterData[]"; generates error
		}
	}
	// Write modified heightmap data back to the terrain
	terrain.SetHeights(x-Bigcrater.width/2, z-Bigcrater.height/2, tData);
}
function Crater (hitX : float, hitZ : float) {
	// Get heightmap coordinates from hit point
	var x : int = Mathf.Lerp(0, terrain.heightmapWidth, Mathf.InverseLerp(0, terrain.size.x, hitX));
	var z : int = Mathf.Lerp(0, terrain.heightmapHeight, Mathf.InverseLerp(0, terrain.size.z, hitZ));
	// Make sure crater area stays within bounds of heightmap
	x = Mathf.Clamp(x, crater.width/2, terrain.heightmapWidth-crater.width/2);
	z = Mathf.Clamp(z, crater.height/2, terrain.heightmapHeight-crater.height/2);
	// Get terrain heightmap data from area surrounding hit point
	var tData = terrain.GetHeights(x-crater.width/2, z-crater.height/2, crater.width, crater.height);
	// Subtract crater from heightmap data
	for (var i = 0; i < crater.height; i++) {
		for (var j = 0; j < crater.width; j++) {
			tData[i,j] = tData[i,j] - craterData[i*crater.width + j].a;	// Can't do "tData -= craterData[]"; generates error
		}
	}
	// Write modified heightmap data back to the terrain
	terrain.SetHeights(x-crater.width/2, z-crater.height/2, tData);
}