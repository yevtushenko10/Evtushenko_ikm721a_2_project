﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Evtushenko_ikm721a_2_project
{
    class MajorWork
    {
        // Вміст робочого об'єкта
        // Поля
        private System.DateTime TimeBegin;
        private string Data; //вхідні дані
        private string Result; // Поле результату

        public bool Modify;
        private int Key;

        private string SaveFileName;// ім’я файлу для запису
        private string OpenFileName;// ім’я файлу для читання
        public void WriteSaveFileName(string S)// метод запису даних в об'єкт
        {
            this.SaveFileName = S;// запам'ятати ім’я файлу для запису
        }
        public void WriteOpenFileName(string S)
        {
            this.OpenFileName = S;// запам'ятати ім’я файлу для відкриття
        }
     
        public void Write(string D)// метод запису даних в об'єкт.
        {
            this.Data = D;
        }
        public string Read()
        {
            return this.Result;// метод відображення результату
        }
        public void Task() // метод реалізації програмного завдання
        {
            if (this.Data.Length > 5)
            {
                this.Result = Convert.ToString(true);
            }
            else
            {
                this.Result = Convert.ToString(false);
            }
            this.Modify = true;
        }
        public void SetTime() // метод запису часу початку роботи програми
        {
            this.TimeBegin = System.DateTime.Now;
        }
        public System.DateTime GetTime() // Метод отримання часу завершення програми
        {
            return this.TimeBegin;
        }
        public void SaveToFile() // Запис даних до файлу
        {
            if (!this.Modify)
                return;
            try
            {
                Stream S; // створення потоку
                if (File.Exists(this.SaveFileName))// існує файл?
                    S = File.Open(this.SaveFileName, FileMode.Append);// Відкриття файлу для збереження
                else
                    S = File.Open(this.SaveFileName, FileMode.Create);// створити файл
                Buffer D = new Buffer(); // створення буферної змінної
                D.Data = this.Data;
                D.Result = Convert.ToString(this.Result);
                D.Key = Key;
                Key++;

                BinaryFormatter BF = new BinaryFormatter(); // створення об'єкта для
                BF.Serialize(S, D);
                S.Flush(); // очищення буфера потоку
                S.Close(); // закриття потоку
                this.Modify = false; // Заборона повторного запису
            }
            catch
            {
                MessageBox.Show("Помилка роботи з файлом");
            }
        }
        public void ReadFromFile(System.Windows.Forms.DataGridView DG) // зчитування з файлу
        {
            try
            {
                if (!File.Exists(this.OpenFileName))
                {
                    MessageBox.Show("Файлу немає"); // Виведення на екран повідомлення "файлу немає"
                    return;
                }
                Stream S; // створення потоку
                S = File.Open(this.OpenFileName, FileMode.Open); // зчитування даних з файлу                                                                                                
                Buffer D;
                object O; // буферна змінна для контролю формату
                BinaryFormatter BF = new BinaryFormatter(); // створення об'єкту для форматування
                while (S.Position < S.Length)
                {
                    O = BF.Deserialize(S); // десеріалізація
                    D = O as Buffer;
                    if (D == null) break;
                    // Виведення даних на екран
                }
                S.Close(); // закриття
            }
            catch
            {
                MessageBox.Show("Помилка файлу"); // Виведення на екран повідомлення "Помилка файлу"
            }
        } // ReadFromFile закінчився
        public void Generator() // метод формування ключового поля
        {
            try
            {
                if (!File.Exists(this.SaveFileName)) // існує файл?
                {
                    Key = 1;
                    return;
                }
                Stream S; // створення потоку
                S = File.Open(this.SaveFileName, FileMode.Open); // Відкриття файлу 
                Buffer D;
                object O; // буферна змінна для контролю формату
                BinaryFormatter BF = new BinaryFormatter(); // створення елементу для форматування
                while (S.Position < S.Length)
                {
                    O = BF.Deserialize(S);
                    D = O as Buffer;
                    if (D == null) break;
                    Key = D.Key;
                }
                Key++;
                S.Close();
            }
            catch
            {
                MessageBox.Show("Помилка файлу"); // Виведення на екран повідомлення "Помилка файлу"
            }
        }
        public bool SaveFileNameExists()
        {
            if (this.SaveFileName == null)
                return false;
            else return true;
        }
        public void NewRec() // новий запис
        {
            this.Data = ""; // "" - ознака порожнього рядка
            this.Result = null; // для string- null
        }


    }
}
