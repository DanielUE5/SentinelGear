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

        public class Order
        {
            public const int FirstNameMaxLength = 50;
            public const int LastNameMaxLength = 50;
            public const int EmailMaxLength = 100;
            public const string PhoneNumberPattern = @"^(\+359|0)8[7-9]\d{2}\s?\d{3}\s?\d{3}$";
            public const int AddressMaxLength = 100;
        }

        public class OrderItem
        {
            public const int minQuantity = 1;
        }

        public class ContactMessage
        {
            public const int NameMaxLength = 100;
            public const int EmailMaxLength = 100;
            public const int SubjectMaxLength = 200;
            public const int MessageMaxLength = 2000;
        }

        public class CategoryFormViewModel
        {
            public const int NameMaxLength = 100;
        }

        public class CheckoutViewModel
        {
            public const int FirstNameMaxLength = 50;
            public const int LastNameMaxLength = 50;
            public const int EmailMaxLength = 100;
            public const string PhoneNumberPattern = @"^(\+359|0)8[7-9]\d{2}\s?\d{3}\s?\d{3}$";
            public const int AddressMaxLength = 100;
        }

        public class ProductFormViewModel
        {
            public const int NameMaxLength = 100;
            public const int DescriptionMaxLength = 1000;
            public const int ManufacturerMaxLength = 100;
        }
    }
}