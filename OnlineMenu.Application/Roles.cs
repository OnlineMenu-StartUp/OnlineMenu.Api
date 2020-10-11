namespace OnlineMenu.Application
{
    /// <summary>
    /// Customer is assigned an ID when he enters the first time
    /// Cook's account is created by the admin
    /// Admin has an account in db, and can create other admins
    /// </summary>
    /// Also, its a static class rather than an enum because [Authorize] requires a string
    public static class Roles
    {
        public const string Admin = "Admin"; // Can see cooks, customers and orders, and edit the menu(in future will edit the UI)
        public const string Customer = "Customer"; // Can make order and see his order
        public const string Cook = "Cook"; // Can see all orders in restaurant and complete order
    }
}