using Worlde___WPF.Models;

namespace Wordle___Unit_Tests
{
    public class PlayerClass_UnitTests
    {
        [Fact]
        public void PlayerCreated_ReturnTrue()
        {
            //Arrange
            var player = new Player("Test");

            //Act
            int rounds = player.Rounds;

            //Assert
            Assert.NotNull(player);
            Assert.Equal(0, rounds);
        }
    }
}
