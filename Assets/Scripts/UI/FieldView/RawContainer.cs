using UnityEngine;

public class RawContainer : MonoBehaviour
{
    public FieldView PutElement(FieldView fieldViewTemplate)
    {
        FieldView fieldView = Instantiate(fieldViewTemplate, transform);
        return fieldView;
    }
}