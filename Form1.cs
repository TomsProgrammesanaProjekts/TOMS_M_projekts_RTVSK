using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace TOMS_M_projekts_RTVSK
{
    public partial class Form1 : Form
    {

        private OpenFileDialog openFileDialog1;
        private List<string> lv = new List<string>();
        private List<string> en = new List<string>();

        private int correctAnswer = 0;
        private int correctAnswerCount = 0;
        private int totalAnswerCount = 0;
        private int correctTranslationIndex = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSTART_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Spēle ir uzsākta!", "DEBUG", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
       
            openFileDialog1 = new OpenFileDialog();                
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamReader read = new StreamReader(openFileDialog1.FileName))
                    {
                        string line;
                        while ((line = read.ReadLine()) != null)
                        {
                            //MessageBox.Show(line, "DEBUG", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            string[] words = line.Split('=');
                            lv.Add(words[0]);
                            en.Add(words[1]);                                
                        }
                        
                    }
                }                    
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
                //MessageBox.Show(lv[2], "DEBUG", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                buttonSTART.Enabled = false;
                labelCommand.Text = "Lūdzu, iztulkojiet:";
                correctAnswerCount = 0;
                totalAnswerCount = 0;
                updateCounter();
                buttonVAR1.Enabled = true;
                buttonVAR2.Enabled = true;
                buttonVAR3.Enabled = true;
                buttonVAR4.Enabled = true;
                nextTask();

            }
            

        }

        private void updateCounter()
        {
            labelCounter.Text = "Pareizi: " + correctAnswerCount + " no " + totalAnswerCount;
        }

        private void nextTask()
        {

            totalAnswerCount++;

            Random rnd = new Random();
            int newTask = rnd.Next(0, lv.Count);
            correctTranslationIndex = newTask;
            labelTask.Text = lv[newTask];
            correctAnswer = rnd.Next(1, 5);
            //MessageBox.Show(lv[lv.Count-1], "DEBUG", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            int wrongAnswer1, wrongAnswer2, wrongAnswer3;  
            wrongAnswer1 = newTask;
            while (wrongAnswer1 == newTask)
            {
                wrongAnswer1 = rnd.Next(0, en.Count);
            }
            wrongAnswer2 = newTask;
            while ((wrongAnswer2 == newTask) || (wrongAnswer2 == wrongAnswer1))
            {
                wrongAnswer2 = rnd.Next(0, en.Count);
            }
            wrongAnswer3 = newTask;
            while ((wrongAnswer3 == newTask) || (wrongAnswer3 == wrongAnswer1) || (wrongAnswer3 == wrongAnswer2))
            {
                wrongAnswer3 = rnd.Next(0, en.Count);
            }

            switch (correctAnswer)
            {
                case 1:
                    buttonVAR1.Text = en[newTask];

                    buttonVAR2.Text = en[wrongAnswer1];
                    buttonVAR3.Text = en[wrongAnswer2];
                    buttonVAR4.Text = en[wrongAnswer3];

                    break;

                case 2:
                    buttonVAR2.Text = en[newTask];

                    buttonVAR1.Text = en[wrongAnswer1];
                    buttonVAR3.Text = en[wrongAnswer2];
                    buttonVAR4.Text = en[wrongAnswer3];

                    break;

                case 3:
                    buttonVAR3.Text = en[newTask];

                    buttonVAR1.Text = en[wrongAnswer1];
                    buttonVAR2.Text = en[wrongAnswer2];
                    buttonVAR4.Text = en[wrongAnswer3];

                    break;

                case 4:
                    buttonVAR4.Text = en[newTask];

                    buttonVAR1.Text = en[wrongAnswer1];
                    buttonVAR2.Text = en[wrongAnswer2];
                    buttonVAR3.Text = en[wrongAnswer3];

                    break;

                default:                    
                    break;
            }

        }

        public void showCorrectAnswer()
        {
            string answer = "Pareiza atbilde bija: " + en[correctTranslationIndex];
            MessageBox.Show(answer, "NEPAREIZI!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void buttonVAR1_Click(object sender, EventArgs e)
        {

            if (correctAnswer == 1)
            {
                correctAnswerCount++;
            } else
            {
                showCorrectAnswer();
            }
            updateCounter();
            nextTask();
        }

        private void buttonVAR2_Click(object sender, EventArgs e)
        {
            if (correctAnswer == 2)
            {
                correctAnswerCount++;
            }
            else
            {
                showCorrectAnswer();
            }
            updateCounter();
            nextTask();
        }

        private void buttonVAR3_Click(object sender, EventArgs e)
        {
            if (correctAnswer == 3)
            {
                correctAnswerCount++;
            }
            else
            {
                showCorrectAnswer();
            }
            updateCounter();
            nextTask();
        }

        private void buttonVAR4_Click(object sender, EventArgs e)
        {
            if (correctAnswer == 4)
            {
                correctAnswerCount++;
            }
            else
            {
                showCorrectAnswer();
            }
            updateCounter();
            nextTask();
        }
    }
    }

