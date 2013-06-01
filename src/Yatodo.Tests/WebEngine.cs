using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentAutomation;
using FluentAutomation.Interfaces;
using Machine.Specifications;
using System.Linq;

namespace Yatodo.Tests
{
    public class WebEngine : FluentTest
    {
        static WebEngine()
        {
            SetupFluentAutomation();
        }

        private static void SetupFluentAutomation()
        {
            SeleniumWebDriver.Bootstrap();
            SeleniumWebDriver.SelectedBrowser = SeleniumWebDriver.Browser.Chrome;
            Settings.MinimizeAllWindowsOnTestStart = false;
        }

        public IEnumerable<TodoTask> Tasks
        {
            get
            {
                var todoTasks =
                    I.FindMultiple("#data-table tr")().Select(r => new TodoTask()).ToList();
                return todoTasks;
            }
        }

        public void Open()
        {
            I.Open("http://localhost:9917/");
        }

        public void AddTask(string text)
        {
            I.Click("#add-task");
            I.Enter("text").In("#Text");
            I.Click("#save");
        }

        public void ContainsSingleTaskWithName(string text)
        {
            I.Expect.Text(text).In("#data-table tr:nth-child(1) td:nth-child(1)");
        }

        public TaskAssert TaskWithName(string text)
        {
            return new TaskAssert(text, I);
        }

        public class TaskAssert
        {
            private readonly string text;
            private readonly INativeActionSyntaxProvider I;

            public TaskAssert(string text, INativeActionSyntaxProvider nativeActionSyntaxProvider)
            {
                this.text = text;
                this.I = nativeActionSyntaxProvider;
            }

            public void ShouldBeIncomplete()
            {
                var taskId = GetTaskId();
                var selector = String.Format("#data-table input[type=checkbox][checked][id^={0}]", taskId);
                I.Expect.Count(0).Of(selector);
            }

            private string GetTaskId()
            {
                var textSpan = I.FindMultiple("#data-table span")().FirstOrDefault(s => s.Text == text);
                textSpan.Should().NotBeNull("task with text {0} was not found", text);
                var taskId = textSpan.Attributes.Get("id").Split('_')[0];
                return taskId;
            }

            public void ShouldHaveCompleteButton()
            {
                var taskId = GetTaskId();
                var buttonSelector = "#complete__" + taskId;
                I.Expect.Exists(buttonSelector);
            }

            public void ShouldExist()
            {
                GetTaskId().ShouldNotBeEmpty();
            }
        }
    }

    [Subject("Acceptance"), Tags("InProgress")]
    public class when_user_adds_new_task : WebAcceptanceTest
    {
        Because of = () => site.AddTask("text");

        It should_be_shown_in_the_task_list = () => site.TaskWithName("text").ShouldExist();
        It should_be_incomplete = () => site.TaskWithName("text").ShouldBeIncomplete();
        It complete_button_should_be_shown = () => site.TaskWithName("text").ShouldHaveCompleteButton();
    }

    [Subject("Acceptance")]
    public class WebAcceptanceTest
    {
        protected static WebEngine site;
        Establish context = () =>
            {
                site = new WebEngine();
                site.Open();
            };

        Cleanup cl = () => site.Dispose();
    }


    public class TodoTask
    {
        public string Text { get; set; }
    }
}