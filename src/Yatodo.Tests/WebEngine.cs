using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentAutomation;
using Machine.Specifications;

namespace Yatodo.Tests
{
    public class WebEngine : FluentTest
    {
        static WebEngine()
        {
            SeleniumWebDriver.Bootstrap();
            SeleniumWebDriver.SelectedBrowser = SeleniumWebDriver.Browser.Chrome;
            Settings.MinimizeAllWindowsOnTestStart = false;
        }

        public void test_fluent_automation()
        {
            I.Open("http://google.com");
        }
    }

    [Subject("Integration")]
    public class when_user_opens_page
    {
        static WebSite site = new WebSite();
        
        Because of = () => site.Open();

        It should_see_empty_task_list = () => site.Tasks.Should().BeEmpty();
    }

    internal class WebSite
    {
        private WebEngine webEngine;

        public IEnumerable<TodoTask> Tasks
        {
            get { return new List<TodoTask>(); }
        }

        public WebSite()
        {
            this.webEngine = new WebEngine();

        }

        public void Open()
        {
            
        }
    }

    internal class TodoTask
    {
    }
}