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
        public string GetId ()
        {
            return id;
        }
        public string GetCatTitle()
        {
            return catTitle;
        }
        public string GetEspTitle()
        {
            return espTitle;
        }
        public string GetEngTitle()
        {
            return engTitle;
        }
        public string GetCatBody()
        {
            return catBody;
        }
        public string GetEspBody()
        {
            return espBody;
        }
        public string GetEngBody()
        {
            return engBody;
        }

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
            if (_indexOnLine == config.GetSPeachCSVSettings().ObjectIdentifierPosition)
            {
                id = (_fieldData);
            }
            else if (_indexOnLine == config.GetSPeachCSVSettings().CatTitlePosition)
            {
                catTitle = (_fieldData);
            }
            else if (_indexOnLine == config.GetSPeachCSVSettings().EspTitlePosition)
            {
                espTitle = (_fieldData);
            }
            else if (_indexOnLine == config.GetSPeachCSVSettings().EngTitlePosition)
            {
                engTitle = (_fieldData);
            }
            else if (_indexOnLine == config.GetSPeachCSVSettings().CatBodyPosition)
            {
                catBody = (_fieldData);
            }
            else if (_indexOnLine == config.GetSPeachCSVSettings().EspBodyPosition)
            {
                espBody = (_fieldData);
            }
            else if (_indexOnLine == config.GetSPeachCSVSettings().EngBodyPosition)
            {
                engBody = (_fieldData);
            }
        }
    }
}
