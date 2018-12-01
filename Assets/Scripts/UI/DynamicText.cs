using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

// a dynamic text relies on a Text
[RequireComponent(typeof(Text))]
public class DynamicText : MonoBehaviour 
{
    public MonoBehaviour target;

    public string property;
   
    public string prefix = "";

    // one or the other must be set
    protected FieldInfo fieldInfo;
    protected PropertyInfo propInfo;
	protected MethodInfo methodInfo;

    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }


    void Start()
    {
        fieldInfo = target.GetType().GetField(property);
        propInfo = target.GetType().GetProperty(property);
		methodInfo = target.GetType().GetMethod(property);
        if (fieldInfo == null && propInfo == null && methodInfo == null)
            Debug.LogError("Error: unable to retrieve property, field or method '" + property + "' for object " + target);
    }


    void Update()
    {
        object value = GetValue();
        if (value != null)
        {
            //Debug.Log(value);
            text.text = prefix + value.ToString();
        }
    }


    protected object GetValue()
    {
		if (methodInfo !=null)
			return methodInfo.Invoke(target, new Object[0]);
        else if (fieldInfo != null)
            return fieldInfo.GetValue(target);
        else if (propInfo != null)
            return propInfo.GetValue(target, new Object[0]);
        else return null;
    }
}
