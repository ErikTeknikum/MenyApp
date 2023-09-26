using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

/*  TEMPORÄR KOMMENTAR: UppgiftsLista 
 *  1. div class="Panel Primary" ignoreras.
 *  2. Om tredje maträtten inte finns så krashar programmet.
 *  3. Föra över webbskrapare till Api.
 *  4. Ändra ER Diagram så den kan spara ner menuDate, varchar 8 length. Byt ut med Month i Date.
 *  5. Ändra Json objekt.
 *  6. Lägg till quartz i Api så den skrapar automatiskt efter bestämd tid.
*/

internal class Webscraper_Menu
{
    public  void Main(string[] args)
    {
        string url = "https://mpi.mashie.com/public/app/V%C3%A4xj%C3%B6%20kommun%20ny/6f5fa240";
        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc = web.Load(url);
        HtmlNodeCollection panelParentNode = doc.DocumentNode.SelectNodes("//div[@class='panel panel-default app-default']");

        foreach (HtmlNode node in panelParentNode)
        {
            var luncher = node.SelectNodes("./div[@class='list-group']/div");

            //ChildNode for day
            string dayName = node.SelectSingleNode(".//div//div[2]").InnerText;
            Console.WriteLine(SentenceFixer(dayName));

            //ChildNode for date
            string menuDate = node.SelectSingleNode(".//div//div").InnerText;
            Console.WriteLine(SentenceFixer(menuDate));

            //ChildNode for foodName 1
            string menuFood1 = node.SelectSingleNode(".//div//div//div[2]").InnerText;
            Console.WriteLine("Lunch 1: " + SentenceFixer(menuFood1));

            //ChildNode for foodName 2
            string menuFood2 = node.SelectSingleNode(".//div[2]//div[2]//div[2]").InnerText;
            Console.WriteLine("Lunch 2: " + SentenceFixer(menuFood2));


            //

            /* TEMPORÄR KOMMENTAR: Problem: Se problemlista index 2 vänta, vem?
            *
            *string menuFood3 = node.SelectSingleNode(".//div[2]//div[3]//div[2]").InnerText;
            *Console.WriteLine(SentenceFixer(menuFood3));
            *ChildNode for foodName 3 
            */

            Console.WriteLine();
        }
    }

    public string SentenceFixer(string sentenceNeedsFix)
    {
        //Codes for special characters
        string codeForUpperAUmplaut = "&#196;"; //Ä
        string codeForLowerAUmplaut = "&#228;"; //ä
        string codeForLowerAOverring = "&#229;"; //å
        string codeForUpperOUmlaut = "&#214;"; //Ö
        string codeForLowerOUmlaut = "&#246;"; //ö
        string codeForLowerEAccuteAccent = "&#233;"; //é
        string codeForLowerEAccent = "&#232;"; //è
        string codeForà = "&#224;"; //à
        string codeForAccent = "&#180;"; //´
        string codeForQuotation = "&quot;"; //"


        //Checks sentenceNeedsFix for codes for special characters and replaces them with correct character.    ex: "&#214;" becomes "Ö"
        sentenceNeedsFix = sentenceNeedsFix.Replace(codeForUpperAUmplaut, "Ä");
        sentenceNeedsFix = sentenceNeedsFix.Replace(codeForLowerAUmplaut, "ä");
        sentenceNeedsFix = sentenceNeedsFix.Replace(codeForLowerAOverring, "å");
        sentenceNeedsFix = sentenceNeedsFix.Replace(codeForUpperOUmlaut, "Ö");
        sentenceNeedsFix = sentenceNeedsFix.Replace(codeForLowerOUmlaut, "ö");
        sentenceNeedsFix = sentenceNeedsFix.Replace(codeForLowerEAccuteAccent, "é");
        sentenceNeedsFix = sentenceNeedsFix.Replace(codeForLowerEAccent, "è");
        sentenceNeedsFix = sentenceNeedsFix.Replace(codeForà, "à");
        sentenceNeedsFix = sentenceNeedsFix.Replace(codeForAccent, "´");
        sentenceNeedsFix = sentenceNeedsFix.Replace(codeForQuotation, "\"");


        //returns string with special characters
        return sentenceNeedsFix;
    }
}

