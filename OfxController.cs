using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace OfxImport
{
    class OfxController
    {
        public static IEnumerable<OfxDocument> LerArquivoOfx(string pathToOfxFile)
        {
            if (!File.Exists(pathToOfxFile))
                throw new FileNotFoundException();

            //use LINQ TO GET ONLY THE LINES THAT WE WANT
            var tags = from line in File.ReadAllLines(pathToOfxFile)
                       where line.Contains("<STMTTRN>") ||
                       line.Contains("<TRNTYPE>") ||
                       line.Contains("<DTPOSTED>") ||
                       line.Contains("<TRNAMT>") ||
                       line.Contains("<FITID>") ||
                       line.Contains("<CHECKNUM>") ||
                       line.Contains("<MEMO>")
                       select line;


            var ofxDocument = new List<OfxDocument>();

            foreach (var line in tags)
            {
                if (line.IndexOf("<STMTTRN>") != -1)
                {
                    ofxDocument.Add(new OfxDocument());
                    continue;
                }

                if (getTagName(line) == "CHECKNUM")
                    ofxDocument.Last().Documento = getTagValue(line);
                if (getTagName(line) == "TRNTYPE")
                    ofxDocument.Last().SetCreditoDebito(getTagValue(line));
                if (getTagName(line) == "DTPOSTED")
                    ofxDocument.Last().SetDataOperacao(getTagValue(line));

                if (getTagName(line) == "TRNAMT")
                    ofxDocument.Last().SetValorOperacao(getTagValue(line));

                if (getTagName(line) == "MEMO")
                    ofxDocument.Last().Observacoes = getTagValue(line);
            }

            return ofxDocument;
        }

        /// <summary>
        /// Get the Tag name to create an Xelement
        /// </summary>
        /// <param name="line">One line from the file</param>
        /// <returns></returns>
        private static string getTagName(string line)
        {
            int pos_init = line.IndexOf("<") + 1;
            int pos_end = line.IndexOf(">");
            pos_end = pos_end - pos_init;
            return line.Substring(pos_init, pos_end);
        }

        /// <summary>
        /// Get the value of the tag to put on the Xelement
        /// </summary>
        /// <param name="line">The line</param>
        /// <returns></returns>
        private static string getTagValue(string line)
        {
            int pos_init = line.IndexOf(">") + 1;
            string retValue = line.Substring(pos_init).Trim();
            if (retValue.IndexOf("[") != -1)
            {
                //date--lets get only the 8 date digits
                retValue = retValue.Substring(0, 8);
            }
            return retValue;
        }
    }
}
