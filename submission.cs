using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;



/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{


    public class Program
    {
        public static string xmlURL = "Your XML URL";
        public static string xmlErrorURL = "Your Error XML URL";
        public static string xsdURL = "Your XSD URL";
        public static string errorMessage = null;

        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);


            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);


            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            XmlSchemaSet sc = new XmlSchemaSet();
            sc.Add(null, xsdURL);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = sc;

            ///event for error
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
            XmlReader reader = XmlReader.Create(xmlUrl, settings);

            while (reader.Read()) ;
            //complete parsing ;; it will navigate to callback if error

            if (errorMessage == null)
            {
                return "No Error";
            }
            else
            {
                return errorMessage;
            }

            //return "No Error" if XML is valid. Otherwise, return the desired exception message.
        }

        private static void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            errorMessage = ("Validation Error: {0}", e.Message);
        }


        public static string Xml2Json(string xmlUrl)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load(xmlUrl);

            string jsonText = JsonConvert.SerializeXmlNode(xd);

            // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))
            return jsonText;

        }
    }

}
