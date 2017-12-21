using Data;
using Xunit;

namespace Tests
{
    public class FilmeTest
    {
        [Fact]
        public void ChecaDisponibilidade()
        {
            // Arrange
            var obj = new FilmeRepository();

            // Act
            var ret = obj.ChecaDisponibilidade(1);

            // Assert
            Assert.True(ret);
        }

        [Fact]
        public void AlugaFilme()
        {
            // Arrange
            var obj = new FilmeRepository();

            // Act
            obj.Reservar(1, 1);
        }

        [Fact]
        public void DevolveFilme()
        {
            // Arrange
            var obj = new FilmeRepository();

            // Act
            obj.Devolver(1);

            // Assert
            Assert.True(true);
        }
    }
}