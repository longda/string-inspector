using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringInspector.Core.Models
{
    public class ArticleInfo
    {
        public Article Article { get; set; }
        public char MostCommonCharacter { get; set; }
        public int MostCommonCharacterCount { get; set; }
    }
}
