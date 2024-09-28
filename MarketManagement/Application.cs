using MaretManagement.Domain.Aggregates.Product;
using MaretManagement.Domain.Aggregates.ShoppingCart;
using MaretManagement.Domain.Aggregates.ShoppingCart.Entities;
using MaretManagement.Domain.Repositories;
using MaretManagement.Domain.Specifications.DiscountRules;
using System.Numerics;


namespace MarketManagement;

public class Application
{
    private readonly IRepository<Product> _productRepo;
    private readonly IRepository<ShoppingCart> _shoppingCartRepo;

    public Application(IRepository<Product> productRepo, IRepository<ShoppingCart> shoppingCartRepo)
    {
        _productRepo = productRepo;
        _shoppingCartRepo=shoppingCartRepo;
    }

    public void Run()
    {
        string? userInput;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Supermarché Console App ===");
            Console.WriteLine("1. Quitter l'application");
            Console.WriteLine("2. Afficher les produits");
            Console.WriteLine("3. Ajouter un produit au panier");
            Console.WriteLine("4. Afficher le panier");
            Console.WriteLine("Entrez le numéro de votre choix :");

            userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Console.WriteLine("Fermeture de l'application...");
                    break;
                case "2":
                    DisplayProducts();
                    break;
                case "3":
                    AddProductToCart();
                    break;
                case "4":
                    DisplayCart();
                    break;
                default:
                    Console.WriteLine("Choix invalide. Veuillez essayer à nouveau.");
                    Console.WriteLine("Appuyez sur une touche pour continuer...");
                    Console.ReadKey();
                    break;
            }

        } while (userInput != "1");

        Console.WriteLine("L'application a été fermée. Merci d'avoir utilisé notre service.");
    }

    private void DisplayProducts(bool isDisplayMenu = true)
    {
        Console.Clear();
        Console.WriteLine("=== Liste des Produits ===");
        foreach (var product in _productRepo.GetAll())
        {
            Console.WriteLine(product.ToString());
        }
        if (!isDisplayMenu)
        {
            return;
        }
        Console.WriteLine("Appuyez sur une touche pour revenir au menu principal...");
        Console.ReadKey();
    }

    private void AddProductToCart()
    {
        Console.Clear();
        Console.WriteLine("=== Ajout de Produit au Panier ===");

        DisplayProducts(false);
        Console.WriteLine("q pour revenir au menu principal...");

        Console.WriteLine("Entrez l'ID du produit à ajouter au panier :");
        if (int.TryParse(Console.ReadLine(), out int productId))
        {
            var product = _productRepo.Get(productId);
            if (product != null)
            {
                Console.WriteLine($"Combien de {product.Name} voulez-vous ajouter ?");
                if (int.TryParse(Console.ReadLine(), out int quantity))
                {
                    var existingCartItem = _shoppingCartRepo.Get(0);
                    existingCartItem.AddProductToCart(product, quantity);
                    _shoppingCartRepo.Update(existingCartItem);
                    Console.WriteLine($"{quantity} {product.Name}(s) ont été ajoutés au panier.");
                }
                else
                {
                    Console.WriteLine("Quantité invalide. Aucune modification n'a été apportée.");
                }
            }
            else
            {
                Console.WriteLine("Produit non trouvé.");
            }
        }
        else
        {
            Console.WriteLine("ID de produit invalide.");
        }

    }

    private void DisplayCart()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("=== Contenu du Panier ===");
            var discountProduct1 = _productRepo.Get(1);
            var discountProduct2 = _productRepo.Get(5);
            var shoppingCart = _shoppingCartRepo.Get(0);
            shoppingCart.AddDiscountRules([new BundleOneEuroDiscount(), new BuyXGetYFree(discountProduct1), new BuyXGetYFree(discountProduct2)]);
            shoppingCart.GetDiscountsAmount();
            Console.WriteLine($"Total du panier sans remise(s) : {shoppingCart.GetTotalBeforeDiscount()}");
            Console.WriteLine($"Total des remise : {shoppingCart.GetDiscountsAmount()}");
            Console.WriteLine($"Total du panier apres remise(s) : {shoppingCart.GetTotalBeforeDiscount() - shoppingCart.GetDiscountsAmount()}");
            Console.WriteLine("Appuyez sur une touche pour revenir au menu principal...");
            Console.ReadKey();
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Appuyez sur une touche pour revenir au menu principal...");
            Console.ReadKey();
        }
    }

    private void Checkout()
    {
        Console.Clear();
        Console.WriteLine("Commande payée, merci");
        _shoppingCartRepo.DeleteAll();
    }
}

