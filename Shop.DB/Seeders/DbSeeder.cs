using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Shop.DB.Context;
using Shop.DB.Entities.Catalog;
using Shop.DB.Entities.Catalog.Products;
using Shop.DB.Entities.Catalog.Products.Attributes;
using Shop.DB.Entities.Catalog.Products.Images;
using Shop.DB.Entities.Catalog.Products.Options;
using Shop.DB.Entities.Discounts;
using Shop.DB.Entities.Identity;

namespace Shop.DB.Seeders;

public static class DbSeeder
{
    public static void Seed(MyShopDbContext db)
    {
        SeedRoles(db);
        SeedAdminUser(db);
        SeedCategories(db);
        SeedProductOptions(db);
        SeedProductAttributes(db);
        SeedProducts(db);
        SeedDiscounts(db);

        db.SaveChanges();
    }

    private static void SeedRoles(MyShopDbContext db)
    {
        if (db.Roles.Any()) return;

        db.Roles.AddRange(
            new Role { Name = "Customer" },
            new Role { Name = "Admin" },
            new Role { Name = "Manager" }
        );
        db.SaveChanges();
    }

    private static void SeedAdminUser(MyShopDbContext db)
    {
        var adminRole = db.Roles.FirstOrDefault(r => r.Name == "Admin");
        var adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

        if (adminRole == null || string.IsNullOrEmpty(adminPassword)) return;
        if (db.Users.Any(u => u.RoleId == adminRole.Id)) return;

        db.Users.Add(new User
        {
            Email = "admin@myshop.com",
            Username = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(adminPassword),
            RoleId = adminRole.Id,
            IsBlocked = false
        });
        db.SaveChanges();
    }

    private static void SeedCategories(MyShopDbContext db)
    {
        if (db.Categories.Any()) return;

        db.Categories.Add(new Category
        {
            Name = "All",
            SubCategories =
            [
                new Category
                {
                    Name = "Men",
                    SubCategories =
                    [
                        new Category { Name = "Hoodie" },
                        new Category { Name = "T-Shirt" },
                        new Category { Name = "Pants" },
                        new Category { Name = "Jacket" }
                    ]
                },
                new Category
                {
                    Name = "Women",
                    SubCategories =
                    [
                        new Category { Name = "Dresses" },
                        new Category { Name = "Blouse" },
                        new Category { Name = "Skirt" }
                    ]
                },
                new Category
                {
                    Name = "Accessories",
                    SubCategories =
                    [
                        new Category { Name = "Bags" },
                        new Category { Name = "Hats" }
                    ]
                }
            ]
        });
        db.SaveChanges();
    }

    private static void SeedProductOptions(MyShopDbContext db)
    {
        if (db.ProductOptions.Any()) return;

        var colorOption = new ProductOption { Name = "Color" };
        var sizeOption = new ProductOption { Name = "Size" };
        db.ProductOptions.AddRange(colorOption, sizeOption);
        db.SaveChanges();

        db.ProductOptionValues.AddRange(
            new ProductOptionValue { ProductOptionId = colorOption.Id, Value = "Black", SortOrder = 0, ColorHex = "#000000" },
            new ProductOptionValue { ProductOptionId = colorOption.Id, Value = "White", SortOrder = 1, ColorHex = "#FFFFFF" },
            new ProductOptionValue { ProductOptionId = colorOption.Id, Value = "Navy", SortOrder = 2, ColorHex = "#000080" },
            new ProductOptionValue { ProductOptionId = colorOption.Id, Value = "Beige", SortOrder = 3, ColorHex = "#F5F5DC" },
            new ProductOptionValue { ProductOptionId = colorOption.Id, Value = "Burgundy", SortOrder = 4, ColorHex = "#800020" },
            new ProductOptionValue { ProductOptionId = colorOption.Id, Value = "Green", SortOrder = 5, ColorHex = "#008000" },
            new ProductOptionValue { ProductOptionId = colorOption.Id, Value = "Yellow", SortOrder = 6, ColorHex = "#FFFF00" },
            new ProductOptionValue { ProductOptionId = colorOption.Id, Value = "Gray", SortOrder = 7, ColorHex = "#A9A9A9" },
            new ProductOptionValue { ProductOptionId = colorOption.Id, Value = "Dark Gray", SortOrder = 8, ColorHex = "#808080" },
            new ProductOptionValue { ProductOptionId = colorOption.Id, Value = "Brown", SortOrder = 9, ColorHex = "#8B4513" },
            new ProductOptionValue { ProductOptionId = colorOption.Id, Value = "Pink", SortOrder = 10, ColorHex = "#FFC0CB" },
            new ProductOptionValue { ProductOptionId = colorOption.Id, Value = "Purple", SortOrder = 11, ColorHex = "#800080" },
            new ProductOptionValue { ProductOptionId = colorOption.Id, Value = "Blue", SortOrder = 12, ColorHex = "#0000FF" },

            new ProductOptionValue { ProductOptionId = sizeOption.Id, Value = "XS", SortOrder = 0 },
            new ProductOptionValue { ProductOptionId = sizeOption.Id, Value = "S", SortOrder = 1 },
            new ProductOptionValue { ProductOptionId = sizeOption.Id, Value = "M", SortOrder = 2 },
            new ProductOptionValue { ProductOptionId = sizeOption.Id, Value = "L", SortOrder = 3 },
            new ProductOptionValue { ProductOptionId = sizeOption.Id, Value = "XL", SortOrder = 4 },
            new ProductOptionValue { ProductOptionId = sizeOption.Id, Value = "One Size", SortOrder = 5 }
        );
        db.SaveChanges();
    }

