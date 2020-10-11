namespace OnlineMenu.Application
{
    /// <summary>
    /// Customer is assigned an ID when he enters the first time
    /// Cook's account is created by the admin
    /// Admin has an account in db, and can create other admins
    /// </summary>
    public enum Roles
    {
        Customer, // Can make order and see his order
        Cook, // Can see all orders in restaurant and complete order
        Admin // Can see cooks, customers and orders, and edit the menu, (in future will edit the UI)
    }
}