using Soenneker.Pinterest.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Pinterest.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class PinterestOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IPinterestOpenApiClientUtil _openapiclientutil;

    public PinterestOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IPinterestOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
