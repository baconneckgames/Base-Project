//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using System.Collections.Generic;
using Uniject;
using Mono.Xml;

namespace Unibill.Impl {
    public class HelpCentre {
        private Dictionary<UnibillError, string> helpMap = new Dictionary<UnibillError, string>();
        public HelpCentre (UnibillXmlParser parser) {
            foreach (var element in parser.Parse("unibillStrings", "unibillError")) {
                UnibillError error = (UnibillError) Enum.Parse(typeof(UnibillError), element.attributes["id"]);
                helpMap[error] = element.kvps["message"][0];
            }
        }

        public string getMessage(UnibillError error) {
            string url = string.Format("http://www.outlinegames.com/unibillerrors#{0}", error);
            return string.Format ("{0}.\nSee {1}", helpMap[error], url);
        }
    }
}
