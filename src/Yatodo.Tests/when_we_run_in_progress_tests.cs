using FluentAssertions;
using Machine.Specifications;

namespace Yatodo.Tests
{
    [Subject("Acceptance"), Tags("InProgress")]
    public class when_we_run_in_progress_tests 
    {
        It this_test_should_fail = () => true.Should().BeFalse();
        It this_test_should_pass = () => true.Should().BeTrue();
    }
}