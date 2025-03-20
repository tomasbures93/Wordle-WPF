using Worlde___WPF.Models;

namespace Wordle___Unit_Tests
{
    public class GameClass_UnitTests
    {
        [Fact]
        public void GameCreated_ReturnTrue()
        {
            //Arrange
            var words = new Words();
            var player = new Player("Test");

            //Act
            var game = new Game(player, words.GenerateWord());

            //Assert
            Assert.NotNull(game);
            Assert.Equal(5, game.Word.Length);
        }

        [Fact]
        public void IsWordCorrect_WordCorrect_ReturnTrue()
        {
            //Arrange
            var game = new Game(new Player("Test"), "apple");

            //Act
            bool result = game.IsWordCorrect("apple");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsWordCorrect_WordNotCorrect_ReturnFalse()
        {
            //Arrange
            var game = new Game(new Player("Test"), "apple");

            //Act
            bool result = game.IsWordCorrect("bbbbb");

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsCharacterCorrectPosition_CorrectPosition_ReturnTrue()
        {
            //Arrange
            var game = new Game(new Player("Test"), "apple");

            //Act
            bool result = game.IsCharacterCorrectPosition("apple", 3);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsCharacterCorrectPosition_WrongPosition_ReturnFalse()
        {
            //Arrange
            var game = new Game(new Player("Test"), "apple");

            //Act
            bool result = game.IsCharacterCorrectPosition("ddddd", 3);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsCharacterInWord_Yes_ReturnTrue()
        {
            //Arrange
            var game = new Game(new Player("Test"), "apple");

            //Act
            bool result = game.IsCharacterInWord("maree", 4);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsCharacterInWord_No_ReturnFalse()
        {
            //Arrange
            var game = new Game(new Player("Test"), "apple");

            //Act
            bool result = game.IsCharacterInWord("rkute", 0);

            //Assert
            Assert.False(result);
        }
    }
}
