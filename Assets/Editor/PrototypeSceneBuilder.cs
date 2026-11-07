#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PrototypeSceneBuilder
{
    [MenuItem("Tools/ShiroAkari/Create Prototype Scene")]
    public static void Create()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        RenderSettings.ambientLight = new Color(.65f, .72f, .78f);
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(.72f, .82f, .86f);
        RenderSettings.fogDensity = .008f;

        var light = new GameObject("Sun"); var sun = light.AddComponent<Light>();
        sun.type = LightType.Directional; sun.intensity = 1.2f; light.transform.rotation = Quaternion.Euler(45, -30, 0);

        MakePrimitive("Meadow Ground", PrimitiveType.Plane, new Vector3(0,0,0), new Vector3(12,1,12), new Color(.28f,.55f,.3f));
        for (int i=0;i<24;i++)
        {
            float a=i*Mathf.PI*2/24f; float r=Random.Range(8f,18f);
            var tree=MakePrimitive("Meadow Tree",PrimitiveType.Cylinder,new Vector3(Mathf.Cos(a)*r,1.5f,Mathf.Sin(a)*r),new Vector3(.5f,1.5f,.5f),new Color(.25f,.38f,.2f));
            MakePrimitive("Tree Canopy",PrimitiveType.Sphere,tree.transform.position+Vector3.up*2.2f,Vector3.one*2.1f,new Color(.18f,.48f,.25f));
        }

        var player=MakePrimitive("Player",PrimitiveType.Capsule,new Vector3(0,1, -4),Vector3.one,new Color(.3f,.55f,.9f));
        var cc=player.AddComponent<CharacterController>(); cc.height=2; cc.center=new Vector3(0,1,0);
        var movement=player.AddComponent<ThirdPersonPlayer>();

        var camObj=new GameObject("Main Camera"); camObj.tag="MainCamera";
        var cam=camObj.AddComponent<Camera>(); cam.fieldOfView=65; camObj.AddComponent<AudioListener>();
        var orbit=camObj.AddComponent<OrbitFollowCamera>(); orbit.target=player.transform; movement.cameraTransform=camObj.transform;
        var interactor=player.AddComponent<PlayerInteractor>(); interactor.viewCamera=cam;

        var journal=new GameObject("Knowledge Journal"); journal.AddComponent<KnowledgeJournal>();
        var quests=new GameObject("Quest Manager"); quests.AddComponent<QuestManager>();
        var save=new GameObject("Save System"); var saver=save.AddComponent<SaveSystem>(); saver.player=player.transform;

        var pickup=MakePrimitive("Ancient Leaf Specimen",PrimitiveType.Sphere,new Vector3(3,.8f,2),Vector3.one*.8f,new Color(.3f,.85f,.45f));
        pickup.AddComponent<KnowledgePickup>();

        var npc=MakePrimitive("Meadow Keeper",PrimitiveType.Capsule,new Vector3(-4,1,3),Vector3.one,new Color(.85f,.7f,.4f));
        npc.AddComponent<SimpleNPC>();

        var door=MakePrimitive("Sealed Gate",PrimitiveType.Cube,new Vector3(0,1.5f,10),new Vector3(4,3,.5f),new Color(.35f,.3f,.5f));
        var rune=MakePrimitive("Rune Pedestal",PrimitiveType.Cylinder,new Vector3(0,.5f,7),new Vector3(1,.5f,1),new Color(.5f,.7f,1));
        var puzzle=rune.AddComponent<RunePuzzle>(); puzzle.door=door;

        for(int i=0;i<8;i++)
        {
            float x=Random.Range(-10f,10f), z=Random.Range(-10f,12f);
            MakePrimitive("Ancient Stone",PrimitiveType.Cube,new Vector3(x,.35f,z),new Vector3(.7f,.7f,.7f),new Color(.42f,.43f,.46f));
        }

        var canvasHint=new GameObject("Prototype Instructions");
        var text=canvasHint.AddComponent<PrototypeInstructions>();
        EditorSceneManager.SaveScene(scene,"Assets/Scenes/WhisperingMeadow.unity");
        AssetDatabase.Refresh();
        Debug.Log("Whispering Meadow created. Press Play!");
    }

    static GameObject MakePrimitive(string name, PrimitiveType type, Vector3 pos, Vector3 scale, Color color)
    {
        var go=GameObject.CreatePrimitive(type); go.name=name; go.transform.position=pos; go.transform.localScale=scale;
        var mat=new Material(Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard"));
        mat.color=color; go.GetComponent<Renderer>().sharedMaterial=mat;
        return go;
    }
}
#endif
