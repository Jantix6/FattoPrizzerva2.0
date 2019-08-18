using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    public sealed class DC_ImportedQuestionAnswerStructure : I_ImportedDialogStructure
    {




        public void PrintStoredData()
        {
            throw new System.NotImplementedException();
        }

        public void SetValue(int _indexOnLine, string _fieldData)
        {
            throw new System.NotImplementedException();
        }
    }
}

public interface I_ImportedDialogStructure
{


    void PrintStoredData();
    void SetValue(int _indexOnLine, string _fieldData);

}


