using UnityEngine;
using System.Collections;

public class RaiseLowerTerrain : MonoBehaviour
{
	private const string LOG_TAG = "RaiseLowerTerrain - ";
	public bool TestWithMouse = true;
	public Terrain myTerrain;
	public int SmoothArea;
	private int xResolution;
	private int zResolution;
	private float[,] heights;
	private float[,] heightMapBackup;
	protected const float DEPTH_METER_CONVERT=0.05f;
	protected const float TEXTURE_SIZE_MULTIPLIER = 1.25f;
	public int DeformationTextureNum = 1;
	protected int alphaMapWidth;
	protected int alphaMapHeight;
	protected int numOfAlphaLayers;
	private float[, ,] alphaMapBackup;


	private const int LENX_TO_RISE_AREA = 10;
	private const int LENZ_TO_RISE_AREA = 10;
	private const float INCDEC_TO_RISE_AREA = 0.01f;
	private const float CRATER_SIZE_IN_METERS = 10 * 2f;


	private Vector3[] picPointPlayer = new Vector3[3];

	private bool flagStartTimer = false;
	private const float TIME_MAX_PIC_ACTIVE = 10.0f;
	private float startTime = 0.0f;

	private  Vector3 NULL_VECTOR3 = new Vector3(200000,200000,200000);

	void Start()
	{
		xResolution = myTerrain.terrainData.heightmapWidth;
		zResolution = myTerrain.terrainData.heightmapHeight;
		alphaMapWidth = myTerrain.terrainData.alphamapWidth;
		alphaMapHeight = myTerrain.terrainData.alphamapHeight;
		numOfAlphaLayers = myTerrain.terrainData.alphamapLayers;
		if (Debug.isDebugBuild)
		{
			heights = myTerrain.terrainData.GetHeights (0, 0, xResolution, zResolution);    
			heightMapBackup = myTerrain.terrainData.GetHeights(0, 0, xResolution, zResolution);
			alphaMapBackup = myTerrain.terrainData.GetAlphamaps(0, 0, alphaMapWidth, alphaMapHeight);   
		}

		for(int index = 0; index <picPointPlayer.Length; index++){
			picPointPlayer [index]=NULL_VECTOR3;
		}
		
	}
	void OnApplicationQuit()
	{
		if (Debug.isDebugBuild)
		{
			myTerrain.terrainData.SetHeights(0, 0, heightMapBackup);
			myTerrain.terrainData.SetAlphamaps(0, 0, alphaMapBackup);
		}
	}


	
	void Update()
	{
		// This is just for testing with mouse!
		// Point mouse to the Terrain. Left mouse button
		// raises and right mouse button lowers terrain.
		timerRun();

		/*if (TestWithMouse == true) 
		{
			if (Input.GetMouseButtonDown (0)) 
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit)) 
				{
					if(oneMoreActivated(hit.point)){

						// area middle point x and z, area width, area height, smoothing distance, area height adjust
						raiselowerTerrainArea (hit.point, LENX_TO_RISE_AREA, LENZ_TO_RISE_AREA, SmoothArea, INCDEC_TO_RISE_AREA); 
						//raiselowerTerrainPoint(hit.point,0.01f);
						// area middle point x and z, area size, texture ID from terrain textures
						//TextureDeformation (hit.point, CRATER_SIZE_IN_METERS, DeformationTextureNum);
					}
				}
			}
			if (Input.GetMouseButtonDown (1)) 
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit)) 
				{
					// area middle point x and z, area width, area height, smoothing distance, area height adjust
					raiselowerTerrainArea (hit.point, 10, 10, SmoothArea, -0.01f);
					//raiselowerTerrainPoint(hit.point,-0.01f);
					// area middle point x and z, area size, texture ID from terrain textures
					//TextureDeformation (hit.point, 10 * 2f, 0);
				}
			}
		}*/
	}

	/*private void riseHeightSlowly(){

		if (Input.GetMouseButtonDown (0)) 
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) 
			{
				if(oneMoreActivated(hit.point)){
					
					// area middle point x and z, area width, area height, smoothing distance, area height adjust
					raiselowerTerrainArea (hit.point, LENX_TO_RISE_AREA, LENZ_TO_RISE_AREA, SmoothArea, INCDEC_TO_RISE_AREA); 
					//raiselowerTerrainPoint(hit.point,0.01f);
					// area middle point x and z, area size, texture ID from terrain textures
					//TextureDeformation (hit.point, CRATER_SIZE_IN_METERS, DeformationTextureNum);
				}
			}
		}
	}*/


