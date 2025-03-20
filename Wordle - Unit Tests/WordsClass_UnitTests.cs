using Worlde___WPF.Models;

namespace Wordle___Unit_Tests
{
    public class WordsClass_UnitTests
    {
        [Fact]
        public void LoadWords_ShouldLoadWordsFromFile()
        {
            //Arrange
            var words = new Words();

            //Act
            List<string> wordList = words.GetAllWords();

            //Assert
            Assert.NotNull(wordList);
            Assert.NotEmpty(wordList);
        }

        [Fact]
        public void IsWordInDictionary_WordExists_ReturnTrue()
        {
            //Arrange
            var words = new Words();
            string testWord = "brain";

            //Act
            bool result = words.IsWordInDictionary(testWord);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsWordInDictionary_WordNotExists_ReturnFalse()
        {
            //Arrange
            var words = new Words();
            string testWord = "aaaaa";

            //Act
            bool result = words.IsWordInDictionary(testWord);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GenerateWord_WordGenerated_ReturnTrue()
        {
            //Arrange
            var words = new Words();

            //Act
            string word = words.GenerateWord();

            //Assert
            Assert.True(!string.IsNullOrEmpty(word));
        }
    }
}