var crater : Texture2D;	// Alpha channel is used for the shape of the crater
var craterDepth : float = .5;	// Values of less than 1 make crater shallower, with 0 having no effect on the terrain
private var terrain : TerrainData;
private var craterData : Color[];
private var heightmap : float[];

function Start () {
	terrain = Terrain.activeTerrain.terrainData;
	// Set array of crater data based on the crater depth value
	craterData = crater.GetPixels();
	for (cData in craterData) {
		cData.a *= craterDepth;
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
	for (var w = 0; w < terrain.heightmapHeight; w++) {
		for (var q = 0; q < terrain.heightmapWidth; q++) {
			tData[w,q] = heightmap[w*terrain.heightmapWidth + q];
		}
	}
	terrain.SetHeights(0, 0, tData);
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