    private static void SeedProductAttributes(MyShopDbContext db)
    {
        if (db.ProductAttributes.Any()) return;

        var materialAttr = new ProductAttribute { Name = "Material" };
        var seasonAttr = new ProductAttribute { Name = "Season" };
        db.ProductAttributes.AddRange(materialAttr, seasonAttr);
        db.SaveChanges();

        db.ProductAttributeValues.AddRange(
            new ProductAttributeValue { ProductAttributeId = materialAttr.Id, Value = "Cotton" },
            new ProductAttributeValue { ProductAttributeId = materialAttr.Id, Value = "Polyester" },
            new ProductAttributeValue { ProductAttributeId = materialAttr.Id, Value = "Wool" },
            new ProductAttributeValue { ProductAttributeId = materialAttr.Id, Value = "Linen" },
            new ProductAttributeValue { ProductAttributeId = materialAttr.Id, Value = "Denim" },

            new ProductAttributeValue { ProductAttributeId = seasonAttr.Id, Value = "Spring" },
            new ProductAttributeValue { ProductAttributeId = seasonAttr.Id, Value = "Summer" },
            new ProductAttributeValue { ProductAttributeId = seasonAttr.Id, Value = "Autumn" },
            new ProductAttributeValue { ProductAttributeId = seasonAttr.Id, Value = "Winter" }
        );
        db.SaveChanges();
    }

