namespace SentinelGear.Common
{
    public static class EntityValidation
    {
        public class Category
        {
            public const int NameMaxLength = 100;
        }

        public class Product
        {
            public const int NameMaxLength = 100;
            public const int DescriptionMaxLength = 1000;
            public const int ManufacturerMaxLength = 100;
            public const int ImageUrlMaxLength = 2048;
            public const int minStockQuantity = 0;
        }

        public class CartItem
        {
            public const int minQuantity = 1;
            public const int maxQuantity = 100;
        }

        public class OrderItem
        {
            public const int minQuantity = 1;
        }
    }
}
