using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MinhaApi.Tests.Models
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Soma_Simples_Deve_Funcionar()
        {
            // Arrange
            var a = 2;
            var b = 3;

            // Act
            var resultado = a + b;

            // Assert
            Assert.AreEqual(5, resultado);
        }

        [DataTestMethod]
        [DataRow(2, 2, 4)]
        [DataRow(3, 5, 8)]
        [DataRow(10, 20, 30)]
        public void Soma_Com_DataRow_Deve_Funcionar(int a, int b, int esperado)
        {
            var resultado = a + b;
            Assert.AreEqual(esperado, resultado);
        }
    }
}
