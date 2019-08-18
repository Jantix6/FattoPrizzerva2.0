using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    public sealed class DC_ImportedSpeachStructure : I_ImportedDialogStructure
    {
        SO_DialogObjectCreatorConfig config;

        private string id;

        private string catTitle;
        private string espTitle;
        private string engTitle;

        private string catBody;
        private string espBody;
        private string engBody;

        // private Dictionary<int, Func<string>> setStringDictionary;

        public DC_ImportedSpeachStructure (SO_DialogObjectCreatorConfig _config)
        {
            config = _config;
        }

        public void PrintStoredData( )
        {
            string data = string.Empty;

            data += "id " + id + "\n" +
                    "catTitle " + catTitle + "\n" +
                    "espTitle " + espTitle + "\n" +
                    "engTitle " + engTitle + "\n" +
                    "catBody " + catBody + "\n" +
                    "esptBody " + espBody + "\n" +
                    "engBody " + engBody;

            Debug.Log(data);
        }

        public void SetValue(int _indexOnLine, string _fieldData)
        {
            if (_indexOnLine == config.ObjectIdentifierPosition)
            {
                id = (_fieldData);
            }
            else if (_indexOnLine == config.CatTitlePosition)
            {
                catTitle = (_fieldData);
            }
            else if (_indexOnLine == config.EspTitlePosition)
            {
                espTitle = (_fieldData);
            }
            else if (_indexOnLine == config.EngTitlePosition)
            {
                engTitle = (_fieldData);
            }
            else if (_indexOnLine == config.CatBodyPosition)
            {
                catBody = (_fieldData);
            }
            else if (_indexOnLine == config.EspBodyPosition)
            {
                espBody = (_fieldData);
            }
            else if (_indexOnLine == config.EngBodyPosition)
            {
                engBody = (_fieldData);
            }
        }
    }
}
