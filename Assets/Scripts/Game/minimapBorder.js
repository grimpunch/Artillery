#pragma strict
@script ExecuteInEditMode
 
var mySkin : GUISkin ;
 
function OnGUI(){
   if(mySkin)
      GUI.skin = mySkin ;
   var cam : Camera = transform.camera ;
   GUI.Box(Rect(cam.pixelRect.x, (Screen.height - cam.pixelRect.yMax), cam.pixelWidth, cam.pixelHeight), "") ;
}