using DomainTester.Services;
using FluentAssertions;

namespace DomainTester.Tests
{
    public class UrlTests
    {
        [Fact]
        public void When_UsingADomainWithoutAPage_Expect_FourCombinationsToTest()
        {
            var url = new Url();

            var result = url.Execute(new Uri("http://www.ajwebsolutions.com")).ToArray();

            result.Should().HaveCount(4);
            result[0].Uri.Should().Be("http://ajwebsolutions.com/");
            result[1].Uri.Should().Be("http://www.ajwebsolutions.com/");
            result[2].Uri.Should().Be("https://ajwebsolutions.com/");
            result[3].Uri.Should().Be("https://www.ajwebsolutions.com/");
        }

        [Fact]
        public void When_UsingADomainWithoutAPageOnPort81_Expect_FourCombinationsToTest()
        {
            var url = new Url();

            Assert.Throws<ApplicationException>(() => url.Execute(new Uri("http://www.ajwebsolutions.com:81")).ToArray());
        }

        [Fact]
        public void When_UsingADomainWithAPage_Expect_EightCombinationsToTest()
        {
            var url = new Url();

            var result = url.Execute(new Uri("http://www.ajwebsolutions.com/contactme")).ToArray();

            result.Should().HaveCount(8);
            result[0].Uri.Should().Be("http://ajwebsolutions.com/");
            result[1].Uri.Should().Be("http://www.ajwebsolutions.com/");
            result[2].Uri.Should().Be("https://ajwebsolutions.com/");
            result[3].Uri.Should().Be("https://www.ajwebsolutions.com/");
            result[4].Uri.Should().Be("http://ajwebsolutions.com/contactme");
            result[5].Uri.Should().Be("http://www.ajwebsolutions.com/contactme");
            result[6].Uri.Should().Be("https://ajwebsolutions.com/contactme");
            result[7].Uri.Should().Be("https://www.ajwebsolutions.com/contactme");
        }
    }
}