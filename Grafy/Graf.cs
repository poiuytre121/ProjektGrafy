using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafy.Logika
{
    public class Graf
    {
        #region Wlasciwosci
        public List<Wierzcholek> Wierzcholki { get; private set; }
        #endregion
        #region Konstruktory
        public Graf()
        {
            Wierzcholki = new List<Wierzcholek>();
        }
        #endregion
        #region Metody
        /// <summary>
        /// Metoda dodaje nowy wierzchołek do grafu
        /// </summary>
        /// <param name="nowy">Wierzchołek dodawany do grafu</param>
        /// <exception cref="ApplicationException">Dublujący się numer wierzchołka</exception>
        public void DodajWierzcholek(Wierzcholek nowy)
        {
            //Graf nie może posiadać dwóch wierzchołków o takim samym numerze
            if (Wierzcholki.Select(w => w.Numer).Contains(nowy.Numer))
            {
                throw new ApplicationException("Ten graf posiada już wierzchołek o tym numerze");
            }
            else
            {
                Wierzcholki.Add(nowy);
            }
        }
        /// <summary>
        /// Usuwa zadany wierzchołek z grafu
        /// </summary>
        /// <param name="usuwany">Wierzchołek do usunięcia</param>
        /// <exception cref="InvalidOperationException">Brak takiego wierzchołka w grafie</exception>
        public void UsunWierzcholek(Wierzcholek usuwany)
        {
            if(!Wierzcholki.Remove(usuwany)) throw new InvalidOperationException("Nie ma takiego wierzchołka w grafie");
            //TODO: usuwanie referencji do usuniętego wierzchołka
        }
        /// <summary>
        /// Usuwa wierzchołek o zadanym numerze z grafu
        /// </summary>
        /// <param name="usuwany">Numer wierzchołka do usunięcia</param>
        /// <exception cref="InvalidOperationException">Brak wierzchołka o zadanym numerze</exception>
        public void UsunWierzcholek(int usuwany)
        {
            UsunWierzcholek(Wierzcholki.Where(w => w.Numer == usuwany).First());
        }
        /// <summary>
        /// Wiąże ze sobą wierzchołki grafu o zadeklarowanych numerach
        /// </summary>
        /// <exception cref="ApplicationException">Brak wierzchołka zadeklarowanego jako sąsiad</exception>
        public void PowiazWierzcholkiGrafu()
        {
            foreach (Wierzcholek w in Wierzcholki)
            {
                foreach (int numer in w.NumerySasiadow)
                {
                    try
                    {
                        w.Sasiedzi.Add(Wierzcholki.Where(wierz => wierz.Numer == numer).First());
                    }
                    catch(InvalidOperationException ex)
                    {
                        throw new ApplicationException(String.Format("Wierzchołek {0} zadeklarowany jako sąsiad wierzchołka {1} nie istnieje",numer,w.Numer));
                    }
                }
            }
        }
        /// <summary>
        /// Pobiera graf z pliku tekstowego o podanej ścieżce
        /// </summary>
        /// <param name="sciezka">Ścieżka do pliku z grafem</param>
        public void WczytajZPliku(string sciezka)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Sprawdza, czy ten obiekt grafu reprezentuje graf pełny dwudzielny
        /// </summary>
        /// <returns>Zwraca prawdę, jeżeli graf jest pełny dwudzielny</returns>
        public bool CzyPelnyDwudzielny()
        {
            return CzyDwudzielnySpojny() && CzyPelnyGdyDwudzielnySpojny();
        }
        /// <summary>
        /// Sprawdza, czy ten obiekt reprezentuje graf dwudzielny spójny.
        /// </summary>
        /// <returns>Zwraca prawdę, gdy graf jest dwudzielny</returns>
        private bool CzyDwudzielnySpojny()
        {
            Wierzcholek biezacy = this.Wierzcholki[0];
            biezacy.IdentyfikatorGrupy = true;
            //przeszukiwanie DFS z jednego wierzchołka startowego
            Stack<Wierzcholek> stos = new Stack<Wierzcholek>();
            stos.Push(biezacy);
            while(stos.Count != 0)
            {
                biezacy = stos.Pop();
                foreach (Wierzcholek sasiad in biezacy.Sasiedzi)
                {
                    //jeżeli sąsiad był 
                    if (sasiad.IdentyfikatorGrupy.HasValue &&
                        (sasiad.IdentyfikatorGrupy.Value && biezacy.IdentyfikatorGrupy.Value)) return false;
                    else if (!sasiad.IdentyfikatorGrupy.HasValue)
                    {
                        sasiad.IdentyfikatorGrupy = !biezacy.IdentyfikatorGrupy;
                        stos.Push(sasiad);
                    }
                }
            }
            //jeżeli graf w tym momencie posiada wierzchołki które nie mają przypisanej grupy to graf nie jest spójny
            if (this.Wierzcholki.Where(w => !w.IdentyfikatorGrupy.HasValue).Count() != 0) return false;
            else return true;
        }
        /// <summary>
        /// Sprawdza, czy ten obiekt reprezentuje graf pełny dwudzielny,
        /// jeżeli jest pewne, że jest on grafem dwudzielnym spójnym.
        /// </summary>
        /// <returns>Zwraca prawdę, gdy reprezentowany graf jest grafem pełnym dwudzielnym</returns>
        private bool CzyPelnyGdyDwudzielnySpojny()
        {
            List<Wierzcholek> grupa1 = this.Wierzcholki.Where(w => w.IdentyfikatorGrupy.Value).ToList();
            List<Wierzcholek> grupa2 = this.Wierzcholki.Where(w => !w.IdentyfikatorGrupy.Value).ToList();
            //TODO: sprawdzić, czy nie da się tego zrobić szybciej
            foreach (Wierzcholek wierzcholek1 in grupa1)
            {
                foreach (Wierzcholek wierzcholek2 in grupa2)
                {
                    if (!wierzcholek1.Sasiedzi.Contains(wierzcholek2)  || !wierzcholek2.Sasiedzi.Contains(wierzcholek1)) return false;
                }
            }
            return true;
        }
        #endregion
    }
}
