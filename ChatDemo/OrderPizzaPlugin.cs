using ChatDemo.Model;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatDemo {
    class OrderPizzaPlugin {
        public static List<Cart> carts = new List<Cart>();

        [KernelFunction("get_pizza_menu")]
        [Description("获取菜单")]
        public Task<List<Menu>> GetPizzaMenuAsync() {
            var ls = new List<Menu>();
            ls.Add(new Menu { Id = "1", Name = "披萨", Price = 25 });
            ls.Add(new Menu { Id = "2", Name = "沙拉", Price = 30 });
            ls.Add(new Menu { Id = "3", Name = "意面", Price = 15 });
            ls.Add(new Menu { Id = "4", Name = "培根", Price = 10 });
            ls.Add(new Menu { Id = "5", Name = "鸡翅", Price = 8 });
            ls.Add(new Menu { Id = "7", Name = "乌蒙", Price = 5 });
            ls.Add(new Menu { Id = "8", Name = "地瓜", Price = 3 });
            return Task.FromResult(ls);
        }

        [KernelFunction("add_pizza_to_cart")]
        [Description("将披萨添加到用户的购物车，返回当前添加的购物车并更新购物车")]
        public Cart AddToCart(int Id, int quantity) {
            var first = this.GetPizzaMenuAsync().Result.Where(x => x.Id == Id.ToString()).FirstOrDefault();
            if (first == null) {
                throw new Exception($"未找到菜单编号为{Id}的菜单");
            }
            var cart = carts.Where(x => x.MenuId == Id.ToString()).FirstOrDefault();
            if (cart == null) {
                carts.Add(new Cart {
                    MenuId = Id.ToString(),
                    Count = quantity,
                    Price = first.Price,
                    SumPrice = quantity * first.Price
                });
            } else {
                cart.Count += quantity;
                cart.SumPrice = cart.Count * cart.Price;
            }
            return carts.Where(x => x.MenuId == Id.ToString()).First(); ;
        }

        [KernelFunction("get_pizza_from_cart")]
        [Description("获取用户当前的购物车")]
        public List<Cart> GetPizzaFromCart() {
            return carts;
        }
    }
}
