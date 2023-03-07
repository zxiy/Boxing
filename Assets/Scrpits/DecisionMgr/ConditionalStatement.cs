// wrapper or container for expressions of the form
//  " if (condition) then response()
//
using System.Collections.Generic;

[System.Serializable]
public class ConditionalStatement 
{
    public string description;
    public List<Condition> _conditions;
    public Response _response;
}
