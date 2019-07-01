using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Text;
using System;
using System.Collections.Generic;

public class DialogXmlReading{

	private Dictionary<int,string> xmlDic = new Dictionary<int, string>();

	public DialogXmlReading() {
		TextAsset textAsset = (TextAsset)Resources.Load ("Xml/Dialog");
		XmlDocument dialogXmlDoc = new XmlDocument ();
		StringReader stringReader = new StringReader (textAsset.text);

		dialogXmlDoc.LoadXml (stringReader.ReadToEnd());

		stringReader.Close ();

		XmlNode node = dialogXmlDoc.SelectSingleNode("Dialog");
		XmlNodeList nodeList = node.SelectNodes ("Text");


		foreach (XmlNode child in nodeList) {
			int temp = Convert.ToInt32 (child.Attributes ["Id"].Value);
			xmlDic.Add (temp, child.InnerText);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string getDialog(int code){
		if(xmlDic.ContainsKey(code))
		{
			string value = xmlDic[code];

			return value;
		}

		string falseString = "text is not exist in xml";
		return falseString;
	}


}
