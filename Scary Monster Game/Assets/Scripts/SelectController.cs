//using System.Collections;
//using System.Collections.Generic;
//using Unity.IO.LowLevel.Unsafe;
//using UnityEngine;
////Source https://www.youtube.com/watch?v=_yf5vzZ2sYE&ab_channel=InfallibleCode
//public class SelectController : MonoBehaviour
//{
//    [SerializeField] private string tag = "Interactable";
//    [SerializeField] private Material selectedMaterial;    
//    private Material defaultMaterial;

//    private Transform _obj;
//    private void Update()
//    {
//        if (_obj != null)
//        {
//            var renderObject = _obj.GetComponent<Renderer>();
//            renderObject.material = defaultMaterial;
//            _obj.tag = "Interactable";
//            _obj = null;
//        }
//        var interact = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
//        RaycastHit objectFound;
//        if (Physics.Raycast(interact, out objectFound))
//        {
//            float distanceCheck = objectFound.distance;
//            if (distanceCheck <= 5)
//            {
//                var obj = objectFound.transform;
//                if (obj.tag == "Interactable" || obj.tag == "Usable")
//                {
//                    var renderObject = obj.GetComponent<Renderer>();
//                    if (renderObject != null)
//                    {
//                        defaultMaterial = renderObject.material;
//                        renderObject.material = selectedMaterial;
//                        obj.tag = "Usable";
//                    }

//                    _obj = obj;
//                }
//            }
//        }


//    }
//    public virtual void Interact()
//    {

//    }
//}