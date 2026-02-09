using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MinhaApi.Tests.Models
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Teste_Simples_Deve_Passar()
        {
            // Arrange
            var esperado = 2;

            // Act
            var resultado = 1 + 1;

            // Assert
            Assert.AreEqual(esperado, resultado);
        }

        [DataTestMethod]
        [DataRow(2, 2, 4)]
        [DataRow(3, 3, 6)]
        [DataRow(5, 5, 10)]
        public void Teste_Com_DataRow_Deve_Somar_Corretamente(
            int a,
            int b,
            int resultadoEsperado)
        {
            // Act
            var resultado = a + b;

            // Assert
            Assert.AreEqual(resultadoEsperado, resultado);
        }
    }
}
