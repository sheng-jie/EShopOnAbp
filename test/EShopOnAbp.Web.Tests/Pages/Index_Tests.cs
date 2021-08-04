using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace EShopOnAbp.Pages
{
    public class Index_Tests : EShopOnAbpWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