	public void riseController(Vector3 hitPoint){

		if(oneMoreActivated(hitPoint)){
				
				// area middle point x and z, area width, area height, smoothing distance, area height adjust
			raiselowerTerrainArea (hitPoint, LENX_TO_RISE_AREA, LENZ_TO_RISE_AREA, SmoothArea, INCDEC_TO_RISE_AREA); 
				//raiselowerTerrainPoint(hit.point,0.01f);
				// area middle point x and z, area size, texture ID from terrain textures
				//TextureDeformation (hit.point, CRATER_SIZE_IN_METERS, DeformationTextureNum);
		}
	}


	private bool oneMoreActivated(Vector3 newPic){
		bool successNewPic = false;
		for(int index = 0; index <picPointPlayer.Length; index++){
			if (picPointPlayer [index].Equals (NULL_VECTOR3)){
				picPointPlayer [index]=newPic;
				successNewPic = true;
				break;
			}
		}
		if(!picPointPlayer.GetValue(picPointPlayer.Length-1).Equals(NULL_VECTOR3)){ 
			flagStartTimer=true;
			startTime = Time.time;//Time when the shield was rised

			Debug.Log(LOG_TAG+"too much pic start timer");
		}
		return successNewPic;
	}

	private void timerRun(){
		if(flagStartTimer == true){
			Debug.Log(LOG_TAG+"timer for pic is running");

			//currentTimer = TIME_MAX_PIC_ACTIVE - (Time.time - startTime);
			if(startTime + TIME_MAX_PIC_ACTIVE < Time.time){
				flagStartTimer = false;
				for(int index = 0; index <picPointPlayer.Length; index++){
				}
				for(int index = 0; index <picPointPlayer.Length; index++){
					erasePoint(picPointPlayer[index]);
					picPointPlayer[index]= NULL_VECTOR3;
				}

			}
		}
	}

	private void erasePoint(Vector3 pointToErase){
		Debug.Log(LOG_TAG+" erasing pic in method erase1"+pointToErase.ToString());

		// area middle point x and z, area width, area height, smoothing distance, area height adjust
		raiselowerTerrainArea (pointToErase, LENX_TO_RISE_AREA, LENZ_TO_RISE_AREA, SmoothArea, -INCDEC_TO_RISE_AREA);
		Debug.Log(LOG_TAG+" erasing pic in method erase2"+pointToErase.ToString());

		//raiselowerTerrainPoint(hit.point,-0.01f);
		// area middle point x and z, area size, texture ID from terrain textures
		//TextureDeformation (pointToErase, CRATER_SIZE_IN_METERS, 0);
		Debug.Log(LOG_TAG+" erasing pic in method erase3"+pointToErase.ToString());

	}
	
	
	private void raiselowerTerrainArea(Vector3 point, int lenx, int lenz, int smooth, float incdec)
	{
		int areax;
		int areaz;
		smooth += 1;
		float smoothing;
		int terX =(int)((point.x / myTerrain.terrainData.size.x) * xResolution);
		int terZ =(int)((point.z / myTerrain.terrainData.size.z) * zResolution);
		lenx += smooth;
		lenz += smooth;
		terX -= (lenx / 2);
		terZ -= (lenz / 2);
		if (terX < 0) terX = 0;
		if (terX > xResolution)    terX = xResolution;
		if (terZ < 0) terZ = 0;
		if (terZ > zResolution)    terZ = zResolution;
		float[,] heights = myTerrain.terrainData.GetHeights(terX, terZ, lenx, lenz);
		float y = heights[lenx/2,lenz/2];
		y += incdec;
		for (smoothing=1; smoothing < smooth+1; smoothing++) 
		{
			float multiplier = smoothing / smooth;
			for (areax = (int)(smoothing/2); areax < lenx-(smoothing/2); areax++) 
			{
				for (areaz = (int)(smoothing/2); areaz < lenz-(smoothing/2); areaz++) 
				{
					if ((areax > -1) && (areaz > -1) && (areax < xResolution) && (areaz < zResolution)) 
					{
						heights [areax, areaz] = Mathf.Clamp((float)y*multiplier,0,1);
					}
				}
			}
		}
		myTerrain.terrainData.SetHeights (terX, terZ, heights);
	}
	
