using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerece.Migrations
{
    /// <inheritdoc />
    public partial class insertproducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
    table: "Products",
    columns: new[] { "Name", "Image", "Description", "IsApproved", "Price", "CategoryId" },
     values: new object[,]
{
            // Laptops
            { "Dell XPS 15", "pic1.jpg", "لابتوب عالي الأداء بمعالج i7.",false, 1500m, 1 },
            { "MacBook Air M2", "pic2.jpeg", "ماك بوك بمعالج M2 الحديث.",false, 1300m, 1 },
            { "HP Spectre x360", "pic3.jpg", "لابتوب نحيف وقوي مع شاشة لمس.",false, 1400m, 1 },
            { "Lenovo ThinkPad X1", "pic4.jpg", "لابتوب للأعمال بأداء رائع.",false, 1600m, 1 },
            { "Asus ROG Zephyrus", "pic5.jpg", "لابتوب مخصص للألعاب بمواصفات قوية.",false, 2000m, 1 },
            { "Acer Predator Helios", "pic6.jpeg", "لابتوب ألعاب بشاشة 144Hz.",false, 1800m, 1 },
            { "MSI Stealth 15M", "pic7.jpg", "لابتوب ألعاب أنيق وخفيف الوزن.", false,1700m, 1 },
            
            // Smartphones
            { "iPhone 15 Pro", "pic8.jpeg", "أحدث هواتف آبل مع كاميرا محسنة.",false, 1200m, 2 },
            { "Samsung Galaxy S23", "pic9.jpg", "هاتف سامسونج الرائد بشاشة AMOLED.",false, 1100m, 2 },
            { "Google Pixel 7", "pic10.png", "هاتف ذكي بكاميرا قوية.",false, 900m, 2 },
            { "OnePlus 11", "pic11.png", "أداء قوي وسرعة شحن فائقة.",false, 800m, 2 },
            { "Xiaomi 13 Pro", "pic12.png", "هاتف بكاميرا متقدمة وسعر رائع.",false, 750m, 2 },
            { "Oppo Find X5", "pic13.jpg", "هاتف أنيق بأداء ممتاز.",false, 850m, 2 },
            { "Sony Xperia 1 V", "pic14.png", "هاتف لمحبي التصوير بكاميرا متطورة.",false, 950m, 2 },
            
            // Cameras
            { "Canon EOS R5", "pic15.jpeg", "كاميرا احترافية بدقة 45 ميجابكسل.",false, 3500m, 3 },
            { "Sony A7 IV", "pic16.jpg", "كاميرا بدقة 33 ميجابكسل مع أداء فيديو رائع.",false, 2500m, 3 },
            { "Nikon Z9", "pic17.jpg", "كاميرا فل فريم للمحترفين.",false, 5000, 3 },
            { "Fujifilm X-T5", "pic18.jpg", "كاميرا مميزة لمحبي الألوان الرائعة.", false,1700m, 3 },
            { "Panasonic Lumix S5", "pic19.jpg", "كاميرا ممتازة للفيديو.",false, 2000m, 3 },
            { "Leica Q2", "pic20.jpg", "كاميرا فاخرة بعدسة 28mm.",false, 5000m, 3 },
            { "GoPro Hero 11", "pic21.jpg", "كاميرا رياضية مقاومة للماء.", false,400m, 3 },
            
            // Accessories
            { "Logitech MX Master 3", "pic22.jpg", "ماوس احترافي مريح.",false, 100m, 4 },
            { "Sony WH-1000XM5", "pic23.jpg", "سماعات لاسلكية بعزل ضوضاء ممتاز.",false, 300m, 4 },
            { "Apple AirPods Pro", "pic24.jpeg", "سماعات أبل اللاسلكية بعزل الضوضاء.", false,250m, 4 },
            { "Samsung Galaxy Buds2 Pro", "pic25.jpeg", "سماعات لاسلكية بجودة صوت رائعة.",false, 200m, 4 },
            { "Razer BlackWidow V4", "pic26.jpg", "لوحة مفاتيح ميكانيكية للألعاب.",false, 150m, 4 },
            { "Anker PowerCore 26800", "pic27.jpeg", "باور بانك بسعة كبيرة.",false, 80m, 4 },
            { "SanDisk Extreme SSD", "pic28.png", "قرص تخزين محمول سريع الأداء.",false, 180m, 4 }
});
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
