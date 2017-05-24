using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafy.Logika
{
    public class Wierzcholek
    {
        #region Wlasciwosci
        /// <summary>
        /// Identyfikator wierzchołka.
        /// </summary>
        public int Numer { get;}
        /// <summary>
        /// Lista obiektów sąsiadujących wierzchołków.
        /// </summary>
        public List<Wierzcholek> Sasiedzi { get; set; }
        /// <summary>
        /// Lista numerów sąsiadujących wierzchołków,
        /// używana do tworzenia powiązań między obiektami w grafie.
        /// </summary>
        public List<int> NumerySasiadow { get; set; }
        /// <summary>
        /// Identyfikator wykorzystywany przy sprawdzaniu dwudzielności grafu.
        /// </summary>
        public bool? IdentyfikatorGrupy { get; set; }
        #endregion

        #region Konstruktory
        /// <summary>
        /// Tworzy nowy obiekt klasy Wierzchołek
        /// </summary>
        /// <param name="numer">Identyfikator Wierzchołka</param>
        /// <param name="sasiedzi">numery sąsiadujących wierzchołków</param>
        public Wierzcholek(int numer, params int[] sasiedzi)
        {
            this.Numer = numer;
            this.NumerySasiadow = sasiedzi.ToList();
            this.IdentyfikatorGrupy = null;
        }
        /// <summary>
        /// Tworzy nowy obiekt klasy Wierzchołek
        /// </summary>
        /// <param name="numer">Identyfikator Wierzchołka</param>
        /// <param name="sasiedzi">numery sąsiadujących wierzchołków</param>
        public Wierzcholek(int numer, List<int> sasiedzi) 
            : this(numer, sasiedzi.ToArray()) { }
        #endregion
    }
}