	private void raiselowerTerrainPoint(Vector3 point, float incdec)
	{
		int terX =(int)((point.x / myTerrain.terrainData.size.x) * xResolution);
		Debug.Log (LOG_TAG+"terX : "+terX);

		int terZ =(int)((point.z / myTerrain.terrainData.size.z) * zResolution);
		Debug.Log (LOG_TAG+"terZ : "+terZ);


		float y = heights[terX,terZ];
		Debug.Log (LOG_TAG+y+"heights"+heights[terX,terZ]);

		y += incdec;
		Debug.Log (LOG_TAG+"y"+y);

		float[,] height = new float[1,1];
		height[0,0] = Mathf.Clamp(y,0,1);
		Debug.Log (LOG_TAG+"height"+height[0,0]);

		heights[terX,terZ] = Mathf.Clamp(y,0,1);
		myTerrain.terrainData.SetHeights(terX, terZ, height);
	}
	
	protected void TextureDeformation(Vector3 pos, float craterSizeInMeters,int textureIDnum)
	{
		Vector3 alphaMapTerrainPos = GetRelativeTerrainPositionFromPos(pos, myTerrain, alphaMapWidth, alphaMapHeight);
		int alphaMapCraterWidth = (int)(craterSizeInMeters * (alphaMapWidth / myTerrain.terrainData.size.x));
		int alphaMapCraterLength = (int)(craterSizeInMeters * (alphaMapHeight / myTerrain.terrainData.size.z));
		int alphaMapStartPosX = (int)(alphaMapTerrainPos.x - (alphaMapCraterWidth / 2));
		int alphaMapStartPosZ = (int)(alphaMapTerrainPos.z - (alphaMapCraterLength/2));
		float[, ,] alphas = myTerrain.terrainData.GetAlphamaps(alphaMapStartPosX, alphaMapStartPosZ, alphaMapCraterWidth, alphaMapCraterLength);
		float circlePosX;
		float circlePosY;
		float distanceFromCenter;
		for (int i = 0; i < alphaMapCraterLength; i++) //width
		{
			for (int j = 0; j < alphaMapCraterWidth; j++) //height
			{
				circlePosX = (j - (alphaMapCraterWidth / 2)) / (alphaMapWidth / myTerrain.terrainData.size.x);
				circlePosY = (i - (alphaMapCraterLength / 2)) / (alphaMapHeight / myTerrain.terrainData.size.z);
				distanceFromCenter = Mathf.Abs(Mathf.Sqrt(circlePosX * circlePosX + circlePosY * circlePosY));
				if (distanceFromCenter < (craterSizeInMeters / 2.0f))
				{
					for (int layerCount = 0; layerCount < numOfAlphaLayers; layerCount++)
					{
						//could add blending here in the future
						if (layerCount == textureIDnum)
						{
							alphas[i, j, layerCount] = 1;
						}
						else
						{
							alphas[i, j, layerCount] = 0;
						}
					}
				}
			}
		} 
		myTerrain.terrainData.SetAlphamaps(alphaMapStartPosX, alphaMapStartPosZ, alphas);
	}
	
	protected Vector3 GetNormalizedPositionRelativeToTerrain(Vector3 pos, Terrain terrain)
	{
		Vector3 tempCoord = (pos - terrain.gameObject.transform.position);
		Vector3 coord;
		coord.x = tempCoord.x / myTerrain.terrainData.size.x;
		coord.y = tempCoord.y / myTerrain.terrainData.size.y;
		coord.z = tempCoord.z / myTerrain.terrainData.size.z;
		return coord;
	}
	
	protected Vector3 GetRelativeTerrainPositionFromPos(Vector3 pos,Terrain terrain, int mapWidth, int mapHeight)
	{
		Vector3 coord = GetNormalizedPositionRelativeToTerrain(pos, terrain);
		return new Vector3((coord.x * mapWidth), 0, (coord.z * mapHeight));
	}     
}