    private static void SeedProducts(MyShopDbContext db)
    {
        if (db.Products.Any()) return;

        var allCategories = db.Categories
            .Include(c => c.SubCategories)
                .ThenInclude(c => c.SubCategories)
            .AsSplitQuery()
            .AsNoTracking()
            .ToList();

        Category GetCat(string parent, string name) =>
            allCategories
                .SelectMany(c => c.SubCategories)
                .First(c => c.Name == parent)
                .SubCategories
                .First(c => c.Name == name);

        var catHoodie = GetCat("Men", "Hoodie");
        var catTShirt = GetCat("Men", "T-Shirt");
        var catPants = GetCat("Men", "Pants");
        var catJacket = GetCat("Men", "Jacket");
        var catDresses = GetCat("Women", "Dresses");
        var catBlouse = GetCat("Women", "Blouse");
        var catSkirt = GetCat("Women", "Skirt");
        var catBags = GetCat("Accessories", "Bags");
        var catHats = GetCat("Accessories", "Hats");

        var attrValues = db.ProductAttributeValues
            .AsNoTracking()
            .ToDictionary(v => v.Value);

        var cotton = attrValues["Cotton"];
        var polyester = attrValues["Polyester"];
        var wool = attrValues["Wool"];
        var linen = attrValues["Linen"];
        var denim = attrValues["Denim"];
        var spring = attrValues["Spring"];
        var summer = attrValues["Summer"];
        var autumn = attrValues["Autumn"];
        var winter = attrValues["Winter"];

        var colorOpt = db.ProductOptions.First(o => o.Name == "Color");
        var sizeOpt = db.ProductOptions.First(o => o.Name == "Size");

        var colors = db.ProductOptionValues
            .Where(v => v.ProductOptionId == colorOpt.Id)
            .AsNoTracking()
            .ToDictionary(v => v.Value);

        var sizes = db.ProductOptionValues
            .Where(v => v.ProductOptionId == sizeOpt.Id)
            .AsNoTracking()
            .ToDictionary(v => v.Value);


        var catalogPairs = new List<(Product product, ProductVariant defaultVariant)>();

        catalogPairs.Add(CreateSeedProduct(db, colorOpt.Id, sizeOpt.Id,
            name: "Classic Hoodie Black & Beige",
            desc: "Timeless pullover hoodie crafted from soft cotton. Features a kangaroo pocket and ribbed cuffs — perfect for layering in cold weather.",
            categoryId: catHoodie.Id,
            isNew: true,
            isPopular: true,
            attrs: [cotton, winter],
            variants:
            [
                (colors["Black"], sizes["S"], 59.99m, 15),
                (colors["Black"], sizes["M"], 59.99m, 20),
                (colors["Black"], sizes["L"], 59.99m, 10),
                (colors["Beige"], sizes["S"], 64.99m, 12),
                (colors["Beige"], sizes["M"], 64.99m, 18)
            ],
            images:
            [
                ("https://i.postimg.cc/ZKtg7Dpz/hoodie-black-1.png", colors["Black"]),
                ("https://i.postimg.cc/fTN2jSnr/hoodie-black-2.png", colors["Black"]),
                ("https://i.postimg.cc/TwT1Jc0J/hoodie-beige-1.png", colors["Beige"])
            ]
            ));

        catalogPairs.Add(CreateSeedProduct(db, colorOpt.Id, sizeOpt.Id,
            name: "Navy Oversized Hoodie",
            desc: "A relaxed-fit oversized hoodie in a rich navy colorway. Made from heavyweight cotton for extra warmth during autumn days.",
            categoryId: catHoodie.Id,
            isNew: true,
            isPopular: false,
            attrs: [cotton, autumn],
            variants:
            [
                (colors["Navy"], sizes["M"], 69.99m, 14),
                (colors["Navy"], sizes["L"], 69.99m, 10),
                (colors["Navy"], sizes["XL"], 74.99m, 8)
            ],
            images:
            [
                ("https://i.postimg.cc/J4X8tkcz/hoodie-navy-1.png", colors["Navy"]),
                ("https://i.postimg.cc/PxcG5XPB/hoodie-navy-2.png", colors["Navy"])
            ]
            ));

        catalogPairs.Add(CreateSeedProduct(db, colorOpt.Id, sizeOpt.Id,
            name: "Essential White & Black Tees",
            desc: "Everyday essential T-shirts in clean white and classic black. Lightweight 100% cotton fabric with a regular fit — a wardrobe staple for any season.",
            categoryId: catTShirt.Id,
            isNew: true,
            isPopular: true,
            attrs: [cotton, summer],
            variants:
            [
                (colors["White"], sizes["XS"], 24.99m, 25),
                (colors["White"], sizes["S"], 24.99m, 30),
                (colors["White"], sizes["M"], 24.99m, 35),
                (colors["White"], sizes["L"], 24.99m, 20),
                (colors["Black"], sizes["S"], 24.99m, 28),
                (colors["Black"], sizes["M"], 24.99m, 32),
                (colors["Black"], sizes["L"], 24.99m, 18)
            ],
            images:
            [
                ("https://i.postimg.cc/Hs1YwzLy/tshirt-white-1.png", colors["White"]),
                ("https://i.postimg.cc/PrMjmkc6/tshirt-white-2.png", colors["White"]),
                ("https://i.postimg.cc/Gm14prDP/tshirt-black-1.png", colors["Black"])
            ]
            ));

        catalogPairs.Add(CreateSeedProduct(db, colorOpt.Id, sizeOpt.Id,
            name: "Graphic Print Tee",
            desc: "A bold statement tee featuring an artistic graphic print on premium cotton. Slightly oversized silhouette pairs well with jeans or joggers.",
            categoryId: catTShirt.Id,
            isNew: false,
            isPopular: false,
            attrs: [cotton, spring],
            variants:
            [
                (colors["Black"], sizes["S"], 34.99m, 15),
                (colors["Black"], sizes["M"], 34.99m, 20),
                (colors["Black"], sizes["L"], 34.99m, 12)
            ],
            images:
            [
                ("https://i.postimg.cc/KcB3LJ2d/tshirt-graphic-black-1.png", colors["Black"]),
                ("https://i.postimg.cc/gjWJNbWQ/tshirt-graphic-black-2.png", colors["Black"]),
                ("https://i.postimg.cc/J7Ntt03C/tshirt-graphic-black-3.png", colors["Black"])
            ]
            ));

        catalogPairs.Add(CreateSeedProduct(db, colorOpt.Id, sizeOpt.Id,
            name: "Skinny Blue Jeans",
            desc: "Classic slim-fit jeans in a versatile mid-blue wash. Cut from sturdy denim with slight stretch for all-day comfort.",
            categoryId: catPants.Id,
            isNew: false,
            isPopular: false,
            attrs: [denim, spring],
            variants:
            [
                (colors["Blue"], sizes["S"], 79.99m, 10),
                (colors["Blue"], sizes["M"], 79.99m, 15),
                (colors["Blue"], sizes["L"], 79.99m, 12)
            ],
            images:
            [
                ("https://i.postimg.cc/DysV741f/jeans-slim-blue-1.png", colors["Blue"]),
                ("https://i.postimg.cc/MHQLZgMt/jeans-slim-blue-2.png", colors["Blue"]),
                ("https://i.postimg.cc/BngVvTb6/jeans-slim-blue-3.png", colors["Blue"])
            ]
            ));

        catalogPairs.Add(CreateSeedProduct(db, colorOpt.Id, sizeOpt.Id,
            name: "Baggy Dark Gray Jeans",
            desc: "Wide-leg baggy jeans in a faded dark gray denim. A streetwear-inspired silhouette with roomy comfort for casual everyday wear.",
            categoryId: catPants.Id,
            isNew: false,
            isPopular: false,
            attrs: [denim, autumn],
            variants:
            [
                (colors["Dark Gray"], sizes["S"], 89.99m, 10),
                (colors["Dark Gray"], sizes["M"], 89.99m, 12),
                (colors["Dark Gray"], sizes["L"], 89.99m, 8)
            ],
            images:
            [
                ("https://i.postimg.cc/k4kjHw1c/jeans-baggy-dark-gray-1.png", colors["Dark Gray"]),
                ("https://i.postimg.cc/x1TPtWjz/jeans-baggy-dark-gray-2.png", colors["Dark Gray"]),
                ("https://i.postimg.cc/pXkfhqfZ/jeans-baggy-dark-gray-3.png", colors["Dark Gray"])
            ]
            ));

        catalogPairs.Add(CreateSeedProduct(db, colorOpt.Id, sizeOpt.Id,
            name: "Wool Blend Overcoat",
            desc: "A sophisticated long overcoat tailored from a premium wool blend. Clean lines and a structured silhouette make it an elegant choice for autumn and early winter.",
            categoryId: catJacket.Id,
            isNew: false,
            isPopular: true,
            attrs: [wool, autumn],
            variants:
            [
                (colors["Beige"], sizes["M"], 239.99m, 5),
                (colors["Beige"], sizes["L"], 229.99m, 9),
                (colors["Black"], sizes["M"], 229.99m, 10),
                (colors["Black"], sizes["L"], 229.99m, 8)
            ],
            images:
            [
                ("https://i.postimg.cc/BZPZ16xX/coat-wool-beige-1.png", colors["Beige"]),
                ("https://i.postimg.cc/g2JTnwZH/coat-wool-beige-2.png", colors["Beige"]),
                ("https://i.postimg.cc/DyfCMWB8/coat-wool-beige-3.png", colors["Beige"]),
                ("https://i.postimg.cc/8PsRvLZN/coat-wool-dark-1.png", colors["Black"]),
                ("https://i.postimg.cc/DzQLh6wc/coat-wool-black-2.png", colors["Black"])
            ]
            ));

        catalogPairs.Add(CreateSeedProduct(db, colorOpt.Id, sizeOpt.Id,
            name: "Floral Midi Dress",
            desc: "A light and airy midi dress with a delicate floral pattern. Crafted from breathable linen for effortless summer styling.",
            categoryId: catDresses.Id,
            isNew: false,
            isPopular: false,
            attrs: [linen, summer],
            variants:
            [
                (colors["White"], sizes["XS"], 109.99m, 10),
                (colors["White"], sizes["S"], 109.99m, 14),
                (colors["White"], sizes["M"], 109.99m, 12)
            ],
            images:
            [
                ("https://i.postimg.cc/MT7SdctQ/dress-midi-floral-white-1.jpg", colors["White"]),
                ("https://i.postimg.cc/pX90vNbw/dress-midi-floral-white-2.jpg", colors["White"]),
                ("https://i.postimg.cc/QMqmmh19/dress-midi-floral-white-3.jpg", colors["White"])
            ]
            ));

        catalogPairs.Add(CreateSeedProduct(db, colorOpt.Id, sizeOpt.Id,
            name: "Knitted Dress",
            desc: "A cozy knitted midi dress in a fine wool blend. Fitted silhouette with ribbed texture — ideal for autumn evenings.",
            categoryId: catDresses.Id,
            isNew: false,
            isPopular: false,
            attrs: [wool, autumn],
            variants:
            [
                (colors["Black"], sizes["XS"], 129.99m, 8),
                (colors["Black"], sizes["S"], 129.99m, 12),
                (colors["Black"], sizes["M"], 129.99m, 10),
                (colors["White"], sizes["S"], 134.99m, 7),
                (colors["White"], sizes["M"], 134.99m, 9)
            ],
            images:
            [
                ("https://i.postimg.cc/3R4BTpzw/dress-knitted-black-1.png", colors["Black"]),
                ("https://i.postimg.cc/5tHvYJgm/dress-knitted-black-2.png", colors["Black"]),
                ("https://i.postimg.cc/RhmnDHBG/dress-knitted-white-1.png", colors["White"])
            ]
            ));

        catalogPairs.Add(CreateSeedProduct(db, colorOpt.Id, sizeOpt.Id,
            name: "Silk Touch Blouse",
            desc: "A flowing blouse with a luxurious silk-like finish. Lightweight polyester drapes beautifully — easy to dress up or down for spring.",
            categoryId: catBlouse.Id,
            isNew: true,
            isPopular: false,
            attrs: [polyester, spring],
            variants:
            [
                (colors["White"], sizes["XS"], 59.99m, 12),
                (colors["White"], sizes["S"], 59.99m, 16),
                (colors["White"], sizes["M"], 59.99m, 14),
                (colors["Brown"], sizes["S"], 64.99m, 10),
                (colors["Brown"], sizes["M"], 64.99m, 12),
                (colors["Burgundy"], sizes["S"], 59.99m, 11),
                (colors["Burgundy"], sizes["M"], 64.99m, 12)
            ],
            images:
            [
                ("https://i.postimg.cc/sgzsG4cR/blouse-silk-burgundy-1.png", colors["Burgundy"]),
                ("https://i.postimg.cc/FF6M3Vh6/blouse-silk-white-1.png", colors["White"]),
                ("https://i.postimg.cc/pV027jRs/blouse-silk-brown-1.png", colors["Brown"])
            ]
            ));

        catalogPairs.Add(CreateSeedProduct(db, colorOpt.Id, sizeOpt.Id,
            name: "Midi Skirt",
            desc: "A versatile midi skirt with a soft, flowing silhouette. Available in neutral tones — pairs effortlessly with blouses and casual tops.",
            categoryId: catSkirt.Id,
            isNew: false,
            isPopular: false,
            attrs: [polyester, spring],
            variants:
            [
                (colors["White"], sizes["XS"], 69.99m, 10),
                (colors["White"], sizes["S"], 69.99m, 14),
                (colors["White"], sizes["M"], 69.99m, 12),
                (colors["Gray"], sizes["S"], 74.99m, 9),
                (colors["Gray"], sizes["M"], 74.99m, 11),
                (colors["Brown"], sizes["S"], 74.99m, 7)
            ],
            images:
            [
                ("https://i.postimg.cc/Y24Wdtw2/skirt-midi-grey-1.png", colors["Gray"]),
                ("https://i.postimg.cc/SKNXChrJ/skirt-midi-grey-2.png", colors["Gray"]),
                ("https://i.postimg.cc/4ytnsNJt/skirt-midi-grey-3.png", colors["Gray"]),
                ("https://i.postimg.cc/N0YFYwVd/skirt-midi-white-1.png", colors["White"]),
                ("https://i.postimg.cc/1z9m2rn8/skirt-midi-white-2.png", colors["White"]),
                ("https://i.postimg.cc/SxVRyPpH/skirt-midi-brown-1.png", colors["Brown"]),
                ("https://i.postimg.cc/Y9LCzqmS/skirt-midi-brown-2.png", colors["Brown"])
            ]
            ));

        catalogPairs.Add(CreateSeedProduct(db, colorOpt.Id, sizeOpt.Id,
            name: "Shopper Bag",
            desc: "A spacious cotton canvas shopper bag in vibrant seasonal colors. Sturdy handles and a roomy interior make it a perfect everyday carryall.",
            categoryId: catBags.Id,
            isNew: false,
            isPopular: true,
            attrs: [cotton, spring],
            variants:
            [
                (colors["Green"], sizes["One Size"], 44.99m, 14),
                (colors["Yellow"], sizes["One Size"], 44.99m, 10)
            ],
            images:
            [
                ("https://i.postimg.cc/qMw6Rqsb/bag-leather-green-1.png", colors["Green"]),
                ("https://i.postimg.cc/T2qp7141/bag-leather-green-2.png", colors["Green"]),
                ("https://i.postimg.cc/B6fnNf5F/bag-leather-green-3.png", colors["Green"]),
                ("https://i.postimg.cc/bNVYWt0K/bag-leather-yellow-1.png", colors["Yellow"]),
                ("https://i.postimg.cc/Y97tYfkt/bag-leather-yellow-2.png", colors["Yellow"])
            ]
            ));

        catalogPairs.Add(CreateSeedProduct(db, colorOpt.Id, sizeOpt.Id,
            name: "Knitted Scarf",
            desc: "A chunky knitted scarf in soft wool. Long enough to wrap and layer — an essential winter accessory in cozy neutral and pastel tones.",
            categoryId: catHats.Id,
            isNew: false,
            isPopular: false,
            attrs: [wool, winter],
            variants:
            [
                (colors["Beige"], sizes["One Size"], 29.99m, 25),
                (colors["Pink"], sizes["One Size"], 29.99m, 20),
                (colors["Dark Gray"], sizes["One Size"], 29.99m, 18)
            ],
            images:
            [
                ("https://i.postimg.cc/d15jr6dg/scarf-knitted-pink-1.png", colors["Pink"]),
                ("https://i.postimg.cc/502w4zG6/scarf-knitted-pink-2.png", colors["Pink"]),
                ("https://i.postimg.cc/26r043tp/scarf-knitted-beige-1.png", colors["Beige"]),
                ("https://i.postimg.cc/jSfcmz00/scarf-knitted-beige-2.png", colors["Beige"]),
                ("https://i.postimg.cc/KjKBL2mR/scarf-knitted-dark-gray-1.png", colors["Dark Gray"]),
                ("https://i.postimg.cc/DyX1wnC6/scarf-knitted-dark-gray-2.png", colors["Dark Gray"])
            ]
            ));

        catalogPairs.Add(CreateSeedProduct(db, colorOpt.Id, sizeOpt.Id,
            name: "Ribbed Knit Beanie",
            desc: "A classic ribbed beanie knitted from soft wool blend. Slim and slouchy fit keeps your head warm in style through winter.",
            categoryId: catHats.Id,
            isNew: false,
            isPopular: false,
            attrs: [wool, winter],
            variants:
            [
                (colors["Beige"], sizes["One Size"], 29.99m, 25),
                (colors["Purple"], sizes["One Size"], 29.99m, 20)
            ],
            images:
            [
                ("https://i.postimg.cc/8kWh1TXb/hat-knitted-purple-1.png", colors["Purple"]),
                ("https://i.postimg.cc/d0pyYf8x/hat-knitted-purple-2.png", colors["Purple"]),
                ("https://i.postimg.cc/br2Sr9Rw/hat-knitted-beige-1.png", colors["Beige"]),
                ("https://i.postimg.cc/13MGLM7X/hat-knitted-beige-2.png", colors["Beige"])
            ]
            ));

        db.SaveChanges();

        foreach (var (product, defaultVariant) in catalogPairs)
            product.CatalogVariant = defaultVariant;

        db.SaveChanges();
    }

