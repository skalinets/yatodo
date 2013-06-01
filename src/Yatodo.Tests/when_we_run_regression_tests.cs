using FluentAssertions;
using Machine.Specifications;

namespace Yatodo.Tests
{
    [Subject("Acceptance")]
    public class when_we_run_regression_tests 
    {
        It this_test_should_fail = () => true.Should().BeFalse();
        It this_test_should_pass = () => true.Should().BeTrue();
    }
}