﻿using Microsoft.EntityFrameworkCore;
using PracticeProject.Data;
using PracticeProject.Interface;
using PracticeProject.Models;
using System.Text.RegularExpressions;

namespace PracticeProject.Repository
{
    public class TextRepository : ITextRepository
    {
        private readonly ApplicationDbContext _dataContext;

        public TextRepository(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public bool Add(TextWork text)
        {
            string[] patternsToReplace = { @"\[ж\]", @"\[/ж\]", @"\[к\]", @"\[/к\]", "\n" };
            string[] replacements = { "<b>", "</b>", "<i>", "</i>", "<br/>" };

            for (int i = 0; i < patternsToReplace.Length; i++)
            {
                text.Task = Regex.Replace(text.Task, patternsToReplace[i], replacements[i]);
            }

            _dataContext.Add(text);
            return Save();
        }

        public bool Delete(TextWork text)
        {
            _dataContext.Remove(text);
            return Save();
        }

        public Lesson GetBycourseIdAndlessonIdAsync(int courseId, int lessonNumber) => _dataContext.Lessons.FirstOrDefault(x => x.Course.Id == courseId && x.OrderNumber == lessonNumber);

        public Lesson GetById(int id) => _dataContext.Lessons.FirstOrDefault(x => x.Id == id);

        public int GetLastTextWorkOrderNumberByLessonId(int lessonid)
        {
            var lesson = _dataContext.Lessons.FirstOrDefault(x => x.Id == lessonid);
            return lesson.TextWork.Count > 0 ? lesson.TextWork.Max(x => x.OrderNumber) + 1 : lesson.TextWork.Count + 1;
        }

        public bool Save() => _dataContext.SaveChanges() > 0 ? true : false;

        public bool Update(TextWork text)
        {
            _dataContext.Update(text);
            return Save();
        }
    }
}
