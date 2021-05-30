using NUnit.Framework;
using System;
using System.IO;

namespace FileLogger.Tests
{
    public class Tests
    {
        FileLogger fileLogger;

        [SetUp]
        public void Setup()
        {
            fileLogger = new FileLogger();
        }

        [Test]
        public void FileCreatedOnWeekDay()
        {
            DateTime weekday = new DateTime(2021, 05, 31);
            fileLogger.Log("File is created", weekday);

            Assert.That(File.Exists($"log{weekday.ToString(FileLogger.DateSuffixFormat)}.txt"));
        }

        [Test]
        public void FileCreationOnWeekend()
        {
            fileLogger.Log("Weekend File renamed to format weekend-YYYYMMDD.txt", new DateTime(2021, 5, 23));
            fileLogger.Log("Weekend File Created", new DateTime(2021, 5, 30));

            Assert.That(File.Exists("weekend-20210529.txt"));
            Assert.That(File.Exists("weekend.txt"));
        }
    }
}  