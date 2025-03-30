using E_Commerece.Data.Interfaces;
using E_Commerece.Data.Repositories;
using E_Commerece.Models;
using E_Commerece.Services.Implementations;
using E_Commerece.Services.Interfaces;
using E_Commerece.UnitOfWork.Implementations;
using E_Commerece.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductItemService, ProductItemService>();
builder.Services.AddScoped<IProductItemRepository, ProductItemRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<ICartItemService, CartItemService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IWishlistService, WishlistService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IVariationService, VariationService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<EcommereceContext>().AddDefaultTokenProviders();
builder.Services.AddDbContext<EcommereceContext>(optionbuilder =>
{
	optionbuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = new PathString("/Account/Login");
		options.LogoutPath = new PathString("/Account/Logout");
	})
	.AddFacebook(options =>
	{
		options.AppId = builder.Configuration["Authentication:Facebook:AppId"];
		options.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
		options.CallbackPath = new PathString("/signin-facebook");
	})
	.AddGoogle(googleOptions =>
	{
		googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
		googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
		googleOptions.CallbackPath = builder.Configuration["Authentication:Google:CallbackPath"];
		googleOptions.AccessType = "offline"; 
		googleOptions.SaveTokens = true; 

		googleOptions.Events.OnRedirectToAuthorizationEndpoint = context =>
		{
			var googleAuthUrl = context.RedirectUri + "&prompt=consent&select_account";
			context.Response.Redirect(googleAuthUrl);
			return Task.CompletedTask;
		};
	});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	await CreateRoles(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.Use(async (context, next) =>
{
	Console.WriteLine("User Identity: " + context.User?.Identity?.Name);
	Console.WriteLine("Is Authenticated: " + context.User?.Identity?.IsAuthenticated);

	var user = context.User;

	if (user?.Identity?.IsAuthenticated == true)
	{

		var userManager = context.RequestServices.GetRequiredService<UserManager<User>>();
		var currentUser = await userManager.GetUserAsync(context.User);

		if (currentUser != null)
		{
			var roles = await userManager.GetRolesAsync(currentUser);
			Console.WriteLine("Roles: " + string.Join(", ", roles));

			var path = context.Request.Path;
			Console.WriteLine(path);


            var excludedPaths = new List<string>
            {
                "/Account/Logout",
                "/Product/QuickView",
                "/Product/AddReviewOnProduct",
                "/Product/AddToWishlist",
                "/Product/ShowUserInfo",
                "/Account/Login",
                "/Dashboard/DashBoard",
                "/Product/ProductsStartWith",
                "/Wishlist/Index",
                "/Product/ShowAllProductsSelledBySpecificUser",
                "/Cart/Index",
                "/Order/CheckOut",
                "/Profile/ShowUserInfo",
                "/About/Index",
                "/Product/Laptops",
                "/Product/Accessories",
                "/Product/Cameras",
                "/Product/SmartPhones",
                "/Store/Index",
                "/Product/GetAllProducts",
                "/Admin/Categories",
                "/Product/Create",
                "/Product/ProductAddededBySellerDetails",
                "/Product/EditProductAddededBySeller"
            };

            // استثناء المسارات من التوجيه
            if (excludedPaths.Any(p => context.Request.Path.StartsWithSegments(p)))
            {
                await next();
                return;
            }
            if (roles.Contains("Admin") && !context.Request.Path.StartsWithSegments("/Account/Admin"))
			{
				context.Response.Redirect("/Account/Admin");
				return;
			}
			else if (roles.Contains("User") && !context.Request.Path.StartsWithSegments("/Product/Index"))
			{
				context.Response.Redirect("/Product/Index");
				return;
			}
		}
	}

	await next();
});



app.MapControllerRoute(
	  name: "areas",
	  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");


app.Run();
// ✅ وظيفة إنشاء الأدوار
 async Task CreateRoles(IServiceProvider serviceProvider)
{
	var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

	string[] roleNames = { "Admin", "User" };
	foreach (var roleName in roleNames)
	{
		var roleExists = await roleManager.RoleExistsAsync(roleName);
		if (!roleExists)
		{
			await roleManager.CreateAsync(new IdentityRole(roleName));
		}
	}
}
