using UnityEngine;
using System.Collections;
using System.Text;
using UnityEditor;

public class LevelEditor : MonoBehaviour {

	public GameObject platforms;
	bool showGUI = false;
	bool saveWindowShow = false;
	ArrayList allBlocks;
	ArrayList blueBlocks;
	ArrayList redBlocks;
	ArrayList greenBlocks;
	ArrayList yellowBlocks;
	ArrayList grassBlocks;

	Ray ray;
	RaycastHit2D hit;

	public string path;

	// Use this for initialization
	void Start () {
		blueBlocks = new ArrayList();
		redBlocks = new ArrayList();
		greenBlocks = new ArrayList();
		yellowBlocks = new ArrayList();
		grassBlocks = new ArrayList();
		allBlocks = new ArrayList();
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		path = "";
		platforms = GameObject.Find("Platforms");
		if(platforms.transform.childCount > 0){
			foreach(Transform child in platforms.transform){
				allBlocks.Add(child.gameObject);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Mouse0) && !showGUI && !saveWindowShow){
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			hit = Physics2D.GetRayIntersection(ray);
			if(hit.collider != null){
				foreach(GameObject block in allBlocks){
					block.GetComponent<PositionBlock>().hasFocus = false;
				}
				hit.collider.gameObject.GetComponent<PositionBlock>().hasFocus = true;
			}
			else if(!showGUI && !saveWindowShow){
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				foreach(GameObject block in allBlocks){
					PositionBlock pb = block.GetComponent<PositionBlock>();
					if(pb.hasFocus){
						Vector3 v = Vector3.zero;
						v.x = Mathf.Floor(ray.origin.x);
						v.y = Mathf.Floor(ray.origin.y) + 0.25f;
						v.z = 0;
						pb.clampAndMove(v);
					}
				}
			}
		}
		Debug.DrawLine(ray.origin, hit.point, Color.red);
	}

	void OnGUI(){

		float WindowX = 0.2f * Screen.width;
		float WindowY = 0.1f * Screen.height;
		float WindowW = 0.6f * Screen.width;
		float WindowH = 0.8f * Screen.height;

		Event e = Event.current;
		
		if(e.button == 1){
			showGUI = true;
		}

		if(Input.GetKeyDown(KeyCode.Escape)){
			showGUI = false;
		}

		if(showGUI){
			GUI.Window(0, new Rect(WindowX, WindowY, WindowW, WindowH), DoWindow, "Tile Window");
		}

		if(saveWindowShow){
			GUI.Window(0, new Rect(WindowX, WindowY, WindowW, WindowH), saveWindow, "Save Window");
		}
	}

	private void createBlock(int numBlocks, string partialPath){

		StringBuilder path = new StringBuilder();
		path.Append("Prefabs/LevelComponents/");
		path.Append(partialPath); 
		GameObject go = (GameObject)Instantiate(Resources.Load(path.ToString()));
		go.transform.parent = platforms.transform;
		go.transform.position = new Vector3(0.0f,0.25f,0.0f);
		go.GetComponent<PositionBlock>().numBlocks = numBlocks;
		blueBlocks.Add(go);
		
		if(allBlocks.Count > 0){
			foreach(GameObject block in allBlocks){
				block.GetComponent<PositionBlock>().hasFocus = false;
			}
		}
		go.GetComponent<PositionBlock>().hasFocus = true;
		allBlocks.Add(go);
	}

	private void saveWindow(int windowID){

		foreach(GameObject block in allBlocks){
			block.GetComponent<PositionBlock>().hasFocus = false;
		}

		float x = 0.2f * Screen.width;
		float y = 0.1f * Screen.height;
		float w = 0.2f * Screen.width;
		float h = 0.1f * Screen.height;
		float padding = 0.12f * Screen.height;
	
		path = GUI.TextField(new Rect(x,y,w,h),path);

		y += padding;

		if(GUI.Button(new Rect(x,y,w,h), "Save") && Input.GetMouseButtonUp(0)){
			StringBuilder sb = new StringBuilder();
			sb.Append("Assets/Scenes/");
			sb.Append(path);
			sb.Append(".unity");

			path = sb.ToString();

			Debug.Log("Saving: " + EditorApplication.SaveScene(path));
		}

		y += padding;

		if(GUI.Button(new Rect(x,y,w,h), "Close") && Input.GetMouseButtonUp(0)){
			saveWindowShow = false;
		}
	}

