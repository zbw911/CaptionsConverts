using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CaptionsConverts
{
    public static class SubtitleHelper
    {
        ///https://github.com/ymoradi/SubtitleHelper
        /// <summary>
        /// Convert WebVTT subtitles to Srt subtitles
        /// </summary>
        /// <param name="webvttContent">WebVTT string</param>
        /// <returns>SRT result</returns>
        public static String ConvertWebvttToSrt(String webvttContent)
        {
            if (webvttContent == null)
                throw new ArgumentNullException("webvttContent");

            String srtResult = webvttContent;

            Int32 srtPartLineNumber = 0;

            srtResult = Regex.Replace(srtResult, @"(WEBVTT\s+)(\d{2}:)", "$2"); // Removes 'WEBVTT' word

            srtResult = Regex.Replace(srtResult, @"(\d{2}:\d{2}:\d{2})\.(\d{3}\s+)-->(\s+\d{2}:\d{2}:\d{2})\.(\d{3}\s*)", match =>
            {
                srtPartLineNumber++;
                return srtPartLineNumber.ToString() + Environment.NewLine +
                    Regex.Replace(match.Value, @"(\d{2}:\d{2}:\d{2})\.(\d{3}\s+)-->(\s+\d{2}:\d{2}:\d{2})\.(\d{3}\s*)", "$1,$2-->$3,$4");
                // Writes '00:00:19.620' instead of '00:00:19,620'
            }); // Writes Srt section numbers for each section

            return srtResult;
        }
    }


}
