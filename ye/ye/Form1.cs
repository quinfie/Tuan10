using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ye
{
    public partial class Form1 : Form
    {
        private List<Question> questions;
        private int currentQuestionIndex;
        public Form1()
        {
            InitializeComponent();

            questions = new List<Question>
            {
                new Question("Câu hỏi 1", new List<string> { "Đáp án A", "Đáp án B", "Đáp án C", "Đáp án D" }, 0),
                new Question("Câu hỏi 2", new List<string> { "Đáp án A", "Đáp án B", "Đáp án C", "Đáp án D" }, 1),
                new Question("Câu hỏi 3", new List<string> { "Đáp án A", "Đáp án B", "Đáp án C", "Đáp án D" }, 2),
                new Question("Câu hỏi 4", new List<string> { "Đáp án A", "Đáp án B", "Đáp án C", "Đáp án D" }, 3)          
            };

            currentQuestionIndex = 0;
            DisplayQuestion();
        }
        private void DisplayQuestion()
        {
            if (currentQuestionIndex >= 0 && currentQuestionIndex < questions.Count)
            {
                Question currentQuestion = questions[currentQuestionIndex];
                label1.Text = currentQuestion.Content;

                radioBTN_A.Text = currentQuestion.Answers[0];
                radioBTN_B.Text = currentQuestion.Answers[1];
                radioBTN_C.Text = currentQuestion.Answers[2];
                radioBTN_D.Text = currentQuestion.Answers[3];
            }
        }

         private void nextButton_Click_1(object sender, EventArgs e)
         {
             int selectedAnswer = -1;
             if (radioBTN_A.Checked)
                 selectedAnswer = 0;
             else if (radioBTN_B.Checked)
                 selectedAnswer = 1;
             else if (radioBTN_C.Checked)
                 selectedAnswer = 2;
             else if (radioBTN_D.Checked)
                 selectedAnswer = 3;

             if (selectedAnswer == questions[currentQuestionIndex].CorrectAnswer)
             {
                 MessageBox.Show("Câu trả lời đúng!");
             }
             else
             {
                 MessageBox.Show("Câu trả lời sai!");
             }

             // Chuyển đến câu hỏi tiếp theo
             currentQuestionIndex++;
             if (currentQuestionIndex < questions.Count)
             {
                 DisplayQuestion();
             }
             else
             {
                 MessageBox.Show("Bạn đã hoàn thành trắc nghiệm!");
             }
         }

         private void Form1_Load(object sender, EventArgs e)
         {

         }

    }
    public class Question
    {
        public string Content { get; set; }
        public List<string> Answers { get; set; }
        public int CorrectAnswer { get; set; }

        public Question(string content, List<string> answers, int correctAnswer)
        {
            Content = content;
            Answers = answers;
            CorrectAnswer = correctAnswer;
        }
    }
}
