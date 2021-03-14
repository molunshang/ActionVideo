using ActionVideo.Models;
using ActionVideo.Views;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace ActionVideo
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            var categories = new List<Category>
            {
                new Category() { TypeId = 75, TypeName = "国产女奴调教" },
                new Category() { TypeId = 78, TypeName = "韩国明星学生" },
                new Category() { TypeId = 81, TypeName = "福利姬" },
                new Category() { TypeId = 79, TypeName = "其他亚洲视频" },
                new Category() { TypeId = 99, TypeName = "亚洲无码" },
                new Category() { TypeId = 83, TypeName = "主播大秀" },
                new Category() { TypeId = 82, TypeName = "精品三级" },
                new Category() { TypeId = 85, TypeName = "国模私拍" },
                new Category() { TypeId = 32, TypeName = "VR有码" },
                new Category() { TypeId = 23, TypeName = "人妻无码" },
                new Category() { TypeId = 26, TypeName = "步兵无码" },
                new Category() { TypeId = 94, TypeName = "野合车震" },
                new Category() { TypeId = 51, TypeName = "国产伦理" },
                new Category() { TypeId = 27, TypeName = "金发幼齿" },
                new Category() { TypeId = 92, TypeName = "国产乱伦" },
                new Category() { TypeId = 87, TypeName = "颜射瞬间" },
                new Category() { TypeId = 93, TypeName = "自慰群交" },
                new Category() { TypeId = 30, TypeName = "韩美眉伦理" },
                new Category() { TypeId = 89, TypeName = "美熟少妇" },
                new Category() { TypeId = 95, TypeName = "职场同事" },
                new Category() { TypeId = 25, TypeName = "骑兵有码" },
                new Category() { TypeId = 97, TypeName = "网曝门事件" },
                new Category() { TypeId = 33, TypeName = "VR无码" },
                new Category() { TypeId = 88, TypeName = "女神学生" },
                new Category() { TypeId = 98, TypeName = "小鸟酱专题" },
                new Category() { TypeId = 29, TypeName = "韩美眉主播" },
                new Category() { TypeId = 22, TypeName = "强奸无码" },
                new Category() { TypeId = 96, TypeName = "国产名人" },
                new Category() { TypeId = 24, TypeName = "制服无码" },
                new Category() { TypeId = 91, TypeName = "空姐模特" },
                new Category() { TypeId = 100, TypeName = "国产偷拍" },
                new Category() { TypeId = 86, TypeName = "[原创]水果派" },
                new Category() { TypeId = 84, TypeName = "抖阴视频" },
                new Category() { TypeId = 67, TypeName = "乱伦中文av" },
                new Category() { TypeId = 68, TypeName = "制服中文av" },
                new Category() { TypeId = 69, TypeName = "人妻中文av" },
                new Category() { TypeId = 70, TypeName = "调教中文av" },
                new Category() { TypeId = 71, TypeName = "出轨中文av" },
                new Category() { TypeId = 73, TypeName = "明星换脸" },
                new Category() { TypeId = 72, TypeName = "精品短视频" },
                new Category() { TypeId = 74, TypeName = "国产女王调教" },
                new Category() { TypeId = 62, TypeName = "自淫系列" },
                new Category() { TypeId = 61, TypeName = "SM捆绑" },
                new Category() { TypeId = 65, TypeName = "强奸中文av" },
                new Category() { TypeId = 64, TypeName = "无码中文av" },
                new Category() { TypeId = 63, TypeName = "拳交系列" },
                new Category() { TypeId = 66, TypeName = "巨乳中文av" },
                new Category() { TypeId = 56, TypeName = "人妻熟女" },
                new Category() { TypeId = 60, TypeName = "制服丝袜" },
                new Category() { TypeId = 58, TypeName = "美女写真" },
                new Category() { TypeId = 59, TypeName = "国产精品" },
                new Category() { TypeId = 54, TypeName = "欧美伦理" },
                new Category() { TypeId = 57, TypeName = "4K岛国" },
                new Category() { TypeId = 52, TypeName = "日本伦理" },
                new Category() { TypeId = 53, TypeName = "韩国伦理" },
                new Category() { TypeId = 55, TypeName = "丝袜美腿" },
                new Category() { TypeId = 48, TypeName = "JAV高清" },
                new Category() { TypeId = 49, TypeName = "动漫" },
                new Category() { TypeId = 50, TypeName = "香港伦理" },
                new Category() { TypeId = 47, TypeName = "欧美性爱" },
                new Category() { TypeId = 46, TypeName = "制服师生" },
                new Category() { TypeId = 44, TypeName = "偷拍自拍" },
                new Category() { TypeId = 43, TypeName = "强奸乱伦" },
                new Category() { TypeId = 40, TypeName = "男同" },
                new Category() { TypeId = 41, TypeName = "女同" },
                new Category() { TypeId = 45, TypeName = "风骚寡妇" },
                new Category() { TypeId = 42, TypeName = "亚洲情色" },
                new Category() { TypeId = 37, TypeName = "明星淫梦" },
                new Category() { TypeId = 38, TypeName = "人兽" },
                new Category() { TypeId = 39, TypeName = "人妖" },
                new Category() { TypeId = 34, TypeName = "名优写真" },
                new Category() { TypeId = 35, TypeName = "名优综艺" },
                new Category() { TypeId = 36, TypeName = "中文字幕" },
                new Category() { TypeId = 20, TypeName = "乱伦无码" },
                new Category() { TypeId = 90, TypeName = "娇妻素人" },
                new Category() { TypeId = 200, TypeName = "萝莉少女" },
                new Category() { TypeId = 201, TypeName = "多人群交" }
            };
            foreach (var category in categories.OrderBy(c => c.TypeId))
            {
                var menuItem = new MenuItem() { Text = category.TypeName, BindingContext = category };
                menuItem.Clicked += async (sender, e) =>
                {
                    Current.FlyoutIsPresented = false;
                    var data = (Category)((MenuItem)sender).BindingContext;
                    await Navigation.PushAsync(new VideosPage(data.TypeId, data.TypeName, false));
                };
                Items.Add(menuItem);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            //todo 连续两次退出
            return base.OnBackButtonPressed();
        }
    }
}
