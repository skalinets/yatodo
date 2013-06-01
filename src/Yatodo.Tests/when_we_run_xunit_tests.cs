using FluentAssertions;
using Xunit;

namespace Yatodo.Tests
{
    public class when_we_run_xunit_tests
    {
        [Fact]
        public void this_test_should_pass()
        {
            true.Should().BeTrue();
        }

        [Fact]
        public void this_test_should_fail()
        {
            true.Should().BeFalse();
        }
    }
}