	private void DoWindow(int windowID){
		float x = 0.18f * Screen.width;
		float y = 0.1f * Screen.height;
		float w = 0.1f * Screen.width;
		float h = 0.1f * Screen.height;
		float padding = 0.12f * Screen.height;
		
		// 2 blocks column
		if(GUI.Button(new Rect(x,y,w,h), "2b") && Input.GetMouseButtonUp(0)){
			if(blueBlocks.Count < 10){
				createBlock(2, "2_blocks/2_b_blocks1");
			}
			else{
				Debug.Log("That's 10 already, fool");
			}
		}
		y += padding;

		if(GUI.Button(new Rect(x,y,w,h), "2r") && Input.GetMouseButtonUp(0)){
			if(redBlocks.Count < 10){
				createBlock(2, "2_blocks/2_r_blocks1");
			}
			else{
				Debug.Log("That's 10 already, fool");
			}
		}
		y += padding;

		if(GUI.Button(new Rect(x,y,w,h), "2g") && Input.GetMouseButtonUp(0)){
			if(greenBlocks.Count < 10){
				createBlock(2, "2_blocks/2_g_blocks1");
			}
			else{
				Debug.Log("That's 10 already, fool");
			}
		}
		y += padding;

		if(GUI.Button(new Rect(x,y,w,h), "2y") && Input.GetMouseButtonUp(0)){
			if(yellowBlocks.Count < 10){
				createBlock(2, "2_blocks/2_y_blocks1");
			}
			else{
				Debug.Log("That's 10 already, fool");
			}
		}
		y += padding * 1.5f;

		if(GUI.Button (new Rect(x,y,w,h), "Save") && Input.GetMouseButtonUp(0)){
			showGUI = false;
			saveWindowShow = true;
		}
		
		y = 0.1f * Screen.height;
		x += padding*2;

		//4 blocks column
		if(GUI.Button(new Rect(x,y,w,h), "4b") && Input.GetMouseButtonUp(0)){
			if(blueBlocks.Count < 10){
				createBlock(4, "4_blocks/4_b_blocks1");
			}
			else{
				Debug.Log("That's 10 already, fool");
			}
		}
		y += padding;

		if(GUI.Button(new Rect(x,y,w,h), "4r") && Input.GetMouseButtonUp(0)){
			if(redBlocks.Count < 10){
				createBlock(4, "4_blocks/4_r_blocks1");
			}
			else{
				Debug.Log("That's 10 already, fool");
			}
		}
		y += padding;

		if(GUI.Button(new Rect(x,y,w,h), "4g") && Input.GetMouseButtonUp(0)){
			if(greenBlocks.Count < 10){
				createBlock(4, "4_blocks/4_g_blocks1");
			}
			else{
				Debug.Log("That's 10 already, fool");
			}
		}
		y += padding;

		if(GUI.Button(new Rect(x,y,w,h), "4y") && Input.GetMouseButtonUp(0)){
			if(yellowBlocks.Count < 10){
				createBlock(4, "4_blocks/4_y_blocks1");
			}
			else{
				Debug.Log("That's 10 already, fool");
			}
		}
		y += padding * 1.5f;

		if(GUI.Button(new Rect(x,y,w,h), "Close") && Input.GetMouseButtonUp(0)){
			showGUI = false;
		}

	}//DoWindow

	private GameObject[] getAllFloors(){
		GameObject[] floors = GameObject.FindGameObjectsWithTag("floor");
		return floors;
	}
}
