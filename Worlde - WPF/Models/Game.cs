using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Worlde___WPF.Models
{
    public class Game
    {
        public string Word { get; set; }
        public Player Player { get; set; }
        public int Rounds { get; set; }
        public int InnerRounds { get; set; }

        public Game(Player player, string word)
        {
            Word = word;
            Player = player;
            Rounds = 0;
            InnerRounds = 0;
        }

        public Player SavePlayerHighscore()
        {
            Player savePlayer = new Player
            {
                Name = Player.Name,
                Datum = Player.Datum,
                Rounds = Player.Rounds
            };
            return savePlayer;
        }
        
        public bool IsWordCorrect(string word)
        {
            if(word == Word)
            {
                return true;
            }
            return false;
        }

        public bool IsCharacterCorrectPosition(string word, int position)
        {
            if (word[position] == Word[position])
            {
                return true;
            }
            return false;
        }

        public bool IsCharacterInWord(string word, int position)
        {
            if (Word.Contains(word[position]))
            {
                return true;
            }
            return false;
        }
    }
}
