namespace DomainTester.Services
{
    public class UrlTestGenerator
    {
        public IEnumerable<UrlTestSuite> Execute(Uri url)
        {
            if (!url.IsDefaultPort)
            {
                // HTTPS & HTTP run on different ports, we have no way of determining what they might be
                throw new ApplicationException("We can only use the Default Ports");
            }

            // If a subdomain, other than www, is passed in, we have no easy way of finding out
            var nakedDomain = url.Host.Replace("www.", String.Empty);
            var expectedResult = new Uri($"https://www.{nakedDomain}");

            var uri = new Uri($"http://{nakedDomain}");
            yield return new UrlTestSuite(uri, expectedResult);

            uri = new Uri($"http://www.{nakedDomain}");
            yield return new UrlTestSuite(uri, expectedResult);

            uri = new Uri($"https://{nakedDomain}");
            yield return new UrlTestSuite(uri, expectedResult);

            yield return new UrlTestSuite(expectedResult, expectedResult);

            if (url.PathAndQuery != "/")
            {
                expectedResult = new Uri($"https://www.{nakedDomain}{url.PathAndQuery}");

                uri = new Uri($"http://{nakedDomain}{url.PathAndQuery}");
                yield return new UrlTestSuite(uri, expectedResult);

                uri = new Uri($"http://www.{nakedDomain}{url.PathAndQuery}");
                yield return new UrlTestSuite(uri, expectedResult);

                uri = new Uri($"https://{nakedDomain}{url.PathAndQuery}");
                yield return new UrlTestSuite(uri, expectedResult);

                yield return new UrlTestSuite(expectedResult, expectedResult);
            }
        }

        public struct UrlTestSuite
        {
            public UrlTestSuite(Uri uri, Uri expectedResult)
            {
                Uri = uri;
                ExpectedResult = expectedResult;
            }

            public Uri Uri { get; set; }

            public Uri ExpectedResult { get; set; }
        }
    }
}
