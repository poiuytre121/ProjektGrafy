using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grafy.Logika.Tests
{
    [TestClass]
    public class GrafTests
    {
        /// <summary>
        /// Testuje podstawowe dodawanie wierzchołka do grafu
        /// </summary>
        [TestMethod]
        public void TestDodajWierzcholek()
        {
            //Arrange
            Graf graf = new Graf();
            Wierzcholek w1 = new Wierzcholek(1, 2, 5, 3, 4);
            //Act
            graf.DodajWierzcholek(w1);
            //Assert
            Assert.AreEqual(w1, graf.Wierzcholki[0]);
        }
        /// <summary>
        /// Testuje wyrzucenie wyjątku w razie dodania wierzchołka o takim samym numerze
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestDodajWierzcholekEx()
        {
            //Arrange
            Graf graf = new Graf();
            Wierzcholek w1 = new Wierzcholek(1, 2, 5, 3, 4);
            Wierzcholek w2 = new Wierzcholek(1, 3, 4);
            //Act
            graf.DodajWierzcholek(w1);
            graf.DodajWierzcholek(w2);
        }
        /// <summary>
        /// Testuje wiązanie wierzchołków w grafie
        /// </summary>
        [TestMethod]
        public void TestPowiazWierzcholkiGrafu()
        {
            //Arrange
            Graf graf = new Graf();
            Wierzcholek w1 = new Wierzcholek(1, 2, 3, 4);
            Wierzcholek w2 = new Wierzcholek(2, 1, 3);
            Wierzcholek w3 = new Wierzcholek(3, 1, 2);
            Wierzcholek w4 = new Wierzcholek(4, 1);
            //Act
            graf.PowiazWierzcholkiGrafu();
            //Assert
            bool isCorrect = true;
            int[] counts = { 3, 2, 2, 1 };
            for (int i = 0; i < graf.Wierzcholki.Count; i++)
            {
                if (graf.Wierzcholki[i].Sasiedzi.Count != counts[i]) isCorrect = false; 
            }
            Assert.IsTrue(isCorrect);
        }
        /// <summary>
        /// Testuje usuwanie wierzchołka z listy wierzchołków grafu
        /// </summary>
        [TestMethod]
        public void TestUsunWierzcholek()
        {
            //Arrange
            Graf graf = new Graf();
            Wierzcholek w1 = new Wierzcholek(1, 2, 3, 4);
            Wierzcholek w2 = new Wierzcholek(2, 1, 3);
            Wierzcholek w3 = new Wierzcholek(3, 1, 2);
            Wierzcholek w4 = new Wierzcholek(4, 1);
            //Act
            graf.DodajWierzcholek(w1);
            graf.DodajWierzcholek(w2);
            graf.DodajWierzcholek(w3);
            graf.DodajWierzcholek(w4);
            graf.PowiazWierzcholkiGrafu();
            graf.UsunWierzcholek(1);
            graf.UsunWierzcholek(w2);
            //Assert
            Assert.AreEqual(2, graf.Wierzcholki.Count);
            Assert.IsTrue(!w3.Sasiedzi.Contains(w2));
        }
    }
}