    private static (Product product, ProductVariant defaultVariant) CreateSeedProduct(
        MyShopDbContext db,
        int colorOptId,
        int sizeOptId,
        string name,
        string desc,
        int categoryId,
        bool isNew,
        bool isPopular,
        ProductAttributeValue[] attrs,
        (ProductOptionValue color, ProductOptionValue size, decimal price, int stock)[] variants,
        (string url, ProductOptionValue color)[] images)
    {
        var now = DateTimeOffset.UtcNow;

        var product = new Product
        {
            Name = name,
            Description = desc,
            CategoryId = categoryId,
            IsArchived = false,
            IsNew = isNew,
            IsPopular = isPopular,
            CreatedAt = now,
            UpdatedAt = now
        };
        db.Products.Add(product);

        foreach (var attr in attrs)
            db.ProductAttributeLinks.Add(new ProductAttributeLink
            {
                Product = product,
                ProductAttributeValueId = attr.Id
            });

        var colorOptionLinks = variants
            .Select(v => v.color)
            .DistinctBy(c => c.Id)
            .Select(c => new ProductOptionLink { Product = product, ProductOptionValueId = c.Id })
            .ToList();

        var sizeOptionLinks = variants
            .Select(v => v.size)
            .DistinctBy(s => s.Id)
            .Select(s => new ProductOptionLink { Product = product, ProductOptionValueId = s.Id })
            .ToList();

        db.ProductOptionLinks.AddRange(colorOptionLinks);
        db.ProductOptionLinks.AddRange(sizeOptionLinks);

        var optionLinkByValueId = colorOptionLinks
            .Concat(sizeOptionLinks)
            .ToDictionary(pol => (int)pol.ProductOptionValueId!);

        ProductVariant? defaultVariant = null;

        foreach (var (color, size, price, stock) in variants)
        {
            var variant = new ProductVariant
            {
                Product = product,
                Price = price,
                StockQuantity = stock,
                VariantKey = $"{color.Value.ToLower()}_{size.Value.ToLower().Replace(" ", "_")}"
            };

            variant.VariantOptionLinks.Add(new VariantOptionLink
            {
                ProductVariant = variant,
                ProductOptionLink = optionLinkByValueId[color.Id],
                ProductOptionId = colorOptId,
                ProductOptionValueId = color.Id
            });

            variant.VariantOptionLinks.Add(new VariantOptionLink
            {
                ProductVariant = variant,
                ProductOptionLink = optionLinkByValueId[size.Id],
                ProductOptionId = sizeOptId,
                ProductOptionValueId = size.Id
            });

            db.ProductVariants.Add(variant);

            defaultVariant ??= variant;
        }

        var sortOrder = 0;
        foreach (var (url, color) in images)
            db.ProductImages.Add(new ProductImage
            {
                Product = product,
                Url = url,
                ProductOptionValueId = color.Id,
                SortOrder = sortOrder++
            });

        return (product, defaultVariant ?? db.ProductVariants.Local.First(v => v.Product == product));
    }

