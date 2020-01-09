namespace ClubestApp.Common
{
    public class ErrorMessages
    {
        public const string EmailRequired = "Имейлът е задължителен";

        public const string FirstNameRequired = "Името е задължителено";

        public const string LastNameRequired = "Фамилията е задължителна";

        public const string PasswordRequired = "Паролата е задължителна";

        public const string PasswordsDontMatch = "Паролите не съвпадат";

        public const string MinLength = "Паролата {0} трябва да бъде поне {2} символа";

        public const string DublicateEmail = "Имейлът вече е използван";

        public const string InvalidEmailOrPassword = "Невалиден имейл или парола";

        public const string ClubNameRequired = "Името на клуба e задължително";

        public const string ClubFeeRange = "Таксата трябва да е между {1} и {2} лева";

        public const string ClubFeeRequired = "Таксата на клуба е задължителна";

        public const string ClubPriceTypeRequired = "Видът на таксата е задължителен";

        public const string ClubIsPublicRequired = "Видът на клуба е задължителен";

        public const string ClubNameRange = "Името на клуба трябва да е между {2} и {1} символа";
    }
}