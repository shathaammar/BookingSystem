namespace BookingSystem.Application.Common
{
    public static class ResponseMessages
    {
        public static (string En, string Ar) EmailAlreadyExists =>
            ("Email already exists.", "البريد الإلكتروني مستخدم مسبقاً.");

        public static (string En, string Ar) InvalidRole =>
            ("Invalid role. Allowed: Customer or BusinessOwner.", "الدور غير صالح. المسموح: Customer أو BusinessOwner.");

        public static (string En, string Ar) RegisterSuccess =>
            ("Registration successful.", "تم التسجيل بنجاح.");

        public static (string En, string Ar) InvalidCredentials =>
            ("Invalid email or password.", "البريد الإلكتروني أو كلمة المرور غير صحيحة.");

        public static (string En, string Ar) LoginSuccess =>
            ("Login successful.", "تم تسجيل الدخول بنجاح.");

        public static (string En, string Ar) ServerError =>
            ("An unexpected error occurred.", "حدث خطأ غير متوقع.");
    }
}