    private static void SeedDiscounts(MyShopDbContext db)
    {
        if (db.Discounts.Any()) return;

        var saleType = new DiscountType { Name = "Sale" };
        var promoType = new DiscountType { Name = "Promo" };
        db.DiscountTypes.AddRange(saleType, promoType);
        db.SaveChanges();

        var now = DateTimeOffset.UtcNow;

        var p1 = db.Products.First(p => p.Name == "Classic Hoodie Black & Beige");
        var p2 = db.Products.First(p => p.Name == "Essential White & Black Tees");
        var p3 = db.Products.First(p => p.Name == "Floral Midi Dress");

        var d1 = new Discount
        {
            Name = "Hoodie Winter Sale",
            TypeId = saleType.Id,
            Form = 0,
            Value = 20,
            IsActive = true,
            StartAt = now,
            EndAt = now.AddYears(1)
        };
        var d2 = new Discount
        {
            Name = "Tee Summer Promo",
            TypeId = promoType.Id,
            Form = 1,
            Value = 5,
            IsActive = true,
            StartAt = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            EndAt = new DateTimeOffset(2026, 12, 31, 23, 59, 59, TimeSpan.Zero)
        };
        var d3 = new Discount
        {
            Name = "Dress Spring Sale",
            TypeId = saleType.Id,
            Form = 0,
            Value = 15,
            IsActive = true,
            StartAt = now,
            EndAt = now.AddMonths(3)
        };

        db.Discounts.AddRange(d1, d2, d3);
        db.SaveChanges();

        db.DiscountProducts.AddRange(
            new DiscountProduct { DiscountId = d1.Id, ProductId = p1.Id },
            new DiscountProduct { DiscountId = d2.Id, ProductId = p2.Id },
            new DiscountProduct { DiscountId = d3.Id, ProductId = p3.Id }
        );
        db.SaveChanges();
    }
}