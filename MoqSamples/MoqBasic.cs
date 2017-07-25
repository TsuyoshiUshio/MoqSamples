using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace MoqSamples
{
    public class ProductType
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }
    public interface IProudct
    {
        string GetContent(string url);
        ProductType ProductType { get; set; }

    }

    public class ProductImpl : IProudct
    {
        public virtual ProductType ProductType { get; set; }

        public virtual string GetContent(string url) 
        {
            return $"{url}: <html><head><title>hello</title></head><body>Hello!</body></html> "; 
        }
    }

    public class MoqBasic
    {
        public void Exec()
        {
            // Interface mocking
            var productMock = new Mock<IProudct>();
            productMock.Setup(product => product.GetContent("http://www.yahoo.co.jp")).Returns("This is html.");
            Console.WriteLine("#[MoqBasic] http://www.yahoo.co.jp:" + productMock.Object.GetContent("http://www.yahoo.co.jp"));
            Console.WriteLine("#[MoqBasic] http://www.foo.bar:" + productMock.Object.GetContent("http://www.foo.bar"));

            // Nested Object Mocking

            productMock.Setup(product => product.ProductType.Name).Returns("Suppliment");
            Console.WriteLine("#[MoqBasic] IProduct.ProductType.Name:" + productMock.Object.ProductType.Name);

            // Class mocking

            var productImplMock = new Mock<ProductImpl>();
            productImplMock.Setup(product => product.GetContent("http://www.yahoo.co.jp")).Returns("This is html.");
            Console.WriteLine("#[MoqBasic] http://www.yahoo.co.jp:" + productImplMock.Object.GetContent("http://www.yahoo.co.jp"));
            Console.WriteLine("#[MoqBasic] http://www.foo.bar:" + productImplMock.Object.GetContent("http://www.foo.bar"));

            // Validate the parameter
            try
            {
                productMock.Verify(product => product.GetContent("http://www.yahoo.co.jp"));
                productMock.Verify(product => product.GetContent("http://www.microsoft.com"));
            } catch (MockException ex)
            {
                Console.WriteLine("#[MoqBasic]" + ex.Message);
            }
        }
    }
}
