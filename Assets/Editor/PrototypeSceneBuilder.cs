#if UNITY_EDITOR
using UnityEditor;using UnityEditor.SceneManagement;using UnityEngine;
public static class PrototypeSceneBuilder
{
    [MenuItem("Tools/ShiroAkari/Create Prototype Scene")]
    public static void Create()
    {
        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,NewSceneMode.Single);
        RenderSettings.ambientLight=new Color(.65f,.72f,.78f);RenderSettings.fog=true;RenderSettings.fogColor=new Color(.72f,.82f,.86f);RenderSettings.fogDensity=.008f;
        var sunObj=new GameObject("Sun");var sun=sunObj.AddComponent<Light>();sun.type=LightType.Directional;sun.intensity=1.2f;sunObj.transform.rotation=Quaternion.Euler(45,-30,0);
        Make("Meadow Ground",PrimitiveType.Plane,new Vector3(0,0,0),new Vector3(12,1,12),new Color(.28f,.55f,.3f));
        for(int i=0;i<24;i++){float a=i*Mathf.PI*2/24f,r=Random.Range(8f,18f);Vector3 p=new Vector3(Mathf.Cos(a)*r,1.5f,Mathf.Sin(a)*r);Make("Meadow Tree",PrimitiveType.Cylinder,p,new Vector3(.5f,1.5f,.5f),new Color(.25f,.38f,.2f));Make("Tree Canopy",PrimitiveType.Sphere,p+Vector3.up*2.2f,Vector3.one*2.1f,new Color(.18f,.48f,.25f));}
        var player=Make("Player",PrimitiveType.Capsule,new Vector3(0,1,-4),Vector3.one,new Color(.3f,.55f,.9f));var cc=player.AddComponent<CharacterController>();cc.height=2;cc.center=new Vector3(0,1,0);var movement=player.AddComponent<ThirdPersonPlayer>();
        var cameraObject=new GameObject("Main Camera");cameraObject.tag="MainCamera";var cam=cameraObject.AddComponent<Camera>();cam.fieldOfView=65;cameraObject.AddComponent<AudioListener>();var orbit=cameraObject.AddComponent<OrbitFollowCamera>();orbit.target=player.transform;movement.cameraTransform=cameraObject.transform;var interactor=player.AddComponent<PlayerInteractor>();interactor.viewCamera=cam;
        new GameObject("Knowledge Journal").AddComponent<KnowledgeJournal>();new GameObject("Inventory").AddComponent<InventorySystem>();new GameObject("Quest Manager").AddComponent<QuestManager>();var save=new GameObject("Save System").AddComponent<SaveSystem>();save.player=player.transform;
        var leaf=Make("Ancient Leaf Specimen",PrimitiveType.Sphere,new Vector3(3,.8f,2),Vector3.one*.8f,new Color(.3f,.85f,.45f));leaf.AddComponent<KnowledgePickup>();
        var shard=Make("Star Shard",PrimitiveType.Sphere,new Vector3(-2,.7f,1),Vector3.one*.65f,new Color(.5f,.8f,1));shard.AddComponent<StarShardPickup>();
        var npc=Make("Meadow Keeper",PrimitiveType.Capsule,new Vector3(-4,1,3),Vector3.one,new Color(.85f,.7f,.4f));npc.AddComponent<SimpleNPC>();
        var gate=Make("Sealed Gate",PrimitiveType.Cube,new Vector3(0,1.5f,10),new Vector3(4,3,.5f),new Color(.35f,.3f,.5f));
        var rune=Make("Rune Pedestal",PrimitiveType.Cylinder,new Vector3(0,.5f,7),new Vector3(1,.5f,1),new Color(.5f,.7f,1));rune.AddComponent<RunePuzzle>().door=gate;
        for(int i=0;i<8;i++){float x=Random.Range(-10f,10f),z=Random.Range(-10f,12f);Make("Ancient Stone",PrimitiveType.Cube,new Vector3(x,.35f,z),Vector3.one*.7f,new Color(.42f,.43f,.46f));}
        new GameObject("Prototype Instructions").AddComponent<PrototypeInstructions>();
        EditorSceneManager.SaveScene(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene(),"Assets/Scenes/WhisperingMeadow.unity");AssetDatabase.Refresh();Debug.Log("Whispering Meadow generated. Press Play!");
    }
    static GameObject Make(string n,PrimitiveType t,Vector3 p,Vector3 s,Color c){var g=GameObject.CreatePrimitive(t);g.name=n;g.transform.position=p;g.transform.localScale=s;var sh=Shader.Find("Universal Render Pipeline/Lit");var m=new Material(sh?sh:Shader.Find("Standard"));m.color=c;g.GetComponent<Renderer>().sharedMaterial=m;return g;}
}
#endif
