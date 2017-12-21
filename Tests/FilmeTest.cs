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
    }
}