using FluentAssertions;
using FluentAutomation;
using Machine.Specifications;

namespace Yatodo.Tests
{
    [Subject("Acceptance")]
    public class when_we_run_in_progress_tests : AcceptanceTests
    {
        Because of = () => site.OpenDinnerList();

        It should_show_login_page = () => site.ShowsLoginPage();
    }

    public class AcceptanceTests
    {
        private Establish context = () => site = new WebSite();
        private Cleanup _ = () => site.Dispose();
        protected static WebSite site;
    }

    public class WebSite : FluentTest
    {
        static WebSite()
        {
            SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);
            Settings.MinimizeAllWindowsOnTestStart = false;
        }

        public void OpenDinnerList()
        {
            I.Open("http://localhost:9917/Dinners");
        }

        public void ShowsLoginPage()
        {
            I.Expect.Exists("#login");

            I.Enter("text").In("#name");
            I.Click("#submit");
        }
    }
}