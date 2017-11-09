using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookScraper
{
    class Perfil
    {
        public string id { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public string Universidade { get; set; }
        public string Local { get; set; }
        
        public override string ToString()
        {
            return $"{Nome}                                                   * {id} * {Foto} * {Universidade} * {Local}";
        }

    }
}
