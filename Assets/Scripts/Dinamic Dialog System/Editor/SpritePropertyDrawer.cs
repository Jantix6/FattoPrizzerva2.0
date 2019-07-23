using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Experimental.UIElements;

namespace Dialogues 
{
    /* 
    [CustomPropertyDrawer(typeof(Sprite))]
    public class SpritePropertyDrawer : PropertyDrawer
    {


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {

           var ident = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
 
            var spriteRect = new Rect(position.x, position.y 
                                    , position.width , position.height );
    
            property.objectReferenceValue = EditorGUI.ObjectField(spriteRect, property.objectReferenceValue, typeof(Texture2D), false);

            EditorGUI.indentLevel = ident;
        }
 
        public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + 50f;
        }
    }
    */
}


