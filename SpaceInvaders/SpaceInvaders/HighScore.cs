using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SpaceInvaders
{
    /// <summary>
    /// this is to manipulate the high score
    /// </summary>
    class HighScore
    {
        String highScore = "highScore";
        StreamReader file1;
        StreamWriter writer;
        String highScore2;
        int score = 0;
        public HighScore()
        {

        }

        /// <summary>
        /// opens or creats file and returns an intager value from the file 
        /// </summary>
        /// <returns></returns>
        public int readFile()
        {
          try
            {
                using (file1 = new StreamReader(new FileStream(highScore, FileMode.OpenOrCreate , FileAccess.Read)))
                {
                    highScore2 = file1.ReadLine();
                }
             
              Int32.TryParse(highScore2, out score);
            }
          catch (IOException)
          {

          }

          return score;
        }


        /// <summary>
        /// this saves new high score if the player beat old one
        /// </summary>
        /// <param name="score"></param>
        public void writeFile(String score)
        {
            try
            {
                using (writer = new StreamWriter(new FileStream(highScore, FileMode.Open, FileAccess.Write)))
                {
                    {
                        writer.Write(score);
                    }
                }
            }
            catch (IOException)
            {

            }
        }

    }
}
