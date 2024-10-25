using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuizGame
{
    
    abstract class Question
    {
        public string Text { get; set; }
        public abstract void Display();
        public abstract bool CorrectAnswer();
    }

    
    class MultipleChoiceQuestion : Question
    {
        public string[] Choices { get; set; }
        public int CorrectAnswerIndex { get; set; }

        public MultipleChoiceQuestion(string text, string[] choices, int correctAnswerIndex)
        {
            Text = text;
            Choices = choices;
            CorrectAnswerIndex = correctAnswerIndex;
        }

        public override void Display()
        {
            Console.WriteLine($"Question: {Text}");
            for (int i = 0; i < Choices.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Choices[i]}");
            }
        }

        public override bool CorrectAnswer()
        {
            try
            {
                Console.Write("Your answer (1-4): ");
                int answer = Convert.ToInt32(Console.ReadLine()) - 1;


                if (answer >= 0 && answer < 4)
                {
                    if (answer == CorrectAnswerIndex)
                    {
                        Console.WriteLine("Your answer is correct!");
                        return true;
                    }
                    else if (answer != CorrectAnswerIndex)
                    {
                        Console.WriteLine("Your answer is wrong!");
      
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Try again.");
                    
                }
            }
            catch
            {
                Console.WriteLine("Some thing is wrong");
                
            }

            return false;

        }
    }

    
    class OpenEndedQuestion : Question
    {
        public string[] CorrectAnswers { get; set; }

        public OpenEndedQuestion(string text, string[] correctAnswers)
        {
            Text = text;
            CorrectAnswers = correctAnswers;
        }

        public override void Display()
        {
            Console.WriteLine($"Question: {Text}");
        }
        public override bool CorrectAnswer()
        {
            try
            {
                Console.Write("Your answer: ");
                string answer = Console.ReadLine();

                for (int i = 0; i < CorrectAnswers.Length; i++)
                {
                    if (answer == CorrectAnswers[i])
                    {
                        Console.WriteLine("Your answer is correct!");
                        return true;
                    }
                }

                Console.WriteLine("Your answer is wrong!");
            }
            catch
            {
                Console.WriteLine("Some thing is wrong");
            }

            return false;
        }
    }

  
    class TrueFalseQuestion : Question
    {
        
        public bool IsTrue { get; set; }

        public TrueFalseQuestion(string text, bool isTrue)
        {
            Text = text;
            IsTrue = isTrue;
        }

        public override void Display()
        {
            Console.WriteLine($"True or False: {Text}");
        }
        public override bool CorrectAnswer()
        {
            try
            {
                Console.Write("Your answer: ");
                bool answer = bool.Parse(Console.ReadLine());

                if (answer == IsTrue)
                {
                    Console.WriteLine("Your answer is correct!");
                    return true;
                }
                else
                {
                    Console.WriteLine("Your answer is wrong!");
                }
            }
            catch
            {
                Console.WriteLine("Some thing is wrong");
            }
            return false;
        }
    }

    class QuizGame
    {
        static List<Question> questions = new List<Question>();
        static int correctNumbers = 0;

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nQuiz Game");
                Console.WriteLine("1. Quiz Game - Create Mode");
                Console.WriteLine("2. Quiz Game - Play Mode");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");
                string choiceMode = Console.ReadLine();

                switch (choiceMode)
                {
                    case "1":
                        EnterCreateMode();
                        break;
                    case "2":
                        EnterPlayMode();
                        break;
                    case "3":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }

            }
        }

        static void EnterCreateMode()
        {
            bool CreateMode = true;

            while (CreateMode)
            {
                Console.WriteLine("\nQuiz Game - Create Mode");
                Console.WriteLine("1. Add Multiple Choice Question");
                Console.WriteLine("2. Add Open-Ended Question");
                Console.WriteLine("3. Add True/False Question");
                Console.WriteLine("4. Edit a Question");
                Console.WriteLine("5. Delete a Question");
                Console.WriteLine("6. View All Questions");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddMultipleChoiceQuestion();
                        break;
                    case "2":
                         AddOpenEndedQuestion();
                         break;
                    case "3":
                         AddTrueFalseQuestion();
                         break;
                    case "4":
                         EditQuestion();
                         break;
                    case "5":
                         DeleteQuestion();
                         break;
                    case "6":
                         ViewAllQuestions();
                         break;
                    case "7":
                         CreateMode = false;
                         break;
                    default:
                         Console.WriteLine("Invalid choice. Try again.");
                         break;
                }
                
            }
        }

        static void EnterPlayMode()
        {
            playQuizz();

        }
        
        static void playQuizz()
        {
            try
            {
                if (questions.Count == 0)
                {
                    Console.WriteLine("No questions available.");
                    return;
                }

                int correctNumber = 0;
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                for (int i = 0; i < questions.Count; i++)
                {
                    Console.WriteLine($"\nQuestion {i + 1}:");
                    questions[i].Display();

                    if (questions[i].CorrectAnswer())
                    {
                        correctNumber++;
                    }
                }

                stopwatch.Stop();

                Console.WriteLine($"\nNumber of correct answers: {correctNumber}/{questions.Count}");
                Console.WriteLine($"Time spent: {stopwatch.Elapsed.TotalMinutes:F2} minutes.");

                Console.Write("Do you want to see the correct answers? (y/n): ");
                string AllCorrectAnswer = Console.ReadLine();

                if (AllCorrectAnswer == "y")
                {
                    for (int i = 0; i < questions.Count; i++)
                    {
                        var question = questions[i];
                        Console.WriteLine($"Question {i + 1}:");

                        if (question is MultipleChoiceQuestion mcq)
                        {
                            Console.WriteLine($"{mcq.Text} - Correct Answer: {mcq.CorrectAnswerIndex}");
                        }
                        else if (question is OpenEndedQuestion oeq)
                        {
                            Console.WriteLine($"{oeq.Text} - Acceptable Answers: {string.Join(", ", oeq.CorrectAnswers)}");
                        }
                        else if (question is TrueFalseQuestion tfq)
                        {
                            Console.WriteLine($"{tfq.Text} - Correct Answer: {tfq.IsTrue}");
                        }

                        Console.WriteLine(); // Adds a blank line for readability
                    }
                }
            }
            catch
            {
                Console.WriteLine("Some thing is wrong");
            }

        }
        // Add a multiple-choice question
        static void AddMultipleChoiceQuestion()
        {
            try
            {
                Console.Write("Enter the question: ");
                string questionText = Console.ReadLine();

                string[] choices = new string[4];
                for (int i = 0; i < 4; i++)
                {
                    Console.Write($"Enter choice {i + 1}: ");
                    choices[i] = Console.ReadLine();
                }

                Console.Write("Enter the correct choice (1-4): ");
                int correctIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                if (correctIndex >= 0 && correctIndex < 4)
                {
                    questions.Add(new MultipleChoiceQuestion(questionText, choices, correctIndex));
                    Console.WriteLine("Multiple choice question added.");
                }
                else
                {
                    Console.WriteLine("Invalid. Please try again");
                }
            }
            catch
            {
                Console.WriteLine("Some thing is wrong");
            }
        }

        // Add an open-ended question
        static void AddOpenEndedQuestion()
        {
            try
            {
                Console.Write("Enter the question: ");
                string questionText = Console.ReadLine();

                Console.Write("Enter possible correct answers (comma-separated): ");
                string[] correctAnswers = Console.ReadLine().Split(',');

                questions.Add(new OpenEndedQuestion(questionText, correctAnswers));
                Console.WriteLine("Open-ended question added.");
            }
            catch
            {
                Console.WriteLine("Some thing is wrong");
            }
        }

        // Add a true/false question
        static void AddTrueFalseQuestion()
        {
            try
            {
                Console.Write("Enter the statement: ");
                string statement = Console.ReadLine();

                Console.Write("Is it true or false? (true/false): ");
                bool isTrue = bool.Parse(Console.ReadLine());

                questions.Add(new TrueFalseQuestion(statement, isTrue));
                Console.WriteLine("True/False question added.");
            }
            catch
            {
                Console.WriteLine("Some thing is wrong");
            }
        }

        // Edit an existing question
        static void EditQuestion()
        {
            try
            {
                if (questions.Count == 0)
                {
                    Console.WriteLine("No questions available to edit.");
                    return;
                }

                ViewAllQuestions();
                Console.Write("Enter the question number to edit: ");
                int index = int.Parse(Console.ReadLine()) - 1;

                if (index >= 0 && index < questions.Count)
                {
                    Console.Write("Enter the new question text: ");
                    string newText = Console.ReadLine();
                    questions[index].Text = newText;
                    Console.WriteLine("Question updated.");
                }
                else
                {
                    Console.WriteLine("Invalid question number.");
                }
            }
            catch
            {
                Console.WriteLine("Some thing is wrong");
            }

        }

        // Delete a question
        static void DeleteQuestion()
        {
            try
            {
                if (questions.Count == 0)
                {
                    Console.WriteLine("No questions available to delete.");
                    return;
                }

                ViewAllQuestions();
                Console.Write("Enter the question number to delete: ");
                int index = int.Parse(Console.ReadLine()) - 1;

                if (index >= 0 && index < questions.Count)
                {
                    questions.RemoveAt(index);
                    Console.WriteLine("Question deleted.");
                }
                else
                {
                    Console.WriteLine("Invalid question number.");
                }
            }
            catch
            {
                Console.WriteLine("Some thing is wrong");
            }
        }

        // View all questions
        static void ViewAllQuestions()
        {
            if (questions.Count == 0)
            {
                Console.WriteLine("No questions available.");
                return;
            }

            for (int i = 0; i < questions.Count; i++)
            {
                Console.WriteLine($"\nQuestion {i + 1}:");
                questions[i].Display();
            }
        }
    }
}
