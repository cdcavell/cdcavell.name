using System;

namespace CDCavell.ClassLibrary.Commons
{
    /// <summary>
    /// Static class for converting ascii decimal value to string equivalent.
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0 | 07/05/2020 | Initial build |~ 
    /// </revision>
    public static class AsciiCodes
    {
        /// <value>(null)</value>
        public static string NUL = ToString(00);
        /// <value>(start of heading)</value>
        public static string SOH = ToString(01);
        /// <value>(start of text)</value>
        public static string STX = ToString(02);
        /// <value>(end of text)</value>
        public static string ETX = ToString(03);
        /// <value>(end of transmission)</value>
        public static string EOT = ToString(04);
        /// <value>(enquiry)</value>
        public static string ENQ = ToString(05);
        /// <value>(acknowledge)</value>
        public static string ACK = ToString(06);
        /// <value>(bell)</value>
        public static string BEL = ToString(07);
        /// <value>(backspace)</value>
        public static string BS = ToString(08);
        /// <value>(horizontal tab)</value>
        public static string TAB = ToString(09);
        /// <value>(NL line feed, new line)</value>
        public static string LF = ToString(10);
        /// <value>(vertical tab)</value>
        public static string VT = ToString(11);
        /// <value>(NP form feed, new page)</value>
        public static string FF = ToString(12);
        /// <value>(carriage return)</value>
        public static string CR = ToString(13);
        /// <value>(shift out)</value>
        public static string SO = ToString(14);
        /// <value>(shift in)</value>
        public static string SI = ToString(15);
        /// <value>(data link escape)</value>
        public static string DLE = ToString(16);
        /// <value>(device control 1)</value>
        public static string DC1 = ToString(17);
        /// <value>(device control 2)</value>
        public static string DC2 = ToString(18);
        /// <value>(device control 3)</value>
        public static string DC3 = ToString(19);
        /// <value>(device control 4)</value>
        public static string DC4 = ToString(20);
        /// <value>(negative acknowledge)</value>
        public static string NAK = ToString(21);
        /// <value>(synchronous idle)</value>
        public static string SYN = ToString(22);
        /// <value>(end of trans. block)</value>
        public static string ETB = ToString(23);
        /// <value>(cancel)</value>
        public static string CAN = ToString(24);
        /// <value>(end of medium)</value>
        public static string EM = ToString(25);
        /// <value>(substitute)</value>
        public static string SUB = ToString(26);
        /// <value>(escape)</value>
        public static string ESC = ToString(27);
        /// <value>(file separator)</value>
        public static string FS = ToString(28);
        /// <value>(group separator)</value>
        public static string GS = ToString(29);
        /// <value>(record separator)</value>
        public static string RS = ToString(30);
        /// <value>(unit separator)</value>
        public static string US = ToString(31);
        /// <value>(carriage return) + (NL line feed, new line)</value>
        public static string CRLF = CR + LF;

        /// <summary>
        /// Method to return string value of given ascii decimal
        /// </summary>
        /// <param name="dec">int</param>
        /// <returns>string</returns>
        /// <method>ToString(int dec)</method>
        public static string ToString(int dec)
        {
            string hexValue = dec.ToString("X");
            return (Convert.ToChar(Convert.ToUInt32(hexValue, 16))).ToString();
        }
    }
}
